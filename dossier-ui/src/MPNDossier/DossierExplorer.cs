using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
namespace MPNDossier;
public static class DossierExplorer
{
 public static void Open(IWin32Window owner,string path=null){var browser=new DossierFileExplorer(DossierBrowseMode.Browse,path??AppDomain.CurrentDomain.BaseDirectory);browser.SetEntryKinds(DossierDecks.EntryKinds.Keys);browser.FileRequested+=(file,kind)=>OpenEntry(browser,file,kind);StudioModule.ShowDocument(browser,owner);}
 static void OpenEntry(IWin32Window owner,string path,string kind)
 {
  if(!path.EndsWith(".json",StringComparison.OrdinalIgnoreCase))throw new InvalidOperationException("This file can be previewed here. Select an entry JSON to open an editor.");
  if(new FileInfo(path).Length>2*1024*1024)throw new InvalidOperationException("This JSON exceeds the 2 MB editor limit.");
  if(Path.GetFileName(path).Equals(DossierProject.ManifestName,StringComparison.OrdinalIgnoreCase)){var manager=new DossierProjectManager();try{manager.LoadProject(path);StudioModule.ShowDocument(manager,owner);}catch{manager.Dispose();throw;}return;}
  if(kind==null){for(var folder=new DirectoryInfo(Path.GetDirectoryName(path));folder!=null;folder=folder.Parent){var match=DossierDecks.EntryKinds.Keys.FirstOrDefault(k=>k.Equals(folder.Name,StringComparison.OrdinalIgnoreCase));if(match!=null){kind=match;break;}}}
  if(kind==null)throw new InvalidOperationException("Choose this file's entry type in the dropdown beside Refresh, then click Edit entry.");
  if(kind=="rooms"){var editor=new DossierRoomEditor(false);try{editor.SetRoomFolder(Path.GetDirectoryName(path));editor.OpenPath(path);StudioModule.ShowDocument(editor,owner);}catch{editor.Dispose();throw;}}
  else{var editor=new FormEditorV2();try{editor.OpenDeckEntry(kind,path,Path.GetDirectoryName(path));StudioModule.ShowDocument(editor,owner);}catch{editor.Dispose();throw;}}
 }
}
