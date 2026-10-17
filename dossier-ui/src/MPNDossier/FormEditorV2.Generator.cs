using System;
using System.IO;
using Newtonsoft.Json.Linq;
namespace MPNDossier;
public partial class FormEditorV2
{
 public void OpenAdvancedCharacter(string directory=null){CharacterToolStripMenuItem_Click(this,EventArgs.Empty);_deckSaveDirectory=directory==null?null:Path.GetFullPath(directory);}
 public void OpenGeneratedCharacter(JObject character,string name,string directory=null)
 {
  if(character==null)throw new ArgumentNullException(nameof(character));CharacterToolStripMenuItem_Click(this,EventArgs.Empty);Reset();LoadObject(ObjEditor.Nodes,(JObject)character.DeepClone(),ControlScheme);FileName.Text=DossierCharacterGenerator.FileName(name);_deckSaveDirectory=directory==null?null:Path.GetFullPath(directory);DisplayPrice();ResetHistory();ReportEditorStatus("Generated character ready. Review its settings and Save JSON to keep it.");
 }
}
