using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MadCardMaker;

public sealed class MakerIssue
{
 public string Field,Message;public bool Error;
 public override string ToString()=>(Error?"ERROR":"CHECK")+" · "+Field+" — "+Message;
}

public static class MakerStudio
{
 public static bool ValidId(string id)=>!string.IsNullOrWhiteSpace(id)&&id==id.Trim()&&id.IndexOfAny(Path.GetInvalidFileNameChars())<0&&id!="."&&id!=".."&&!id.EndsWith(".");
 public static JObject EncodeDeck(JObject deck)
 {
  var copy=(JObject)deck.DeepClone();Encode(copy);return copy;
 }
 static void Encode(JToken token)
 {
  if(token is JArray array){if(array.Count==0){array.Add("empty");return;}foreach(var child in array.ToArray())Encode(child);}
  else if(token is JObject obj)foreach(var p in obj.Properties().ToArray())Encode(p.Value);
 }
 public static List<MakerIssue> Check(string type,JObject card,JObject full=null)
 {
  var issues=new List<MakerIssue>();void Add(string field,string message,bool error=false)=>issues.Add(new MakerIssue{Field=field,Message=message,Error=error});
  if(!ValidId((string)card["Filename"]))Add("Filename","Enter a valid card ID without path separators or surrounding spaces.",true);
  string root=(string)card["Root"];
  if(!ModEnums.CARDTYPES.Contains(type)){Add("Type","Unknown card category.",true);return issues;}
  if(!new[]{"Event","Cutscene","Mission","Driver"}.Contains(type)){
   if(string.IsNullOrWhiteSpace(root))Add("Root","Choose an inherited card.",true);
   else if(!Exists(type,root))Add("Root","The inherited card cannot be found in the current database or loaded mods.",true);
  }
  if(card["FullName"]!=null||card["myRef"]!=null)Add("Format","This resembles legacy NEM loose JSON. Use MadCard fields such as Name, ObjRef, Filename and Root.",true);
  var value=full??card;
  if(type=="Character"){
   foreach(var field in new[]{"Health","Scale"})if(value[field] is JValue n&&double.TryParse(n.ToString(),out var amount)&&amount<=0)Add(field,"A playable combat character normally needs a positive value.");
   if(value["CorpusBlocks"] is JArray blocks&&(blocks.Count==0||(blocks.Count==1&&(string)blocks[0]=="empty")))Add("CorpusBlocks","No health blocks are assigned. A normal combat character needs at least one.");
   if(value["Attributes"] is JArray attributes&&attributes.Count!=4)Add("Attributes","Character attributes normally have four entries.");
  }
  if(value["Description"]?.ToString().Length==0)Add("Description","A short description will make this card easier to identify later.");
  return issues;
 }
 public static bool Exists(string type,string id)
 {
  if(string.IsNullOrEmpty(id)||id=="None"||id=="empty")return true;
  if(type=="SkillGroups")type="SkillGroup";
  return ModEnums.SC_DIR.TryGetValue(type,out var a)&&a.ContainsKey(id)||ModEnums.MC_DIR.TryGetValue(type,out var b)&&b.ContainsKey(id)||ModEnums.ASSETS.TryGetValue(type,out var c)&&c.Contains(id)||ModEnums.ENUMS.TryGetValue(type,out var e)&&e.Contains(id);
 }
 public static JObject Duplicate(JObject card,string id)
 {
  if(!ValidId(id))throw new FormatException("Enter a valid card ID.");var copy=(JObject)card.DeepClone();copy["Filename"]=id;return copy;
 }
 public static string NotesPath(string project)
 {
  using(var sha=SHA256.Create()){
   var key=BitConverter.ToString(sha.ComputeHash(Encoding.UTF8.GetBytes(Path.GetFullPath(project).TrimEnd('\\').ToUpperInvariant()))).Replace("-","").Substring(0,24);
   return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),"MadCardMakerRevanced","Projects",key+".json");
  }
 }
 public static void TextReport(IWin32Window owner,string title,string text)
 {
  using(var f=new Form{Text=title,Size=new Size(860,650),MinimumSize=new Size(620,420),StartPosition=FormStartPosition.CenterParent}){
   var root=new TableLayoutPanel{Dock=DockStyle.Fill,Padding=new Padding(20),RowCount=2,ColumnCount=1};root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,100));root.RowStyles.Add(new RowStyle(SizeType.Percent,100));root.RowStyles.Add(new RowStyle(SizeType.Absolute,46));
   var box=new TextBox{Text=text,Dock=DockStyle.Fill,Multiline=true,ReadOnly=true,ScrollBars=ScrollBars.Both,WordWrap=false,AccessibleName=title};root.Controls.Add(box,0,0);
   var actions=new FlowLayoutPanel{Dock=DockStyle.Fill};var copy=new DossierButton{Text="Copy report",Width=150,Height=34};copy.Click+=(_,__)=>{try{Clipboard.SetText(box.Text);}catch(Exception e){DossierDialog.Show(f,e.Message);}};actions.Controls.Add(copy);var save=new DossierButton{Text="Save report…",Width=150,Height=34};save.Click+=(_,__)=>{try{using(var picker=new DossierSaveFileDialog{Title="Save review report",FileName="MCM-review.txt",Filter="Text report (*.txt)|*.txt",DefaultExt="txt"})if(picker.ShowDialog(f)==DialogResult.OK)MakerHelp.Write(picker.FileName,title+"\r\n\r\n"+box.Text);}catch(Exception e){DossierDialog.Show(f,e.Message);}};actions.Controls.Add(save);root.Controls.Add(actions,0,1);f.Controls.Add(root);DossierStyle.Apply(f);box.Font=DossierFonts.Create("Consolas",10);f.ShowDialog(owner);
  }
 }
 public static string Prompt(IWin32Window owner,string title,string label,string initial)
 {
  using(var f=new Form{Text=title,Size=new Size(540,250),StartPosition=FormStartPosition.CenterParent,MaximizeBox=false,MinimizeBox=false}){
   var root=new TableLayoutPanel{Dock=DockStyle.Fill,Padding=new Padding(22),ColumnCount=1,RowCount=3};root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,100));root.RowStyles.Add(new RowStyle(SizeType.Absolute,32));root.RowStyles.Add(new RowStyle(SizeType.Absolute,42));root.RowStyles.Add(new RowStyle(SizeType.Percent,100));root.Controls.Add(new Label{Text=label,Dock=DockStyle.Fill},0,0);var input=new TextBox{Text=initial,Dock=DockStyle.Fill};root.Controls.Add(input,0,1);var go=new DossierButton{Text="Continue",DialogResult=DialogResult.OK,Width=150,Height=35};root.Controls.Add(go,0,2);f.AcceptButton=go;f.Controls.Add(root);DossierStyle.Apply(f);f.Shown+=(_,__)=>{input.Focus();input.SelectAll();};return f.ShowDialog(owner)==DialogResult.OK?input.Text:null;
  }
 }
}

