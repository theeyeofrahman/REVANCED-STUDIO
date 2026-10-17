using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace MPNDossier;
public sealed partial class DossierRoomEditor:Form
{
 readonly DossierRoomDocument document=new DossierRoomDocument();
 readonly ListBox files=new ListBox{Dock=DockStyle.Fill,HorizontalScrollbar=true};
 readonly TextBox fileSearch=new TextBox{Dock=DockStyle.Top,AccessibleName="Filter room files"};
 readonly TreeView tree=new TreeView{Dock=DockStyle.Fill,HideSelection=false};
 readonly TextBox fieldName=new TextBox{Dock=DockStyle.Fill},value=new TextBox{Dock=DockStyle.Fill,Multiline=true,AcceptsReturn=true,ScrollBars=ScrollBars.Both,WordWrap=false};
 readonly ComboBox type=new ComboBox{Dock=DockStyle.Fill,DropDownStyle=ComboBoxStyle.DropDownList},choices=new ComboBox{Dock=DockStyle.Fill,DropDownStyle=ComboBoxStyle.DropDownList};
 readonly TextBox help=new TextBox{Dock=DockStyle.Fill,Multiline=true,ReadOnly=true,ScrollBars=ScrollBars.Vertical};
 readonly Label status=new Label{Dock=DockStyle.Bottom,Height=38,Padding=new Padding(16,6,0,0),AutoEllipsis=true};
 readonly TextBox find=new TextBox{Width=180,AccessibleName="Find a room field or value"};
 List<string> roomFiles=new List<string>();string folder;bool loading,pending,selecting;
 public static void Open(IWin32Window owner,string path=null)
 {var editor=new DossierRoomEditor();if(path!=null)editor.OpenPath(path);StudioModule.ShowDocument(editor,owner);}
 public DossierRoomEditor(bool discover=true)
 {
  DossierDpi.Initialize(this);Text="MPN Dossier REVANCED — Room editor";Size=new Size(1460,940);MinimumSize=new Size(1120,760);StartPosition=FormStartPosition.CenterParent;
  var root=new TableLayoutPanel{Dock=DockStyle.Fill,Padding=new Padding(16),ColumnCount=1,RowCount=3};root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,100));root.RowStyles.Add(new RowStyle(SizeType.Absolute,84));root.RowStyles.Add(new RowStyle(SizeType.Absolute,44));root.RowStyles.Add(new RowStyle(SizeType.Percent,100));
  var tools=new FlowLayoutPanel{Dock=DockStyle.Fill,WrapContents=true};
  void Tool(string name,Action action){var button=new DossierButton{Text=name,Width=100,Height=34};button.Click+=(_,__)=>Run(action);tools.Controls.Add(button);}
  Tool("Open room",OpenPicker);Tool("Characters",()=>QuickEdit(false));Tool("Dialogue",()=>QuickEdit(true));Tool("Make copy",MakeRoomCopy);Tool("Folder…",BrowseFolder);Tool("Save",()=>Save(false));Tool("Save as…",()=>Save(true));Tool("Undo",()=>Travel(-1));Tool("Redo",()=>Travel(1));Tool("Check file",Check);tools.Controls.Add(find);Tool("Find next",FindNext);Tool("Help",()=>DossierHelp.Open(this));root.Controls.Add(tools,0,0);
  root.Controls.Add(new Label{Text="Open a room → Characters to change players, enemies and allies → Dialogue to edit lines quickly → Save. Simple view handles other settings.",Dock=DockStyle.Fill},0,1);
  var panes=new TableLayoutPanel{Dock=DockStyle.Fill,ColumnCount=3,RowCount=1};panes.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,22));panes.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,37));panes.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,41));
  var browser=new Panel{Dock=DockStyle.Fill,Padding=new Padding(0,0,12,0)};browser.Controls.Add(files);browser.Controls.Add(fileSearch);browser.Controls.Add(new Label{Text="Room files and templates",Dock=DockStyle.Top,Height=28});panes.Controls.Add(browser,0,0);
  var outline=new Panel{Dock=DockStyle.Fill,Padding=new Padding(0,0,12,0)};outline.Controls.Add(tree);var structure=new FlowLayoutPanel{Dock=DockStyle.Bottom,Height=112,WrapContents=true};
  void Structure(string label,Action action){var b=new DossierButton{Text=label,Width=108,Height=30};b.Click+=(_,__)=>Run(action);structure.Controls.Add(b);}
  Structure("Add…",Add);Structure("Duplicate",Duplicate);Structure("Remove",Remove);Structure("Move up",()=>Reorder(-1));Structure("Move down",()=>Reorder(1));Structure("New room",NewRoom);outline.Controls.Add(structure);panes.Controls.Add(outline,1,0);
  var inspector=new TableLayoutPanel{Dock=DockStyle.Fill,ColumnCount=1,RowCount=9};inspector.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,100));foreach(var height in new[]{24,32,24,34,24})inspector.RowStyles.Add(new RowStyle(SizeType.Absolute,height));inspector.RowStyles.Add(new RowStyle(SizeType.Percent,65));inspector.RowStyles.Add(new RowStyle(SizeType.Absolute,36));inspector.RowStyles.Add(new RowStyle(SizeType.Absolute,34));inspector.RowStyles.Add(new RowStyle(SizeType.Percent,35));
  inspector.Controls.Add(new Label{Text="Property name / object ID",Dock=DockStyle.Fill},0,0);inspector.Controls.Add(fieldName,0,1);inspector.Controls.Add(new Label{Text="Value type",Dock=DockStyle.Fill},0,2);type.Items.AddRange(new object[]{"String","Integer","Number","Boolean","Null","Object","Array"});inspector.Controls.Add(type,0,3);inspector.Controls.Add(new Label{Text="Value (plain text for strings; JSON for objects/arrays)",Dock=DockStyle.Fill},0,4);inspector.Controls.Add(value,0,5);
  var apply=new DossierButton{Text="Apply value",Dock=DockStyle.Fill};apply.Click+=(_,__)=>ApplySelected();inspector.Controls.Add(apply,0,6);inspector.Controls.Add(choices,0,7);inspector.Controls.Add(help,0,8);panes.Controls.Add(inspector,2,0);root.Controls.Add(panes,0,2);Controls.Add(root);Controls.Add(status);ConfigureSimpleView(inspector,browser,outline,structure);DossierStyle.Apply(this);value.Font=DossierFonts.Create(DossierStyle.Theme.FontFamily,11);
  tree.BeforeSelect+=(_,e)=>{if(selecting||!pending)return;var old=tree.SelectedNode;bool inside=false;for(var n=e.Node.Parent;n!=null;n=n.Parent)if(n==old)inside=true;bool rebuilds=views.SelectedIndex==1&&old?.Tag is JContainer;if(!ApplySelected()||(inside&&rebuilds))e.Cancel=true;};
  tree.AfterSelect+=(_,__)=>ReadInspector();fieldName.TextChanged+=(_,__)=>MarkPending();value.TextChanged+=(_,__)=>MarkPending();type.SelectedIndexChanged+=(_,__)=>MarkPending();choices.SelectionChangeCommitted+=(_,__)=>{if(choices.SelectedItem is RoomChoice c&&choices.SelectedIndex>0)value.Text=c.Value;};
  files.SelectedIndexChanged+=(_,__)=>{if(!selecting&&files.SelectedItem is RoomFile file){if(TryLeave())Run(()=>OpenPath(file.Path));else{selecting=true;files.SelectedItem=files.Items.Cast<RoomFile>().FirstOrDefault(f=>f.Path==sourcePath);selecting=false;}}};fileSearch.TextChanged+=(_,__)=>FilterFiles();find.KeyDown+=(_,e)=>{if(e.KeyCode==Keys.Enter){FindNext();e.SuppressKeyPress=true;}};
  FormClosing+=(_,e)=>{if(!TryLeave())e.Cancel=true;};KeyPreview=true;KeyDown+=(_,e)=>{if(e.Control&&e.KeyCode==Keys.S){Save(false);e.SuppressKeyPress=true;}else if(e.Control&&e.KeyCode==Keys.F){find.Focus();find.SelectAll();e.SuppressKeyPress=true;}else if(e.Control&&!value.Focused&&(e.KeyCode==Keys.Z||e.KeyCode==Keys.Y)){Travel(e.KeyCode==Keys.Z?-1:1);e.SuppressKeyPress=true;}};
  DossierDpi.Complete(this);if(discover){var initial=LocateRooms();if(initial!=null)Run(()=>LoadFolder(initial));}UpdateTitle();
 }
 void Run(Action action){try{action();}catch(Exception ex){status.Text=ex.Message;}}
 void MarkPending(){if(!loading){pending=true;UpdateTitle();}}
 public void SetRoomFolder(string path){LoadFolder(path);}
 public void OpenPath(string path){document.Load(path);PrepareTemplate(path);pending=false;Rebuild();UpdateTitle();status.Text=IsTemplate(path)?"Template opened as a copy. Save chooses a destination; the template stays unchanged.":"Loaded "+path;}
 static string LocateRooms()
 {
  var saved=DossierPreferences.Current.RoomFolder;if(!string.IsNullOrEmpty(saved)&&Directory.Exists(saved))return saved;
  if(!string.IsNullOrEmpty(ModEnums.DIR_TRANSFER)){var direct=Path.Combine(ModEnums.DIR_TRANSFER,"rooms");if(Directory.Exists(direct))return direct;}
  var downloads=Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),"Downloads");if(Directory.Exists(downloads))foreach(var dir in Directory.GetDirectories(downloads,"NotEnoughMadness*")){var candidate=Path.Combine(dir,"NotEnoughMadness","Custom Stuff","rooms");if(Directory.Exists(candidate))return candidate;}return null;
 }
 void LoadFolder(string path){folder=path;var issues=new List<string>();roomFiles=DossierDecks.Scan(path,issues).Where(p=>p.EndsWith(".json",StringComparison.OrdinalIgnoreCase)).ToList();FilterFiles();status.Text=roomFiles.Count+" room files / templates"+(issues.Count>0?" • "+issues[0]:"");}
 void FilterFiles(){if(files==null)return;selecting=true;var previous=(files.SelectedItem as RoomFile)?.Path;var visible=roomFiles.Where(p=>{string label=folder==null?p:p.Substring(folder.TrimEnd('\\').Length).TrimStart('\\');return label.IndexOf(fileSearch.Text,StringComparison.OrdinalIgnoreCase)>=0&&(fileScope==null||fileScope.SelectedIndex==0||(fileScope.SelectedIndex==1&&!IsTemplate(p))||(fileScope.SelectedIndex==2&&IsTemplate(p)));}).Select(p=>new RoomFile{Path=p,Label=(IsTemplate(p)?"Template · ":"")+Path.GetFileNameWithoutExtension(p)}).ToArray();files.DataSource=visible;files.SelectedItem=visible.FirstOrDefault(f=>f.Path==(previous??sourcePath));if(fileCount!=null)fileCount.Text=visible.Length+" files · choose one below";selecting=false;}
 void BrowseFolder(){using(var dialog=new DossierFolderBrowserDialog{Description="Choose a rooms folder",SelectedPath=folder??""})if(dialog.ShowDialog(this)==DialogResult.OK){LoadFolder(dialog.SelectedPath);DossierPreferences.Current.RoomFolder=dialog.SelectedPath;DossierPreferences.Current.Persist();}}
 void OpenPicker(){if(!TryLeave())return;using(var dialog=new DossierOpenFileDialog{Filter="Room JSON (*.json)|*.json",InitialDirectory=folder??Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)})if(dialog.ShowDialog(this)==DialogResult.OK)OpenPath(dialog.FileName);}
 void UpdateTitle(){Text="MPN Dossier REVANCED — Room editor / "+(document.FilePath==null?"Unsaved room":Path.GetFileName(document.FilePath))+(document.Dirty||pending?" *":"");}
 void Rebuild(){string selected=tree.SelectedNode?.Tag is JToken old?AddressKey(old):null;var expanded=new HashSet<string>(Walk(tree.Nodes).Where(n=>n.IsExpanded&&n.Tag is JToken).Select(n=>AddressKey((JToken)n.Tag)));selecting=true;pending=false;tree.BeginUpdate();tree.Nodes.Clear();foreach(var property in document.Data.Properties())tree.Nodes.Add(Node(property.Value));foreach(var node in Walk(tree.Nodes)){if(node.Level==0||expanded.Contains(AddressKey((JToken)node.Tag)))node.Expand();}tree.SelectedNode=Walk(tree.Nodes).FirstOrDefault(n=>AddressKey((JToken)n.Tag)==selected)??tree.Nodes.Cast<TreeNode>().FirstOrDefault();tree.EndUpdate();selecting=false;ReadInspector();}
 TreeNode Node(JToken token){var node=new TreeNode(Label(token)){Tag=token,ToolTipText=(token.Parent as JProperty)?.Name??"List item"};if(token is JObject obj)foreach(var p in obj.Properties())node.Nodes.Add(Node(p.Value));else if(token is JArray arr)foreach(var item in arr)node.Nodes.Add(Node(item));return node;}
 string Label(JToken token){string raw=token.Parent is JProperty property?property.Name:token.Parent is JArray array?"Item "+(array.IndexOf(token)+1):"Room";string key=Friendly(raw);if(token is JObject obj){var reference=At(template,Address(token)) as JObject;if(token.Parent is JProperty actor&&actor.Parent?.Parent is JProperty section&&section.Name=="Characters"&&actor.Name.StartsWith("C-")){var effective=DossierRoomQuickData.Effective(obj,reference);return DossierRoomQuickData.ActorLabel(actor.Name,effective);}string name=(obj["myName"]??obj["name"]??obj["DisplayedName"]??reference?["myName"]??reference?["name"]??obj["myType"]??obj["myItem"])?.ToString();if(token.Parent is JArray&&obj["Index"]!=null)name="Step "+obj["Index"]+" · "+(obj["Blurb"]?.ToString().Replace("\n"," ")??obj["ActorSerial"]?.ToString());return (string.IsNullOrWhiteSpace(name)?key:name)+"  ·  "+obj.Count+" settings";}if(token is JArray a)return key+"  ·  "+a.Count+" items";var text=token.Type==JTokenType.Boolean?((bool)token?"On":"Off"):token.Type==JTokenType.Null?"Not set":token.ToString().Replace("\r"," ").Replace("\n"," ");return key+": "+(text.Length>70?text.Substring(0,70)+"…":text);}
 void ReadInspector()
 {
  loading=true;var token=tree.SelectedNode?.Tag as JToken;fieldName.Enabled=token?.Parent is JProperty;fieldName.Text=(token?.Parent as JProperty)?.Name??"";type.Enabled=value.Enabled=token!=null;
  if(token==null){type.SelectedIndex=-1;value.Clear();help.Text="Select a room, section, object or field. Existing IDs link overrides to objects in the game. Use a template for available IDs.";choices.Items.Clear();}
  else{type.SelectedItem=DossierRoomDocument.TypeName(token);value.Text=token.Type==JTokenType.String?token.ToString():token.ToString(Formatting.Indented);help.Text=FieldHelp(token);FillChoices(token);}
  BuildSimple(token);pending=false;loading=false;UpdateTitle();
 }
 bool ApplySelected()
 {
  if(!pending)return true;if(bindings.Any(b=>b.Changed))return ApplySimple();var node=tree.SelectedNode;var token=node?.Tag as JToken;if(token==null)return true;
  try{var replacement=DossierRoomDocument.ReadValue(type.SelectedItem?.ToString(),value.Text);var property=token.Parent as JProperty;
   if(property!=null&&(string.IsNullOrWhiteSpace(fieldName.Text)||(fieldName.Text!=property.Name&&((JObject)property.Parent).Property(fieldName.Text)!=null)))throw new FormatException("Use a non-empty property name that is not already present.");
   document.Change(()=>{if(property!=null)property.Replace(new JProperty(fieldName.Text,replacement));else token.Replace(replacement);});node.Tag=replacement;node.Text=Label(replacement);node.Nodes.Clear();if(replacement is JObject obj)foreach(var p in obj.Properties())node.Nodes.Add(Node(p.Value));else if(replacement is JArray array)foreach(var item in array)node.Nodes.Add(Node(item));pending=false;UpdateTitle();status.Text="Value applied. Save to write the room file.";return true;
  }catch(Exception ex){status.Text="Value not applied: "+ex.Message;return false;}
 }
 bool Save(bool saveAs)
 {
  if(!ApplySelected())return false;string path=document.FilePath;
  using(var dialog=new DossierSaveFileDialog{Filter="Room JSON (*.json)|*.json",DefaultExt="json",AddExtension=true,FileName=path==null?suggestedName:Path.GetFileName(path),InitialDirectory=path==null?(folder??Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)):Path.GetDirectoryName(path)}){
   if(saveAs||path==null){if(dialog.ShowDialog(this)!=DialogResult.OK)return false;path=dialog.FileName;}
   try{document.Save(path);UpdateTitle();status.Text="Saved "+path+DossierTransfer.AfterSave(path,"rooms");return true;}catch(Exception ex){status.Text="Could not save: "+ex.Message;return false;}
  }
 }
 bool TryLeave(){if(!pending&&!document.Dirty)return true;var answer=DossierDialog.Show(this,"Save your room changes before continuing?","Room changes",MessageBoxButtons.YesNoCancel);return answer==DialogResult.No||(answer==DialogResult.Yes&&Save(false));}
 void Travel(int delta){if(pending){ReadInspector();return;}if(delta<0)document.Undo();else document.Redo();Rebuild();UpdateTitle();}
 void Check(){var issues=document.Validate();status.Text=issues.Count==0?"Room structure is valid. Object IDs and game behavior still depend on the target scene.":string.Join(" • ",issues.Take(5));}
 void FindNext(){var nodes=Walk(tree.Nodes).ToArray();int start=Array.IndexOf(nodes,tree.SelectedNode);for(int i=1;i<=nodes.Length;i++){var candidate=nodes[(start+i+nodes.Length)%nodes.Length];if(candidate.Text.IndexOf(find.Text,StringComparison.OrdinalIgnoreCase)>=0){tree.SelectedNode=candidate;candidate.EnsureVisible();return;}}status.Text="No matching field or value.";}
 static IEnumerable<TreeNode> Walk(TreeNodeCollection nodes){foreach(TreeNode node in nodes){yield return node;foreach(var child in Walk(node.Nodes))yield return child;}}
 sealed class RoomFile{public string Path,Label;public override string ToString()=>Label;}
}
