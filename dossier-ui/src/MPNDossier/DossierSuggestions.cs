using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
namespace MPNDossier;
public sealed class DossierSuggestion
{
 public object Value;
 public string Name,Reason;
 public int Rank;
 public string Id=>Value?.ToString()??"";
 public override string ToString()=>(string.IsNullOrEmpty(Name)||Name==Id?Id:Name+"  ·  "+Id)+(string.IsNullOrEmpty(Reason)?"":"  — "+Reason);
}
public static class DossierSuggestions
{
 public static string FieldKey(string folder,TreeNode node)
 {
  var names=new Stack<string>();for(var n=node;n!=null;n=n.Parent)if(n.Tag is ModCards.NodeTag tag)names.Push(tag.LinkedControl.PropName);
  return folder+"/"+string.Join("/",names);
 }
 public static Dictionary<string,JObject> Database(string folder,string field)
 {
  switch(field){
   case "DefaultPrimary":case "DefaultSecondary":case "DefaultSidearm":case "DefaultThrown":case "Weapon":case "Weapons":case "Fist":case "Kick":case "Headbutt":case "UnarmedReplacement":case "BrokenReplacement":return ModEnums.DB_WEAPONS;
   case "Armor":case "ArmorPicks":case "DefaultArmor":return ModEnums.DB_GEAR;
   case "AnimationSet":return ModEnums.DB_ANIMSETS;
   case "CorpusBlocks":return ModEnums.DB_CORPUS;
   case "VoiceSet":return ModEnums.DB_VOICESETS;
   case "TextChatter":return ModEnums.DB_CHATTER;
   case "Groups":return ModEnums.DB_SKILLGROUPS;
   case "DefaultLoadout":return ModEnums.DB_LOADOUTS;
   case "Executions_Offensive":case "Executions_Defensive":case "Executions":return ModEnums.DB_EXECUTION;
   case "myGroups":return ModEnums.DB_ANIMATIONS;
   case "SlamExplode":case "BrokenExplosion":return ModEnums.DB_EXPLOSION;
  }
  if(string.Equals(field,"myRef",StringComparison.OrdinalIgnoreCase)){
   switch(folder){case "characters":return ModEnums.DB_CHAR;case "armor":return ModEnums.DB_GEAR;case "weapons":case "ranged":case "weaponmodded":case "thrown":return ModEnums.DB_WEAPONS;}
  }
  return null;
 }
 public static List<DossierSuggestion> Build(string folder,TreeNode node,string query,TreeNodeCollection root)
 {
  var tag=node?.Tag as ModCards.NodeTag;if(tag==null||tag.NodeType!=ModCards.NodeType.Val)return new List<DossierSuggestion>();
  var control=tag.LinkedControl;var all=new List<DossierSuggestion>();
  var database=Database(folder,control.PropName);
  if(control.Selection is IEnumerable choices&&!(control.Selection is string)){
   foreach(var value in choices){if(value==null)continue;JObject record=null;database?.TryGetValue(value.ToString(),out record);all.Add(new DossierSuggestion{Value=value,Name=record?["FullName"]?.ToString()??record?["Name"]?.ToString()??value.ToString()});}
  }
  else if(control.PropType==ModCards.PropType.Int||control.PropType==ModCards.PropType.Float){
   string type=DossierHelp.ObjectType(folder);for(var parent=node.Parent;parent!=null;parent=parent.Parent)if(parent.Tag is ModCards.NodeTag pt&&pt.NodeType==ModCards.NodeType.Obj){type=DossierHelp.ObjectType(pt.LinkedControl.ObjectName);break;}
   var definitions=DossierHelp.Objects[type]?["Properties"]?[control.PropName]?["Definitions"] as JObject;
   if(definitions!=null)foreach(var definition in definitions.Properties()){
    double number;string numeric,label;
    if(double.TryParse(definition.Name,NumberStyles.Float,CultureInfo.InvariantCulture,out number)){numeric=definition.Name;label=definition.Value.ToString();}
    else if(double.TryParse(definition.Value.ToString(),NumberStyles.Float,CultureInfo.InvariantCulture,out number)){numeric=definition.Value.ToString();label=definition.Name;}
    else continue;
    if(double.IsNaN(number)||double.IsInfinity(number)||(control.PropType==ModCards.PropType.Int&&(number!=Math.Truncate(number)||number<int.MinValue||number>int.MaxValue)))continue;
    object value=control.PropType==ModCards.PropType.Int?(object)(int)number:number;all.Add(new DossierSuggestion{Value=value,Name=label,Reason="Documented value"});
   }
  }
  var words=(query??"").Split(new[]{' '},StringSplitOptions.RemoveEmptyEntries);var key=FieldKey(folder,node);List<string> recent;DossierPreferences.Current.FieldChoices.TryGetValue(key,out recent);
  var pref=root?["WeaponPreference"]?.Tag as ModCards.NodeTag;
  foreach(var suggestion in all){
   if(recent?.Contains(suggestion.Id)==true){suggestion.Rank+=15;suggestion.Reason="Recently used";}
   JObject record;int category;
   if(folder=="characters"&&new[]{"DefaultPrimary","DefaultSecondary","DefaultSidearm","DefaultThrown","UnarmedReplacement"}.Contains(control.PropName)&&pref?.Value!=null&&ModEnums.DB_WEAPONS.TryGetValue(suggestion.Id,out record)&&int.TryParse(record["Category"]?.ToString(),out category)&&category==Convert.ToInt32(pref.Value)){
    suggestion.Rank+=25;suggestion.Reason="Matches weapon preference";
   }
   if(string.Equals(suggestion.Id,query,StringComparison.OrdinalIgnoreCase))suggestion.Rank+=100;
   else if(suggestion.Id.StartsWith(query??"",StringComparison.OrdinalIgnoreCase)||suggestion.Name.StartsWith(query??"",StringComparison.OrdinalIgnoreCase))suggestion.Rank+=50;
  }
  return all.Where(s=>words.All(w=>(s.Id+" "+s.Name).IndexOf(w,StringComparison.OrdinalIgnoreCase)>=0)).OrderByDescending(s=>s.Rank).ThenBy(s=>s.Name,StringComparer.OrdinalIgnoreCase).Take(12).ToList();
 }
}
