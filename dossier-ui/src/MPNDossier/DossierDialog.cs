using System;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.VisualBasic;
namespace MPNDossier;
public static class DossierDialog
{
 public static bool PreventDialogsForTests;
 public static DialogResult Show(string message,string title="MPN Dossier REVANCED",MessageBoxButtons buttons=MessageBoxButtons.OK,MessageBoxIcon icon=MessageBoxIcon.None)=>Show(null,message,title,buttons,icon);
 public static DialogResult Show(IWin32Window owner,string message,string title="MPN Dossier REVANCED",MessageBoxButtons buttons=MessageBoxButtons.OK,MessageBoxIcon icon=MessageBoxIcon.None){if(PreventDialogsForTests)throw new InvalidOperationException("Unexpected dialog during background checks: "+message);using(var dialog=new Prompt(message,title,buttons))return owner==null?dialog.ShowDialog():dialog.ShowDialog(owner);}
 public static MsgBoxResult MsgBox(object prompt,MsgBoxStyle style=MsgBoxStyle.OkOnly,object title=null){int code=(int)style&7;var buttons=code==4?MessageBoxButtons.YesNo:code==3?MessageBoxButtons.YesNoCancel:code==1?MessageBoxButtons.OKCancel:code==2?MessageBoxButtons.AbortRetryIgnore:code==5?MessageBoxButtons.RetryCancel:MessageBoxButtons.OK;return (MsgBoxResult)(int)Show(Convert.ToString(prompt),title==null?"MPN Dossier REVANCED":Convert.ToString(title),buttons);}
 public static Form CreatePrompt(string message,string title,MessageBoxButtons buttons)=>new Prompt(message,title,buttons);
 sealed class Prompt:Form
 {
  public Prompt(string message,string title,MessageBoxButtons buttons){DossierDpi.Initialize(this);Text=title;Size=new Size(620,360);MinimumSize=new Size(480,300);StartPosition=FormStartPosition.CenterParent;MaximizeBox=false;MinimizeBox=false;FormBorderStyle=FormBorderStyle.FixedDialog;BackColor=Color.FromArgb(17,17,20);ForeColor=Color.FromArgb(235,230,230);Icon=DossierBrand.Icon;Font=DossierFonts.Create(DossierTheme.Load().FontFamily,10);
   var root=new TableLayoutPanel{Dock=DockStyle.Fill,Padding=new Padding(22),ColumnCount=1,RowCount=2};root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,100));root.RowStyles.Add(new RowStyle(SizeType.Percent,100));root.RowStyles.Add(new RowStyle(SizeType.Absolute,48));var text=new TextBox{Dock=DockStyle.Fill,Text=message,ReadOnly=true,Multiline=true,ScrollBars=ScrollBars.Vertical,BorderStyle=BorderStyle.None,BackColor=BackColor,ForeColor=ForeColor,TabStop=false};root.Controls.Add(text,0,0);var actions=new FlowLayoutPanel{Dock=DockStyle.Fill,FlowDirection=FlowDirection.RightToLeft};root.Controls.Add(actions,0,1);Controls.Add(root);
   DialogResult[] results=buttons==MessageBoxButtons.YesNoCancel?new[]{DialogResult.Cancel,DialogResult.No,DialogResult.Yes}:buttons==MessageBoxButtons.YesNo?new[]{DialogResult.No,DialogResult.Yes}:buttons==MessageBoxButtons.OKCancel?new[]{DialogResult.Cancel,DialogResult.OK}:buttons==MessageBoxButtons.RetryCancel?new[]{DialogResult.Cancel,DialogResult.Retry}:buttons==MessageBoxButtons.AbortRetryIgnore?new[]{DialogResult.Ignore,DialogResult.Retry,DialogResult.Abort}:new[]{DialogResult.OK};
   foreach(var result in results){var button=new Button{Text=result.ToString(),DialogResult=result,Width=104,Height=36,FlatStyle=FlatStyle.Flat,BackColor=Color.FromArgb(113,27,36),ForeColor=ForeColor};button.FlatAppearance.BorderColor=Color.FromArgb(185,45,55);actions.Controls.Add(button);if(result==DialogResult.Cancel||result==DialogResult.No||results.Length==1){CancelButton=button;AcceptButton=button;ActiveControl=button;}if(results.Length==1)AcceptButton=button;}
   FormClosing+=(_,__)=>{if(DialogResult==DialogResult.None)DialogResult=buttons==MessageBoxButtons.YesNo?DialogResult.No:buttons==MessageBoxButtons.OK?DialogResult.OK:DialogResult.Cancel;};DossierDpi.Complete(this);
  }
 }
}
