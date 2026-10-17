using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace MadCardMaker;

public sealed class MakerGameSettings
{
 public string ExecutablePath {get;set;}="";
 public string Arguments {get;set;}="";
 public static string FilePath=>Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),"MadCardMakerRevanced","game.json");
 public static MakerGameSettings Load(string path=null)=>File.Exists(path??FilePath)?JsonConvert.DeserializeObject<MakerGameSettings>(File.ReadAllText(path??FilePath))??new MakerGameSettings():new MakerGameSettings();
 public void Save(string path=null){var executable=MakerGame.Resolve(ExecutablePath);MakerHelp.Write(path??FilePath,JsonConvert.SerializeObject(new MakerGameSettings{ExecutablePath=executable,Arguments=Arguments??""},Formatting.Indented));ExecutablePath=executable;}
}

public static class MakerGame
{
 public static string Resolve(string input)
 {
  if(string.IsNullOrWhiteSpace(input))throw new ArgumentException("Choose your game's executable or installation folder first.");
  string path=Path.GetFullPath(Environment.ExpandEnvironmentVariables(input.Trim().Trim('"')));
  if(Directory.Exists(path)){
   var choices=Directory.GetFiles(path,"*.exe").Where(f=>!Path.GetFileName(f).StartsWith("unins",StringComparison.OrdinalIgnoreCase)&&Path.GetFileName(f).IndexOf("Crash",StringComparison.OrdinalIgnoreCase)<0).ToArray();
   var known=choices.Where(f=>new[]{"Madness Project Nexus.exe","MADNESS.Project.Nexus.exe","MPN.exe"}.Contains(Path.GetFileName(f),StringComparer.OrdinalIgnoreCase)).ToArray();
   if(known.Length==1)path=known[0];else if(choices.Length==1)path=choices[0];else throw new ArgumentException("Choose the game's .exe directly. This folder has "+choices.Length+" possible executables; the game may be inside a Game subfolder.");
  }
  if(!string.Equals(Path.GetExtension(path),".exe",StringComparison.OrdinalIgnoreCase)||!File.Exists(path))throw new FileNotFoundException("The game executable could not be found. Browse to its .exe file.",path);
  return path;
 }
 public static ProcessStartInfo StartInfo(MakerGameSettings settings)
 {
  var file=Resolve(settings.ExecutablePath);return new ProcessStartInfo{FileName=file,WorkingDirectory=Path.GetDirectoryName(file),Arguments=settings.Arguments??"",UseShellExecute=true};
 }
 public static void Launch(MakerGameSettings settings,Func<ProcessStartInfo,bool> starter=null)
 {
  var info=StartInfo(settings);
  if(starter!=null){if(!starter(info))throw new IOException("The game did not start.");return;}
  using(var process=Process.Start(info)){if(process==null)throw new IOException("Windows did not return a game process. Check its path and launch requirements.");}
 }
 public static bool Configure(IWin32Window owner)
 {
  using(var dialog=new MakerGameDialog())return dialog.ShowDialog(owner)==DialogResult.OK;
 }
 public static void LaunchSaved(IWin32Window owner)
 {
  var settings=MakerGameSettings.Load();if(string.IsNullOrWhiteSpace(settings.ExecutablePath)){Configure(owner);return;}Launch(settings);
 }
}

public sealed class MakerGameDialog:Form
{
 public MakerGameDialog()
 {
  AutoScaleMode=AutoScaleMode.Dpi;AutoScaleDimensions=new SizeF(96,96);Text="Game path and launch settings";Size=new Size(780,460);MinimumSize=new Size(640,400);StartPosition=FormStartPosition.CenterParent;
  var settings=MakerGameSettings.Load();var root=new TableLayoutPanel{Dock=DockStyle.Fill,Padding=new Padding(22),ColumnCount=1,RowCount=7};root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,100));foreach(int height in new[]{38,48,30,40,64})root.RowStyles.Add(new RowStyle(SizeType.Absolute,height));root.RowStyles.Add(new RowStyle(SizeType.Percent,100));root.RowStyles.Add(new RowStyle(SizeType.Absolute,46));
  root.Controls.Add(new Label{Text="Game executable or installation folder",Dock=DockStyle.Fill},0,0);
  var pathRow=new TableLayoutPanel{Dock=DockStyle.Fill,ColumnCount=3};pathRow.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,100));pathRow.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute,95));pathRow.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute,95));var input=new TextBox{Text=settings.ExecutablePath,Dock=DockStyle.Fill,AccessibleName="Game executable or folder"};pathRow.Controls.Add(input,0,0);
  var browse=new DossierButton{Text="EXE…",Dock=DockStyle.Fill};browse.Click+=(_,__)=>{using(var picker=new DossierOpenFileDialog{Title="Choose the game executable",Filter="Windows executable (*.exe)|*.exe"})if(picker.ShowDialog(this)==DialogResult.OK)input.Text=picker.FileName;};pathRow.Controls.Add(browse,1,0);
  var folder=new DossierButton{Text="Folder",Dock=DockStyle.Fill};folder.Click+=(_,__)=>{using(var picker=new DossierFolderBrowserDialog{Description="Choose your game folder"})if(picker.ShowDialog(this)==DialogResult.OK)input.Text=picker.SelectedPath;};pathRow.Controls.Add(folder,2,0);root.Controls.Add(pathRow,0,1);
  root.Controls.Add(new Label{Text="Launch arguments (optional)",Dock=DockStyle.Fill},0,2);var args=new TextBox{Text=settings.Arguments,Dock=DockStyle.Fill,AccessibleName="Optional game launch arguments"};root.Controls.Add(args,0,3);
  root.Controls.Add(new Label{Text="Save the path here, then use Launch game in the MCM sidebar.\r\nThis launches your installed game; it does not deploy mods or change game versions.",Dock=DockStyle.Fill},0,4);
  var status=new Label{Dock=DockStyle.Fill,AutoEllipsis=true};root.Controls.Add(status,0,5);var buttons=new FlowLayoutPanel{Dock=DockStyle.Fill};var check=new DossierButton{Text="Check path",Width=140,Height=36};check.Click+=(_,__)=>{try{status.Text="Found: "+MakerGame.Resolve(input.Text);}catch(Exception ex){status.Text=ex.Message;}};buttons.Controls.Add(check);
  var save=new DossierButton{Text="Save game path",Width=160,Height=36};save.Click+=(_,__)=>{try{new MakerGameSettings{ExecutablePath=input.Text,Arguments=args.Text}.Save();DialogResult=DialogResult.OK;Close();}catch(Exception ex){status.Text=ex.Message;}};buttons.Controls.Add(save);buttons.Controls.Add(new DossierButton{Text="Cancel",Width=110,Height=36,DialogResult=DialogResult.Cancel});root.Controls.Add(buttons,0,6);Controls.Add(root);DossierStyle.Apply(this);
 }
}
