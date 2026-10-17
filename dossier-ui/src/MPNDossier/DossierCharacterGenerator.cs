using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Linq;
namespace MPNDossier;
public sealed class DossierCharacterTemplate
{
 public string Name{get;set;}public string Category{get;set;}public string SourceId{get;set;}public string Description{get;set;}
 public override string ToString()=>Name;
}
public sealed class DossierCharacterBrief
{
 public bool AutoWrite{get;set;}=true;public string Temperament{get;set;}="Calm";public string Motivation{get;set;}="Survival";
 public string Name{get;set;}="";public string ShortName{get;set;}="";public string Title{get;set;}="";public string Personality{get;set;}="";public string Backstory{get;set;}="";
 public int? Faction{get;set;}public int? Level{get;set;}
 public string Durability{get;set;}="Template default";public string Mobility{get;set;}="Template default";public string Size{get;set;}="Template default";
 public string Primary{get;set;}public string Secondary{get;set;}
}
public static class DossierCharacterGenerator
{
 public static readonly string[] Durabilities={"Template default","Fragile","Tough","Elite","Boss"},Mobilities={"Template default","Slow","Quick","Very quick"},Sizes={"Template default","Small","Large","Huge"};
 public static readonly DossierCharacterTemplate[] Templates=BuildTemplates();
 static DossierCharacterTemplate[] BuildTemplates()
 {
  var list=new List<DossierCharacterTemplate>();void Add(string category,string name,string source,string description){list.Add(new DossierCharacterTemplate{Category=category,Name=name,SourceId=source,Description=description});}
  Add("Civilians","Street civilian","Cityboy","An everyday city character as a starting point for a local or bystander.");
  Add("Civilians","Wandering vagrant","Cityboy_Vagrant","A street wanderer for a drifter, outcast or reluctant survivor.");
  Add("Civilians","Nightclub regular","CityBoy_Raver","A nightlife character for a club scene or an eccentric contact.");
  Add("Civilians","Lab scientist","Scientist","A scientist base for a researcher, inventor or laboratory contact.");
  Add("Civilians","Field doctor","Arena_Doctor","A doctor-inspired base. The template does not add a new healing mechanic.");
  Add("Civilians","Workshop laborer","Merc0_Grunt_laborer","A laborer base for a worker, mechanic or industrial recruit.");
  Add("Outlaws","Bandit scavenger","Bandit_Grunt","A basic bandit for a scavenger, raider or wasteland recruit.");
  Add("Outlaws","Wasteland survivor","Bandit_Survivor","A survivor base for a hardened traveler or resourceful loner.");
  Add("Outlaws","Raider brawler","Bandit_Brute","A brute base for a heavy-hitting outlaw or gang enforcer.");
  Add("Outlaws","Cyber raider","Bandit_Cyber","A cyber bandit base with the source character's appearance and behavior.");
  Add("Outlaws","Wasteland fanatic","Bandit_Fanatic","A fanatic base for a zealot, cult recruit or reckless fighter.");
  Add("Outlaws","Graveseeker","Bandit_Graveseeker","A graveseeker base for a sinister wanderer or dangerous scavenger.");
  Add("Agency","Agency grunt","AAHW0_Grunt","A standard AAHW grunt for a recruit or expendable foot soldier.");
  Add("Agency","Agency agent","AAHW1_Agent","An AAHW agent base for a suited operative or security agent.");
  Add("Agency","Agency engineer","AAHW2_Engineer","An AAHW engineer base for an armed technician or specialist.");
  Add("Agency","Agency soldat","AAHW3_Soldat","An AAHW soldat base for a dangerous combat specialist.");
  Add("Agency","Half-Mag enforcer","AAHW4_HalfMag","A Half-Mag base retaining its specialized body, corpus and animation setup.");
  Add("Agency","Agency Mag","AAHW_Mag","A Mag base for a large encounter centerpiece; review room clearance.");
  Add("Nexus","Nexus agent","Nexus1_Agent","A Nexus agent base for a facility guard or corporate operative.");
  Add("Nexus","Nexus scout","Nexus2_Scout","A Nexus scout base with its existing reconnaissance-oriented setup.");
  Add("Nexus","Nexus support","Nexus3_Support","A Nexus support base for a squad specialist or support combatant.");
  Add("Nexus","Nexus riot guard","Nexus4_Riot Guard","A riot guard base retaining its original defensive equipment setup.");
  Add("Nexus","Nexus engineer","Nexus5_Engineer","A Nexus engineer base for a facility technician or armed specialist.");
  Add("Nexus","Nexus soldat","Nexus6_Soldat","A Nexus soldat base for a high-threat facility combatant.");
  Add("Military","Recon trooper","N51_0_Recon","A recon base for a forward scout or patrol specialist.");
  Add("Military","Assassin","N51_1_Assassin","An assassin base retaining its existing close-combat behavior.");
  Add("Military","Commando","N51_2_Commando","A commando base for an assault soldier or elite squad member.");
  Add("Military","Overwatch trooper","N51_3_Overwatch","An overwatch base for a ranged specialist or perimeter guard.");
  Add("Military","Field officer","N51_5_Officer","An officer base for a squad leader; it does not invent new command abilities.");
  Add("Military","Heavy gunner","Merc3_HvyGunner","A heavy gunner base retaining the source's equipment and combat setup.");
  Add("Arena","Arena rookie","Arena","A simple arena player base for a fresh original character.");
  Add("Arena","Pugilist","Heister2_Pugilist","A pugilist base for a boxer, hand-to-hand fighter or arena bruiser.");
  Add("Arena","Ranger","Heister3b_Ranger","A ranger base for a frontier fighter or wandering gunslinger.");
  Add("Arena","Gambler","Arena_Gambler","A gambler-inspired arena base for a risk taker or stylish rogue.");
  Add("Arena","Experiment","Arena_Experiment","An experimental arena base for an altered or unusual character.");
  Add("Arena","Arena Mag","Arena_Mag","An arena Mag base for a large original character, retaining its specialized setup.");
  return list.ToArray();
 }
 public static JObject Generate(DossierCharacterTemplate template,DossierCharacterBrief brief)
 {
  if(template==null||!Templates.Contains(template))throw new ArgumentException("Choose a character template.");
  if(brief==null||string.IsNullOrWhiteSpace(brief.Name))throw new ArgumentException("Give your character a name.");
  if(brief.Level.HasValue&&(brief.Level<1||brief.Level>100))throw new ArgumentException("Choose a level from 1 to 100.");
  if(brief.Faction.HasValue&&!Enum.IsDefined(typeof(ModEnums.Faction),brief.Faction.Value))throw new ArgumentException("Choose a valid faction.");
  if(!Durabilities.Contains(brief.Durability)||!Mobilities.Contains(brief.Mobility)||!Sizes.Contains(brief.Size))throw new ArgumentException("Choose a listed build option.");
  JObject source;if(!ModEnums.DB_CHAR.TryGetValue(template.SourceId,out source))throw new InvalidOperationException("The character database is missing "+template.SourceId+". Choose another template or restore the base database.");
  var result=(JObject)source.DeepClone();result.Remove("_DossierNotes");result["FullName"]=brief.Name.Trim();result["ShortName"]=string.IsNullOrWhiteSpace(brief.ShortName)?brief.Name.Trim():brief.ShortName.Trim();result["Title"]=string.IsNullOrWhiteSpace(brief.Title)?template.Name:brief.Title.Trim();
  result["Description"]=(brief.AutoWrite?WriteProfile(template,brief):template.Description)+(string.IsNullOrWhiteSpace(brief.Personality)?"":"\n\n"+brief.Personality.Trim());
  if(brief.Faction.HasValue)result["DefaultFaction"]=brief.Faction.Value;if(brief.Level.HasValue)result["Level"]=brief.Level.Value;
  double durability=brief.Durability=="Fragile"?.65:brief.Durability=="Tough"?1.5:brief.Durability=="Elite"?2:brief.Durability=="Boss"?3:1;
  if(durability!=1){result["Health"]=Math.Max(1,(int)Math.Round((double)source["Health"]*durability));result["Toughness"]=Math.Max(0,(int)Math.Round((double)source["Toughness"]*durability));}
  double mobility=brief.Mobility=="Slow"?.8:brief.Mobility=="Quick"?1.15:brief.Mobility=="Very quick"?1.3:1;
  if(mobility!=1){var speed=result["Speed"] as JObject;if(speed==null)throw new FormatException("This template is missing its movement settings.");foreach(var p in speed.Properties())if(p.Value.Type==JTokenType.Integer||p.Value.Type==JTokenType.Float)p.Value=new JValue(Math.Round((double)p.Value*mobility,4));}
  double size=brief.Size=="Small"?.85:brief.Size=="Large"?1.2:brief.Size=="Huge"?1.5:1;if(size!=1)result["Scale"]=Math.Round((double)source["Scale"]*size,4);
  Weapon(result,"DefaultPrimary",brief.Primary);Weapon(result,"DefaultSecondary",brief.Secondary);
  string story=string.IsNullOrWhiteSpace(brief.Backstory)?(brief.AutoWrite?WriteBackstory(template,brief,result):""):brief.Backstory.Trim();
  if(story.Length>0)result["_DossierNotes"]=new JObject(new JProperty("version",1),new JProperty("entries",new JArray(new JObject(new JProperty("title","Backstory"),new JProperty("text",story)))));
  return result;
 }
 public static readonly string[] Temperaments={"Calm","Loyal","Ruthless","Reckless","Secretive","Optimistic"},Motivations={"Survival","Protecting friends","Revenge","Money","Finding the truth","Freedom"};
 static string Disposition(string value){switch(value){case "Loyal":return "They stand by the people who have earned their trust, even when walking away would be easier.";case "Ruthless":return "They judge plans by results and rarely let sympathy interrupt a decision.";case "Reckless":return "They act before doubt can catch up, trusting instinct when a careful plan falls apart.";case "Secretive":return "They reveal little about themselves and listen carefully to what others leave unsaid.";case "Optimistic":return "They look for a way forward even when everyone around them expects the worst.";default:return "They keep a level head under pressure, watching for an opening before making their move.";}}
 static string Goal(string value){switch(value){case "Protecting friends":return "keeping their friends out of the line of fire";case "Revenge":return "settling a debt they have never been able to forget";case "Money":return "earning enough to stop answering to somebody else's orders";case "Finding the truth":return "uncovering the truth behind a story that never added up";case "Freedom":return "building a life that nobody else gets to control";default:return "making it through the next day without losing who they are";}}
 static string Article(string role)=>("aeiou".IndexOf(char.ToLowerInvariant(role[0]))>=0?"an ":"a ")+role;
 public static string WriteProfile(DossierCharacterTemplate template,DossierCharacterBrief brief)
 {
  string role=string.IsNullOrWhiteSpace(brief.Title)?template.Name:brief.Title.Trim();string text=brief.Name.Trim()+" works as "+Article(role.ToLowerInvariant())+". "+Disposition(brief.Temperament)+" Their priority is "+Goal(brief.Motivation)+".";
  if(brief.Mobility=="Quick"||brief.Mobility=="Very quick")text+=" In a fight, they prefer to stay mobile rather than let an opponent pin them down.";else if(brief.Mobility=="Slow")text+=" They move deliberately, preferring a solid position to a hurried advance.";
  if(new[]{"Tough","Elite","Boss"}.Contains(brief.Durability))text+=" They are built to endure a hard encounter and keep going.";else if(brief.Durability=="Fragile")text+=" They survive by avoiding punishment rather than trying to absorb it.";return text;
 }
 static string WriteBackstory(DossierCharacterTemplate template,DossierCharacterBrief brief,JObject result)
 {
  string origin;switch(template.Category){case "Civilians":origin="ordinary work in a city where even a quiet day could turn dangerous";break;case "Outlaws":origin="scraping together a living on the outskirts, where useful supplies rarely changed hands without a price";break;case "Agency":origin="a chain of assignments where following orders mattered more than asking why";break;case "Nexus":origin="the routines of a guarded facility, learning how much could happen behind a locked door";break;case "Military":origin="field assignments where preparation and a reliable partner could decide who came home";break;default:origin="small arena jobs, learning which risks paid off and which left lasting scars";break;}
  return brief.Name.Trim()+" came from "+origin+". Work as "+Article(template.Name.ToLowerInvariant())+" gave them a set of skills, but no guarantee of a future.\n\n"+Disposition(brief.Temperament)+" Now they are focused on "+Goal(brief.Motivation)+". Their current allegiance is "+((ModEnums.Faction)(int)result["DefaultFaction"])+", though the people they choose to trust may matter more than the name on a uniform."+(string.IsNullOrWhiteSpace(brief.Personality)?"":"\n\n"+brief.Personality.Trim());
 }
 static void Weapon(JObject result,string key,string value){if(value==null)return;if(value!="None"&&!ModEnums.DB_WEAPONS.ContainsKey(value))throw new ArgumentException("Choose an available weapon for "+key+".");result[key]=value;if(key=="DefaultPrimary"&&value!="None"){var category=ModEnums.DB_WEAPONS[value]["Category"];if(category!=null&&category.Type==JTokenType.Integer&&Enum.IsDefined(typeof(ModEnums.WeaponCategories),(int)category))result["WeaponPreference"]=(int)category;}}
 public static string FileName(string name){var clean=new string((name??"").Trim().Select(c=>Path.GetInvalidFileNameChars().Contains(c)?'_':c).ToArray()).TrimEnd('.',' ');if(clean.Length>80)clean=clean.Substring(0,80);if(string.IsNullOrWhiteSpace(clean))clean="MyCharacter";var reserved=clean.Split('.')[0].ToUpperInvariant();if(new[]{"CON","PRN","AUX","NUL","COM1","COM2","COM3","COM4","COM5","COM6","COM7","COM8","COM9","LPT1","LPT2","LPT3","LPT4","LPT5","LPT6","LPT7","LPT8","LPT9"}.Contains(reserved))clean="Character_"+clean;return clean;}
}
