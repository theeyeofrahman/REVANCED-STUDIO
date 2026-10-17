using System.Drawing;
using System.Windows.Forms;
namespace MPNDossier;
public sealed class DossierCharacterMode:Form
{
 public bool Assisted{get;private set;}
 public DossierCharacterMode()
 {
  DossierDpi.Initialize(this);Text="MPN Dossier REVANCED — Create a character";Size=new Size(680,440);MinimumSize=new Size(580,400);StartPosition=FormStartPosition.CenterParent;MaximizeBox=false;
  var layout=new TableLayoutPanel{Dock=DockStyle.Fill,Padding=new Padding(24),ColumnCount=1,RowCount=5};layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,100));foreach(var height in new[]{44,65,68,65,68})layout.RowStyles.Add(new RowStyle(SizeType.Absolute,height));
  layout.Controls.Add(new Label{Text="How would you like to create your character?",Dock=DockStyle.Fill},0,0);
  layout.Controls.Add(new Label{Text="Assisted: pick a template and a few basic details. We write a profile and backstory, fill the character settings, and let you review everything.",Dock=DockStyle.Fill},0,1);var assisted=new DossierButton{Text="Assisted character creation",Dock=DockStyle.Fill};assisted.Click+=(_,__)=>Choose(true);layout.Controls.Add(assisted,0,2);
  layout.Controls.Add(new Label{Text="Advanced: start in the full character workbench and control the fields yourself.",Dock=DockStyle.Fill},0,3);var advanced=new DossierButton{Text="Advanced character creation",Dock=DockStyle.Fill};advanced.Click+=(_,__)=>Choose(false);layout.Controls.Add(advanced,0,4);Controls.Add(layout);DossierStyle.Apply(this);DossierDpi.Complete(this);
 }
 void Choose(bool assisted){Assisted=assisted;DialogResult=DialogResult.OK;Close();}
}
