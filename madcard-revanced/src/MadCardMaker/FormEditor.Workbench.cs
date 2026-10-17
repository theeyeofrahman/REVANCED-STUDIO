using System;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
namespace MadCardMaker;
public partial class FormEditor
{
 void CompareStudioRoot(){if(!StudioReady())return;var root=(string)CardRoot.SelectedItem;var inherited=ModNodes.FinalVersion(SaveType,RootableEntries[root]);MakerStudio.TextReport(this,"Changes from "+root,MakerWorkbench.Compare(inherited,SaveObj(ObjEditor.Nodes,true)));}
 void WriteStudioChatter()
 {
  if(!StudioReady())return;if(SaveType!="Chatter"){modernStatus.Text="The chatter writer is available for Chatter cards. Create one using Starter presets → Custom chatter.";return;}
  using(var writer=new MakerChatterWriter(SaveObj(ObjEditor.Nodes,true)))if(writer.ShowDialog(this)==DialogResult.OK&&writer.Changes.Count>0)ApplyStudioChatter(writer.Changes);
 }
 void ApplyStudioChatter(JObject patch)
 {
  if(SaveType!="Chatter")throw new InvalidOperationException("Open a Chatter card first.");
  var full=MakerWorkbench.PatchChatter(SaveObj(ObjEditor.Nodes,true),patch);var sparse=MakerWorkbench.PatchChatter(SaveObj(ObjEditor.Nodes),patch);
  historyBusy=true;try{Reset();EditedProperties.Clear();Loading=true;LoadObject(ObjEditor.Nodes,full,ControlScheme,false);MarkEditedNodes(sparse);}finally{Loading=false;historyBusy=false;}
  MarkUnsaved(true);DisplayPrice();modernStatus.Text="Applied "+patch.Count+" chatter events. Undo restores the previous draft.";
 }
}
