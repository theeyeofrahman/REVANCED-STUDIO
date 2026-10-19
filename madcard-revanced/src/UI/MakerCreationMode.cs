using System;
using System.Drawing;
using System.Windows.Forms;

namespace MadCardMaker;

public enum MakerCreationMode { Cancel, Assisted, Advanced }

public sealed class MakerCreationModeDialog : Form
{
    public MakerCreationMode Mode { get; private set; } = MakerCreationMode.Cancel;

    public MakerCreationModeDialog(string cardType)
    {
        string display = (cardType ?? "card").Replace('_', ' ');
        Text = "Choose how to create"; Size = new Size(660, 360); MinimumSize = new Size(580, 320);
        StartPosition = FormStartPosition.CenterParent; AutoScaleMode = AutoScaleMode.Dpi;
        var layout = new TableLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(24), ColumnCount = 1, RowCount = 4 };
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 42)); layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 64));
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100)); layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 48));
        layout.Controls.Add(new Label { Text = "Create a " + display, Dock = DockStyle.Fill, Font = DossierFonts.Create(DossierStyle.Theme.HeadingFont, 18, FontStyle.Bold), TextAlign = ContentAlignment.MiddleLeft }, 0, 0);
        layout.Controls.Add(new Label { Text = "Pick a guided setup or open every field in the full editor. You can still change everything after creation.", Dock = DockStyle.Fill }, 0, 1);
        var options = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 1, Padding = new Padding(0, 8, 0, 0) };
        options.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50)); options.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
        var assisted = new DossierButton { Text = "Assisted\r\nGuided choices and suggestions", Dock = DockStyle.Fill, Height = 94, Margin = new Padding(0, 0, 8, 0) };
        var advanced = new DossierButton { Text = "Advanced\r\nFull field editor", Dock = DockStyle.Fill, Height = 94, Margin = new Padding(8, 0, 0, 0) };
        assisted.Click += (_, __) => Choose(MakerCreationMode.Assisted); advanced.Click += (_, __) => Choose(MakerCreationMode.Advanced);
        options.Controls.Add(assisted, 0, 0); options.Controls.Add(advanced, 1, 0); layout.Controls.Add(options, 0, 2);
        var cancel = new DossierButton { Text = "Cancel", Width = 100, Height = 38, Anchor = AnchorStyles.Right };
        cancel.Click += (_, __) => Close(); layout.Controls.Add(cancel, 0, 3); Controls.Add(layout); CancelButton = cancel;
        DossierStyle.Apply(this);
    }

    void Choose(MakerCreationMode mode) { Mode = mode; DialogResult = DialogResult.OK; Close(); }
}