public sealed class MakerPreset
{
 public string Name,Type,Root,Summary;public JObject Values;
 public override string ToString()=>Name;
 public static MakerPreset[] All=>new[]{
  P("Patrol scout","Character","AAHW1_Agent","A lighter patrol character: quick to set up, modest health.",new JObject{{"Health",7.0},{"Level",3},{"TacBar",0.5}}),
  P("Frontline guard","Character","AAHW2_Engineer","A sturdier starting point for defensive encounters.",new JObject{{"Health",12.0},{"Level",8},{"TacBar",1.0}}),
  P("Elite soldier","Character","AAHW3_Soldat","An experienced combatant with a larger health reserve.",new JObject{{"Health",18.0},{"Level",15},{"TacBar",1.0}}),
  P("Scrappy rookie","Character","AAHW0_Grunt","Low-level opposition with a short health track.",new JObject{{"Health",6.0},{"Level",1},{"TacBar",0.0}}),
  P("Squad veteran","Character","AAHW2_Engineer","An experienced squad member without oversized proportions.",new JObject{{"Health",14.0},{"Level",12},{"Scale",1.0}}),
  P("Heavy specialist","Character","AAHW3_Soldat","A tougher enemy. Health, level and silhouette can be adjusted before saving.",new JObject{{"Health",22.0},{"Level",18},{"Scale",1.08}}),
  P("Controlled SMG","Ranged","R_MP5","Lower cyclic speed and stronger stability, using the original MP5 model.",new JObject{{"ROF",700.0},{"Stability",82.0}}),
  P("Patrol carbine","Ranged","R_AN15","A deliberate rifle variant with a modest accuracy adjustment.",new JObject{{"ROF",550.0},{"Accuracy",5.0}}),
  P("Heavy sidearm","Ranged","R_Deagle","A slower follow-up with a small damage adjustment. Original caliber is inherited.",new JObject{{"Damage",0.5},{"Recovery",45.0}}),
  P("Durable blade","Melee","M_Katana","A longer-lived blade with a modest defensive adjustment.",new JObject{{"MeleeAmmo",380},{"MeleeDefense",6.0}}),
  P("Worksite tool","Melee","M_Pipewrench","A durable improvised weapon for worker or salvage characters.",new JObject{{"MeleeAmmo",300}}),
  P("Custom chatter","Chatter","AAHW_Grunt","A small, editable set of direct combat lines. The other events inherit from the root.",new JObject{{"Spawned",new JArray("Hold this position.","Watch the next doorway.")},{"Attacking",new JArray("Contact ahead!","Cover the approach!")},{"Wounded",new JArray("Need cover!","Still standing!")}})
 };
 static MakerPreset P(string name,string type,string root,string summary,JObject values)=>new MakerPreset{Name=name,Type=type,Root=root,Summary=summary,Values=values};
 public JObject Create(string id,string name,string root)
 {
  if(!MakerStudio.ValidId(id))throw new FormatException("Enter a valid card ID.");
  if(!MakerStudio.Exists(Type,root))throw new FormatException("Choose a root from this database.");
  var result=(JObject)Values.DeepClone();result["Filename"]=id;result["Root"]=root;result["Name"]=name;result["NameShort"]=name;result["Description"]=Summary;return result;
 }
}

