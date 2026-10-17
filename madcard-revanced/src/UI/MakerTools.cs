using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json.Linq;

namespace MadCardMaker;

public sealed class MakerReference
{
 public string Field,Type,Id;
 public override string ToString()=>Field+" → "+Type+":"+Id;
}

public static class MakerTools
{
 public static ModCards.PropertyControl[] Schema(string type)
 {
  string schemaType=type;switch(type){case "Addon":schemaType="WeaponAddon";break;case "Keycard":schemaType="Collectible";break;case "Animations":schemaType="Animation";break;case "AnimationProfile":schemaType="AnimSet";break;case "VisualStockpile":schemaType="Texture";break;case "Stage_Arena":schemaType="Arena";break;case "Stage_Story":schemaType="Story";break;case "Stage_Custom":schemaType="CustomStage";break;}
  var method=typeof(ModCards).GetMethods(BindingFlags.Public|BindingFlags.Static).FirstOrDefault(m=>m.Name.Equals("Statcard_"+schemaType,StringComparison.OrdinalIgnoreCase)&&m.GetParameters().Length==0);
  return method==null?new ModCards.PropertyControl[0]:(ModCards.PropertyControl[])method.Invoke(null,null);
 }
 public static IEnumerable<MakerReference> References(string type,JObject card)
 {
  if(card["Root"] is JValue root&&!string.IsNullOrEmpty(root.ToString()))yield return new MakerReference{Field="Root",Type=type,Id=root.ToString()};
  foreach(var r in Walk(card,Schema(type),""))yield return r;
 }
 static IEnumerable<MakerReference> Walk(JObject card,ModCards.PropertyControl[] schema,string prefix)
 {
  foreach(var field in schema){var value=card[field.PropName];if(value==null)continue;var values=field.IsList&&value is JArray list?list.ToArray():new[]{value};
   for(int i=0;i<values.Length;i++){var item=values[i];var path=prefix+field.PropName+(field.IsList?"["+i+"]":"");
    if(field.PropType==ModCards.PropType.Ref&&!field.WriteNewValues&&!string.IsNullOrEmpty(field.ObjectName)&&field.ObjectName!="Object"&&item is JValue&&item.Type==JTokenType.String){var id=item.ToString();if(!string.IsNullOrEmpty(id)&&id!="None"&&id!="empty")yield return new MakerReference{Field=path,Type=field.ObjectName=="SkillGroups"?"SkillGroup":field.ObjectName,Id=id};}
    if(item is JObject child&&field.Defines!=null)foreach(var r in Walk(child,field.Defines,path+"."))yield return r;
   }
  }
 }
 public static JObject SetScalar(string type,JObject original,string field,JToken value)
 {
  var prop=Schema(type).FirstOrDefault(p=>p.PropName==field&&!p.IsList&&p.PropType!=ModCards.PropType.Obj&&p.PropType!=ModCards.PropType.Color);
  if(prop==null||new[]{"Filename","Root","Priority"}.Contains(field))throw new FormatException("Choose an editable scalar field from this card type.");
  bool valid=value is JValue&&(
   prop.PropType==ModCards.PropType.Bool&&value.Type==JTokenType.Boolean||
   prop.PropType==ModCards.PropType.Int&&value.Type==JTokenType.Integer||
   prop.PropType==ModCards.PropType.Float&&(value.Type==JTokenType.Integer||value.Type==JTokenType.Float)||
   (prop.PropType==ModCards.PropType.Str||prop.PropType==ModCards.PropType.Ref)&&value.Type==JTokenType.String);
  if(!valid)throw new FormatException("The value does not match "+field+" ("+prop.PropType+"). Use JSON syntax: 12, true, or \"text\".");
  if(prop.PropType==ModCards.PropType.Ref&&!prop.WriteNewValues&&!string.IsNullOrEmpty(prop.ObjectName)&&prop.ObjectName!="Object"&&!MakerStudio.Exists(prop.ObjectName,value.ToString()))throw new FormatException("That reference is missing from the loaded "+prop.ObjectName+" database.");
  if(value.Type==JTokenType.Float&&(double.IsInfinity((double)value)||double.IsNaN((double)value)))throw new FormatException("Enter a finite number.");
  var copy=(JObject)original.DeepClone();copy[field]=value.DeepClone();return copy;
 }
 public static string PresetPath=>Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),"MadCardMakerRevanced","presets.json");
 public static JArray ReadPresets(string path=null)=>File.Exists(path??PresetPath)?JArray.Parse(File.ReadAllText(path??PresetPath)):new JArray();
 public static void SavePreset(string name,string type,JObject card,string path=null)
 {
  if(string.IsNullOrWhiteSpace(name))throw new FormatException("Give this template a name.");
  if(MakerStudio.Check(type,card).Any(x=>x.Error))throw new FormatException("Fix the card ID and inherited root before saving a template.");
  var all=ReadPresets(path);all.Add(new JObject{{"Name",name.Trim()},{"Type",type},{"Card",card.DeepClone()}});MakerHelp.Write(path??PresetPath,all.ToString());
 }
 public static int Package(string folder,string destination)
 {
  folder=Path.GetFullPath(folder).TrimEnd(Path.DirectorySeparatorChar)+Path.DirectorySeparatorChar;destination=Path.GetFullPath(destination);
  if(destination.StartsWith(folder,StringComparison.OrdinalIgnoreCase))throw new IOException("Save the ZIP outside the mod folder.");
  var files=Directory.GetFiles(folder,"*",SearchOption.AllDirectories).Where(f=>!f.EndsWith(".bak",StringComparison.OrdinalIgnoreCase)&&!f.EndsWith(".tmp",StringComparison.OrdinalIgnoreCase)).OrderBy(f=>f,StringComparer.OrdinalIgnoreCase).ToArray();
  if(files.Length==0)throw new IOException("This mod has no saved files to package.");
  if((File.GetAttributes(folder)&FileAttributes.ReparsePoint)!=0||Directory.GetDirectories(folder,"*",SearchOption.AllDirectories).Any(d=>(File.GetAttributes(d)&FileAttributes.ReparsePoint)!=0))throw new IOException("Package a local mod folder without linked folders.");
  string temp=destination+"."+Guid.NewGuid().ToString("N")+".tmp";
  try{using(var zip=ZipFile.Open(temp,ZipArchiveMode.Create))foreach(var file in files){if((File.GetAttributes(file)&FileAttributes.ReparsePoint)!=0)throw new IOException("Linked files cannot be packaged.");zip.CreateEntryFromFile(file,file.Substring(folder.Length).Replace('\\','/'),CompressionLevel.Optimal);}
   if(File.Exists(destination))File.Replace(temp,destination,destination+".bak");else File.Move(temp,destination);
  }finally{if(File.Exists(temp))File.Delete(temp);}return files.Length;
 }
}
