using System;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
namespace MadCardMaker;
public partial class FormEditor
{
 void CopyStudioField()
 {
  if(!StudioReady())return;if(ObjEditor.SelectedNode==null)throw new InvalidOperationException("Select a field to copy.");var field=ModNodes.GetRootNode(ObjEditor.SelectedNode).Name;var value=SaveObj(ObjEditor.Nodes,true)[field];if(value==null)throw new InvalidOperationException("This field has no copyable value.");Clipboard.SetText(MakerFields.Envelope(SaveType,field,value).ToString());modernStatus.Text="Copied "+field+". Paste into a card of the same type from Review.";
 }
 void PasteStudioFields(){if(!StudioReady())return;if(!Clipboard.ContainsText())throw new FormatException("Copy an MCM field first.");ApplyStudioFields(JObject.Parse(Clipboard.GetText()));}
 void ApplyStudioFields(JObject envelope)
 {
  var fields=MakerFields.Read(SaveType,envelope);ApplyStudioDraft(MakerFields.Merge(SaveObj(ObjEditor.Nodes,true),fields),MakerFields.Merge(SaveObj(ObjEditor.Nodes),fields),"Pasted "+fields.Count+" fields. Undo restores the previous draft.");
 }
 void ResetStudioOverrides()
 {
  if(!StudioReady())return;if(DossierDialog.Show(this,"Reset every override to its inherited value? The card ID and root stay the same. You can Undo this reset.","Reset overrides",MessageBoxButtons.YesNo)!=DialogResult.Yes)return;ResetStudioOverridesCore();
 }
 void ResetStudioOverridesCore()
 {
  string root=CardRoot.SelectedItem.ToString();var inherited=ModNodes.FinalVersion(SaveType,RootableEntries[root]);ApplyStudioDraft(inherited,new JObject{{"Filename",FileName.Text},{"Root",root}},"Reset all overrides. Undo restores the previous draft.");
 }
 void ApplyStudioDraft(JObject full,JObject sparse,string message)
 {
  historyBusy=true;try{Reset();EditedProperties.Clear();Loading=true;LoadObject(ObjEditor.Nodes,full,ControlScheme,false);MarkEditedNodes(sparse);}finally{Loading=false;historyBusy=false;}
  MarkUnsaved(true);DisplayPrice();modernStatus.Text=message;
 }
}
