using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Cyotek.Windows.Forms;
using MadCardMaker.My;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Newtonsoft.Json.Linq;

namespace MadCardMaker;

[DesignerGenerated]
public partial class FormEditor : Form
{
	[CompilerGenerated]
	internal sealed class _Closure_0024__234_002D0
	{
		public ModCards.NodeTag _0024VB_0024Local_tag;

		public FormEditor _0024VB_0024Me;

		public _Closure_0024__234_002D0(_Closure_0024__234_002D0 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_tag = arg0._0024VB_0024Local_tag;
			}
		}

		[SpecialName]
		internal bool _Lambda_0024__0(string s)
		{
			return s.ToLower().Contains(_0024VB_0024Me.Param.Text.ToLower()) & (Operators.CompareString(_0024VB_0024Me.SaveType, _0024VB_0024Local_tag.LinkedControl.ObjectName, TextCompare: false) != 0 || ModNodes.LoadedPrior(_0024VB_0024Me.SaveType, s, _0024VB_0024Me.PreloadPriority));
		}
	}

	private IContainer components;

	private string SaveType;

	private ModCards.PropertyList SavedList;

	private bool Unsaved;

	private bool IsResizing;

	private ModCards.PropertyControl[] ControlScheme;

	private SortedDictionary<string, JObject> LoadableEntries;

	private SortedDictionary<string, JObject> RootableEntries;

	private Dictionary<string, TreeNode> EditedProperties;

	private ModCards.NodeTag SelectedTag;

	private bool Loading;

	private bool NotifyAddedProperties;

	private string[] NoRoots;

	[field: AccessedThroughProperty("EditorStrip")]
	internal virtual MenuStrip EditorStrip
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

	[field: AccessedThroughProperty("ActionButton")]
	internal virtual Button ActionButton
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = ActionButton_Click;
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

	[field: AccessedThroughProperty("ViewToolStripMenuItem")]
	internal virtual ToolStripMenuItem ViewToolStripMenuItem
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ExpandCurrentToolStripMenuItem")]
	internal virtual ToolStripMenuItem ExpandCurrentToolStripMenuItem
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = ExpandCurrrentToolStripMenuItem_Click;
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

	[field: AccessedThroughProperty("CollapseCurrentToolStripMenuItem")]
	internal virtual ToolStripMenuItem CollapseCurrentToolStripMenuItem
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = CollapseCurrentToolStripMenuItem_Click;
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

	[field: AccessedThroughProperty("ExpandAllToolStripMenuItem")]
	internal virtual ToolStripMenuItem ExpandAllToolStripMenuItem
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = ExpandAllToolStripMenuItem_Click;
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

	[field: AccessedThroughProperty("CollapseAllToolStripMenuItem")]
	internal virtual ToolStripMenuItem CollapseAllToolStripMenuItem
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = CollapseAllToolStripMenuItem_Click;
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

	[field: AccessedThroughProperty("LoadingProgress")]
	internal virtual ProgressBar LoadingProgress
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ShowInfoToolStripMenuItem")]
	internal virtual ToolStripMenuItem ShowInfoToolStripMenuItem
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = ShowInfoToolStripMenuItem_Click;
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

	[field: AccessedThroughProperty("EditToolStripMenuItem")]
	internal virtual ToolStripMenuItem EditToolStripMenuItem
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ColorWheelToolStripMenuItem")]
	internal virtual ToolStripMenuItem ColorWheelToolStripMenuItem
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = ColorWheelToolStripMenuItem_Click;
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

	[field: AccessedThroughProperty("DuplicateToolStripMenuItem")]
	internal virtual ToolStripMenuItem DuplicateToolStripMenuItem
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = DuplicateObjectToolStripMenuItem_Click;
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

	[field: AccessedThroughProperty("RevertToolStripMenuItem")]
	internal virtual ToolStripMenuItem RevertToolStripMenuItem
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = RevertToolStripMenuItem_Click;
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

	[field: AccessedThroughProperty("ShowPriceToolStripMenuItem")]
	internal virtual ToolStripMenuItem ShowPriceToolStripMenuItem
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = ShowPriceToolStripMenuItem_Click;
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

	[field: AccessedThroughProperty("RevertAllToolStripMenuItem")]
	internal virtual ToolStripMenuItem RevertAllToolStripMenuItem
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = RevertAllToolStripMenuItem_Click;
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

	[field: AccessedThroughProperty("LoadToolStripMenuItem")]
	internal virtual ToolStripMenuItem LoadToolStripMenuItem
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = LoadToolStripMenuItem_Click;
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

	[field: AccessedThroughProperty("ToolStripSeparator1")]
	internal virtual ToolStripSeparator ToolStripSeparator1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ShiftUpToolStripMenuItem")]
	internal virtual ToolStripMenuItem ShiftUpToolStripMenuItem
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = ShiftUpToolStripMenuItem_Click;
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

	[field: AccessedThroughProperty("ShiftDownToolStripMenuItem")]
	internal virtual ToolStripMenuItem ShiftDownToolStripMenuItem
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = ShiftDownToolStripMenuItem_Click;
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

	[field: AccessedThroughProperty("ToolStripSeparator2")]
	internal virtual ToolStripSeparator ToolStripSeparator2
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("LoadFromSelectedToolStripMenuItem")]
	internal virtual ToolStripMenuItem LoadFromSelectedToolStripMenuItem
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = LoadFromSelectedToolStripMenuItem_Click;
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

	[field: AccessedThroughProperty("ToolStripSeparator3")]
	internal virtual ToolStripSeparator ToolStripSeparator3
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("CardType")]
	internal virtual ComboBox CardType
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = CardType_SelectedIndexChanged;
			KeyPressEventHandler value3 = CardType_KeyPress;
			ComboBox comboBox = field;
			if (comboBox != null)
			{
				comboBox.SelectedIndexChanged -= value2;
				comboBox.KeyPress -= value3;
			}
			field = value;
			comboBox = field;
			if (comboBox != null)
			{
				comboBox.SelectedIndexChanged += value2;
				comboBox.KeyPress += value3;
			}
		}
	}

	[field: AccessedThroughProperty("Param")]
	internal virtual TextBox Param
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = Param_TextChanged;
			KeyEventHandler value3 = Param_KeyDown;
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

	[field: AccessedThroughProperty("FileName")]
	internal virtual TextBox FileName
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("CardRoot")]
	internal virtual ComboBox CardRoot
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = CardRoot_SelectedIndexChanged;
			KeyPressEventHandler value3 = CardRoot_KeyPress;
			ComboBox comboBox = field;
			if (comboBox != null)
			{
				comboBox.SelectedIndexChanged -= value2;
				comboBox.KeyPress -= value3;
			}
			field = value;
			comboBox = field;
			if (comboBox != null)
			{
				comboBox.SelectedIndexChanged += value2;
				comboBox.KeyPress += value3;
			}
		}
	}

	[field: AccessedThroughProperty("InfoTab")]
	internal virtual RichTextBox InfoTab
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Price")]
	internal virtual TextBox Price
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ResizePanels")]
	internal virtual TableLayoutPanel ResizePanels
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MouseEventHandler value2 = ResizePanels_MouseDown;
			MouseEventHandler value3 = ResizePanels_MouseMove;
			MouseEventHandler value4 = ResizePanels_MouseUp;
			TableLayoutPanel tableLayoutPanel = field;
			if (tableLayoutPanel != null)
			{
				tableLayoutPanel.MouseDown -= value2;
				tableLayoutPanel.MouseMove -= value3;
				tableLayoutPanel.MouseUp -= value4;
			}
			field = value;
			tableLayoutPanel = field;
			if (tableLayoutPanel != null)
			{
				tableLayoutPanel.MouseDown += value2;
				tableLayoutPanel.MouseMove += value3;
				tableLayoutPanel.MouseUp += value4;
			}
		}
	}

	[field: AccessedThroughProperty("Panel1")]
	internal virtual Panel Panel1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ListObjects")]
	internal virtual ListBox ListObjects
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = ListObjects_SelectedIndexChanged;
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

	[field: AccessedThroughProperty("ObjEditor")]
	internal virtual TreeView ObjEditor
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			KeyPressEventHandler value2 = ObjEditor_KeyPress;
			TreeViewEventHandler value3 = ObjEditor_AfterSelect;
			TreeView treeView = field;
			if (treeView != null)
			{
				treeView.KeyPress -= value2;
				treeView.AfterSelect -= value3;
			}
			field = value;
			treeView = field;
			if (treeView != null)
			{
				treeView.KeyPress += value2;
				treeView.AfterSelect += value3;
			}
		}
	}

	[field: AccessedThroughProperty("StringEditorToolStripMenuItem")]
	internal virtual ToolStripMenuItem StringEditorToolStripMenuItem
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = StringEditorToolStripMenuItem_Click;
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

	[field: AccessedThroughProperty("ToolStripSeparator4")]
	internal virtual ToolStripSeparator ToolStripSeparator4
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	public FormDeckManager Manager { get; set; }

	public string PreloadType { get; set; }

	public string PreloadRoot { get; set; }

	public string PreloadFile { get; set; }

	public bool UnlistAllValues { get; set; }

	public string PreloadFileInfo { get; set; }

	public int PreloadPriority { get; set; }

	public FormEditor()
	{
		Load += FormEditorV2_Load;
		FormClosing += FormEditorV2_Closing;
		PreloadType = "";
		PreloadRoot = "";
		PreloadFile = "";
		UnlistAllValues = false;
		PreloadFileInfo = "";
		PreloadPriority = int.MaxValue;
		SaveType = "";
		Unsaved = false;
		IsResizing = false;
		EditedProperties = new Dictionary<string, TreeNode>();
		Loading = false;
		NotifyAddedProperties = false;
		NoRoots = new string[4] { "Event", "Cutscene", "Mission", "Driver" };
		InitializeComponent();
        BuildModernLayout();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MadCardMaker.FormEditor));
		this.EditorStrip = new System.Windows.Forms.MenuStrip();
		this.FileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.SaveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.LoadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.EditToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.ColorWheelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.RevertToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.RevertAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.ToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
		this.DuplicateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.ShiftUpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.ShiftDownToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.ViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.ExpandCurrentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.CollapseCurrentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.ExpandAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.CollapseAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.ToolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
		this.ShowInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.ShowPriceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.ToolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
		this.LoadFromSelectedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.ActionButton = new System.Windows.Forms.Button();
		this.LoadingProgress = new System.Windows.Forms.ProgressBar();
		this.CardType = new System.Windows.Forms.ComboBox();
		this.Param = new System.Windows.Forms.TextBox();
		this.FileName = new System.Windows.Forms.TextBox();
		this.CardRoot = new System.Windows.Forms.ComboBox();
		this.InfoTab = new System.Windows.Forms.RichTextBox();
		this.Price = new System.Windows.Forms.TextBox();
		this.ResizePanels = new System.Windows.Forms.TableLayoutPanel();
		this.ListObjects = new System.Windows.Forms.ListBox();
		this.Panel1 = new System.Windows.Forms.Panel();
		this.ObjEditor = new System.Windows.Forms.TreeView();
		this.StringEditorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.ToolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
		this.EditorStrip.SuspendLayout();
		this.ResizePanels.SuspendLayout();
		this.Panel1.SuspendLayout();
		base.SuspendLayout();
		this.EditorStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[3] { this.FileToolStripMenuItem, this.EditToolStripMenuItem, this.ViewToolStripMenuItem });
		this.EditorStrip.Location = new System.Drawing.Point(0, 0);
		this.EditorStrip.Name = "EditorStrip";
		this.EditorStrip.Size = new System.Drawing.Size(784, 24);
		this.EditorStrip.TabIndex = 20;
		this.EditorStrip.Text = "MenuStrip1";
		this.FileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[2] { this.SaveToolStripMenuItem, this.LoadToolStripMenuItem });
		this.FileToolStripMenuItem.Name = "FileToolStripMenuItem";
		this.FileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
		this.FileToolStripMenuItem.Text = "File";
		this.SaveToolStripMenuItem.Enabled = false;
		this.SaveToolStripMenuItem.Name = "SaveToolStripMenuItem";
		this.SaveToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.S | System.Windows.Forms.Keys.Control;
		this.SaveToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
		this.SaveToolStripMenuItem.Text = "Save";
		this.LoadToolStripMenuItem.Enabled = false;
		this.LoadToolStripMenuItem.Name = "LoadToolStripMenuItem";
		this.LoadToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.L | System.Windows.Forms.Keys.Control;
		this.LoadToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
		this.LoadToolStripMenuItem.Text = "Load";
		this.EditToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[9] { this.ColorWheelToolStripMenuItem, this.StringEditorToolStripMenuItem, this.ToolStripSeparator4, this.RevertToolStripMenuItem, this.RevertAllToolStripMenuItem, this.ToolStripSeparator1, this.DuplicateToolStripMenuItem, this.ShiftUpToolStripMenuItem, this.ShiftDownToolStripMenuItem });
		this.EditToolStripMenuItem.Name = "EditToolStripMenuItem";
		this.EditToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
		this.EditToolStripMenuItem.Text = "Edit";
		this.ColorWheelToolStripMenuItem.Enabled = false;
		this.ColorWheelToolStripMenuItem.Name = "ColorWheelToolStripMenuItem";
		this.ColorWheelToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F | System.Windows.Forms.Keys.Control;
		this.ColorWheelToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
		this.ColorWheelToolStripMenuItem.Text = "Color Wheel";
		this.RevertToolStripMenuItem.Enabled = false;
		this.RevertToolStripMenuItem.Name = "RevertToolStripMenuItem";
		this.RevertToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Z | System.Windows.Forms.Keys.Control;
		this.RevertToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
		this.RevertToolStripMenuItem.Text = "Revert";
		this.RevertAllToolStripMenuItem.Enabled = false;
		this.RevertAllToolStripMenuItem.Name = "RevertAllToolStripMenuItem";
		this.RevertAllToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Z | System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Control;
		this.RevertAllToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
		this.RevertAllToolStripMenuItem.Text = "Revert All";
		this.ToolStripSeparator1.Name = "ToolStripSeparator1";
		this.ToolStripSeparator1.Size = new System.Drawing.Size(194, 6);
		this.DuplicateToolStripMenuItem.Enabled = false;
		this.DuplicateToolStripMenuItem.Name = "DuplicateToolStripMenuItem";
		this.DuplicateToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.D | System.Windows.Forms.Keys.Control;
		this.DuplicateToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
		this.DuplicateToolStripMenuItem.Text = "Duplicate";
		this.ShiftUpToolStripMenuItem.Enabled = false;
		this.ShiftUpToolStripMenuItem.Name = "ShiftUpToolStripMenuItem";
		this.ShiftUpToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Q | System.Windows.Forms.Keys.Control;
		this.ShiftUpToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
		this.ShiftUpToolStripMenuItem.Text = "Shift Up";
		this.ShiftDownToolStripMenuItem.Enabled = false;
		this.ShiftDownToolStripMenuItem.Name = "ShiftDownToolStripMenuItem";
		this.ShiftDownToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.E | System.Windows.Forms.Keys.Control;
		this.ShiftDownToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
		this.ShiftDownToolStripMenuItem.Text = "ShiftDown";
		this.ViewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[9] { this.ExpandCurrentToolStripMenuItem, this.CollapseCurrentToolStripMenuItem, this.ExpandAllToolStripMenuItem, this.CollapseAllToolStripMenuItem, this.ToolStripSeparator2, this.ShowInfoToolStripMenuItem, this.ShowPriceToolStripMenuItem, this.ToolStripSeparator3, this.LoadFromSelectedToolStripMenuItem });
		this.ViewToolStripMenuItem.Name = "ViewToolStripMenuItem";
		this.ViewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
		this.ViewToolStripMenuItem.Text = "View";
		this.ExpandCurrentToolStripMenuItem.Enabled = false;
		this.ExpandCurrentToolStripMenuItem.Name = "ExpandCurrentToolStripMenuItem";
		this.ExpandCurrentToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.K | System.Windows.Forms.Keys.Control;
		this.ExpandCurrentToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
		this.ExpandCurrentToolStripMenuItem.Text = "Expand Current";
		this.CollapseCurrentToolStripMenuItem.Enabled = false;
		this.CollapseCurrentToolStripMenuItem.Name = "CollapseCurrentToolStripMenuItem";
		this.CollapseCurrentToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.J | System.Windows.Forms.Keys.Control;
		this.CollapseCurrentToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
		this.CollapseCurrentToolStripMenuItem.Text = "Collapse Current";
		this.ExpandAllToolStripMenuItem.Enabled = false;
		this.ExpandAllToolStripMenuItem.Name = "ExpandAllToolStripMenuItem";
		this.ExpandAllToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.K | System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Control;
		this.ExpandAllToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
		this.ExpandAllToolStripMenuItem.Text = "Expand All";
		this.CollapseAllToolStripMenuItem.Enabled = false;
		this.CollapseAllToolStripMenuItem.Name = "CollapseAllToolStripMenuItem";
		this.CollapseAllToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.J | System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Control;
		this.CollapseAllToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
		this.CollapseAllToolStripMenuItem.Text = "Collapse All";
		this.ToolStripSeparator2.Name = "ToolStripSeparator2";
		this.ToolStripSeparator2.Size = new System.Drawing.Size(218, 6);
		this.ShowInfoToolStripMenuItem.CheckOnClick = true;
		this.ShowInfoToolStripMenuItem.Enabled = false;
		this.ShowInfoToolStripMenuItem.Name = "ShowInfoToolStripMenuItem";
		this.ShowInfoToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.I | System.Windows.Forms.Keys.Control;
		this.ShowInfoToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
		this.ShowInfoToolStripMenuItem.Text = "Show Info";
		this.ShowPriceToolStripMenuItem.CheckOnClick = true;
		this.ShowPriceToolStripMenuItem.Enabled = false;
		this.ShowPriceToolStripMenuItem.Name = "ShowPriceToolStripMenuItem";
		this.ShowPriceToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.P | System.Windows.Forms.Keys.Control;
		this.ShowPriceToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
		this.ShowPriceToolStripMenuItem.Text = "Show Price";
		this.ToolStripSeparator3.Name = "ToolStripSeparator3";
		this.ToolStripSeparator3.Size = new System.Drawing.Size(218, 6);
		this.LoadFromSelectedToolStripMenuItem.Enabled = false;
		this.LoadFromSelectedToolStripMenuItem.Name = "LoadFromSelectedToolStripMenuItem";
		this.LoadFromSelectedToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.O | System.Windows.Forms.Keys.Control;
		this.LoadFromSelectedToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
		this.LoadFromSelectedToolStripMenuItem.Text = "Load From Selected";
		this.ActionButton.BackColor = System.Drawing.Color.FromArgb(32, 32, 32);
		this.ActionButton.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.ActionButton.Enabled = false;
		this.ActionButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.ActionButton.Font = new System.Drawing.Font("Impact", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.ActionButton.ForeColor = System.Drawing.SystemColors.Info;
		this.ActionButton.Location = new System.Drawing.Point(0, 539);
		this.ActionButton.Name = "ActionButton";
		this.ActionButton.Size = new System.Drawing.Size(560, 33);
		this.ActionButton.TabIndex = 16;
		this.ActionButton.TextAlign = System.Drawing.ContentAlignment.TopLeft;
		this.ActionButton.UseVisualStyleBackColor = false;
		this.LoadingProgress.BackColor = System.Drawing.Color.DarkRed;
		this.LoadingProgress.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.LoadingProgress.ForeColor = System.Drawing.Color.DarkRed;
		this.LoadingProgress.Location = new System.Drawing.Point(0, 619);
		this.LoadingProgress.Name = "LoadingProgress";
		this.LoadingProgress.Size = new System.Drawing.Size(560, 12);
		this.LoadingProgress.TabIndex = 27;
		this.CardType.BackColor = System.Drawing.Color.Black;
		this.CardType.Dock = System.Windows.Forms.DockStyle.Top;
		this.CardType.Font = new System.Drawing.Font("Impact", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.CardType.ForeColor = System.Drawing.Color.White;
		this.CardType.FormattingEnabled = true;
		this.CardType.Location = new System.Drawing.Point(0, 0);
		this.CardType.Name = "CardType";
		this.CardType.Size = new System.Drawing.Size(560, 28);
		this.CardType.TabIndex = 28;
		this.CardType.Text = "Select Card Type";
		this.Param.BackColor = System.Drawing.Color.Black;
		this.Param.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.Param.Enabled = false;
		this.Param.Font = new System.Drawing.Font("Impact", 24f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.Param.ForeColor = System.Drawing.SystemColors.Info;
		this.Param.Location = new System.Drawing.Point(0, 572);
		this.Param.MinimumSize = new System.Drawing.Size(4, 47);
		this.Param.Name = "Param";
		this.Param.Size = new System.Drawing.Size(560, 47);
		this.Param.TabIndex = 24;
		this.Param.Text = "Parameter";
		this.FileName.BackColor = System.Drawing.Color.Black;
		this.FileName.Dock = System.Windows.Forms.DockStyle.Top;
		this.FileName.Enabled = false;
		this.FileName.Font = new System.Drawing.Font("Impact", 15.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.FileName.ForeColor = System.Drawing.Color.White;
		this.FileName.Location = new System.Drawing.Point(0, 28);
		this.FileName.Name = "FileName";
		this.FileName.Size = new System.Drawing.Size(560, 33);
		this.FileName.TabIndex = 21;
		this.FileName.Text = "Name your Card";
		this.CardRoot.BackColor = System.Drawing.Color.Black;
		this.CardRoot.Dock = System.Windows.Forms.DockStyle.Top;
		this.CardRoot.Font = new System.Drawing.Font("Impact", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.CardRoot.ForeColor = System.Drawing.Color.White;
		this.CardRoot.FormattingEnabled = true;
		this.CardRoot.Location = new System.Drawing.Point(0, 61);
		this.CardRoot.Name = "CardRoot";
		this.CardRoot.Size = new System.Drawing.Size(560, 28);
		this.CardRoot.TabIndex = 29;
		this.CardRoot.Text = "Select Root";
		this.InfoTab.BackColor = System.Drawing.Color.FromArgb(64, 64, 64);
		this.InfoTab.BulletIndent = 2;
		this.InfoTab.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.InfoTab.Font = new System.Drawing.Font("Impact", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.InfoTab.ForeColor = System.Drawing.Color.White;
		this.InfoTab.Location = new System.Drawing.Point(0, 439);
		this.InfoTab.Name = "InfoTab";
		this.InfoTab.ReadOnly = true;
		this.InfoTab.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
		this.InfoTab.Size = new System.Drawing.Size(560, 100);
		this.InfoTab.TabIndex = 30;
		this.InfoTab.Text = "";
		this.InfoTab.Visible = false;
		this.Price.BackColor = System.Drawing.Color.Black;
		this.Price.Dock = System.Windows.Forms.DockStyle.Top;
		this.Price.Font = new System.Drawing.Font("Impact", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.Price.ForeColor = System.Drawing.Color.Lime;
		this.Price.Location = new System.Drawing.Point(0, 89);
		this.Price.Name = "Price";
		this.Price.ReadOnly = true;
		this.Price.Size = new System.Drawing.Size(560, 23);
		this.Price.TabIndex = 31;
		this.Price.Text = "$$$";
		this.Price.Visible = false;
		this.ResizePanels.ColumnCount = 2;
		this.ResizePanels.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 566f));
		this.ResizePanels.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 218f));
		this.ResizePanels.Controls.Add(this.ListObjects, 1, 0);
		this.ResizePanels.Controls.Add(this.Panel1, 0, 0);
		this.ResizePanels.Dock = System.Windows.Forms.DockStyle.Fill;
		this.ResizePanels.Location = new System.Drawing.Point(0, 24);
		this.ResizePanels.Name = "ResizePanels";
		this.ResizePanels.RowCount = 1;
		this.ResizePanels.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 637f));
		this.ResizePanels.Size = new System.Drawing.Size(784, 637);
		this.ResizePanels.TabIndex = 32;
		this.ListObjects.BackColor = System.Drawing.Color.FromArgb(32, 32, 32);
		this.ListObjects.Dock = System.Windows.Forms.DockStyle.Fill;
		this.ListObjects.Font = new System.Drawing.Font("Impact", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.ListObjects.ForeColor = System.Drawing.Color.White;
		this.ListObjects.FormattingEnabled = true;
		this.ListObjects.ItemHeight = 20;
		this.ListObjects.Location = new System.Drawing.Point(569, 3);
		this.ListObjects.Name = "ListObjects";
		this.ListObjects.Size = new System.Drawing.Size(212, 631);
		this.ListObjects.TabIndex = 24;
		this.Panel1.Controls.Add(this.ObjEditor);
		this.Panel1.Controls.Add(this.Price);
		this.Panel1.Controls.Add(this.InfoTab);
		this.Panel1.Controls.Add(this.CardRoot);
		this.Panel1.Controls.Add(this.FileName);
		this.Panel1.Controls.Add(this.ActionButton);
		this.Panel1.Controls.Add(this.Param);
		this.Panel1.Controls.Add(this.CardType);
		this.Panel1.Controls.Add(this.LoadingProgress);
		this.Panel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.Panel1.Location = new System.Drawing.Point(3, 3);
		this.Panel1.Name = "Panel1";
		this.Panel1.Size = new System.Drawing.Size(560, 631);
		this.Panel1.TabIndex = 0;
		this.ObjEditor.BackColor = System.Drawing.Color.Black;
		this.ObjEditor.Dock = System.Windows.Forms.DockStyle.Fill;
		this.ObjEditor.Font = new System.Drawing.Font("Courier New", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.ObjEditor.ForeColor = System.Drawing.Color.Gray;
		this.ObjEditor.Location = new System.Drawing.Point(0, 0);
		this.ObjEditor.Name = "ObjEditor";
		this.ObjEditor.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.ObjEditor.Size = new System.Drawing.Size(560, 631);
		this.ObjEditor.TabIndex = 25;
		this.StringEditorToolStripMenuItem.Enabled = false;
		this.StringEditorToolStripMenuItem.Name = "StringEditorToolStripMenuItem";
		this.StringEditorToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.G | System.Windows.Forms.Keys.Control;
		this.StringEditorToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
		this.StringEditorToolStripMenuItem.Text = "String Editor";
		this.ToolStripSeparator4.Name = "ToolStripSeparator4";
		this.ToolStripSeparator4.Size = new System.Drawing.Size(194, 6);
		base.AutoScaleDimensions = new System.Drawing.SizeF(96f, 96f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
		this.BackColor = System.Drawing.Color.FromArgb(64, 64, 64);
		base.ClientSize = new System.Drawing.Size(784, 661);
		base.Controls.Add(this.ResizePanels);
		base.Controls.Add(this.EditorStrip);
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "FormEditor";
		this.Text = "Create an Entry";
		this.EditorStrip.ResumeLayout(false);
		this.EditorStrip.PerformLayout();
		this.ResizePanels.ResumeLayout(false);
		this.Panel1.ResumeLayout(false);
		this.Panel1.PerformLayout();
		base.ResumeLayout(false);
		base.PerformLayout();
	}

	private void FormEditorV2_Load(object sender, EventArgs e)
	{
		ObjEditor.KeyDown += Action_KeyDown;
		Param.KeyDown += Action_KeyDown;
		ListObjects.KeyDown += Action_KeyDown;
		ObjEditor.KeyUp += Action_KeyUp;
		Param.KeyUp += Action_KeyUp;
		ListObjects.KeyUp += Action_KeyUp;
		ShowInfoToolStripMenuItem.Checked = MySettingsProperty.Settings.ShowInfo;
		InfoTab.Visible = MySettingsProperty.Settings.ShowInfo;
		ShowPriceToolStripMenuItem.Checked = MySettingsProperty.Settings.ShowPrice;
		Price.Visible = MySettingsProperty.Settings.ShowPrice;
		CardType.Items.AddRange(ModEnums.CARDTYPES);
		ModColors.ApplyMenuColors(this, colorDisabled: true);
		if (PreloadType.Length > 0)
		{
			CardType.SelectedItem = PreloadType;
		}
		PreloadType = "";
		MarkUnsaved(edited: false);
	}

	private void FormEditorV2_Closing(object sender, FormClosingEventArgs e)
	{
		if (Unsaved)
		{
			switch (DossierDialog.MsgBox("Save Changes to " + FileName.Text + "?", MsgBoxStyle.YesNoCancel, "Save Changes"))
			{
			case MsgBoxResult.Yes:
				if(!SaveToDirectory("")){e.Cancel=true;return;}
				Unsaved = false;
				Close();
				break;
			case MsgBoxResult.No:
				Unsaved = false;
				Close();
				break;
			default:
				e.Cancel = true;
				break;
			}
		}
	}

	private void ResizePanels_MouseDown(object sender, MouseEventArgs e)
	{
		checked
		{
			int num = (int)Math.Round(ResizePanels.ColumnStyles[0].Width);
			int num2 = (int)Math.Round((float)num + ResizePanels.ColumnStyles[1].Width);
			if ((e.X + 20 > num) & (e.X - 20 < num2))
			{
				IsResizing = true;
			}
		}
	}

	private void ResizePanels_MouseMove(object sender, MouseEventArgs e)
	{
		if (IsResizing & (e.Button == MouseButtons.Left))
		{
			Cursor = Cursors.VSplit;
			ResizePanels.ColumnStyles[0].Width = Math.Max(100, e.X);
		}
	}

	private void ResizePanels_MouseUp(object sender, MouseEventArgs e)
	{
		IsResizing = false;
		Cursor = Cursors.Default;
	}

	private void CardType_SelectedIndexChanged(object sender, EventArgs e)
	{
		object selectedItem = CardType.SelectedItem;
		if (Operators.ConditionalCompareObjectEqual(selectedItem, "Armor", TextCompare: false))
		{
			ModCards.PropertyList Schema = ModCards.Statcard_Armor;
			NewCard("Armor", "0_Armor", ref Schema, "New Armor");
		}
		else if (Operators.ConditionalCompareObjectEqual(selectedItem, "Melee", TextCompare: false))
		{
			ModCards.PropertyList Schema = ModCards.StatCard_Melee;
			NewCard("Melee", "0_Melee", ref Schema, "New Melee");
		}
		else if (Operators.ConditionalCompareObjectEqual(selectedItem, "Thrown", TextCompare: false))
		{
			ModCards.PropertyList Schema = ModCards.StatCard_Thrown;
			NewCard("Thrown", "0_Thrown", ref Schema, "New Throwable");
		}
		else if (Operators.ConditionalCompareObjectEqual(selectedItem, "Ranged", TextCompare: false))
		{
			ModCards.PropertyList Schema = ModCards.StatCard_Ranged;
			NewCard("Ranged", "0_Ranged", ref Schema, "New Ranged");
		}
		else if (Operators.ConditionalCompareObjectEqual(selectedItem, "Keycard", TextCompare: false))
		{
			ModCards.PropertyList Schema = ModCards.StatCard_Collectible;
			NewCard("Keycard", "0_Collectible", ref Schema, "New Keycard");
		}
		else if (Operators.ConditionalCompareObjectEqual(selectedItem, "Addon", TextCompare: false))
		{
			ModCards.PropertyList Schema = ModCards.StatCard_WeaponAddon;
			NewCard("Addon", "0_Addon", ref Schema, "New Addon");
		}
		else if (Operators.ConditionalCompareObjectEqual(selectedItem, "AddonGroup", TextCompare: false))
		{
			ModCards.PropertyList Schema = ModCards.StatCard_AddonGroup;
			NewCard("AddonGroup", "Firearms_Barrels_Core", ref Schema, "New Addon Set");
		}
		else if (Operators.ConditionalCompareObjectEqual(selectedItem, "Ammo", TextCompare: false))
		{
			ModCards.PropertyList Schema = ModCards.StatCard_Ammo;
			NewCard("Ammo", "0_Ammo", ref Schema, "New Ammo");
		}
		else if (Operators.ConditionalCompareObjectEqual(selectedItem, "DoT", TextCompare: false))
		{
			ModCards.PropertyList Schema = ModCards.Statcard_DoT;
			NewCard("DoT", "Drugged", ref Schema, "New Pain");
		}
		else if (Operators.ConditionalCompareObjectEqual(selectedItem, "Explosion", TextCompare: false))
		{
			ModCards.PropertyList Schema = ModCards.Statcard_Explosion;
			NewCard("Explosion", "Grenade", ref Schema, "New Explosion");
		}
		else if (Operators.ConditionalCompareObjectEqual(selectedItem, "SoundPack", TextCompare: false))
		{
			ModCards.PropertyList Schema = ModCards.Statcard_SoundPack;
			NewCard("SoundPack", "M_Bat", ref Schema, "New Sound Pack");
		}
		else if (Operators.ConditionalCompareObjectEqual(selectedItem, "Swatch", TextCompare: false))
		{
			ModCards.PropertyList Schema = ModCards.Statcard_Swatch;
			NewCard("Swatch", "Tint_Ew", ref Schema, "New Colors");
		}
		else if (Operators.ConditionalCompareObjectEqual(selectedItem, "Character", TextCompare: false))
		{
			ModCards.PropertyList Schema = ModCards.Statcard_Character;
			NewCard("Character", "0_Blank", ref Schema, "Original Character");
		}
		else if (Operators.ConditionalCompareObjectEqual(selectedItem, "Animations", TextCompare: false))
		{
			ModCards.PropertyList Schema = ModCards.Statcard_Animation;
			NewCard("Animations", "Idle_Gen", ref Schema, "New Moves");
		}
		else if (Operators.ConditionalCompareObjectEqual(selectedItem, "AnimationProfile", TextCompare: false))
		{
			ModCards.PropertyList Schema = ModCards.Statcard_AnimSet;
			NewCard("AnimationProfile", "Default", ref Schema, "New Profile");
		}
		else if (Operators.ConditionalCompareObjectEqual(selectedItem, "Chatter", TextCompare: false))
		{
			ModCards.PropertyList Schema = ModCards.Statcard_Chatter;
			NewCard("Chatter", "0_Empty", ref Schema, "New Chatter");
		}
		else if (Operators.ConditionalCompareObjectEqual(selectedItem, "Corpus", TextCompare: false))
		{
			ModCards.PropertyList Schema = ModCards.Statcard_Corpus;
			NewCard("Corpus", "Basic", ref Schema, "New Corpus Block");
		}
		else if (Operators.ConditionalCompareObjectEqual(selectedItem, "Execution", TextCompare: false))
		{
			ModCards.PropertyList Schema = ModCards.Statcard_Execution;
			NewCard("Execution", "0_Standard_U", ref Schema, "New Execution");
		}
		else if (Operators.ConditionalCompareObjectEqual(selectedItem, "Skin", TextCompare: false))
		{
			ModCards.PropertyList Schema = ModCards.Statcard_Skin;
			NewCard("Skin", "0_Default", ref Schema, "New Look");
		}
		else if (Operators.ConditionalCompareObjectEqual(selectedItem, "Skill", TextCompare: false))
		{
			ModCards.PropertyList Schema = ModCards.Statcard_Skill;
			NewCard("Skill", "C_U1", ref Schema, "New Skillz");
		}
		else if (Operators.ConditionalCompareObjectEqual(selectedItem, "SkillGroup", TextCompare: false))
		{
			ModCards.PropertyList Schema = ModCards.Statcard_Skillgroup;
			NewCard("SkillGroup", "AAHW_Base", ref Schema, "New Skillz");
		}
		else if (Operators.ConditionalCompareObjectEqual(selectedItem, "VisualStockpile", TextCompare: false))
		{
			ModCards.PropertyList Schema = ModCards.Statcard_Texture;
			NewCard("VisualStockpile", "M_Bat", ref Schema, "New Visual Stockpile");
		}
		else if (Operators.ConditionalCompareObjectEqual(selectedItem, "VoiceSet", TextCompare: false))
		{
			ModCards.PropertyList Schema = ModCards.Statcard_VoiceSet;
			NewCard("VoiceSet", "0_VoiceSet", ref Schema, "New Voice Set");
		}
		else if (Operators.ConditionalCompareObjectEqual(selectedItem, "Loadout", TextCompare: false))
		{
			ModCards.PropertyList Schema = ModCards.Statcard_Loadout;
			NewCard("Loadout", "Warchest_Weapons", ref Schema, "New Loadout");
		}
		else if (Operators.ConditionalCompareObjectEqual(selectedItem, "Origin", TextCompare: false))
		{
			ModCards.PropertyList Schema = ModCards.Statcard_Origin;
			NewCard("Origin", "None", ref Schema, "New Origin");
		}
		else if (Operators.ConditionalCompareObjectEqual(selectedItem, "Wardrobe", TextCompare: false))
		{
			ModCards.PropertyList Schema = ModCards.Statcard_Wardrobe;
			NewCard("Wardrobe", "S_Hank_1", ref Schema, "New Drip");
		}
		else if (Operators.ConditionalCompareObjectEqual(selectedItem, "Event", TextCompare: false))
		{
			ModCards.PropertyList Schema = ModCards.StatCard_Event;
			NewCard("Event", "0_Event", ref Schema, "New Event");
		}
		else if (Operators.ConditionalCompareObjectEqual(selectedItem, "Cutscene", TextCompare: false))
		{
			ModCards.PropertyList Schema = ModCards.StatCard_Cutscene;
			NewCard("Cutscene", "0_Cutscene", ref Schema, "New Cutscene");
		}
		else if (Operators.ConditionalCompareObjectEqual(selectedItem, "Driver", TextCompare: false))
		{
			ModCards.PropertyList Schema = ModCards.StatCard_Driver;
			NewCard("Driver", "0_Driver", ref Schema, "New Driver");
		}
		else if (Operators.ConditionalCompareObjectEqual(selectedItem, "Mission", TextCompare: false))
		{
			ModCards.PropertyList Schema = ModCards.Statcard_Mission;
			NewCard("Mission", "0_Mission", ref Schema, "New Mission");
		}
		else if (Operators.ConditionalCompareObjectEqual(selectedItem, "Stage_Arena", TextCompare: false))
		{
			ModCards.PropertyList Schema = ModCards.Statcard_Arena;
			NewCard("Stage_Arena", "Arena_0_Tutorial", ref Schema, "Arena_0_Tutorial");
		}
		else if (Operators.ConditionalCompareObjectEqual(selectedItem, "Stage_Story", TextCompare: false))
		{
			ModCards.PropertyList Schema = ModCards.Statcard_Story;
			NewCard("Stage_Story", "S_Outs_0", ref Schema, "S_Outs_0");
		}
		else if (Operators.ConditionalCompareObjectEqual(selectedItem, "Stage_Custom", TextCompare: false))
		{
			ModCards.PropertyList Schema = ModCards.Statcard_CustomStage;
			NewCard("Stage_Custom", "0_Stage", ref Schema, "New Stage");
		}
		else if (Operators.ConditionalCompareObjectEqual(selectedItem, "Campaign", TextCompare: false))
		{
			ModCards.PropertyList Schema = ModCards.Statcard_Campaign;
			NewCard("Campaign", "0_Campaign", ref Schema, "New Campaign");
		}
	}

	private void CardRoot_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (CardRoot.SelectedItem != null && RootableEntries != null && RootableEntries.ContainsKey(CardRoot.SelectedItem.ToString()) && !Loading)
		{
			Reset();
			string key = CardRoot.SelectedItem.ToString();
			NotifyAddedProperties = true;
			LoadObject(ObjEditor.Nodes, ModNodes.FinalVersion(SaveType, RootableEntries[key]), ControlScheme);
			NotifyAddedProperties = false;
			if (!FileName.Enabled)
			{
				FileName.Text = key;
			}
			MarkUnsaved(edited: true);
			DisplayPrice();
		}
	}

	private void ObjEditor_KeyPress(object sender, KeyPressEventArgs e)
	{
		e.Handled = true;
	}

	private void CardType_KeyPress(object sender, KeyPressEventArgs e)
	{
		e.Handled = true;
	}

	private void CardRoot_KeyPress(object sender, KeyPressEventArgs e)
	{
		e.Handled = true;
	}

	private void Action_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.ControlKey)
		{
			DisplayActions(controlOptions: true);
		}
		if ((e.KeyCode == Keys.Return) & ActionButton.Enabled)
		{
			e.SuppressKeyPress = true;
			ButtonAction();
		}
		else if ((e.KeyCode == Keys.Right) & (ListObjects.SelectedItem != null))
		{
			if (!ListObjects.Focused & !Param.Focused)
			{
				ListObjects.Focus();
			}
			if (Param.Focused)
			{
				Param.Text = ListObjects.SelectedItem.ToString();
			}
		}
		else if (e.KeyCode == Keys.Left)
		{
			if (!ObjEditor.Focused & !Param.Focused)
			{
				ObjEditor.Focus();
			}
		}
		else if (Param.Enabled & !Param.Focused & (e.KeyCode != Keys.ControlKey) & (e.KeyCode != Keys.Up) & (e.KeyCode != Keys.Down))
		{
			Param.Focus();
		}
	}

	private void Action_KeyUp(object sender, KeyEventArgs e)
	{
		DisplayActions();
	}

	private void ObjEditor_AfterSelect(object sender, TreeViewEventArgs e)
	{
		if (ObjEditor.SelectedNode == null)
		{
			return;
		}
		Loading = true;
		ActionButton.Enabled = true;
		ExpandCurrentToolStripMenuItem.Enabled = true;
		CollapseCurrentToolStripMenuItem.Enabled = true;
		SelectedTag = (ModCards.NodeTag)ObjEditor.SelectedNode.Tag;
		ModCards.PropertyControl linkedControl = SelectedTag.LinkedControl;
		Param.Enabled = (linkedControl.PropType != ModCards.PropType.Bool) & (linkedControl.PropType != ModCards.PropType.Obj);
		ColorWheelToolStripMenuItem.Enabled = linkedControl.PropType == ModCards.PropType.Color;
		StringEditorToolStripMenuItem.Enabled = linkedControl.PropType == ModCards.PropType.Str;
		RevertToolStripMenuItem.Enabled = SelectedTag.Edited;
		bool flag = linkedControl.IsList & !linkedControl.Fixed & (SelectedTag.NodeType != ModCards.NodeType.Arr);
		DuplicateToolStripMenuItem.Enabled = flag;
		ShiftUpToolStripMenuItem.Enabled = flag;
		ShiftDownToolStripMenuItem.Enabled = flag;
		LoadFromSelectedToolStripMenuItem.Enabled = false;
		bool enabled = linkedControl.IsList & (SelectedTag.NodeType == ModCards.NodeType.Arr);
		ExpandCurrentToolStripMenuItem.Enabled = enabled;
		CollapseCurrentToolStripMenuItem.Enabled = enabled;
		if (flag & (linkedControl.PropType == ModCards.PropType.Ref))
		{
			EntryToolTip(SelectedTag.Value.ToString());
		}
		else
		{
			string objType = SaveType;
			if (ObjEditor.SelectedNode.Parent != null && Operators.ConditionalCompareObjectEqual(NewLateBinding.LateGet(ObjEditor.SelectedNode.Parent.Tag, null, "NodeType", new object[0], null, null, null), ModCards.NodeType.Obj, TextCompare: false))
			{
				objType = Conversions.ToString(NewLateBinding.LateGet(NewLateBinding.LateGet(ObjEditor.SelectedNode.Parent.Tag, null, "LinkedControl", new object[0], null, null, null), null, "ObjectName", new object[0], null, null, null));
			}
			PropertyToolTip(SelectedTag, objType);
		}
		DisplayActions();
		Param.Text = ((Param.Enabled & (SelectedTag.NodeType == ModCards.NodeType.Val)) ? SelectedTag.Value.ToString() : "");
		if ((linkedControl.PropType == ModCards.PropType.Ref) & (linkedControl.Selection != null))
		{
			ListObjects.DataSource = RuntimeHelpers.GetObjectValue((Operators.CompareString(SaveType, linkedControl.ObjectName, TextCompare: false) == 0) ? ((string[])linkedControl.Selection).Where([SpecialName] (string s) => ModNodes.LoadedPrior(SaveType, s, PreloadPriority)).ToArray() : linkedControl.Selection);
		}
		else
		{
			ListObjects.DataSource = null;
		}
		Loading = false;
	}

	private void Param_TextChanged(object sender, EventArgs e)
	{
		if ((SelectedTag != null) & !Loading)
		{
			EvaluateParameter(ObjEditor.SelectedNode, SelectedTag);
		}
	}

	private void Param_KeyDown(object sender, KeyEventArgs e)
	{
		if (((e.KeyCode == Keys.Return) & (SelectedTag != null)) && Operators.CompareString(SelectedTag.LinkedControl.PropName, "Description", TextCompare: false) == 0)
		{
			int selectionStart = Param.SelectionStart;
			Param.Text = Param.Text.Substring(0, selectionStart) + "\r\n" + Param.Text.Substring(selectionStart, checked(Param.Text.Length - selectionStart));
			Param.SelectionStart = selectionStart;
		}
	}

	private void ListObjects_SelectedIndexChanged(object sender, EventArgs e)
	{
		if ((SelectedTag != null) & (ListObjects.SelectedItem != null) & !Loading)
		{
			EvaluateSelected(ObjEditor.SelectedNode, SelectedTag);
		}
	}

	private void ActionButton_Click(object sender, EventArgs e)
	{
		ButtonAction();
	}

	internal void NewCard(string type, string loadEntry, ref ModCards.PropertyList Schema, string newName = "")
	{
		if (Operators.CompareString(SaveType, type, TextCompare: false) != 0)
		{
			EditedProperties.Clear();
		}
		Reset();
		SaveType = type;
		LoadableEntries = ModEnums.MC_DIR[SaveType];
		RootableEntries = new SortedDictionary<string, JObject>(ModEnums.SC_DIR[SaveType]);
		foreach (KeyValuePair<string, JObject> loadableEntry in LoadableEntries)
		{
			string iD = SaveType + ":" + loadableEntry.Key;
			if (ModNodes.ReturnVersionCount(iD, PreloadPriority) > 0)
			{
				JObject value = ModNodes.ReturnCardInfo(iD, PreloadPriority);
				if (RootableEntries.ContainsKey(loadableEntry.Key))
				{
					RootableEntries[loadableEntry.Key] = value;
				}
				else
				{
					RootableEntries.Add(loadableEntry.Key, value);
				}
			}
		}
		SavedList = Schema;
		ControlScheme = Schema();
		FileName.Text = ((PreloadFile.Length > 0) ? PreloadFile : newName);
		FileName.Enabled = true;
		SaveToolStripMenuItem.Enabled = true;
		LoadToolStripMenuItem.Enabled = true;
		ExpandAllToolStripMenuItem.Enabled = true;
		CollapseAllToolStripMenuItem.Enabled = true;
		ShowInfoToolStripMenuItem.Enabled = true;
		ShowPriceToolStripMenuItem.Enabled = IsPriced();
		LoadFromSelectedToolStripMenuItem.Enabled = false;
		Price.Visible = ShowPriceToolStripMenuItem.Checked & IsPriced();
		CardRoot.DataSource = RootableEntries.Keys.ToArray();
		bool hasImportedDraft=PreloadFile.Length>0;
		if (PreloadFile.Length > 0)
		{
			_ = SaveType + ":" + PreloadFile;
			JObject jObject = JObject.Parse(PreloadFileInfo);
			LoadObject(ObjEditor.Nodes, ModNodes.FinalVersion(SaveType, jObject), ControlScheme);
			MarkEditedNodes(jObject);
			PreloadFile = "";
			PreloadFileInfo = "";
		}
		Loading=hasImportedDraft;
		try{CardRoot.SelectedItem = ((PreloadRoot.Length > 0) ? PreloadRoot : loadEntry);}
		finally{Loading=false;}
		PreloadRoot = "";
		CardRoot.Enabled = !NoRoots.Contains(type);
		MarkUnsaved(edited: true);
		GeneralToolTip();
		DisplayPrice();
        ResetModernHistory();
	}

	public void RefreshCard()
	{
		bool unsaved = Unsaved;
		string loadEntry = CardRoot.SelectedItem.ToString();
		NewCard(SaveType, loadEntry, ref SavedList, FileName.Text);
		foreach (TreeNode value in EditedProperties.Values)
		{
			UpdateMarkedSelections(value);
		}
		MarkUnsaved(unsaved);
	}

	private void LoadToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (LoadableEntries.Count == 0)
		{
			DossierDialog.MsgBox("No entries to load.", MsgBoxStyle.Exclamation, "No Entries");
			return;
		}
		DialogCreateEntry dialogCreateEntry = new DialogCreateEntry
		{
			Text = "Load Entry",
			CardType = SaveType,
			Database = LoadableEntries.Keys.ToArray()
		};
		dialogCreateEntry.ComboEntrySelect.AutoCompleteMode = AutoCompleteMode.Append;
		dialogCreateEntry.Action = "Load";
		if ((dialogCreateEntry.ShowDialog() == DialogResult.OK) & (dialogCreateEntry.ComboEntrySelect.SelectedItem != null))
		{
			Reset();
			string key = dialogCreateEntry.ComboEntrySelect.SelectedItem.ToString();
			FileName.Text = key;
			NotifyAddedProperties = true;
			LoadObject(ObjEditor.Nodes, ModNodes.FinalVersion(SaveType, LoadableEntries[key]), ControlScheme);
			MarkEditedNodes(LoadableEntries[key]);
			MarkUnsaved(edited: false);
			NotifyAddedProperties = false;
			DisplayPrice();
		}
	}

	private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
	{
		SaveToDirectory("");
	}

	private void ColorWheelToolStripMenuItem_Click(object sender, EventArgs e)
	{
		ColorPickerDialog colorPickerDialog = new ColorPickerDialog();
		ModCards.ColorRGBA colorRGBA = (ModCards.ColorRGBA)((SelectedTag.Value != null) ? SelectedTag.Value : new ModCards.ColorRGBA());
		colorPickerDialog.Color = colorRGBA.AsColor();
		if (colorPickerDialog.ShowDialog() != DialogResult.Cancel)
		{
			if (SelectedTag.Value != null)
			{
				SelectedTag.Value = NewColorFromHex(colorPickerDialog.Color);
			}
			Param.Text = ColorTranslator.ToHtml(colorPickerDialog.Color);
		}
		DisplayActions();
	}

	private void StringEditorToolStripMenuItem_Click(object sender, EventArgs e)
	{
		DialogTextEditor dialogTextEditor = new DialogTextEditor();
		dialogTextEditor.IncludeText = Conversions.ToString((SelectedTag.Value != null) ? SelectedTag.Value : "");
		if (dialogTextEditor.ShowDialog() == DialogResult.OK)
		{
			if (SelectedTag.Value != null)
			{
				SelectedTag.Value = dialogTextEditor.IncludeText;
			}
			Param.Text = dialogTextEditor.IncludeText;
		}
	}

	private void DuplicateObjectToolStripMenuItem_Click(object sender, EventArgs e)
	{
		JToken token = SerializeNode(ObjEditor.SelectedNode, SelectedTag);
		ObjEditor.SelectedNode.Parent.Nodes.Add(NewPropertyNode(SelectedTag.LinkedControl, token));
		HighlightAllNodes(ModNodes.GetRootNode(ObjEditor.SelectedNode));
		MarkUnsaved(edited: true);
	}

	private void RevertToolStripMenuItem_Click(object sender, EventArgs e)
	{
		string key = CardRoot.SelectedItem.ToString();
		ModCards.PropertyControl linkedControl = SelectedTag.LinkedControl;
		string propName = linkedControl.PropName;
		JToken token = ModNodes.FinalVersion(SaveType, RootableEntries[key])[linkedControl.PropName];
		int index = ObjEditor.SelectedNode.Index;
		TreeNode treeNode = ((!DataMatches(linkedControl, token)) ? DefaultNode(linkedControl, UseDefaultValues: true) : NewPropertyNode(linkedControl, token, isParent: true));
		ObjEditor.Nodes.Remove(ObjEditor.SelectedNode);
		ObjEditor.Nodes.Insert(index, treeNode);
		ObjEditor.SelectedNode = treeNode;
		if (EditedProperties.ContainsKey(propName))
		{
			EditedProperties.Remove(propName);
		}
		RevertAllToolStripMenuItem.Enabled = EditedProperties.Count > 0;
		MarkUnsaved(edited: true);
		DisplayPrice();
	}

	private void RevertAllToolStripMenuItem_Click(object sender, EventArgs e)
	{
		EditedProperties.Clear();
		NotifyAddedProperties = true;
		LoadObject(ObjEditor.Nodes, ModNodes.FinalVersion(SaveType, RootableEntries[CardRoot.SelectedItem.ToString()]), ControlScheme);
		NotifyAddedProperties = false;
		DisplayPrice();
	}

	private void ShiftUpToolStripMenuItem_Click(object sender, EventArgs e)
	{
		TreeNode selectedNode = ObjEditor.SelectedNode;
		if (ObjEditor.SelectedNode.Parent != null)
		{
			TreeNodeCollection nodes = ObjEditor.SelectedNode.Parent.Nodes;
			int index = ObjEditor.SelectedNode.Index;
			if (index > 0)
			{
				selectedNode.Remove();
				nodes.Insert(checked(index - 1), selectedNode);
				ObjEditor.SelectedNode = selectedNode;
				HighlightAllNodes(ModNodes.GetRootNode(ObjEditor.SelectedNode));
				MarkUnsaved(edited: true);
				ObjEditor.Focus();
			}
		}
	}

	private void ShiftDownToolStripMenuItem_Click(object sender, EventArgs e)
	{
		TreeNode selectedNode = ObjEditor.SelectedNode;
		checked
		{
			if (ObjEditor.SelectedNode.Parent != null)
			{
				TreeNodeCollection nodes = ObjEditor.SelectedNode.Parent.Nodes;
				int index = ObjEditor.SelectedNode.Index;
				if (index < nodes.Count - 1)
				{
					selectedNode.Remove();
					nodes.Insert(index + 1, selectedNode);
					ObjEditor.SelectedNode = selectedNode;
					HighlightAllNodes(ModNodes.GetRootNode(ObjEditor.SelectedNode));
					MarkUnsaved(edited: true);
					ObjEditor.Focus();
				}
			}
		}
	}

	private void LoadFromSelectedToolStripMenuItem_Click(object sender, EventArgs e)
	{
		FormEditor formEditor = new FormEditor();
		formEditor.Manager = Manager;
		formEditor.PreloadType = SelectedTag.LinkedControl.ObjectName;
		formEditor.PreloadRoot = ListObjects.SelectedItem.ToString();
		StudioModule.ShowDocument(formEditor);
	}

	private void ExpandCurrrentToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (ObjEditor.SelectedNode != null)
		{
			ObjEditor.SelectedNode.ExpandAll();
		}
	}

	private void CollapseCurrentToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (ObjEditor.SelectedNode != null)
		{
			ObjEditor.SelectedNode.Collapse();
		}
	}

	private void ExpandAllToolStripMenuItem_Click(object sender, EventArgs e)
	{
		foreach (TreeNode node in ObjEditor.Nodes)
		{
			node.ExpandAll();
		}
	}

	private void CollapseAllToolStripMenuItem_Click(object sender, EventArgs e)
	{
		foreach (TreeNode node in ObjEditor.Nodes)
		{
			node.Collapse();
		}
	}

	private void ShowInfoToolStripMenuItem_Click(object sender, EventArgs e)
	{
		InfoTab.Visible = ShowInfoToolStripMenuItem.Checked;
	}

	private void ShowPriceToolStripMenuItem_Click(object sender, EventArgs e)
	{
		Price.Visible = ShowPriceToolStripMenuItem.Checked;
	}

	private bool SaveToDirectory(string saveDir)
	{
		try{
		if(!ValidateStudioSave())return false;
		if (Manager != null && Manager.CardManager.SelectedNode != null)
		{
			JObject jObject = SaveObj(ObjEditor.Nodes);
			if (saveDir.Length > 0)
			{
				MakerHelp.Write(saveDir, jObject.ToString());
			}
			ModCards.CardTag cardTag = new ModCards.CardTag(FileName.Text, SaveType, CardRoot.SelectedItem.ToString(), jObject)
			{
				AttachedDeck = ModNodes.GetRootNode(Manager.CardManager.SelectedNode).Name
			};
			if (ObjEditor.Nodes["Name"] != null)
			{
				cardTag.DisplayName = NewLateBinding.LateGet(ObjEditor.Nodes["Name"].Tag, null, "Value", new object[0], null, null, null).ToString();
			}
			if (ObjEditor.Nodes["Description"] != null)
			{
				cardTag.Description = NewLateBinding.LateGet(ObjEditor.Nodes["Description"].Tag, null, "Value", new object[0], null, null, null).ToString();
			}
			cardTag.Converter = UnlistAllValues;
			if (IsPriced())
			{
				cardTag.DisplayPrice = Price.Text;
			}
			Manager.AddCard(ModNodes.GetRootNode(Manager.CardManager.SelectedNode), cardTag, SelectAfter: true, ModNodes.GetRootPriority(cardTag.Type, cardTag.RootName, GetSameTypeRefs(jObject)));
			MarkUnsaved(edited: false);
			modernStatus.Text="Card saved to deck. Use Save decks if automatic saving is off.";
			DossierFeedback.Play("save");
			return true;
		}
		else
		{
			DossierDialog.MsgBox("No deck selected. Go select one!", MsgBoxStyle.Exclamation, "No Deck");
		}
		return false;
		}catch(Exception ex){DossierDialog.Show(this,"The card was not saved.\r\n"+ex.Message,"Save failed");return false;}
	}

	private JObject SaveObj(TreeNodeCollection nodeSpace, bool SaveAll = false)
	{
		JObject jObject = new JObject();
		if (!SaveAll)
		{
			jObject.Add("Filename", FileName.Text);
			jObject.Add("Root", CardRoot.SelectedItem.ToString());
		}
		foreach (TreeNode item in nodeSpace)
		{
			if (SaveAll | EditedProperties.ContainsKey(item.Name))
			{
				jObject.Add(item.Name, SerializeNode(item, (ModCards.NodeTag)item.Tag));
			}
		}
		return jObject;
	}

	private JToken SerializeNode(TreeNode node, ModCards.NodeTag nodetag)
	{
		ModCards.PropertyControl linkedControl = nodetag.LinkedControl;
		switch (nodetag.NodeType)
		{
		case ModCards.NodeType.Val:
		{
			ModCards.PropType propType = linkedControl.PropType;
			if (propType == ModCards.PropType.Color)
			{
				ModCards.ColorRGBA colorRGBA = (ModCards.ColorRGBA)nodetag.Value;
				return new JObject
				{
					{ "r", colorRGBA.r },
					{ "g", colorRGBA.g },
					{ "b", colorRGBA.b },
					{ "a", colorRGBA.a }
				};
			}
			return new JValue(RuntimeHelpers.GetObjectValue(nodetag.Value));
		}
		case ModCards.NodeType.Arr:
		{
			JArray jArray = new JArray();
			foreach (TreeNode node2 in node.Nodes)
			{
				jArray.Add(SerializeNode(node2, (ModCards.NodeTag)node2.Tag));
			}
			return jArray;
		}
		case ModCards.NodeType.Obj:
			return SaveObj(node.Nodes, SaveAll: true);
		default:
			return null;
		}
	}

	private void LoadObject(TreeNodeCollection nodeSpace, JObject card, ModCards.PropertyControl[] scheme, bool MindEdits = true, bool KeepNodes = false)
	{
		if (!KeepNodes)
		{
			nodeSpace.Clear();
		}
		int num = 0;
		string text = "";
		checked
		{
			foreach (ModCards.PropertyControl propertyControl in scheme)
			{
				if (UnlistAllValues & (propertyControl.PropType == ModCards.PropType.Ref))
				{
					propertyControl.WriteNewValues = true;
				}
				if (MindEdits & EditedProperties.ContainsKey(propertyControl.PropName))
				{
					EditedProperties[propertyControl.PropName].Remove();
					nodeSpace.Add(EditedProperties[propertyControl.PropName]);
				}
				else if (DataMatches(propertyControl, card[propertyControl.PropName]))
				{
					TreeNode treeNode = NewPropertyNode(propertyControl, card[propertyControl.PropName], isParent: true);
					nodeSpace.Add(treeNode);
					if (propertyControl.Defines != null)
					{
						ModCards.NodeTag tag = (ModCards.NodeTag)treeNode.Tag;
						MorphProperties(treeNode, tag, card);
					}
				}
				else if (IsDeferred(propertyControl, card[propertyControl.PropName]))
				{
					nodeSpace.Add(NewPropertyNode(propertyControl, RootableEntries[(string)card[propertyControl.PropName]][propertyControl.PropName], isParent: true));
					num++;
					text = text + propertyControl.PropName + " ";
				}
				else
				{
					nodeSpace.Add(DefaultNode(propertyControl, UseDefaultValues: true));
					num++;
					text = text + propertyControl.PropName + " ";
				}
			}
			if ((num > 0) & NotifyAddedProperties)
			{
				DossierDialog.MsgBox(Conversions.ToString(num) + " new properties have been added to this object; Save your work to write them in for compatibility.\r\n( " + text + ")", MsgBoxStyle.Information, "Updated Properites");
			}
		}
	}

	private bool DataMatches(ModCards.PropertyControl control, JToken token)
	{
		if (token == null)
		{
			return false;
		}
		if (control.IsList)
		{
			return token.Type == JTokenType.Array;
		}
		switch (control.PropType)
		{
		case ModCards.PropType.Color:
		case ModCards.PropType.Obj:
			return token.Type == JTokenType.Object;
		case ModCards.PropType.Int:
		{
			string s2 = (string)token;
			CultureInfo invariantCulture2 = CultureInfo.InvariantCulture;
			int result3 = default;
			return int.TryParse(s2, NumberStyles.Integer, invariantCulture2, out result3);
		}
		case ModCards.PropType.Float:
		{
			string s = (string)token;
			CultureInfo invariantCulture = CultureInfo.InvariantCulture;
			double result2 = default;
			return double.TryParse(s, NumberStyles.Float, invariantCulture, out result2);
		}
		case ModCards.PropType.Bool:
		{
			string value = (string)token;
			bool result = default;
			return bool.TryParse(value, out result);
		}
		default:
			return true;
		}
	}

	private bool IsDeferred(ModCards.PropertyControl control, JToken token)
	{
		if (token == null)
		{
			return false;
		}
		if (!RootableEntries.ContainsKey(token.ToString()))
		{
			return false;
		}
		return DataMatches(control, RootableEntries[token.ToString()][control.PropName]);
	}

	private TreeNode NewPropertyNode(ModCards.PropertyControl control, JToken token, bool isParent = false)
	{
		TreeNode treeNode = new TreeNode(control.PropName);
		if (isParent)
		{
			treeNode.Name = control.PropName;
		}
		if (control.IsList & (token.Type == JTokenType.Array))
		{
			treeNode.Tag = new ModCards.NodeTag(control, ModCards.NodeType.Arr);
			foreach (JToken item in token.Children())
			{
				treeNode.Nodes.Add(NewPropertyNode(control, item));
			}
		}
		else if (control.PropType == ModCards.PropType.Obj)
		{
			treeNode.Tag = new ModCards.NodeTag(control, ModCards.NodeType.Obj);
			LoadObject(treeNode.Nodes, JObject.Parse(token.ToString()), (ModCards.PropertyControl[])control.Selection, MindEdits: false, KeepNodes: true);
		}
		else
		{
			treeNode.Tag = new ModCards.NodeTag(control, ModCards.NodeType.Val);
			switch (control.PropType)
			{
			case ModCards.PropType.Color:
			{
				ModCards.ColorRGBA colorRGBA = NewColorFromHex(Color.White);
				double.TryParse(token["r"].ToString(), out colorRGBA.r);
				double.TryParse(token["g"].ToString(), out colorRGBA.g);
				double.TryParse(token["b"].ToString(), out colorRGBA.b);
				double.TryParse(token["a"].ToString(), out colorRGBA.a);
				NewLateBinding.LateSetComplex(treeNode.Tag, null, "Value", new object[1] { colorRGBA }, null, null, OptimisticSet: false, RValueBase: true);
				treeNode.ForeColor = colorRGBA.AsColor();
				break;
			}
			case ModCards.PropType.Float:
			{
				string s2 = token.ToString();
				CultureInfo currentCulture2 = CultureInfo.CurrentCulture;
				object tag;
				double result3 = Conversions.ToDouble(NewLateBinding.LateGet(tag = treeNode.Tag, null, "Value", new object[0], null, null, null));
				double.TryParse(s2, NumberStyles.Float, currentCulture2, out result3);
				NewLateBinding.LateSetComplex(tag, null, "Value", new object[1] { result3 }, null, null, OptimisticSet: true, RValueBase: true);
				break;
			}
			case ModCards.PropType.Int:
			{
				string s = token.ToString();
				CultureInfo currentCulture = CultureInfo.CurrentCulture;
				object tag;
				int result2 = Conversions.ToInteger(NewLateBinding.LateGet(tag = treeNode.Tag, null, "Value", new object[0], null, null, null));
				int.TryParse(s, NumberStyles.Integer, currentCulture, out result2);
				NewLateBinding.LateSetComplex(tag, null, "Value", new object[1] { result2 }, null, null, OptimisticSet: true, RValueBase: true);
				break;
			}
			case ModCards.PropType.Bool:
			{
				string value = token.ToString();
				object tag;
				bool result = Conversions.ToBoolean(NewLateBinding.LateGet(tag = treeNode.Tag, null, "Value", new object[0], null, null, null));
				bool.TryParse(value, out result);
				NewLateBinding.LateSetComplex(tag, null, "Value", new object[1] { result }, null, null, OptimisticSet: true, RValueBase: true);
				break;
			}
			default:
				NewLateBinding.LateSetComplex(treeNode.Tag, null, "Value", new object[1] { token.ToString() }, null, null, OptimisticSet: false, RValueBase: true);
				break;
			}
		}
		treeNode.Text = GetDisplayNode(treeNode);
		return treeNode;
	}

	private TreeNode DefaultNode(ModCards.PropertyControl prop, bool UseDefaultValues = false)
	{
		TreeNode treeNode = new TreeNode();
		ModCards.NodeTag nodeTag = (ModCards.NodeTag)(treeNode.Tag = new ModCards.NodeTag(prop, (prop.PropType == ModCards.PropType.Obj) ? ModCards.NodeType.Obj : ModCards.NodeType.Val));
		if (UseDefaultValues)
		{
			treeNode.Name = prop.PropName;
			if (prop.IsList)
			{
				nodeTag.NodeType = ModCards.NodeType.Arr;
			}
			else if (nodeTag.NodeType == ModCards.NodeType.Obj)
			{
				foreach (ModCards.PropertyControl item in (IEnumerable)prop.Selection)
				{
					treeNode.Nodes.Add(DefaultNode(item, UseDefaultValues: true));
				}
			}
			else
			{
				NewLateBinding.LateSetComplex(treeNode.Tag, null, "Value", new object[1] { GetDefaultValue(prop.PropType, RuntimeHelpers.GetObjectValue((prop.Selection == null) ? null : (Operators.ConditionalCompareObjectGreater(NewLateBinding.LateGet(prop.Selection, null, "Length", new object[0], null, null, null), 0, TextCompare: false) ? NewLateBinding.LateIndexGet(prop.Selection, new object[1] { 0 }, null) : "None"))) }, null, null, OptimisticSet: false, RValueBase: true);
			}
			treeNode.Text = GetDisplayNode(treeNode);
		}
		else if (!Loading & (ListObjects.SelectedItem != null) & !(prop.WriteNewValues & (Param.Text.Length > 0)))
		{
			EvaluateSelected(treeNode, (ModCards.NodeTag)treeNode.Tag);
		}
		else if (prop.PropType != ModCards.PropType.Obj)
		{
			EvaluateParameter(treeNode, (ModCards.NodeTag)treeNode.Tag);
		}
		else
		{
			if (UseDefaultValues)
			{
				treeNode.Name = prop.PropName;
			}
			foreach (ModCards.PropertyControl item2 in (IEnumerable)NewLateBinding.LateGet(NewLateBinding.LateGet(treeNode.Tag, null, "LinkedControl", new object[0], null, null, null), null, "Selection", new object[0], null, null, null))
			{
				treeNode.Nodes.Add(DefaultNode(item2, UseDefaultValues: true));
			}
			treeNode.Text = GetDisplayNode(treeNode);
		}
		return treeNode;
	}

	private void RemoveSelected()
	{
		Loading = true;
		TreeNode selectedNode = ObjEditor.SelectedNode;
		TreeNode selectedNode2;
		if (selectedNode.NextNode != null)
		{
			selectedNode2 = selectedNode.NextNode;
		}
		else
		{
			selectedNode2 = ((selectedNode.PrevNode != null) ? selectedNode.PrevNode : selectedNode.Parent);
		}
		ObjEditor.SelectedNode.Remove();
		ObjEditor.SelectedNode = selectedNode2;
		HighlightAllNodes(ModNodes.GetRootNode(ObjEditor.SelectedNode));
		MarkUnsaved(edited: true);
		Loading = false;
	}

	private ModCards.PropertyControl[] DefineAdditionalProperties(ModCards.NodeTag tag)
	{
		if (tag.LinkedControl.Defines != null && ModEnums.TT_ENUMS[tag.LinkedControl.ObjectName][tag.Value.ToString()] != null)
		{
			JObject jObject = JObject.Parse(ModEnums.TT_ENUMS[tag.LinkedControl.ObjectName][tag.Value.ToString()].ToString());
			if (jObject["Definitions"] != null)
			{
				JObject jObject2 = JObject.Parse(jObject["Definitions"].ToString());
				if (jObject2.Properties().Count() > 0)
				{
					Dictionary<string, string> dictionary = new Dictionary<string, string>();
					Dictionary<string, string[]> dictionary2 = new Dictionary<string, string[]>();
					foreach (JProperty item in jObject2.Properties())
					{
						dictionary.Add(item.Name, (string)item.Value);
					}
					bool unlisted = jObject["AllowUnlisted"] != null;
					if (jObject["Overrides"] != null)
					{
						JObject jObject3 = JObject.Parse(jObject["Overrides"].ToString());
						foreach (JProperty item2 in jObject3.Properties())
						{
							string[] value;
							if (item2.Value.Type == JTokenType.Array)
							{
								List<string> list = new List<string>();
								foreach (JToken item3 in item2.Value.Children())
								{
									list.Add(item3.ToString());
								}
								value = list.ToArray();
							}
							else
							{
								value = ModCards.Nullable(ModCards.Options((string)item2.Value));
							}
							dictionary2.Add(item2.Name, value);
						}
					}
					return ModCards.Filtered(tag.LinkedControl.Defines, dictionary, dictionary2, unlisted);
				}
			}
		}
		return null;
	}

	private void MorphProperties(TreeNode node, ModCards.NodeTag tag, JToken token = null)
	{
		ModCards.PropertyControl[] array = DefineAdditionalProperties(tag);
		TreeNodeCollection nodes = node.Parent.Nodes;
		checked
		{
			if (array != null)
			{
				if (token != null)
				{
					LoadObject(nodes, (JObject)token, array, MindEdits: false, KeepNodes: true);
					return;
				}
				while (Operators.CompareString(nodes[nodes.Count - 1].Name, tag.LinkedControl.PropName, TextCompare: false) != 0)
				{
					nodes.RemoveAt(nodes.Count - 1);
				}
				ModCards.PropertyControl[] array2 = array;
				foreach (ModCards.PropertyControl prop in array2)
				{
					nodes.Add(DefaultNode(prop, UseDefaultValues: true));
				}
			}
			else if ((tag.LinkedControl.Defines != null) & (token == null))
			{
				while (Operators.CompareString(nodes[nodes.Count - 1].Name, tag.LinkedControl.PropName, TextCompare: false) != 0)
				{
					nodes.RemoveAt(nodes.Count - 1);
				}
			}
		}
	}

	private void EvaluateParameter(TreeNode node, ModCards.NodeTag tag)
 {
  if(node==null)return;
  if(tag.LinkedControl.Selection is string[] options){
   Loading=true;
   try{ListObjects.DataSource=options.Where(text=>text.IndexOf(Param.Text,StringComparison.OrdinalIgnoreCase)>=0&&(SaveType!=tag.LinkedControl.ObjectName||ModNodes.LoadedPrior(SaveType,text,PreloadPriority))).ToArray();ListObjects.SelectedIndex=-1;}
   finally{Loading=false;}
   if(!tag.LinkedControl.WriteNewValues){if(modernStatus!=null)modernStatus.Text="Choose a reference from the list to accept it.";return;}
  }
  if(tag.NodeType!=ModCards.NodeType.Val)return;
  object next=tag.Value;
  switch(tag.LinkedControl.PropType){
   case ModCards.PropType.Int:
    if(!int.TryParse(Param.Text,NumberStyles.Integer,CultureInfo.CurrentCulture,out int integer)){if(modernStatus!=null)modernStatus.Text="Enter a whole number. The previous value is kept.";return;}next=integer;break;
   case ModCards.PropType.Float:
    if(!double.TryParse(Param.Text,NumberStyles.Float,CultureInfo.CurrentCulture,out double number)||double.IsNaN(number)||double.IsInfinity(number)){if(modernStatus!=null)modernStatus.Text="Enter a finite number. The previous value is kept.";return;}next=number;break;
   case ModCards.PropType.Bool:return;
   case ModCards.PropType.Color:
    try{var color=ColorTranslator.FromHtml(Param.Text);if(color==Color.Empty)return;next=ColorFromHex(tag.Value as ModCards.ColorRGBA??NewColorFromHex(Color.White),color);}catch{return;}break;
   default:next=Param.Text;break;
  }
  tag.Value=next;node.Text=GetDisplayNode(node);HighlightAllNodes(ModNodes.GetRootNode(node));MarkUnsaved(true);DisplayPrice();if(modernStatus!=null)modernStatus.Text="Value applied. Save to deck when ready.";
 }

 private void EvaluateSelected(TreeNode node, ModCards.NodeTag tag)
	{
		object objectValue = RuntimeHelpers.GetObjectValue(tag.Value);
		if (tag.NodeType == ModCards.NodeType.Val)
		{
			Loading = true;
			tag.Value = RuntimeHelpers.GetObjectValue(ListObjects.SelectedItem);
			node.Text = GetDisplayNode(node);
			if (Conversions.ToBoolean(Operators.AndObject(tag.LinkedControl.Defines != null, Operators.CompareObjectNotEqual(objectValue, ListObjects.SelectedItem, TextCompare: false))))
			{
				MorphProperties(node, tag);
			}
			HighlightAllNodes(ModNodes.GetRootNode(node));
			Loading = false;
			MarkUnsaved(edited: true);
		}
		DisplayPrice();
		string objectName = tag.LinkedControl.ObjectName;
		string key = ListObjects.SelectedItem.ToString();
		LoadFromSelectedToolStripMenuItem.Enabled = CardType.Items.Contains(objectName) && (ModEnums.SC_DIR[objectName].ContainsKey(key) | ModEnums.MC_DIR[objectName].ContainsKey(key));
		EntryToolTip(ListObjects.SelectedItem.ToString());
		DisplayActions();
	}

	private void ButtonAction()
	{
		if ((SelectedTag != null) & !Loading)
		{
			ModCards.PropertyControl linkedControl = SelectedTag.LinkedControl;
			if (linkedControl.IsList & !linkedControl.Fixed)
			{
				if (SelectedTag.NodeType == ModCards.NodeType.Arr)
				{
					if ((linkedControl.PropType != ModCards.PropType.Ref) | (ListObjects.SelectedItem != null) | (linkedControl.WriteNewValues & (Param.Text.Length > 0)))
					{
						HighlightAllNodes(ModNodes.GetRootNode(ObjEditor.SelectedNode));
						TreeNode treeNode = DefaultNode(linkedControl);
						ObjEditor.SelectedNode.Nodes.Add(treeNode);
						HighlightAllNodes(ModNodes.GetRootNode(ObjEditor.SelectedNode));
						ObjEditor.SelectedNode.Expand();
						MarkUnsaved(edited: true);
						treeNode.ExpandAll();
					}
				}
				else
				{
					RemoveSelected();
				}
			}
			else if (SelectedTag.NodeType == ModCards.NodeType.Val)
			{
				if (linkedControl.PropType == ModCards.PropType.Bool)
				{
					SelectedTag.Value = Operators.ConditionalCompareObjectEqual(SelectedTag.Value, false, TextCompare: false);
					ObjEditor.SelectedNode.Text = GetDisplayNode(ObjEditor.SelectedNode);
					HighlightAllNodes(ModNodes.GetRootNode(ObjEditor.SelectedNode));
					MarkUnsaved(edited: true);
				}
				else if (linkedControl.PropType == ModCards.PropType.Color)
				{
					ColorWheelToolStripMenuItem.PerformClick();
				}
			}
		}
		DisplayPrice();
	}

	private void GeneralToolTip()
	{
		InfoTab.Text = "";
		string text = "";
		if (ModEnums.TT_PROPERTIES[SaveType]["Description"] != null)
		{
			text = ModEnums.TT_PROPERTIES[SaveType]["Description"].ToString();
		}
		InfoTab.Text = "[" + SaveType + "]\r\n" + text;
		ModColors.HighlightToolTips(InfoTab, "\\[.+\\]:", ModColors.MenuColors.Highlight);
		ModColors.HighlightToolTips(InfoTab, "\\(.+\\)", ModColors.MenuColors.Note);
		ModColors.HighlightToolTips(InfoTab, "\\{.+\\}", ModColors.MenuColors.Warning);
		ModColors.HighlightToolTips(InfoTab, "\\[[^\\[]+\\]", ModColors.MenuColors.Highlight);
	}

	private void PropertyToolTip(ModCards.NodeTag Tag, string ObjType)
	{
		InfoTab.Text = "";
		ModCards.PropertyControl linkedControl = Tag.LinkedControl;
		string text = ToolTipType(linkedControl);
		string text2 = "";
		string text3 = "";
		string text4 = ((linkedControl.IsList & linkedControl.Fixed) ? "\r\n{Fixed Size}" : "") + (linkedControl.WriteNewValues ? "\r\n(Supports Unlisted Values)" : "") + ((linkedControl.Defines != null) ? "\r\n(Selection defines additional properties)" : "");
		if (ModEnums.TT_PROPERTIES[ObjType]["Properties"][linkedControl.PropName] != null)
		{
			JObject jObject = JObject.Parse(ModEnums.TT_PROPERTIES[ObjType]["Properties"][linkedControl.PropName].ToString());
			if (jObject["Description"] != null)
			{
				text2 = jObject["Description"].ToString();
			}
			if (jObject["Definitions"] != null)
			{
				JObject jObject2 = JObject.Parse(jObject["Definitions"].ToString());
				if (jObject2.Properties().Count() > 0)
				{
					foreach (JProperty item in jObject2.Properties())
					{
						text3 = text3 + "\r\n[" + item.Name + "]: " + item.Value.ToString();
					}
				}
			}
			if (jObject["Notes"] != null)
			{
				foreach (JToken item2 in jObject["Notes"].Children())
				{
					text4 = text4 + "\r\n(" + item2.ToString() + ")";
				}
			}
			if (jObject["Warnings"] != null)
			{
				foreach (JToken item3 in jObject["Warnings"].Children())
				{
					text4 = text4 + "\r\n{" + item3.ToString() + "}";
				}
			}
		}
		else if (linkedControl.OverrideDesc.Length > 0)
		{
			text2 = linkedControl.OverrideDesc;
		}
		if (Operators.CompareString(text, SaveType, TextCompare: false) == 0)
		{
			text4 += "\r\n{Only objects of greater priority are valid in same-type references}";
		}
		string text5 = linkedControl.PropName + ": " + (linkedControl.IsList ? ("List<" + text + ">") : text) + "\r\n" + text2 + text4;
		int length = text5.Length;
		int length2 = text3.Length;
		InfoTab.Text = text5 + text3;
		InfoTab.Select(0, linkedControl.PropName.Length);
		InfoTab.SelectionColor = ModColors.MenuColors.Highlight;
		InfoTab.Select(checked(InfoTab.Text.IndexOf(linkedControl.IsList ? "<" : " ") + 1), text.Length);
		InfoTab.SelectionColor = ToolTipColor(linkedControl);
		if (length2 > 0)
		{
			InfoTab.Select(length, length2);
			InfoTab.SelectionBullet = true;
		}
		ModColors.HighlightToolTips(InfoTab, "\\[.+\\]:", ModColors.MenuColors.Highlight);
		ModColors.HighlightToolTips(InfoTab, "\\(.+\\)", ModColors.MenuColors.Note);
		ModColors.HighlightToolTips(InfoTab, "\\{.+\\}", ModColors.MenuColors.Warning);
		ModColors.HighlightToolTips(InfoTab, "\\[[^\\[]+\\]", ModColors.MenuColors.Highlight);
	}

	private void EnumToolTip(string EType, string Name)
	{
		string text = "";
		string text2 = "";
		string text3 = "";
		if (ModEnums.TT_ENUMS[EType][Name] != null)
		{
			JObject jObject = JObject.Parse(ModEnums.TT_ENUMS[EType][Name].ToString());
			if (jObject["Description"] != null)
			{
				text = jObject["Description"].ToString();
			}
			if (jObject["Definitions"] != null)
			{
				JObject jObject2 = JObject.Parse(jObject["Definitions"].ToString());
				if (jObject2.Properties().Count() > 0)
				{
					foreach (JProperty item in jObject2.Properties())
					{
						text2 = text2 + "\r\n[" + item.Name + "]: " + item.Value.ToString();
					}
				}
			}
			if (jObject["Notes"] != null)
			{
				foreach (JToken item2 in jObject["Notes"].Children())
				{
					text3 = text3 + "\r\n(" + item2.ToString() + ")";
				}
			}
			if (jObject["Warnings"] != null)
			{
				foreach (JToken item3 in jObject["Warnings"].Children())
				{
					text3 = text3 + "\r\n{" + item3.ToString() + "}";
				}
			}
		}
		string text4 = "[" + Name + "]: \r\n" + text + text3;
		int length = text4.Length;
		int length2 = text2.Length;
		InfoTab.Text = text4 + text2;
		if (length2 > 0)
		{
			InfoTab.Select(length, length2);
			InfoTab.SelectionBullet = true;
		}
		InfoTab.Select(0, InfoTab.Text.Length);
		InfoTab.SelectionColor = ModColors.MenuColors.Text;
		ModColors.HighlightToolTips(InfoTab, "\\[.+\\]:", ModColors.MenuColors.Highlight);
		ModColors.HighlightToolTips(InfoTab, "\\(.+\\)", ModColors.MenuColors.Note);
		ModColors.HighlightToolTips(InfoTab, "\\{.+\\}", ModColors.MenuColors.Warning);
		ModColors.HighlightToolTips(InfoTab, "\\[[^\\[]+\\]", ModColors.MenuColors.Highlight);
	}

	private void AssetToolTip(string AType, string Name)
	{
		string text = "";
		if (ModEnums.AssetByMod.ContainsKey(AType + "|" + Name))
		{
			Console.WriteLine(ModEnums.AssetByMod[AType + "|" + Name]);
			text = ((Operators.CompareString(ModEnums.AssetByMod[AType + "|" + Name], "V", TextCompare: false) == 0) ? "Vanilla Asset" : ("Modded Asset " + ModEnums.AssetByMod[AType + "|" + Name]));
		}
		InfoTab.Text = "[" + Name + "]: \r\n" + text;
		ModColors.HighlightToolTips(InfoTab, "\\{.+\\}", Color.Thistle);
		ModColors.HighlightToolTips(InfoTab, "\\[.+\\]:", ModColors.MenuColors.Highlight);
	}

	private void EntryToolTip(string ID)
	{
		string objectName = SelectedTag.LinkedControl.ObjectName;
		if (ModEnums.TT_ENUMS[objectName] != null)
		{
			EnumToolTip(objectName, ID);
			return;
		}
		if (ModEnums.TT_ASSETS[objectName] != null)
		{
			AssetToolTip(objectName, ID);
			return;
		}
		if (Operators.CompareString(ID, "None", TextCompare: false) == 0)
		{
			InfoTab.Text = "[None]";
			InfoTab.Select(0, InfoTab.Text.Length);
			InfoTab.SelectionColor = ModColors.MenuColors.Highlight;
			return;
		}
		InfoTab.Text = "";
		string text = "";
		string text2 = "";
		string text3 = "";
		string text4 = FindFromGeneralType(objectName, ID);
		if (ModEnums.SC_DIR.ContainsKey(text4))
		{
			JObject info = new JObject
			{
				{
					"Root",
					new JValue(ID)
				},
				{
					"Priority",
					new JValue(2147483647L)
				}
			};
			JObject jObject = ModNodes.FinalVersion(text4, info, IncludeModSource: true);
			text = ((jObject["Name"] != null) ? jObject["Name"].ToString() : "?????");
			text2 = ((jObject["Description"] != null) ? jObject["Description"].ToString() : "");
			string text5 = ((jObject["Mod_Name"] != null) ? jObject["Mod_Name"].ToString() : "");
			if ((text5.Length > 0) & !ModEnums.SC_DIR[text4].ContainsKey(ID) & (Operators.CompareString(Manager.GetCurrentMod(), text5, TextCompare: false) != 0) & ModEnums.FolderByMod.ContainsKey(text5))
			{
				text3 = "\r\n\r\nSource: " + ModEnums.FolderByMod[text5];
			}
		}
		else
		{
			text = ID;
		}
		InfoTab.Text = "[" + text + "]\r\n" + text2 + text3;
		checked
		{
			InfoTab.Select(0, text.Length + 2);
			InfoTab.SelectionColor = ModColors.MenuColors.Highlight;
			if (text3.Length > 0)
			{
				InfoTab.Select(InfoTab.Text.Length - text3.Length + 2, text3.Length);
				InfoTab.SelectionColor = Color.DarkGray;
			}
		}
	}

	private string ToolTipType(ModCards.PropertyControl control)
	{
		return control.PropType switch
		{
			ModCards.PropType.Bool => "Boolean",
			ModCards.PropType.Int => "Integer",
			ModCards.PropType.Float => "Float",
			ModCards.PropType.Str => "String",
			ModCards.PropType.Color => "Color",
			_ => control.ObjectName,
		};
	}

	private Color ToolTipColor(ModCards.PropertyControl control)
	{
		return control.PropType switch
		{
			ModCards.PropType.Bool => Color.Salmon,
			ModCards.PropType.Int => Color.SandyBrown,
			ModCards.PropType.Float => Color.Khaki,
			ModCards.PropType.Ref => Color.PaleGreen,
			ModCards.PropType.Str => Color.LightSkyBlue,
			ModCards.PropType.Color => Color.Thistle,
			_ => Color.MediumPurple,
		};
	}

	private bool IsPriced()
	{
		return new string[5] { "Armor", "Melee", "Addon", "Thrown", "Ranged" }.Contains(SaveType);
	}

	private string FindFromGeneralType(object searchType, object ID)
	{
		if (Conversions.ToBoolean(Operators.OrObject(Operators.CompareObjectEqual(searchType, "Item", TextCompare: false), Operators.CompareObjectEqual(searchType, "Weapon", TextCompare: false))))
		{
			string[] array = new string[4] { "Melee", "Thrown", "Ranged", "Keycard" };
			if (Operators.ConditionalCompareObjectEqual(searchType, "Item", TextCompare: false))
			{
				array = new string[5] { "Melee", "Thrown", "Ranged", "Keycard", "Armor" };
			}
			string[] array2 = array;
			foreach (string text in array2)
			{
				if (ModEnums.SC_DIR[text].ContainsKey(Conversions.ToString(ID)) | ModEnums.MC_DIR[text].ContainsKey(Conversions.ToString(ID)))
				{
					return text;
				}
			}
		}
		return Conversions.ToString(searchType);
	}

	private void DisplayPrice()
	{
		try
		{
			Price.ForeColor = ((Operators.CompareString(SaveType, "Ranged", TextCompare: false) == 0) ? ModColors.MenuColors.Highlight : ModColors.MenuColors.Note);
			if (Operators.CompareString(SaveType, "Armor", TextCompare: false) == 0)
			{
				Price.Text = "$" + ModPrice.Market_ArmorPrice(ObjEditor.Nodes).ToString("N0");
			}
			else if (Operators.CompareString(SaveType, "Melee", TextCompare: false) == 0)
			{
				Price.Text = "$" + ModPrice.Market_WeaponPrice(ObjEditor.Nodes).ToString("N0");
			}
			else if (Operators.CompareString(SaveType, "Addon", TextCompare: false) == 0)
			{
				Price.Text = "$" + ModPrice.Market_AddonPrice(ObjEditor.Nodes).ToString("N0");
			}
			else if (Operators.CompareString(SaveType, "Thrown", TextCompare: false) == 0)
			{
				Price.Text = "$" + ModPrice.Market_RangedPrice(ObjEditor.Nodes, amThrown: true).ToString("N0");
			}
			else if (Operators.CompareString(SaveType, "Ranged", TextCompare: false) == 0)
			{
				Price.Text = "~$" + ModPrice.Market_RangedPrice(ObjEditor.Nodes).ToString("N0");
			}
			else
			{
				Price.Text = "";
			}
		}
		catch (OverflowException ex)
		{
			ProjectData.SetProjectError(ex);
			OverflowException ex2 = ex;
			Price.Text = "Overflowing!";
			Price.ForeColor = ModColors.MenuColors.Warning;
			Console.WriteLine(ex2.Source);
			ProjectData.ClearProjectError();
		}
	}

	private void DisplayActions(bool controlOptions = false)
	{
		if (SelectedTag != null)
		{
			string text = "";
			if (RevertToolStripMenuItem.Enabled)
			{
				text += "  [Z] Revert";
			}
			if (ExpandCurrentToolStripMenuItem.Enabled)
			{
				text += "  [K] Expand";
			}
			if (CollapseCurrentToolStripMenuItem.Enabled)
			{
				text += "  [J] Collapse";
			}
			if (DuplicateToolStripMenuItem.Enabled)
			{
				text += "  [D] Duplicate";
			}
			if (ShiftUpToolStripMenuItem.Enabled)
			{
				text += "  [Q] Move Up";
			}
			if (ShiftDownToolStripMenuItem.Enabled)
			{
				text += "  [E] Move Down";
			}
			if (ColorWheelToolStripMenuItem.Enabled)
			{
				text += "  [F] Color Wheel";
			}
			if (StringEditorToolStripMenuItem.Enabled)
			{
				text += "  [G] String Editor";
			}
			if (LoadFromSelectedToolStripMenuItem.Enabled)
			{
				text += "  [O] Load Entry";
			}
			if (!controlOptions)
			{
				ActionButton.Text = "";
				ModCards.PropertyControl linkedControl = SelectedTag.LinkedControl;
				if (linkedControl.IsList & !linkedControl.Fixed)
				{
					ActionButton.Text = "Click or [Enter] = " + ((SelectedTag.NodeType == ModCards.NodeType.Arr) ? " Add  " : "Remove  ");
				}
				else if (linkedControl.PropType == ModCards.PropType.Color)
				{
					ActionButton.Text = "Click or [Enter] = Color Wheel  ";
				}
				else if (linkedControl.PropType == ModCards.PropType.Bool)
				{
					ActionButton.Text = "Click or [Enter] = Toggle  ";
				}
				if (text.Length > 0)
				{
					ActionButton.Text += "[CTRL] for more options";
				}
			}
			else if (text.Length > 0)
			{
				ActionButton.Text = "CTRL +" + text;
			}
		}
		else
		{
			ActionButton.Text = "";
		}
	}

	public object GetDefaultValue(ModCards.PropType vtype, object val)
	{
		return vtype switch
		{
			ModCards.PropType.Bool => false,
			ModCards.PropType.Int => 0,
			ModCards.PropType.Float => 1m,
			ModCards.PropType.Str => "",
			ModCards.PropType.Ref => val,
			ModCards.PropType.Color => NewColorFromHex(Color.White),
			_ => null,
		};
	}

	public ModCards.ColorRGBA NewColorFromHex(Color color)
	{
		return new ModCards.ColorRGBA
		{
			r = Math.Round((double)(int)color.R / 255.0, 7),
			g = Math.Round((double)(int)color.G / 255.0, 7),
			b = Math.Round((double)(int)color.B / 255.0, 7),
			a = Math.Round((double)(int)color.A / 255.0, 7)
		};
	}

	public ModCards.ColorRGBA ColorFromHex(ModCards.ColorRGBA unityColor, Color color)
	{
		unityColor.r = Math.Round((double)(int)color.R / 255.0, 7);
		unityColor.g = Math.Round((double)(int)color.G / 255.0, 7);
		unityColor.b = Math.Round((double)(int)color.B / 255.0, 7);
		return unityColor;
	}

	public void CheckNullValue(ModCards.PropertyControl control, TreeNode node)
	{
		if ((control.PropType == ModCards.PropType.Ref) & (NewLateBinding.LateGet(node.Tag, null, "Value", new object[0], null, null, null) != null) & (control.Selection != null))
		{
			bool flag = Array.FindIndex((string[])control.Selection, [SpecialName] (string s) => NewLateBinding.LateGet(node.Tag, null, "Value", new object[0], null, null, null).ToString().Equals(s, StringComparison.OrdinalIgnoreCase) & (Operators.CompareString(SaveType, control.ObjectName, TextCompare: false) != 0 || ModNodes.LoadedPrior(SaveType, s, PreloadPriority))) < 0;
			TreeNode treeNode = node;
			Color foreColor;
			if (flag)
			{
				foreColor = (control.WriteNewValues ? ModColors.MenuColors.Highlight : ModColors.MenuColors.Warning);
			}
			else
			{
				foreColor = ModColors.MenuColors.Text;
			}
			treeNode.ForeColor = foreColor;
		}
	}

	private void MarkUnsaved(bool edited)
	{
		Unsaved = edited;
        if(edited&&historyIndex>=0)TrackModernHistory();
        RefreshStudioPreview();
		FileName.ForeColor = (edited ? ModColors.MenuColors.Highlight : ModColors.MenuColors.Text);
	}

	private string[] GetSameTypeRefs(JObject obj)
	{
		List<string> list = new List<string>();
		ModCards.PropertyControl[] controlScheme = ControlScheme;
		foreach (ModCards.PropertyControl propertyControl in controlScheme)
		{
			if (!((Operators.CompareString(SaveType, propertyControl.ObjectName, TextCompare: false) == 0) & (obj[propertyControl.PropName] != null)))
			{
				continue;
			}
			JToken jToken = obj[propertyControl.PropName];
			if (jToken.Type == JTokenType.Array)
			{
				foreach (JToken item in jToken.Children())
				{
					list.Add(item.ToString());
				}
			}
			else
			{
				list.Add(jToken.ToString());
			}
		}
		return list.ToArray();
	}

	public string GetDisplayNode(TreeNode node)
	{
		return ((node.Name.Length > 0) ? (node.Name + ": ") : "") + (Conversions.ToBoolean(Operators.NotObject(Operators.CompareObjectEqual(NewLateBinding.LateGet(node.Tag, null, "NodeType", new object[0], null, null, null), ModCards.NodeType.Arr, TextCompare: false))) ? node.Tag.ToString() : "");
	}

	public void HighlightAllNodes(TreeNode parentNode, bool MarkAsEdited = true)
	{
		if ((parentNode.Parent == null) & !EditedProperties.ContainsKey(Conversions.ToString(NewLateBinding.LateGet(NewLateBinding.LateGet(parentNode.Tag, null, "LinkedControl", new object[0], null, null, null), null, "PropName", new object[0], null, null, null))))
		{
			NewLateBinding.LateSetComplex(parentNode.Tag, null, "Edited", new object[1] { true }, null, null, OptimisticSet: false, RValueBase: true);
			EditedProperties.Add(Conversions.ToString(NewLateBinding.LateGet(NewLateBinding.LateGet(parentNode.Tag, null, "LinkedControl", new object[0], null, null, null), null, "PropName", new object[0], null, null, null)), parentNode);
			RevertAllToolStripMenuItem.Enabled = true;
		}
		object obj = (Conversions.ToBoolean(Operators.AndObject(Operators.CompareObjectEqual(NewLateBinding.LateGet(NewLateBinding.LateGet(parentNode.Tag, null, "LinkedControl", new object[0], null, null, null), null, "PropType", new object[0], null, null, null), ModCards.PropType.Color, TextCompare: false), Operators.CompareObjectEqual(NewLateBinding.LateGet(parentNode.Tag, null, "NodeType", new object[0], null, null, null), ModCards.NodeType.Val, TextCompare: false))) ? NewLateBinding.LateGet(NewLateBinding.LateGet(parentNode.Tag, null, "Value", new object[0], null, null, null), null, "AsColor", new object[0], null, null, null) : ((object)ModColors.MenuColors.Text));
		parentNode.ForeColor = ((obj != null) ? ((Color)obj) : default(Color));
		CheckNullValue((ModCards.PropertyControl)NewLateBinding.LateGet(parentNode.Tag, null, "LinkedControl", new object[0], null, null, null), parentNode);
		foreach (TreeNode node in parentNode.Nodes)
		{
			HighlightAllNodes(node, MarkAsEdited: false);
		}
	}

	private void MarkEditedNodes(JObject prop)
	{
		foreach (JProperty item in prop.Properties())
		{
			if (ObjEditor.Nodes[item.Name] != null)
			{
				HighlightAllNodes(ObjEditor.Nodes[item.Name]);
			}
		}
	}

	private void UpdateMarkedSelections(TreeNode node)
	{
		ModCards.PropertyControl linkedControl = ((ModCards.NodeTag)node.Tag).LinkedControl;
		if ((linkedControl.PropType == ModCards.PropType.Ref) & ModEnums.SC_DIR.ContainsKey(linkedControl.ObjectName))
		{
			bool flag = Operators.ConditionalCompareObjectEqual(NewLateBinding.LateIndexGet(linkedControl.Selection, new object[1] { "0" }, null), "None", TextCompare: false);
			linkedControl.Selection = (flag ? ModCards.Nullable(ModCards.Options(linkedControl.ObjectName)) : ModCards.Options(linkedControl.ObjectName));
			CheckNullValue(linkedControl, node);
		}
		foreach (TreeNode node2 in node.Nodes)
		{
			UpdateMarkedSelections(node2);
		}
	}

	private void Reset()
	{
		Loading = true;
		SelectedTag = null;
		ListObjects.DataSource = null;
		ObjEditor.SelectedNode = null;
		ActionButton.Text = "";
		ActionButton.Enabled = false;
		Param.Text = "";
		Param.Enabled = false;
		InfoTab.Text = "";
		Loading = false;
	}
}
