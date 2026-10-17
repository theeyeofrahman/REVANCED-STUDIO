using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

namespace MadCardMaker;

[DesignerGenerated]
public class DialogCreateEntry : Form
{
	private IContainer components;

	[field: AccessedThroughProperty("ComboEntrySelect")]
	internal virtual ComboBox ComboEntrySelect
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = ComboEntrySelect_SelectedIndexChanged;
			ComboBox comboBox = field;
			if (comboBox != null)
			{
				comboBox.SelectedIndexChanged -= value2;
			}
			field = value;
			comboBox = field;
			if (comboBox != null)
			{
				comboBox.SelectedIndexChanged += value2;
			}
		}
	}

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

	public string Action { get; set; }

	public string CardType { get; set; }

	public string[] Database { get; set; }

	public string Card { get; set; }

	public DialogCreateEntry()
	{
		Load += DialogCreateEntry_Load;
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
		this.ComboEntrySelect = new System.Windows.Forms.ComboBox();
		base.SuspendLayout();
		this.OK_Button.Font = new System.Drawing.Font("Impact", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.OK_Button.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
		this.OK_Button.Location = new System.Drawing.Point(299, 43);
		this.OK_Button.Name = "OK_Button";
		this.OK_Button.Size = new System.Drawing.Size(107, 31);
		this.OK_Button.TabIndex = 0;
		this.OK_Button.Text = "Load Entry";
		this.ComboEntrySelect.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
		this.ComboEntrySelect.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
		this.ComboEntrySelect.BackColor = System.Drawing.SystemColors.InfoText;
		this.ComboEntrySelect.Font = new System.Drawing.Font("Impact", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.ComboEntrySelect.ForeColor = System.Drawing.SystemColors.Info;
		this.ComboEntrySelect.FormattingEnabled = true;
		this.ComboEntrySelect.Location = new System.Drawing.Point(12, 12);
		this.ComboEntrySelect.Name = "ComboEntrySelect";
		this.ComboEntrySelect.Size = new System.Drawing.Size(394, 27);
		this.ComboEntrySelect.TabIndex = 2;
		base.AcceptButton = this.OK_Button;
		base.AutoScaleDimensions = new System.Drawing.SizeF(96f, 96f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
		this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
		base.ClientSize = new System.Drawing.Size(420, 86);
		base.Controls.Add(this.OK_Button);
		base.Controls.Add(this.ComboEntrySelect);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "DialogCreateEntry";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		base.ResumeLayout(false);
	}

	private void DialogCreateEntry_Load(object sender, EventArgs e)
	{
		ComboEntrySelect.DataSource = Database;
		ComboEntrySelect.DisplayMember = "Key";
		OK_Button.Text = Action;
	}

	private void OK_Button_Click(object sender, EventArgs e)
	{
		DialogResult = DialogResult.OK;
		Close();
	}

	private void ComboEntrySelect_SelectedIndexChanged(object sender, EventArgs e)
	{
		OK_Button.Enabled = false;
		if (ComboEntrySelect.SelectedIndex > -1)
		{
			Card = ComboEntrySelect.SelectedItem.ToString();
			OK_Button.Enabled = true;
		}
	}
}
