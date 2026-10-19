using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json;
using M=MadCardMaker;
namespace RevancedStudio;

public sealed class StudioState
{
 public bool Maximized{get;set;}=true;
 public int Width{get;set;}=1400;
 public int Height{get;set;}=900;
 public static string DirectoryPath=>Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),"REVANCEDStudio");
 public static string FilePath=>Path.Combine(DirectoryPath,"studio.json");
 public static StudioState Load(){try{return File.Exists(FilePath)?JsonConvert.DeserializeObject<StudioState>(File.ReadAllText(FilePath))??new StudioState():new StudioState();}catch{return new StudioState();}}
 public void Save()=>M.MakerHelp.Write(FilePath,JsonConvert.SerializeObject(this,Formatting.Indented));
}

public sealed class StudioWindow:Form
{
 public sealed class Workspace
 {
  public string Name;public TabControl Tabs;public Form Home;
  public readonly Dictionary<Form,TabPage> Documents=new Dictionary<Form,TabPage>();
 }
 readonly Dictionary<string,Workspace> spaces=new Dictionary<string,Workspace>();
 readonly Dictionary<string,Button> buttons=new Dictionary<string,Button>();
 readonly Panel canvas=new Panel{Dock=DockStyle.Fill};readonly Label status=new Label{Dock=DockStyle.Fill,AutoEllipsis=true};readonly StudioState state;
 readonly string root;bool switching,closing;bool testing;MusicPlayer music;
 public string ActiveWorkspace=>spaces.ContainsKey("MadCard")?"MadCard":null;
 public IReadOnlyDictionary<string,Workspace> Workspaces=>spaces;
 public StudioWindow():this(AppDomain.CurrentDomain.BaseDirectory,false){}
 public StudioWindow(string directory,bool testMode)
 {
  root=Path.GetFullPath(directory);testing=testMode;state=testing?new StudioState():StudioState.Load();
  string theme=Path.Combine(StudioState.DirectoryPath,"theme.json");
  M.StudioModule.Configure(Path.Combine(root,"Data","MadCard"),theme,OpenMadCardDocument);
  Text="MadCard Maker REVANCED";MinimumSize=new Size(1120,740);Size=new Size(1400,900);StartPosition=FormStartPosition.CenterScreen;AutoScaleMode=AutoScaleMode.Dpi;AutoScaleDimensions=new SizeF(96,96);
  var layout=new TableLayoutPanel{Dock=DockStyle.Fill,ColumnCount=1,RowCount=3,Padding=new Padding(16,10,16,12)};layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,100));layout.RowStyles.Add(new RowStyle(SizeType.Absolute,64));layout.RowStyles.Add(new RowStyle(SizeType.Percent,100));layout.RowStyles.Add(new RowStyle(SizeType.Absolute,28));
  var header=new TableLayoutPanel{Dock=DockStyle.Fill,ColumnCount=3,RowCount=1};header.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute,250));header.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,100));header.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute,360));
  var title=new Label{Text="MadCard Maker REVANCED",Dock=DockStyle.Fill,TextAlign=ContentAlignment.MiddleLeft};header.Controls.Add(title,0,0);
  var tools=new FlowLayoutPanel{Dock=DockStyle.Fill,FlowDirection=FlowDirection.RightToLeft,WrapContents=false,Padding=new Padding(0,8,0,0)};
  var appearance=new M.DossierButton{Text="Appearance",Width=120,Height=40};appearance.Click+=(_,__)=>{try{M.MakerHelp.Interface(this);}catch(Exception e){M.DossierDialog.Show(this,e.Message);}};
  var close=new M.DossierButton{Text="Close tab",Width=110,Height=40};close.Click+=(_,__)=>CloseActiveDocument();var musicButton=new M.DossierButton{Text="Music",Width=100,Height=40};musicButton.Click+=(_,__)=>{if(music==null||music.IsDisposed)music=new MusicPlayer();if(!music.Visible)music.Show(this);music.BringToFront();};tools.Controls.Add(appearance);tools.Controls.Add(musicButton);tools.Controls.Add(close);header.Controls.Add(tools,2,0);layout.Controls.Add(header,0,0);layout.Controls.Add(canvas,0,1);layout.Controls.Add(status,0,2);Controls.Add(layout);
  M.StudioModule.Style(this);title.Font=M.DossierFonts.Create(M.DossierTheme.Load().HeadingFont,18,FontStyle.Bold);title.ForeColor=Color.FromArgb(217,84,96);status.ForeColor=Color.FromArgb(183,171,173);
  Shown+=(_,__)=>{if(testing)return;if(state.Maximized)WindowState=FormWindowState.Maximized;else{var area=Screen.FromControl(this).WorkingArea;Size=new Size(Math.Min(area.Width,Math.Max(1120,state.Width)),Math.Min(area.Height,Math.Max(740,state.Height)));}SwitchWorkspace("MadCard");};
  FormClosing+=CloseStudio;
 }
 protected override bool ProcessCmdKey(ref Message msg,Keys keyData){if(keyData==(Keys.Control|Keys.W)){CloseActiveDocument();return true;}return base.ProcessCmdKey(ref msg,keyData);}
 public void SwitchWorkspace(string name)
 {
  if(switching||name!="MadCard")return;switching=true;
  try{
   if(!spaces.TryGetValue(name,out var space)){
    string folder=Path.Combine(root,"Data","MadCard","db","base");
    if(!Directory.Exists(folder))throw new DirectoryNotFoundException("The MadCard reference database is missing. Restore the Data folder beside REVANCED Studio.");
    space=new Workspace{Name=name,Tabs=new M.DossierTabs{Dock=DockStyle.Fill}};spaces[name]=space;canvas.Controls.Add(space.Tabs);
    space.Home=M.StudioModule.CreateWorkspace();AddForm(space,space.Home,"Mods & decks");M.StudioModule.Style(space.Tabs);
   }
   space.Tabs.Visible=true;space.Tabs.BringToFront();
   status.Text="MADCard / typed decks, inherited roots and load order   ·   Ctrl+W Close tab";
  }catch(Exception ex){status.Text="Could not open MadCard Maker: "+ex.Message;if(!testing)M.DossierDialog.Show(this,ex.Message,"Workspace unavailable");}
  finally{switching=false;}
 }
 public void OpenDocument(string workspace,Form form){if(workspace!="MadCard")return;SwitchWorkspace("MadCard");if(!spaces.TryGetValue("MadCard",out var space))return;if(space.Documents.TryGetValue(form,out var existing)){space.Tabs.SelectedTab=existing;return;}AddForm(space,form,form.Text);space.Tabs.SelectedTab=space.Documents[form];}
 void OpenMadCardDocument(Form form)=>OpenDocument("MadCard",form);
 void AddForm(Workspace space,Form form,string caption)
 {
  var tab=new TabPage(caption);space.Tabs.TabPages.Add(tab);space.Documents[form]=tab;
  form.TopLevel=false;form.FormBorderStyle=FormBorderStyle.None;form.MinimumSize=Size.Empty;form.MaximumSize=Size.Empty;form.Dock=DockStyle.Fill;form.WindowState=FormWindowState.Normal;tab.Controls.Add(form);
  form.TextChanged+=(_,__)=>{if(!ReferenceEquals(form,space.Home))tab.Text=form.Text;};
  form.FormClosed+=(_,__)=>{space.Documents.Remove(form);space.Tabs.TabPages.Remove(tab);tab.Dispose();};
  if(!testing)form.Show();
 }
 public void CloseActiveDocument(){if(!spaces.TryGetValue("MadCard",out var space)||space.Tabs.SelectedTab==null)return;var doc=space.Documents.FirstOrDefault(p=>p.Value==space.Tabs.SelectedTab).Key;if(doc==null||ReferenceEquals(doc,space.Home)){status.Text="The workspace home stays open. Select an editor tab to close it.";return;}doc.Close();if(doc.IsDisposed&&space.Documents.TryGetValue(doc,out var tab)){space.Documents.Remove(doc);space.Tabs.TabPages.Remove(tab);tab.Dispose();}}
 void CloseStudio(object sender,FormClosingEventArgs e)
 {
  if(closing)return;
  if(testing){foreach(var w in spaces.Values)foreach(var f in w.Documents.Keys.ToArray())f.Dispose();return;}
  foreach(var space in spaces.Values)foreach(var doc in space.Documents.Keys.Where(f=>!ReferenceEquals(f,space.Home)).ToArray()){doc.Close();if(!doc.IsDisposed){e.Cancel=true;return;}}
  foreach(var home in spaces.Values.Select(w=>w.Home).Where(f=>f!=null&&!f.IsDisposed).ToArray()){home.Close();if(!home.IsDisposed){e.Cancel=true;return;}}
  closing=true;music?.Dispose();try{var b=WindowState==FormWindowState.Normal?Bounds:RestoreBounds;state.Width=b.Width;state.Height=b.Height;state.Maximized=WindowState==FormWindowState.Maximized;state.Save();}catch(Exception ex){status.Text="Window preferences could not be saved: "+ex.Message;}
 }
 protected override void Dispose(bool disposing){if(disposing)music?.Dispose();base.Dispose(disposing);}
}
