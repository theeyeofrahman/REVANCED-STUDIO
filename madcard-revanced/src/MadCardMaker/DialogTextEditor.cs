using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

namespace MadCardMaker;

[DesignerGenerated]
public class DialogTextEditor : Form
{
	private IContainer components;

	[field: AccessedThroughProperty("TagPanel")]
	internal virtual Panel TagPanel
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ColorTag")]
	internal virtual Button ColorTag
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = ColorTag_Click;
			Button button = field;
			if (button != null)
			{
				button.Click -= value2;
			}
			field = value;
			button = field;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("ItalicTag")]
	internal virtual Button ItalicTag
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = ItalicTag_Click;
			Button button = field;
			if (button != null)
			{
				button.Click -= value2;
			}
			field = value;
			button = field;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("BoldTag")]
	internal virtual Button BoldTag
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = BoldTag_Click;
			Button button = field;
			if (button != null)
			{
				button.Click -= value2;
			}
			field = value;
			button = field;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("SizeTag")]
	internal virtual Button SizeTag
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = SizeTag_Click;
			Button button = field;
			if (button != null)
			{
				button.Click -= value2;
			}
			field = value;
			button = field;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("ColorWheel")]
	internal virtual ColorDialog ColorWheel
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Done")]
	internal virtual Button Done
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = Done_Click;
			Button button = field;
			if (button != null)
			{
				button.Click -= value2;
			}
			field = value;
			button = field;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("AllText")]
	internal virtual RichTextBox AllText
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("MenuStrip")]
	internal virtual MenuStrip MenuStrip
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("FileToolStripMenuItem")]
	internal virtual ToolStripMenuItem FileToolStripMenuItem
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("BoldToolStripMenuItem")]
	internal virtual ToolStripMenuItem BoldToolStripMenuItem
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = BoldToolStripMenuItem_Click;
			ToolStripMenuItem toolStripMenuItem = field;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			field = value;
			toolStripMenuItem = field;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("ItalicizeToolStripMenuItem")]
	internal virtual ToolStripMenuItem ItalicizeToolStripMenuItem
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = ItalicizeToolStripMenuItem_Click;
			ToolStripMenuItem toolStripMenuItem = field;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			field = value;
			toolStripMenuItem = field;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("ColorToolStripMenuItem")]
	internal virtual ToolStripMenuItem ColorToolStripMenuItem
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = ColorToolStripMenuItem_Click;
			ToolStripMenuItem toolStripMenuItem = field;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			field = value;
			toolStripMenuItem = field;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("SizeToolStripMenuItem")]
	internal virtual ToolStripMenuItem SizeToolStripMenuItem
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = SizeToolStripMenuItem_Click;
			ToolStripMenuItem toolStripMenuItem = field;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			field = value;
			toolStripMenuItem = field;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("SaveToolStripMenuItem")]
	internal virtual ToolStripMenuItem SaveToolStripMenuItem
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = SaveToolStripMenuItem_Click;
			ToolStripMenuItem toolStripMenuItem = field;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			field = value;
			toolStripMenuItem = field;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	public string IncludeText { get; set; }

	public DialogTextEditor()
	{
		Load += DialogTextEditor_Load;
		IncludeText = "";
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
		this.TagPanel = new System.Windows.Forms.Panel();
		this.BoldTag = new System.Windows.Forms.Button();
		this.ItalicTag = new System.Windows.Forms.Button();
		this.ColorTag = new System.Windows.Forms.Button();
		this.ColorWheel = new System.Windows.Forms.ColorDialog();
		this.SizeTag = new System.Windows.Forms.Button();
		this.Done = new System.Windows.Forms.Button();
		this.AllText = new System.Windows.Forms.RichTextBox();
		this.MenuStrip = new System.Windows.Forms.MenuStrip();
		this.FileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.BoldToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.ItalicizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.ColorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.SizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.SaveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.TagPanel.SuspendLayout();
		this.MenuStrip.SuspendLayout();
		base.SuspendLayout();
		this.TagPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.TagPanel.Controls.Add(this.Done);
		this.TagPanel.Controls.Add(this.SizeTag);
		this.TagPanel.Controls.Add(this.ColorTag);
		this.TagPanel.Controls.Add(this.ItalicTag);
		this.TagPanel.Controls.Add(this.BoldTag);
		this.TagPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.TagPanel.Location = new System.Drawing.Point(0, 298);
		this.TagPanel.Name = "TagPanel";
		this.TagPanel.Size = new System.Drawing.Size(592, 55);
		this.TagPanel.TabIndex = 0;
		this.BoldTag.BackColor = System.Drawing.Color.Black;
		this.BoldTag.Dock = System.Windows.Forms.DockStyle.Left;
		this.BoldTag.Font = new System.Drawing.Font("Impact", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.BoldTag.ForeColor = System.Drawing.Color.White;
		this.BoldTag.Location = new System.Drawing.Point(0, 0);
		this.BoldTag.Name = "BoldTag";
		this.BoldTag.Size = new System.Drawing.Size(67, 51);
		this.BoldTag.TabIndex = 0;
		this.BoldTag.Text = "Bold";
		this.BoldTag.UseVisualStyleBackColor = false;
		this.ItalicTag.BackColor = System.Drawing.Color.Black;
		this.ItalicTag.Dock = System.Windows.Forms.DockStyle.Left;
		this.ItalicTag.Font = new System.Drawing.Font("Impact", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.ItalicTag.ForeColor = System.Drawing.Color.White;
		this.ItalicTag.Location = new System.Drawing.Point(67, 0);
		this.ItalicTag.Name = "ItalicTag";
		this.ItalicTag.Size = new System.Drawing.Size(67, 51);
		this.ItalicTag.TabIndex = 1;
		this.ItalicTag.Text = "Italic";
		this.ItalicTag.UseVisualStyleBackColor = false;
		this.ColorTag.BackColor = System.Drawing.Color.Black;
		this.ColorTag.Dock = System.Windows.Forms.DockStyle.Left;
		this.ColorTag.Font = new System.Drawing.Font("Impact", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.ColorTag.ForeColor = System.Drawing.Color.White;
		this.ColorTag.Location = new System.Drawing.Point(134, 0);
		this.ColorTag.Name = "ColorTag";
		this.ColorTag.Size = new System.Drawing.Size(67, 51);
		this.ColorTag.TabIndex = 2;
		this.ColorTag.Text = "Color";
		this.ColorTag.UseVisualStyleBackColor = false;
		this.SizeTag.BackColor = System.Drawing.Color.Black;
		this.SizeTag.Dock = System.Windows.Forms.DockStyle.Left;
		this.SizeTag.Font = new System.Drawing.Font("Impact", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.SizeTag.ForeColor = System.Drawing.Color.White;
		this.SizeTag.Location = new System.Drawing.Point(201, 0);
		this.SizeTag.Name = "SizeTag";
		this.SizeTag.Size = new System.Drawing.Size(67, 51);
		this.SizeTag.TabIndex = 3;
		this.SizeTag.Text = "Size";
		this.SizeTag.UseVisualStyleBackColor = false;
		this.Done.BackColor = System.Drawing.Color.Black;
		this.Done.Dock = System.Windows.Forms.DockStyle.Right;
		this.Done.Font = new System.Drawing.Font("Impact", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.Done.ForeColor = System.Drawing.Color.PaleGreen;
		this.Done.Location = new System.Drawing.Point(521, 0);
		this.Done.Name = "Done";
		this.Done.Size = new System.Drawing.Size(67, 51);
		this.Done.TabIndex = 4;
		this.Done.Text = "Done";
		this.Done.UseVisualStyleBackColor = false;
		this.AllText.BackColor = System.Drawing.Color.Black;
		this.AllText.Dock = System.Windows.Forms.DockStyle.Fill;
		this.AllText.Font = new System.Drawing.Font("Impact", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.AllText.ForeColor = System.Drawing.Color.White;
		this.AllText.Location = new System.Drawing.Point(0, 24);
		this.AllText.Name = "AllText";
		this.AllText.Size = new System.Drawing.Size(592, 274);
		this.AllText.TabIndex = 1;
		this.AllText.Text = "Info";
		this.MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[1] { this.FileToolStripMenuItem });
		this.MenuStrip.Location = new System.Drawing.Point(0, 0);
		this.MenuStrip.Name = "MenuStrip";
		this.MenuStrip.Size = new System.Drawing.Size(592, 24);
		this.MenuStrip.TabIndex = 2;
		this.MenuStrip.Text = "MenuStrip1";
		this.FileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[5] { this.BoldToolStripMenuItem, this.ItalicizeToolStripMenuItem, this.ColorToolStripMenuItem, this.SizeToolStripMenuItem, this.SaveToolStripMenuItem });
		this.FileToolStripMenuItem.Name = "FileToolStripMenuItem";
		this.FileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
		this.FileToolStripMenuItem.Text = "File";
		this.BoldToolStripMenuItem.Name = "BoldToolStripMenuItem";
		this.BoldToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.B | System.Windows.Forms.Keys.Control;
		this.BoldToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
		this.BoldToolStripMenuItem.Text = "Bold";
		this.ItalicizeToolStripMenuItem.Name = "ItalicizeToolStripMenuItem";
		this.ItalicizeToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.I | System.Windows.Forms.Keys.Control;
		this.ItalicizeToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
		this.ItalicizeToolStripMenuItem.Text = "Italicize";
		this.ColorToolStripMenuItem.Name = "ColorToolStripMenuItem";
		this.ColorToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F | System.Windows.Forms.Keys.Control;
		this.ColorToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
		this.ColorToolStripMenuItem.Text = "Color";
		this.SizeToolStripMenuItem.Name = "SizeToolStripMenuItem";
		this.SizeToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.R | System.Windows.Forms.Keys.Control;
		this.SizeToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
		this.SizeToolStripMenuItem.Text = "Size";
		this.SaveToolStripMenuItem.Name = "SaveToolStripMenuItem";
		this.SaveToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.S | System.Windows.Forms.Keys.Control;
		this.SaveToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
		this.SaveToolStripMenuItem.Text = "Save";
		base.AutoScaleDimensions = new System.Drawing.SizeF(96f, 96f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
		this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
		base.ClientSize = new System.Drawing.Size(592, 353);
		base.Controls.Add(this.AllText);
		base.Controls.Add(this.TagPanel);
		base.Controls.Add(this.MenuStrip);
		base.MainMenuStrip = this.MenuStrip;
		base.Name = "DialogTextEditor";
		this.Text = "Edit Text";
		this.TagPanel.ResumeLayout(false);
		this.MenuStrip.ResumeLayout(false);
		this.MenuStrip.PerformLayout();
		base.ResumeLayout(false);
		base.PerformLayout();
	}

	private void DialogTextEditor_Load(object sender, EventArgs e)
	{
		AllText.Text = IncludeText;
		ModColors.ApplyMenuColors(this);
	}

	private void Add(string tag, string value = "")
	{
		int startIndex = Math.Max(0, AllText.SelectionStart);
		int endIndex = ((AllText.SelectionLength > 0) ? checked(AllText.SelectionStart + AllText.SelectionLength) : AllText.Text.Length);
		AddTag(startIndex, endIndex, tag, value);
	}

	private void AddTag(int startIndex, int endIndex, string tag, string value = "")
	{
		string value2 = "<" + tag + ((value.Length > 0) ? ("=" + value) : "") + ">";
		string value3 = "</" + tag + ">";
		string text = AllText.Text;
		text = text.Insert(endIndex, value3);
		text = text.Insert(startIndex, value2);
		AllText.Text = text;
	}

	private void BoldTag_Click(object sender, EventArgs e)
	{
		BoldToolStripMenuItem.PerformClick();
	}

	private void ItalicTag_Click(object sender, EventArgs e)
	{
		ItalicizeToolStripMenuItem.PerformClick();
	}

	private void ColorTag_Click(object sender, EventArgs e)
	{
		ColorToolStripMenuItem.PerformClick();
	}

	private void SizeTag_Click(object sender, EventArgs e)
	{
		SizeToolStripMenuItem.PerformClick();
	}

	private void Done_Click(object sender, EventArgs e)
	{
		SaveToolStripMenuItem.PerformClick();
	}

	private void BoldToolStripMenuItem_Click(object sender, EventArgs e)
	{
		Add("b");
	}

	private void ItalicizeToolStripMenuItem_Click(object sender, EventArgs e)
	{
		Add("i");
	}

	private void ColorToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (ColorWheel.ShowDialog() != DialogResult.Cancel)
		{
			Add("color", ColorTranslator.ToHtml(ColorWheel.Color));
		}
	}

	private void SizeToolStripMenuItem_Click(object sender, EventArgs e)
	{
		Add("size", "10");
	}

	private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
	{
		IncludeText = AllText.Text.Replace("\r\n", "\\n");
		DialogResult = DialogResult.OK;
	}
}
