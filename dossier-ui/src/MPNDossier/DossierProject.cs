using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace MPNDossier;
public sealed class DossierProjectFolder
{
 public string Path{get;set;}
 public string Kind{get;set;}="Files";
 public override string ToString()=>Kind+" — "+Path;
}
public sealed class DossierProjectFile
{
 public string Path{get;set;}
 public string RelativeName{get;set;}
 public DossierProjectFolder Folder{get;set;}
 public override string ToString()=>Folder.Kind+" · "+RelativeName;
}
public sealed class DossierProject
{
 public const string ManifestName="dossier-project.json";
 public int Version{get;set;}=1;
 public string Name{get;set;}="New project";
 public string Description{get;set;}="";
 public List<DossierProjectFolder> Folders{get;set;}=new List<DossierProjectFolder>();
 [JsonIgnore]public IDictionary<string,JToken> Extra{get;set;}
 [JsonIgnore]public string ManifestPath{get;private set;}
 [JsonIgnore]public string Root=>System.IO.Path.GetDirectoryName(ManifestPath);
 [JsonIgnore]public string IdeasPath=>System.IO.Path.Combine(Root,"project-ideas.json");
 public static DossierProject Create(string root)
 {
  root=System.IO.Path.GetFullPath(root);Directory.CreateDirectory(root);var path=System.IO.Path.Combine(root,ManifestName);
  if(File.Exists(path))throw new IOException("This folder already has a project. Use Open project.");
  var project=new DossierProject{Name=new DirectoryInfo(root).Name,ManifestPath=path};
  foreach(var kind in new[]{"characters","weapons","armor","rooms","assets","exports"}){var folder=System.IO.Path.Combine(root,kind);Directory.CreateDirectory(folder);project.Folders.Add(new DossierProjectFolder{Path=kind,Kind=DossierDecks.EntryKinds.ContainsKey(kind)?kind:"Files"});}
  project.Save();return project;
 }
 public static DossierProject Load(string path)
 {
  path=System.IO.Path.GetFullPath(path);var raw=JObject.Parse(File.ReadAllText(path));if(raw["Version"]?.Type!=JTokenType.Integer||raw["Name"]?.Type!=JTokenType.String||!(raw["Folders"] is JArray))throw new FormatException("Choose dossier-project.json, rather than an entry JSON file.");var project=JsonConvert.DeserializeObject<DossierProject>(raw.ToString());
  if(project==null||project.Version!=1||string.IsNullOrWhiteSpace(project.Name)||project.Folders==null)throw new FormatException("This is not a supported Dossier project file.");
  project.Extra=raw.Properties().Where(p=>!new[]{"Version","Name","Description","Folders"}.Contains(p.Name)).ToDictionary(p=>p.Name,p=>p.Value.DeepClone());project.ManifestPath=path;foreach(var f in project.Folders){if(f==null||string.IsNullOrWhiteSpace(f.Path)||!ValidKind(f.Kind))throw new FormatException("The project has an invalid folder link.");project.Resolve(f);}return project;
 }
 public static bool ValidKind(string kind)=>kind=="Files"||kind!=null&&DossierDecks.EntryKinds.ContainsKey(kind);
 public string Resolve(DossierProjectFolder folder)=>System.IO.Path.GetFullPath(System.IO.Path.IsPathRooted(folder.Path)?folder.Path:System.IO.Path.Combine(Root,folder.Path));
 string StoredPath(string path){var full=System.IO.Path.GetFullPath(path);var prefix=Root.TrimEnd('\\','/')+System.IO.Path.DirectorySeparatorChar;return full.StartsWith(prefix,StringComparison.OrdinalIgnoreCase)?full.Substring(prefix.Length):full;}
 public bool LinkFolder(string path,string kind)
 {
  path=System.IO.Path.GetFullPath(path);if(!Directory.Exists(path))throw new DirectoryNotFoundException("Choose an existing folder.");if(!ValidKind(kind))throw new ArgumentException("Choose a supported entry type.");
  if(Folders.Any(f=>SamePath(Resolve(f),path)))return false;
  Folders.Add(new DossierProjectFolder{Path=StoredPath(path),Kind=kind});return true;
 }
 static bool SamePath(string a,string b)=>string.Equals(a.TrimEnd('\\','/'),b.TrimEnd('\\','/'),StringComparison.OrdinalIgnoreCase);
 public int LinkDeck(DossierDeck deck){int added=0;foreach(var folder in deck.Folders)if(LinkFolder(folder.Path,folder.Kind))added++;return added;}
 public void Save()
 {
  if(string.IsNullOrWhiteSpace(Name))throw new ArgumentException("Give your project a name.");
  var temp=ManifestPath+".tmp";File.WriteAllText(temp,ToJson());if(File.Exists(ManifestPath))File.Replace(temp,ManifestPath,ManifestPath+".bak");else File.Move(temp,ManifestPath);
 }
 string ToJson(){var obj=JObject.Parse(JsonConvert.SerializeObject(this));if(Extra!=null)foreach(var pair in Extra)if(obj.Property(pair.Key)==null)obj.Add(pair.Key,pair.Value.DeepClone());return obj.ToString();}
 public void Commit(Action change){var snapshot=JsonConvert.SerializeObject(this);var extras=Extra;try{change();Save();}catch{var before=JsonConvert.DeserializeObject<DossierProject>(snapshot);Name=before.Name;Description=before.Description;Folders=before.Folders;Extra=extras;throw;}}
 public List<DossierProjectFile> Scan(List<string> issues,int limit=5000)
 {
  var found=new List<DossierProjectFile>();var seen=new HashSet<string>(StringComparer.OrdinalIgnoreCase);
  foreach(var folder in Folders){if(found.Count>=limit){issues.Add("Showing the first "+limit+" files. Filter by folder or organize a smaller project.");break;}string root=Resolve(folder);if(!Directory.Exists(root)){issues.Add("Missing folder: "+root);continue;}
   foreach(var file in DossierDecks.Scan(root,issues,limit-found.Count))if(seen.Add(file)&&!SamePath(file,ManifestPath)&&!SamePath(file,IdeasPath)&&!file.EndsWith(".bak",StringComparison.OrdinalIgnoreCase)&&!file.EndsWith(".tmp",StringComparison.OrdinalIgnoreCase))found.Add(new DossierProjectFile{Path=file,Folder=folder,RelativeName=file.Substring(root.TrimEnd('\\','/').Length).TrimStart('\\','/')});
  }return found;
 }
 public List<string> ImportFiles(DossierProjectFolder target,IEnumerable<string> paths)
 {
  if(!Folders.Contains(target))throw new ArgumentException("Choose a project folder first.");string root=Resolve(target);if(!Directory.Exists(root))throw new DirectoryNotFoundException("This project folder is missing. Relink it before importing files.");
  var imported=new List<string>();foreach(var path in paths){var source=System.IO.Path.GetFullPath(path);if(!File.Exists(source))throw new FileNotFoundException("An import file is missing.",source);string name=System.IO.Path.GetFileName(source),destination=System.IO.Path.Combine(root,name);if(SamePath(source,destination))continue;int index=2;while(File.Exists(destination)||Directory.Exists(destination))destination=System.IO.Path.Combine(root,System.IO.Path.GetFileNameWithoutExtension(name)+" ("+(index++)+ ")"+System.IO.Path.GetExtension(name));File.Copy(source,destination,false);imported.Add(destination);}return imported;
 }
}
