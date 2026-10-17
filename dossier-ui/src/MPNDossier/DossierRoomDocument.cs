using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace MPNDossier;
public sealed class DossierRoomDocument
{
 public static readonly string[] Sections={"Characters","Interactives","Cutscenes","Lockers","Areas","Items","Entrances"};
 public JObject Data{get;private set;}=new JObject();
 public string FilePath{get;private set;}
 readonly List<string> history=new List<string>();int index=-1;string saved="{}";bool unsavedCopy;
 public DossierRoomDocument(){Remember();}
 public bool Dirty=>unsavedCopy||Data.ToString(Formatting.None)!=saved;
 public bool CanUndo=>index>0;public bool CanRedo=>index+1<history.Count;
 public static JObject Parse(string text)
 {
  using(var reader=new JsonTextReader(new StringReader(text)){DateParseHandling=DateParseHandling.None,MaxDepth=128}){
   var root=JObject.Load(reader);if(reader.Read())throw new FormatException("Unexpected content after the room JSON.");return root;
  }
 }
 public void Load(string path){if(new FileInfo(path).Length>10*1024*1024)throw new InvalidDataException("Room files must be smaller than 10 MB.");Set(Parse(File.ReadAllText(path)),Path.GetFullPath(path));}
 public void Set(JObject data,string path=null){Data=(JObject)data.DeepClone();FilePath=path;unsavedCopy=false;history.Clear();index=-1;saved=Data.ToString(Formatting.None);Remember();}
 public void MakeCopy(bool markDirty=true){FilePath=null;unsavedCopy=markDirty;}
 public void Remember(){string text=Data.ToString(Formatting.None);if(index>=0&&history[index]==text)return;if(index+1<history.Count)history.RemoveRange(index+1,history.Count-index-1);history.Add(text);if(history.Count>100)history.RemoveAt(0);index=history.Count-1;}
 public void Change(Action action){string before=Data.ToString(Formatting.None);try{action();Remember();}catch{Data=Parse(before);throw;}}
 public void Undo(){if(CanUndo)Data=Parse(history[--index]);}
 public void Redo(){if(CanRedo)Data=Parse(history[++index]);}
 public List<string> Validate()
 {
  var errors=new List<string>();
  foreach(var room in Data.Properties()){
   if(!(room.Value is JObject layout)){errors.Add(room.Name+": room contents must be an object.");continue;}
   foreach(var section in Sections){var value=layout[section];if(value==null)continue;
    if(section=="Items"){if(!(value is JArray items))errors.Add(room.Name+" / Items must be an array.");else if(items.Any(t=>!(t is JObject)))errors.Add(room.Name+" / Items entries must be objects.");}
    else if(!(value is JObject))errors.Add(room.Name+" / "+section+" must be an object containing IDs or shared overrides.");
   }
  }
  return errors;
 }
 public void Save(string path)
 {
  var errors=Validate();if(errors.Count>0)throw new InvalidDataException(string.Join("\r\n",errors.Take(8)));
  path=Path.GetFullPath(path);string temp=path+".tmp";File.WriteAllText(temp,Data.ToString(Formatting.Indented));if(File.Exists(path))File.Replace(temp,path,path+".bak");else File.Move(temp,path);FilePath=path;unsavedCopy=false;saved=Data.ToString(Formatting.None);
 }
 public static JObject EmptyRoom()=>new JObject(Sections.Select(s=>new JProperty(s,s=="Items"?(JToken)new JArray():new JObject())));
 public static JToken ReadValue(string type,string text)
 {
  switch(type){
   case "String":return new JValue(text);
   case "Integer":long integer;if(!long.TryParse(text,System.Globalization.NumberStyles.Integer,System.Globalization.CultureInfo.InvariantCulture,out integer))throw new FormatException("Enter a whole number.");return new JValue(integer);
   case "Number":double number;if(!double.TryParse(text,System.Globalization.NumberStyles.Float,System.Globalization.CultureInfo.InvariantCulture,out number)||double.IsNaN(number)||double.IsInfinity(number))throw new FormatException("Enter a finite decimal number, using a dot.");return new JValue(number);
   case "Boolean":bool flag;if(!bool.TryParse(text,out flag))throw new FormatException("Use true or false.");return new JValue(flag);
   case "Null":return new JValue((object)null);
   case "Object":return Parse(text);
   case "Array":using(var reader=new JsonTextReader(new StringReader(text)){DateParseHandling=DateParseHandling.None,MaxDepth=128}){var array=JArray.Load(reader);if(reader.Read())throw new FormatException("Unexpected content after the array.");return array;}
   default:throw new FormatException("Choose a JSON value type.");
  }
 }
 public static string TypeName(JToken token)=>token.Type==JTokenType.Object?"Object":token.Type==JTokenType.Array?"Array":token.Type==JTokenType.Integer?"Integer":token.Type==JTokenType.Float?"Number":token.Type==JTokenType.Boolean?"Boolean":token.Type==JTokenType.Null?"Null":"String";
}
