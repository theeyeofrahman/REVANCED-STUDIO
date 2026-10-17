using System;
using System.IO;
using System.Windows.Forms;
namespace MadCardMaker;
public static class StudioModule
{
 public static bool Hosted {get;private set;}
 public static string BaseDirectory {get;private set;}=Path.GetDirectoryName(typeof(StudioModule).Assembly.Location);
 public static string SharedThemePath {get;private set;}
 public static Action<Form> OpenDocument {get;private set;}
 public static void Configure(string directory,string themePath,Action<Form> open){BaseDirectory=Path.GetFullPath(directory);SharedThemePath=themePath;OpenDocument=open;Hosted=true;ModEnums.MOD_DIR=Path.Combine(BaseDirectory,"Mods")+Path.DirectorySeparatorChar;Directory.CreateDirectory(ModEnums.MOD_DIR);}
 public static Form CreateWorkspace()=>My.MyProject.Forms.FormDeckManager;
 public static void ShowDocument(Form form,IWin32Window owner=null){if(Hosted&&OpenDocument!=null)OpenDocument(form);else if(owner!=null)form.Show(owner);else form.Show();}
 public static void Style(Control control)=>DossierStyle.Apply(control);
}
