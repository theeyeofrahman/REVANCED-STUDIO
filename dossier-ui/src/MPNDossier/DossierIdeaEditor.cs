using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
namespace MPNDossier;
public sealed class DossierIdeaEditor:Form
{
 public DossierIdea Result{get;private set;}
 readonly TextBox title=new TextBox{Dock=DockStyle.Fill,MaxLength=160};
 readonly TextBox notes=new TextBox{Dock=DockStyle.Fill,Multiline=true,AcceptsReturn=true,ScrollBars=ScrollBars.Vertical,MaxLength=20000};
 readonly TextBox tags=new TextBox{Dock=DockStyle.Fill,MaxLength=250};
 readonly DateTimePicker dueDate=new DateTimePicker{Dock=DockStyle.Left,Width=220,Format=DateTimePickerFormat.Short,ShowCheckBox=true};
 readonly ComboBox column=new ComboBox{Dock=DockStyle.Fill,DropDownStyle=ComboBoxStyle.DropDownList},priority=new ComboBox{Dock=DockStyle.Fill,DropDownStyle=ComboBoxStyle.DropDownList};
 readonly CheckedListBox tasks=new CheckedListBox{Dock=DockStyle.Fill,CheckOnClick=true,IntegralHeight=false};
 readonly TextBox taskText=new TextBox{Dock=DockStyle.Fill,MaxLength=300};
 readonly Label feedback=new Label{Dock=DockStyle.Fill,AutoEllipsis=true};
 readonly DossierIdea original;
 public DossierIdeaEditor(DossierIdea idea)
 {
  DossierDpi.Initialize(this);
  original=idea.Copy();Text=string.IsNullOrEmpty(idea.Title)?"New idea":"Edit idea";Size=new Size(800,820);MinimumSize=new Size(720,720);StartPosition=FormStartPosition.CenterParent;MinimizeBox=false;
  var root=new TableLayoutPanel{Dock=DockStyle.Fill,Padding=new Padding(22),ColumnCount=1,RowCount=12};root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,100));
  foreach(var h in new[]{24,36,62,24,36,24,36,26})root.RowStyles.Add(new RowStyle(SizeType.Absolute,h));root.RowStyles.Add(new RowStyle(SizeType.Percent,35));root.RowStyles.Add(new RowStyle(SizeType.Absolute,30));root.RowStyles.Add(new RowStyle(SizeType.Percent,65));root.RowStyles.Add(new RowStyle(SizeType.Absolute,64));
  root.Controls.Add(new Label{Text="Idea title",Dock=DockStyle.Fill},0,0);root.Controls.Add(title,0,1);
  var meta=new TableLayoutPanel{Dock=DockStyle.Fill,ColumnCount=2,RowCount=2};meta.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,60));meta.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,40));meta.RowStyles.Add(new RowStyle(SizeType.Absolute,24));meta.RowStyles.Add(new RowStyle(SizeType.Percent,100));meta.Controls.Add(new Label{Text="Column",Dock=DockStyle.Fill},0,0);meta.Controls.Add(new Label{Text="Priority",Dock=DockStyle.Fill},1,0);meta.Controls.Add(column,0,1);meta.Controls.Add(priority,1,1);root.Controls.Add(meta,0,2);
  root.Controls.Add(new Label{Text="Tags (separate with commas)",Dock=DockStyle.Fill},0,3);root.Controls.Add(tags,0,4);root.Controls.Add(new Label{Text="Due date · optional",Dock=DockStyle.Fill},0,5);root.Controls.Add(dueDate,0,6);root.Controls.Add(new Label{Text="Notes",Dock=DockStyle.Fill},0,7);root.Controls.Add(notes,0,8);root.Controls.Add(new Label{Text="To-do list — check items as you finish them",Dock=DockStyle.Fill},0,9);
  var todo=new TableLayoutPanel{Dock=DockStyle.Fill,ColumnCount=1,RowCount=2};todo.RowStyles.Add(new RowStyle(SizeType.Percent,100));todo.RowStyles.Add(new RowStyle(SizeType.Absolute,36));todo.Controls.Add(tasks,0,0);
  var taskBar=new TableLayoutPanel{Dock=DockStyle.Fill,ColumnCount=4};taskBar.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,100));foreach(var _ in new[]{1,2,3})taskBar.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute,86));taskBar.Controls.Add(taskText,0,0);
  var add=new DossierButton{Text="Add",Dock=DockStyle.Fill};var rename=new DossierButton{Text="Rename",Dock=DockStyle.Fill};var remove=new DossierButton{Text="Remove",Dock=DockStyle.Fill};taskBar.Controls.Add(add,1,0);taskBar.Controls.Add(rename,2,0);taskBar.Controls.Add(remove,3,0);todo.Controls.Add(taskBar,0,1);root.Controls.Add(todo,0,10);
  var bottom=new TableLayoutPanel{Dock=DockStyle.Fill,ColumnCount=3,RowCount=2,Padding=new Padding(0,8,0,0)};bottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,100));bottom.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute,110));bottom.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute,110));bottom.RowStyles.Add(new RowStyle(SizeType.Absolute,20));bottom.RowStyles.Add(new RowStyle(SizeType.Percent,100));bottom.Controls.Add(feedback,0,0);bottom.SetColumnSpan(feedback,3);
  var archive=new DossierButton{Text=idea.Archived?"Restore card":"Archive card",Dock=DockStyle.Left,Width=140,Visible=!string.IsNullOrWhiteSpace(idea.Title)};var cancel=new DossierButton{Text="Cancel",DialogResult=DialogResult.Cancel,Dock=DockStyle.Fill};var save=new DossierButton{Text="Save idea",Dock=DockStyle.Fill};bottom.Controls.Add(archive,0,1);bottom.Controls.Add(cancel,1,1);bottom.Controls.Add(save,2,1);root.Controls.Add(bottom,0,11);Controls.Add(root);DossierStyle.Apply(this);tasks.BackColor=DossierStyle.Input;tasks.ForeColor=DossierStyle.Text;tasks.BorderStyle=BorderStyle.None;tasks.Font=DossierFonts.Create(DossierStyle.Theme.FontFamily,10);title.Font=DossierFonts.Create(DossierStyle.Theme.FontFamily,13,FontStyle.Bold);
  title.Text=idea.Title;notes.Text=idea.Notes;tags.Text=idea.Tags;dueDate.Checked=idea.DueDate.HasValue;if(idea.DueDate.HasValue)dueDate.Value=idea.DueDate.Value;column.Items.AddRange(DossierIdeas.Columns);column.SelectedItem=idea.Column;priority.Items.AddRange(DossierIdeas.Priorities);priority.SelectedItem=idea.Priority;foreach(var task in idea.Tasks)tasks.Items.Add(task.Text,task.Done);
  void AddTask(){if(string.IsNullOrWhiteSpace(taskText.Text))return;tasks.Items.Add(taskText.Text.Trim(),false);taskText.Clear();taskText.Focus();}
  add.Click+=(_,__)=>AddTask();taskText.KeyDown+=(_,e)=>{if(e.KeyCode==Keys.Enter){AddTask();e.SuppressKeyPress=true;}};
  tasks.SelectedIndexChanged+=(_,__)=>{if(tasks.SelectedItem!=null)taskText.Text=tasks.SelectedItem.ToString();};
  rename.Click+=(_,__)=>{if(tasks.SelectedIndex>=0&&!string.IsNullOrWhiteSpace(taskText.Text))tasks.Items[tasks.SelectedIndex]=taskText.Text.Trim();};remove.Click+=(_,__)=>{if(tasks.SelectedIndex>=0){tasks.Items.RemoveAt(tasks.SelectedIndex);taskText.Clear();}};
  save.Click+=(_,__)=>SaveIdea(original.Archived);archive.Click+=(_,__)=>SaveIdea(!original.Archived);CancelButton=cancel;
  DossierDpi.Complete(this);
 }
 private void SaveIdea(bool archived)
 {
  if(string.IsNullOrWhiteSpace(title.Text)){feedback.Text="Give your idea a title first.";title.Focus();return;}
  var result=original.Copy();result.Title=title.Text.Trim();result.Notes=notes.Text;result.Tags=tags.Text;result.DueDate=dueDate.Checked?dueDate.Value.Date:(DateTime?)null;result.Column=column.SelectedItem.ToString();result.Priority=priority.SelectedItem.ToString();result.Archived=archived;result.Tasks=Enumerable.Range(0,tasks.Items.Count).Select(i=>new DossierIdeaTask{Text=tasks.Items[i].ToString(),Done=tasks.GetItemChecked(i)}).ToList();Result=result;DialogResult=DialogResult.OK;Close();
 }
 public void ShowSaveError(string message){feedback.Text=message;feedback.ForeColor=Color.Salmon;}
}
