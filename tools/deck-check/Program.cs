using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MadCardMaker;
using Newtonsoft.Json.Linq;
class DeckCheck
{
 static string database;
 static void Lists(Dictionary<string,List<string>> target,string file){foreach(var p in JObject.Parse(File.ReadAllText(Path.Combine(database,file))).Properties())target[p.Name]=p.Value.Select(v=>v.ToString()).ToList();}
 static bool Stock(string type,string id){
  if(MakerStudio.Exists(type,id))return true;
  foreach(string folder in type=="Weapon"?new[]{"Melee","Ranged","Thrown"}:type=="Item"?new[]{"Armor","Keycard","Melee","Ranged","Thrown"}:new[]{type}){
   string file=Path.Combine(database,"base",folder,id+".json");if(!File.Exists(file))continue;
   if(!ModEnums.SC_DIR.ContainsKey(type))ModEnums.SC_DIR[type]=new SortedDictionary<string,JObject>(StringComparer.OrdinalIgnoreCase);
   ModEnums.SC_DIR[type][id]=JObject.Parse(File.ReadAllText(file));return true;
  }return false;
 }
 [STAThread]static int Main(string[] args){try{
  Console.WriteLine("Reading deck inputs");database=Path.GetFullPath(args[0]);var deck=new JObject();
  foreach(string file in args.Skip(1))foreach(var group in JObject.Parse(File.ReadAllText(file)).Properties()){
   if(!(group.Value is JArray))throw new Exception("Expected a typed card array: "+group.Name);
   if(!(deck[group.Name] is JArray))deck[group.Name]=new JArray();foreach(var card in group.Value)((JArray)deck[group.Name]).Add(card.DeepClone());
  }
  DossierPreferences.SuppressPersistenceForTests=true;DossierFeedback.MutedForTests=true;DossierMotion.SuppressForTests=true;DossierDialog.PreventDialogsForTests=true;
  Lists(ModEnums.ENUMS,"DefaultEnums.json");Lists(ModEnums.ASSETS,"DefaultAssets.json");
  ModEnums.TT_PROPERTIES=JObject.Parse(File.ReadAllText(Path.Combine(database,"Tooltips_Obj.json")));ModEnums.TT_ENUMS=JObject.Parse(File.ReadAllText(Path.Combine(database,"Tooltips_Enums.json")));
  foreach(string type in ModEnums.CARDTYPES.Concat(new[]{"Item","Weapon"})){ModEnums.SC_DIR[type]=new SortedDictionary<string,JObject>(StringComparer.OrdinalIgnoreCase);ModEnums.MC_DIR[type]=new SortedDictionary<string,JObject>(StringComparer.OrdinalIgnoreCase);}
  int total=0,references=0;var errors=new List<string>();
  foreach(var group in deck.Properties())foreach(JObject card in group.Value){
   if(!ModEnums.MC_DIR.ContainsKey(group.Name))throw new Exception("Unknown card type: "+group.Name);
   string id=(string)card["Filename"],root=(string)card["Root"];
   if(string.IsNullOrEmpty(id)||ModEnums.MC_DIR[group.Name].ContainsKey(id))throw new Exception("Missing or duplicate card ID: "+id);
   if(!string.IsNullOrEmpty(root))Stock(group.Name,root);
   ModEnums.MC_DIR[group.Name][id]=card;total++;
  }
  foreach(var group in deck.Properties())foreach(JObject card in group.Value){string id=(string)card["Filename"];
   errors.AddRange(MakerStudio.Check(group.Name,card).Where(x=>x.Error).Select(x=>id+": "+x));
   foreach(var reference in MakerTools.References(group.Name,card)){references++;if(reference.Type=="Font"&&ModEnums.SC_DIR["Chatter"].Values.Any(c=>(string)c["Font"]==reference.Id))continue;if(!Stock(reference.Type,reference.Id))errors.Add(id+" / "+reference.Field+" missing "+reference.Type+":"+reference.Id);}
   if(group.Name=="Stage_Arena"){
    var sequences=(card["WaveSequences"] as JArray??new JArray()).Children().Concat(card["InfiniteSequence"]==null?new JToken[0]:new[]{card["InfiniteSequence"]});
    foreach(var sequence in sequences)foreach(string bucket in new[]{"Units","Bosses"})foreach(var spawn in (sequence[bucket] as JArray??new JArray()).OfType<JObject>()){
     string character=(string)spawn["Type"];references++;if(string.IsNullOrEmpty(character)||!Stock("Character",character))errors.Add(id+" has an unresolved wave character: "+character);
     foreach(string slot in new[]{"Weapon","Sidearm","Throwable","Key"}){string value=(string)spawn[slot];if(string.IsNullOrEmpty(value)||value=="None")continue;references++;if(!Stock(slot=="Key"?"Keycard":slot=="Throwable"?"Thrown":"Weapon",value))errors.Add(id+" has an unresolved boss "+slot+": "+value);}
    }
   }
   var schema=MakerTools.Schema(group.Name);foreach(var field in card.Properties())if(!new[]{"Filename","Root","Priority"}.Contains(field.Name)&&!schema.Any(p=>p.PropName==field.Name))errors.Add(id+" has unknown field "+field.Name);
  }
  if(errors.Count>0)throw new Exception(string.Join("\n",errors));
  using(var loader=new DialogLoader()){
   typeof(DialogLoader).GetField("AddedCards",System.Reflection.BindingFlags.Instance|System.Reflection.BindingFlags.NonPublic).SetValue(loader,new List<ModCards.CardTag>());loader.SortDeck(Path.GetFileNameWithoutExtension(args[1]),deck);
   var loaded=(List<ModCards.CardTag>)typeof(DialogLoader).GetField("AddedCards",System.Reflection.BindingFlags.Instance|System.Reflection.BindingFlags.NonPublic).GetValue(loader);if(loaded.Count!=total)throw new Exception("Native loader did not recognize every card.");
  }
  Console.WriteLine("PASS: "+total+" cards recognized by the native MadCard deck loader; "+references+" references checked; IDs, roots and field names valid. No windows or game were opened.");return 0;
 }catch(Exception e){Console.Error.WriteLine(e);return 1;}}
}
