using System;
using System.Drawing;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
namespace MPNDossier;
public sealed class DossierDeckForm:Form
{
 DossierDecks store;
 readonly ListBox decks=new ListBox{Dock=DockStyle.Fill},folders=new ListBox{Dock=DockStyle.Fill,HorizontalScrollbar=true};
 readonly ListBox files=new ListBox{Dock=DockStyle.Fill,HorizontalScrollbar=true};
 readonly TextBox preview=new TextBox{Dock=DockStyle.Fill,Multiline=true,ReadOnly=true,ScrollBars=ScrollBars.Both,WordWrap=false};
 readonly TextBox name=new TextBox{Width=200,AccessibleName="Deck name"},filter=new TextBox{Dock=DockStyle.Top,AccessibleName="Filter files by name"};
 readonly ComboBox kind=new ComboBox{Width=145,DropDownStyle=ComboBoxStyle.DropDownList};
 readonly Label status=new Label{Dock=DockStyle.Bottom,Height=40,AutoEllipsis=true};
 List<string> filePaths=new List<string>();
 DossierDeck Deck=>decks.SelectedItem as DossierDeck;
 DossierDeckFolder Folder=>folders.SelectedItem as DossierDeckFolder;
 public DossierDeckForm(DossierDecks data=null)
 {
  DossierDpi.Initialize(this);
  Text="MPN Dossier REVANCED — Decks";Size=new Size(1240,830);MinimumSize=new Size(1100,700);StartPosition=FormStartPosition.CenterParent;
  try{store=data??DossierDecks.Load();}catch(Exception ex){store=new DossierDecks();status.Text="Cannot read saved decks: "+ex.Message;}
  var root=new TableLayoutPanel{Dock=DockStyle.Fill,Padding=new Padding(18),ColumnCount=1,RowCount=3};root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,100));root.RowStyles.Add(new RowStyle(SizeType.Absolute,40));root.RowStyles.Add(new RowStyle(SizeType.Absolute,48));root.RowStyles.Add(new RowStyle(SizeType.Percent,100));
  root.Controls.Add(new Label{Text="Decks group folders of entries and assets. Removing a link leaves its files on disk.",Dock=DockStyle.Fill},0,0);
  var tools=new FlowLayoutPanel{Dock=DockStyle.Fill,WrapContents=false};tools.Controls.Add(name);
  void Button(string text,Action action){var b=new DossierButton{Text=text,Width=112,Height=32};b.Click+=(_,__)=>Run(action);tools.Controls.Add(b);}
  Button("New deck",()=>{var title=name.Text.Trim();if(title.Length==0)throw new InvalidOperationException("Enter a deck name first.");var deck=new DossierDeck{Name=title};store.Decks.Add(deck);Persist();RefreshDecks(deck);});
  Button("Rename",()=>{if(Deck==null)return;if(string.IsNullOrWhiteSpace(name.Text))throw new InvalidOperationException("Enter a deck name.");Deck.Name=name.Text.Trim();Persist();RefreshDecks(Deck);});
  Button("Remove deck",()=>{if(Deck==null)return;store.Decks.Remove(Deck);Persist();RefreshDecks(null);});
  kind.Items.Add("Files");kind.Items.AddRange(DossierDecks.EntryKinds.Keys.Cast<object>().ToArray());kind.SelectedIndex=0;tools.Controls.Add(kind);
  Button("Add folder",()=>{if(Deck==null)throw new InvalidOperationException("Create or select a deck first.");using(var dialog=new DossierFolderBrowserDialog{Description="Choose a folder for this deck"})if(dialog.ShowDialog(this)==DialogResult.OK){DossierDecks.AddFolder(Deck,dialog.SelectedPath,kind.SelectedItem.ToString());Persist();RefreshFolders();}});
  Button("Unlink folder",()=>{if(Deck==null||Folder==null)return;Deck.Folders.Remove(Folder);Persist();RefreshFolders();});root.Controls.Add(tools,0,1);
  var columns=new TableLayoutPanel{Dock=DockStyle.Fill,ColumnCount=3,RowCount=1};columns.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,20));columns.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,30));columns.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,50));
  var left=new TableLayoutPanel{Dock=DockStyle.Fill,ColumnCount=1,RowCount=4};left.RowStyles.Add(new RowStyle(SizeType.Absolute,26));left.RowStyles.Add(new RowStyle(SizeType.Percent,40));left.RowStyles.Add(new RowStyle(SizeType.Absolute,32));left.RowStyles.Add(new RowStyle(SizeType.Percent,60));left.Controls.Add(new Label{Text="Decks",Dock=DockStyle.Fill},0,0);left.Controls.Add(decks,0,1);left.Controls.Add(new Label{Text="Folders in this deck",Dock=DockStyle.Fill},0,2);left.Controls.Add(folders,0,3);columns.Controls.Add(left,0,0);
  var center=new Panel{Dock=DockStyle.Fill,Padding=new Padding(10,0,10,0)};center.Controls.Add(files);center.Controls.Add(filter);columns.Controls.Add(center,1,0);
  var right=new Panel{Dock=DockStyle.Fill};right.Controls.Add(preview);var actions=new FlowLayoutPanel{Dock=DockStyle.Bottom,Height=42,WrapContents=false};
  var open=new DossierButton{Text="Edit JSON entry",Width=150,Height=32};open.Click+=(_,__)=>Run(()=>OpenEntry(false));actions.Controls.Add(open);
  var create=new DossierButton{Text="New entry here",Width=150,Height=32};create.Click+=(_,__)=>Run(()=>OpenEntry(true));actions.Controls.Add(create);
  var browse=new DossierButton{Text="Open folder",Width=120,Height=32};browse.Click+=(_,__)=>Run(()=>{if(Folder!=null&&Directory.Exists(Folder.Path))DossierExplorer.Open(this,Folder.Path);});actions.Controls.Add(browse);right.Controls.Add(actions);columns.Controls.Add(right,2,0);root.Controls.Add(columns,0,2);Controls.Add(root);Controls.Add(status);DossierStyle.Apply(this);preview.Font=DossierFonts.Create(DossierStyle.Theme.FontFamily,10);
  decks.SelectedIndexChanged+=(_,__)=>{name.Text=Deck?.Name??"";RefreshFolders();};folders.SelectedIndexChanged+=(_,__)=>RefreshFiles();files.SelectedIndexChanged+=(_,__)=>ReadPreview();filter.TextChanged+=(_,__)=>FilterFiles();files.DoubleClick+=(_,__)=>Run(()=>OpenEntry(false));RefreshDecks(null);DossierDpi.Complete(this);
 }
 void Run(Action action){try{action();}catch(Exception ex){status.Text=ex.Message;}}
 void Persist(){store.Save();status.Text="Deck links saved.";}
 void RefreshDecks(DossierDeck selected){decks.DataSource=null;decks.DataSource=store.Decks.ToArray();if(selected!=null)decks.SelectedItem=selected;RefreshFolders();}
 void RefreshFolders(){folders.DataSource=null;folders.DataSource=Deck?.Folders.ToArray()??new DossierDeckFolder[0];RefreshFiles();}
 void RefreshFiles(){var issues=new List<string>();filePaths=Folder==null?new List<string>():DossierDecks.Scan(Folder.Path,issues);FilterFiles();status.Text=issues.Count>0?string.Join("  ·  ",issues):filePaths.Count+" files. Select an entry type when adding a folder to enable editing.";}
 void FilterFiles(){files.DataSource=filePaths.Where(p=>Path.GetFileName(p).IndexOf(filter.Text,StringComparison.OrdinalIgnoreCase)>=0).Select(p=>new DeckFile{Path=p,Root=Folder?.Path}).ToArray();ReadPreview();}
 void ReadPreview()
 {
  if(!(files.SelectedItem is DeckFile file)){preview.Text="Select a file to preview it.";return;}
  try{var info=new FileInfo(file.Path);if(!file.Path.EndsWith(".json",StringComparison.OrdinalIgnoreCase)){preview.Text=file.Path+"\r\n\r\n"+info.Length.ToString("N0")+" bytes\r\n\r\nAsset linked to this deck. Use Open folder to access it.";return;}if(info.Length>2*1024*1024){preview.Text="This JSON is too large for the inline preview.\r\n"+file.Path;return;}preview.Text=JToken.Parse(File.ReadAllText(file.Path)).ToString();}
  catch(Exception ex){preview.Text="Could not preview this file:\r\n"+ex.Message;}
 }
 void OpenEntry(bool create)
 {
  if(Folder==null||!DossierDecks.EntryKinds.ContainsKey(Folder.Kind))throw new InvalidOperationException("Choose a typed entry folder, such as characters or weapons. Files-only folders hold assets.");
  var file=files.SelectedItem as DeckFile;if(!create&&(file==null||!file.Path.EndsWith(".json",StringComparison.OrdinalIgnoreCase)))throw new InvalidOperationException("Select a JSON entry to edit.");
  if(!create&&new FileInfo(file.Path).Length>2*1024*1024)throw new InvalidOperationException("This entry exceeds the 2 MB editor limit.");
  if(create&&Folder.Kind=="characters"){DossierCharacterCreator.Open(this,Folder.Path);return;}
  if(Folder.Kind=="rooms"){var roomEditor=new DossierRoomEditor(false);try{roomEditor.SetRoomFolder(Folder.Path);if(!create)roomEditor.OpenPath(file.Path);StudioModule.ShowDocument(roomEditor,this);}catch{roomEditor.Dispose();throw;}return;}
  var editor=new FormEditorV2();try{editor.OpenDeckEntry(Folder.Kind,create?null:file.Path,create?Folder.Path:Path.GetDirectoryName(file.Path));StudioModule.ShowDocument(editor,this);}catch{editor.Dispose();throw;}
 }
 sealed class DeckFile{public string Path,Root;public override string ToString()=>!string.IsNullOrEmpty(Root)&&Path.StartsWith(Root,StringComparison.OrdinalIgnoreCase)?Path.Substring(Root.TrimEnd('\\').Length).TrimStart('\\'):Path;}
}
