using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Linq;
namespace MadCardMaker;
public static class MakerFavorites
{
 public static string FilePath=>Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),"MadCardMakerRevanced","favorites.json");
 public static HashSet<string> Read(string path=null)=>new HashSet<string>(File.Exists(path??FilePath)?JArray.Parse(File.ReadAllText(path??FilePath)).Values<string>():Enumerable.Empty<string>(),StringComparer.OrdinalIgnoreCase);
 public static bool Toggle(string type,string id,string path=null)
 {
  if(!ModEnums.CARDTYPES.Contains(type)||string.IsNullOrWhiteSpace(id))throw new ArgumentException("Choose a reference card first.");var all=Read(path);string key=type+":"+id;bool added=all.Add(key);if(!added)all.Remove(key);MakerHelp.Write(path??FilePath,new JArray(all.OrderBy(x=>x,StringComparer.OrdinalIgnoreCase)).ToString());return added;
 }
}
