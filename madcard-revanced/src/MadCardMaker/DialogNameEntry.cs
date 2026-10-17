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
public class DialogNameEntry : Form
{
	private IContainer components;

	private Keys[] RestrictChar;

	[field: AccessedThroughProperty("OK_Button")]
	internal virtual Button OK_Button
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = OK_Button_Click;
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

	[field: AccessedThroughProperty("NameField")]
	internal virtual TextBox NameField
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = NameField_TextChanged;
			KeyEventHandler value3 = Action_KeyDown;
			TextBox textBox = field;
			if (textBox != null)
			{
				textBox.TextChanged -= value2;
				textBox.KeyDown -= value3;
			}
			field = value;
			textBox = field;
			if (textBox != null)
			{
				textBox.TextChanged += value2;
				textBox.KeyDown += value3;
			}
		}
	}

	public string DefaultName { get; set; }

	public DialogNameEntry()
	{
		Load += DialogNameEntry_Load;
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
		this.OK_Button = new System.Windows.Forms.Button();
		this.NameField = new System.Windows.Forms.TextBox();
		base.SuspendLayout();
		this.OK_Button.Font = new System.Drawing.Font("Impact", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.OK_Button.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
		this.OK_Button.Location = new System.Drawing.Point(316, 45);
		this.OK_Button.Name = "OK_Button";
		this.OK_Button.Size = new System.Drawing.Size(107, 31);
		this.OK_Button.TabIndex = 3;
		this.OK_Button.Text = "Name Entry";
		this.NameField.BackColor = System.Drawing.Color.Black;
		this.NameField.Font = new System.Drawing.Font("Impact", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.NameField.ForeColor = System.Drawing.Color.White;
		this.NameField.Location = new System.Drawing.Point(13, 12);
		this.NameField.Name = "NameField";
		this.NameField.Size = new System.Drawing.Size(410, 27);
		this.NameField.TabIndex = 4;
		this.NameField.Text = "New Deck";
		base.AutoScaleDimensions = new System.Drawing.SizeF(96f, 96f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
		this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
		base.ClientSize = new System.Drawing.Size(435, 83);
		base.Controls.Add(this.NameField);
		base.Controls.Add(this.OK_Button);
		base.Name = "DialogNameEntry";
		this.Text = "Name Entry";
		base.ResumeLayout(false);
		base.PerformLayout();
	}

	private void DialogNameEntry_Load(object sender, EventArgs e)
	{
		ModColors.ApplyMenuColors(this);
		NameField.Text = DefaultName;
		NameField.Select();
	}

	private void OK_Button_Click(object sender, EventArgs e)
	{
		DialogResult = DialogResult.OK;
	}

	private void NameField_TextChanged(object sender, EventArgs e)
	{
		OK_Button.Enabled = NameField.Text.Length > 0;
	}

	private void Action_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			OK_Button.PerformClick();
		}
		else if (RestrictChar.Contains(e.KeyCode))
		{
			e.SuppressKeyPress = true;
		}
	}
}