public sealed class MakerPresetDialog:Form
{
 public MakerPreset Preset;public JObject Card;
 readonly ListBox choices=new ListBox{Dock=DockStyle.Fill};readonly ComboBox roots=new ComboBox{Dock=DockStyle.Fill,DropDownStyle=ComboBoxStyle.DropDownList};
 readonly TextBox name=new TextBox{Dock=DockStyle.Fill,Text="New character"},id=new TextBox{Dock=DockStyle.Fill,Text="My_Character"};readonly Label description=new Label{Dock=DockStyle.Fill,AutoEllipsis=true};
 public MakerPresetDialog(){Text="Start from a preset";Size=new Size(880,600);MinimumSize=new Size(760,540);StartPosition=FormStartPosition.CenterParent;
  var layout=new TableLayoutPanel{Dock=DockStyle.Fill,Padding=new Padding(22),ColumnCount=2,RowCount=1};layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,38));layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,62));choices.Margin=new Padding(0,0,20,0);choices.Items.AddRange(MakerPreset.All);layout.Controls.Add(choices,0,0);
  var fields=new TableLayoutPanel{Dock=DockStyle.Fill,ColumnCount=1,RowCount=9};fields.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,100));foreach(var h in new[]{95,27,40,27,40,27,40})fields.RowStyles.Add(new RowStyle(SizeType.Absolute,h));fields.RowStyles.Add(new RowStyle(SizeType.Percent,100));fields.RowStyles.Add(new RowStyle(SizeType.Absolute,44));
  fields.Controls.Add(description,0,0);fields.Controls.Add(new Label{Text="Display name",Dock=DockStyle.Fill},0,1);fields.Controls.Add(name,0,2);fields.Controls.Add(new Label{Text="Card ID · used by other cards",Dock=DockStyle.Fill},0,3);fields.Controls.Add(id,0,4);fields.Controls.Add(new Label{Text="Inherited root",Dock=DockStyle.Fill},0,5);fields.Controls.Add(roots,0,6);fields.Controls.Add(new Label{Text="This creates an editable draft. Review its equipment and behavior before saving it to your deck.",Dock=DockStyle.Fill},0,7);var create=new DossierButton{Text="Open editable draft",Dock=DockStyle.Fill};fields.Controls.Add(create,0,8);layout.Controls.Add(fields,1,0);Controls.Add(layout);
  choices.SelectedIndexChanged+=(_,__)=>{var p=(MakerPreset)choices.SelectedItem;description.Text=p.Type+"\r\n\r\n"+p.Summary;roots.Items.Clear();if(ModEnums.SC_DIR.TryGetValue(p.Type,out var cards))roots.Items.AddRange(cards.Keys.Cast<object>().ToArray());roots.SelectedItem=p.Root;if(roots.SelectedIndex<0&&roots.Items.Count>0)roots.SelectedIndex=0;name.Text=p.Name;id.Text="My_"+p.Name.Replace(' ','_');};
  create.Click+=(_,__)=>{try{Preset=(MakerPreset)choices.SelectedItem;Card=Preset.Create(id.Text,name.Text,roots.Text);DialogResult=DialogResult.OK;Close();}catch(Exception ex){DossierDialog.Show(this,ex.Message);}};DossierStyle.Apply(this);choices.SelectedIndex=0;
 }
}

