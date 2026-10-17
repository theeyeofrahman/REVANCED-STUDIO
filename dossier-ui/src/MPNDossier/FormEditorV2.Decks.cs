using System;
using System.IO;
using System.Reflection;
using System.Linq;
using Newtonsoft.Json.Linq;
namespace MPNDossier;
public partial class FormEditorV2
{
 private readonly System.Runtime.CompilerServices.ConditionalWeakTable<System.Windows.Forms.TreeNodeCollection,JObject> _extraProperties=new System.Runtime.CompilerServices.ConditionalWeakTable<System.Windows.Forms.TreeNodeCollection,JObject>();
 private string _deckSaveDirectory;
 public void OpenDeckEntry(string kind,string path,string directory)
 {
  if(kind=="rooms")throw new ArgumentException("Use the dedicated Rooms editor for room JSON.");
  string prefix;if(!DossierDecks.EntryKinds.TryGetValue(kind,out prefix))throw new ArgumentException("Unsupported entry folder type.");
  string method=prefix.EndsWith("1")?prefix+"_Click":prefix+"ToolStripMenuItem_Click";
  typeof(FormEditorV2).GetMethod(method,BindingFlags.Instance|BindingFlags.NonPublic).Invoke(this,new object[]{this,EventArgs.Empty});
  if(path!=null){var json=JObject.Parse(File.ReadAllText(path));if(!json.Properties().Any(p=>ControlScheme.Any(c=>c.PropName==p.Name)))throw new InvalidDataException("This JSON has no matching "+kind+" fields. Check the folder's entry type.");Reset();LoadObject(ObjEditor.Nodes,json,ControlScheme);FileName.Text=Path.GetFileNameWithoutExtension(path);DisplayPrice();ResetHistory();}
  _deckSaveDirectory=Path.GetFullPath(directory);ReportEditorStatus("Deck folder • "+_deckSaveDirectory);
 }
}
