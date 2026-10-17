using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
namespace MPNDossier;
public partial class FormEditorV2
{
 private ListBox _suggestions;
 private Label _suggestionHint;
 private bool _acceptingSuggestion;
 private TreeNode _suggestionNode;
 private readonly Timer _suggestionTimer=new Timer{Interval=150};
 private void InitializeSuggestions(TableLayoutPanel edit)
 {
  _suggestions=new ListBox{Dock=DockStyle.Fill,HorizontalScrollbar=true,IntegralHeight=false,TabStop=false};
  _suggestionHint=new Label{Text="Select a field for suggestions.",Dock=DockStyle.Fill,AutoEllipsis=true};
  edit.Controls.Add(_suggestions,0,2);edit.SetColumnSpan(_suggestions,2);edit.Controls.Add(_suggestionHint,0,3);edit.SetColumnSpan(_suggestionHint,2);
  _suggestions.MouseClick+=(_,e)=>{int index=_suggestions.IndexFromPoint(e.Location);if(index>=0){_suggestions.SelectedIndex=index;AcceptSuggestion();}};
  _suggestions.KeyDown+=(_,e)=>{if(e.KeyCode==Keys.Enter||e.KeyCode==Keys.Tab){AcceptSuggestion();e.SuppressKeyPress=true;}else if(e.KeyCode==Keys.Escape){_suggestions.Visible=false;Param.Focus();e.SuppressKeyPress=true;}};
  _suggestionTimer.Tick+=(_,__)=>{_suggestionTimer.Stop();RefreshSuggestions(Param.Text);};
  Param.TextChanged+=(_,__)=>{if(Loading||_acceptingSuggestion||!Param.Focused)return;_suggestionTimer.Stop();_suggestionTimer.Start();};
  Param.Enter+=(_,__)=>{if(SelectedTag!=null&&SelectedTag.NodeType==ModCards.NodeType.Val)RefreshSuggestions("");};
  ObjEditor.AfterSelect+=(_,__)=>{_suggestionTimer.Stop();RefreshSuggestions("");};
  FormClosed+=(_,__)=>_suggestionTimer.Dispose();
 }
 private void RefreshSuggestions(string query)
 {
  if(_suggestions==null||_acceptingSuggestion)return;
  var results=DossierSuggestions.Build(SaveFolder,ObjEditor.SelectedNode,query,ObjEditor.Nodes);
  _suggestionNode=ObjEditor.SelectedNode;
  _suggestions.DataSource=results;_suggestions.Visible=results.Count>0;
  if(_suggestions.Parent is TableLayoutPanel edit&&edit.Parent is TableLayoutPanel content){edit.RowStyles[2].SizeType=SizeType.Absolute;edit.RowStyles[2].Height=results.Count>0?96:0;content.RowStyles[3].Height=results.Count>0?200:100;}
  var tag=SelectedTag;
  if(tag==null||tag.NodeType!=ModCards.NodeType.Val){_suggestionHint.Text="Select a field or expand a list to edit its items.";return;}
  if(results.Count>0)_suggestionHint.Text="↑ ↓ select  ·  Tab / Enter accept  ·  Esc dismiss  ·  Click to choose";
  else if(tag.LinkedControl.Selection!=null)_suggestionHint.Text="No matching choice. Try a name or ID; the existing value is preserved.";
  else _suggestionHint.Text=tag.LinkedControl.PropType==ModCards.PropType.Int?"Whole number. See field help for its meaning.":tag.LinkedControl.PropType==ModCards.PropType.Float?"Decimal number. Suggestions appear when the reference documents special values.":"Free text — enter your own value.";
 }
 private void AcceptSuggestion()
 {
  if(!(_suggestions.SelectedItem is DossierSuggestion suggestion)||SelectedTag==null||SelectedTag.NodeType!=ModCards.NodeType.Val||!ReferenceEquals(_suggestionNode,ObjEditor.SelectedNode))return;
  _suggestionTimer.Stop();_acceptingSuggestion=true;
  try{
   var tag=SelectedTag;var node=ObjEditor.SelectedNode;tag.Value=suggestion.Value;node.Text=GetDisplayNode(node);Loading=true;
   Param.Text=suggestion.Value is double number?number.ToString(System.Globalization.CultureInfo.CurrentCulture):suggestion.Id;
   if(ListObjects.DataSource!=null)ListObjects.SelectedItem=suggestion.Value;
   Loading=false;DisplayPrice();TrackEditorChange();UpdateFieldHelp();
   string key=DossierSuggestions.FieldKey(SaveFolder,node);var choices=DossierPreferences.Current.FieldChoices;
   if(!choices.ContainsKey(key)||choices[key]==null)choices[key]=new System.Collections.Generic.List<string>();
   choices[key].Remove(suggestion.Id);choices[key].Insert(0,suggestion.Id);if(choices[key].Count>8)choices[key].RemoveRange(8,choices[key].Count-8);
   if(choices.Count>150)choices.Remove(choices.Keys.First(k=>k!=key));DossierPreferences.Current.Persist();
   ReportEditorStatus("Applied "+suggestion.Id+(string.IsNullOrEmpty(suggestion.Reason)?"":" • "+suggestion.Reason));
  }finally{Loading=false;_acceptingSuggestion=false;}
  DismissSuggestions();Param.Focus();Param.SelectAll();DismissSuggestions();
 }
 private bool HandleSuggestionKey(Keys keys)
 {
  if(Param.Focused&&keys==Keys.Escape&&_suggestions!=null){_suggestionTimer.Stop();DismissSuggestions();return true;}
  if(!Param.Focused||_suggestions==null||!_suggestions.Visible||_suggestions.Items.Count==0)return false;
  if(keys==Keys.Down||keys==Keys.Up){_suggestions.SelectedIndex=Math.Max(0,Math.Min(_suggestions.Items.Count-1,_suggestions.SelectedIndex+(keys==Keys.Down?1:-1)));return true;}
  if(keys==Keys.Tab||keys==Keys.Enter){AcceptSuggestion();return true;}
  if(keys==Keys.Escape){_suggestions.Visible=false;_suggestionTimer.Stop();return true;}
  return false;
 }
 protected override bool ProcessCmdKey(ref Message message,Keys keys)
 {if(HandleSuggestionKey(keys))return true;return base.ProcessCmdKey(ref message,keys);}
 private void DismissSuggestions()
 {
  _suggestions.Visible=false;
  if(_suggestions.Parent is TableLayoutPanel edit&&edit.Parent is TableLayoutPanel content){edit.RowStyles[2].Height=0;content.RowStyles[3].Height=100;}
 }
}
