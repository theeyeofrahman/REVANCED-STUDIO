using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace MadCardMaker;

public static class DossierMotion
{
 public static bool SuppressForTests{get;set;}
 static readonly ConditionalWeakTable<Control,object> Wired=new ConditionalWeakTable<Control,object>();
 static readonly HashSet<ButtonMotion> Buttons=new HashSet<ButtonMotion>();
 static readonly HashSet<FormMotion> Forms=new HashSet<FormMotion>();
 static readonly Timer Clock=new Timer{Interval=16};
 static DossierMotion(){Clock.Tick+=(_,__)=>Tick();}
 public static double Ease(double progress){progress=Math.Max(0,Math.Min(1,progress));return 1-Math.Pow(1-progress,3);}
 public static Color Blend(Color from,Color to,double progress){double p=Ease(progress);return Color.FromArgb((int)Math.Round(from.R+(to.R-from.R)*p),(int)Math.Round(from.G+(to.G-from.G)*p),(int)Math.Round(from.B+(to.B-from.B)*p));}
 public static void Attach(Control root){if(Wired.TryGetValue(root,out _))return;Wired.Add(root,new object());if(root is Button button)new ButtonMotion(button);if(root is Form form)form.Shown+=(_,__)=>Fade(form);foreach(Control child in root.Controls)Attach(child);root.ControlAdded+=(_,e)=>Attach(e.Control);}

 static void Fade(Form form)
 {
  // Embedded workspace forms should appear immediately; changing their opacity repeatedly
  // forces expensive repaints and can make the whole editor feel choppy.
  if(SuppressForTests||!DossierPreferences.Current.Animations||!form.Enabled||!form.TopLevel)return;
  Forms.Add(new FormMotion(form));StartClock();
 }
 static void StartClock(){if(!Clock.Enabled&& (Buttons.Count>0||Forms.Count>0))Clock.Start();}
 static void StopClockIfIdle(){if(Buttons.Count==0&&Forms.Count==0)Clock.Stop();}
 static void Tick()
 {
  foreach(var motion in new List<ButtonMotion>(Buttons))if(motion.Step())Buttons.Remove(motion);
  foreach(var motion in new List<FormMotion>(Forms))if(motion.Step())Forms.Remove(motion);
  StopClockIfIdle();
 }

 sealed class FormMotion
 {
  readonly Form form;readonly Stopwatch elapsed=Stopwatch.StartNew();
  public FormMotion(Form form){this.form=form;form.Opacity=.72;form.Disposed+=(_,__)=>Forms.Remove(this);}
  public bool Step(){if(form.IsDisposed)return true;form.Opacity=.72+.28*Ease(elapsed.Elapsed.TotalMilliseconds/140);if(elapsed.ElapsedMilliseconds<140)return false;form.Opacity=1;return true;}
 }

 sealed class ButtonMotion
 {
  readonly Button button;readonly Stopwatch elapsed=new Stopwatch();Color original,start,target;bool inside,pressed,writing,active;
  public ButtonMotion(Button button)
  {
   this.button=button;original=button.BackColor;
   button.MouseEnter+=(_,__)=>{inside=true;Animate();};button.MouseLeave+=(_,__)=>{inside=pressed=false;Animate();};
   button.MouseDown+=(_,e)=>{if(e.Button==MouseButtons.Left){pressed=true;Animate();}};button.MouseUp+=(_,__)=>{pressed=false;Animate();};
   button.BackColorChanged+=(_,__)=>{if(!writing){original=button.BackColor;Cancel();}};button.Disposed+=(_,__)=>Cancel();
  }
  void Animate()
  {
   if(button.IsDisposed)return;
   start=button.BackColor;
   target=!inside?original:button.AccessibleName=="Close"?Color.FromArgb(185,39,48):pressed?Color.FromArgb(Math.Max(0,original.R-12),Math.Max(0,original.G-12),Math.Max(0,original.B-12)):Color.FromArgb(Math.Min(255,original.R+20),Math.Min(255,original.G+16),Math.Min(255,original.B+18));
   if(SuppressForTests||!DossierPreferences.Current.Animations){SetColor(target);return;}
   elapsed.Restart();active=true;Buttons.Add(this);StartClock();
  }
  public bool Step()
  {
   if(!active||button.IsDisposed)return true;
   SetColor(Blend(start,target,elapsed.Elapsed.TotalMilliseconds/150));
   if(elapsed.ElapsedMilliseconds<150)return false;
   SetColor(target);active=false;return true;
  }
  void SetColor(Color color){writing=true;button.BackColor=color;writing=false;}
  void Cancel(){active=false;Buttons.Remove(this);StopClockIfIdle();}
 }
}
