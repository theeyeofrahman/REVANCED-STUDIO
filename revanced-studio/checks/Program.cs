using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using RevancedStudio;
using M=MadCardMaker;
class Integration
{
 [DllImport("user32.dll")]static extern IntPtr GetThreadDpiAwarenessContext();
 [DllImport("user32.dll")] [return:MarshalAs(UnmanagedType.Bool)] static extern bool AreDpiAwarenessContextsEqual(IntPtr first,IntPtr second);
 static int checks;static readonly BindingFlags Hidden=BindingFlags.NonPublic|BindingFlags.Instance;
 static void Check(bool ok,string why){if(!ok)throw new Exception(why);checks++;}
 [STAThread]static void Main(string[] args){try{if(args.Length>0&&args[0]=="--music-only"){MusicChecks.Run(Path.GetFullPath(args[1]));return;}Run(Path.GetFullPath(args[0]));Console.WriteLine("PASS: "+checks+" standalone MadCard Studio checks. One MCM workspace loads, editor tabs persist, and no Dossier assembly is referenced. No windows were shown.");}catch(Exception ex){Console.Error.WriteLine(ex);Environment.ExitCode=1;}}
 static void Run(string root){Application.EnableVisualStyles();Application.SetCompatibleTextRenderingDefault(false);M.DossierDialog.PreventDialogsForTests=true;M.DossierFeedback.MutedForTests=true;M.DossierMotion.SuppressForTests=true;M.DossierPreferences.SuppressPersistenceForTests=true;
  Console.WriteLine("Loading MadCard database");M.StudioModule.Configure(Path.Combine(root,"Data","MadCard"),Path.Combine(Path.GetTempPath(),"revanced-studio-check-theme.json"),_=>{});using(var loader=new M.DialogLoader()){typeof(M.DialogLoader).GetField("FileAmount",Hidden).SetValue(loader,loader.GetDatabaseSize(Path.Combine(M.StudioModule.BaseDirectory,"db")));loader.loadDatabase("base",M.ModEnums.SC_DIR);M.ModEnums.InitializeData();}
  Check(M.ModEnums.SC_DIR["Character"].Count>=193,"MadCard characters are missing");Check(M.ModEnums.SC_DIR["Character"]["AAHW0_Grunt"]["Name"]!=null,"MadCard schema changed");
  Check(typeof(StudioWindow).Assembly.GetReferencedAssemblies().All(a=>!a.Name.Equals("MPNDossier",StringComparison.OrdinalIgnoreCase)),"Studio still references the Dossier assembly");
  Console.WriteLine("Creating Studio shell");using(var shell=new StudioWindow(root,true)){
   var hiddenHandle=shell.Handle;
   Check(AreDpiAwarenessContextsEqual(GetThreadDpiAwarenessContext(),new IntPtr(-4)),"Studio did not enable Per Monitor V2 high-DPI rendering");
   Check(shell.Text.Contains("MadCard Maker")&&shell.Workspaces.Count==0,"Studio did not open as a standalone MCM application");
   shell.SwitchWorkspace("MadCard");Check(shell.ActiveWorkspace=="MadCard"&&shell.Workspaces.Count==1,"MCM workspace did not open on its own");
   var home=shell.Workspaces["MadCard"].Home;Check(!home.TopLevel&&home.Dock==DockStyle.Fill,"MCM home isn't embedded");
   if(!SystemInformation.HighContrast)Check(M.DossierScrollbars.IsAttached(shell)&&M.DossierScrollbars.IsAttached(home),"Studio scrollbar styling missing");
   var note=new Form{Text="Unsaved MCM draft",Tag="keep-this"};shell.OpenDocument("MadCard",note);Check(shell.Workspaces["MadCard"].Documents.Count==2,"MCM editor tab missing");
   shell.SwitchWorkspace("MadCard");Check(ReferenceEquals(home,shell.Workspaces["MadCard"].Home),"MCM workspace reloaded while opening a document");Check(!note.IsDisposed&&(string)note.Tag=="keep-this","MCM draft was lost");
   shell.OpenDocument("MadCard",note);Check(shell.Workspaces["MadCard"].Documents.Count==2,"Existing MCM document duplicated");shell.CloseActiveDocument();Check(shell.Workspaces["MadCard"].Documents.Count==1&&note.IsDisposed,"Close tab didn't remove the editor");shell.CloseActiveDocument();Check(!home.IsDisposed,"Workspace home was closed");
  }
 }
}
