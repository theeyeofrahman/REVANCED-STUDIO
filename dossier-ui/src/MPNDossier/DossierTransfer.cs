using System;
using System.IO;
using Newtonsoft.Json.Linq;
namespace MPNDossier;

public static class DossierTransfer
{
 public static string ConfigPathOverrideForTests{get;set;}
 public static string ConfigPath=>ConfigPathOverrideForTests??Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"config.json");
 public static JObject ReadSettings(){return File.Exists(ConfigPath)?JObject.Parse(File.ReadAllText(ConfigPath)):new JObject();}
 public static void Configure(string folder,bool automatic)
 {
  folder=Path.GetFullPath(folder);if(!Directory.Exists(folder))throw new DirectoryNotFoundException("Choose an existing Custom Stuff folder.");
  var settings=ReadSettings();settings["Folder"]=folder;settings["AutoTransferOnSave"]=automatic;
  var temporary=ConfigPath+".tmp";File.WriteAllText(temporary,settings.ToString());if(File.Exists(ConfigPath))File.Replace(temporary,ConfigPath,ConfigPath+".bak");else File.Move(temporary,ConfigPath);
  ModEnums.DIR_TRANSFER=folder;
 }
 public static string AfterSave(string source,string kind)
 {
  if(DossierPreferences.SuppressPersistenceForTests&&ConfigPathOverrideForTests==null)return "";
  try{
   var settings=ReadSettings();if(settings["AutoTransferOnSave"]?.Type!=JTokenType.Boolean||!(bool)settings["AutoTransferOnSave"])return "";
   var folder=settings["Folder"]?.ToString();if(string.IsNullOrWhiteSpace(folder))throw new IOException("Choose your game's Custom Stuff folder in Game folder / auto-send.");
   ModEnums.DIR_TRANSFER=folder;var destination=CopySavedFile(source,kind,folder);return " • Sent to game: "+destination;
  }catch(Exception ex){return " • Saved locally; game copy failed: "+ex.Message;}
 }
 public static string CopySavedFile(string source,string kind,string root)
 {
  // The editor schema supplies the kind: ambiguous JSON is never guessed from filenames.
  if(!DossierDecks.EntryKinds.ContainsKey(kind??""))throw new ArgumentException("This entry type has no game destination.");
  source=Path.GetFullPath(source);if(!Path.GetExtension(source).Equals(".json",StringComparison.OrdinalIgnoreCase))throw new ArgumentException("Only completed JSON saves can be sent to the game.");
  DossierRoomDocument.Parse(File.ReadAllText(source));
  root=Path.GetFullPath(root).TrimEnd(Path.DirectorySeparatorChar,Path.AltDirectorySeparatorChar);
  if(!Directory.Exists(root))throw new DirectoryNotFoundException("The configured Custom Stuff folder is missing.");
  var destination=Path.GetFullPath(Path.Combine(root,kind,Path.GetFileName(source)));
  if(!destination.StartsWith(root+Path.DirectorySeparatorChar,StringComparison.OrdinalIgnoreCase))throw new IOException("Invalid game destination.");
  if(destination.Equals(source,StringComparison.OrdinalIgnoreCase))return destination;
  // Refuse junctions/symlinks that could redirect a category outside the configured folder.
  var category=Path.GetDirectoryName(destination);
  if(Directory.Exists(category)&&(File.GetAttributes(category)&FileAttributes.ReparsePoint)!=0)throw new IOException("Choose a regular category folder instead of a linked folder.");
  if(File.Exists(destination)&&(File.GetAttributes(destination)&FileAttributes.ReparsePoint)!=0)throw new IOException("The destination is a linked file.");
  if(File.Exists(destination)&&File.ReadAllText(destination)==File.ReadAllText(source))return destination;
  Directory.CreateDirectory(category);var temporary=destination+"."+Guid.NewGuid().ToString("N")+".tmp";
  try{File.Copy(source,temporary);if(File.Exists(destination))File.Replace(temporary,destination,destination+".bak");else File.Move(temporary,destination);}
  finally{if(File.Exists(temporary))File.Delete(temporary);}
  return destination;
 }
}
