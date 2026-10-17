using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

namespace MPNDossier;

[DesignerGenerated]
public class FormWave : Form
{
	private IContainer components;

	[AccessedThroughProperty("WaveViewer")]
	private TreeView _WaveViewer;

	internal virtual TreeView WaveViewer
	{
		get
		{
			return _WaveViewer;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			_WaveViewer = value;
		}
	}

	public FormWave()
	{
		InitializeComponent();
		DossierStyle.Apply(this);
	}

	[DebuggerNonUserCode]
	protected override void Dispose(bool disposing)
	{
		try
		{
			if (disposing && components != null)
			{
				components.Dispose();
			}
		}
		finally
		{
			base.Dispose(disposing);
		}
	}

	[System.Diagnostics.DebuggerStepThrough]
	private void InitializeComponent()
	{
		this.WaveViewer = new System.Windows.Forms.TreeView();
		this.SuspendLayout();
		this.WaveViewer.BackColor = System.Drawing.Color.Black;
		this.WaveViewer.Dock = System.Windows.Forms.DockStyle.Fill;
		this.WaveViewer.Font = new System.Drawing.Font("Courier New", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.WaveViewer.ForeColor = System.Drawing.Color.White;
		System.Windows.Forms.TreeView waveViewer = this.WaveViewer;
		System.Drawing.Point location = new System.Drawing.Point(0, 0);
		waveViewer.Location = location;
		this.WaveViewer.Name = "WaveViewer";
		this.WaveViewer.RightToLeft = System.Windows.Forms.RightToLeft.No;
		System.Windows.Forms.TreeView waveViewer2 = this.WaveViewer;
		System.Drawing.Size size = new System.Drawing.Size(451, 342);
		waveViewer2.Size = size;
		this.WaveViewer.TabIndex = 26;
		System.Drawing.SizeF sizeF = new System.Drawing.SizeF(6f, 13f);
		this.AutoScaleDimensions = sizeF;
		this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
		size = new System.Drawing.Size(451, 342);
		this.ClientSize = size;
		this.Controls.Add(this.WaveViewer);
		this.Name = "FormWave";
		this.Text = "Wave Viewer";
		this.ResumeLayout(false);
	}
}
