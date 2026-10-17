using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
namespace MPNDossier;
public static class DossierMotion
{
 public static bool SuppressForTests{get;set;}
 static readonly ConditionalWeakTable<Control,object> Wired=new ConditionalWeakTable<Control,object>();
 public static double Ease(double progress){progress=Math.Max(0,Math.Min(1,progress));return 1-Math.Pow(1-progress,3);}
 public static Color Blend(Color from,Color to,double progress){double p=Ease(progress);return Color.FromArgb((int)Math.Round(from.R+(to.R-from.R)*p),(int)Math.Round(from.G+(to.G-from.G)*p),(int)Math.Round(from.B+(to.B-from.B)*p));}
 public static void Attach(Control root){if(Wired.TryGetValue(root,out _))return;Wired.Add(root,new object());if(root is Button button)new ButtonMotion(button);if(root is Form form){form.Shown+=(_,__)=>Fade(form);}
  foreach(Control child in root.Controls)Attach(child);root.ControlAdded+=(_,e)=>Attach(e.Control);
 }
 static void Fade(Form form){if(SuppressForTests||!DossierPreferences.Current.Animations||!form.Enabled)return;var timer=new Timer{Interval=15};var clock=Stopwatch.StartNew();form.Opacity=.08;timer.Tick+=(_,__)=>{if(form.IsDisposed){timer.Dispose();return;}form.Opacity=.08+.92*Ease(clock.Elapsed.TotalMilliseconds/150);if(clock.ElapsedMilliseconds>=150){form.Opacity=1;timer.Dispose();}};form.Disposed+=(_,__)=>timer.Dispose();timer.Start();}
 sealed class ButtonMotion
 {
  readonly Button button;readonly Timer timer=new Timer{Interval=15};readonly Stopwatch clock=new Stopwatch();Color original,start,target;bool inside,pressed,writing;
  public ButtonMotion(Button button){this.button=button;original=button.BackColor;button.MouseEnter+=(_,__)=>{inside=true;Animate();};button.MouseLeave+=(_,__)=>{inside=pressed=false;Animate();};button.MouseDown+=(_,e)=>{if(e.Button==MouseButtons.Left){pressed=true;Animate();}};button.MouseUp+=(_,__)=>{pressed=false;Animate();};button.BackColorChanged+=(_,__)=>{if(!writing){original=button.BackColor;timer.Stop();}};button.Disposed+=(_,__)=>timer.Dispose();timer.Tick+=(_,__)=>{if(button.IsDisposed){timer.Dispose();return;}writing=true;button.BackColor=Blend(start,target,clock.Elapsed.TotalMilliseconds/120);writing=false;if(clock.ElapsedMilliseconds>=120)timer.Stop();};}
  void Animate(){if(button.IsDisposed)return;start=button.BackColor;target=!inside?original:button.AccessibleName=="Close"?Color.FromArgb(185,39,48):pressed?Color.FromArgb(Math.Max(0,original.R-12),Math.Max(0,original.G-12),Math.Max(0,original.B-12)):Color.FromArgb(Math.Min(255,original.R+20),Math.Min(255,original.G+16),Math.Min(255,original.B+18));if(SuppressForTests||!DossierPreferences.Current.Animations){writing=true;button.BackColor=target;writing=false;return;}clock.Restart();timer.Start();}
 }
}
