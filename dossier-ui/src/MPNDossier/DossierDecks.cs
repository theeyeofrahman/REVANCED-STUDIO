using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
namespace MPNDossier;
public sealed class DossierDeck
{
 public string Name{get;set;}="New deck";
 public List<DossierDeckFolder> Folders{get;set;}=new List<DossierDeckFolder>();
 public override string ToString()=>Name;
}
public sealed class DossierDeckFolder
{
 public string Path{get;set;}
 public string Kind{get;set;}="Files";
 public override string ToString()=>Kind+" — "+Path;
}
public sealed class DossierDecks
{
 public List<DossierDeck> Decks{get;set;}=new List<DossierDeck>();
 public static string StorePath=>System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),"MPNDossier","decks.json");
 public static DossierDecks Load(string path=null)
 {
  path=path??StorePath;if(!File.Exists(path))return new DossierDecks();
  var store=JsonConvert.DeserializeObject<DossierDecks>(File.ReadAllText(path));if(store==null)throw new FormatException("Deck file is empty.");
  store.Decks=(store.Decks??new List<DossierDeck>()).Where(d=>d!=null).ToList();
  foreach(var deck in store.Decks)deck.Folders=(deck.Folders??new List<DossierDeckFolder>()).Where(f=>f!=null&&!string.IsNullOrWhiteSpace(f.Path)).ToList();
  return store;
 }
 public void Save(string path=null)
 {
  path=path??StorePath;Directory.CreateDirectory(System.IO.Path.GetDirectoryName(System.IO.Path.GetFullPath(path)));var temp=path+".tmp";File.WriteAllText(temp,JsonConvert.SerializeObject(this,Formatting.Indented));if(File.Exists(path))File.Replace(temp,path,path+".bak");else File.Move(temp,path);
 }
 public static void AddFolder(DossierDeck deck,string path,string kind)
 {
  var full=System.IO.Path.GetFullPath(path).TrimEnd(System.IO.Path.DirectorySeparatorChar);
  if(!Directory.Exists(full))throw new DirectoryNotFoundException("This folder is missing.");
  if(deck.Folders.Any(f=>string.Equals(System.IO.Path.GetFullPath(f.Path).TrimEnd(System.IO.Path.DirectorySeparatorChar),full,StringComparison.OrdinalIgnoreCase)))throw new InvalidOperationException("That folder is already in this deck.");
  deck.Folders.Add(new DossierDeckFolder{Path=System.IO.Path.GetFullPath(path),Kind=kind});
 }
 public static List<string> Scan(string path,List<string> issues,int limit=5000)
 {
  var files=new List<string>();var pending=new Stack<string>();pending.Push(path);
  while(pending.Count>0&&files.Count<limit){var current=pending.Pop();try{
   foreach(var file in Directory.GetFiles(current).OrderBy(f=>f,StringComparer.OrdinalIgnoreCase)){files.Add(file);if(files.Count==limit){issues.Add("Showing the first "+limit+" files. Choose a smaller folder to see more.");break;}}
   foreach(var child in Directory.GetDirectories(current).OrderByDescending(f=>f,StringComparer.OrdinalIgnoreCase))if((File.GetAttributes(child)&FileAttributes.ReparsePoint)==0)pending.Push(child);
  }catch(IOException ex){issues.Add(current+": "+ex.Message);}catch(UnauthorizedAccessException ex){issues.Add(current+": "+ex.Message);}}
  return files;
 }
 public static readonly Dictionary<string,string> EntryKinds=new Dictionary<string,string>{
  {"rooms","Rooms"},{"characters","Character"},{"armor","Armor"},{"weapons","Weapon"},{"weaponmodded","ModdedWeapon"},{"ranged","Ranged"},{"thrown","Thrown"},{"ammo","Munition"},{"weaponaddons","WeaponAddon"},{"weaponaddongroups","AddonGroup"},{"animsets","AnimationSet"},{"animation","Animation"},{"chatter","Chatter"},{"corpus","Corpus"},{"dot","DoTEffect"},{"loadouts","Normal"},{"storyloadouts","StoryToolStripMenuItem1"},{"origins","Origin"},{"skillgroups","SkillGroup"},{"wardrobe","Wardrobe"},{"swatches","Swatch"},{"skins","Skin"},{"executions","Execution"},{"explosions","Explosion"},{"stories","Story"},{"arenas","Arena"}};
}
