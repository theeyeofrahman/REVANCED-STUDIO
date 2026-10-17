using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

namespace MPNDossier;

public partial class FormPNDossier
{
    private TextBox _characterSearch;
    private Label _characterCount,_statusLine;
    private DossierCharacterBrowser _characterBrowser;
    private ComboBox _browserScope,_factionFilter;
    private DossierSummary _summary;
    private DossierButton _favoriteButton,_compareButton,_soundButton;
    private bool _filtering;
    private string _pinnedSubject;
    private void BuildModernLayout()
    {
        Text=DossierBrand.Name;ShowIcon=true;SuspendLayout();AutoScaleMode=AutoScaleMode.Dpi;AutoScaleDimensions=new SizeF(96,96);AutoSize=false;MaximumSize=Size.Empty;MinimumSize=new Size(1100,760);Size=new Size(1390,940);MaximizeBox=true;StartPosition=FormStartPosition.CenterScreen;
        SetStyle(ControlStyles.OptimizedDoubleBuffer|ControlStyles.AllPaintingInWmPaint,true);Controls.Clear();CharSelect.BindingContext=new BindingContext();
        var shell=new TableLayoutPanel{Dock=DockStyle.Fill,Padding=new Padding(18),ColumnCount=1,RowCount=3};shell.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,100));shell.RowStyles.Add(new RowStyle(SizeType.Absolute,88));shell.RowStyles.Add(new RowStyle(SizeType.Percent,100));shell.RowStyles.Add(new RowStyle(SizeType.Absolute,27));
        var header=new TableLayoutPanel{Dock=DockStyle.Fill,ColumnCount=8};header.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,100));header.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute,155));header.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute,120));header.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute,100));header.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute,72));header.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute,90));header.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute,125));header.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute,95));
        var title=new FlowLayoutPanel{Dock=DockStyle.Fill,FlowDirection=FlowDirection.TopDown,WrapContents=false};title.Controls.Add(DossierStyle.Heading("REVANCED",23));title.Controls.Add(new Label{Text="MPN Dossier",AutoSize=true});header.Controls.Add(title,0,0);
        BtnConvert.Text="Entry editor";BtnGlossary.Text="Glossary";_soundButton=new DossierButton{Text=DossierPreferences.Current.Sounds?"Sound: on":"Sound: off"};
        foreach(var button in new[]{BtnConvert,BtnGlossary,_soundButton}){button.Dock=DockStyle.Fill;button.Margin=new Padding(8,8,0,24);}
        header.Controls.Add(BtnConvert,1,0);header.Controls.Add(BtnGlossary,2,0);header.Controls.Add(_soundButton,3,0);var help=new DossierButton{Text="Help",Dock=DockStyle.Fill,Margin=new Padding(8,8,0,24)};help.Click+=(_,__)=>DossierHelp.Open(this);header.Controls.Add(help,4,0);var decksButton=new DossierButton{Text="Projects",Dock=DockStyle.Fill,Margin=new Padding(8,8,0,24)};decksButton.Click+=(_,__)=>DossierProjectManager.Open(this);header.Controls.Add(decksButton,5,0);var ideasButton=new DossierButton{Text="Idea Gallery",Dock=DockStyle.Fill,Margin=new Padding(8,8,0,24)};ideasButton.Click+=(_,__)=>DossierIdeaGallery.Open(this);header.Controls.Add(ideasButton,6,0);var roomsButton=new DossierButton{Text="Rooms",Dock=DockStyle.Fill,Margin=new Padding(8,8,0,24)};roomsButton.Click+=(_,__)=>DossierRoomEditor.Open(this);header.Controls.Add(roomsButton,7,0);shell.Controls.Add(header,0,0);
        _soundButton.Click+=(_,__)=>{using(var settings=new DossierSettingsForm()){settings.ShowDialog(this);_soundButton.Text=DossierPreferences.Current.Sounds?"Sound: on":"Sound: off";}};
        var body=new TableLayoutPanel{Dock=DockStyle.Fill,ColumnCount=2};body.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute,302));body.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,100));
        var browser=new TableLayoutPanel{Dock=DockStyle.Fill,ColumnCount=1,RowCount=7,Padding=new Padding(10),Margin=new Padding(0,0,15,0)};browser.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,100));
        foreach(var row in new[]{28,32,36})browser.RowStyles.Add(new RowStyle(SizeType.Absolute,row));browser.RowStyles.Add(new RowStyle(SizeType.Percent,100));browser.RowStyles.Add(new RowStyle(SizeType.Absolute,26));browser.RowStyles.Add(new RowStyle(SizeType.Absolute,170));browser.RowStyles.Add(new RowStyle(SizeType.Absolute,24));
        browser.Controls.Add(DossierStyle.Heading("Characters"),0,0);_characterSearch=new TextBox{Dock=DockStyle.Fill,AccessibleName="Search character names or IDs"};_characterSearch.TextChanged+=(_,__)=>FilterCharacters();browser.Controls.Add(_characterSearch,0,1);
        var filters=new TableLayoutPanel{Dock=DockStyle.Fill,ColumnCount=2};filters.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,45));filters.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,55));
        _browserScope=new ComboBox{Dock=DockStyle.Fill,DropDownStyle=ComboBoxStyle.DropDownList};_browserScope.Items.AddRange(new object[]{"All characters","Favorites","Recent"});_browserScope.SelectedIndex=0;
        _factionFilter=new ComboBox{Dock=DockStyle.Fill,DropDownStyle=ComboBoxStyle.DropDownList};_factionFilter.Items.Add("All factions");_factionFilter.SelectedIndex=0;
        _browserScope.SelectedIndexChanged+=(_,__)=>FilterCharacters();_factionFilter.SelectedIndexChanged+=(_,__)=>FilterCharacters();filters.Controls.Add(_browserScope,0,0);filters.Controls.Add(_factionFilter,1,0);browser.Controls.Add(filters,0,2);
        _characterBrowser=new DossierCharacterBrowser{Dock=DockStyle.Fill};_characterBrowser.SelectedIndexChanged+=(_,__)=>{if(!_filtering&&_characterBrowser.SelectedItem is string id)SelectSubject(id);};browser.Controls.Add(_characterBrowser,0,3);
        _characterCount=new Label{Dock=DockStyle.Fill,Text="Loading archive…"};browser.Controls.Add(_characterCount,0,4);CharDisplay1.Dock=DockStyle.Fill;browser.Controls.Add(CharDisplay1,0,5);browser.Controls.Add(new Label{Text="Ctrl+F: search  ·  F1: help",Dock=DockStyle.Fill},0,6);body.Controls.Add(browser,0,0);
        var dossier=new TableLayoutPanel{Dock=DockStyle.Fill,ColumnCount=1,RowCount=2};dossier.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,100));dossier.RowStyles.Add(new RowStyle(SizeType.Absolute,43));dossier.RowStyles.Add(new RowStyle(SizeType.Percent,100));
        var actions=new FlowLayoutPanel{Dock=DockStyle.Fill,WrapContents=false};_favoriteButton=ArchiveAction("☆ Favorite",()=>ToggleFavorite());_compareButton=ArchiveAction("Pin comparison",()=>PinComparison());
        actions.Controls.Add(_favoriteButton);actions.Controls.Add(_compareButton);actions.Controls.Add(ArchiveAction("Copy summary",()=>{if(_summary.Data!=null){Clipboard.SetText(_summary.Brief());SetArchiveStatus("Dossier brief copied.");}}));actions.Controls.Add(ArchiveAction("Export PNG",()=>ExportSummary()));var fileExplorer=ArchiveAction("Files",()=>DossierExplorer.Open(this));fileExplorer.Width=96;actions.Controls.Add(fileExplorer);dossier.Controls.Add(actions,0,0);
        Tabs.Dock=DockStyle.Fill;dossier.Controls.Add(Tabs,0,1);body.Controls.Add(dossier,1,0);shell.Controls.Add(body,0,1);
        _statusLine=new Label{Dock=DockStyle.Fill,Text="LOCAL ARCHIVE / Waiting for database",TextAlign=ContentAlignment.MiddleLeft};shell.Controls.Add(_statusLine,0,2);Controls.Add(shell);
        PgOverview.Controls.Clear();var scroll=new Panel{Dock=DockStyle.Fill,AutoScroll=true};_summary=new DossierSummary{Dock=DockStyle.Top,Height=860};scroll.Controls.Add(_summary);PgOverview.Controls.Add(scroll);
        ArrangePage(PgAttributes,CharLstAttr1,CharAttDesc1,Label1);ArrangePage(PgDefense,CharListArmor1,CharArmorDesc1,CharArmorList1);ArrangePage(PgOffense,CharListWeap1,CharWeaponDesc1,CharFavored1);ArrangePage(PgSkills,CharLists1,CharListDesc1,CharSkills1);
        PgPersonality.Controls.Clear();CharLstChatter1.Dock=DockStyle.Fill;PgPersonality.Controls.Add(CharLstChatter1);
        DossierStyle.Apply(this);title.Controls[0].Font=DossierFonts.Create(DossierStyle.Theme.HeadingFont,24,FontStyle.Bold);title.Controls[0].ForeColor=DossierStyle.Accent;title.Controls[1].ForeColor=_statusLine.ForeColor=_characterCount.ForeColor=DossierStyle.Muted;
        BtnConvert.BackColor=DossierStyle.Surface;BtnConvert.ForeColor=DossierStyle.Text;CharDisplay1.SizeMode=PictureBoxSizeMode.Zoom;_characterBrowser.ItemHeight=48;
        KeyPreview=true;KeyDown+=(_,e)=>{if(e.Control&&e.KeyCode==Keys.F){_characterSearch.Focus();_characterSearch.SelectAll();e.SuppressKeyPress=true;}else if(e.Control&&e.KeyCode==Keys.D){ToggleFavorite();e.SuppressKeyPress=true;}};
        Shown+=(_,__)=>{_soundButton.Text=DossierPreferences.Current.Sounds?"Sound: on":"Sound: off";if(CharSelect.SelectedItem is string id)SelectSubject(id);};ResumeLayout(true);
    }
    private static DossierButton ArchiveAction(string text,Action action){var button=new DossierButton{Text=text,Width=145,Height=34,Margin=new Padding(0,0,8,0)};button.Click+=(_,__)=>action();return button;}
    private void FilterCharacters()
    {
        if(_characterBrowser==null||_filtering)return;_filtering=true;
        try
        {
            var selected=CharSelect.SelectedItem as string;var all=ModEnums.DB_CHAR.Keys.Union(ModEnums.IMAGES.Keys,StringComparer.OrdinalIgnoreCase).OrderBy(id=>id).ToArray();
            if(CharSelect.Items.Count!=all.Length)CharSelect.DataSource=all;
            var factions=ModEnums.DB_CHAR.Values.Select(d=>FactionName(d)).Distinct().OrderBy(f=>f).ToArray();
            if(_factionFilter.Items.Count!=factions.Length+1){var previous=_factionFilter.SelectedItem as string;_factionFilter.Items.Clear();_factionFilter.Items.Add("All factions");_factionFilter.Items.AddRange(factions);_factionFilter.SelectedItem=previous??"All factions";if(_factionFilter.SelectedIndex<0)_factionFilter.SelectedIndex=0;}
            var terms=_characterSearch.Text.Split(new[]{' '},StringSplitOptions.RemoveEmptyEntries);var scope=_browserScope.SelectedIndex;var faction=_factionFilter.SelectedItem as string;
            var visible=all.Where(id=>
            {
                JObject data;ModEnums.DB_CHAR.TryGetValue(id,out data);var searchable=id+" "+data?["FullName"]+" "+data?["Title"];
                return terms.All(t=>searchable.IndexOf(t,StringComparison.OrdinalIgnoreCase)>=0)&&(scope!=1||DossierPreferences.Current.Favorites.Contains(id))&&(scope!=2||DossierPreferences.Current.Recent.Contains(id))&&(faction==null||faction=="All factions"||FactionName(data)==faction);
            }).ToArray();
            if(scope==2)visible=visible.OrderBy(id=>DossierPreferences.Current.Recent.IndexOf(id)).ToArray();
            _characterBrowser.DataSource=visible;if(selected!=null&&visible.Contains(selected))_characterBrowser.SelectedItem=selected;
            _characterCount.Text=visible.Length+" / "+all.Length+" subjects";
        }
        finally{_filtering=false;}
        if(_characterBrowser.SelectedItem is string match)SelectSubject(match);
        else{Tabs.Enabled=false;_summary.Bind("",null);_summary.Error="NO MATCHING SUBJECTS\nTry another search or filter.";CharDisplay1.Image=null;_summary.Invalidate();SetArchiveStatus("No matching subjects.");}
    }
    private static string FactionName(JObject data){int value;return int.TryParse(data?["DefaultFaction"]?.ToString(),out value)?((ModEnums.Faction)value).ToString():"Unknown";}
    private void SelectSubject(string id){if(CharSelect.SelectedItem as string!=id)CharSelect.SelectedItem=id;CharSelect_SelectedIndexChanged(CharSelect,EventArgs.Empty);}
    private void SubjectUpdated(string id)
    {
        if(_summary==null)return;JObject data;ModEnums.DB_CHAR.TryGetValue(id,out data);_summary.Bind(id,data);_summary.CompareId=_pinnedSubject;Tabs.Enabled=data!=null;
        if(data==null){_summary.Error="No character record is available for this portrait.";SetArchiveStatus(id+" / Portrait only");return;}
        var prefs=DossierPreferences.Current;prefs.Recent.Remove(id);prefs.Recent.Insert(0,id);if(prefs.Recent.Count>20)prefs.Recent.RemoveRange(20,prefs.Recent.Count-20);prefs.Persist();
        _favoriteButton.Text=prefs.Favorites.Contains(id)?"★ Favorited":"☆ Favorite";SetArchiveStatus(id+" / "+FactionName(data)+" / "+ModEnums.DB_CHAR.Count+" character records loaded");DossierFeedback.Play("tab");
    }
    private void ToggleFavorite(){if(_summary.Data==null)return;var prefs=DossierPreferences.Current;var id=_summary.SubjectId;if(prefs.Favorites.Contains(id))prefs.Favorites.Remove(id);else prefs.Favorites.Add(id);prefs.Persist();_favoriteButton.Text=prefs.Favorites.Contains(id)?"★ Favorited":"☆ Favorite";_characterBrowser.Invalidate();if(_browserScope.SelectedIndex==1)FilterCharacters();}
    private void PinComparison(){if(_summary.Data==null)return;_pinnedSubject=_pinnedSubject==_summary.SubjectId?null:_summary.SubjectId;_summary.CompareId=_pinnedSubject;_compareButton.Text=_pinnedSubject==null?"Pin comparison":"Unpin: "+_pinnedSubject;_summary.Invalidate();SetArchiveStatus(_pinnedSubject==null?"Comparison cleared.":"Pinned "+_pinnedSubject+". Select another subject to compare base attributes.");}
    private void ExportSummary(){if(_summary.Data==null)return;using(var dialog=new DossierSaveFileDialog{Filter="Dossier card (*.png)|*.png",FileName=_summary.SubjectId+"-dossier.png",AddExtension=true,DefaultExt="png"})if(dialog.ShowDialog(this)==DialogResult.OK){_summary.ExportCard(dialog.FileName);SetArchiveStatus("Exported "+dialog.FileName);DossierFeedback.Play("save");}}
    private void SetArchiveStatus(string message){if(_statusLine!=null)_statusLine.Text=message;}
    private static void ArrangePage(TabPage page,TreeView tree,RichTextBox description,Label heading)
    {
        page.Controls.Clear();page.Padding=new Padding(12);var split=new SplitContainer{Size=new Size(680,450),Dock=DockStyle.Fill,BackColor=DossierStyle.Line,Panel1MinSize=170,Panel2MinSize=170,SplitterDistance=280};tree.Dock=DockStyle.Fill;description.Dock=DockStyle.Fill;description.ReadOnly=true;split.Panel1.Controls.Add(tree);split.Panel2.Padding=new Padding(14);split.Panel2.Controls.Add(description);heading.Dock=DockStyle.Top;heading.Height=36;page.Controls.Add(split);page.Controls.Add(heading);
    }
}
