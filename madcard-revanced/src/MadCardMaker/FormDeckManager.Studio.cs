using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
namespace MadCardMaker;
public partial class FormDeckManager
{
 void AddStudioShell()
 {
  MinimumSize=new Size(1100,720);Size=new Size(1320,850);
  var content=Controls.OfType<TableLayoutPanel>().First();Controls.Remove(content);
  var shell=new TableLayoutPanel{Dock=DockStyle.Fill,ColumnCount=2,RowCount=1,Margin=Padding.Empty,Padding=Padding.Empty};shell.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute,230));shell.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,100));shell.RowStyles.Add(new RowStyle(SizeType.Percent,100));content.Margin=Padding.Empty;shell.Controls.Add(content,1,0);
  var sidebar=new FlowLayoutPanel{Dock=DockStyle.Fill,Margin=Padding.Empty,Padding=new Padding(18,24,12,16),FlowDirection=FlowDirection.TopDown,WrapContents=false,AutoScroll=true};
  sidebar.Controls.Add(new Label{Text="MCM\r\nREVANCED",Tag="Heading",Width=195,Height=82,Font=DossierFonts.Create(DossierStyle.Theme.HeadingFont,20,FontStyle.Bold)});
  sidebar.Controls.Add(new Label{Text="Your workspace",Width=195,Height=24,Font=DossierFonts.Create(DossierStyle.Theme.FontFamily,11,FontStyle.Bold),ForeColor=DossierStyle.Muted,Margin=new Padding(0,0,0,7)});
  void Extra(string text,Action action){var b=new DossierButton{Text=text,Width=195,Height=44,Margin=new Padding(0,0,0,9),TextAlign=ContentAlignment.MiddleLeft,Padding=new Padding(12,3,8,3)};b.Click+=(_,__)=>RunStudio(action);sidebar.Controls.Add(b);}
  Extra("Create with assistance",NewAssistedCharacter);Extra("Browse starter presets",NewStudioPreset);Extra("Duplicate selected card",DuplicateStudioCard);Extra("Check this mod",CheckStudioDecks);Extra("Open project notebook",OpenStudioNotebook);Extra("More tools…",OpenStudioTools);
  sidebar.Controls.Add(new Label{Text="Game",Width=195,Height=24,Font=DossierFonts.Create(DossierStyle.Theme.FontFamily,11,FontStyle.Bold),ForeColor=DossierStyle.Muted,Margin=new Padding(0,16,0,7)});
  Extra("Set game location",()=>{if(MakerGame.Configure(this))workspaceStatus.Text="Game location saved. Choose Launch game when you are ready.";});Extra("Launch game",()=>{MakerGame.LaunchSaved(this);workspaceStatus.Text="Launch game requested. If you just set a location, choose Launch game again.";});
  sidebar.Controls.Add(new Label{Text="About MADCard decks\r\n\r\nDecks organize typed game cards and can inherit settings from a root. Pick a database that matches your game version.\r\n\r\nCtrl+F  Search\r\nCtrl+D  Duplicate selected card",Width=195,Height=178,Margin=new Padding(0,24,0,0),ForeColor=DossierStyle.Muted});
  shell.Controls.Add(sidebar,0,0);Controls.Add(shell);shell.SendToBack();
  Shown+=(_,__)=>{
   if(StudioModule.Hosted)return;var prefs=DossierPreferences.Current;
   if(prefs.MainWindowMaximized)WindowState=FormWindowState.Maximized;
   else{WindowState=FormWindowState.Normal;var area=Screen.FromControl(this).WorkingArea;double scale=DeviceDpi/96d;Size=new Size(Math.Min(area.Width,(int)(Math.Max(1100,Math.Min(3000,prefs.MainWindowWidth))*scale)),Math.Min(area.Height,(int)(Math.Max(700,Math.Min(2000,prefs.MainWindowHeight))*scale)));}
  };
  FormClosing+=(_,e)=>{if(e.Cancel||StudioModule.Hosted||DossierPreferences.SuppressPersistenceForTests)return;if(StudioModule.Hosted)return;var prefs=DossierPreferences.Current;var bounds=WindowState==FormWindowState.Normal?Bounds:RestoreBounds;double scale=DeviceDpi/96d;prefs.MainWindowWidth=(int)Math.Round(bounds.Width/scale);prefs.MainWindowHeight=(int)Math.Round(bounds.Height/scale);prefs.MainWindowMaximized=WindowState==FormWindowState.Maximized;prefs.Persist();};
  FormClosing+=(_,e)=>{if(e.Cancel||DossierPreferences.SuppressPersistenceForTests||!CardManager.Visible||ViewOnly||!CP_Save.Enabled)return;var answer=DossierDialog.Show(this,"Save the loaded deck changes before closing?","Unsaved decks",MessageBoxButtons.YesNoCancel);if(answer==DialogResult.Cancel){e.Cancel=true;return;}if(answer==DialogResult.Yes)try{ReloadAllDecks(true);}catch(Exception ex){e.Cancel=true;DossierDialog.Show(this,ex.Message,"Could not save decks");}};
  // Less-used ordering actions stay one click away instead of taking two toolbar rows.
  var more=new DossierButton{Text="More…",Width=90,Height=34};var menu=new ContextMenuStrip();var gated=new List<Tuple<ToolStripMenuItem,Button>>();
  void More(Button b,string text,Action action){ControlPanel.Controls.Remove(b);var item=new ToolStripMenuItem(text);item.Click+=(_,__)=>RunStudio(action);menu.Items.Add(item);gated.Add(Tuple.Create(item,b));}
  More(CP_View,"Preview selection",()=>CP_View_Click(CP_View,EventArgs.Empty));More(CP_Rename,"Rename",()=>CP_Rename_Click(CP_Rename,EventArgs.Empty));More(CP_Remove,"Remove from deck",()=>CP_Remove_Click(CP_Remove,EventArgs.Empty));menu.Items.Add(new ToolStripSeparator());More(CP_ShiftUp,"Move up",()=>CP_ShiftUp_Click(CP_ShiftUp,EventArgs.Empty));More(CP_ShiftDown,"Move down",()=>CP_ShiftDown_Click(CP_ShiftDown,EventArgs.Empty));More(CP_PriorityUp,"Increase priority",()=>CP_PriorityUp_Click(CP_PriorityUp,EventArgs.Empty));More(CP_PriorityDown,"Decrease priority",()=>CP_PriorityDown_Click(CP_PriorityDown,EventArgs.Empty));More(CP_Sort,"Move to top",()=>CP_Sort_Click(CP_Sort,EventArgs.Empty));More(CP_Unlist,"Move to end",()=>CP_Unlist_Click(CP_Unlist,EventArgs.Empty));
  menu.Opening+=(_,__)=>{foreach(var pair in gated)pair.Item1.Enabled=pair.Item2.Enabled;};more.Click+=(_,__)=>{DossierStyle.Apply(menu);menu.Show(more,new Point(0,more.Height));};ControlPanel.Controls.Add(more);
  content.RowStyles[1].Height=50;CP_Refresh.Text="Back";CP_Refresh.Width=80;CP_Add.Width=140;CP_AddDeck.Width=105;CP_Edit.Width=105;CP_Save.Width=110;
  KeyDown+=(_,e)=>{if(e.Control&&e.KeyCode==Keys.D){RunStudio(DuplicateStudioCard);e.SuppressKeyPress=true;}};
 }
 void RunStudio(Action action){try{action();}catch(Exception ex){DossierDialog.Show(this,ex.Message,"Could not complete that action");}}
 bool StudioDeckReady(){if(!CardManager.Visible||CardManager.SelectedNode==null||SelectedTag?.ObjType==ModCards.NodeType.Log){workspaceStatus.Text="Open a mod, create a deck and select it first.";return false;}return true;}
 void NewStudioPreset(){if(!StudioDeckReady())return;using(var f=new MakerPresetDialog())if(f.ShowDialog(this)==DialogResult.OK){StudioModule.ShowDocument(new FormEditor{Manager=this,PreloadType=f.Preset.Type,PreloadRoot=(string)f.Card["Root"],PreloadFile=(string)f.Card["Filename"],PreloadFileInfo=f.Card.ToString()},this);}}
 void DuplicateStudioCard(){if(!StudioDeckReady()||SelectedTag?.ObjType!=ModCards.NodeType.Val){workspaceStatus.Text="Select a card to duplicate.";return;}string id=MakerStudio.Prompt(this,"Duplicate card","New card ID",SelectedTag.FileName+"_Copy");if(id==null)return;var deck=ModNodes.GetRootNode(CardManager.SelectedNode);if(deck.Nodes[SelectedTag.Type]?.Nodes[id]!=null)throw new FormatException("That card ID already exists in this deck.");var copy=MakerStudio.Duplicate(SelectedTag.Info,id);var tag=new ModCards.CardTag(id,SelectedTag.Type,SelectedTag.RootName,copy){DisplayName=SelectedTag.DisplayName,Description=SelectedTag.Description};AddCard(deck,tag,true,SelectedTag.Priority);workspaceStatus.Text="Duplicated as "+id;}
 void CheckStudioDecks(){if(!CardManager.Visible){workspaceStatus.Text="Open a mod to check its loaded decks.";return;}var lines=new List<string>();var seen=new HashSet<string>();int total=0;foreach(var n in Descend(CardManager.Nodes)){if(!(n.Tag is ModCards.CardTag c)||c.ObjType!=ModCards.NodeType.Val)continue;total++;if(!seen.Add(c.ID()))lines.Add("CHECK · "+c.ID()+" has multiple definitions; review their priorities.");foreach(var issue in MakerStudio.Check(c.Type,c.Info))lines.Add(c.ID()+" / "+issue);foreach(var reference in MakerTools.References(c.Type,c.Info))if(!MakerStudio.Exists(reference.Type,reference.Id))lines.Add("CHECK · "+c.ID()+" / "+reference+" is missing from the loaded database.");}MakerStudio.TextReport(this,"Deck check",$"Checked {total} cards in the loaded mod.\r\nThis checks editor data, not whether the game can run a mod.\r\n\r\n"+(lines.Count==0?"No card ID or root issues found.":string.Join("\r\n",lines)));}
 void OpenStudioNotebook(){string project=Path.Combine(ModEnums.MOD_DIR,string.IsNullOrEmpty(LoadedMod)?"_Workspace":LoadedMod);using(var f=new MakerNotebook(project,SelectedTag?.ObjType==ModCards.NodeType.Val?SelectedTag.ID():""))f.ShowDialog(this);}
}
