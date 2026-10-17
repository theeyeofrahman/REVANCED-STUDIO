using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using RevancedStudio;
using M=MadCardMaker;
using D=MPNDossier;
class Integration
{
 [DllImport("user32.dll")]static extern IntPtr GetThreadDpiAwarenessContext();
 [DllImport("user32.dll")] [return:MarshalAs(UnmanagedType.Bool)] static extern bool AreDpiAwarenessContextsEqual(IntPtr first,IntPtr second);
 static int checks;static readonly BindingFlags Hidden=BindingFlags.NonPublic|BindingFlags.Instance;
 static void Check(bool ok,string why){if(!ok)throw new Exception(why);checks++;}
 [STAThread]static void Main(string[] args){try{if(args[0]=="--music-only"){MusicChecks.Run(Path.GetFullPath(args[1]));return;}bool focused=args.Contains("--workspace-only");Run(Path.GetFullPath(args[0]),focused);Console.WriteLine("PASS: "+checks+(focused?" focused workspace checks; native Dossier character/chatter data and complete MCM database loaded. ":" Studio integration checks; both real databases load in one process. ")+"Document state survives switching and formats stay isolated. No windows were shown.");}catch(Exception ex){Console.Error.WriteLine(ex);Environment.ExitCode=1;}}
 static void Run(string root,bool focused){Application.EnableVisualStyles();Application.SetCompatibleTextRenderingDefault(false);M.DossierDialog.PreventDialogsForTests=true;D.DossierDialog.PreventDialogsForTests=true;M.DossierFeedback.MutedForTests=true;M.DossierMotion.SuppressForTests=true;M.DossierPreferences.SuppressPersistenceForTests=true;D.DossierFeedback.MutedForTests=true;D.DossierMotion.SuppressForTests=true;D.DossierPreferences.SuppressPersistenceForTests=true;
  Console.WriteLine("Creating Studio shell"); using(var shell=new StudioWindow(root,true)){
   var hiddenHandle=shell.Handle;
   Check(AreDpiAwarenessContextsEqual(GetThreadDpiAwarenessContext(),new IntPtr(-4)),"Studio did not enable Per Monitor V2 high-DPI rendering");
   Check(new StudioState().Workspace=="MadCard"&&shell.Text.StartsWith("MCM REVANCED"),"MCM is not the primary editor");
   var workspaceButtons=(System.Collections.Generic.Dictionary<string,Button>)typeof(StudioWindow).GetField("buttons",Hidden).GetValue(shell);Check(workspaceButtons["Dossier"].Text.Contains("Legacy")&&workspaceButtons["MadCard"].Width>workspaceButtons["Dossier"].Width,"Dossier is not labelled as the secondary legacy workspace");
   Check(M.StudioModule.BaseDirectory!=D.StudioModule.BaseDirectory,"Module databases are sharing a root");Check(M.DossierTheme.ThemePath==D.DossierTheme.ThemePath,"Theme is not shared");Check(M.StudioModule.Hosted&&D.StudioModule.Hosted,"Module bridge not configured");
   if(!SystemInformation.HighContrast)Check(M.DossierScrollbars.IsAttached(shell),"Studio shell scrollbar styling missing");
   Console.WriteLine("Loading Dossier database");using(var loader=new D.DialogLoader()){typeof(D.DialogLoader).GetField("FileAmount",Hidden).SetValue(loader,loader.GetDatabaseSize());if(focused){foreach(string folder in new[]{"base","nem"}){loader.LoadDirectory("/db/"+folder+"/characters/",ref D.ModEnums.DB_CHAR);loader.LoadDirectory("/db/"+folder+"/chatter/",ref D.ModEnums.DB_CHATTER);}}else{loader.loadDatabase("base");loader.loadDatabase("nem");}D.ModEnums.InitializeData();}
   Console.WriteLine("Loading MadCard database");using(var loader=new M.DialogLoader()){typeof(M.DialogLoader).GetField("FileAmount",Hidden).SetValue(loader,loader.GetDatabaseSize(Path.Combine(M.StudioModule.BaseDirectory,"db")));loader.loadDatabase("base",M.ModEnums.SC_DIR);M.ModEnums.InitializeData();}
   Check(D.ModEnums.DB_CHAR.Count>=193,"Dossier characters are missing");Check(M.ModEnums.SC_DIR["Character"].Count>=193,"MadCard characters are missing");Check(D.ModEnums.DB_CHAR["AAHW0_Grunt"]["FullName"]!=null,"Dossier schema changed");Check(M.ModEnums.SC_DIR["Character"]["AAHW0_Grunt"]["Name"]!=null,"MadCard schema changed");Check(M.ModEnums.SC_DIR["Character"]["AAHW0_Grunt"]["FullName"]==null,"Legacy fields leaked into MadCard");
   if(focused){GC.Collect();GC.WaitForPendingFinalizers();GC.Collect();}Console.WriteLine("Switching workspaces");shell.SwitchWorkspace("Dossier");Check(shell.ActiveWorkspace=="Dossier","Dossier workspace did not open");var first=shell.Workspaces["Dossier"].Home;Check(!first.TopLevel&&first.Dock==DockStyle.Fill,"Dossier isn't embedded");
   var note=new Form{Text="Unsaved Dossier draft",Tag="keep-this"};shell.OpenDocument("Dossier",note);Check(shell.Workspaces["Dossier"].Documents.Count==2,"Dossier document tab missing");
   shell.SwitchWorkspace("MadCard");Check(shell.ActiveWorkspace=="MadCard","MadCard workspace did not open");Check(!shell.Workspaces["MadCard"].Home.TopLevel,"MadCard isn't embedded");var other=shell.Workspaces["MadCard"].Home;if(!SystemInformation.HighContrast)Check(M.DossierScrollbars.IsAttached(other)&&D.DossierScrollbars.IsAttached(first),"Workspace scrollbar styling missing");
   shell.SwitchWorkspace("Dossier");Check(ReferenceEquals(first,shell.Workspaces["Dossier"].Home),"Dossier reloaded while switching");Check(!note.IsDisposed&&(string)note.Tag=="keep-this","Dossier draft was lost");
   shell.OpenDocument("Dossier",note);Check(shell.Workspaces["Dossier"].Documents.Count==2,"Existing document duplicated");shell.CloseActiveDocument();Check(shell.Workspaces["Dossier"].Documents.Count==1,"Close tab didn't remove document");
   shell.SwitchWorkspace("MadCard");Check(ReferenceEquals(other,shell.Workspaces["MadCard"].Home),"MadCard reloaded while switching");shell.CloseActiveDocument();Check(!other.IsDisposed,"Workspace home was closed");
   M.DossierPreferences.Current.Volume=.07;M.DossierPreferences.Current.Sounds=false;shell.SwitchWorkspace("Dossier");Check(D.DossierPreferences.Current.Volume==.07&&!D.DossierPreferences.Current.Sounds,"Shared feedback settings did not sync");
  }
 }
}
