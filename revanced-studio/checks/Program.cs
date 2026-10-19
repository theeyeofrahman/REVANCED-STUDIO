using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
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
  var assisted=M.MakerAssistedCharacter.Create(M.MakerPreset.All.First(p=>p.Name=="Patrol scout"),"AAHW1_Agent","Check_Assisted","Check Assisted","Test draft",12,4,1m,.5m,null,"TestFaction","TestVoice","TestWeapon","TestOffhand","TestAnimation",true,"TestStyle","TestSidearm");
  Check((string)assisted["Faction"]=="TestFaction"&&(string)assisted["VoiceSet"]=="TestVoice","Assisted creator did not write identity options");
  Check((string)assisted["DefaultWeapon"]=="TestWeapon"&&(string)assisted["DefaultOffHand"]=="TestOffhand"&&(string)assisted["DefaultSidearm"]=="TestSidearm"&&(string)assisted["WeaponPref"]=="TestStyle"&&(string)assisted["AnimationSet"]=="TestAnimation","Assisted creator did not write combat and animation options");
  using(var wizard=new M.MakerAssistedCharacterDialog()){
   var searchable=(ComboBox)typeof(M.MakerAssistedCharacterDialog).GetField("faction",Hidden).GetValue(wizard);
   Check(searchable.DropDownStyle==ComboBoxStyle.DropDown&&searchable.AutoCompleteMode==AutoCompleteMode.SuggestAppend,"Assisted character fields are not searchable");
   typeof(M.MakerAssistedCharacterDialog).GetMethod("ApplySuggestions",Hidden).Invoke(wizard,null);
   var guidance=(Label)typeof(M.MakerAssistedCharacterDialog).GetField("guidance",Hidden).GetValue(wizard);
   Check(guidance.Text.Contains("Suggested values were copied"),"Appearance based field suggestions did not load");
  }
  int assistedTypes=0;foreach(var type in M.ModEnums.CARDTYPES.Where(t=>t!="Character")){using(var wizard=new M.MakerAssistedCardDialog(type)){Check(wizard.Text.Contains("Assisted"),"Missing assisted creator for "+type);assistedTypes++;}}
  Check(assistedTypes==M.ModEnums.CARDTYPES.Length-1,"Not every non-character card type has an assisted creator");
  using(var origin=new M.MakerAssistedOriginDialog()){
   var build=typeof(M.MakerAssistedOriginDialog).GetMethod("Build",Hidden);
   var draft=(JObject)build.Invoke(origin,null);
   Check(M.MakerStudio.ValidId((string)draft["Filename"])&&!string.IsNullOrWhiteSpace((string)draft["Root"]),"Origin assistant did not initialize a valid draft");
   var setAttributes=(CheckBox)typeof(M.MakerAssistedOriginDialog).GetField("setAttributes",Hidden).GetValue(origin);setAttributes.Checked=true;
   var values=(NumericUpDown[])typeof(M.MakerAssistedOriginDialog).GetField("attributes",Hidden).GetValue(origin);for(int i=0;i<values.Length;i++)values[i].Value=i+1;
   draft=(JObject)build.Invoke(origin,null);
   var bonuses=(JArray)draft["AttributeBonuses"];Check(bonuses.Count==4&&Enumerable.Range(0,4).All(i=>(int)bonuses[i]==i+1),"Origin attribute bonuses are not written in Strength/Dexterity/Endurance/Tactics order");
  }
  using(var choice=new M.MakerCreationModeDialog("Ranged"))Check(choice.Mode==M.MakerCreationMode.Cancel,"Creation mode dialog did not start without a selection");
  var styleType=typeof(M.StudioModule).Assembly.GetType("MadCardMaker.DossierStyle");var theme=styleType.GetField("Theme",BindingFlags.Static|BindingFlags.NonPublic).GetValue(null);float interfaceScale=(float)theme.GetType().GetProperty("InterfaceScale").GetValue(theme,null);
  using(var scaling=new Form{Size=new Size(500,360)}){var button=new Button{Size=new Size(100,30)};scaling.Controls.Add(button);styleType.GetMethod("Apply",BindingFlags.Static|BindingFlags.NonPublic).Invoke(null,new object[]{scaling});Check(Math.Abs(button.Font.Size-10f*interfaceScale)<.2f,"Interface size scaling still enlarges fonts twice");Check(Math.Abs(button.Width-100*interfaceScale)<=1,"Interface size scaling did not resize control bounds once");}
  Check(typeof(StudioWindow).Assembly.GetReferencedAssemblies().All(a=>!a.Name.Equals("MPNDossier",StringComparison.OrdinalIgnoreCase)),"Studio still references the Dossier assembly");
  Console.WriteLine("Creating Studio shell");using(var shell=new StudioWindow(root,true)){
   var hiddenHandle=shell.Handle;
   Check(AreDpiAwarenessContextsEqual(GetThreadDpiAwarenessContext(),new IntPtr(-4)),"Studio did not enable Per Monitor V2 high-DPI rendering");
   Check(shell.Text.Contains("MadCard Maker")&&shell.Workspaces.Count==0,"Studio did not open as a standalone MCM application");
   shell.SwitchWorkspace("MadCard");Check(shell.ActiveWorkspace=="MadCard"&&shell.Workspaces.Count==1,"MCM workspace did not open on its own");
   var home=shell.Workspaces["MadCard"].Home;Check(!home.TopLevel&&home.Dock==DockStyle.Fill,"MCM home isn't embedded");
   if(!SystemInformation.HighContrast)Check(M.DossierScrollbars.IsAttached(shell)&&M.DossierScrollbars.IsAttached(home),"Studio scrollbar styling missing");
   using(var notepad=new M.MakerNotepad()){
    notepad.Editor.Text="Formatted note";notepad.Editor.SelectAll();using(var chosenFont=new Font("Consolas",14,FontStyle.Bold)){notepad.Editor.SelectionFont=chosenFont;notepad.Editor.SelectionColor=Color.FromArgb(220,50,65);}
    string notePath=Path.Combine(Path.GetDirectoryName(root),"checks","StudioNotepadSmoke.rtf");typeof(M.MakerNotepad).GetMethod("SaveTo",Hidden).Invoke(notepad,new object[]{notePath});
    using(var restored=new RichTextBox()){restored.LoadFile(notePath,RichTextBoxStreamType.RichText);restored.SelectAll();Check(restored.Text=="Formatted note"&&restored.SelectionFont.Bold&&restored.SelectionFont.FontFamily.Name=="Consolas"&&restored.SelectionColor.R==220,"Notepad did not preserve text formatting and chosen fonts in RTF");}
    File.Delete(notePath);shell.OpenDocument("MadCard",notepad);Check(shell.Workspaces["MadCard"].Documents.Count==2,"Studio Notepad did not open as a workspace document tab");shell.CloseActiveDocument();Check(notepad.IsDisposed&&shell.Workspaces["MadCard"].Documents.Count==1,"Studio Notepad tab did not close cleanly");
   }
   var note=new Form{Text="Unsaved MCM draft",Tag="keep-this"};shell.OpenDocument("MadCard",note);Check(shell.Workspaces["MadCard"].Documents.Count==2,"MCM editor tab missing");
   shell.SwitchWorkspace("MadCard");Check(ReferenceEquals(home,shell.Workspaces["MadCard"].Home),"MCM workspace reloaded while opening a document");Check(!note.IsDisposed&&(string)note.Tag=="keep-this","MCM draft was lost");
   shell.OpenDocument("MadCard",note);Check(shell.Workspaces["MadCard"].Documents.Count==2,"Existing MCM document duplicated");shell.CloseActiveDocument();Check(shell.Workspaces["MadCard"].Documents.Count==1&&note.IsDisposed,"Close tab didn't remove the editor");shell.CloseActiveDocument();Check(!home.IsDisposed,"Workspace home was closed");
  }
 }
}
