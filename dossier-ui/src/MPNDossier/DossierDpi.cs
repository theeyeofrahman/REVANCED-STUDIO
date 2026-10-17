using System;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
namespace MPNDossier;
public interface IDossierDpiMetrics{void RefreshDpiMetrics();}
public static class DossierDpi
{
 sealed class WindowState{public Size LogicalMinimum;public bool Ready;}
 static readonly ConditionalWeakTable<Form,WindowState> Watched=new ConditionalWeakTable<Form,WindowState>();
 public static int Pixels(Control control,int value)=>(int)Math.Round(value*control.DeviceDpi/96d);
 public static void Initialize(Form form){form.SuspendLayout();form.AutoScaleMode=AutoScaleMode.Dpi;form.AutoScaleDimensions=new SizeF(96,96);Watch(form);}
 public static void Complete(Form form){form.ResumeLayout(true);}
 public static void Watch(Form form)
 {
  if(Watched.TryGetValue(form,out _))return;var state=new WindowState();Watched.Add(form,state);DossierWindow.Attach(form);
  form.Shown+=(_,__)=>{if(!state.Ready){state.LogicalMinimum=new Size((int)Math.Round(form.MinimumSize.Width*96d/form.DeviceDpi),(int)Math.Round(form.MinimumSize.Height*96d/form.DeviceDpi));state.Ready=true;}Refresh(form);Fit(form,state);};
  form.DpiChanged+=(_,__)=>{if(form.IsHandleCreated&&!form.IsDisposed)form.BeginInvoke((Action)(()=>{if(form.IsDisposed)return;Refresh(form);Fit(form,state);form.Invalidate(true);}));};
 }
 static void Fit(Form form,WindowState state)
 {
  if(!form.TopLevel||!state.Ready||form.WindowState!=FormWindowState.Normal)return;
  var available=Screen.FromControl(form).WorkingArea;int gap=Pixels(form,12);
  var limit=new Size(Math.Max(200,available.Width-gap),Math.Max(200,available.Height-gap));
  form.MinimumSize=new Size(Math.Min(Pixels(form,state.LogicalMinimum.Width),limit.Width),Math.Min(Pixels(form,state.LogicalMinimum.Height),limit.Height));
  form.Size=new Size(Math.Min(form.Width,limit.Width),Math.Min(form.Height,limit.Height));
 }
 public static void Refresh(Control root)
 {
  if(root is TableLayoutPanel table&&table.RowStyles.Count==0&&table.ColumnCount>0&&table.Controls.Count<=table.ColumnCount){table.RowCount=1;table.RowStyles.Add(new RowStyle(SizeType.Percent,100));}
  if(root is IDossierDpiMetrics metrics)metrics.RefreshDpiMetrics();
  else if(root is TreeView tree)tree.ItemHeight=Math.Max(tree.Font.Height+Pixels(tree,6),Pixels(tree,27));
  else if(root is ListBox list)list.ItemHeight=Math.Max(list.Font.Height+Pixels(list,4),Pixels(list,25));
  else if(root is ComboBox combo&&combo.DrawMode!=DrawMode.Normal)combo.ItemHeight=Math.Max(combo.Font.Height+Pixels(combo,4),Pixels(combo,22));
  foreach(Control child in root.Controls)Refresh(child);
 }
}
