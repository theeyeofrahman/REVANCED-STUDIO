using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

namespace MadCardMaker;

[DesignerGenerated]
public class DialogModInfo : Form
{
	private IContainer components;

	private Keys[] RestrictChar;

	[field: AccessedThroughProperty("FolderName")]
	internal virtual TextBox FolderName
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			KeyEventHandler value2 = Folder_KeyDown;
			EventHandler value3 = Folder_TextChanged;
			TextBox textBox = field;
			if (textBox != null)
			{
				textBox.KeyDown -= value2;
				textBox.TextChanged -= value3;
			}
			field = value;
			textBox = field;
			if (textBox != null)
			{
				textBox.KeyDown += value2;
				textBox.TextChanged += value3;
			}
		}
	}

	[field: AccessedThroughProperty("ModName")]
	internal virtual TextBox ModName
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			KeyEventHandler value2 = Name_KeyDown;
			TextBox textBox = field;
			if (textBox != null)
			{
				textBox.KeyDown -= value2;
			}
			field = value;
			textBox = field;
			if (textBox != null)
			{
				textBox.KeyDown += value2;
			}
		}
	}

	[field: AccessedThroughProperty("NewMod")]
	internal virtual Button NewMod
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = NewMod_Click;
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

	[field: AccessedThroughProperty("Author")]
	internal virtual TextBox Author
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			KeyEventHandler value2 = Author_KeyDown;
			TextBox textBox = field;
			if (textBox != null)
			{
				textBox.KeyDown -= value2;
			}
			field = value;
			textBox = field;
			if (textBox != null)
			{
				textBox.KeyDown += value2;
			}
		}
	}

	[field: AccessedThroughProperty("ModTags")]
	internal virtual TextBox ModTags
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			KeyEventHandler value2 = Tags_KeyDown;
			TextBox textBox = field;
			if (textBox != null)
			{
				textBox.KeyDown -= value2;
			}
			field = value;
			textBox = field;
			if (textBox != null)
			{
				textBox.KeyDown += value2;
			}
		}
	}

	[field: AccessedThroughProperty("ModDesc")]
	internal virtual RichTextBox ModDesc
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	public string DefaultName { get; set; }

	public string DefaultFolder { get; set; }

	public string DefaultDesc { get; set; }

	public string DefaultAuthor { get; set; }

	public string DefaultTags { get; set; }

	public DialogModInfo()
	{
		Load += DialogModInfo_Load;
		RestrictChar = new Keys[9]
		{
			Keys.OemBackslash,
			Keys.Oemcomma,
			Keys.OemPeriod,
			Keys.OemQuestion,
			Keys.OemQuotes,
			Keys.OemSemicolon,
			Keys.Divide,
			Keys.Oemcomma,
			Keys.OemPipe
		};
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
		this.FolderName = new System.Windows.Forms.TextBox();
		this.ModName = new System.Windows.Forms.TextBox();
		this.NewMod = new System.Windows.Forms.Button();
		this.Author = new System.Windows.Forms.TextBox();
		this.ModTags = new System.Windows.Forms.TextBox();
		this.ModDesc = new System.Windows.Forms.RichTextBox();
		base.SuspendLayout();
		this.FolderName.BackColor = System.Drawing.Color.Black;
		this.FolderName.Dock = System.Windows.Forms.DockStyle.Top;
		this.FolderName.ForeColor = System.Drawing.Color.White;
		this.FolderName.Location = new System.Drawing.Point(0, 0);
		this.FolderName.Name = "FolderName";
		this.FolderName.Size = new System.Drawing.Size(485, 31);
		this.FolderName.TabIndex = 1;
		this.FolderName.Text = "Folder Name";
		this.ModName.BackColor = System.Drawing.Color.Black;
		this.ModName.Dock = System.Windows.Forms.DockStyle.Top;
		this.ModName.ForeColor = System.Drawing.Color.White;
		this.ModName.Location = new System.Drawing.Point(0, 31);
		this.ModName.Name = "ModName";
		this.ModName.Size = new System.Drawing.Size(485, 31);
		this.ModName.TabIndex = 2;
		this.ModName.Text = "Mod Name";
		this.NewMod.BackColor = System.Drawing.Color.FromArgb(64, 64, 64);
		this.NewMod.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.NewMod.Location = new System.Drawing.Point(0, 254);
		this.NewMod.Name = "NewMod";
		this.NewMod.Size = new System.Drawing.Size(485, 36);
		this.NewMod.TabIndex = 4;
		this.NewMod.Text = "Mod it up!";
		this.NewMod.UseVisualStyleBackColor = false;
		this.Author.BackColor = System.Drawing.Color.Black;
		this.Author.Dock = System.Windows.Forms.DockStyle.Top;
		this.Author.ForeColor = System.Drawing.Color.White;
		this.Author.Location = new System.Drawing.Point(0, 62);
		this.Author.Name = "Author";
		this.Author.Size = new System.Drawing.Size(485, 31);
		this.Author.TabIndex = 5;
		this.Author.Text = "Author(s)";
		this.ModTags.BackColor = System.Drawing.Color.Black;
		this.ModTags.Dock = System.Windows.Forms.DockStyle.Top;
		this.ModTags.ForeColor = System.Drawing.Color.White;
		this.ModTags.Location = new System.Drawing.Point(0, 93);
		this.ModTags.Name = "ModTags";
		this.ModTags.Size = new System.Drawing.Size(485, 31);
		this.ModTags.TabIndex = 6;
		this.ModTags.Text = "Tag1, Tag2, Tag3";
		this.ModDesc.BackColor = System.Drawing.Color.Black;
		this.ModDesc.Dock = System.Windows.Forms.DockStyle.Fill;
		this.ModDesc.ForeColor = System.Drawing.Color.White;
		this.ModDesc.Location = new System.Drawing.Point(0, 124);
		this.ModDesc.Name = "ModDesc";
		this.ModDesc.Size = new System.Drawing.Size(485, 130);
		this.ModDesc.TabIndex = 7;
		this.ModDesc.Text = "Description";
		base.AutoScaleDimensions = new System.Drawing.SizeF(96f, 96f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
		this.BackColor = System.Drawing.Color.DimGray;
		base.ClientSize = new System.Drawing.Size(485, 290);
		base.Controls.Add(this.ModDesc);
		base.Controls.Add(this.ModTags);
		base.Controls.Add(this.Author);
		base.Controls.Add(this.NewMod);
		base.Controls.Add(this.ModName);
		base.Controls.Add(this.FolderName);
		this.Font = new System.Drawing.Font("Impact", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.ForeColor = System.Drawing.Color.White;
		base.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
		base.Name = "DialogModInfo";
		this.Text = "Mod Info";
		base.ResumeLayout(false);
		base.PerformLayout();
	}

	private void DialogModInfo_Load(object sender, EventArgs e)
	{
		ModColors.ApplyMenuColors(this);
		FolderName.Text = DefaultFolder;
		ModName.Text = DefaultName;
		ModDesc.Text = DefaultDesc;
		Author.Text = DefaultAuthor;
		ModTags.Text = DefaultTags;
		FolderName.Select();
	}

	private void NewMod_Click(object sender, EventArgs e)
	{
		DialogResult = DialogResult.OK;
	}

	private void Folder_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			ModName.Select();
		}
		else if (RestrictChar.Contains(e.KeyCode))
		{
			e.SuppressKeyPress = true;
		}
	}

	private void Name_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			Author.Select();
		}
	}

	private void Author_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			ModTags.Select();
		}
	}

	private void Tags_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			ModDesc.Select();
		}
	}

	private void Folder_TextChanged(object sender, EventArgs e)
	{
		NewMod.Enabled = FolderName.Text.Length > 0;
	}
}
