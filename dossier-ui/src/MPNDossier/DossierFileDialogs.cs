using System;
using System.Linq;
using System.Windows.Forms;
namespace MPNDossier;
public class DossierOpenFileDialog:IDisposable
{
 public string Title{get;set;}public string Filter{get;set;}="All files|*.*";public string InitialDirectory{get;set;}public string FileName{get;set;}="";public string[] FileNames{get;protected set;}=new string[0];public bool Multiselect{get;set;}public bool AddExtension{get;set;}=true;public string DefaultExt{get;set;}="";public bool OverwritePrompt{get;set;}=true;
 protected virtual DossierBrowseMode Mode=>DossierBrowseMode.Open;
 public DialogResult ShowDialog(IWin32Window owner=null){using(var browser=new DossierFileExplorer(Mode,InitialDirectory,FileName,Filter){Multiselect=Multiselect,AddExtension=AddExtension,DefaultExt=DefaultExt,OverwritePrompt=OverwritePrompt}){if(!string.IsNullOrWhiteSpace(Title))browser.Text=Title;var result=owner==null?browser.ShowDialog():browser.ShowDialog(owner);if(result==DialogResult.OK){FileNames=browser.SelectedPaths;FileName=FileNames.FirstOrDefault()??"";}return result;}}
 public void Dispose(){}
}
public sealed class DossierSaveFileDialog:DossierOpenFileDialog{protected override DossierBrowseMode Mode=>DossierBrowseMode.Save;}
public sealed class DossierFolderBrowserDialog:IDisposable
{
 public string Description{get;set;}="Choose a folder";public string SelectedPath{get;set;}="";public bool ShowNewFolderButton{get;set;}=true;
 public DialogResult ShowDialog(IWin32Window owner=null){using(var browser=new DossierFileExplorer(DossierBrowseMode.Folder,SelectedPath){ShowNewFolderButton=ShowNewFolderButton}){browser.Text=Description;var result=owner==null?browser.ShowDialog():browser.ShowDialog(owner);if(result==DialogResult.OK)SelectedPath=browser.SelectedPaths[0];return result;}}
 public void Dispose(){}
}
