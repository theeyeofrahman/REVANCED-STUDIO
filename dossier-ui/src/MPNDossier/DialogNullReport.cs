using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;
using Newtonsoft.Json.Linq;

namespace MPNDossier;

[DesignerGenerated]
public class DialogNullReport : Form
{
	private IContainer components;

	[AccessedThroughProperty("Label1")]
	private Label _Label1;

	[AccessedThroughProperty("PropReplace")]
	private ListBox _PropReplace;

	[AccessedThroughProperty("SaveButton")]
	private Button _SaveButton;

	[AccessedThroughProperty("SearchBar")]
	private TextBox _SearchBar;

	[AccessedThroughProperty("ListRefs")]
	private TreeView _ListRefs;

	public ModCards.PropertyControl[] ControlScheme;

	public Dictionary<string, JObject> LoadableEntries;

	private string CurrentCard;

	private ModCards.NodeTag SelectedTag;

	private bool Saving;

	private bool Loading;

	internal virtual Label Label1
	{
		get
		{
			return _Label1;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			_Label1 = value;
		}
	}

	internal virtual ListBox PropReplace
	{
		get
		{
			return _PropReplace;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			EventHandler value2 = PropReplace_SelectedIndexChanged;
			if (_PropReplace != null)
			{
				_PropReplace.SelectedIndexChanged -= value2;
			}
			_PropReplace = value;
			if (_PropReplace != null)
			{
				_PropReplace.SelectedIndexChanged += value2;
			}
		}
	}

	internal virtual Button SaveButton
	{
		get
		{
			return _SaveButton;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			EventHandler value2 = SaveButton_Click;
			if (_SaveButton != null)
			{
				_SaveButton.Click -= value2;
			}
			_SaveButton = value;
			if (_SaveButton != null)
			{
				_SaveButton.Click += value2;
			}
		}
	}

	internal virtual TextBox SearchBar
	{
		get
		{
			return _SearchBar;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			EventHandler value2 = SearchBar_TextChanged;
			if (_SearchBar != null)
			{
				_SearchBar.TextChanged -= value2;
			}
			_SearchBar = value;
			if (_SearchBar != null)
			{
				_SearchBar.TextChanged += value2;
			}
		}
	}

	internal virtual TreeView ListRefs
	{
		get
		{
			return _ListRefs;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			TreeViewEventHandler value2 = ListRefs_AfterSelect;
			if (_ListRefs != null)
			{
				_ListRefs.AfterSelect -= value2;
			}
			_ListRefs = value;
			if (_ListRefs != null)
			{
				_ListRefs.AfterSelect += value2;
			}
		}
	}

