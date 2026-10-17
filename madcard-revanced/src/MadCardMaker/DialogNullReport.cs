using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;
using Newtonsoft.Json.Linq;

namespace MadCardMaker;

[DesignerGenerated]
public class DialogNullReport : Form
{
	private IContainer components;

	public ModCards.PropertyControl[] ControlScheme;

	public Dictionary<string, JObject> LoadableEntries;

	private string CurrentCard;

	private ModCards.NodeTag SelectedTag;

	private bool Saving;

	private bool Loading;

	[field: AccessedThroughProperty("Label1")]
	internal virtual Label Label1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("PropReplace")]
	internal virtual ListBox PropReplace
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = PropReplace_SelectedIndexChanged;
			ListBox listBox = field;
			if (listBox != null)
			{
				listBox.SelectedIndexChanged -= value2;
			}
			field = value;
			listBox = field;
			if (listBox != null)
			{
				listBox.SelectedIndexChanged += value2;
			}
		}
	}

	[field: AccessedThroughProperty("SaveButton")]
	internal virtual Button SaveButton
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = SaveButton_Click;
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

	[field: AccessedThroughProperty("SearchBar")]
	internal virtual TextBox SearchBar
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = SearchBar_TextChanged;
			TextBox textBox = field;
			if (textBox != null)
			{
				textBox.TextChanged -= value2;
			}
			field = value;
			textBox = field;
			if (textBox != null)
			{
				textBox.TextChanged += value2;
			}
		}
	}

	[field: AccessedThroughProperty("ListRefs")]
	internal virtual TreeView ListRefs
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			TreeViewEventHandler value2 = ListRefs_AfterSelect;
			TreeView treeView = field;
			if (treeView != null)
			{
				treeView.AfterSelect -= value2;
			}
			field = value;
			treeView = field;
			if (treeView != null)
			{
				treeView.AfterSelect += value2;
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
		this.SaveButton = new System.Windows.Forms.Button();
		this.SearchBar = new System.Windows.Forms.TextBox();
		this.ListRefs = new System.Windows.Forms.TreeView();
		base.SuspendLayout();
		this.Label1.AutoSize = true;
		this.Label1.Dock = System.Windows.Forms.DockStyle.Top;
		this.Label1.Font = new System.Drawing.Font("Impact", 15.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.Label1.ForeColor = System.Drawing.SystemColors.Info;
		this.Label1.Location = new System.Drawing.Point(0, 0);
		this.Label1.Name = "Label1";
		this.Label1.Size = new System.Drawing.Size(70, 26);
		this.Label1.TabIndex = 0;
		this.Label1.Text = "Report";
		this.PropReplace.BackColor = System.Drawing.Color.FromArgb(32, 32, 32);
		this.PropReplace.Dock = System.Windows.Forms.DockStyle.Right;
		this.PropReplace.Font = new System.Drawing.Font("Impact", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.PropReplace.ForeColor = System.Drawing.SystemColors.Info;
		this.PropReplace.FormattingEnabled = true;
		this.PropReplace.ItemHeight = 20;
		this.PropReplace.Location = new System.Drawing.Point(528, 26);
		this.PropReplace.Name = "PropReplace";
		this.PropReplace.Size = new System.Drawing.Size(256, 535);
		this.PropReplace.TabIndex = 23;
		this.SaveButton.BackColor = System.Drawing.Color.FromArgb(32, 32, 32);
		this.SaveButton.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.SaveButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.SaveButton.Font = new System.Drawing.Font("Impact", 15.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.SaveButton.ForeColor = System.Drawing.Color.White;
		this.SaveButton.Location = new System.Drawing.Point(0, 528);
		this.SaveButton.Name = "SaveButton";
		this.SaveButton.Size = new System.Drawing.Size(528, 33);
		this.SaveButton.TabIndex = 25;
		this.SaveButton.Text = "Save All Changes";
		this.SaveButton.UseVisualStyleBackColor = false;
		this.SearchBar.BackColor = System.Drawing.SystemColors.InfoText;
		this.SearchBar.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.SearchBar.Enabled = false;
		this.SearchBar.Font = new System.Drawing.Font("Impact", 24f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.SearchBar.ForeColor = System.Drawing.SystemColors.Info;
		this.SearchBar.Location = new System.Drawing.Point(0, 481);
		this.SearchBar.Name = "SearchBar";
		this.SearchBar.Size = new System.Drawing.Size(528, 47);
		this.SearchBar.TabIndex = 27;
		this.ListRefs.BackColor = System.Drawing.Color.Black;
		this.ListRefs.Dock = System.Windows.Forms.DockStyle.Fill;
		this.ListRefs.Font = new System.Drawing.Font("Impact", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.ListRefs.ForeColor = System.Drawing.Color.White;
		this.ListRefs.Location = new System.Drawing.Point(0, 26);
		this.ListRefs.Name = "ListRefs";
		this.ListRefs.Size = new System.Drawing.Size(528, 455);
		this.ListRefs.TabIndex = 28;
		base.AutoScaleDimensions = new System.Drawing.SizeF(96f, 96f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
		this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
		base.ClientSize = new System.Drawing.Size(784, 561);
		base.Controls.Add(this.ListRefs);
		base.Controls.Add(this.SearchBar);
		base.Controls.Add(this.SaveButton);
		base.Controls.Add(this.PropReplace);
		base.Controls.Add(this.Label1);
		base.Name = "DialogNullReport";
		this.Text = "Diagnostics Report";
		base.ResumeLayout(false);
		base.PerformLayout();
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
			if ((linkedControl.PropType == ModCards.PropType.Ref) & (linkedControl.Selection != null))
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
