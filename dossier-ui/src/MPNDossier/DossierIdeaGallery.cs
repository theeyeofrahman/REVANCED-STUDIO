using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
namespace MPNDossier;
public sealed class DossierIdeaGallery:Form
{
 const string DragFormat="MPNDossier.IdeaId";
 DossierIdeas board;
 readonly string storePath;
 bool canSave=true;
 readonly TextBox search=new TextBox{Dock=DockStyle.Fill,AccessibleName="Search idea titles, notes, tags and tasks"};
 readonly CheckBox archived=new CheckBox{Text="View archive",AutoSize=true};
 readonly ComboBox priorityFilter=new ComboBox{Dock=DockStyle.Fill,DropDownStyle=ComboBoxStyle.DropDownList};
 readonly ComboBox sortOrder=new ComboBox{Dock=DockStyle.Fill,DropDownStyle=ComboBoxStyle.DropDownList};
 readonly Label summary=new Label{Dock=DockStyle.Fill,AutoEllipsis=true};
 readonly Label status=new Label{Dock=DockStyle.Bottom,Height=32,AutoEllipsis=true,Padding=new Padding(18,4,0,0)};
 readonly Dictionary<string,FlowLayoutPanel> columns=new Dictionary<string,FlowLayoutPanel>();
 readonly Dictionary<string,Label> headings=new Dictionary<string,Label>();
 public static void Open(IWin32Window owner,string path=null,string projectName=null)
 {
  var target=Path.GetFullPath(path??DossierIdeas.StorePath);var form=Application.OpenForms.OfType<DossierIdeaGallery>().FirstOrDefault(f=>string.Equals(Path.GetFullPath(f.storePath),target,StringComparison.OrdinalIgnoreCase));if(form==null){form=new DossierIdeaGallery(null,target);if(projectName!=null)form.Text="Idea Gallery / "+projectName;StudioModule.ShowDocument(form,owner);}else{form.WindowState=FormWindowState.Normal;StudioModule.ActivateDocument(form);}
 }
 public DossierIdeaGallery(DossierIdeas data=null,string path=null)
 {
  DossierDpi.Initialize(this);
  storePath=path??DossierIdeas.StorePath;
  try{board=data??DossierIdeas.Load(storePath);}catch(Exception ex){board=new DossierIdeas();canSave=false;status.Text="Cannot read your saved board: "+ex.Message+". Fix the file, then reopen the gallery.";}
  Text="MPN Dossier REVANCED — Idea Gallery";Size=new Size(1340,860);MinimumSize=new Size(1120,700);StartPosition=FormStartPosition.CenterParent;
  var root=new TableLayoutPanel{Dock=DockStyle.Fill,Padding=new Padding(18),ColumnCount=1,RowCount=4};root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,100));root.RowStyles.Add(new RowStyle(SizeType.Absolute,50));root.RowStyles.Add(new RowStyle(SizeType.Absolute,30));root.RowStyles.Add(new RowStyle(SizeType.Absolute,26));root.RowStyles.Add(new RowStyle(SizeType.Percent,100));
  var toolbar=new TableLayoutPanel{Dock=DockStyle.Fill,ColumnCount=8};toolbar.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute,160));toolbar.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,100));toolbar.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute,112));toolbar.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute,136));toolbar.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute,124));toolbar.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute,114));toolbar.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute,116));toolbar.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute,118));
  var title=DossierStyle.Heading("Idea Gallery",20);toolbar.Controls.Add(title,0,0);
  var searchArea=new TableLayoutPanel{Dock=DockStyle.Fill,ColumnCount=1,RowCount=2};searchArea.RowStyles.Add(new RowStyle(SizeType.Absolute,18));searchArea.RowStyles.Add(new RowStyle(SizeType.Percent,100));searchArea.Controls.Add(new Label{Text="Search ideas and tasks",Dock=DockStyle.Fill},0,0);searchArea.Controls.Add(search,0,1);toolbar.Controls.Add(searchArea,1,0);
  priorityFilter.Items.AddRange(new object[]{"All priorities","Low","Normal","High"});priorityFilter.SelectedIndex=0;toolbar.Controls.Add(priorityFilter,2,0);
  sortOrder.Items.AddRange(new object[]{"Recently updated","Due date","Priority","Title A–Z"});sortOrder.SelectedIndex=0;toolbar.Controls.Add(sortOrder,3,0);
  toolbar.Controls.Add(archived,4,0);
  var templates=new DossierButton{Text="Quick idea ▾",Dock=DockStyle.Fill,Enabled=canSave};var templateMenu=new ContextMenuStrip();foreach(var template in new[]{new {Name="Blank idea",Tags="",Notes=""},new {Name="Feature request",Tags="feature",Notes="What should it do?\r\nWho would benefit?\r\nAcceptance notes:"},new {Name="Bug report",Tags="bug",Notes="What happened?\r\nWhat did you expect?\r\nSteps to reproduce:"},new {Name="Content idea",Tags="content",Notes="Concept:\r\nCharacters / assets:\r\nKey moments:"},new {Name="Task",Tags="task",Notes="Next steps:"}}){var t=template;templateMenu.Items.Add(t.Name,null,(_,__)=>{var idea=new DossierIdea{Title=t.Name=="Blank idea"?"":"New "+t.Name.ToLowerInvariant(),Tags=t.Tags,Notes=t.Notes,Column="Ideas"};Edit(idea);});}templates.Click+=(_,__)=>templateMenu.Show(templates,0,templates.Height);toolbar.Controls.Add(templates,5,0);
  var add=new DossierButton{Text="New idea +",Dock=DockStyle.Fill,Enabled=canSave};add.Click+=(_,__)=>Edit(new DossierIdea());toolbar.Controls.Add(add,6,0);
  var export=new DossierButton{Text="Export board",Dock=DockStyle.Fill};export.Click+=(_,__)=>ExportBoard();toolbar.Controls.Add(export,7,0);root.Controls.Add(toolbar,0,0);
  root.Controls.Add(summary,0,1);
  root.Controls.Add(new Label{Text="Drag cards between columns. Click a card to edit it. Right-click for quick actions; due dates, priorities and task progress stay visible on the board.",Dock=DockStyle.Fill},0,2);
  var lanes=new TableLayoutPanel{Dock=DockStyle.Fill,ColumnCount=4,RowCount=1};for(int i=0;i<4;i++)lanes.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,25));
  for(int i=0;i<DossierIdeas.Columns.Length;i++){
   var column=DossierIdeas.Columns[i];var lane=new Panel{Dock=DockStyle.Fill,Margin=new Padding(0,0,i==3?0:12,0),Padding=new Padding(6),AllowDrop=true};
   var caption=new Label{Text=column,Dock=DockStyle.Top,Height=34,AllowDrop=true};headings[column]=caption;
   var flow=new FlowLayoutPanel{Dock=DockStyle.Fill,FlowDirection=FlowDirection.TopDown,WrapContents=false,AutoScroll=true,AllowDrop=true};columns[column]=flow;lane.Controls.Add(flow);lane.Controls.Add(caption);lanes.Controls.Add(lane,i,0);
   flow.SizeChanged+=(_,__)=>SizeCards(flow);WireDrop(flow,column);WireDrop(lane,column);WireDrop(caption,column);
  }
  root.Controls.Add(lanes,0,3);Controls.Add(root);Controls.Add(status);DossierStyle.Apply(this);title.Font=DossierFonts.Create(DossierStyle.Theme.HeadingFont,20,FontStyle.Bold);foreach(var flow in columns.Values)flow.BackColor=DossierStyle.Background;
  search.TextChanged+=(_,__)=>RenderBoard();archived.CheckedChanged+=(_,__)=>RenderBoard();priorityFilter.SelectedIndexChanged+=(_,__)=>RenderBoard();sortOrder.SelectedIndexChanged+=(_,__)=>RenderBoard();KeyPreview=true;KeyDown+=(_,e)=>{if(e.Control&&e.KeyCode==Keys.N){Edit(new DossierIdea());e.SuppressKeyPress=true;}else if(e.Control&&e.KeyCode==Keys.F){search.Focus();search.SelectAll();e.SuppressKeyPress=true;}};
  RenderBoard();if(canSave)status.Text=board.RecoveryNotice??"Ideas are stored locally. Ctrl+N adds a card; Ctrl+F searches.";
  DossierDpi.Complete(this);
 }
 private void Edit(DossierIdea idea)
 {
  if(!canSave)return;string error=null;
  while(true)using(var dialog=new DossierIdeaEditor(idea)){
   if(error!=null)dialog.ShowSaveError(error);
   if(dialog.ShowDialog(this)!=DialogResult.OK)return;
   idea=dialog.Result;if(Commit(()=>board.Put(idea)))return;error=status.Text;
  }
 }
 private bool Commit(Action mutation)
 {
  if(!canSave)return false;var previous=board.ToJson();
  try{mutation();board.Save(storePath);RenderBoard();status.Text="Saved • "+board.Cards.Count(c=>!c.Archived)+" active ideas • "+board.Cards.Sum(c=>c.Tasks.Count(t=>t.Done))+" tasks completed";return true;}
  catch(Exception ex){board=DossierIdeas.Parse(previous);RenderBoard();status.Text="Change could not be saved: "+ex.Message;return false;}
 }
 private void RenderBoard()
 {
  IEnumerable<DossierIdea> filtered=board.Filter(search.Text,archived.Checked);if(priorityFilter.SelectedIndex>0)filtered=filtered.Where(c=>c.Priority==priorityFilter.Text);
  var visible=filtered.ToArray();string sort=sortOrder.SelectedItem as string;
  Func<DossierIdea,int> rank=c=>c.Priority=="High"?0:c.Priority=="Normal"?1:2;
  if(sort=="Due date")visible=visible.OrderBy(c=>c.DueDate.HasValue?0:1).ThenBy(c=>c.DueDate).ThenByDescending(c=>c.UpdatedUtc).ToArray();
  else if(sort=="Priority")visible=visible.OrderBy(rank).ThenByDescending(c=>c.UpdatedUtc).ToArray();
  else if(sort=="Title A–Z")visible=visible.OrderBy(c=>c.Title,StringComparer.CurrentCultureIgnoreCase).ToArray();
  else visible=visible.OrderByDescending(c=>c.UpdatedUtc).ToArray();
  var active=board.Cards.Where(c=>!c.Archived).ToArray();var overdue=active.Count(c=>c.DueDate.HasValue&&c.DueDate.Value.Date<DateTime.Today&&c.Column!="Done");var dueSoon=active.Count(c=>c.DueDate.HasValue&&c.DueDate.Value.Date>=DateTime.Today&&c.DueDate.Value.Date<=DateTime.Today.AddDays(7)&&c.Column!="Done");var openTasks=active.Sum(c=>c.Tasks.Count(t=>!t.Done));var doneTasks=active.Sum(c=>c.Tasks.Count(t=>t.Done));
  summary.Text=active.Length+" active ideas   ·   "+dueSoon+" due this week   ·   "+overdue+" overdue   ·   "+doneTasks+" / "+(doneTasks+openTasks)+" tasks complete";summary.ForeColor=overdue>0?DossierStyle.Accent:DossierStyle.Muted;
  foreach(var column in DossierIdeas.Columns){
   var flow=columns[column];flow.SuspendLayout();foreach(Control old in flow.Controls.Cast<Control>().ToArray()){flow.Controls.Remove(old);old.Dispose();}
   var cards=visible.Where(c=>c.Column==column).ToArray();headings[column].Text=column+"  ·  "+cards.Length;
   foreach(var idea in cards){var card=new IdeaCard(idea){Width=Math.Max(180,flow.ClientSize.Width-24),Margin=new Padding(2,3,2,9),AllowDrop=true};flow.Controls.Add(card);
    card.OpenRequested+=(_,__)=>Edit(idea);WireDrop(card,column);
    var menu=new ContextMenuStrip();foreach(var destination in DossierIdeas.Columns){var target=destination;menu.Items.Add("Move to "+target,null,(_,__)=>Commit(()=>board.Move(idea.Id,target)));}
    menu.Items.Add("Create new from template",null,(_,__)=>Edit(DossierIdeas.CreateFromTemplate(idea)));menu.Items.Add("Duplicate idea",null,(_,__)=>{var copy=idea.Copy();copy.Id=Guid.NewGuid().ToString("N");copy.Title+=" copy";copy.CreatedUtc=copy.UpdatedUtc=DateTime.UtcNow;Edit(copy);});menu.Items.Add(idea.Archived?"Restore card":"Archive card",null,(_,__)=>Commit(()=>{idea.Archived=!idea.Archived;idea.UpdatedUtc=DateTime.UtcNow;}));DossierStyle.Apply(menu);card.ContextMenuStrip=menu;card.Disposed+=(_,__)=>menu.Dispose();
   }
   if(cards.Length==0){var empty=new Label{Text=archived.Checked?"No archived cards here.":string.IsNullOrWhiteSpace(search.Text)?"Drop an idea here, or add a new one.":"No matching ideas.",ForeColor=DossierStyle.Muted,BackColor=DossierStyle.Background,Width=Math.Max(180,flow.ClientSize.Width-24),Height=70,Padding=new Padding(10),Margin=new Padding(2,3,2,9),AllowDrop=true};WireDrop(empty,column);flow.Controls.Add(empty);}
   SizeCards(flow);flow.ResumeLayout(true);
  }
 }
 private static void SizeCards(FlowLayoutPanel flow){foreach(Control card in flow.Controls)card.Width=Math.Max(180,flow.ClientSize.Width-SystemInformation.VerticalScrollBarWidth-10);}
 private void WireDrop(Control target,string column)
 {
  target.DragEnter+=(_,e)=>{var id=e.Data.GetDataPresent(DragFormat)?e.Data.GetData(DragFormat) as string:null;e.Effect=canSave&&id!=null&&board.Cards.Any(c=>c.Id==id)?DragDropEffects.Move:DragDropEffects.None;};
  target.DragDrop+=(_,e)=>{var id=e.Data.GetData(DragFormat) as string;if(id==null||!board.Cards.Any(c=>c.Id==id))return;
   var flow=columns[column];var local=flow.PointToClient(new Point(e.X,e.Y));string before=flow.Controls.OfType<IdeaCard>().FirstOrDefault(c=>local.Y<c.Top+c.Height/2)?.Idea.Id;
   Commit(()=>board.Move(id,column,before));
  };
 }
 private void ExportBoard()
 {
  using(var dialog=new DossierSaveFileDialog{Title="Export idea board",Filter="Idea board (*.json)|*.json",FileName="dossier-ideas.json",DefaultExt="json",AddExtension=true})if(dialog.ShowDialog(this)==DialogResult.OK)try{board.Save(dialog.FileName);status.Text="Exported board to "+dialog.FileName;}catch(Exception ex){status.Text="Could not export: "+ex.Message;}
 }
 private sealed class IdeaCard:Control,IDossierDpiMetrics
 {
  public readonly DossierIdea Idea;
  public event EventHandler OpenRequested;
  Point start;bool dragged;
  public IdeaCard(DossierIdea idea){Idea=idea;Height=205;Cursor=Cursors.Hand;TabStop=true;DoubleBuffered=true;BackColor=DossierStyle.Surface;ForeColor=DossierStyle.Text;AccessibleName=idea.Title+", "+idea.Column;}
  public void RefreshDpiMetrics(){Height=DossierDpi.Pixels(this,205);Margin=new Padding(DossierDpi.Pixels(this,2),DossierDpi.Pixels(this,3),DossierDpi.Pixels(this,2),DossierDpi.Pixels(this,9));Invalidate();}
  protected override void OnHandleCreated(EventArgs e){base.OnHandleCreated(e);RefreshDpiMetrics();}
  protected override void OnDpiChangedAfterParent(EventArgs e){base.OnDpiChangedAfterParent(e);RefreshDpiMetrics();}
  protected override void OnMouseDown(MouseEventArgs e){base.OnMouseDown(e);start=e.Location;dragged=false;Focus();}
  protected override void OnMouseMove(MouseEventArgs e){base.OnMouseMove(e);if(e.Button!=MouseButtons.Left||dragged)return;if(Math.Abs(e.X-start.X)<SystemInformation.DragSize.Width&&Math.Abs(e.Y-start.Y)<SystemInformation.DragSize.Height)return;dragged=true;DoDragDrop(new DataObject(DragFormat,Idea.Id),DragDropEffects.Move);}
  protected override void OnMouseClick(MouseEventArgs e){base.OnMouseClick(e);if(!dragged&&e.Button==MouseButtons.Left)OpenRequested?.Invoke(this,EventArgs.Empty);}
  protected override bool IsInputKey(Keys keyData)=>keyData==Keys.Enter||keyData==Keys.Space||base.IsInputKey(keyData);
  protected override void OnKeyDown(KeyEventArgs e){base.OnKeyDown(e);if(e.KeyCode==Keys.Enter||e.KeyCode==Keys.Space){OpenRequested?.Invoke(this,EventArgs.Empty);e.SuppressKeyPress=true;}}
  protected override void OnPaint(PaintEventArgs e)
  {
   int P(int value)=>DossierDpi.Pixels(this,value);
   e.Graphics.Clear(DossierStyle.Surface);using(var line=new Pen(Focused?DossierStyle.Accent:DossierStyle.Line))e.Graphics.DrawRectangle(line,0,0,Width-1,Height-1);
   using(var title=DossierFonts.Create(DossierStyle.Theme.FontFamily,12,FontStyle.Bold))using(var body=DossierFonts.Create(DossierStyle.Theme.FontFamily,10))using(var small=DossierFonts.Create(DossierStyle.Theme.FontFamily,9)){
    var flags=TextFormatFlags.WordBreak|TextFormatFlags.EndEllipsis|TextFormatFlags.NoPrefix;
    TextRenderer.DrawText(e.Graphics,Idea.Priority+" priority"+(string.IsNullOrWhiteSpace(Idea.Tags)?"":"  ·  "+Idea.Tags),small,new Rectangle(P(12),P(12),Width-P(24),P(22)),Idea.Priority=="High"?DossierStyle.Accent:DossierStyle.Muted,TextFormatFlags.EndEllipsis|TextFormatFlags.NoPrefix);
    TextRenderer.DrawText(e.Graphics,Idea.Title,title,new Rectangle(P(12),P(40),Width-P(24),P(50)),DossierStyle.Text,flags);
    TextRenderer.DrawText(e.Graphics,Idea.Notes,body,new Rectangle(P(12),P(95),Width-P(24),P(48)),DossierStyle.Muted,flags);
    var progress=Idea.Tasks.Count==0?"No to-do items yet":Idea.Tasks.Count(t=>t.Done)+" / "+Idea.Tasks.Count+" tasks done";
    TextRenderer.DrawText(e.Graphics,progress,small,new Rectangle(P(12),P(153),Width-P(24),P(22)),DossierStyle.Cyan,TextFormatFlags.EndEllipsis|TextFormatFlags.NoPrefix);
    string due=Idea.DueDate.HasValue?"Due "+Idea.DueDate.Value.ToString("MMM d, yyyy")+(Idea.DueDate.Value.Date<DateTime.Today&&Idea.Column!="Done"?" · OVERDUE":""):"No due date";
    Color dueColor=Idea.DueDate.HasValue&&Idea.DueDate.Value.Date<DateTime.Today&&Idea.Column!="Done"?DossierStyle.Accent:DossierStyle.Muted;
    TextRenderer.DrawText(e.Graphics,due,small,new Rectangle(P(12),P(178),Width-P(24),P(20)),dueColor,TextFormatFlags.EndEllipsis|TextFormatFlags.NoPrefix);
   }
  }
 }
}
