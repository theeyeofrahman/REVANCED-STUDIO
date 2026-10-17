using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace MPNDossier;
public sealed partial class DossierRoomEditor
{
 DossierTabs views;FlowLayoutPanel simple;Label selectionTitle,saveHint,fileCount;ComboBox fileScope;
 JObject template;string sourcePath,suggestedName="MyRoom.json";bool switchingView;
 readonly List<SimpleBinding> bindings=new List<SimpleBinding>();
 void QuickEdit(bool dialogue,JToken selection=null)
 {
  if(!ApplySelected())return;
  if(document.Data.Count==0&&template==null){status.Text="Open a room file first.";return;}
  using(var editor=new DossierRoomQuickEditor(document,template,dialogue,selection==null?null:AddressKey(selection)))editor.ShowDialog(this);
  Rebuild();UpdateTitle();status.Text="Quick edits are in this room. Use Save to write the file; Undo restores earlier changes.";
 }
 sealed class SimpleBinding{public JToken Token;public Func<JToken> Read;public bool Changed;public Control Input;}
 static readonly Dictionary<string,string> FriendlyNames=new Dictionary<string,string>{
  {"Characters","Characters and spawns"},{"Interactives","Buttons and interactive objects"},{"Cutscenes","Dialogue and cutscenes"},{"Lockers","Item containers"},{"Areas","Triggers and areas"},{"Entrances","Doors and entrances"},{"myType","Character preset"},{"myName","Spawn name"},{"myFaction","Faction"},{"MyFaction","Faction"},{"myControlType","Controller"},{"alertStatus","Starting alert state"},{"itemsHeld","Weapons in hands"},{"itemsBack","Holstered weapons"},{"itemsWorn","Equipped armor"},{"itemThrown","Throwable"},{"Blurb","Dialogue text"},{"ActorSerial","Speaker / actor"},{"ActorSerials","Actors"},{"Steps","Dialogue steps"},{"Index","Step number"},{"myItem","Item preset"},{"myItemsStored","Stored items"},{"myWeaponsStored","Stored weapons"},{"disableSpawn","Disable spawning"},{"allPurposes","Interaction actions"},{"VocationRequirement","Required vocation"},{"VocationProhibited","Blocked vocation"},{"mySpeech","Speech text"},{"CustomTalk","Conversation lines"},{"CustomBye","Goodbye lines"},{"myPlayerLoad","Player loadout mode"}};
 static string Friendly(string key){string label;if(FriendlyNames.TryGetValue(key,out label))return label;return System.Text.RegularExpressions.Regex.Replace(key??"","([a-z])([A-Z])","$1 $2").Replace('_',' ');}
 static object[] Address(JToken token){var parts=new List<object>();for(var t=token;t?.Parent!=null;){if(t.Parent is JProperty p){parts.Add(p.Name);t=p.Parent;}else if(t.Parent is JArray a){parts.Add(a.IndexOf(t));t=a;}else break;}parts.Reverse();return parts.ToArray();}
 static string AddressKey(JToken token)=>JsonConvert.SerializeObject(Address(token));
 static JToken At(JToken root,object[] path){foreach(var part in path){if(part is int n&&root is JArray array)root=n>=0&&n<array.Count?array[n]:null;else if(part is string key&&root is JObject obj)root=obj[key];else return null;}return root;}
 void ConfigureSimpleView(TableLayoutPanel inspector,Panel browser,Panel outline,FlowLayoutPanel structure)
 {
  views=new DossierTabs{Dock=DockStyle.Fill};var basic=new TabPage("Simple view");var advanced=new TabPage("Advanced");views.TabPages.Add(basic);views.TabPages.Add(advanced);var host=(TableLayoutPanel)inspector.Parent;host.Controls.Remove(inspector);advanced.Controls.Add(inspector);host.Controls.Add(views,2,0);
  var shell=new TableLayoutPanel{Dock=DockStyle.Fill,Padding=new Padding(10),ColumnCount=1,RowCount=4};shell.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,100));shell.RowStyles.Add(new RowStyle(SizeType.Absolute,52));shell.RowStyles.Add(new RowStyle(SizeType.Percent,100));shell.RowStyles.Add(new RowStyle(SizeType.Absolute,40));shell.RowStyles.Add(new RowStyle(SizeType.Absolute,38));
  selectionTitle=new Label{Dock=DockStyle.Fill,AutoEllipsis=true};simple=new FlowLayoutPanel{Dock=DockStyle.Fill,FlowDirection=FlowDirection.TopDown,WrapContents=false,AutoScroll=true};var apply=new DossierButton{Text="Apply changes",Dock=DockStyle.Fill};apply.Click+=(_,__)=>ApplySelected();saveHint=new Label{Text="Changes stay in this window until you save the room.",Dock=DockStyle.Fill,AutoEllipsis=true};shell.Controls.Add(selectionTitle,0,0);shell.Controls.Add(simple,0,1);shell.Controls.Add(apply,0,2);shell.Controls.Add(saveHint,0,3);basic.Controls.Add(shell);
  simple.SizeChanged+=(_,__)=>{foreach(Control card in simple.Controls)card.Width=Math.Max(DossierDpi.Pixels(simple,180),simple.ClientSize.Width-SystemInformation.VerticalScrollBarWidth-8);};
  var oldAdd=structure.Controls.Cast<Control>().First(c=>c.Text=="Add…");oldAdd.Text="Add from template";oldAdd.Width=150;var newRoom=structure.Controls.Cast<Control>().First(c=>c.Text=="New room");newRoom.Visible=false;
  views.SelectedIndexChanged+=(_,__)=>{if(switchingView)return;if(pending&&!ApplySelected()){switchingView=true;views.SelectedIndex=views.SelectedIndex==0?1:0;switchingView=false;return;}newRoom.Visible=views.SelectedIndex==1;oldAdd.Text=views.SelectedIndex==0?"Add from template":"Add field…";ReadInspector();};
  var scopes=new Panel{Dock=DockStyle.Top,Height=76};fileScope=new ComboBox{Dock=DockStyle.Top,DropDownStyle=ComboBoxStyle.DropDownList};fileScope.Items.AddRange(new object[]{"All rooms and templates","Room overrides only","Templates only"});fileScope.SelectedIndex=0;fileScope.SelectedIndexChanged+=(_,__)=>FilterFiles();fileCount=new Label{Text="Choose a rooms folder to begin.",Dock=DockStyle.Bottom,Height=36,AutoEllipsis=true};scopes.Controls.Add(fileScope);scopes.Controls.Add(fileCount);browser.Controls.Add(scopes);
  var navigation=new FlowLayoutPanel{Dock=DockStyle.Bottom,Height=112,WrapContents=true};void Nav(string text,Action action){var b=new DossierButton{Text=text,Width=110,Height=30};b.Click+=(_,__)=>Run(action);navigation.Controls.Add(b);}Nav("Previous file",()=>AdjacentFile(-1));Nav("Next file",()=>AdjacentFile(1));Nav("Use template",OpenMatchingTemplate);browser.Controls.Add(navigation);
  var expand=new FlowLayoutPanel{Dock=DockStyle.Top,Height=38};var all=new DossierButton{Text="Expand all",Width=110,Height=30};all.Click+=(_,__)=>tree.ExpandAll();var collapse=new DossierButton{Text="Collapse all",Width=110,Height=30};collapse.Click+=(_,__)=>{tree.CollapseAll();foreach(TreeNode room in tree.Nodes)room.Expand();};expand.Controls.Add(all);expand.Controls.Add(collapse);outline.Controls.Add(expand);
  tree.ShowNodeToolTips=true;var menu=new ContextMenuStrip();menu.Items.Add("Copy identifier",null,(_,__)=>{var token=tree.SelectedNode?.Tag as JToken;if(token?.Parent is JProperty p)Clipboard.SetText(p.Name);});menu.Items.Add("Copy selected JSON",null,(_,__)=>{if(tree.SelectedNode?.Tag is JToken token)Clipboard.SetText(token.ToString(Formatting.Indented));});tree.ContextMenuStrip=menu;Disposed+=(_,__)=>menu.Dispose();
 }
 void AdjacentFile(int direction){int current=files.SelectedIndex;if(current<0)current=direction>0?-1:files.Items.Count;int next=current+direction;if(next>=0&&next<files.Items.Count)files.SelectedIndex=next;}
 static bool IsTemplate(string path)=>path.Split(Path.DirectorySeparatorChar,Path.AltDirectorySeparatorChar).Any(p=>p.Equals("template",StringComparison.OrdinalIgnoreCase));
 void PrepareTemplate(string path)
 {
  sourcePath=path;suggestedName=Path.GetFileName(path);template=null;string matching=IsTemplate(path)?path:roomFiles.FirstOrDefault(p=>IsTemplate(p)&&Path.GetFileName(p).Equals(suggestedName,StringComparison.OrdinalIgnoreCase));
  if(matching!=null)try{template=DossierRoomDocument.Parse(File.ReadAllText(matching));}catch{ }
  if(IsTemplate(path))document.MakeCopy(false);
 }
 void MakeRoomCopy(){if(!ApplySelected())return;if(document.FilePath!=null)suggestedName=Path.GetFileName(document.FilePath);document.MakeCopy();UpdateTitle();status.Text="Working copy created. Save will ask for a destination and leave the original file untouched.";}
 void OpenMatchingTemplate(){if(sourcePath==null){status.Text="Open a room first, then choose its matching template.";return;}var path=roomFiles.FirstOrDefault(p=>IsTemplate(p)&&Path.GetFileName(p).Equals(Path.GetFileName(sourcePath),StringComparison.OrdinalIgnoreCase));if(path==null){status.Text="No template with the same stage filename was found in this folder.";return;}if(TryLeave())OpenPath(path);}
 void BuildSimple(JToken selected)
 {
  if(simple==null)return;bindings.Clear();simple.SuspendLayout();foreach(Control old in simple.Controls.Cast<Control>().ToArray()){simple.Controls.Remove(old);old.Dispose();}
  selectionTitle.Text=selected==null?"Start here":selected is JContainer?Label(selected):Friendly((selected.Parent as JProperty)?.Name??"List item");
  void Text(string text){simple.Controls.Add(new Label{Text=text,AutoSize=false,Height=DossierDpi.Pixels(simple,96),Padding=new Padding(4),ForeColor=DossierStyle.Muted,BackColor=DossierStyle.Surface});}
  void OpenChild(JToken token){var b=new DossierButton{Text=Label(token)+"  ›",Height=DossierDpi.Pixels(simple,40)};b.Click+=(_,__)=>{var node=Walk(tree.Nodes).FirstOrDefault(n=>ReferenceEquals(n.Tag,token));if(node!=null){tree.SelectedNode=node;node.EnsureVisible();}};simple.Controls.Add(b);}
  if(selected==null)Text("1. Pick a room file on the left.\r\n2. Open a section, then choose an object.\r\n3. Edit its settings here and save.\r\nUse a template if you need more objects or fields.");
  else{
   bool room=selected.Parent is JProperty roomProperty&&ReferenceEquals(roomProperty.Parent,document.Data);
   Text(room?"Choose a category below to see its objects and settings. The game already supplies the room layout; this file changes what happens inside it.":selected is JValue?FieldHelp(selected):selected is JObject&&selected.Parent is JProperty entry&&DossierRoomDocument.Sections.Contains(entry.Name)?FieldHelp(selected):"Edit the settings below, then Apply changes. Open a nested setting with ›. Simple view keeps the original names and value types for you.");
   if(selected is JObject obj){if(obj.Parent is JProperty actor&&actor.Parent?.Parent is JProperty section&&section.Name=="Characters"&&actor.Name.StartsWith("C-")){var change=new DossierButton{Text="Change character / role…",Height=DossierDpi.Pixels(simple,40)};change.Click+=(_,__)=>QuickEdit(false,obj);simple.Controls.Add(change);}if(obj.Count==0)Text("No settings are present in this override. Add from template lists fields or object IDs available in the matching template.");foreach(var p in obj.Properties()){if(p.Value is JValue)AddSimpleField(p.Value);else OpenChild(p.Value);}}
   else if(selected is JArray array){Text("Choose an item below to edit it. Duplicate an existing item to make another; use Move up / down to change order.");if(array.Count==0)Text("This list is empty. Add from template can copy an item from the matching template.");foreach(var item in array){if(item is JValue)AddSimpleField(item);else OpenChild(item);}}
   else AddSimpleField(selected);
  }
  foreach(Control control in simple.Controls){control.Width=Math.Max(DossierDpi.Pixels(simple,180),simple.ClientSize.Width-SystemInformation.VerticalScrollBarWidth-8);DossierStyle.Apply(control);DossierDpi.Refresh(control);}simple.ResumeLayout(true);
 }
 void AddSimpleField(JToken token)
 {
  string key=(token.Parent as JProperty)?.Name??"Item "+(((JArray)token.Parent).IndexOf(token)+1);var row=new Panel{Height=DossierDpi.Pixels(simple,76),Margin=new Padding(0,4,0,6)};var caption=new Label{Text=Friendly(key),Dock=DockStyle.Top,Height=DossierDpi.Pixels(simple,24),AutoEllipsis=true};row.Controls.Add(caption);var binding=new SimpleBinding{Token=token};bindings.Add(binding);
  void Changed(){if(loading)return;binding.Changed=true;MarkPending();}
  if(token.Type==JTokenType.Boolean){var toggle=new CheckBox{Text="On",Checked=(bool)token,Dock=DockStyle.Bottom,Height=DossierDpi.Pixels(simple,38)};binding.Read=()=>new JValue(toggle.Checked);toggle.CheckedChanged+=(_,__)=>Changed();row.Controls.Add(toggle);}
  else{
   var options=SuggestionsFor(token).ToArray();if(options.Length>0){var combo=new ComboBox{Dock=DockStyle.Bottom,DropDownStyle=ComboBoxStyle.DropDown,AutoCompleteMode=AutoCompleteMode.SuggestAppend,AutoCompleteSource=AutoCompleteSource.ListItems};combo.Items.AddRange(options);var current=options.FirstOrDefault(c=>c.Value==token.ToString());if(current!=null)combo.SelectedItem=current;else combo.Text=token.ToString();binding.Read=()=>{var match=options.FirstOrDefault(c=>c.ToString()==combo.Text);return DossierRoomDocument.ReadValue(DossierRoomDocument.TypeName(token),match?.Value??combo.Text);};combo.TextChanged+=(_,__)=>Changed();row.Controls.Add(combo);}
   else if(token.Type==JTokenType.Null){row.Controls.Add(new Label{Text="No value set. Advanced view can change its type.",Dock=DockStyle.Bottom,Height=DossierDpi.Pixels(simple,40)});binding.Read=()=>token.DeepClone();}
   else{bool multiline=new[]{"Blurb","mySpeech","myChatter","Description"}.Contains(key)||token.Type==JTokenType.String&&token.ToString().Contains("\n");var input=new TextBox{Text=token.ToString(),Dock=DockStyle.Bottom,Multiline=multiline,ScrollBars=multiline?ScrollBars.Vertical:ScrollBars.None,Height=DossierDpi.Pixels(simple,multiline?90:30),AccessibleName=Friendly(key)};if(multiline)row.Height=DossierDpi.Pixels(simple,122);binding.Read=()=>DossierRoomDocument.ReadValue(DossierRoomDocument.TypeName(token),input.Text);input.TextChanged+=(_,__)=>Changed();row.Controls.Add(input);}
  }
  binding.Input=row.Controls.Cast<Control>().FirstOrDefault(c=>!(c is Label));simple.Controls.Add(row);
 }
 bool ApplySimple()
 {
  try{
   var edits=new List<KeyValuePair<JToken,JToken>>();foreach(var binding in bindings.Where(b=>b.Changed)){JToken replacement;try{replacement=binding.Read();}catch(Exception ex){binding.Input?.Focus();throw new FormatException(Friendly((binding.Token.Parent as JProperty)?.Name??"List item")+": "+ex.Message);}if(!JToken.DeepEquals(binding.Token,replacement))edits.Add(new KeyValuePair<JToken,JToken>(binding.Token,replacement));}
   document.Change(()=>{foreach(var edit in edits)edit.Key.Replace(edit.Value);});foreach(var edit in edits){var node=Walk(tree.Nodes).FirstOrDefault(n=>ReferenceEquals(n.Tag,edit.Key));if(node!=null){node.Tag=edit.Value;node.Text=Label(edit.Value);}}
   pending=false;ReadInspector();status.Text="Changes applied. Save the room when you are ready.";return true;
  }catch(Exception ex){status.Text="Check this value: "+ex.Message;return false;}
 }
 void AddFromTemplate()
 {
  if(!ApplySelected())return;var selected=tree.SelectedNode?.Tag as JToken;var options=TemplateChoices(selected);
  if(options.Count==0){status.Text="No matching template fields were found here. Use template opens the full stage template, or use Advanced for custom fields.";return;}
  using(var dialog=new RoomTemplatePicker(options))if(dialog.ShowDialog(this)==DialogResult.OK)ApplyTemplateChoice(selected,dialog.Result);
 }
 List<TemplateChoice> TemplateChoices(JToken selected)
 {
  var reference=selected==null?null:At(template,Address(selected));var options=new List<TemplateChoice>();
  if(selected is JObject obj&&reference is JObject original)foreach(var p in original.Properties().Where(p=>obj.Property(p.Name)==null))options.Add(new TemplateChoice{Key=p.Name,Value=p.Value.DeepClone()});
  else if(selected is JArray&&reference is JArray source)for(int i=0;i<source.Count;i++)options.Add(new TemplateChoice{Key="Item "+(i+1),Value=source[i].DeepClone()});return options;
 }
 void ApplyTemplateChoice(JToken selected,TemplateChoice choice){document.Change(()=>{if(selected is JObject parent)parent.Add(choice.Key,choice.Value.DeepClone());else ((JArray)selected).Add(choice.Value.DeepClone());});Rebuild();UpdateTitle();}
 internal sealed class TemplateChoice{public string Key;public JToken Value;public override string ToString()=>Friendly(Key)+(Value is JObject obj?" — "+(obj["myName"]??obj["name"]??obj["myType"]??obj["myItem"])?.ToString():" — "+Value.ToString().Replace("\n"," "));}
 sealed class RoomTemplatePicker:Form
 {
  public TemplateChoice Result{get;private set;}
  public RoomTemplatePicker(List<TemplateChoice> options){DossierDpi.Initialize(this);Text="Add from the matching template";Size=new Size(700,520);StartPosition=FormStartPosition.CenterParent;var list=new ListBox{Dock=DockStyle.Fill,HorizontalScrollbar=true,DataSource=options};var bottom=new FlowLayoutPanel{Dock=DockStyle.Bottom,Height=44};var add=new DossierButton{Text="Add selected",Width=150,Height=34};var cancel=new DossierButton{Text="Cancel",DialogResult=DialogResult.Cancel,Width=110,Height=34};bottom.Controls.Add(add);bottom.Controls.Add(cancel);Controls.Add(list);Controls.Add(bottom);Controls.Add(new Label{Text="These values come from the stage's real template. Review the copied settings before saving.",Dock=DockStyle.Top,Height=50});add.Click+=(_,__)=>{if(list.SelectedItem is TemplateChoice choice){Result=choice;DialogResult=DialogResult.OK;}};DossierStyle.Apply(this);CancelButton=cancel;DossierDpi.Complete(this);}
 }
}
