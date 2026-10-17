using System;
using System.Drawing;
using System.Windows.Forms;
namespace MPNDossier;
public sealed class DossierTransferSettings:Form
{
 public DossierTransferSettings()
 {
  DossierDpi.Initialize(this);Text="Game folder / auto-send";Size=new Size(700,420);MinimumSize=new Size(580,380);StartPosition=FormStartPosition.CenterParent;MaximizeBox=false;
  var settings=DossierTransfer.ReadSettings();var path=new TextBox{Text=settings["Folder"]?.ToString()??ModEnums.DIR_TRANSFER,Dock=DockStyle.Fill,ReadOnly=true};
  var auto=new CheckBox{Text="Automatically send completed JSON files when I save",AutoSize=true,Checked=settings["AutoTransferOnSave"]?.Type==Newtonsoft.Json.Linq.JTokenType.Boolean&&(bool)settings["AutoTransferOnSave"]};
  var root=new TableLayoutPanel{Dock=DockStyle.Fill,Padding=new Padding(20),ColumnCount=1,RowCount=6};root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,100));foreach(var h in new[]{34,38,40,40})root.RowStyles.Add(new RowStyle(SizeType.Absolute,h));root.RowStyles.Add(new RowStyle(SizeType.Percent,100));root.RowStyles.Add(new RowStyle(SizeType.Absolute,42));
  root.Controls.Add(new Label{Text="Your game's Custom Stuff folder",Dock=DockStyle.Fill},0,0);root.Controls.Add(path,0,1);var browse=new DossierButton{Text="Choose folder…",Dock=DockStyle.Fill};root.Controls.Add(browse,0,2);root.Controls.Add(auto,0,3);var message=new Label{Text="Characters → characters; rooms → rooms; weapons → weapons, ranged, thrown or weaponmodded. Other types use their matching folder. Existing game files get a .bak backup. Your local/project copy is kept.",Dock=DockStyle.Fill};root.Controls.Add(message,0,4);var save=new DossierButton{Text="Apply transfer settings",Dock=DockStyle.Fill};root.Controls.Add(save,0,5);Controls.Add(root);
  browse.Click+=(_,__)=>{using(var picker=new DossierFolderBrowserDialog{Description="Select the game's Custom Stuff folder",SelectedPath=path.Text})if(picker.ShowDialog(this)==DialogResult.OK)path.Text=picker.SelectedPath;};
  save.Click+=(_,__)=>{try{DossierTransfer.Configure(path.Text,auto.Checked);DialogResult=DialogResult.OK;}catch(Exception ex){message.Text=ex.Message;}};
  DossierStyle.Apply(this);DossierDpi.Complete(this);
 }
}