	public DialogNullReport()
	{
		CurrentCard = "";
		Saving = false;
		Loading = false;
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
		this.Label1 = new System.Windows.Forms.Label();
		this.PropReplace = new System.Windows.Forms.ListBox();
		this.SaveButton = new DossierButton();
		this.SearchBar = new System.Windows.Forms.TextBox();
		this.ListRefs = new System.Windows.Forms.TreeView();
		this.SuspendLayout();
		this.Label1.AutoSize = true;
		this.Label1.Dock = System.Windows.Forms.DockStyle.Top;
		this.Label1.Font = new System.Drawing.Font("Impact", 15.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.Label1.ForeColor = System.Drawing.SystemColors.Info;
		System.Windows.Forms.Label label = this.Label1;
		System.Drawing.Point location = new System.Drawing.Point(0, 0);
		label.Location = location;
		this.Label1.Name = "Label1";
		System.Windows.Forms.Label label2 = this.Label1;
		System.Drawing.Size size = new System.Drawing.Size(70, 26);
		label2.Size = size;
		this.Label1.TabIndex = 0;
		this.Label1.Text = "Report";
		this.PropReplace.BackColor = System.Drawing.Color.FromArgb(32, 32, 32);
		this.PropReplace.Dock = System.Windows.Forms.DockStyle.Right;
		this.PropReplace.Font = new System.Drawing.Font("Impact", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.PropReplace.ForeColor = System.Drawing.SystemColors.Info;
		this.PropReplace.FormattingEnabled = true;
		this.PropReplace.ItemHeight = 20;
		System.Windows.Forms.ListBox propReplace = this.PropReplace;
		location = new System.Drawing.Point(528, 26);
		propReplace.Location = location;
		this.PropReplace.Name = "PropReplace";
		System.Windows.Forms.ListBox propReplace2 = this.PropReplace;
		size = new System.Drawing.Size(256, 535);
		propReplace2.Size = size;
		this.PropReplace.TabIndex = 23;
		this.SaveButton.BackColor = System.Drawing.Color.FromArgb(32, 32, 32);
		this.SaveButton.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.SaveButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.SaveButton.Font = new System.Drawing.Font("Impact", 15.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.SaveButton.ForeColor = System.Drawing.Color.White;
		System.Windows.Forms.Button saveButton = this.SaveButton;
		location = new System.Drawing.Point(0, 528);
		saveButton.Location = location;
		this.SaveButton.Name = "SaveButton";
		System.Windows.Forms.Button saveButton2 = this.SaveButton;
		size = new System.Drawing.Size(528, 33);
		saveButton2.Size = size;
		this.SaveButton.TabIndex = 25;
		this.SaveButton.Text = "Save All Changes";
		this.SaveButton.UseVisualStyleBackColor = false;
		this.SearchBar.BackColor = System.Drawing.SystemColors.InfoText;
		this.SearchBar.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.SearchBar.Enabled = false;
		this.SearchBar.Font = new System.Drawing.Font("Impact", 24f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.SearchBar.ForeColor = System.Drawing.SystemColors.Info;
		System.Windows.Forms.TextBox searchBar = this.SearchBar;
		location = new System.Drawing.Point(0, 481);
		searchBar.Location = location;
		this.SearchBar.Name = "SearchBar";
		System.Windows.Forms.TextBox searchBar2 = this.SearchBar;
		size = new System.Drawing.Size(528, 47);
		searchBar2.Size = size;
		this.SearchBar.TabIndex = 27;
		this.ListRefs.BackColor = System.Drawing.Color.Black;
		this.ListRefs.Dock = System.Windows.Forms.DockStyle.Fill;
		this.ListRefs.Font = new System.Drawing.Font("Impact", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.ListRefs.ForeColor = System.Drawing.Color.White;
		System.Windows.Forms.TreeView listRefs = this.ListRefs;
		location = new System.Drawing.Point(0, 26);
		listRefs.Location = location;
		this.ListRefs.Name = "ListRefs";
		System.Windows.Forms.TreeView listRefs2 = this.ListRefs;
		size = new System.Drawing.Size(528, 455);
		listRefs2.Size = size;
		this.ListRefs.TabIndex = 28;
		System.Drawing.SizeF sizeF = new System.Drawing.SizeF(6f, 13f);
		this.AutoScaleDimensions = sizeF;
		this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
		size = new System.Drawing.Size(784, 561);
		this.ClientSize = size;
		this.Controls.Add(this.ListRefs);
		this.Controls.Add(this.SearchBar);
		this.Controls.Add(this.SaveButton);
		this.Controls.Add(this.PropReplace);
		this.Controls.Add(this.Label1);
		this.Name = "DialogNullReport";
		this.Text = "Diagnostics Report";
		this.ResumeLayout(false);
		this.PerformLayout();
	}

	private void ListRefs_AfterSelect(object sender, TreeViewEventArgs e)
	{
		if (!(!Saving & (ListRefs.SelectedNode != null)))
		{
			return;
		}
		Loading = true;
		if ((ListRefs.SelectedNode.Parent != null) & (ListRefs.SelectedNode.Tag != null))
		{
			CurrentCard = ListRefs.SelectedNode.Parent.Name;
			SelectedTag = (ModCards.NodeTag)ListRefs.SelectedNode.Tag;
			SearchBar.Enabled = true;
			ModCards.PropertyControl linkedControl = SelectedTag.LinkedControl;
			if ((linkedControl.PropType == ModCards.PropType.Ref) | ((linkedControl.PropType == ModCards.PropType.Ord) & (linkedControl.Selection != null)))
			{
				PropReplace.DataSource = RuntimeHelpers.GetObjectValue(linkedControl.Selection);
				SearchBar.Text = "";
			}
		}
		else
		{
			PropReplace.DataSource = null;
			SearchBar.Enabled = false;
			SearchBar.Text = "";
		}
		Loading = false;
	}

	private void PropReplace_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (!Loading & !Saving & (SelectedTag != null) & (PropReplace.SelectedItem != null))
		{
			SelectedTag.Value = RuntimeHelpers.GetObjectValue(PropReplace.SelectedItem);
			ListRefs.SelectedNode.Text = ListRefs.SelectedNode.Name + ": " + SelectedTag.ToString();
			ListRefs.SelectedNode.ForeColor = ListRefs.ForeColor;
			ListRefs.SelectedNode.Parent.ForeColor = DiagnosticStatusColor(ListRefs.SelectedNode.Parent.Nodes);
		}
	}

	private void SaveButton_Click(object sender, EventArgs e)
	{
		DialogResult = DialogResult.Yes;
		Close();
	}

	private void SearchBar_TextChanged(object sender, EventArgs e)
	{
		if (!((SearchBar.Text.Length > 0) & (PropReplace.DataSource != null)))
		{
			return;
		}
		foreach (object item in PropReplace.Items)
		{
			object objectValue = RuntimeHelpers.GetObjectValue(item);
			if (objectValue.ToString().IndexOf(SearchBar.Text, 0, StringComparison.InvariantCultureIgnoreCase) >= 0)
			{
				PropReplace.SelectedItem = RuntimeHelpers.GetObjectValue(objectValue);
				break;
			}
		}
	}

	private Color DiagnosticStatusColor(TreeNodeCollection properties)
	{
		foreach (TreeNode property in properties)
		{
			if (property.ForeColor == Color.Salmon)
			{
				return Color.Yellow;
			}
		}
		return Color.Lime;
	}
}
