using System;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
namespace MPNDossier;
public partial class FormEditorV2
{
 private const string NotesKey="_DossierNotes";
 private JToken ReadEntryNotes(){JObject extra;return _extraProperties.TryGetValue(ObjEditor.Nodes,out extra)?extra[NotesKey]?.DeepClone():null;}
 private void ApplyEntryNotes(JToken notes)
 {
  JObject extra;if(!_extraProperties.TryGetValue(ObjEditor.Nodes,out extra)){extra=new JObject();_extraProperties.Add(ObjEditor.Nodes,extra);}
  if(notes==null)extra.Remove(NotesKey);else extra[NotesKey]=notes.DeepClone();
  TrackEditorChange();ReportEditorStatus("Notes applied to this entry. Save JSON to keep them in its file.");
 }
 private void OpenEntryNotes()
 {
  if(ObjEditor.Nodes.Count==0){ReportEditorStatus("Create or load an entry before adding notes.",true);return;}
  try{using(var dialog=new DossierEntryNotes(FileName.Text,ReadEntryNotes()))if(dialog.ShowDialog(this)==DialogResult.OK)ApplyEntryNotes(dialog.Result);}
  catch(Exception ex){ReportEditorStatus("Could not open notes: "+ex.Message,true);}
 }
}
