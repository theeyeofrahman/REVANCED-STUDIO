using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
namespace MadCardMaker;

internal static class DossierComboSkin
{
    static readonly ConditionalWeakTable<ComboBox,Skin> Skins=new ConditionalWeakTable<ComboBox,Skin>();
    internal static void Attach(ComboBox combo){if(!Skins.TryGetValue(combo,out _))Skins.Add(combo,new Skin(combo));}
    sealed class Skin:NativeWindow
    {
        readonly ComboBox combo;bool painting;
        internal Skin(ComboBox combo){this.combo=combo;combo.HandleCreated+=(_,__)=>AssignHandle(combo.Handle);combo.HandleDestroyed+=(_,__)=>ReleaseHandle();combo.Disposed+=(_,__)=>ReleaseHandle();combo.SelectedIndexChanged+=(_,__)=>combo.Invalidate();combo.GotFocus+=(_,__)=>combo.Invalidate();combo.LostFocus+=(_,__)=>combo.Invalidate();if(combo.IsHandleCreated)AssignHandle(combo.Handle);}
        protected override void WndProc(ref Message message)
        {
            base.WndProc(ref message);
            if(painting||combo.IsDisposed||!combo.IsHandleCreated||SystemInformation.HighContrast)return;
            if(message.Msg!=0xF&&message.Msg!=0x317&&message.Msg!=0x318)return;
            painting=true;try{
                using(var g=(message.Msg==0x317||message.Msg==0x318)&&message.WParam!=IntPtr.Zero?Graphics.FromHdc(message.WParam):Graphics.FromHwnd(combo.Handle)){
                    int arrow=DossierDpi.Pixels(combo,30),pad=DossierDpi.Pixels(combo,8);var bounds=combo.ClientRectangle;if(bounds.Width<arrow||bounds.Height<6)return;
                    using(var fill=new SolidBrush(DossierStyle.Input))g.FillRectangle(fill,combo.DropDownStyle==ComboBoxStyle.DropDownList?bounds:new Rectangle(bounds.Right-arrow,0,arrow,bounds.Height));
                    g.SmoothingMode=SmoothingMode.AntiAlias;
                    using(var path=DossierGeometry.Rounded(new Rectangle(0,0,bounds.Width-1,bounds.Height-1),DossierDpi.Pixels(combo,6)))using(var line=new Pen(combo.Focused?DossierStyle.Accent:DossierStyle.Line))g.DrawPath(line,path);
                    if(combo.DropDownStyle==ComboBoxStyle.DropDownList)TextRenderer.DrawText(g,combo.GetItemText(combo.SelectedItem),combo.Font,new Rectangle(pad,0,bounds.Width-arrow-pad,bounds.Height),combo.Enabled?DossierStyle.Text:DossierStyle.Muted,TextFormatFlags.Left|TextFormatFlags.VerticalCenter|TextFormatFlags.EndEllipsis|TextFormatFlags.NoPrefix);
                    float x=bounds.Right-arrow/2f,y=bounds.Height/2f;using(var pen=new Pen(DossierStyle.Muted,1.5f))g.DrawLines(pen,new[]{new PointF(x-4,y-2),new PointF(x,y+2),new PointF(x+4,y-2)});
                }
            }finally{painting=false;}
        }
    }
}
