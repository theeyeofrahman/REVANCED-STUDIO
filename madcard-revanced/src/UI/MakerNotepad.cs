using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MadCardMaker;

public sealed class MakerNotepad : Form
{
    static readonly Color Canvas = Color.FromArgb(15, 15, 18);
    static readonly Color Panel = Color.FromArgb(25, 24, 28);
    static readonly Color Ink = Color.FromArgb(239, 233, 235);
    static readonly Color Muted = Color.FromArgb(168, 156, 161);
    static readonly Color Red = Color.FromArgb(211, 55, 70);
    static readonly Color RedHover = Color.FromArgb(83, 29, 39);
    static readonly Color Border = Color.FromArgb(74, 37, 45);

    readonly RichTextBox editor = new RichTextBox { Dock = DockStyle.Fill, BorderStyle = BorderStyle.None, DetectUrls = false, AcceptsTab = true, HideSelection = false, WordWrap = true, ScrollBars = RichTextBoxScrollBars.Vertical };
    readonly ComboBox fontFamily = new ComboBox { Width = 205, DropDownStyle = ComboBoxStyle.DropDownList, IntegralHeight = false, MaxDropDownItems = 18 };
    readonly ComboBox fontSize = new ComboBox { Width = 72, DropDownStyle = ComboBoxStyle.DropDownList };
    readonly Label status = new Label { Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft, Padding = new Padding(8, 0, 0, 0) };
    readonly ToolTip tips = new ToolTip();
    string currentPath;
    bool dirty, loading;

    public RichTextBox Editor => editor;

