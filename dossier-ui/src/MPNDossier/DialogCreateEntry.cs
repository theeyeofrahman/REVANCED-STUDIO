using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

namespace MPNDossier;

[DesignerGenerated]
public class DialogCreateEntry : Form
{
	private IContainer components;

	[AccessedThroughProperty("ComboEntrySelect")]
	private ComboBox _ComboEntrySelect;

	[AccessedThroughProperty("OK_Button")]
	private Button _OK_Button;

	internal virtual ComboBox ComboEntrySelect
	{
		get
		{
			return _ComboEntrySelect;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			EventHandler value2 = ComboEntrySelect_SelectedIndexChanged;
			if (_ComboEntrySelect != null)
			{
				_ComboEntrySelect.SelectedIndexChanged -= value2;
			}
			_ComboEntrySelect = value;
			if (_ComboEntrySelect != null)
			{
				_ComboEntrySelect.SelectedIndexChanged += value2;
			}
		}
	}

	internal virtual Button OK_Button
	{
		get
		{
			return _OK_Button;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			EventHandler value2 = OK_Button_Click;
			if (_OK_Button != null)
			{
				_OK_Button.Click -= value2;
			}
			_OK_Button = value;
			if (_OK_Button != null)
			{
				_OK_Button.Click += value2;
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
		this.OK_Button = new DossierButton();
		this.ComboEntrySelect = new System.Windows.Forms.ComboBox();
		this.SuspendLayout();
		this.OK_Button.Font = new System.Drawing.Font("Impact", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.OK_Button.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
		System.Windows.Forms.Button oK_Button = this.OK_Button;
		System.Drawing.Point location = new System.Drawing.Point(299, 43);
		oK_Button.Location = location;
		this.OK_Button.Name = "OK_Button";
		System.Windows.Forms.Button oK_Button2 = this.OK_Button;
		System.Drawing.Size size = new System.Drawing.Size(107, 31);
		oK_Button2.Size = size;
		this.OK_Button.TabIndex = 0;
		this.OK_Button.Text = "Load Entry";
		this.ComboEntrySelect.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
		this.ComboEntrySelect.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
		this.ComboEntrySelect.BackColor = System.Drawing.SystemColors.InfoText;
		this.ComboEntrySelect.Font = new System.Drawing.Font("Impact", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.ComboEntrySelect.ForeColor = System.Drawing.SystemColors.Info;
		this.ComboEntrySelect.FormattingEnabled = true;
		System.Windows.Forms.ComboBox comboEntrySelect = this.ComboEntrySelect;
		location = new System.Drawing.Point(12, 12);
		comboEntrySelect.Location = location;
		this.ComboEntrySelect.Name = "ComboEntrySelect";
		System.Windows.Forms.ComboBox comboEntrySelect2 = this.ComboEntrySelect;
		size = new System.Drawing.Size(394, 27);
		comboEntrySelect2.Size = size;
		this.ComboEntrySelect.TabIndex = 2;
		this.AcceptButton = this.OK_Button;
		System.Drawing.SizeF sizeF = new System.Drawing.SizeF(6f, 13f);
		this.AutoScaleDimensions = sizeF;
		this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
		size = new System.Drawing.Size(435, 86);
		this.ClientSize = size;
		this.Controls.Add(this.OK_Button);
		this.Controls.Add(this.ComboEntrySelect);
		this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		this.MaximizeBox = false;
		this.MinimizeBox = false;
		this.Name = "DialogCreateEntry";
		this.ShowInTaskbar = false;
		this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.ResumeLayout(false);
	}

	private void DialogCreateEntry_Load(object sender, EventArgs e)
	{
		ComboEntrySelect.DataSource = Database;
        ComboEntrySelect.DisplayMember = "";
		OK_Button.Text = Action;
	}

	private void OK_Button_Click(object sender, EventArgs e)
	{
        if (string.IsNullOrWhiteSpace(ComboEntrySelect.Text)) return;
        if (Action == "Load" && ComboEntrySelect.SelectedItem == null) return;
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
