using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
namespace MPNDossier;
public sealed class DossierHelpForm:Form
{
 public DossierHelpForm()
 {
  DossierDpi.Initialize(this);
  Text="MPN Dossier REVANCED — Help & field reference";Size=new Size(1060,750);MinimumSize=new Size(830,600);StartPosition=FormStartPosition.CenterParent;
  var articles=DossierHelp.Articles();var split=new SplitContainer{Size=new Size(1000,600),Dock=DockStyle.Fill,SplitterDistance=330,Panel1MinSize=250,Panel2MinSize=350,Padding=new Padding(14),SplitterWidth=8};
  var topics=new ListBox{Dock=DockStyle.Fill,HorizontalScrollbar=true};var search=new TextBox{Dock=DockStyle.Top,AccessibleName="Search help topics and descriptions"};var prompt=new Label{Text="Search help",Dock=DockStyle.Top,Height=30};var body=new TextBox{Dock=DockStyle.Fill,Multiline=true,ReadOnly=true,WordWrap=true,ScrollBars=ScrollBars.Vertical,BorderStyle=BorderStyle.None};
  var category=new ComboBox{Dock=DockStyle.Top,DropDownStyle=ComboBoxStyle.DropDownList};category.Items.AddRange(new object[]{"Feature guide","Field reference","Enum reference","All topics"});category.SelectedIndex=0;
  split.Panel1.Padding=new Padding(12);split.Panel1.Controls.Add(topics);split.Panel1.Controls.Add(category);split.Panel1.Controls.Add(search);split.Panel1.Controls.Add(prompt);split.Panel2.Padding=new Padding(20);split.Panel2.Controls.Add(body);Controls.Add(split);
  topics.SelectedIndexChanged+=(_,__)=>{body.Text=topics.SelectedItem is string key?key+"\r\n\r\n"+articles[key]:"No matching help topics.";};
  void Filter(){topics.DataSource=articles.Where(kv=>(category.SelectedIndex==3||(category.SelectedIndex==0&&!kv.Key.Contains(" / "))||(category.SelectedIndex==1&&kv.Key.StartsWith("Reference / "))||(category.SelectedIndex==2&&kv.Key.StartsWith("Enums / ")))&&(kv.Key.IndexOf(search.Text,StringComparison.OrdinalIgnoreCase)>=0||kv.Value.IndexOf(search.Text,StringComparison.OrdinalIgnoreCase)>=0)).Select(kv=>kv.Key).ToArray();if(topics.Items.Count==0)body.Text="No matching help topics in this category.";}
  search.TextChanged+=(_,__)=>Filter();category.SelectedIndexChanged+=(_,__)=>Filter();
  DossierStyle.Apply(this);body.BackColor=DossierStyle.Surface;body.Font=DossierFonts.Create(DossierStyle.Theme.FontFamily,11);Filter();DossierDpi.Complete(this);
 }
}
