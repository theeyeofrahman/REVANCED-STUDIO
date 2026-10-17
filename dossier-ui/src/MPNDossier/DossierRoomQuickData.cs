using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace MPNDossier;

// This view combines scene defaults with overrides for display only. Writes touch only edited paths.
public static class DossierRoomQuickData
{
 public sealed class Entry
 {
  public object[] Path; public JToken Value; public string Label;
  public string Key=>JsonConvert.SerializeObject(Path);
  public override string ToString()=>Label;
 }
 public static JObject Effective(JObject data,JObject template)
 {
  var result=template==null?new JObject():(JObject)template.DeepClone();
  Overlay(result,data);return result;
 }
 static void Overlay(JObject target,JObject source)
 {
  foreach(var p in source.Properties()){
   if(target[p.Name] is JObject nested&&p.Value is JObject obj)Overlay(nested,obj);
   else if(p.Name=="Steps"&&target[p.Name] is JArray steps&&p.Value is JArray overrides&&steps.All(s=>s is JObject o&&o["Index"]!=null)&&overrides.All(s=>s is JObject o&&o["Index"]!=null)){
    // The game applies step overrides by explicit Index, not by array position.
    foreach(JObject step in overrides){var match=steps.OfType<JObject>().FirstOrDefault(s=>s["Index"].ToString()==step["Index"].ToString());if(match==null)steps.Add(step.DeepClone());else Overlay(match,step);}
   }else target[p.Name]=p.Value.DeepClone();
  }
 }
 public static JToken At(JToken root,IEnumerable<object> path)
 {
  foreach(var part in path){if(root is JObject obj&&part is string key)root=obj[key];else if(root is JArray a&&part is int index&&index>=0&&index<a.Count)root=a[index];else return null;}return root;
 }
 public static void Write(JObject data,JObject effective,object[] path,JToken value)
 {
  JToken target=data,source=effective;
  for(int i=0;i<path.Length;i++){
   var part=path[i];bool last=i==path.Length-1;var sourceChild=At(source,new[]{part});
   if(target is JObject obj&&part is string key){
    if(last){obj[key]=value.DeepClone();return;}
    if(obj[key]==null){obj[key]=i==0?DossierRoomDocument.EmptyRoom():sourceChild is JArray?sourceChild.DeepClone():new JObject();}
    target=obj[key];
   }else if(target is JArray array&&part is int n){
    if(i>0&&path[i-1] is string collection&&collection=="Steps"&&sourceChild is JObject step&&step["Index"]!=null){
     var match=array.OfType<JObject>().FirstOrDefault(s=>s["Index"]?.ToString()==step["Index"].ToString());
     if(match==null){match=new JObject(new JProperty("Index",step["Index"].DeepClone()));array.Add(match);}n=array.IndexOf(match);
    }
    if(n<0||n>=array.Count)throw new InvalidOperationException("This dialogue list changed. Reopen quick edit.");
    if(last){array[n]=value.DeepClone();return;}target=array[n];
   }
   else throw new InvalidOperationException("This room field has an incompatible structure. Use Advanced view to review it.");
   source=sourceChild;
  }
 }
 public static string CharacterName(JObject actor)
 {
  string id=actor?["myType"]?.ToString();JObject card;
  return !string.IsNullOrWhiteSpace(id)&&ModEnums.DB_CHAR.TryGetValue(id,out card)?(card["FullName"]?.ToString()??id):id??"Scene character";
 }
 static int? Number(JToken value){int n;return int.TryParse(value?.ToString(),out n)?n:(int?)null;}
 public static string Role(JObject actor)
 {
  int? controller=Number(actor?["myControlType"]),faction=Number(actor?["myFaction"]);JObject card;
  if(ModEnums.DB_CHAR.TryGetValue(actor?["myType"]?.ToString()??"",out card)){
   if(controller==100)controller=Number(card["DefaultControl"]);
   if(faction==100)faction=Number(card["DefaultFaction"]);
  }
  if(controller==1)return "PLAYER";
  if(controller==2)return "NETWORK PLAYER";
  if(faction==10)return "NEUTRAL";
  if(faction==0)return controller.HasValue&&controller!=100?"ALLY":"PLAYER SIDE (controller unknown)";
  // The game's default attackability rule treats different non-neutral factions as hostile to Player (0).
  if(faction.HasValue&&faction!=100)return "ENEMY";
  return "SCENE DEFAULT";
 }
 public static string ActorLabel(string id,JObject actor)
 {
  string name=(actor?["DisplayedName"]??actor?["myName"])?.ToString();
  return Role(actor)+" · "+(string.IsNullOrWhiteSpace(name)?CharacterName(actor):name)+" · "+id;
 }
 public static List<Entry> Characters(JObject effective)
 {
  var entries=new List<Entry>();foreach(var room in effective.Properties())if(room.Value is JObject layout&&layout["Characters"] is JObject actors)
   foreach(var actor in actors.Properties())if(actor.Value is JObject obj&&(actor.Name.StartsWith("C-",StringComparison.Ordinal)||obj["myControlType"]!=null))
    entries.Add(new Entry{Path=new object[]{room.Name,"Characters",actor.Name},Value=obj,Label=ActorLabel(actor.Name,obj)+" / "+room.Name});
  return entries;
 }
 static readonly HashSet<string> DialogueKeys=new HashSet<string>{"Blurb","mySpeech","CustomTalk","CustomBye"};
 public static List<Entry> Dialogue(JObject effective)
 {
  var entries=new List<Entry>();Visit(effective,new List<object>(),false,entries,effective);return entries;
 }
 static void Visit(JToken token,List<object> path,bool dialogue,List<Entry> entries,JObject root)
 {
  if(token is JObject obj){foreach(var p in obj.Properties()){var next=new List<object>(path){p.Name};Visit(p.Value,next,DialogueKeys.Contains(p.Name),entries,root);}}
  else if(token is JArray a){for(int i=0;i<a.Count;i++){var next=new List<object>(path){i};Visit(a[i],next,dialogue,entries,root);}}
  else if(dialogue&&token.Type==JTokenType.String){
   var parent=token.Parent is JProperty p?p.Parent as JObject:null;string speaker=parent?["ActorSerial"]?.ToString();
   var step=parent?["Index"]?.ToString();string prefix=string.Join(" / ",path.Take(3));
   var text=token.ToString().Replace("\r"," ").Replace("\n"," ");if(text.Length>85)text=text.Substring(0,85)+"…";
   string lineLabel=step!=null?"Step "+step:path.Last() is int n?(path.Contains("CustomBye")?"Goodbye":path.Contains("CustomTalk")?"Conversation":"Speech")+" line "+(n+1):"Dialogue";
   entries.Add(new Entry{Path=path.ToArray(),Value=token,Label=lineLabel+" · "+Speaker(root,path[0].ToString(),speaker)+" — "+(text.Length==0?"(empty line)":text)+" ["+prefix+"]"});
  }
 }
 public static string Speaker(JObject root,string room,string serial)
 {
  if(string.IsNullOrEmpty(serial))return "No speaker / narration";
  if(serial.StartsWith("PSQUAD",StringComparison.Ordinal))return "Player squad · "+serial;
  var actors=Characters(root).Where(e=>(string)e.Path[0]==room);
  var match=actors.FirstOrDefault(e=>(e.Value["AssignSerialNumber"]?.ToString()==serial)||(string)e.Path[2]==serial);
  return match==null?serial:ActorLabel((string)match.Path[2],(JObject)match.Value)+" ["+serial+"]";
 }
}
