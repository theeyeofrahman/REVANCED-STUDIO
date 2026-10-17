using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace MPNDossier;
public sealed partial class DossierRoomEditor
{
 void Add(){if(views?.SelectedIndex==0){AddFromTemplate();return;}if(!ApplySelected())return;var token=tree.SelectedNode?.Tag as JToken;if(token==null){NewRoom();return;}var container=token as JContainer;if(!(container is JObject)&&!(container is JArray))container=token.Parent is JProperty p?p.Parent:token.Parent;
  if(container is JObject obj)AddTo(obj);else if(container is JArray array){string initial=array.First==null||array.First is JObject?"Object":DossierRoomDocument.TypeName(array.First);using(var dialog=new DossierRoomValueDialog("Add list item",null,initial,initial=="Object"?"{}":""))if(dialog.ShowDialog(this)==DialogResult.OK){document.Change(()=>array.Add(dialog.Result));Rebuild();UpdateTitle();}}
 }
 void AddTo(JObject obj,string name="",string text="{}")
 {using(var dialog=new DossierRoomValueDialog(obj==document.Data?"Add room":"Add field / existing object ID",name,"Object",text))if(dialog.ShowDialog(this)==DialogResult.OK){if(obj.Property(dialog.Key)!=null)throw new ArgumentException("That name already exists here.");document.Change(()=>obj.Add(dialog.Key,dialog.Result));Rebuild();UpdateTitle();}}
 void NewRoom(){if(!ApplySelected())return;AddTo(document.Data,"Room_Name",DossierRoomDocument.EmptyRoom().ToString());}
 void Duplicate()
 {
  if(!ApplySelected())return;var token=tree.SelectedNode?.Tag as JToken;if(token==null)return;
  if(token.Parent is JArray array)document.Change(()=>array.Insert(array.IndexOf(token)+1,token.DeepClone()));
  else if(token.Parent is JProperty property)using(var dialog=new DossierRoomValueDialog("Duplicate with a different name / game ID",property.Name+"_copy",DossierRoomDocument.TypeName(token),token.Type==JTokenType.String?token.ToString():token.ToString(Formatting.Indented))){if(dialog.ShowDialog(this)!=DialogResult.OK)return;var parent=(JObject)property.Parent;if(parent.Property(dialog.Key)!=null)throw new ArgumentException("That name already exists here.");document.Change(()=>property.AddAfterSelf(new JProperty(dialog.Key,dialog.Result)));}
  Rebuild();UpdateTitle();
 }
 void Remove(){if(!ApplySelected())return;var token=tree.SelectedNode?.Tag as JToken;if(token==null)return;document.Change(()=>{if(token.Parent is JProperty property)property.Remove();else token.Remove();});Rebuild();UpdateTitle();status.Text="Removed from this document. Undo restores it.";}
 void Reorder(int delta)
 {
  if(!ApplySelected())return;var token=tree.SelectedNode?.Tag as JToken;if(token==null)return;
  if(token.Parent is JArray array){int index=array.IndexOf(token),next=index+delta;if(next<0||next>=array.Count)return;document.Change(()=>{token.Remove();array.Insert(next,token);});}
  else if(token.Parent is JProperty property){var siblings=((JObject)property.Parent).Properties().ToArray();int index=Array.IndexOf(siblings,property),next=index+delta;if(next<0||next>=siblings.Length)return;var other=siblings[next];document.Change(()=>{property.Remove();if(delta<0)other.AddBeforeSelf(property);else other.AddAfterSelf(property);});}
  Rebuild();var node=Walk(tree.Nodes).FirstOrDefault(n=>ReferenceEquals(n.Tag,token));if(node!=null){tree.SelectedNode=node;node.EnsureVisible();}UpdateTitle();
 }
 static string FieldHelp(JToken token)
 {
  var key=(token.Parent as JProperty)?.Name??(token.Parent?.Parent as JProperty)?.Name??"";
  switch(key){
   case "Characters":return "Character and spawn overrides, usually keyed by existing scene IDs. Some files also place shared fields, such as myFaction, directly in this section. Keep the template structure and IDs.";
   case "Interactives":return "Interactive object overrides, keyed by game object IDs. Includes usage requirements, sounds, health and allPurposes arrays.";
   case "Cutscenes":return "Cutscene overrides. ActorSerials identifies actors and Steps contains ordered step data, dialogue, actions and sounds. Reordering preserves explicit Index fields.";
   case "Lockers":return "Locker overrides, including stored items, extra magazines, respawning and initial open/spawned state.";
   case "Areas":return "Existing area overrides, keyed by scene IDs. Available fields vary by area type; use the template's fields as a starting point.";
   case "Items":return "An array of item records. The examples use myItem, SerialNumber, extraMags and physicsStartOn. Preserve serial numbers that other game objects refer to.";
   case "Entrances":return "Existing entrance overrides. These files use name, disableSpawn, vocation requirements and spawn/exit point settings.";
   case "myType":return "Character/type reference used by this room object. Suggestions for character entries use IDs from Dossier's loaded database; custom IDs can also be entered directly.";
   case "myControlType":return "Who controls the character: 0 no controller, 1 player, 2 network player, 3 NPC, 4 boss, 5 simple AI, 6 bot, 7 turret, 100 character preset default. Faction separately controls starting allegiance. Characters offers quick role assignments.";
   case "myFaction":case "MyFaction":return "Starting faction: Player faction NPCs are allies, Neutral is non-hostile, and different non-neutral factions are enemies of Player faction by the game's default rule. A player controller still means a player regardless of faction. Scripts and squads can change relationships.";
   case "myName":return "Name stored on this scene/spawn object. Preserve names used by room events and spawn links.";
   case "Blurb":return "Cutscene/dialogue text. Strings support line breaks and game markup such as color and size tags. The editor writes them as JSON strings.";
   case "ActorSerial":case "ActorSerials":case "AssignSerialNumber":case "SerialNumber":return "Actor or item serial/reference. Keep matching references consistent across room events and cutscene steps.";
   case "disableSpawn":case "StartOpen":case "Enabled":case "physicsStartOn":return "A boolean setting. Use true or false; JSON booleans remain booleans on export.";
   case "Index":return "Explicit cutscene step index. Moving a list item does not rewrite this value.";
  }
  return "This field is preserved directly from the room JSON. Edit strings as plain text; edit objects/arrays as JSON. Numeric and boolean types stay explicit. Room names and object IDs must match the target scene. This editor changes room data, not 3D geometry.";
 }
 void FillChoices(JToken token)
 {
  choices.Items.Clear();choices.Items.Add(new RoomChoice{Label="Reference suggestions (optional)",Value=""});choices.SelectedIndex=0;
  choices.Items.AddRange(SuggestionsFor(token).Cast<object>().ToArray());choices.Enabled=choices.Items.Count>1;
 }
 IEnumerable<RoomChoice> SuggestionsFor(JToken token)
 {
  var result=new List<RoomChoice>();
  string key=(token.Parent as JProperty)?.Name??(token.Parent?.Parent as JProperty)?.Name??"";Dictionary<string,JObject> source=null;
  bool character=token.Ancestors().OfType<JProperty>().Any(p=>p.Name=="Characters");
  if(key=="myType"&&character&&token.Type==JTokenType.String)source=ModEnums.DB_CHAR;
  else if(new[]{"myItem","itemsHeld","itemsBack","itemThrown","myWeaponsStored","myWeaponsStored_Memory","PossibleWeapons"}.Contains(key)&&token.Type==JTokenType.String)source=ModEnums.DB_WEAPONS;
  else if(new[]{"itemsWorn","AdditionalArmor"}.Contains(key)&&token.Type==JTokenType.String)source=ModEnums.DB_GEAR;
  if(source!=null)foreach(var pair in source.OrderBy(p=>p.Key))result.Add(new RoomChoice{Label=(pair.Value["FullName"]?.ToString()??pair.Key)+" · "+pair.Key,Value=pair.Key});
  if((key=="myFaction"||key=="MyFaction"||key=="Faction")&&token.Type==JTokenType.Integer)foreach(ModEnums.Faction f in Enum.GetValues(typeof(ModEnums.Faction)))result.Add(new RoomChoice{Label=((int)f==13?"Spaghett":(int)f==27?"Union":f.ToString())+" · "+(int)f,Value=((int)f).ToString()});
  if(key=="myControlType"&&token.Type==JTokenType.Integer){string[] names={"No controller / stationary","Player","Network player","NPC / AI","Boss AI","Simple AI","Bot AI","Turret AI"};for(int i=0;i<names.Length;i++)result.Add(new RoomChoice{Label=names[i]+" · "+i,Value=i.ToString()});result.Add(new RoomChoice{Label="Character preset default · 100",Value="100"});}
  if(key=="alertStatus"&&token.Type==JTokenType.Integer)foreach(var pair in new[]{new RoomChoice{Label="Unaware · 0",Value="0"},new RoomChoice{Label="Hunting · 1",Value="1"},new RoomChoice{Label="Combat · 2",Value="2"},new RoomChoice{Label="Match room · 100",Value="100"}})result.Add(pair);
  return result;
 }
 sealed class RoomChoice{public string Label,Value;public override string ToString()=>Label;}
}
public sealed class DossierRoomValueDialog:Form
{
 public JToken Result{get;private set;}public string Key=>name.Text.Trim();
 readonly TextBox name=new TextBox{Dock=DockStyle.Fill};readonly ComboBox type=new ComboBox{Dock=DockStyle.Fill,DropDownStyle=ComboBoxStyle.DropDownList};readonly TextBox value=new TextBox{Dock=DockStyle.Fill,Multiline=true,ScrollBars=ScrollBars.Both,AcceptsReturn=true};
 public DossierRoomValueDialog(string title,string key,string kind,string text)
 {
  DossierDpi.Initialize(this);Text=title;Size=new Size(660,550);MinimumSize=new Size(550,450);StartPosition=FormStartPosition.CenterParent;
  var root=new TableLayoutPanel{Dock=DockStyle.Fill,Padding=new Padding(18),ColumnCount=1,RowCount=7};root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,100));foreach(var h in new[]{24,34,24,34})root.RowStyles.Add(new RowStyle(SizeType.Absolute,h));root.RowStyles.Add(new RowStyle(SizeType.Percent,100));root.RowStyles.Add(new RowStyle(SizeType.Absolute,40));root.RowStyles.Add(new RowStyle(SizeType.Absolute,34));
  root.Controls.Add(new Label{Text=key==null?"List item (no property name)":"Name / existing scene ID",Dock=DockStyle.Fill},0,0);name.Text=key??"";name.Enabled=key!=null;root.Controls.Add(name,0,1);root.Controls.Add(new Label{Text="Value type",Dock=DockStyle.Fill},0,2);type.Items.AddRange(new object[]{"String","Integer","Number","Boolean","Null","Object","Array"});type.SelectedItem=kind;root.Controls.Add(type,0,3);value.Text=text;root.Controls.Add(value,0,4);
  var message=new Label{Dock=DockStyle.Fill,AutoEllipsis=true};root.Controls.Add(message,0,5);var buttons=new FlowLayoutPanel{Dock=DockStyle.Fill,FlowDirection=FlowDirection.RightToLeft};var save=new DossierButton{Text="Add / apply",Width=120,Height=30};var cancel=new DossierButton{Text="Cancel",Width=100,Height=30,DialogResult=DialogResult.Cancel};buttons.Controls.Add(save);buttons.Controls.Add(cancel);root.Controls.Add(buttons,0,6);Controls.Add(root);DossierStyle.Apply(this);save.Click+=(_,__)=>{try{if(name.Enabled&&Key.Length==0)throw new FormatException("Enter a name or game ID.");Result=DossierRoomDocument.ReadValue(type.SelectedItem.ToString(),value.Text);DialogResult=DialogResult.OK;Close();}catch(Exception ex){message.Text=ex.Message;}};CancelButton=cancel;DossierDpi.Complete(this);
 }
}
