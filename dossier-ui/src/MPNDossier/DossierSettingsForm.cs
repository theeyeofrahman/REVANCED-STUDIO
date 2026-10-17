using System.Drawing;
using System.Windows.Forms;

namespace MPNDossier;

public sealed class DossierSettingsForm:Form
{
    public DossierSettingsForm()
    {
  DossierDpi.Initialize(this);
        Text="Interface / audio";ClientSize=new Size(500,380);FormBorderStyle=FormBorderStyle.FixedDialog;StartPosition=FormStartPosition.CenterParent;MaximizeBox=false;MinimizeBox=false;
        var root=new TableLayoutPanel{Dock=DockStyle.Fill,Padding=new Padding(24),ColumnCount=1,RowCount=7};
        var enabled=new CheckBox{Text="Enable quiet interface sounds",Checked=DossierPreferences.Current.Sounds,AutoSize=true};
        var typing=new CheckBox{Text="Soft typing ticks",Checked=DossierPreferences.Current.TypingSounds,AutoSize=true};
        var motion=new CheckBox{Text="Smooth interface animations",Checked=DossierPreferences.Current.Animations,AutoSize=true};
        var volume=new TrackBar{Minimum=0,Maximum=100,Value=(int)(DossierPreferences.Current.Volume*100),Dock=DockStyle.Fill,TickFrequency=10};
        root.Controls.Add(DossierStyle.Heading("MAKE IT FEEL RIGHT",17));root.Controls.Add(enabled);root.Controls.Add(typing);root.Controls.Add(motion);root.Controls.Add(new Label{Text="Sound level (synthesized effects stay quiet)",AutoSize=true});root.Controls.Add(volume);
        var save=new DossierButton{Text="Apply preferences",Dock=DockStyle.Fill,Height=40};root.Controls.Add(save);Controls.Add(root);DossierStyle.Apply(this);
        save.Click+=(_,__)=>{var prefs=DossierPreferences.Current;prefs.Sounds=enabled.Checked;prefs.TypingSounds=typing.Checked;prefs.Volume=volume.Value/100d;prefs.Animations=motion.Checked;prefs.QuietFeedbackVersion=1;prefs.Persist();DossierFeedback.Play("save");DialogResult=DialogResult.OK;};
        DossierDpi.Complete(this);
    }
}
