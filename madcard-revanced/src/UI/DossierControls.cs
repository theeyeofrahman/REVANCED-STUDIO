using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace MadCardMaker;

internal static class DossierGeometry
{
    internal static GraphicsPath Rounded(Rectangle r, int radius)
    {
        float a=Math.Max(1,Math.Min(radius,Math.Min(r.Width,r.Height)/2f)),k=a*.18f;var path=new GraphicsPath();
        path.AddLine(r.Left+a,r.Top,r.Right-a,r.Top);path.AddBezier(r.Right-a,r.Top,r.Right-k,r.Top,r.Right,r.Top+k,r.Right,r.Top+a);
        path.AddLine(r.Right,r.Top+a,r.Right,r.Bottom-a);path.AddBezier(r.Right,r.Bottom-a,r.Right,r.Bottom-k,r.Right-k,r.Bottom,r.Right-a,r.Bottom);
        path.AddLine(r.Right-a,r.Bottom,r.Left+a,r.Bottom);path.AddBezier(r.Left+a,r.Bottom,r.Left+k,r.Bottom,r.Left,r.Bottom-k,r.Left,r.Bottom-a);
        path.AddLine(r.Left,r.Bottom-a,r.Left,r.Top+a);path.AddBezier(r.Left,r.Top+a,r.Left,r.Top+k,r.Left+k,r.Top,r.Left+a,r.Top);path.CloseFigure();return path;
    }
}

public sealed class DossierButton : Button
{
    private bool _hover;
    public DossierButton(){SetStyle(ControlStyles.UserPaint|ControlStyles.AllPaintingInWmPaint|ControlStyles.OptimizedDoubleBuffer|ControlStyles.ResizeRedraw,true);}
    protected override void OnMouseEnter(EventArgs e){_hover=true;Invalidate();base.OnMouseEnter(e);}
    protected override void OnMouseLeave(EventArgs e){_hover=false;Invalidate();base.OnMouseLeave(e);}
    protected override void OnPaint(PaintEventArgs e)
    {
        int P(int value)=>DossierDpi.Pixels(this,value);
        e.Graphics.Clear(Parent?.BackColor??DossierStyle.Surface);e.Graphics.SmoothingMode=SmoothingMode.AntiAlias;
        using(var path=DossierGeometry.Rounded(new Rectangle(1,1,Width-3,Height-3),P(12)))
        using(var fill=new SolidBrush(!Enabled?DossierStyle.Input:_hover?Color.FromArgb(Math.Min(255,BackColor.R+12),Math.Min(255,BackColor.G+7),Math.Min(255,BackColor.B+9)):BackColor))
        using(var line=new Pen(Focused?DossierStyle.Accent:DossierStyle.Line))
        {e.Graphics.FillPath(fill,path);e.Graphics.DrawPath(line,path);}
        TextRenderer.DrawText(e.Graphics,Text,Font,new Rectangle(P(8),P(4),Math.Max(1,Width-P(16)),Math.Max(1,Height-P(8))),Enabled?ForeColor:DossierStyle.Muted,TextFormatFlags.HorizontalCenter|TextFormatFlags.VerticalCenter|TextFormatFlags.WordBreak);
    }
}

public sealed class DossierGroupBox : GroupBox
{
    protected override void OnPaint(PaintEventArgs e)
    {
        e.Graphics.Clear(BackColor);e.Graphics.SmoothingMode=SmoothingMode.AntiAlias;
        using(var outline=DossierGeometry.Rounded(new Rectangle(1,10,Width-3,Height-12),10))
        using(var pen=new Pen(DossierStyle.Line))e.Graphics.DrawPath(pen,outline);
        var size=TextRenderer.MeasureText(Text,Font);
        using(var fill=new SolidBrush(BackColor))e.Graphics.FillRectangle(fill,12,0,size.Width+8,22);
        TextRenderer.DrawText(e.Graphics,Text,Font,new Point(16,0),DossierStyle.Muted);
    }
}

public sealed class DossierTabs : TabControl,IDossierDpiMetrics
{
    readonly Timer motion=new Timer{Interval=15};readonly System.Diagnostics.Stopwatch clock=new System.Diagnostics.Stopwatch();float indicator,start,target;bool positioned;
    public DossierTabs(){SelectedIndexChanged+=(_,__)=>MoveIndicator();motion.Tick+=(_,__)=>{indicator=start+(target-start)*(float)DossierMotion.Ease(clock.Elapsed.TotalMilliseconds/160);Invalidate();if(clock.ElapsedMilliseconds>=160)motion.Stop();};}
    void MoveIndicator(){if(!IsHandleCreated||SelectedIndex<0)return;target=GetTabRect(SelectedIndex).Left;if(!positioned||DossierMotion.SuppressForTests||!DossierPreferences.Current.Animations){positioned=true;indicator=target;motion.Stop();Invalidate();return;}start=indicator;clock.Restart();motion.Start();}
    protected override void Dispose(bool disposing){if(disposing)motion.Dispose();base.Dispose(disposing);}
    public void RefreshDpiMetrics(){ItemSize=new Size(DossierDpi.Pixels(this,Width>0?Math.Min(100,Math.Max(65,(Width-12)/Math.Max(1,TabPages.Count))):100),DossierDpi.Pixels(this,36));positioned=false;MoveIndicator();Invalidate();}
    protected override void WndProc(ref Message message)
    {
        base.WndProc(ref message);
        if(!IsHandleCreated||TabPages.Count==0||(message.Msg!=0xF&&message.Msg!=0x317))return;
        using(var g=message.Msg==0x317&&message.WParam!=IntPtr.Zero?Graphics.FromHdc(message.WParam):Graphics.FromHwnd(Handle))
        {
            var display=DisplayRectangle;
            using(var pen=new Pen(DossierStyle.Surface,5))g.DrawRectangle(pen,display.X-2,display.Y-2,display.Width+3,display.Height+3);
            var last=GetTabRect(TabPages.Count-1);
            using(var fill=new SolidBrush(DossierStyle.Background))g.FillRectangle(fill,last.Right+2,0,Math.Max(0,Width-last.Right-2),Math.Max(0,display.Top-2));
            if(SelectedIndex>=0){if(!positioned){indicator=GetTabRect(SelectedIndex).Left;positioned=true;}var selected=GetTabRect(SelectedIndex);int inset=DossierDpi.Pixels(this,14);using(var line=new Pen(DossierStyle.Accent,DossierDpi.Pixels(this,3)))g.DrawLine(line,indicator+inset,selected.Bottom-2,indicator+selected.Width-inset,selected.Bottom-2);}
        }
    }
}
