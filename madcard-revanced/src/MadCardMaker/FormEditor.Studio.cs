using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
namespace MadCardMaker;
public partial class FormEditor
{
 TabControl studioTabs;TextBox sparsePreview,resolvedPreview,deckPreview;ListBox changedFields;
 void AddStudioEditor()
 {
  var context=InfoTab.Parent;var split=(TableLayoutPanel)context.Parent;split.Controls.Remove(context);
  studioTabs=new DossierTabs{Dock=DockStyle.Fill};var guide=new TabPage("Field help");context.Dock=DockStyle.Fill;guide.Controls.Add(context);studioTabs.TabPages.Add(guide);
  TextBox JsonPage(string text){var tab=new TabPage(text);var box=new TextBox{Dock=DockStyle.Fill,Multiline=true,ReadOnly=true,ScrollBars=ScrollBars.Both,WordWrap=false,AccessibleName=text};tab.Controls.Add(box);studioTabs.TabPages.Add(tab);return box;}
  sparsePreview=JsonPage("Overrides");resolvedPreview=JsonPage("Resolved");deckPreview=JsonPage("Deck JSON");var changes=new TabPage("Changes");changedFields=new ListBox{Dock=DockStyle.Fill};changes.Controls.Add(changedFields);studioTabs.TabPages.Add(changes);split.Controls.Add(studioTabs,1,0);
  studioTabs.SelectedIndexChanged+=(_,__)=>RefreshStudioPreview();changedFields.SelectedIndexChanged+=(_,__)=>{var key=changedFields.SelectedItem as string;if(key!=null&&ObjEditor.Nodes.ContainsKey(key)){ObjEditor.SelectedNode=ObjEditor.Nodes[key];ObjEditor.SelectedNode.EnsureVisible();}};
  var toolbar=Controls.OfType<TableLayoutPanel>().First().GetControlFromPosition(0,0) as FlowLayoutPanel;
  var review=new DossierButton{Text="Review…",Width=100,Height=36};var menu=new ContextMenuStrip();
  void Item(string text,Action action){var i=new ToolStripMenuItem(text);i.Click+=(_,__)=>{try{action();}catch(Exception e){DossierDialog.Show(this,e.Message);}};menu.Items.Add(i);}
  Item("Check this card",CheckStudioCard);Item("Copy selected field",CopyStudioField);Item("Paste copied fields",PasteStudioFields);Item("Reset all overrides…",ResetStudioOverrides);Item("Compare with inherited card",CompareStudioRoot);Item("Chatter writer…",WriteStudioChatter);Item("Import a card JSON…",ImportStudioCard);Item("Copy typed deck JSON",()=>{if(StudioReady())Clipboard.SetText(StudioDeck().ToString());});Item("Save as reusable template…",()=>{if(!StudioReady())return;var name=MakerStudio.Prompt(this,"Save reusable template","Template name",FileName.Text);if(name==null)return;MakerTools.SavePreset(name,SaveType,SaveObj(ObjEditor.Nodes));modernStatus.Text="Template saved. Use it from workspace Tools → Browse saved templates.";});Item("Next changed field",NextStudioChange);Item("Revert selected field",()=>RevertToolStripMenuItem.PerformClick());
  review.Click+=(_,__)=>{DossierStyle.Apply(menu);menu.Show(review,new Point(0,review.Height));};toolbar.Controls.Add(review);
  FileName.TextChanged+=(_,__)=>{if(!Loading&&!historyBusy&&CardRoot.SelectedItem!=null&&ObjEditor.Nodes.Count>0)MarkUnsaved(true);};
  KeyDown+=(_,e)=>{if(e.Control&&e.Shift&&e.KeyCode==Keys.V){CheckStudioCard();e.SuppressKeyPress=true;}if(e.Control&&e.KeyCode==Keys.S){SaveToDirectory("");e.SuppressKeyPress=true;}};
  foreach(var box in new[]{sparsePreview,resolvedPreview,deckPreview})box.Font=DossierFonts.Create("Consolas",10);
 }
 bool StudioReady(){if(ObjEditor.Nodes.Count==0||CardRoot.SelectedItem==null){modernStatus.Text="Choose a card type and inherited root first.";return false;}return true;}
 JObject StudioDeck()=>MakerStudio.EncodeDeck(new JObject{{SaveType,new JArray(SaveObj(ObjEditor.Nodes))}});
 List<MakerIssue> StudioIssues()
 {
  var result=MakerStudio.Check(SaveType,SaveObj(ObjEditor.Nodes),SaveObj(ObjEditor.Nodes,true));
  foreach(var node in WalkModern(ObjEditor.Nodes)){
   if(!(node.Tag is ModCards.NodeTag tag)||tag.NodeType!=ModCards.NodeType.Val||tag.LinkedControl.PropType!=ModCards.PropType.Ref)continue;
   var type=tag.LinkedControl.ObjectName;var value=Convert.ToString(tag.Value);
   if(!tag.LinkedControl.WriteNewValues&&!string.IsNullOrEmpty(type)&&type!="Object"&&!MakerStudio.Exists(type,value))result.Add(new MakerIssue{Field=node.FullPath,Message="Reference '"+value+"' is not in the currently loaded "+type+" choices."});
  }
  return result;
 }
 bool ValidateStudioSave()
 {
  if(!StudioReady())return false;var errors=StudioIssues().Where(x=>x.Error).ToArray();
  if(errors.Length==0)return true;modernStatus.Text="Resolve the card ID / root errors before saving.";MakerStudio.TextReport(this,"Card needs attention",string.Join("\r\n",errors.Select(x=>x.ToString())));return false;
 }
 void CheckStudioCard(){if(!StudioReady())return;var issues=StudioIssues();MakerStudio.TextReport(this,"Card check",$"{SaveType}: {FileName.Text}\r\n\r\n"+(issues.Count==0?"No card ID, root, health-track or loaded-reference issues found.":string.Join("\r\n",issues.Select(x=>x.ToString())))+"\r\n\r\nThis checks the loaded editor database. It does not verify compatibility with a running game.");}
 void RefreshStudioPreview()
 {
  if(studioTabs==null||studioTabs.SelectedIndex==0||Loading||CardRoot.SelectedItem==null||ObjEditor.Nodes.Count==0)return;
  try{
   if(studioTabs.SelectedIndex==1)sparsePreview.Text=SaveObj(ObjEditor.Nodes).ToString();
   if(studioTabs.SelectedIndex==2)resolvedPreview.Text=SaveObj(ObjEditor.Nodes,true).ToString();
   if(studioTabs.SelectedIndex==3)deckPreview.Text=StudioDeck().ToString();
   if(studioTabs.SelectedIndex==4){changedFields.BeginUpdate();changedFields.Items.Clear();changedFields.Items.AddRange(EditedProperties.Keys.Cast<object>().ToArray());changedFields.EndUpdate();}
  }catch(Exception ex){modernStatus.Text="Preview: "+ex.Message;}
 }
 void NextStudioChange(){var keys=EditedProperties.Keys.Where(k=>ObjEditor.Nodes.ContainsKey(k)).ToArray();if(keys.Length==0){modernStatus.Text="No overridden fields yet.";return;}var current=ObjEditor.SelectedNode==null?"":ModNodes.GetRootNode(ObjEditor.SelectedNode).Name;int ix=Array.IndexOf(keys,current);ObjEditor.SelectedNode=ObjEditor.Nodes[keys[(ix+1)%keys.Length]];ObjEditor.SelectedNode.EnsureVisible();}
 void ImportStudioCard()
 {
  using(var dialog=new DossierOpenFileDialog{Title="Import a single MadCard",Filter="MadCard JSON (*.json)|*.json"}){
   if(dialog.ShowDialog(this)!=DialogResult.OK)return;var parsed=JObject.Parse(File.ReadAllText(dialog.FileName));string type=CardType.SelectedItem as string;JObject card;
   if(parsed["Filename"]!=null)card=parsed;
   else{
    var cats=parsed.Properties().Where(p=>ModEnums.CARDTYPES.Contains(p.Name)).ToArray();
    if(cats.Length!=1||!(cats[0].Value is JArray array)||array.Count!=1||!(array[0] is JObject single))throw new FormatException("Import one card, or a deck containing exactly one card. Open larger decks from the workspace File menu.");
    type=cats[0].Name;card=single;
   }
   if(string.IsNullOrEmpty(type))throw new FormatException("Choose the card type before importing a standalone override.");
   var errors=MakerStudio.Check(type,card).Where(x=>x.Error).ToArray();if(errors.Length>0)throw new FormatException(string.Join("\r\n",errors.Select(x=>x.ToString())));
   if(Unsaved&&DossierDialog.Show(this,"Replace the current unsaved draft with this imported card?","Import card",MessageBoxButtons.YesNo)!=DialogResult.Yes)return;
   PreloadFile=(string)card["Filename"];PreloadFileInfo=card.ToString();PreloadRoot=(string)card["Root"]??"";
   if((string)CardType.SelectedItem==type)CardType_SelectedIndexChanged(this,EventArgs.Empty);else CardType.SelectedItem=type;
   MarkUnsaved(true);modernStatus.Text="Imported an editable draft. Save it to a deck when ready.";
  }
 }
}
