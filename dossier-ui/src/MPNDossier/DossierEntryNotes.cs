using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
namespace MPNDossier;
public sealed class DossierEntryNotes:Form
{
 readonly JObject data;readonly JToken original;
 readonly ListBox list=new ListBox{Dock=DockStyle.Fill,IntegralHeight=false,HorizontalScrollbar=true};
 readonly TextBox search=new TextBox{Dock=DockStyle.Fill,AccessibleName="Search notes"};
 readonly TextBox title=new TextBox{Dock=DockStyle.Fill,AccessibleName="Note title"};
 readonly TextBox body=new TextBox{Dock=DockStyle.Fill,Multiline=true,AcceptsReturn=true,ScrollBars=ScrollBars.Vertical,AccessibleName="Note text",MaxLength=int.MaxValue};
 readonly Label count=new Label{Dock=DockStyle.Fill,AutoEllipsis=true};
 JObject selected;bool reading,finished;
 JArray Entries=>(JArray)data["entries"];
 public JToken Result{get;private set;}
 public DossierEntryNotes(string entryName,JToken notes)
 {
  original=notes?.DeepClone();
  if(notes!=null&&(!(notes is JObject obj)||!(obj["entries"] is JArray items)||items.Any(n=>!(n is JObject note)||note["title"]?.Type!=JTokenType.String||note["text"]?.Type!=JTokenType.String)))throw new FormatException("The embedded notes use an unrecognized format. They have been left unchanged.");
  data=notes==null?new JObject(new JProperty("version",1),new JProperty("entries",new JArray())):(JObject)notes.DeepClone();
  DossierDpi.Initialize(this);Text="Notes / "+entryName;Size=new Size(920,700);MinimumSize=new Size(760,560);StartPosition=FormStartPosition.CenterParent;
  var root=new TableLayoutPanel{Dock=DockStyle.Fill,Padding=new Padding(18),ColumnCount=1,RowCount=3};root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,100));root.RowStyles.Add(new RowStyle(SizeType.Absolute,64));root.RowStyles.Add(new RowStyle(SizeType.Percent,100));root.RowStyles.Add(new RowStyle(SizeType.Absolute,48));
  root.Controls.Add(new Label{Text="Give this entry more personality: backstory, relationships, ideas or reminders.\r\nNotes live inside this entry's JSON. After applying, use Save JSON in the workbench.",Dock=DockStyle.Fill},0,0);
  var panes=new TableLayoutPanel{Dock=DockStyle.Fill,ColumnCount=2,RowCount=1};panes.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,32));panes.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,68));
  var sidebar=new TableLayoutPanel{Dock=DockStyle.Fill,ColumnCount=1,RowCount=5,Padding=new Padding(0,0,12,0)};sidebar.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,100));foreach(var h in new[]{24,34})sidebar.RowStyles.Add(new RowStyle(SizeType.Absolute,h));sidebar.RowStyles.Add(new RowStyle(SizeType.Percent,100));sidebar.RowStyles.Add(new RowStyle(SizeType.Absolute,112));sidebar.RowStyles.Add(new RowStyle(SizeType.Absolute,26));sidebar.Controls.Add(new Label{Text="Find a note",Dock=DockStyle.Fill},0,0);sidebar.Controls.Add(search,0,1);sidebar.Controls.Add(list,0,2);
  var actions=new FlowLayoutPanel{Dock=DockStyle.Fill,WrapContents=true};void Action(string label,System.Action action){var b=new DossierButton{Text=label,Width=94,Height=30};b.Click+=(_,__)=>action();actions.Controls.Add(b);}Action("New note",AddNote);Action("Duplicate",DuplicateNote);Action("Remove",RemoveNote);sidebar.Controls.Add(actions,0,3);sidebar.Controls.Add(count,0,4);
  var editor=new TableLayoutPanel{Dock=DockStyle.Fill,ColumnCount=1,RowCount=4};editor.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,100));editor.RowStyles.Add(new RowStyle(SizeType.Absolute,24));editor.RowStyles.Add(new RowStyle(SizeType.Absolute,38));editor.RowStyles.Add(new RowStyle(SizeType.Absolute,28));editor.RowStyles.Add(new RowStyle(SizeType.Percent,100));editor.Controls.Add(new Label{Text="Title",Dock=DockStyle.Fill},0,0);editor.Controls.Add(title,0,1);editor.Controls.Add(new Label{Text="Your notes",Dock=DockStyle.Fill},0,2);editor.Controls.Add(body,0,3);panes.Controls.Add(sidebar,0,0);panes.Controls.Add(editor,1,0);root.Controls.Add(panes,0,1);
  var bottom=new FlowLayoutPanel{Dock=DockStyle.Fill,FlowDirection=FlowDirection.RightToLeft};var apply=new DossierButton{Text="Apply to entry",Width=150,Height=36};var cancel=new DossierButton{Text="Cancel",Width=100,Height=36};bottom.Controls.Add(apply);bottom.Controls.Add(cancel);root.Controls.Add(bottom,0,2);Controls.Add(root);
  list.SelectedIndexChanged+=(_,__)=>SelectNote();search.TextChanged+=(_,__)=>RefreshList(selected);title.TextChanged+=(_,__)=>EditSelected();body.TextChanged+=(_,__)=>EditSelected();apply.Click+=(_,__)=>ApplyNotes();cancel.Click+=(_,__)=>Close();
  FormClosing+=(_,e)=>{if(finished||!Dirty())return;var answer=DossierDialog.Show(this,"Apply your notes to the entry before closing? You will still need to Save JSON in the workbench.","Unapplied notes",MessageBoxButtons.YesNoCancel);if(answer==DialogResult.Cancel)e.Cancel=true;else if(answer==DialogResult.Yes)CaptureNotes();};
  KeyPreview=true;KeyDown+=(_,e)=>{if(e.Control&&e.KeyCode==Keys.S){ApplyNotes();e.SuppressKeyPress=true;}else if(e.Control&&e.KeyCode==Keys.F){search.Focus();search.SelectAll();e.SuppressKeyPress=true;}};
  DossierStyle.Apply(this);DossierDpi.Complete(this);RefreshList(null);
 }
 bool Dirty()=>original==null?Entries.Count>0:!JToken.DeepEquals(original,data);
 void RefreshList(JObject keep)
 {
  reading=true;list.BeginUpdate();list.Items.Clear();foreach(JObject note in Entries)if((note["title"]+"\n"+note["text"]).IndexOf(search.Text,StringComparison.OrdinalIgnoreCase)>=0)list.Items.Add(new NoteItem(note));int index=-1;for(int i=0;i<list.Items.Count;i++)if(ReferenceEquals(((NoteItem)list.Items[i]).Value,keep))index=i;list.SelectedIndex=index>=0?index:list.Items.Count>0?0:-1;list.EndUpdate();reading=false;SelectNote();count.Text=list.Items.Count+" shown / "+Entries.Count+" notes";
 }
 void SelectNote(){if(reading)return;selected=(list.SelectedItem as NoteItem)?.Value;reading=true;title.Enabled=body.Enabled=selected!=null;title.Text=selected?["title"]?.ToString()??"";body.Text=selected?["text"]?.ToString()??"";reading=false;}
 void EditSelected(){if(reading||selected==null)return;selected["title"]=title.Text;selected["text"]=body.Text;if(list.SelectedIndex>=0){reading=true;list.Items[list.SelectedIndex]=new NoteItem(selected);reading=false;}}
 void AddNote(){var note=new JObject(new JProperty("title","New note"),new JProperty("text",""));Entries.Add(note);search.Clear();RefreshList(note);title.Focus();title.SelectAll();}
 void DuplicateNote(){if(selected==null)return;var note=(JObject)selected.DeepClone();note["title"]=note["title"]+" (copy)";Entries.Add(note);search.Clear();RefreshList(note);}
 void RemoveNote(){if(selected==null)return;if(DossierDialog.Show(this,"Remove this note? Cancel the Notes window to discard this removal.","Remove note",MessageBoxButtons.YesNo)!=DialogResult.Yes)return;selected.Remove();RefreshList(null);}
 void CaptureNotes(){Result=original==null&&Entries.Count==0?null:data.DeepClone();finished=true;DialogResult=DialogResult.OK;}
 void ApplyNotes(){CaptureNotes();Close();}
 sealed class NoteItem{public readonly JObject Value;public NoteItem(JObject value){Value=value;}public override string ToString()=>string.IsNullOrWhiteSpace(Value["title"]?.ToString())?"Untitled note":Value["title"].ToString();}
}
