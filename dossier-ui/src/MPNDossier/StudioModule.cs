using System;
using System.IO;
using System.Windows.Forms;
namespace MPNDossier;
public static class StudioModule
{
 public static bool Hosted {get;private set;}
 public static string BaseDirectory {get;private set;}=Path.GetDirectoryName(typeof(StudioModule).Assembly.Location);
 public static string SharedThemePath {get;private set;}
 public static Action<Form> OpenDocument {get;private set;}
 public static void Configure(string directory,string themePath,Action<Form> open){BaseDirectory=Path.GetFullPath(directory);SharedThemePath=themePath;OpenDocument=open;Hosted=true;}
 public static Form CreateWorkspace()=>My.MyProject.Forms.FormPNDossier;
 public static void ShowDocument(Form form,IWin32Window owner=null){if(Hosted&&OpenDocument!=null)OpenDocument(form);else if(owner!=null)form.Show(owner);else form.Show();}
 public static void ActivateDocument(Form form){if(Hosted&&OpenDocument!=null)OpenDocument(form);else form.Activate();}
}