    public MakerNotepad()
    {
        Text = "Studio Notepad";
        Size = new Size(1180, 800);
        MinimumSize = new Size(760, 540);
        StartPosition = FormStartPosition.CenterParent;
        AutoScaleMode = AutoScaleMode.Dpi;
        AutoScaleDimensions = new SizeF(96, 96);
        BackColor = Canvas;
        ForeColor = Ink;
        Font = DossierFonts.Create(DossierTheme.Load().FontFamily, 10f);

        var layout = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 2, Padding = new Padding(12), BackColor = Canvas };
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));

        var toolbar = new FlowLayoutPanel { Dock = DockStyle.Fill, BackColor = Panel, Padding = new Padding(7, 5, 5, 3), WrapContents = false, AutoScroll = true };
        AddButton(toolbar, "New", "Start a blank note", NewDocument, 54);
        AddButton(toolbar, "Open", "Open an RTF or text note", OpenDocument, 58);
        AddButton(toolbar, "Save", "Save   Ctrl+S", Save, 58, true);
        AddButton(toolbar, "Save as", "Save a copy under another name", SaveAs, 72);
        AddSeparator(toolbar);

        fontFamily.Items.AddRange(new InstalledFontCollection().Families.Select(f => f.Name).Distinct(StringComparer.OrdinalIgnoreCase).OrderBy(f => f, StringComparer.CurrentCultureIgnoreCase).Cast<object>().ToArray());
        string preferred = DossierTheme.Load().FontFamily;
        if (!fontFamily.Items.Contains(preferred)) fontFamily.Items.Insert(0, preferred);
        fontFamily.SelectedItem = preferred;
        fontFamily.SelectionChangeCommitted += (_, __) => ApplyFontFamily();
        StyleCombo(fontFamily); toolbar.Controls.Add(fontFamily); tips.SetToolTip(fontFamily, "Choose a font for selected text or the next text you type.");

        for (int size = 8; size <= 72; size += 2) fontSize.Items.Add(size.ToString());
        fontSize.Items.Add("96"); fontSize.SelectedItem = "12";
        fontSize.SelectionChangeCommitted += (_, __) => ApplyFontSize();
        StyleCombo(fontSize); toolbar.Controls.Add(fontSize); tips.SetToolTip(fontSize, "Choose the text size.");

        AddSeparator(toolbar);
        AddButton(toolbar, "A‾", "Choose text color", ChooseTextColor, 42);
        AddButton(toolbar, "▰", "Choose highlight color", ChooseHighlightColor, 42);

        var formatting = new FlowLayoutPanel { Dock = DockStyle.Fill, BackColor = Panel, Padding = new Padding(7, 5, 5, 3), WrapContents = false, AutoScroll = true };
        AddButton(formatting, "B", "Bold   Ctrl+B", () => ToggleStyle(FontStyle.Bold), 42, false, FontStyle.Bold);
        AddButton(formatting, "I", "Italic   Ctrl+I", () => ToggleStyle(FontStyle.Italic), 42, false, FontStyle.Italic);
        AddButton(formatting, "U", "Underline   Ctrl+U", () => ToggleStyle(FontStyle.Underline), 42, false, FontStyle.Underline);
        AddButton(formatting, "S̶", "Strikethrough", () => ToggleStyle(FontStyle.Strikeout), 42);
        AddSeparator(formatting);
        AddButton(formatting, "• List", "Toggle bullet list", ToggleBullets, 70);
        AddSeparator(formatting);
        AddButton(formatting, "Left", "Align left", () => Align(HorizontalAlignment.Left), 58);
        AddButton(formatting, "Center", "Center text", () => Align(HorizontalAlignment.Center), 68);
        AddButton(formatting, "Right", "Align right", () => Align(HorizontalAlignment.Right), 62);
        AddSeparator(formatting);
        AddButton(formatting, "Undo", "Undo   Ctrl+Z", () => { if (editor.CanUndo) editor.Undo(); }, 58);
        AddButton(formatting, "Redo", "Redo   Ctrl+Y", () => { if (editor.CanRedo) editor.Redo(); }, 58);
        AddButton(formatting, "Time", "Insert current date and time", InsertDateTime, 54);

        var editorFrame = new Panel { Dock = DockStyle.Fill, Padding = new Padding(15), BackColor = Color.FromArgb(9, 9, 11), Margin = new Padding(0, 8, 0, 8) };
        editor.BackColor = Color.FromArgb(9, 9, 11); editor.ForeColor = Ink; editor.Font = DossierFonts.Create(preferred, 12f);
        editorFrame.Controls.Add(editor);

        var statusPanel = new Panel { Dock = DockStyle.Fill, BackColor = Panel };
        var accent = new Panel { Dock = DockStyle.Left, Width = 3, BackColor = Red }; statusPanel.Controls.Add(accent);
        status.ForeColor = Muted; status.Font = Font; statusPanel.Controls.Add(status);
        layout.Controls.Add(toolbar, 0, 0); layout.Controls.Add(editorFrame, 0, 1); layout.Controls.Add(statusPanel, 0, 2);
        var stacked = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 3, BackColor = Canvas, Padding = new Padding(0) };
        stacked.RowStyles.Add(new RowStyle(SizeType.Absolute, 46)); stacked.RowStyles.Add(new RowStyle(SizeType.Absolute, 46)); stacked.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        stacked.Controls.Add(toolbar, 0, 0); stacked.Controls.Add(formatting, 0, 1); stacked.Controls.Add(editorFrame, 0, 2);
        layout.Controls.Add(stacked, 0, 0); layout.Controls.Add(statusPanel, 0, 1);
        Controls.Add(layout);

        editor.SelectionChanged += (_, __) => SyncFormatChoices();
        editor.TextChanged += (_, __) => { if (!loading) { dirty = true; RefreshTitleAndStatus(); } };
        FormClosing += (_, e) => { if (!ConfirmSaveChanges()) e.Cancel = true; };
        RefreshTitleAndStatus();
    }

    void AddButton(FlowLayoutPanel row, string text, string tip, Action action, int width, bool primary = false, FontStyle style = FontStyle.Regular)
    {
        var button = new NotepadButton { Text = text, Width = width, Height = 34, Margin = new Padding(2, 0, 2, 0), Font = DossierFonts.Create(Font.FontFamily.Name, 9.5f, style), Primary = primary };
        button.Click += (_, __) => { try { action(); } catch (Exception ex) { DossierDialog.Show(this, "The note action failed.\r\n" + ex.Message, "Studio Notepad"); } }; row.Controls.Add(button); tips.SetToolTip(button, tip);
    }

    static void AddSeparator(FlowLayoutPanel row) => row.Controls.Add(new Panel { Width = 1, Height = 26, BackColor = Border, Margin = new Padding(7, 4, 7, 0) });

    void StyleCombo(ComboBox combo)
    {
        combo.Height = 34; combo.BackColor = Color.FromArgb(12, 12, 15); combo.ForeColor = Ink; combo.FlatStyle = FlatStyle.Flat;
        combo.Font = DossierFonts.Create(Font.FontFamily.Name, 9.5f); combo.Margin = new Padding(3, 0, 3, 0);
    }

    void SyncFormatChoices()
    {
        var selection = editor.SelectionFont ?? editor.Font;
        fontFamily.SelectedItem = fontFamily.Items.Cast<object>().FirstOrDefault(x => string.Equals(x.ToString(), selection.FontFamily.Name, StringComparison.OrdinalIgnoreCase));
        string size = ((int)Math.Round(selection.Size)).ToString(); if (!fontSize.Items.Contains(size)) fontSize.Items.Add(size); fontSize.SelectedItem = size;
        UpdateStatus();
    }

    Font SelectionFont => editor.SelectionFont ?? editor.Font;
    void ApplyFontFamily()
    {
        if (fontFamily.SelectedItem == null) return;
        var old = SelectionFont; editor.SelectionFont = new Font(fontFamily.SelectedItem.ToString(), old.Size, old.Style); MarkFormatted();
    }
    void ApplyFontSize()
    {
        if (!float.TryParse(fontSize.Text, out var size) || size < 6 || size > 200) return;
        var old = SelectionFont; editor.SelectionFont = new Font(old.FontFamily, size, old.Style); MarkFormatted();
    }
    void ToggleStyle(FontStyle style)
    {
        var old = SelectionFont; var next = old.Style.HasFlag(style) ? old.Style & ~style : old.Style | style;
        editor.SelectionFont = new Font(old.FontFamily, old.Size, next); MarkFormatted();
    }
    void ChooseTextColor()
    {
        using (var picker = new ColorDialog { Color = editor.SelectionColor, FullOpen = true, AnyColor = true })
            if (picker.ShowDialog(this) == DialogResult.OK) { editor.SelectionColor = picker.Color; MarkFormatted(); }
    }
    void ChooseHighlightColor()
    {
        using (var picker = new ColorDialog { Color = editor.SelectionBackColor, FullOpen = true, AnyColor = true })
            if (picker.ShowDialog(this) == DialogResult.OK) { editor.SelectionBackColor = picker.Color; MarkFormatted(); }
    }
    void ToggleBullets()
    {
        editor.SelectionBullet = !editor.SelectionBullet;
        if (editor.SelectionBullet) { editor.SelectionIndent = 22; editor.SelectionHangingIndent = 6; }
        MarkFormatted();
    }
    void Align(HorizontalAlignment alignment) { editor.SelectionAlignment = alignment; MarkFormatted(); }
    void InsertDateTime() { editor.SelectedText = DateTime.Now.ToString("yyyy-MM-dd HH:mm"); editor.Focus(); }
    void MarkFormatted() { dirty = true; RefreshTitleAndStatus(); editor.Focus(); }

    void NewDocument()
    {
        if (!ConfirmSaveChanges()) return;
        loading = true; editor.Clear(); editor.Font = DossierFonts.Create(DossierTheme.Load().FontFamily, 12f); loading = false;
        currentPath = null; dirty = false; RefreshTitleAndStatus(); editor.Focus();
    }

    void OpenDocument()
    {
        if (!ConfirmSaveChanges()) return;
        using (var picker = new OpenFileDialog { Title = "Open a Studio note", Filter = "Rich Text Format (*.rtf)|*.rtf|Text files (*.txt;*.md)|*.txt;*.md|All files (*.*)|*.*", CheckFileExists = true, Multiselect = false })
        {
            if (picker.ShowDialog(this) != DialogResult.OK) return;
            try
            {
                loading = true;
                if (string.Equals(Path.GetExtension(picker.FileName), ".rtf", StringComparison.OrdinalIgnoreCase)) editor.LoadFile(picker.FileName, RichTextBoxStreamType.RichText);
                else { editor.Text = File.ReadAllText(picker.FileName); editor.Select(0, 0); editor.SelectionFont = editor.Font; }
                loading = false; currentPath = picker.FileName; dirty = false; RefreshTitleAndStatus(); editor.Focus();
            }
            catch (Exception ex) { loading = false; DossierDialog.Show(this, "Could not open that note.\r\n" + ex.Message, "Studio Notepad"); }
        }
    }

    void Save()
    {
        if (string.IsNullOrEmpty(currentPath)) { SaveAs(); return; }
        SaveTo(currentPath);
    }
    void SaveAs()
    {
        using (var picker = new SaveFileDialog { Title = "Save Studio note", Filter = "Rich Text Format (*.rtf)|*.rtf|Plain text (*.txt)|*.txt", DefaultExt = "rtf", AddExtension = true, FileName = string.IsNullOrEmpty(currentPath) ? "Studio Note.rtf" : Path.GetFileName(currentPath), OverwritePrompt = true })
            if (picker.ShowDialog(this) == DialogResult.OK) SaveTo(picker.FileName);
    }

    void SaveTo(string path)
    {
        string fullPath = Path.GetFullPath(path); string directory = Path.GetDirectoryName(fullPath); Directory.CreateDirectory(directory);
        string temporary = fullPath + ".notepad-tmp"; string backup = fullPath + ".bak";
        if (File.Exists(backup)) backup = fullPath + "." + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".bak";
        try
        {
            if (string.Equals(Path.GetExtension(fullPath), ".rtf", StringComparison.OrdinalIgnoreCase)) editor.SaveFile(temporary, RichTextBoxStreamType.RichText);
            else File.WriteAllText(temporary, editor.Text, new UTF8Encoding(true));
            if (File.Exists(fullPath)) File.Replace(temporary, fullPath, backup, true); else File.Move(temporary, fullPath);
            currentPath = fullPath; dirty = false; DossierFeedback.Play("save"); RefreshTitleAndStatus();
        }
        catch
        {
            if (File.Exists(temporary)) File.Delete(temporary);
            throw;
        }
    }

    bool ConfirmSaveChanges()
    {
        if (!dirty) return true;
        var answer = DossierDialog.Show(this, "Save changes to this note before continuing?", "Unsaved note", MessageBoxButtons.YesNoCancel);
        if (answer == DialogResult.Cancel) return false;
        if (answer == DialogResult.No) return true;
        try { Save(); return !dirty; }
        catch (Exception ex) { DossierDialog.Show(this, "The note could not be saved.\r\n" + ex.Message, "Studio Notepad"); return false; }
    }

    void RefreshTitleAndStatus()
    {
        string label = string.IsNullOrEmpty(currentPath) ? "Studio Notepad" : "Studio Notepad · " + Path.GetFileName(currentPath);
        Text = label + (dirty ? " *" : ""); UpdateStatus();
    }
    void UpdateStatus() => status.Text = editor.TextLength + " characters   ·   " + editor.Lines.Length + " lines   ·   " + (string.IsNullOrEmpty(currentPath) ? "Not saved" : Path.GetFileName(currentPath)) + (dirty ? "   ·   Edited" : "");

    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
        if (keyData == (Keys.Control | Keys.S)) { Run(Save); return true; }
        if (keyData == (Keys.Control | Keys.Shift | Keys.S)) { Run(SaveAs); return true; }
        if (keyData == (Keys.Control | Keys.O)) { Run(OpenDocument); return true; }
        if (keyData == (Keys.Control | Keys.N)) { Run(NewDocument); return true; }
        if (keyData == (Keys.Control | Keys.B)) { Run(() => ToggleStyle(FontStyle.Bold)); return true; }
        if (keyData == (Keys.Control | Keys.I)) { Run(() => ToggleStyle(FontStyle.Italic)); return true; }
        if (keyData == (Keys.Control | Keys.U)) { Run(() => ToggleStyle(FontStyle.Underline)); return true; }
        return base.ProcessCmdKey(ref msg, keyData);
    }

    void Run(Action action) { try { action(); } catch (Exception ex) { DossierDialog.Show(this, "The note action failed.\r\n" + ex.Message, "Studio Notepad"); } }

    sealed class NotepadButton : Button
    {
        bool hovered;
        public bool Primary { get; set; }
        public NotepadButton()
        {
            FlatStyle = FlatStyle.Flat; FlatAppearance.BorderSize = 0; BackColor = Panel; ForeColor = Ink; Cursor = Cursors.Hand; UseVisualStyleBackColor = false;
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.ResizeRedraw, true);
        }
        protected override void OnMouseEnter(EventArgs e) { hovered = true; Invalidate(); base.OnMouseEnter(e); }
        protected override void OnMouseLeave(EventArgs e) { hovered = false; Invalidate(); base.OnMouseLeave(e); }
        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            var bounds = Rectangle.Inflate(ClientRectangle, -1, -1);
            var color = Primary ? (hovered ? Color.FromArgb(231, 71, 85) : Red) : hovered ? RedHover : Panel;
            using (var path = Rounded(bounds, 8)) using (var fill = new SolidBrush(color)) e.Graphics.FillPath(fill, path);
            using (var path = Rounded(bounds, 8)) using (var pen = new Pen(Primary ? Red : Border, 1)) e.Graphics.DrawPath(pen, path);
            TextRenderer.DrawText(e.Graphics, Text, Font, bounds, Ink, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.NoPrefix);
        }
        static GraphicsPath Rounded(Rectangle rectangle, int radius)
        {
            int diameter = radius * 2; var path = new GraphicsPath();
            path.AddArc(rectangle.Left, rectangle.Top, diameter, diameter, 180, 90); path.AddArc(rectangle.Right - diameter, rectangle.Top, diameter, diameter, 270, 90);
            path.AddArc(rectangle.Right - diameter, rectangle.Bottom - diameter, diameter, diameter, 0, 90); path.AddArc(rectangle.Left, rectangle.Bottom - diameter, diameter, diameter, 90, 90); path.CloseFigure(); return path;
        }
    }
}
