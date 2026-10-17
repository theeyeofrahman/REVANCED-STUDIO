using System;
using System.Linq;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MPNDossier;

public partial class FormEditorV2
{
    private TextBox _propertyHelp;
    private void UpdateFieldHelp()
    {
        if(_propertyHelp==null)return;
        _propertyHelp.Text=DossierHelp.FieldText(SaveFolder,ObjEditor.SelectedNode);
        if(SelectedTag==null){ActionButton.Enabled=false;return;}
        var control=SelectedTag.LinkedControl;
        var action=control.IsList&&!control.Fixed?(SelectedTag.NodeType==ModCards.NodeType.Arr?"Add item":"Remove item"):control.PropType==ModCards.PropType.Bool?"Toggle":control.PropType==ModCards.PropType.Color?"Choose color":"";
        ActionButton.Text=action;ActionButton.Enabled=action.Length>0;
    }
    private void BuildModernLayout()
    {
        SuspendLayout(); AutoScaleMode = AutoScaleMode.Dpi; AutoScaleDimensions = new SizeF(96, 96); Controls.Clear(); AutoSize = false; MinimumSize = new Size(900, 650); Size = new Size(1150, 820);
        Text = "MPN Dossier REVANCED / Workbench"; StartPosition = FormStartPosition.CenterParent;
        SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
        var content = new TableLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(18), ColumnCount = 1, RowCount = 5 };
        content.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        content.RowStyles.Add(new RowStyle(SizeType.Absolute, 54));content.RowStyles.Add(new RowStyle(SizeType.Absolute, 58)); content.RowStyles.Add(new RowStyle(SizeType.Percent, 100)); content.RowStyles.Add(new RowStyle(SizeType.Absolute, 200));content.RowStyles.Add(new RowStyle(SizeType.Absolute, 28));
        var tools=new FlowLayoutPanel{Dock=DockStyle.Fill,WrapContents=false};
        var title=DossierStyle.Heading("Entry editor",16);title.Width=160;title.Height=42;tools.Controls.Add(title);
        void Tool(string text,EventHandler action){var button=new DossierButton{Text=text,Width=text=="New character"?125:90,Height=36,Margin=new Padding(4)};button.Click+=action;tools.Controls.Add(button);}
        Tool("New character",(_,__)=>DossierCharacterCreator.Open(this,SaveFolder=="characters"?_deckSaveDirectory:null));
        CharacterToolStripMenuItem.Click-=CharacterToolStripMenuItem_Click;CharacterToolStripMenuItem.Click+=(_,__)=>DossierCharacterCreator.Open(this,SaveFolder=="characters"?_deckSaveDirectory:null);
        NewToolStripMenuItem.DropDownItems.Insert(0,new ToolStripMenuItem("Character creation: Assisted or Advanced…",null,(_,__)=>DossierCharacterCreator.Open(this,SaveFolder=="characters"?_deckSaveDirectory:null)));
        Tool("Load entry",(_,__)=>{if(LoadableEntries!=null)LoadToolStripMenuItem_Click(this,EventArgs.Empty);});
        Tool("Save JSON",(_,__)=>SaveWithPicker());Tool("Undo",(_,__)=>TravelHistory(-1));Tool("Redo",(_,__)=>TravelHistory(1));
        Tool("Help",(_,__)=>DossierHelp.Open(this));
        content.Controls.Add(tools,0,0);
        FileName.Dock = DockStyle.Fill; FileName.Margin = new Padding(0, 0, 0, 14); content.Controls.Add(FileName, 0, 1);
        var split = new SplitContainer { Size = new Size(1000, 500), Dock = DockStyle.Fill, Panel1MinSize = 300, Panel2MinSize = 220, SplitterDistance = 660, SplitterWidth = 8 };
        ObjEditor.Dock = DockStyle.Fill; split.Panel1.Controls.Add(ObjEditor);
        var options = new Panel { Dock = DockStyle.Fill, Padding = new Padding(14, 0, 0, 0) };
        var inspector=new TableLayoutPanel{Dock=DockStyle.Fill,ColumnCount=1,RowCount=4};inspector.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,100));inspector.RowStyles.Add(new RowStyle(SizeType.Absolute,28));inspector.RowStyles.Add(new RowStyle(SizeType.Percent,40));inspector.RowStyles.Add(new RowStyle(SizeType.Absolute,36));inspector.RowStyles.Add(new RowStyle(SizeType.Percent,60));
        ListObjects.Dock = DockStyle.Fill; inspector.Controls.Add(ListObjects,0,1);
        var valuesTitle = DossierStyle.Heading("Available values"); valuesTitle.Dock = DockStyle.Fill; inspector.Controls.Add(valuesTitle,0,0);
        inspector.Controls.Add(new Label{Text="What this field does",Dock=DockStyle.Fill,TextAlign=ContentAlignment.MiddleLeft},0,2);
        _propertyHelp=new TextBox{Dock=DockStyle.Fill,Multiline=true,WordWrap=true,ScrollBars=ScrollBars.Vertical,ReadOnly=true,Text="Select a property to read its explanation."};inspector.Controls.Add(_propertyHelp,0,3);options.Controls.Add(inspector);
        split.Panel2.Controls.Add(options); content.Controls.Add(split, 0, 2);
        var edit = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 4, Padding = new Padding(0, 14, 0, 0) };
        edit.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100)); edit.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 300));
        edit.RowStyles.Add(new RowStyle(SizeType.Absolute, 24)); edit.RowStyles.Add(new RowStyle(SizeType.Absolute, 36));edit.RowStyles.Add(new RowStyle(SizeType.Percent,100));edit.RowStyles.Add(new RowStyle(SizeType.Absolute,24));
        var label = new Label { Text = "Value", Dock = DockStyle.Fill }; edit.Controls.Add(label, 0, 0); edit.SetColumnSpan(label, 2);
        Param.Dock = DockStyle.Fill; Param.Margin = new Padding(0, 3, 12, 3); edit.Controls.Add(Param, 0, 1);
        ActionButton.Dock = DockStyle.Fill; ActionButton.TextAlign = ContentAlignment.MiddleCenter; edit.Controls.Add(ActionButton, 1, 1); content.Controls.Add(edit, 0, 3);
        InitializeSuggestions(edit);
        _editorStatus=new Label{Dock=DockStyle.Fill,Text="Use the New menu for weapons, armor, animations and other entry types.",AutoEllipsis=true,TextAlign=ContentAlignment.MiddleLeft};content.Controls.Add(_editorStatus,0,4);
        LoadingProgress.Dock = DockStyle.Top; LoadingProgress.Height = 5;
        EditorStrip.Dock = DockStyle.Top;
        var filesMenu=new ToolStripMenuItem("Files");filesMenu.Click+=(_,__)=>DossierExplorer.Open(this,_deckSaveDirectory);EditorStrip.Items.Add(filesMenu);
        var projectsMenu=new ToolStripMenuItem("Projects");projectsMenu.Click+=(_,__)=>DossierProjectManager.Open(this);EditorStrip.Items.Add(projectsMenu);
        var decksMenu=new ToolStripMenuItem("Decks");decksMenu.Click+=(_,__)=>StudioModule.ShowDocument(new DossierDeckForm(),this);EditorStrip.Items.Add(decksMenu);
        var ideasMenu=new ToolStripMenuItem("Idea Gallery");ideasMenu.Click+=(_,__)=>DossierIdeaGallery.Open(this);EditorStrip.Items.Add(ideasMenu);
        var roomsMenu=new ToolStripMenuItem("Rooms");roomsMenu.Click+=(_,__)=>DossierRoomEditor.Open(this);EditorStrip.Items.Add(roomsMenu);
        var notesMenu=new ToolStripMenuItem("Notes");notesMenu.DropDownItems.Add("Notes for this entry…",null,(_,__)=>OpenEntryNotes());EditorStrip.Items.Add(notesMenu);
        Controls.Add(content); Controls.Add(LoadingProgress); Controls.Add(EditorStrip);
        DossierStyle.Apply(this); Param.Font = DossierFonts.Create(DossierStyle.Theme.FontFamily,12); ObjEditor.Font = DossierFonts.Create(DossierStyle.Theme.FontFamily,10);title.Font=DossierFonts.Create(DossierStyle.Theme.HeadingFont,16,FontStyle.Bold);
        _propertyHelp.BackColor=DossierStyle.Surface;
        ObjEditor.AfterSelect+=(_,__)=>UpdateFieldHelp();ListObjects.SelectedIndexChanged+=(_,__)=>UpdateFieldHelp();ActionButton.Click+=(_,__)=>UpdateFieldHelp();
        FileName.Enabled = true; FileName.ReadOnly = false; FileName.BorderStyle = BorderStyle.FixedSingle;
        ActionButton.AutoEllipsis = true;
        ActionButton.BackColor = DossierStyle.Accent; ActionButton.ForeColor = DossierStyle.Background;
        FileName.Font = DossierFonts.Create(DossierStyle.Theme.FontFamily, 16, FontStyle.Bold);
        // Price is still updated by the original calculator and remains visible when relevant.
        Price.Dock = DockStyle.Right; Price.AutoSize = true; EditorStrip.Items.Add(new ToolStripControlHost(Price));
        NewTransferToolStripMenuItem.Text="Game folder / auto-send…";
        SaveToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.S;
        KeyPreview=true;
        KeyDown+=(_,e)=>{if(e.Control&&e.KeyCode==Keys.Z){TravelHistory(-1);e.SuppressKeyPress=true;}if(e.Control&&e.KeyCode==Keys.Y){TravelHistory(1);e.SuppressKeyPress=true;}};
        Shown+=(_,__)=>{if(ObjEditor.Nodes.Count==0&&ModEnums.DB_CHAR.Count>0)CharacterToolStripMenuItem_Click(this,EventArgs.Empty);};
        ResumeLayout(true);
    }
}
