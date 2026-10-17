using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
namespace MadCardMaker;
public partial class FormDeckManager
{
 sealed class BatchRecord { public ModCards.CardTag Tag;public JObject Before,After; }
 List<BatchRecord> lastBatch;
 static void ReplaceStudioValues(JObject target,JObject source){target.RemoveAll();foreach(var p in source.Properties())target[p.Name]=p.Value.DeepClone();}
 IEnumerable<ModCards.CardTag> StudioCards()=>Descend(CardManager.Nodes).Select(n=>n.Tag).OfType<ModCards.CardTag>().Where(c=>c.ObjType==ModCards.NodeType.Val);
 void OpenStudioTools()
 {
  var menu=new ContextMenuStrip();
  void Add(string text,Action action){var item=menu.Items.Add(text);item.Click+=(_,__)=>RunStudio(action);}
  Add("Reference library…",BrowseStudioLibrary);Add("Bulk edit cards…",BatchStudioCards);Add("Undo last bulk edit",UndoStudioBatch);Add("Find references to selected card",FindStudioUses);Add("Browse saved templates…",UseStudioTemplate);Add("Manage templates…",()=>{using(var f=new MakerTemplateManager())f.ShowDialog(this);});Add("Export saved mod as ZIP…",ExportStudioPackage);
  DossierStyle.Apply(menu);menu.Show(Cursor.Position);
 }
 void BatchStudioCards()
 {
  if(!StudioDeckReady()||ViewOnly)return;
  if(Application.OpenForms.OfType<FormEditor>().Any(e=>e.Manager==this))throw new InvalidOperationException("Close this workspace's card editor tabs before bulk editing, so their drafts cannot be overwritten.");
  var cards=StudioCards().ToArray();if(cards.Length==0)throw new InvalidOperationException("Add cards to a deck first.");
  using(var f=new Form{Text="Bulk edit · preview before applying",Size=new Size(850,650),MinimumSize=new Size(700,520),StartPosition=FormStartPosition.CenterParent}){
   var layout=new TableLayoutPanel{Dock=DockStyle.Fill,Padding=new Padding(20),ColumnCount=1,RowCount=5};layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,100));foreach(var h in new[]{42,42,48})layout.RowStyles.Add(new RowStyle(SizeType.Absolute,h));layout.RowStyles.Add(new RowStyle(SizeType.Percent,100));layout.RowStyles.Add(new RowStyle(SizeType.Absolute,46));
   var type=new ComboBox{Dock=DockStyle.Fill,DropDownStyle=ComboBoxStyle.DropDownList};type.Items.AddRange(cards.Select(c=>c.Type).Distinct().OrderBy(x=>x).Cast<object>().ToArray());
   var field=new ComboBox{Dock=DockStyle.Fill,DropDownStyle=ComboBoxStyle.DropDownList};var value=new TextBox{Dock=DockStyle.Fill,Text="12",AccessibleName="New value in JSON syntax"};
   var instructions=new FlowLayoutPanel{Dock=DockStyle.Fill};instructions.Controls.Add(new Label{Text="Value (JSON: 12, true, or \"text\")",AutoSize=true});instructions.Controls.Add(value);value.Width=380;
   var list=new CheckedListBox{Dock=DockStyle.Fill,CheckOnClick=true,HorizontalScrollbar=true};var visible=new List<ModCards.CardTag>();
   type.SelectedIndexChanged+=(_,__)=>{field.Items.Clear();field.Items.AddRange(MakerTools.Schema(type.Text).Where(p=>!p.IsList&&p.PropType!=ModCards.PropType.Obj&&p.PropType!=ModCards.PropType.Color&&!new[]{"Filename","Root","Priority"}.Contains(p.PropName)).Select(p=>p.PropName).Distinct().Cast<object>().ToArray());if(field.Items.Count>0)field.SelectedIndex=0;list.Items.Clear();visible.Clear();foreach(var c in cards.Where(c=>c.Type==type.Text)){visible.Add(c);list.Items.Add(c.AttachedDeck+" / "+c.FileName,true);}};
   field.SelectedIndexChanged+=(_,__)=>{var c=cards.FirstOrDefault(x=>x.Type==type.Text);var existing=c?.Info[field.Text];if(existing==null&&c!=null&&ModEnums.SC_DIR.TryGetValue(c.Type,out var roots)&&roots.TryGetValue(c.RootName,out var inherited))existing=inherited[field.Text];var spec=MakerTools.Schema(type.Text).FirstOrDefault(p=>p.PropName==field.Text);value.Text=existing is JValue?existing.ToString(Newtonsoft.Json.Formatting.None):spec?.PropType==ModCards.PropType.Bool?"false":spec?.PropType==ModCards.PropType.Int||spec?.PropType==ModCards.PropType.Float?"1":"\"\"";};
   var apply=new DossierButton{Text="Preview changes…",Width=190,Height=36};
   apply.Click+=(_,__)=>{try{
    var changes=list.CheckedIndices.Cast<int>().Select(i=>visible[i]).Select(c=>new BatchRecord{Tag=c,Before=(JObject)c.Info.DeepClone(),After=MakerTools.SetScalar(c.Type,c.Info,field.Text,JToken.Parse(value.Text))}).Where(c=>!JToken.DeepEquals(c.Before,c.After)).ToList();
    if(changes.Count==0)throw new InvalidOperationException("Select cards and a value that changes them.");
    var report="Set "+type.Text+"."+field.Text+"\r\n\r\n"+string.Join("\r\n",changes.Select(c=>c.Tag.AttachedDeck+" / "+c.Tag.ID()+": "+(c.Before[field.Text]?.ToString(Newtonsoft.Json.Formatting.None)??"(inherited)")+" → "+c.After[field.Text].ToString(Newtonsoft.Json.Formatting.None)));
    if(!ConfirmStudioBatch(f,report,changes.Count))return;
    foreach(var change in changes)ReplaceStudioValues(change.Tag.Info,change.After);lastBatch=changes;ReloadAllDecks();CP_Save.Enabled=true;workspaceStatus.Text="Updated "+changes.Count+" cards. Tools → Undo last bulk edit can restore this batch.";f.Close();
   }catch(Exception ex){DossierDialog.Show(f,ex.Message,"Bulk edit");}};
   layout.Controls.Add(type,0,0);layout.Controls.Add(field,0,1);layout.Controls.Add(instructions,0,2);layout.Controls.Add(list,0,3);layout.Controls.Add(apply,0,4);f.Controls.Add(layout);DossierStyle.Apply(f);type.SelectedIndex=0;f.ShowDialog(this);
  }
 }
 bool ConfirmStudioBatch(IWin32Window owner,string text,int count)
 {
  using(var f=new Form{Text="Review "+count+" card changes",Size=new Size(800,540),StartPosition=FormStartPosition.CenterParent}){var root=new TableLayoutPanel{Dock=DockStyle.Fill,Padding=new Padding(18),RowCount=2};root.RowStyles.Add(new RowStyle(SizeType.Percent,100));root.RowStyles.Add(new RowStyle(SizeType.Absolute,48));root.Controls.Add(new TextBox{Text=text,Dock=DockStyle.Fill,Multiline=true,ReadOnly=true,ScrollBars=ScrollBars.Both,WordWrap=false},0,0);var buttons=new FlowLayoutPanel{Dock=DockStyle.Fill};buttons.Controls.Add(new DossierButton{Text="Apply changes",Width=160,Height=36,DialogResult=DialogResult.OK});buttons.Controls.Add(new DossierButton{Text="Cancel",Width=110,Height=36,DialogResult=DialogResult.Cancel});root.Controls.Add(buttons,0,1);f.Controls.Add(root);DossierStyle.Apply(f);return f.ShowDialog(owner)==DialogResult.OK;}
 }
 void UndoStudioBatch()
 {
  if(lastBatch==null)throw new InvalidOperationException("There is no bulk edit to undo in this session.");
  if(Application.OpenForms.OfType<FormEditor>().Any(e=>e.Manager==this))throw new InvalidOperationException("Close the card editor tabs before restoring a bulk edit.");
  var cards=StudioCards().ToArray();if(lastBatch.Any(c=>!cards.Contains(c.Tag)||!JToken.DeepEquals(c.Tag.Info,c.After)))throw new InvalidOperationException("These cards changed after the bulk edit or belong to a different mod. Undo was stopped to protect newer work.");
  foreach(var c in lastBatch)ReplaceStudioValues(c.Tag.Info,c.Before);lastBatch=null;ReloadAllDecks();CP_Save.Enabled=true;workspaceStatus.Text="Restored the cards from before the last bulk edit.";
 }
 void FindStudioUses()
 {
  if(!StudioDeckReady()||SelectedTag?.ObjType!=ModCards.NodeType.Val)throw new InvalidOperationException("Select a card to find where it is used.");
  var lines=new List<string>();foreach(var c in StudioCards())foreach(var r in MakerTools.References(c.Type,c.Info))if(r.Type.Equals(SelectedTag.Type,StringComparison.OrdinalIgnoreCase)&&r.Id.Equals(SelectedTag.FileName,StringComparison.OrdinalIgnoreCase))lines.Add(c.AttachedDeck+" / "+c.ID()+" / "+r.Field);
  MakerStudio.TextReport(this,"References to "+SelectedTag.ID(),"Explicit references in the loaded mod (including inherited roots):\r\n\r\n"+(lines.Count==0?"No explicit uses found.":string.Join("\r\n",lines)));
 }
 void UseStudioTemplate()
 {
  if(!StudioDeckReady()||ViewOnly)return;var templates=MakerTools.ReadPresets();if(templates.Count==0)throw new InvalidOperationException("Open a card and choose Review → Save as reusable template first.");
  using(var f=new Form{Text="Your reusable templates",Size=new Size(620,450),StartPosition=FormStartPosition.CenterParent}){var root=new TableLayoutPanel{Dock=DockStyle.Fill,Padding=new Padding(18),RowCount=2};root.RowStyles.Add(new RowStyle(SizeType.Percent,100));root.RowStyles.Add(new RowStyle(SizeType.Absolute,48));var list=new ListBox{Dock=DockStyle.Fill};foreach(var p in templates)list.Items.Add(p["Type"]+" · "+p["Name"]);var use=new DossierButton{Text="Use template",Width=160,Height=36};use.Click+=(_,__)=>RunStudio(()=>{if(list.SelectedIndex<0)return;var p=templates[list.SelectedIndex];string id=MakerStudio.Prompt(f,"New card from template","New card ID",(string)p["Card"]["Filename"]+"_Copy");if(id==null)return;var card=MakerStudio.Duplicate((JObject)p["Card"],id);StudioModule.ShowDocument(new FormEditor{Manager=this,PreloadType=(string)p["Type"],PreloadRoot=(string)card["Root"],PreloadFile=id,PreloadFileInfo=card.ToString()},this);f.Close();});root.Controls.Add(list,0,0);root.Controls.Add(use,0,1);f.Controls.Add(root);DossierStyle.Apply(f);list.SelectedIndex=0;f.ShowDialog(this);}
 }
 void ExportStudioPackage()
 {
  if(!StudioDeckReady())return;
  using(var dialog=new DossierSaveFileDialog{Title="Export saved mod files (save deck changes first)",Filter="ZIP package (*.zip)|*.zip",FileName=LoadedMod+".zip"}){if(dialog.ShowDialog(this)!=DialogResult.OK)return;int count=MakerTools.Package(Path.Combine(ModEnums.MOD_DIR,LoadedMod),dialog.FileName);workspaceStatus.Text="Packaged "+count+" saved files. Unsaved editor drafts are not included.";}
 }
 void BrowseStudioLibrary()
 {
  bool ready=CardManager.Visible&&CardManager.SelectedNode!=null&&SelectedTag?.ObjType!=ModCards.NodeType.Log&&!ViewOnly;
  using(var library=new MakerReferenceLibrary(ready))if(library.ShowDialog(this)==DialogResult.OK){string id=MakerStudio.Prompt(this,"Create inherited variant","New card ID",library.RootId+"_Custom");if(id==null)return;if(!MakerStudio.ValidId(id))throw new FormatException("Enter a valid card ID.");var card=new JObject{{"Filename",id},{"Root",library.RootId}};StudioModule.ShowDocument(new FormEditor{Manager=this,PreloadType=library.CardType,PreloadRoot=library.RootId,PreloadFile=id,PreloadFileInfo=card.ToString()},this);}
 }
}
