using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace MadCardMaker;

internal static class DossierStyle
{
    internal static readonly DossierTheme Theme=DossierTheme.Load();
    internal static readonly Color Background = Theme.ColorFor("Background");
    internal static readonly Color Surface = Theme.ColorFor("Surface");
    internal static readonly Color Input = Theme.ColorFor("Input");
    internal static readonly Color Text = Theme.ColorFor("Text");
    internal static readonly Color Muted = Theme.ColorFor("Muted");
    internal static readonly Color Accent = Theme.ColorFor("Accent");
    internal static readonly Color Cyan = Theme.ColorFor("Cyan");
    internal static readonly Color Line = Theme.ColorFor("Line");

    static readonly System.Runtime.CompilerServices.ConditionalWeakTable<Control,object> Styled=new System.Runtime.CompilerServices.ConditionalWeakTable<Control,object>();
    internal static void Apply(Control root)
    {
        if(Styled.TryGetValue(root,out _))return;Styled.Add(root,new object());
        root.Font = DossierFonts.Create(Equals(root.Tag,"Heading")?Theme.HeadingFont:Theme.FontFamily, (Equals(root.Tag,"Heading")?18f:10f)*Theme.InterfaceScale,Equals(root.Tag,"Heading")?FontStyle.Bold:FontStyle.Regular);
        root.ForeColor = Text;
        root.BackColor = root is Form ? Background : Surface;
        if (root is Button button)
        {
            button.FlatStyle = FlatStyle.Flat; button.FlatAppearance.BorderColor = Line;
            button.BackColor = Surface; button.ForeColor = Text;
            button.Cursor = Cursors.Hand; button.Padding = new Padding(6, 2, 6, 2);
            button.SizeChanged += (_, _) => Round(button, DossierDpi.Pixels(button,12)); Round(button,DossierDpi.Pixels(button,12));
        }
        else if (root is TextBoxBase text)
        {
            text.BackColor = Input; text.ForeColor = Text;
            text.BorderStyle = BorderStyle.FixedSingle;
            if (text is RichTextBox rich) { rich.DetectUrls = false; rich.ScrollBars = RichTextBoxScrollBars.Vertical; rich.BorderStyle = BorderStyle.None; }
        }
        else if (root is TreeView tree)
        {
            tree.BackColor = Input; tree.ForeColor = Text; tree.BorderStyle = BorderStyle.None;
            tree.ItemHeight = 27; tree.HideSelection = false; tree.FullRowSelect = true;
            tree.LineColor = Line; tree.Indent = 20;
        }
        else if (root is ListBox list)
        {
            list.BackColor = Input; list.ForeColor = Text; list.BorderStyle = BorderStyle.None;
            list.IntegralHeight = false; list.ItemHeight = Math.Max(30,list.Font.Height+10);
            if(list.DrawMode==DrawMode.Normal){list.DrawMode=DrawMode.OwnerDrawFixed;list.DrawItem+=(_,e)=>{if(e.Index<0)return;using(var background=new SolidBrush(Input))e.Graphics.FillRectangle(background,e.Bounds);bool selected=(e.State&DrawItemState.Selected)!=0;var area=Rectangle.Inflate(e.Bounds,-3,-2);if(area.Width>0&&area.Height>0)using(var shape=DossierGeometry.Rounded(area,8))using(var fill=new SolidBrush(selected?Color.FromArgb(76,38,50):Input))e.Graphics.FillPath(fill,shape);var textArea=new Rectangle(e.Bounds.Left+10,e.Bounds.Top,Math.Max(1,e.Bounds.Width-20),e.Bounds.Height);TextRenderer.DrawText(e.Graphics,list.GetItemText(list.Items[e.Index]),list.Font,textArea,Text,TextFormatFlags.Left|TextFormatFlags.VerticalCenter|TextFormatFlags.NoPrefix);if(selected&&list.Focused)using(var pen=new Pen(Accent))e.Graphics.DrawLine(pen,e.Bounds.Left+4,e.Bounds.Top+7,e.Bounds.Left+4,e.Bounds.Bottom-7);};}
        }
        else if (root is ComboBox combo)
        {
            combo.BackColor = Input; combo.ForeColor = Text; combo.FlatStyle = FlatStyle.Flat;
            combo.IntegralHeight = false; combo.MaxDropDownItems = 18; DossierComboSkin.Attach(combo);
            combo.DrawMode = DrawMode.OwnerDrawFixed;
            combo.DrawItem += (_, e) =>
            {
                using(var fill=new SolidBrush((e.State&DrawItemState.Selected)!=0?Surface:Input))e.Graphics.FillRectangle(fill,e.Bounds);
                if(e.Index>=0)TextRenderer.DrawText(e.Graphics,combo.GetItemText(combo.Items[e.Index]),combo.Font,e.Bounds,Text,TextFormatFlags.Left|TextFormatFlags.VerticalCenter);
                e.DrawFocusRectangle();
            };
        }
        else if (root is PictureBox picture) { picture.BackColor = Input; picture.SizeMode = PictureBoxSizeMode.Zoom; }
        else if (root is TabControl tabs)
        {
            tabs.DrawMode = TabDrawMode.OwnerDrawFixed; tabs.ItemSize = new Size(100, 36); tabs.SizeMode = TabSizeMode.Fixed;
            tabs.DrawItem += (_, e) =>
            {
                var selected = e.Index == tabs.SelectedIndex;
                using(var shape=DossierGeometry.Rounded(Rectangle.Inflate(e.Bounds,-3,-3),DossierDpi.Pixels(tabs,10)))using(var fill=new SolidBrush(selected ? Color.FromArgb(72,36,47) : Surface))e.Graphics.FillPath(fill,shape);
                TextRenderer.DrawText(e.Graphics, tabs.TabPages[e.Index].Text, tabs.Font, e.Bounds, selected ? Text : Muted, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                if (selected&&!(tabs is DossierTabs)) using (var pen = new Pen(Accent, 3)) e.Graphics.DrawLine(pen, e.Bounds.Left + 14, e.Bounds.Bottom - 2, e.Bounds.Right - 14, e.Bounds.Bottom - 2);
            };
        }
        else if (root is ToolStrip strip)
        {
            strip.Renderer = new ToolStripProfessionalRenderer(new DossierColors()); strip.ForeColor = Text;
            foreach (ToolStripItem item in strip.Items) StyleItem(item);
        }
        if(root is TextBoxBase||root is TreeView||root is ListBox||root is ListView||root is PictureBox){root.SizeChanged+=(_,__)=>Round(root,DossierDpi.Pixels(root,root is TextBoxBase?5:10));Round(root,DossierDpi.Pixels(root,root is TextBoxBase?5:10));}
        DossierScrollbars.Attach(root);
        foreach (Control child in root.Controls) Apply(child);
        if(root is Form form){if(Theme.InterfaceScale!=1f){form.SuspendLayout();form.Scale(new SizeF(Theme.InterfaceScale,Theme.InterfaceScale));form.ResumeLayout(true);}form.Icon=DossierBrand.Icon;form.ShowIcon=true;DossierDpi.Watch(form);DossierFeedback.Attach(root);form.KeyPreview=true;form.KeyDown+=(_,e)=>{if(e.KeyCode==Keys.F1){MakerHelp.Open(form);e.SuppressKeyPress=true;}};}
    }

    private static void StyleItem(ToolStripItem item)
    {
        item.ForeColor = Text; item.Font = DossierFonts.Create(Theme.FontFamily, 10f*Theme.InterfaceScale);
        if (item is ToolStripDropDownItem menu) foreach (ToolStripItem child in menu.DropDownItems) StyleItem(child);
    }

    private static void Round(Control control, int radius)
    {
        if (control.Width < radius * 2 || control.Height < radius * 2) return;
        using (var path = new GraphicsPath())
        {
            using(var rounded=DossierGeometry.Rounded(new Rectangle(0,0,control.Width,control.Height),radius))path.AddPath(rounded,false);
            var old = control.Region; control.Region = new Region(path); old?.Dispose();
        }
    }

    internal static Label Heading(string text, float size = 11) => new Label { Text = text, Font = DossierFonts.Create(Theme.FontFamily, size, FontStyle.Bold), ForeColor = Text, AutoSize = true, BackColor = Color.Transparent, Margin = new Padding(0, 0, 0, 8) };

    private sealed class DossierColors : ProfessionalColorTable
    {
        public override Color ToolStripDropDownBackground => Surface;
        public override Color MenuItemSelected => Color.FromArgb(48, 63, 82);
        public override Color MenuItemBorder => Accent;
        public override Color MenuBorder => Line;
        public override Color ImageMarginGradientBegin => Surface;
        public override Color ImageMarginGradientMiddle => Surface;
        public override Color ImageMarginGradientEnd => Surface;
        public override Color MenuItemPressedGradientBegin => Surface;
        public override Color MenuItemPressedGradientEnd => Surface;
    }
}
