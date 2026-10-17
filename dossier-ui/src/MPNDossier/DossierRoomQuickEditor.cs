using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
namespace MPNDossier;

public sealed class DossierRoomQuickEditor:Form
{
 readonly DossierRoomDocument document;readonly JObject template;readonly bool dialogue;
 JObject effective;List<DossierRoomQuickData.Entry> entries;
 readonly ListBox list=new ListBox{Dock=DockStyle.Fill,HorizontalScrollbar=true};
 readonly TextBox search=new TextBox{Dock=DockStyle.Fill,AccessibleName="Search rooms, roles, names or dialogue"};
 readonly Label info=new Label{Dock=DockStyle.Fill,AutoEllipsis=true};
 readonly Label status=new Label{Dock=DockStyle.Bottom,Height=38,AutoEllipsis=true,Padding=new Padding(14,6,0,0)};
 readonly TextBox characterSearch=new TextBox{Dock=DockStyle.Fill,AccessibleName="Search character presets"};
 readonly ListBox characterChoices=new ListBox{Dock=DockStyle.Fill,HorizontalScrollbar=true};
 readonly TextBox characterId=new TextBox{Dock=DockStyle.Fill,AccessibleName="Character preset ID (custom IDs allowed)"};
 readonly TextBox displayName=new TextBox{Dock=DockStyle.Fill,AccessibleName="Displayed character name"};
 readonly ComboBox role=new ComboBox{Dock=DockStyle.Fill,DropDownStyle=ComboBoxStyle.DropDownList};
 readonly ComboBox faction=new ComboBox{Dock=DockStyle.Fill,DropDownStyle=ComboBoxStyle.DropDownList};
 readonly ComboBox controller=new ComboBox{Dock=DockStyle.Fill,DropDownStyle=ComboBoxStyle.DropDownList};
 readonly ComboBox speaker=new ComboBox{Dock=DockStyle.Fill,DropDownStyle=ComboBoxStyle.DropDown,AccessibleName="Dialogue speaker serial"};
 readonly TextBox line=new TextBox{Dock=DockStyle.Fill,Multiline=true,AcceptsReturn=true,ScrollBars=ScrollBars.Vertical,WordWrap=true,AccessibleName="Dialogue text"};
 readonly Label count=new Label{Dock=DockStyle.Fill};
 DossierRoomQuickData.Entry current;bool loading,dirty;readonly HashSet<string> changed=new HashSet<string>();
 sealed class Choice{public string Id,Name;public override string ToString()=>Name;}
 public DossierRoomQuickEditor(DossierRoomDocument document,JObject template,bool dialogue,string initialKey=null)
 {
  this.document=document;this.template=template;this.dialogue=dialogue;
  DossierDpi.Initialize(this);Text=dialogue?"Room dialogue — quick edit":"Room characters — quick edit";Size=new Size(1280,880);MinimumSize=new Size(1050,720);StartPosition=FormStartPosition.CenterParent;
  var root=new TableLayoutPanel{Dock=DockStyle.Fill,Padding=new Padding(16),ColumnCount=2,RowCount=1};root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,45));root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,55));root.RowStyles.Add(new RowStyle(SizeType.Percent,100));
  var left=new TableLayoutPanel{Dock=DockStyle.Fill,ColumnCount=1,RowCount=4,Padding=new Padding(0,0,14,0)};left.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,100));left.RowStyles.Add(new RowStyle(SizeType.Absolute,32));left.RowStyles.Add(new RowStyle(SizeType.Absolute,38));left.RowStyles.Add(new RowStyle(SizeType.Absolute,32));left.RowStyles.Add(new RowStyle(SizeType.Percent,100));
  left.Controls.Add(new Label{Text=dialogue?"Find a conversation or line":"Find a player, enemy or ally",Dock=DockStyle.Fill},0,0);left.Controls.Add(search,0,1);left.Controls.Add(count,0,2);left.Controls.Add(list,0,3);root.Controls.Add(left,0,0);
  var right=new TableLayoutPanel{Dock=DockStyle.Fill,ColumnCount=1,RowCount=3};right.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,100));right.RowStyles.Add(new RowStyle(SizeType.Absolute,100));right.RowStyles.Add(new RowStyle(SizeType.Percent,100));right.RowStyles.Add(new RowStyle(SizeType.Absolute,96));right.Controls.Add(info,0,0);
  right.Controls.Add(dialogue?BuildDialogue():BuildCharacters(),0,1);
  var actions=new FlowLayoutPanel{Dock=DockStyle.Fill,WrapContents=true};
  void Button(string text,Action action){var b=new DossierButton{Text=text,Width=145,Height=34};b.Click+=(_,__)=>action();actions.Controls.Add(b);}
  Button("Apply to room",()=>{if(Commit()){RefreshList(current?.Key);status.Text="Applied. Close this window, then Save the room to write the file.";}});
  Button("Apply + next",()=>{if(Commit())Next(1);});Button("Previous",()=>Next(-1));Button("Done",()=>Close());right.Controls.Add(actions,0,2);root.Controls.Add(right,1,0);Controls.Add(root);Controls.Add(status);
  search.TextChanged+=(_,__)=>{if(!loading&&Commit())RefreshList(current?.Key);};
  list.SelectedIndexChanged+=(_,__)=>{if(loading)return;var next=list.SelectedItem as DossierRoomQuickData.Entry;if(!Commit()){loading=true;list.SelectedItem=current;loading=false;return;}LoadEntry(next);};
  FormClosing+=(_,e)=>{if(!Commit())e.Cancel=true;};KeyPreview=true;KeyDown+=(_,e)=>{if(e.Control&&e.KeyCode==Keys.Enter){if(Commit())Next(1);e.SuppressKeyPress=true;}else if(e.Control&&e.KeyCode==Keys.S){if(Commit())status.Text="Applied to room. Use Save in the room editor to write the file.";e.SuppressKeyPress=true;}};
  DossierStyle.Apply(this);DossierDpi.Complete(this);RefreshList(initialKey);
 }
 TableLayoutPanel BuildCharacters()
 {
  var panel=new TableLayoutPanel{Dock=DockStyle.Fill,ColumnCount=1,RowCount=12};panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,100));
  foreach(var h in new[]{26,34,26})panel.RowStyles.Add(new RowStyle(SizeType.Absolute,h));panel.RowStyles.Add(new RowStyle(SizeType.Percent,100));foreach(var h in new[]{26,34,26,34,26,34,26,34})panel.RowStyles.Add(new RowStyle(SizeType.Absolute,h));
  panel.Controls.Add(new Label{Text="Search characters by name or preset ID",Dock=DockStyle.Fill},0,0);panel.Controls.Add(characterSearch,0,1);panel.Controls.Add(new Label{Text="Choose a replacement character",Dock=DockStyle.Fill},0,2);panel.Controls.Add(characterChoices,0,3);
  panel.Controls.Add(new Label{Text="Character preset ID · custom IDs are welcome",Dock=DockStyle.Fill},0,4);panel.Controls.Add(characterId,0,5);panel.Controls.Add(new Label{Text="Displayed name · does not rename the spawn or scene ID",Dock=DockStyle.Fill},0,6);panel.Controls.Add(displayName,0,7);
  role.Items.AddRange(new object[]{"Keep current role","Player · player controller + Player faction","Ally · NPC controller + Player faction","Enemy · NPC controller + Enemy faction","Neutral · NPC controller + Neutral faction"});role.SelectedIndex=0;
  panel.Controls.Add(new Label{Text="Quick role assignment",Dock=DockStyle.Fill},0,8);panel.Controls.Add(role,0,9);
  var labels=new TableLayoutPanel{Dock=DockStyle.Fill,ColumnCount=2};labels.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,50));labels.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,50));labels.Controls.Add(new Label{Text="Faction",Dock=DockStyle.Fill},0,0);labels.Controls.Add(new Label{Text="Controller",Dock=DockStyle.Fill},1,0);panel.Controls.Add(labels,0,10);
  var settings=new TableLayoutPanel{Dock=DockStyle.Fill,ColumnCount=2};settings.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,50));settings.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,50));settings.Controls.Add(faction,0,0);settings.Controls.Add(controller,1,0);panel.Controls.Add(settings,0,11);
  foreach(ModEnums.Faction f in Enum.GetValues(typeof(ModEnums.Faction)))faction.Items.Add(new Choice{Id=((int)f).ToString(),Name=FactionName((int)f)+" · "+(int)f});
  string[] controls={"No controller / stationary","Player","Network player","NPC / AI","Boss AI","Simple AI","Bot AI","Turret AI"};for(int i=0;i<controls.Length;i++)controller.Items.Add(new Choice{Id=i.ToString(),Name=controls[i]+" · "+i});controller.Items.Add(new Choice{Id="100",Name="Character preset default · 100"});
  characterSearch.TextChanged+=(_,__)=>FilterCharacters();characterChoices.SelectedIndexChanged+=(_,__)=>{if(!loading&&characterChoices.SelectedItem is Choice c)characterId.Text=c.Id;};
  characterId.TextChanged+=(_,__)=>Changed("myType");displayName.TextChanged+=(_,__)=>Changed("DisplayedName");
  faction.SelectedIndexChanged+=(_,__)=>Changed("myFaction");controller.SelectedIndexChanged+=(_,__)=>Changed("myControlType");
  faction.SelectionChangeCommitted+=(_,__)=>role.SelectedIndex=0;controller.SelectionChangeCommitted+=(_,__)=>role.SelectedIndex=0;
  role.SelectionChangeCommitted+=(_,__)=>{if(role.SelectedIndex<=0)return;Select(faction,(role.SelectedIndex==3?11:role.SelectedIndex==4?10:0).ToString());Select(controller,(role.SelectedIndex==1?1:3).ToString());};
  return panel;
 }
 static string FactionName(int n)=>n==13?"Spaghett":n==27?"Union":((ModEnums.Faction)n).ToString();
 TableLayoutPanel BuildDialogue()
 {
  var panel=new TableLayoutPanel{Dock=DockStyle.Fill,ColumnCount=1,RowCount=5};panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,100));panel.RowStyles.Add(new RowStyle(SizeType.Absolute,28));panel.RowStyles.Add(new RowStyle(SizeType.Absolute,36));panel.RowStyles.Add(new RowStyle(SizeType.Absolute,30));panel.RowStyles.Add(new RowStyle(SizeType.Percent,100));panel.RowStyles.Add(new RowStyle(SizeType.Absolute,60));
  panel.Controls.Add(new Label{Text="Speaker · select a known actor or type its serial",Dock=DockStyle.Fill},0,0);panel.Controls.Add(speaker,0,1);panel.Controls.Add(new Label{Text="Dialogue · plain text, line breaks and game tags",Dock=DockStyle.Fill},0,2);panel.Controls.Add(line,0,3);panel.Controls.Add(new Label{Text="Ctrl+Enter applies this line and opens the next. Switching lines applies your edits. Step indices, actions, sounds and markup are preserved.",Dock=DockStyle.Fill},0,4);
  speaker.TextChanged+=(_,__)=>Changed("ActorSerial");line.TextChanged+=(_,__)=>Changed("text");return panel;
 }
 void Changed(string key){if(loading||current==null)return;dirty=true;changed.Add(key);status.Text="Edited in this window · switching entries applies changes to the room; Save the room when done.";}
 void Select(ComboBox combo,string id){var item=combo.Items.Cast<Choice>().FirstOrDefault(c=>c.Id==id);if(item==null){item=new Choice{Id=id??"",Name=id==null?"Scene default (unchanged)":"Custom value · "+id};combo.Items.Add(item);}combo.SelectedItem=item;}
 void RefreshList(string key)
 {
  effective=DossierRoomQuickData.Effective(document.Data,template);entries=dialogue?DossierRoomQuickData.Dialogue(effective):DossierRoomQuickData.Characters(effective);
  string query=search.Text.Trim();var visible=entries.Where(e=>e.Label.IndexOf(query,StringComparison.OrdinalIgnoreCase)>=0||e.Value.ToString().IndexOf(query,StringComparison.OrdinalIgnoreCase)>=0).ToArray();
  loading=true;list.Items.Clear();list.Items.AddRange(visible);list.SelectedItem=visible.FirstOrDefault(e=>e.Key==key)??visible.FirstOrDefault();loading=false;count.Text=visible.Length+" / "+entries.Count+(dialogue?" lines":" characters");LoadEntry(list.SelectedItem as DossierRoomQuickData.Entry);
 }
 void LoadEntry(DossierRoomQuickData.Entry entry)
 {
  loading=true;current=entry;dirty=false;changed.Clear();if(entry!=null)entry.Value=DossierRoomQuickData.At(effective,entry.Path);
  if(entry==null){info.Text="No matches. Clear the search or open a matching stage template to see more characters and dialogue.";characterId.Enabled=displayName.Enabled=role.Enabled=faction.Enabled=controller.Enabled=characterChoices.Enabled=line.Enabled=speaker.Enabled=false;line.Clear();characterId.Clear();displayName.Clear();}
  else{
   info.Text=entry.Label+"\r\n"+(dialogue?"Edit the words below. The original scene references stay intact.":"Starting role relative to Player faction; scripts and squads can change it."+(DossierRoomQuickData.Role((JObject)entry.Value)=="PLAYER"?" Player spawns may use the saved career's squad instead of this preset.":""));
   if(dialogue){line.Enabled=true;line.Text=entry.Value.ToString().Replace("\r\n","\n").Replace("\r","\n").Replace("\n",Environment.NewLine);var path=entry.Path.Take(entry.Path.Length-1).ToArray();var parent=DossierRoomQuickData.At(effective,path) as JObject;speaker.Items.Clear();
    foreach(var serial in effective.Descendants().OfType<JProperty>().Where(p=>p.Name=="ActorSerial"||p.Name=="AssignSerialNumber").Select(p=>p.Value).OfType<JValue>().Select(v=>v.ToString()).Where(s=>!string.IsNullOrWhiteSpace(s)).Distinct().OrderBy(s=>s))speaker.Items.Add(new Choice{Id=serial,Name=DossierRoomQuickData.Speaker(effective,(string)entry.Path[0],serial)});
    speaker.Enabled=parent!=null&&entry.Path.Last() is string field&&field=="Blurb"&&(parent["ActorSerial"]==null||parent["ActorSerial"].Type==JTokenType.String);string actor=parent?["ActorSerial"]?.ToString()??"";speaker.SelectedItem=speaker.Items.Cast<Choice>().FirstOrDefault(c=>c.Id==actor);if(speaker.SelectedIndex<0)speaker.Text=actor;
   }else{characterId.Enabled=displayName.Enabled=role.Enabled=faction.Enabled=controller.Enabled=characterChoices.Enabled=true;characterId.Text=entry.Value["myType"]?.ToString()??"";displayName.Text=entry.Value["DisplayedName"]?.ToString()??"";Select(faction,entry.Value["myFaction"]?.ToString());Select(controller,entry.Value["myControlType"]?.ToString());role.SelectedIndex=0;FilterCharacters();}
  }
  loading=false;
 }
 void FilterCharacters()
 {
  bool wasLoading=loading;loading=true;var query=characterSearch.Text.Trim();characterChoices.BeginUpdate();characterChoices.Items.Clear();
  foreach(var c in ModEnums.DB_CHAR.Select(p=>new Choice{Id=p.Key,Name=(p.Value["FullName"]?.ToString()??p.Key)+" · "+p.Key}).Where(c=>c.Name.IndexOf(query,StringComparison.OrdinalIgnoreCase)>=0).OrderBy(c=>c.Name))characterChoices.Items.Add(c);
  characterChoices.SelectedItem=characterChoices.Items.Cast<Choice>().FirstOrDefault(c=>c.Id==characterId.Text);characterChoices.EndUpdate();loading=wasLoading;
 }
 bool Commit()
 {
  if(!dirty||current==null)return true;
  try{
   var edits=new List<KeyValuePair<object[],JToken>>();
   if(dialogue){if(changed.Contains("text"))edits.Add(new KeyValuePair<object[],JToken>(current.Path,new JValue(line.Text.Replace("\r\n","\n"))));if(changed.Contains("ActorSerial")&&speaker.Enabled){var choice=speaker.SelectedItem as Choice;string serial=choice!=null&&speaker.Text==choice.Name?choice.Id:speaker.Text;edits.Add(new KeyValuePair<object[],JToken>(current.Path.Take(current.Path.Length-1).Concat(new object[]{"ActorSerial"}).ToArray(),new JValue(serial)));}}
   else{
    foreach(var key in changed){JToken value;if(key=="myType"){if(string.IsNullOrWhiteSpace(characterId.Text))throw new FormatException("Choose a character or enter its preset ID.");value=new JValue(characterId.Text.Trim());}else if(key=="DisplayedName")value=new JValue(displayName.Text);else{var choice=(key=="myFaction"?faction:controller).SelectedItem as Choice;int n;if(!int.TryParse(choice?.Id,out n))throw new FormatException("Choose a faction or controller value.");value=new JValue(n);}edits.Add(new KeyValuePair<object[],JToken>(current.Path.Concat(new object[]{key}).ToArray(),value));}
   }
   document.Change(()=>{foreach(var edit in edits)if(!JToken.DeepEquals(DossierRoomQuickData.At(effective,edit.Key),edit.Value))DossierRoomQuickData.Write(document.Data,effective,edit.Key,edit.Value);});
   dirty=false;changed.Clear();effective=DossierRoomQuickData.Effective(document.Data,template);
   var updated=(dialogue?DossierRoomQuickData.Dialogue(effective):DossierRoomQuickData.Characters(effective)).ToDictionary(e=>e.Key);
   loading=true;for(int i=0;i<list.Items.Count;i++)if(list.Items[i] is DossierRoomQuickData.Entry item&&updated.TryGetValue(item.Key,out var latest)){item.Value=latest.Value;item.Label=latest.Label;list.Items[i]=item;}list.Refresh();loading=false;return true;
  }catch(Exception ex){status.Text="Could not apply: "+ex.Message;return false;}
 }
 void Next(int direction){if(!Commit())return;int next=list.SelectedIndex+direction;if(next>=0&&next<list.Items.Count)list.SelectedIndex=next;else status.Text="Reached the end. Close and Save the room to write your edits.";}
}
