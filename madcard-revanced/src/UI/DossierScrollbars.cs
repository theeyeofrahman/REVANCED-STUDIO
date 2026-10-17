using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;
namespace MadCardMaker;

// Repaint the real scrollbar: Windows still owns its hit targets, drag tracking,
// wheel handling and accessibility range. No substitute scrolling model is used.
public static class DossierScrollbars
{
 static readonly ConditionalWeakTable<Control,Skin> Skins=new ConditionalWeakTable<Control,Skin>();
 public static void Attach(Control control)
 {
  if(SystemInformation.HighContrast||Skins.TryGetValue(control,out _))return;
  if(!(control is TextBoxBase||control is TreeView||control is ListBox||control is ListView||control is ScrollableControl||control is ComboBox||control is ScrollBar))return;
  Skins.Add(control,new Skin(control));
 }
 public static bool IsAttached(Control control)=>Skins.TryGetValue(control,out _);
 [StructLayout(LayoutKind.Sequential)]struct Rect{public int Left,Top,Right,Bottom;}
 [StructLayout(LayoutKind.Sequential)]struct BarInfo{public int Size;public Rect Bounds;public int Arrow,ThumbStart,ThumbEnd,Reserved;[MarshalAs(UnmanagedType.ByValArray,SizeConst=6)]public int[] States;}
 [DllImport("user32.dll")]static extern bool GetScrollBarInfo(IntPtr hwnd,int which,ref BarInfo info);
 [DllImport("user32.dll")]static extern bool GetWindowRect(IntPtr hwnd,out Rect rect);
 [DllImport("user32.dll")]static extern IntPtr GetWindowDC(IntPtr hwnd);
 [DllImport("user32.dll")]static extern int ReleaseDC(IntPtr hwnd,IntPtr hdc);
 sealed class Skin:NativeWindow
 {
  readonly Control owner;Popup popup;bool painting;
  public Skin(Control owner){this.owner=owner;owner.HandleCreated+=Created;owner.HandleDestroyed+=Destroyed;owner.Disposed+=Disposed;if(owner.IsHandleCreated)AssignHandle(owner.Handle);}
  void Created(object sender,EventArgs e){if(Handle==IntPtr.Zero)AssignHandle(owner.Handle);}
  void Destroyed(object sender,EventArgs e){if(Handle!=IntPtr.Zero)ReleaseHandle();popup?.Release();popup=null;}
  void Disposed(object sender,EventArgs e){Destroyed(sender,e);owner.HandleCreated-=Created;owner.HandleDestroyed-=Destroyed;owner.Disposed-=Disposed;}
  protected override void WndProc(ref Message m)
  {
   base.WndProc(ref m);
   if(m.Msg==0x134&&owner is ComboBox&&m.LParam!=IntPtr.Zero){if(popup==null||popup.Handle!=m.LParam){popup?.Release();popup=new Popup(m.LParam,owner);}}
   if(painting||Handle==IntPtr.Zero)return;
   if(NeedsPaint(m.Msg)){painting=true;try{Paint(Handle,owner,m.Msg==0x317?m.WParam:IntPtr.Zero);}finally{painting=false;}}
  }
 }
 sealed class Popup:NativeWindow
 {
  readonly Control owner;bool painting;public Popup(IntPtr handle,Control owner){this.owner=owner;AssignHandle(handle);}public void Release(){if(Handle!=IntPtr.Zero)ReleaseHandle();}
  protected override void WndProc(ref Message m){base.WndProc(ref m);if(painting||Handle==IntPtr.Zero||!NeedsPaint(m.Msg))return;painting=true;try{Paint(Handle,owner,m.Msg==0x317?m.WParam:IntPtr.Zero);}finally{painting=false;}}
 }
 static bool NeedsPaint(int msg)=>msg==0x85||msg==0xf||msg==0x317||msg==5||msg==0x114||msg==0x115||msg==0x20a||msg==0x20e||msg==0xa0||msg==0xa1||msg==0xa2||msg==0x113||msg==0x200||msg==0x2e0||msg==0x1a;
 static void Paint(IntPtr handle,Control owner,IntPtr printDc)
 {
  if(owner.IsDisposed||SystemInformation.HighContrast||!GetWindowRect(handle,out var window))return;
  IntPtr dc=printDc!=IntPtr.Zero?printDc:GetWindowDC(handle);if(dc==IntPtr.Zero)return;
  try{using(var g=Graphics.FromHdc(dc)){
   g.SmoothingMode=SmoothingMode.AntiAlias;
   Rectangle vertical=Rectangle.Empty,horizontal=Rectangle.Empty;
   if(owner is ScrollBar)Draw(g,handle,owner,-4,owner is HScrollBar,window,out _);
   else{Draw(g,handle,owner,-5,false,window,out vertical);Draw(g,handle,owner,-6,true,window,out horizontal);}
   if(!vertical.IsEmpty&&!horizontal.IsEmpty)using(var fill=new SolidBrush(DossierStyle.Surface))g.FillRectangle(fill,new Rectangle(vertical.Left,horizontal.Top,vertical.Width,horizontal.Height));
  }}finally{if(printDc==IntPtr.Zero)ReleaseDC(handle,dc);}
 }
 static void Draw(Graphics g,IntPtr handle,Control owner,int which,bool horizontal,Rect window,out Rectangle bounds)
 {
  bounds=Rectangle.Empty;var info=new BarInfo{Size=Marshal.SizeOf(typeof(BarInfo)),States=new int[6]};if(!GetScrollBarInfo(handle,which,ref info)||(info.States[0]&(0x8000|0x10000))!=0)return;
  bounds=new Rectangle(info.Bounds.Left-window.Left,info.Bounds.Top-window.Top,info.Bounds.Right-info.Bounds.Left,info.Bounds.Bottom-info.Bounds.Top);if(bounds.Width<=0||bounds.Height<=0){bounds=Rectangle.Empty;return;}
  using(var track=new SolidBrush(DossierStyle.Input))g.FillRectangle(track,bounds);
  int inset=Math.Max(2,DossierDpi.Pixels(owner,3));int length=horizontal?bounds.Width:bounds.Height;int arrow=Math.Min(info.Arrow,length/2);
  int top=Math.Max(arrow,Math.Min(length-arrow,info.ThumbStart)),bottom=Math.Max(top,Math.Min(length-arrow,info.ThumbEnd));
  if(bottom>top&&(info.States[3]&(1|0x8000))==0){var thumb=horizontal?new Rectangle(bounds.Left+top,bounds.Top+inset,bottom-top,Math.Max(1,bounds.Height-2*inset)):new Rectangle(bounds.Left+inset,bounds.Top+top,Math.Max(1,bounds.Width-2*inset),bottom-top);
   if(thumb.Width>1&&thumb.Height>1)using(var shape=DossierGeometry.Rounded(thumb,Math.Min(thumb.Width,thumb.Height)/2))using(var brush=new SolidBrush((info.States[3]&8)!=0?DossierStyle.Text:DossierStyle.Accent))g.FillPath(brush,shape);
  }
  if(arrow>0){Arrow(g,bounds,horizontal,true,arrow,info.States[1]);Arrow(g,bounds,horizontal,false,arrow,info.States[5]);}
 }
 static void Arrow(Graphics g,Rectangle r,bool horizontal,bool first,int size,int state)
 {
  float x=horizontal?(first?r.Left+size/2f:r.Right-size/2f):r.Left+r.Width/2f;
  float y=horizontal?r.Top+r.Height/2f:(first?r.Top+size/2f:r.Bottom-size/2f);float d=Math.Max(2,Math.Min(r.Width,r.Height)/5f);
  PointF[] points=horizontal?new[]{new PointF(x+(first?-d:d),y),new PointF(x+(first?d:-d),y-d),new PointF(x+(first?d:-d),y+d)}:new[]{new PointF(x,y+(first?-d:d)),new PointF(x-d,y+(first?d:-d)),new PointF(x+d,y+(first?d:-d))};
  using(var brush=new SolidBrush((state&1)!=0?DossierStyle.Line:DossierStyle.Muted))g.FillPolygon(brush,points);
 }
}
