using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using M = MadCardMaker;
namespace RevancedStudio;

public sealed class MusicSlider : Control
{
    int value;
    bool dragging;
    readonly Color accent = ColorTranslator.FromHtml(M.DossierTheme.Load().Accent);
    public int Maximum { get; set; } = 100;
    public int Value { get => value; set { int next = Math.Max(0, Math.Min(Maximum, value)); if (this.value == next) return; this.value = next; Invalidate(); ValueChanged?.Invoke(this, EventArgs.Empty); } }
    public event EventHandler ValueChanged, UserChanged, DragStarted;
    public MusicSlider() { SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.Selectable, true); TabStop = true; Cursor = Cursors.Hand; Height = 44; AccessibleRole = AccessibleRole.Slider; }
    void At(int x) { double pad=10*DeviceDpi/96d; Value=(int)Math.Round(Math.Max(0,Math.Min(1,(x-pad)/Math.Max(1,Width-2*pad)))*Maximum); }
    protected override void OnMouseDown(MouseEventArgs e) { base.OnMouseDown(e); if (e.Button != MouseButtons.Left) return; Focus(); dragging = true; Capture = true; DragStarted?.Invoke(this, EventArgs.Empty); At(e.X); }
    protected override void OnMouseMove(MouseEventArgs e) { base.OnMouseMove(e); if (dragging) At(e.X); }
    protected override void OnMouseUp(MouseEventArgs e) { base.OnMouseUp(e); if (!dragging || e.Button != MouseButtons.Left) return; At(e.X); dragging = false; Capture = false; UserChanged?.Invoke(this, EventArgs.Empty); }
    protected override void OnMouseCaptureChanged(EventArgs e) { base.OnMouseCaptureChanged(e); if (!Capture && dragging) { dragging = false; UserChanged?.Invoke(this, EventArgs.Empty); } }
    protected override bool IsInputKey(Keys keyData) => keyData == Keys.Left || keyData == Keys.Right || keyData == Keys.Home || keyData == Keys.End || base.IsInputKey(keyData);
    protected override void OnKeyDown(KeyEventArgs e)
    {
        base.OnKeyDown(e); int step = Math.Max(1, Maximum / 50);
        if (e.KeyCode == Keys.Left) Value -= step; else if (e.KeyCode == Keys.Right) Value += step; else if (e.KeyCode == Keys.Home) Value = 0; else if (e.KeyCode == Keys.End) Value = Maximum; else return;
        e.Handled = e.SuppressKeyPress = true; UserChanged?.Invoke(this, EventArgs.Empty);
    }
    protected override void OnGotFocus(EventArgs e) { base.OnGotFocus(e); Invalidate(); }
    protected override void OnLostFocus(EventArgs e) { base.OnLostFocus(e); Invalidate(); }
    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e); e.Graphics.SmoothingMode = SmoothingMode.AntiAlias; if (Width < 24 || Height < 12) return;

        float dpi=DeviceDpi/96f,pad=10*dpi;
        float y = Height / 2f, x = pad + (Width - 2*pad) * Value / Math.Max(1, Maximum);
        using (var line = new Pen(Color.FromArgb(72, 62, 70), 6*dpi) { StartCap = LineCap.Round, EndCap = LineCap.Round }) e.Graphics.DrawLine(line, pad, y, Width - pad, y);
        using (var fill = new Pen(accent, 6*dpi) { StartCap = LineCap.Round, EndCap = LineCap.Round }) if (x > pad) e.Graphics.DrawLine(fill, pad, y, x, y);
        using (var path = SpectrumView.Squircle(new RectangleF(x - 6*dpi, y - 9*dpi, 12*dpi, 18*dpi), 5*dpi))
        using (var brush = new SolidBrush(Focused ? Color.FromArgb(245, 210, 205) : accent)) e.Graphics.FillPath(brush, path);
    }
}