public sealed class MakerNotebook:Form
{
 readonly string path;readonly JArray entries;readonly ListBox list=new ListBox{Dock=DockStyle.Fill};readonly TextBox title=new TextBox{Dock=DockStyle.Fill},body=new TextBox{Dock=DockStyle.Fill,Multiline=true,ScrollBars=ScrollBars.Vertical};readonly ComboBox status=new ComboBox{Dock=DockStyle.Fill,DropDownStyle=ComboBoxStyle.DropDownList};bool loading;int selected=-1;
 public MakerNotebook(string project,string cardId){Text="Project notebook";Size=new Size(940,650);MinimumSize=new Size(760,500);StartPosition=FormStartPosition.CenterParent;path=MakerStudio.NotesPath(project);
  try{entries=File.Exists(path)?JArray.Parse(File.ReadAllText(path)):new JArray();}catch(Exception ex){throw new IOException("The project notebook could not be read. It has been left untouched.",ex);}
  var root=new TableLayoutPanel{Dock=DockStyle.Fill,Padding=new Padding(20),ColumnCount=2,RowCount=1};root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,35));root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,65));
  var left=new TableLayoutPanel{Dock=DockStyle.Fill,ColumnCount=1,RowCount=2,Padding=new Padding(0,0,16,0)};left.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,100));left.RowStyles.Add(new RowStyle(SizeType.Percent,100));left.RowStyles.Add(new RowStyle(SizeType.Absolute,44));left.Controls.Add(list,0,0);var add=new DossierButton{Text="Add note / idea",Dock=DockStyle.Fill};left.Controls.Add(add,0,1);root.Controls.Add(left,0,0);
  var right=new TableLayoutPanel{Dock=DockStyle.Fill,ColumnCount=1,RowCount=6};right.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,100));foreach(var h in new[]{28,40,40})right.RowStyles.Add(new RowStyle(SizeType.Absolute,h));right.RowStyles.Add(new RowStyle(SizeType.Percent,100));right.RowStyles.Add(new RowStyle(SizeType.Absolute,32));right.RowStyles.Add(new RowStyle(SizeType.Absolute,44));right.Controls.Add(new Label{Text="Note title",Dock=DockStyle.Fill},0,0);right.Controls.Add(title,0,1);status.Items.AddRange(new object[]{"Idea","To do","In progress","Done"});right.Controls.Add(status,0,2);right.Controls.Add(body,0,3);right.Controls.Add(new Label{Text="Notes stay in the editor; they are not included in game decks.",Dock=DockStyle.Fill},0,4);var save=new DossierButton{Text="Save notebook",Dock=DockStyle.Fill};right.Controls.Add(save,0,5);root.Controls.Add(right,1,0);Controls.Add(root);
  list.SelectedIndexChanged+=(_,__)=>{Commit();selected=list.SelectedIndex;loading=true;if(selected>=0){var e=entries[selected];title.Text=(string)e["Title"];body.Text=(string)e["Text"];status.SelectedItem=(string)e["Status"]??"Idea";}loading=false;};
  add.Click+=(_,__)=>{Commit();entries.Add(new JObject{{"Title",string.IsNullOrEmpty(cardId)?"New idea":cardId},{"Card",cardId},{"Text",""},{"Status","Idea"}});RefreshList(entries.Count-1);};
  save.Click+=(_,__)=>{try{Persist();DossierFeedback.Play("save");save.Text="Notebook saved";}catch(Exception ex){DossierDialog.Show(this,ex.Message);}};
  FormClosing+=(_,e)=>{try{Persist();}catch(Exception ex){e.Cancel=true;DossierDialog.Show(this,ex.Message);}};DossierStyle.Apply(this);RefreshList(entries.Count>0?0:-1);
 }
 void Commit(){if(loading||selected<0||selected>=entries.Count)return;entries[selected]["Title"]=title.Text;entries[selected]["Text"]=body.Text;entries[selected]["Status"]=status.Text;}
 void RefreshList(int index){loading=true;selected=-1;list.Items.Clear();foreach(var e in entries)list.Items.Add("["+e["Status"]+"] "+e["Title"]);loading=false;list.SelectedIndex=index;}
 void Persist(){Commit();MakerHelp.Write(path,entries.ToString());RefreshList(selected);}
}
