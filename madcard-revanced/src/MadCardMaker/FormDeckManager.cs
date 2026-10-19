using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using MadCardMaker.My;
using MadCardMaker.My.Resources;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.VisualBasic.FileIO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MadCardMaker;

[DesignerGenerated]
public partial class FormDeckManager : Form
{
	private IContainer components;

	private string LoadedMod;

	private string LoadModDirectory;

	private string EnabledMods;

	private string SelectedCategory;

	private ModCards.CardTag SelectedTag;

	private ModCards.CardTag DeletedTag;

	private bool Loading;

	private bool ViewOnly;

	[field: AccessedThroughProperty("FileToolStripMenuItem")]
	internal virtual ToolStripMenuItem FileToolStripMenuItem
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("NewCardToolStripMenuItem")]
	internal virtual ToolStripMenuItem NewCardToolStripMenuItem
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("LoadCardToolStripMenuItem")]
	internal virtual ToolStripMenuItem LoadCardToolStripMenuItem
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ToolStripSeparator1")]
	internal virtual ToolStripSeparator ToolStripSeparator1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("NewDeckToolStripMenuItem")]
	internal virtual ToolStripMenuItem NewDeckToolStripMenuItem
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("LoadDeckToolStripMenuItem")]
	internal virtual ToolStripMenuItem LoadDeckToolStripMenuItem
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("SaveDeckTXTToolStripMenuItem")]
	internal virtual ToolStripMenuItem SaveDeckTXTToolStripMenuItem
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("DiagnosticsToolStripMenuItem")]
	internal virtual ToolStripMenuItem DiagnosticsToolStripMenuItem
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ModDisplay")]
	internal virtual Label ModDisplay
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("CP_ShiftDown")]
	internal virtual Button CP_ShiftDown
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = CP_ShiftDown_Click;
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

	[field: AccessedThroughProperty("CP_ShiftUp")]
	internal virtual Button CP_ShiftUp
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = CP_ShiftUp_Click;
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

	[field: AccessedThroughProperty("CP_Sort")]
	internal virtual Button CP_Sort
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = CP_Sort_Click;
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

	[field: AccessedThroughProperty("CP_Remove")]
	internal virtual Button CP_Remove
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = CP_Remove_Click;
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

	[field: AccessedThroughProperty("CP_Add")]
	internal virtual Button CP_Add
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = CP_Add_Click;
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

	[field: AccessedThroughProperty("ControlPanel")]
	internal virtual FlowLayoutPanel ControlPanel
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("CP_Unlist")]
	internal virtual Button CP_Unlist
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = CP_Unlist_Click;
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

	[field: AccessedThroughProperty("CP_Edit")]
	internal virtual Button CP_Edit
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = CP_Edit_Click;
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

	[field: AccessedThroughProperty("Info")]
	internal virtual RichTextBox Info
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("CardManager")]
	internal virtual TreeView CardManager
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			DrawTreeNodeEventHandler value2 = CardManager_DrawNode;
			TreeViewEventHandler value3 = CardManager_AfterSelect;
			TreeView treeView = field;
			if (treeView != null)
			{
				treeView.DrawNode -= value2;
				treeView.AfterSelect -= value3;
			}
			field = value;
			treeView = field;
			if (treeView != null)
			{
				treeView.DrawNode += value2;
				treeView.AfterSelect += value3;
			}
		}
	}

	[field: AccessedThroughProperty("CP_AddDeck")]
	internal virtual Button CP_AddDeck
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = CP_AddDeck_Click;
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

	[field: AccessedThroughProperty("CP_Rename")]
	internal virtual Button CP_Rename
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = CP_Rename_Click;
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

	[field: AccessedThroughProperty("CP_PriorityDown")]
	internal virtual Button CP_PriorityDown
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = CP_PriorityDown_Click;
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

	[field: AccessedThroughProperty("CP_PriorityUp")]
	internal virtual Button CP_PriorityUp
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = CP_PriorityUp_Click;
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

	[field: AccessedThroughProperty("CP_Save")]
	internal virtual Button CP_Save
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = CP_Save_Click;
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

	[field: AccessedThroughProperty("ToolTip")]
	internal virtual ToolTip ToolTip
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("CP_Refresh")]
	internal virtual Button CP_Refresh
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = CP_Refresh_Click;
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

	[field: AccessedThroughProperty("SideControls")]
	internal virtual FlowLayoutPanel SideControls
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("CP_Delete")]
	internal virtual Button CP_Delete
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = CP_Delete_Click;
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

	[field: AccessedThroughProperty("CP_Restore")]
	internal virtual Button CP_Restore
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = CP_Restore_Click;
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

	[field: AccessedThroughProperty("SidePanel")]
	internal virtual Panel SidePanel
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("SideLabel")]
	internal virtual Label SideLabel
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("RemovedCards")]
	internal virtual TreeView RemovedCards
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			TreeViewEventHandler value2 = RemovedCards_AfterSelect;
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

	[field: AccessedThroughProperty("FileToolStripMenuItem1")]
	internal virtual ToolStripMenuItem FileToolStripMenuItem1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("NewDeckToolStripMenuItem1")]
	internal virtual ToolStripMenuItem NewDeckToolStripMenuItem1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = NewDeckToolStripMenuItem1_Click;
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

	[field: AccessedThroughProperty("NewCardToolStripMenuItem1")]
	internal virtual ToolStripMenuItem NewCardToolStripMenuItem1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = NewCardToolStripMenuItem1_Click;
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

	[field: AccessedThroughProperty("RemoveToolStripMenuItem")]
	internal virtual ToolStripMenuItem RemoveToolStripMenuItem
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = RemoveToolStripMenuItem_Click;
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

	[field: AccessedThroughProperty("SaveAllToolStripMenuItem")]
	internal virtual ToolStripMenuItem SaveAllToolStripMenuItem
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = SaveAllToolStripMenuItem_Click;
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

	[field: AccessedThroughProperty("ReturnToolStripMenuItem")]
	internal virtual ToolStripMenuItem ReturnToolStripMenuItem
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = ReturnToolStripMenuItem_Click;
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

	[field: AccessedThroughProperty("EditCardToolStripMenuItem")]
	internal virtual ToolStripMenuItem EditCardToolStripMenuItem
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = EditCardToolStripMenuItem_Click;
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

	[field: AccessedThroughProperty("RenameToolStripMenuItem")]
	internal virtual ToolStripMenuItem RenameToolStripMenuItem
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = RenameToolStripMenuItem_Click;
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

	[field: AccessedThroughProperty("MoveToTopToolStripMenuItem")]
	internal virtual ToolStripMenuItem MoveToTopToolStripMenuItem
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = MoveToTopToolStripMenuItem_Click;
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

	[field: AccessedThroughProperty("MoveUpToolStripMenuItem")]
	internal virtual ToolStripMenuItem MoveUpToolStripMenuItem
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = MoveUpToolStripMenuItem_Click;
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

	[field: AccessedThroughProperty("MoveDownToolStripMenuItem")]
	internal virtual ToolStripMenuItem MoveDownToolStripMenuItem
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = MoveDownToolStripMenuItem_Click;
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

	[field: AccessedThroughProperty("MoveToBottomToolStripMenuItem")]
	internal virtual ToolStripMenuItem MoveToBottomToolStripMenuItem
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = MoveToBottomToolStripMenuItem_Click;
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

	[field: AccessedThroughProperty("IncPriorityToolStripMenuItem")]
	internal virtual ToolStripMenuItem IncPriorityToolStripMenuItem
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = IncPriorityToolStripMenuItem_Click;
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

	[field: AccessedThroughProperty("DecPriorityToolStripMenuItem")]
	internal virtual ToolStripMenuItem DecPriorityToolStripMenuItem
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = DecPriorityToolStripMenuItem_Click;
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

	[field: AccessedThroughProperty("SettingsToolStripMenuItem")]
	internal virtual ToolStripMenuItem SettingsToolStripMenuItem
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("SettingsToolStripMenuItem1")]
	internal virtual ToolStripMenuItem SettingsToolStripMenuItem1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = SettingsToolStripMenuItem1_Click;
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

	[field: AccessedThroughProperty("DeckMenu")]
	internal virtual MenuStrip DeckMenu
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ListMods")]
	internal virtual TreeView ListMods
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			TreeViewEventHandler value2 = ListMods_AfterSelect;
			TreeViewEventHandler value3 = ListMods_AfterCheck;
			TreeView treeView = field;
			if (treeView != null)
			{
				treeView.AfterSelect -= value2;
				treeView.AfterCheck -= value3;
			}
			field = value;
			treeView = field;
			if (treeView != null)
			{
				treeView.AfterSelect += value2;
				treeView.AfterCheck += value3;
			}
		}
	}

	[field: AccessedThroughProperty("HelpToolStripMenuItem")]
	internal virtual ToolStripMenuItem HelpToolStripMenuItem
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("OpenManualToolStripMenuItem")]
	internal virtual ToolStripMenuItem OpenManualToolStripMenuItem
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = OpenManualToolStripMenuItem_Click;
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

	[field: AccessedThroughProperty("CP_View")]
	internal virtual Button CP_View
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = CP_View_Click;
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
		[CompilerGenerated]
		set
		{
			EventHandler value2 = ViewToolStripMenuItem_Click;
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

	public FormDeckManager()
	{
		Load += MadMaker_Load;
		Shown += MadMaker_Show;
		LoadedMod = "";
		LoadModDirectory = "";
		EnabledMods = "";
		SelectedCategory = "";
		Loading = false;
		ViewOnly = false;
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
		this.components = new System.ComponentModel.Container();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MadCardMaker.FormDeckManager));
		this.FileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.NewCardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.LoadCardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.ToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
		this.NewDeckToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.LoadDeckToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.SaveDeckTXTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.DiagnosticsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.ModDisplay = new System.Windows.Forms.Label();
		this.ControlPanel = new System.Windows.Forms.FlowLayoutPanel();
		this.CP_AddDeck = new System.Windows.Forms.Button();
		this.CP_Add = new System.Windows.Forms.Button();
		this.CP_Remove = new System.Windows.Forms.Button();
		this.CP_Edit = new System.Windows.Forms.Button();
		this.CP_Rename = new System.Windows.Forms.Button();
		this.CP_PriorityDown = new System.Windows.Forms.Button();
		this.CP_PriorityUp = new System.Windows.Forms.Button();
		this.CP_ShiftUp = new System.Windows.Forms.Button();
		this.CP_ShiftDown = new System.Windows.Forms.Button();
		this.CP_Sort = new System.Windows.Forms.Button();
		this.CP_Unlist = new System.Windows.Forms.Button();
		this.CP_View = new System.Windows.Forms.Button();
		this.CP_Save = new System.Windows.Forms.Button();
		this.CP_Refresh = new System.Windows.Forms.Button();
		this.Info = new System.Windows.Forms.RichTextBox();
		this.CardManager = new System.Windows.Forms.TreeView();
		this.ToolTip = new System.Windows.Forms.ToolTip(this.components);
		this.SideControls = new System.Windows.Forms.FlowLayoutPanel();
		this.CP_Delete = new System.Windows.Forms.Button();
		this.CP_Restore = new System.Windows.Forms.Button();
		this.SideLabel = new System.Windows.Forms.Label();
		this.SidePanel = new System.Windows.Forms.Panel();
		this.RemovedCards = new System.Windows.Forms.TreeView();
		this.FileToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
		this.NewDeckToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
		this.NewCardToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
		this.RemoveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.SaveAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.ReturnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.ViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.EditToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.EditCardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.RenameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.MoveToTopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.MoveUpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.MoveDownToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.MoveToBottomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.IncPriorityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.DecPriorityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.SettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.SettingsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
		this.DeckMenu = new System.Windows.Forms.MenuStrip();
		this.HelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.OpenManualToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.ListMods = new System.Windows.Forms.TreeView();
		this.ControlPanel.SuspendLayout();
		this.SideControls.SuspendLayout();
		this.SidePanel.SuspendLayout();
		this.DeckMenu.SuspendLayout();
		base.SuspendLayout();
		this.FileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[7] { this.NewCardToolStripMenuItem, this.LoadCardToolStripMenuItem, this.ToolStripSeparator1, this.NewDeckToolStripMenuItem, this.LoadDeckToolStripMenuItem, this.SaveDeckTXTToolStripMenuItem, this.DiagnosticsToolStripMenuItem });
		this.FileToolStripMenuItem.Name = "FileToolStripMenuItem";
		this.FileToolStripMenuItem.Size = new System.Drawing.Size(37, 19);
		this.FileToolStripMenuItem.Text = "File";
		this.NewCardToolStripMenuItem.Name = "NewCardToolStripMenuItem";
		this.NewCardToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
		this.NewCardToolStripMenuItem.Text = "New Card";
		this.LoadCardToolStripMenuItem.Name = "LoadCardToolStripMenuItem";
		this.LoadCardToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
		this.LoadCardToolStripMenuItem.Text = "Load Card";
		this.ToolStripSeparator1.Name = "ToolStripSeparator1";
		this.ToolStripSeparator1.Size = new System.Drawing.Size(132, 6);
		this.NewDeckToolStripMenuItem.Name = "NewDeckToolStripMenuItem";
		this.NewDeckToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
		this.NewDeckToolStripMenuItem.Text = "New Deck";
		this.LoadDeckToolStripMenuItem.Name = "LoadDeckToolStripMenuItem";
		this.LoadDeckToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
		this.LoadDeckToolStripMenuItem.Text = "Load Deck";
		this.SaveDeckTXTToolStripMenuItem.Name = "SaveDeckTXTToolStripMenuItem";
		this.SaveDeckTXTToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
		this.SaveDeckTXTToolStripMenuItem.Text = "Save Deck";
		this.DiagnosticsToolStripMenuItem.Name = "DiagnosticsToolStripMenuItem";
		this.DiagnosticsToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
		this.DiagnosticsToolStripMenuItem.Text = "Diagnostics";
		this.ModDisplay.AutoSize = true;
		this.ModDisplay.Dock = System.Windows.Forms.DockStyle.Top;
		this.ModDisplay.Font = new System.Drawing.Font("Impact", 24f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.ModDisplay.Location = new System.Drawing.Point(0, 25);
		this.ModDisplay.Name = "ModDisplay";
		this.ModDisplay.Size = new System.Drawing.Size(167, 39);
		this.ModDisplay.TabIndex = 1;
		this.ModDisplay.Text = "Mod Loader";
		this.ControlPanel.BackColor = System.Drawing.Color.FromArgb(32, 32, 32);
		this.ControlPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.ControlPanel.Controls.Add(this.CP_AddDeck);
		this.ControlPanel.Controls.Add(this.CP_Add);
		this.ControlPanel.Controls.Add(this.CP_Remove);
		this.ControlPanel.Controls.Add(this.CP_Edit);
		this.ControlPanel.Controls.Add(this.CP_Rename);
		this.ControlPanel.Controls.Add(this.CP_PriorityDown);
		this.ControlPanel.Controls.Add(this.CP_PriorityUp);
		this.ControlPanel.Controls.Add(this.CP_ShiftUp);
		this.ControlPanel.Controls.Add(this.CP_ShiftDown);
		this.ControlPanel.Controls.Add(this.CP_Sort);
		this.ControlPanel.Controls.Add(this.CP_Unlist);
		this.ControlPanel.Controls.Add(this.CP_View);
		this.ControlPanel.Controls.Add(this.CP_Save);
		this.ControlPanel.Controls.Add(this.CP_Refresh);
		this.ControlPanel.Dock = System.Windows.Forms.DockStyle.Top;
		this.ControlPanel.Location = new System.Drawing.Point(0, 64);
		this.ControlPanel.Name = "ControlPanel";
		this.ControlPanel.Size = new System.Drawing.Size(729, 58);
		this.ControlPanel.TabIndex = 2;
		this.CP_AddDeck.BackColor = System.Drawing.Color.Black;
		this.CP_AddDeck.Font = new System.Drawing.Font("Impact", 24f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.CP_AddDeck.ForeColor = System.Drawing.Color.PaleGreen;
		this.CP_AddDeck.Image = MadCardMaker.My.Resources.Resources.DM_AddDeck;
		this.CP_AddDeck.Location = new System.Drawing.Point(3, 3);
		this.CP_AddDeck.Name = "CP_AddDeck";
		this.CP_AddDeck.Size = new System.Drawing.Size(45, 45);
		this.CP_AddDeck.TabIndex = 8;
		this.CP_AddDeck.UseVisualStyleBackColor = false;
		this.CP_AddDeck.Visible = false;
		this.CP_Add.BackColor = System.Drawing.Color.Black;
		this.CP_Add.Font = new System.Drawing.Font("Impact", 24f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.CP_Add.ForeColor = System.Drawing.Color.PaleGreen;
		this.CP_Add.Image = MadCardMaker.My.Resources.Resources.DM_AddCard;
		this.CP_Add.Location = new System.Drawing.Point(54, 3);
		this.CP_Add.Name = "CP_Add";
		this.CP_Add.Size = new System.Drawing.Size(45, 45);
		this.CP_Add.TabIndex = 0;
		this.CP_Add.UseVisualStyleBackColor = false;
		this.CP_Remove.BackColor = System.Drawing.Color.Black;
		this.CP_Remove.Enabled = false;
		this.CP_Remove.Font = new System.Drawing.Font("Impact", 24f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.CP_Remove.ForeColor = System.Drawing.Color.Salmon;
		this.CP_Remove.Image = MadCardMaker.My.Resources.Resources.DM_Remove;
		this.CP_Remove.Location = new System.Drawing.Point(105, 3);
		this.CP_Remove.Name = "CP_Remove";
		this.CP_Remove.Size = new System.Drawing.Size(45, 45);
		this.CP_Remove.TabIndex = 1;
		this.CP_Remove.UseVisualStyleBackColor = false;
		this.CP_Edit.BackColor = System.Drawing.Color.Black;
		this.CP_Edit.Enabled = false;
		this.CP_Edit.Font = new System.Drawing.Font("Impact", 24f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.CP_Edit.ForeColor = System.Drawing.Color.PaleGoldenrod;
		this.CP_Edit.Image = MadCardMaker.My.Resources.Resources.DM_Edit;
		this.CP_Edit.Location = new System.Drawing.Point(156, 3);
		this.CP_Edit.Name = "CP_Edit";
		this.CP_Edit.Size = new System.Drawing.Size(45, 45);
		this.CP_Edit.TabIndex = 6;
		this.CP_Edit.UseVisualStyleBackColor = false;
		this.CP_Rename.BackColor = System.Drawing.Color.Black;
		this.CP_Rename.Enabled = false;
		this.CP_Rename.Font = new System.Drawing.Font("Impact", 24f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.CP_Rename.ForeColor = System.Drawing.Color.PaleGoldenrod;
		this.CP_Rename.Image = MadCardMaker.My.Resources.Resources.DM_Rename;
		this.CP_Rename.Location = new System.Drawing.Point(207, 3);
		this.CP_Rename.Name = "CP_Rename";
		this.CP_Rename.Size = new System.Drawing.Size(45, 45);
		this.CP_Rename.TabIndex = 9;
		this.CP_Rename.UseVisualStyleBackColor = false;
		this.CP_PriorityDown.BackColor = System.Drawing.Color.Black;
		this.CP_PriorityDown.Enabled = false;
		this.CP_PriorityDown.Font = new System.Drawing.Font("Impact", 24f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.CP_PriorityDown.ForeColor = System.Drawing.Color.LightSkyBlue;
		this.CP_PriorityDown.Image = MadCardMaker.My.Resources.Resources.DM_PriorityDown;
		this.CP_PriorityDown.Location = new System.Drawing.Point(258, 3);
		this.CP_PriorityDown.Name = "CP_PriorityDown";
		this.CP_PriorityDown.Size = new System.Drawing.Size(45, 45);
		this.CP_PriorityDown.TabIndex = 11;
		this.CP_PriorityDown.UseVisualStyleBackColor = false;
		this.CP_PriorityDown.Visible = false;
		this.CP_PriorityUp.BackColor = System.Drawing.Color.Black;
		this.CP_PriorityUp.Enabled = false;
		this.CP_PriorityUp.Font = new System.Drawing.Font("Impact", 24f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.CP_PriorityUp.ForeColor = System.Drawing.Color.LightSkyBlue;
		this.CP_PriorityUp.Image = MadCardMaker.My.Resources.Resources.DM_PriorityUp;
		this.CP_PriorityUp.Location = new System.Drawing.Point(309, 3);
		this.CP_PriorityUp.Name = "CP_PriorityUp";
		this.CP_PriorityUp.Size = new System.Drawing.Size(45, 45);
		this.CP_PriorityUp.TabIndex = 10;
		this.CP_PriorityUp.UseVisualStyleBackColor = false;
		this.CP_PriorityUp.Visible = false;
		this.CP_ShiftUp.BackColor = System.Drawing.Color.Black;
		this.CP_ShiftUp.Enabled = false;
		this.CP_ShiftUp.Font = new System.Drawing.Font("Impact", 24f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.CP_ShiftUp.ForeColor = System.Drawing.Color.LightSkyBlue;
		this.CP_ShiftUp.Image = MadCardMaker.My.Resources.Resources.DM_ShiftUp;
		this.CP_ShiftUp.Location = new System.Drawing.Point(360, 3);
		this.CP_ShiftUp.Name = "CP_ShiftUp";
		this.CP_ShiftUp.Size = new System.Drawing.Size(45, 45);
		this.CP_ShiftUp.TabIndex = 3;
		this.CP_ShiftUp.UseVisualStyleBackColor = false;
		this.CP_ShiftUp.Visible = false;
		this.CP_ShiftDown.BackColor = System.Drawing.Color.Black;
		this.CP_ShiftDown.Enabled = false;
		this.CP_ShiftDown.Font = new System.Drawing.Font("Impact", 24f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.CP_ShiftDown.ForeColor = System.Drawing.Color.LightSkyBlue;
		this.CP_ShiftDown.Image = MadCardMaker.My.Resources.Resources.DM_ShiftDown;
		this.CP_ShiftDown.Location = new System.Drawing.Point(411, 3);
		this.CP_ShiftDown.Name = "CP_ShiftDown";
		this.CP_ShiftDown.Size = new System.Drawing.Size(45, 45);
		this.CP_ShiftDown.TabIndex = 4;
		this.CP_ShiftDown.UseVisualStyleBackColor = false;
		this.CP_ShiftDown.Visible = false;
		this.CP_Sort.BackColor = System.Drawing.Color.Black;
		this.CP_Sort.Enabled = false;
		this.CP_Sort.Font = new System.Drawing.Font("Impact", 24f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.CP_Sort.ForeColor = System.Drawing.Color.LightSkyBlue;
		this.CP_Sort.Image = MadCardMaker.My.Resources.Resources.DM_Sort;
		this.CP_Sort.Location = new System.Drawing.Point(462, 3);
		this.CP_Sort.Name = "CP_Sort";
		this.CP_Sort.Size = new System.Drawing.Size(45, 45);
		this.CP_Sort.TabIndex = 2;
		this.CP_Sort.TextAlign = System.Drawing.ContentAlignment.TopCenter;
		this.CP_Sort.UseVisualStyleBackColor = false;
		this.CP_Sort.Visible = false;
		this.CP_Unlist.BackColor = System.Drawing.Color.Black;
		this.CP_Unlist.Enabled = false;
		this.CP_Unlist.Font = new System.Drawing.Font("Impact", 24f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.CP_Unlist.ForeColor = System.Drawing.Color.LightSkyBlue;
		this.CP_Unlist.Image = MadCardMaker.My.Resources.Resources.DM_Unlist;
		this.CP_Unlist.Location = new System.Drawing.Point(513, 3);
		this.CP_Unlist.Name = "CP_Unlist";
		this.CP_Unlist.Size = new System.Drawing.Size(45, 45);
		this.CP_Unlist.TabIndex = 5;
		this.CP_Unlist.UseVisualStyleBackColor = false;
		this.CP_Unlist.Visible = false;
		this.CP_View.BackColor = System.Drawing.Color.Black;
		this.CP_View.Enabled = false;
		this.CP_View.Font = new System.Drawing.Font("Impact", 24f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.CP_View.ForeColor = System.Drawing.Color.Gold;
		this.CP_View.Image = MadCardMaker.My.Resources.Resources.DM_View;
		this.CP_View.Location = new System.Drawing.Point(564, 3);
		this.CP_View.Name = "CP_View";
		this.CP_View.Size = new System.Drawing.Size(45, 45);
		this.CP_View.TabIndex = 14;
		this.CP_View.UseVisualStyleBackColor = false;
		this.CP_Save.BackColor = System.Drawing.Color.Black;
		this.CP_Save.Enabled = false;
		this.CP_Save.Font = new System.Drawing.Font("Impact", 24f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.CP_Save.ForeColor = System.Drawing.Color.Gold;
		this.CP_Save.Image = MadCardMaker.My.Resources.Resources.DM_Save;
		this.CP_Save.Location = new System.Drawing.Point(615, 3);
		this.CP_Save.Name = "CP_Save";
		this.CP_Save.Size = new System.Drawing.Size(45, 45);
		this.CP_Save.TabIndex = 12;
		this.CP_Save.UseVisualStyleBackColor = false;
		this.CP_Save.Visible = false;
		this.CP_Refresh.BackColor = System.Drawing.Color.Black;
		this.CP_Refresh.Enabled = false;
		this.CP_Refresh.Font = new System.Drawing.Font("Impact", 24f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.CP_Refresh.ForeColor = System.Drawing.Color.Gold;
		this.CP_Refresh.Image = MadCardMaker.My.Resources.Resources.DM_Return;
		this.CP_Refresh.Location = new System.Drawing.Point(666, 3);
		this.CP_Refresh.Name = "CP_Refresh";
		this.CP_Refresh.Size = new System.Drawing.Size(45, 45);
		this.CP_Refresh.TabIndex = 13;
		this.CP_Refresh.UseVisualStyleBackColor = false;
		this.Info.BackColor = System.Drawing.Color.FromArgb(64, 64, 64);
		this.Info.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.Info.Font = new System.Drawing.Font("Impact", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.Info.ForeColor = System.Drawing.Color.White;
		this.Info.Location = new System.Drawing.Point(0, 375);
		this.Info.Name = "Info";
		this.Info.ReadOnly = true;
		this.Info.Size = new System.Drawing.Size(729, 126);
		this.Info.TabIndex = 4;
		this.Info.Text = resources.GetString("Info.Text");
		this.CardManager.BackColor = System.Drawing.Color.Black;
		this.CardManager.Dock = System.Windows.Forms.DockStyle.Fill;
		this.CardManager.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawText;
		this.CardManager.Enabled = false;
		this.CardManager.ForeColor = System.Drawing.Color.White;
		this.CardManager.Location = new System.Drawing.Point(121, 122);
		this.CardManager.Name = "CardManager";
		this.CardManager.Size = new System.Drawing.Size(608, 253);
		this.CardManager.TabIndex = 5;
		this.CardManager.Visible = false;
		this.ToolTip.AutomaticDelay = 100;
		this.ToolTip.AutoPopDelay = 5000;
		this.ToolTip.BackColor = System.Drawing.Color.Silver;
		this.ToolTip.InitialDelay = 100;
		this.ToolTip.ReshowDelay = 500;
		this.ToolTip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
		this.ToolTip.UseAnimation = false;
		this.SideControls.BackColor = System.Drawing.Color.FromArgb(32, 32, 32);
		this.SideControls.Controls.Add(this.CP_Delete);
		this.SideControls.Controls.Add(this.CP_Restore);
		this.SideControls.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.SideControls.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
		this.SideControls.Location = new System.Drawing.Point(0, 419);
		this.SideControls.Name = "SideControls";
		this.SideControls.Size = new System.Drawing.Size(276, 57);
		this.SideControls.TabIndex = 6;
		this.SideControls.Visible = false;
		this.CP_Delete.BackColor = System.Drawing.Color.Black;
		this.CP_Delete.Enabled = false;
		this.CP_Delete.Font = new System.Drawing.Font("Impact", 24f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.CP_Delete.ForeColor = System.Drawing.Color.Salmon;
		this.CP_Delete.Image = MadCardMaker.My.Resources.Resources.DM_Delete;
		this.CP_Delete.Location = new System.Drawing.Point(228, 3);
		this.CP_Delete.Name = "CP_Delete";
		this.CP_Delete.Size = new System.Drawing.Size(45, 45);
		this.CP_Delete.TabIndex = 2;
		this.CP_Delete.UseVisualStyleBackColor = false;
		this.CP_Restore.BackColor = System.Drawing.Color.Black;
		this.CP_Restore.Enabled = false;
		this.CP_Restore.Font = new System.Drawing.Font("Impact", 24f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.CP_Restore.ForeColor = System.Drawing.Color.PaleGreen;
		this.CP_Restore.Image = MadCardMaker.My.Resources.Resources.DM_Restore;
		this.CP_Restore.Location = new System.Drawing.Point(177, 3);
		this.CP_Restore.Name = "CP_Restore";
		this.CP_Restore.Size = new System.Drawing.Size(45, 45);
		this.CP_Restore.TabIndex = 1;
		this.CP_Restore.UseVisualStyleBackColor = false;
		this.SideLabel.AutoSize = true;
		this.SideLabel.Dock = System.Windows.Forms.DockStyle.Top;
		this.SideLabel.Font = new System.Drawing.Font("Impact", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.SideLabel.Location = new System.Drawing.Point(0, 0);
		this.SideLabel.Name = "SideLabel";
		this.SideLabel.Size = new System.Drawing.Size(79, 20);
		this.SideLabel.TabIndex = 3;
		this.SideLabel.Text = "Load Order";
		this.SidePanel.BackColor = System.Drawing.Color.FromArgb(32, 32, 32);
		this.SidePanel.Controls.Add(this.RemovedCards);
		this.SidePanel.Controls.Add(this.SideControls);
		this.SidePanel.Controls.Add(this.SideLabel);
		this.SidePanel.Dock = System.Windows.Forms.DockStyle.Right;
		this.SidePanel.Location = new System.Drawing.Point(729, 25);
		this.SidePanel.Name = "SidePanel";
		this.SidePanel.Size = new System.Drawing.Size(276, 476);
		this.SidePanel.TabIndex = 7;
		this.RemovedCards.BackColor = System.Drawing.Color.Black;
		this.RemovedCards.Dock = System.Windows.Forms.DockStyle.Fill;
		this.RemovedCards.ForeColor = System.Drawing.Color.White;
		this.RemovedCards.Location = new System.Drawing.Point(0, 20);
		this.RemovedCards.Name = "RemovedCards";
		this.RemovedCards.Size = new System.Drawing.Size(276, 399);
		this.RemovedCards.TabIndex = 7;
		this.FileToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[6] { this.NewDeckToolStripMenuItem1, this.NewCardToolStripMenuItem1, this.RemoveToolStripMenuItem, this.SaveAllToolStripMenuItem, this.ReturnToolStripMenuItem, this.ViewToolStripMenuItem });
		this.FileToolStripMenuItem1.Name = "FileToolStripMenuItem1";
		this.FileToolStripMenuItem1.Size = new System.Drawing.Size(37, 19);
		this.FileToolStripMenuItem1.Text = "File";
		this.NewDeckToolStripMenuItem1.Enabled = false;
		this.NewDeckToolStripMenuItem1.Name = "NewDeckToolStripMenuItem1";
		this.NewDeckToolStripMenuItem1.ShortcutKeys = System.Windows.Forms.Keys.D | System.Windows.Forms.Keys.Control;
		this.NewDeckToolStripMenuItem1.Size = new System.Drawing.Size(176, 22);
		this.NewDeckToolStripMenuItem1.Text = "New Deck";
		this.NewCardToolStripMenuItem1.Name = "NewCardToolStripMenuItem1";
		this.NewCardToolStripMenuItem1.ShortcutKeys = System.Windows.Forms.Keys.C | System.Windows.Forms.Keys.Control;
		this.NewCardToolStripMenuItem1.Size = new System.Drawing.Size(176, 22);
		this.NewCardToolStripMenuItem1.Text = "Add";
		this.RemoveToolStripMenuItem.Enabled = false;
		this.RemoveToolStripMenuItem.Name = "RemoveToolStripMenuItem";
		this.RemoveToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.X | System.Windows.Forms.Keys.Control;
		this.RemoveToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
		this.RemoveToolStripMenuItem.Text = "Remove";
		this.SaveAllToolStripMenuItem.Enabled = false;
		this.SaveAllToolStripMenuItem.Name = "SaveAllToolStripMenuItem";
		this.SaveAllToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F2;
		this.SaveAllToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
		this.SaveAllToolStripMenuItem.Text = "Save All";
		this.ReturnToolStripMenuItem.Enabled = false;
		this.ReturnToolStripMenuItem.Name = "ReturnToolStripMenuItem";
		this.ReturnToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F4;
		this.ReturnToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
		this.ReturnToolStripMenuItem.Text = "Return To Mods";
		this.ViewToolStripMenuItem.Name = "ViewToolStripMenuItem";
		this.ViewToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.V | System.Windows.Forms.Keys.Control;
		this.ViewToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
		this.ViewToolStripMenuItem.Text = "View";
		this.EditToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[8] { this.EditCardToolStripMenuItem, this.RenameToolStripMenuItem, this.MoveToTopToolStripMenuItem, this.MoveUpToolStripMenuItem, this.MoveDownToolStripMenuItem, this.MoveToBottomToolStripMenuItem, this.IncPriorityToolStripMenuItem, this.DecPriorityToolStripMenuItem });
		this.EditToolStripMenuItem.Name = "EditToolStripMenuItem";
		this.EditToolStripMenuItem.Size = new System.Drawing.Size(39, 19);
		this.EditToolStripMenuItem.Text = "Edit";
		this.EditCardToolStripMenuItem.Enabled = false;
		this.EditCardToolStripMenuItem.Name = "EditCardToolStripMenuItem";
		this.EditCardToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.O | System.Windows.Forms.Keys.Control;
		this.EditCardToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
		this.EditCardToolStripMenuItem.Text = "Edit Card";
		this.RenameToolStripMenuItem.Enabled = false;
		this.RenameToolStripMenuItem.Name = "RenameToolStripMenuItem";
		this.RenameToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.R | System.Windows.Forms.Keys.Control;
		this.RenameToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
		this.RenameToolStripMenuItem.Text = "Rename";
		this.MoveToTopToolStripMenuItem.Enabled = false;
		this.MoveToTopToolStripMenuItem.Name = "MoveToTopToolStripMenuItem";
		this.MoveToTopToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.W | System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Control;
		this.MoveToTopToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
		this.MoveToTopToolStripMenuItem.Text = "Move To Top";
		this.MoveUpToolStripMenuItem.Enabled = false;
		this.MoveUpToolStripMenuItem.Name = "MoveUpToolStripMenuItem";
		this.MoveUpToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.W | System.Windows.Forms.Keys.Control;
		this.MoveUpToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
		this.MoveUpToolStripMenuItem.Text = "Move Up";
		this.MoveDownToolStripMenuItem.Enabled = false;
		this.MoveDownToolStripMenuItem.Name = "MoveDownToolStripMenuItem";
		this.MoveDownToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.S | System.Windows.Forms.Keys.Control;
		this.MoveDownToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
		this.MoveDownToolStripMenuItem.Text = "Move Down";
		this.MoveToBottomToolStripMenuItem.Enabled = false;
		this.MoveToBottomToolStripMenuItem.Name = "MoveToBottomToolStripMenuItem";
		this.MoveToBottomToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.S | System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Control;
		this.MoveToBottomToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
		this.MoveToBottomToolStripMenuItem.Text = "Move To Bottom";
		this.IncPriorityToolStripMenuItem.Enabled = false;
		this.IncPriorityToolStripMenuItem.Name = "IncPriorityToolStripMenuItem";
		this.IncPriorityToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.E | System.Windows.Forms.Keys.Control;
		this.IncPriorityToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
		this.IncPriorityToolStripMenuItem.Text = "Inc. Priority";
		this.DecPriorityToolStripMenuItem.Enabled = false;
		this.DecPriorityToolStripMenuItem.Name = "DecPriorityToolStripMenuItem";
		this.DecPriorityToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Q | System.Windows.Forms.Keys.Control;
		this.DecPriorityToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
		this.DecPriorityToolStripMenuItem.Text = "Dec. Priority";
		this.SettingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[1] { this.SettingsToolStripMenuItem1 });
		this.SettingsToolStripMenuItem.Name = "SettingsToolStripMenuItem";
		this.SettingsToolStripMenuItem.Size = new System.Drawing.Size(61, 19);
		this.SettingsToolStripMenuItem.Text = "Settings";
		this.SettingsToolStripMenuItem1.Name = "SettingsToolStripMenuItem1";
		this.SettingsToolStripMenuItem1.Size = new System.Drawing.Size(116, 22);
		this.SettingsToolStripMenuItem1.Text = "Settings";
		this.DeckMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[4] { this.FileToolStripMenuItem1, this.EditToolStripMenuItem, this.SettingsToolStripMenuItem, this.HelpToolStripMenuItem });
		this.DeckMenu.Location = new System.Drawing.Point(0, 0);
		this.DeckMenu.Name = "DeckMenu";
		this.DeckMenu.Padding = new System.Windows.Forms.Padding(8, 3, 0, 3);
		this.DeckMenu.Size = new System.Drawing.Size(1005, 25);
		this.DeckMenu.TabIndex = 0;
		this.DeckMenu.Text = "DeckMenu";
		this.HelpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[1] { this.OpenManualToolStripMenuItem });
		this.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem";
		this.HelpToolStripMenuItem.Size = new System.Drawing.Size(44, 19);
		this.HelpToolStripMenuItem.Text = "Help";
		this.OpenManualToolStripMenuItem.Name = "OpenManualToolStripMenuItem";
		this.OpenManualToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
		this.OpenManualToolStripMenuItem.Text = "Manual";
		this.ListMods.BackColor = System.Drawing.Color.Black;
		this.ListMods.CheckBoxes = true;
		this.ListMods.Dock = System.Windows.Forms.DockStyle.Left;
		this.ListMods.ForeColor = System.Drawing.Color.White;
		this.ListMods.Location = new System.Drawing.Point(0, 122);
		this.ListMods.Name = "ListMods";
		this.ListMods.Size = new System.Drawing.Size(121, 253);
		this.ListMods.TabIndex = 8;
		base.AutoScaleDimensions = new System.Drawing.SizeF(96f, 96f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
		this.BackColor = System.Drawing.Color.FromArgb(64, 64, 64);
		base.ClientSize = new System.Drawing.Size(1005, 501);
		base.Controls.Add(this.CardManager);
		base.Controls.Add(this.ListMods);
		base.Controls.Add(this.Info);
		base.Controls.Add(this.ControlPanel);
		base.Controls.Add(this.ModDisplay);
		base.Controls.Add(this.SidePanel);
		base.Controls.Add(this.DeckMenu);
		this.Font = new System.Drawing.Font("Impact", 15.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.ForeColor = System.Drawing.Color.White;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.MainMenuStrip = this.DeckMenu;
		base.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
		base.Name = "FormDeckManager";
		this.Text = "MadCard Deck Manager";
		this.ControlPanel.ResumeLayout(false);
		this.SideControls.ResumeLayout(false);
		this.SidePanel.ResumeLayout(false);
		this.SidePanel.PerformLayout();
		this.DeckMenu.ResumeLayout(false);
		this.DeckMenu.PerformLayout();
		base.ResumeLayout(false);
		base.PerformLayout();
	}

	private void MadMaker_Load(object sender, EventArgs e)
	{
		if (new DialogLoader
		{
			Purpose = ModEnums.CardType.StatCard
		}.ShowDialog() != DialogResult.Yes)
		{
			Close();return;
		}
		ToolTip.SetToolTip(CP_AddDeck, "Add New Deck");
		ToolTip.SetToolTip(CP_Add, "Add");
		ToolTip.SetToolTip(CP_Remove, "Remove");
		ToolTip.SetToolTip(CP_Edit, "Edit");
		ToolTip.SetToolTip(CP_Rename, "Rename");
		ToolTip.SetToolTip(CP_Sort, "Sort");
		ToolTip.SetToolTip(CP_ShiftUp, "Move Up");
		ToolTip.SetToolTip(CP_ShiftDown, "Move Down");
		ToolTip.SetToolTip(CP_Unlist, "Unlist Card");
		ToolTip.SetToolTip(CP_PriorityUp, "Priority Up");
		ToolTip.SetToolTip(CP_PriorityDown, "Priority Down");
		ToolTip.SetToolTip(CP_Restore, "Restore to Selected Deck");
		ToolTip.SetToolTip(CP_Delete, "Delete Card");
		ToolTip.SetToolTip(CP_View, "View");
		ToolTip.SetToolTip(CP_Save, "Save All Decks");
		ToolTip.SetToolTip(CP_Refresh, "Return to Mods");
		ListMods.Dock = DockStyle.Fill;
		ModColors.ApplyMenuColors(this);
		if (ModEnums.MOD_DIR.Length == 0)
		{
			if (!MySettingsProperty.Settings.LocalWorkspace)
			{
				string text = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)) + "Low/Gibbing Tree/Madness Project Nexus/Mods/";
				if (Directory.Exists(text))
				{
					ModEnums.MOD_DIR = text;
				}
				else
				{
					// A local workspace is available immediately; no blocking setup popup.
					MySettingsProperty.Settings.LocalWorkspace = true;
				}
			}
			if (MySettingsProperty.Settings.LocalWorkspace)
			{
				ModEnums.MOD_DIR = StudioModule.BaseDirectory + "/Mods/";
				if (!Directory.Exists(ModEnums.MOD_DIR))
				{
					Directory.CreateDirectory(ModEnums.MOD_DIR);
				}
			}
		}
		ReadMods(FirstLoad: true);
	}

	private void MadMaker_Show(object sender, EventArgs e)
	{
		ModColors.HighlightToolTips(Info, "\\[.+\\]", ModColors.MenuColors.Highlight);
		ModColors.HighlightToolTips(Info, "\\(.+\\)", ModColors.MenuColors.Note);
		if (MySettingsProperty.Settings.Fullscreen)
		{
			WindowState = FormWindowState.Maximized;
		}
	}

	private void CP_AddDeck_Click(object sender, EventArgs e)
	{
		DialogNameEntry dialogNameEntry = new DialogNameEntry
		{
			DefaultName = "New Deck"
		};
		if (dialogNameEntry.ShowDialog() == DialogResult.OK)
		{
			if (CardManager.Nodes[dialogNameEntry.NameField.Text] == null)
			{
				AddDeck(dialogNameEntry.NameField.Text);
			}
			else
			{
				DossierDialog.MsgBox("Deck name is aleady taken", MsgBoxStyle.Exclamation, "Nope way!");
			}
		}
	}

	private void CP_Add_Click(object sender, EventArgs e)
	{
		if (StudioModule.Hosted && CardManager.Visible && TryStartStudioCardCreation())
		{
			return;
		}
		if (CardManager.Visible)
		{
			FormEditor formEditor = new FormEditor
			{
				Manager = this
			};
			if (SelectedTag.ObjType != ModCards.NodeType.Obj)
			{
				formEditor.PreloadType = ((SelectedTag.ObjType == ModCards.NodeType.Arr) ? SelectedTag.FileName : SelectedTag.Type);
			}
			if (SelectedTag.ObjType == ModCards.NodeType.Val)
			{
				if ((Operators.CompareString(formEditor.PreloadType, "Event", TextCompare: false) != 0) & (Operators.CompareString(formEditor.PreloadType, "Cutscene", TextCompare: false) != 0))
				{
					formEditor.PreloadRoot = SelectedTag.FileName;
					formEditor.PreloadPriority = checked(SelectedTag.Priority + 1);
				}
				formEditor.UnlistAllValues = SelectedTag.Converter;
			}
			StudioModule.ShowDocument(formEditor);
			return;
		}
		DialogModInfo dialogModInfo = new DialogModInfo
		{
			DefaultName = "New Mod",
			DefaultFolder = "New Folder",
			DefaultDesc = "Description",
			DefaultAuthor = "Author(s)",
			DefaultTags = "tag1 tag2 tag3"
		};
		if (dialogModInfo.ShowDialog() == DialogResult.OK)
		{
			if (!Directory.Exists(ModEnums.MOD_DIR + dialogModInfo.FolderName.Text))
			{
				string text = ModEnums.MOD_DIR + dialogModInfo.FolderName.Text;
				Directory.CreateDirectory(text);
				JObject jObject = new JObject();
				jObject.Add("Name", new JValue(dialogModInfo.ModName.Text));
				jObject.Add("Description", new JValue(dialogModInfo.ModDesc.Text));
				jObject.Add("Author", new JValue(dialogModInfo.Author.Text));
				jObject.Add("Tags", new JArray(dialogModInfo.ModTags.Text.Split(' ')));
				JObject jObject2 = jObject;
				MakerHelp.Write(Path.Combine(text + "\\", "info.txt"), jObject2.ToString());
				ReadMods(FirstLoad: false);
			}
			else
			{
				DossierDialog.MsgBox("Mod name is aleady taken", MsgBoxStyle.Exclamation, "Nope way!");
			}
		}
	}

	private void CP_Remove_Click(object sender, EventArgs e)
	{
		if (CardManager.Visible)
		{
			TreeNode selectedNode = CardManager.SelectedNode;
			TreeNode selectedNode2;
			if (selectedNode.NextNode != null)
			{
				selectedNode2 = selectedNode.NextNode;
			}
			else if (selectedNode.PrevNode != null)
			{
				selectedNode2 = selectedNode.PrevNode;
			}
			else
			{
				selectedNode2 = ((selectedNode.Parent != null) ? selectedNode.Parent : null);
			}
			RemoveAllCards(CardManager.SelectedNode);
			CardManager.SelectedNode = selectedNode2;
			SidePanel.Visible = RemovedCards.Nodes.Count > 0;
			ReloadAllDecks();
		}
		else
		{
			string name = ListMods.SelectedNode.Name;
			if (Directory.Exists(ModEnums.MOD_DIR + name) && DossierDialog.MsgBox("This will delete all mod contents into the recycle bin. Continue?", MsgBoxStyle.YesNo, "Delete Mod?") == MsgBoxResult.Yes)
			{
				MyProject.Computer.FileSystem.DeleteDirectory(ModEnums.MOD_DIR + name, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);
				ReadMods(FirstLoad: false);
			}
		}
	}

	private void CP_Edit_Click(object sender, EventArgs e)
	{
		if (CardManager.Visible)
		{
			FormEditor formEditor = new FormEditor();
			formEditor.Manager = this;
			formEditor.PreloadFile = SelectedTag.FileName;
			formEditor.PreloadFileInfo = SelectedTag.Info.ToString();
			formEditor.PreloadType = SelectedTag.Type;
			formEditor.PreloadRoot = SelectedTag.RootName;
			formEditor.PreloadPriority = SelectedTag.Priority;
			formEditor.UnlistAllValues = SelectedTag.Converter;
			StudioModule.ShowDocument(formEditor);
		}
		else
		{
			LoadMod();
		}
	}

	private void CP_Rename_Click(object sender, EventArgs e)
	{
		if (CardManager.Visible)
		{
			DialogNameEntry dialogNameEntry = new DialogNameEntry
			{
				DefaultName = SelectedTag.FileName
			};
			TreeNodeCollection treeNodeCollection = ((SelectedTag.ObjType == ModCards.NodeType.Obj) ? CardManager.Nodes : CardManager.SelectedNode.Parent.Nodes);
			if (dialogNameEntry.ShowDialog() != DialogResult.OK)
			{
				return;
			}
			if (treeNodeCollection[dialogNameEntry.NameField.Text] == null)
			{
				if ((SelectedTag.ObjType == ModCards.NodeType.Obj) & File.Exists(ModEnums.MOD_DIR + "/" + LoadedMod + "/" + SelectedTag.FileName + ".json"))
				{
					MyProject.Computer.FileSystem.RenameFile(ModEnums.MOD_DIR + "/" + LoadedMod + "/" + SelectedTag.FileName + ".json", dialogNameEntry.NameField.Text);
				}
				SelectedTag.FileName = dialogNameEntry.NameField.Text;
				CardManager.SelectedNode.Name = dialogNameEntry.NameField.Text;
				CardManager.SelectedNode.Text = dialogNameEntry.NameField.Text;
				if (SelectedTag.ObjType == ModCards.NodeType.Val)
				{
					if (SelectedTag.Info["Filename"] != null)
					{
						SelectedTag.Info.Property("Filename").Value = new JValue(dialogNameEntry.NameField.Text);
					}
					else
					{
						SelectedTag.Info.Add("Filename", new JValue(dialogNameEntry.NameField.Text));
					}
				}
				ReloadAllDecks();
			}
			else
			{
				DossierDialog.MsgBox("Object name is aleady taken", MsgBoxStyle.Exclamation, "Nope way!");
			}
			return;
		}
		ModCards.ModTag modTag = (ModCards.ModTag)ListMods.SelectedNode.Tag;
		DialogModInfo dialogModInfo = new DialogModInfo
		{
			DefaultName = modTag.DisplayName,
			DefaultFolder = modTag.FileName,
			DefaultDesc = modTag.Description,
			DefaultAuthor = modTag.Authors,
			DefaultTags = string.Join(" ", modTag.ModTags)
		};
		if (dialogModInfo.ShowDialog() != DialogResult.OK)
		{
			return;
		}
		JObject jObject = new JObject();
		jObject.Add("Name", new JValue(dialogModInfo.ModName.Text));
		jObject.Add("Description", new JValue(dialogModInfo.ModDesc.Text));
		jObject.Add("Author", new JValue(dialogModInfo.Author.Text));
		jObject.Add("Tags", new JArray(dialogModInfo.ModTags.Text.Split(' ')));
		JObject jObject2 = jObject;
		MakerHelp.Write(Path.Combine(ModEnums.MOD_DIR + modTag.FileName + "\\", "info.txt"), jObject2.ToString());
		if (Operators.CompareString(modTag.FileName, dialogModInfo.FolderName.Text, TextCompare: false) != 0)
		{
			if (!Directory.Exists(ModEnums.MOD_DIR + dialogModInfo.FolderName.Text))
			{
				MyProject.Computer.FileSystem.RenameDirectory(ModEnums.MOD_DIR + modTag.FileName, dialogModInfo.FolderName.Text);
			}
			else
			{
				DossierDialog.MsgBox("Info was still changed, but Mod folder name is aleady taken.", MsgBoxStyle.Exclamation, "Nope way!");
			}
		}
		ReadMods(FirstLoad: false);
	}

	private void CP_ShiftUp_Click(object sender, EventArgs e)
	{
		TreeNode selectedNode = CardManager.SelectedNode;
		if (CardManager.SelectedNode.Parent != null)
		{
			TreeNodeCollection nodes = CardManager.SelectedNode.Parent.Nodes;
			int index = CardManager.SelectedNode.Index;
			if (index > 0)
			{
				CP_Save.Enabled = true;
				selectedNode.Remove();
				nodes.Insert(checked(index - 1), selectedNode);
				CardManager.SelectedNode = selectedNode;
				CardManager.Focus();
			}
		}
	}

	private void CP_ShiftDown_Click(object sender, EventArgs e)
	{
		TreeNode selectedNode = CardManager.SelectedNode;
		checked
		{
			if (CardManager.SelectedNode.Parent != null)
			{
				TreeNodeCollection nodes = CardManager.SelectedNode.Parent.Nodes;
				int index = CardManager.SelectedNode.Index;
				if (index < nodes.Count - 1)
				{
					CP_Save.Enabled = true;
					selectedNode.Remove();
					nodes.Insert(index + 1, selectedNode);
					CardManager.SelectedNode = selectedNode;
					CardManager.Focus();
				}
			}
		}
	}

	private void CP_Sort_Click(object sender, EventArgs e)
	{
		CardManager.Sort();
		CP_Save.Enabled = true;
		CardManager.Focus();
	}

	private void CP_Unlist_Click(object sender, EventArgs e)
	{
		UnlistAllValues(CardManager.SelectedNode, @switch: true);
		CP_Save.Enabled = true;
		CardManager.Focus();
	}

	private void CP_PriorityDown_Click(object sender, EventArgs e)
	{
		ShiftPriority(CardManager.SelectedNode, -1);
		CardManager.Refresh();
		CP_Save.Enabled = true;
	}

	private void CP_PriorityUp_Click(object sender, EventArgs e)
	{
		ShiftPriority(CardManager.SelectedNode, 1);
		CardManager.Refresh();
		CP_Save.Enabled = true;
	}

	private void CP_View_Click(object sender, EventArgs e)
	{
		if (CardManager.Visible)
		{
			FormViewer formViewer = new FormViewer();
			formViewer.Card = SelectedTag.Info;
			StudioModule.ShowDocument(formViewer);
		}
		else
		{
			ViewOnly = true;
			LoadMod();
		}
	}

	private void CP_Restore_Click(object sender, EventArgs e)
	{
		if ((CardManager.SelectedNode == null) | (SelectedTag.ObjType == ModCards.NodeType.Log))
		{
			DossierDialog.MsgBox("No deck selected", MsgBoxStyle.Exclamation, "Nope way!");
			return;
		}
		ModCards.CardTag cardTag = (ModCards.CardTag)RemovedCards.SelectedNode.Tag;
		TreeNode rootNode = ModNodes.GetRootNode(CardManager.SelectedNode);
		cardTag.AttachedDeck = rootNode.Name;
		cardTag.AttachedMod = LoadedMod;
		AddCard(rootNode, cardTag, cardTag.Priority != 0);
		RemovedCards.SelectedNode.Remove();
		RemovedCards.SelectedNode = null;
		CP_Restore.Enabled = false;
		CP_Delete.Enabled = false;
		SidePanel.Visible = RemovedCards.Nodes.Count > 0;
	}

	private void CP_Delete_Click(object sender, EventArgs e)
	{
		if (DossierDialog.MsgBox("This item cannot be recovered. Delete?", MsgBoxStyle.YesNo, "Deletion imminent") == MsgBoxResult.Yes)
		{
			RemovedCards.SelectedNode.Remove();
			CP_Delete.Enabled = false;
			CP_Restore.Enabled = false;
			RemovedCards.SelectedNode = null;
			SidePanel.Visible = RemovedCards.Nodes.Count > 0;
		}
	}

	private void CP_Refresh_Click(object sender, EventArgs e)
	{
		if (CardManager.Visible)
		{
			if (Application.OpenForms.OfType<FormEditor>().Count() > 0)
			{
				DossierDialog.MsgBox("Cannot return while editors are open", MsgBoxStyle.Exclamation, "Editors open");
				return;
			}
			if (RemovedCards.Nodes.Count > 0 && DossierDialog.MsgBox("There are removed cards in your workspace. Returning to the mod menu will delete them all forever. Continue?", MsgBoxStyle.YesNo, "Unused Cards") != MsgBoxResult.Yes)
			{
				return;
			}
			Loading = true;
			ViewOnly = false;
			CardManager.SelectedNode = null;
			RemovedCards.Nodes.Clear();
			SideLabel.Text = "Load Order";
			SidePanel.Visible = true;
			SideControls.Visible = false;
			CP_Restore.Enabled = false;
			CP_Delete.Enabled = false;
			CardManager.Nodes.Clear();
			CardManager.Enabled = false;
			CardManager.Visible = false;
			ListMods.Visible = true;
			ListMods.Enabled = true;
			ListMods.Dock = DockStyle.Fill;
			CP_AddDeck.Visible = false;
			CP_Add.Enabled = true;
			CP_Remove.Enabled = false;
			CP_Edit.Enabled = false;
			CP_Rename.Enabled = false;
			CP_Add.Visible = true;
			CP_Remove.Visible = true;
			CP_Edit.Visible = true;
			CP_Rename.Visible = true;
			CP_ShiftDown.Visible = false;
			CP_ShiftUp.Visible = false;
			CP_Sort.Visible = false;
			CP_Unlist.Visible = false;
			CP_PriorityDown.Visible = false;
			CP_PriorityUp.Visible = false;
			CP_View.Visible = true;
			CP_View.Enabled = false;
			CP_Refresh.Visible = true;
			CP_Refresh.Enabled = false;
			CP_Save.Visible = false;
			NewDeckToolStripMenuItem1.Enabled = false;
			NewCardToolStripMenuItem1.Enabled = true;
			RemoveToolStripMenuItem.Enabled = false;
			EditCardToolStripMenuItem.Enabled = false;
			MoveToTopToolStripMenuItem.Enabled = false;
			MoveUpToolStripMenuItem.Enabled = false;
			MoveDownToolStripMenuItem.Enabled = false;
			MoveToBottomToolStripMenuItem.Enabled = false;
			IncPriorityToolStripMenuItem.Enabled = false;
			DecPriorityToolStripMenuItem.Enabled = false;
			ViewToolStripMenuItem.Enabled = true;
			RenameToolStripMenuItem.Enabled = true;
			Info.Text = "";
			ModDisplay.Text = "Mod Loader";
		}
		else
		{
			EnabledMods = GetEnabledListMods();
			CP_Refresh.Enabled = false;
		}
		ReadMods(FirstLoad: false);
	}

	private void CP_Save_Click(object sender, EventArgs e)
	{
		ReloadAllDecks(WriteInstead: true);
	}

	private void CardManager_DrawNode(object sender, DrawTreeNodeEventArgs e)
	{
		e.DrawDefault = true;
		if (e.Node.Tag == null)
		{
			return;
		}
		ModCards.CardTag cardTag = (ModCards.CardTag)e.Node.Tag;
		if (cardTag.ObjType == ModCards.NodeType.Val)
		{
			Color color = ModNodes.PriorityLabel(cardTag);
			string text = ((cardTag.Priority > 0) ? "+" : "");
			if ((cardTag.Priority != 0) | (color == ModColors.MenuColors.Warning))
			{
				TextRenderer.DrawText(e.Graphics, text + cardTag.Priority, e.Node.NodeFont, new Point(checked(e.Node.Bounds.Right + 2), e.Node.Bounds.Top), color);
			}
		}
	}

	private void CardManager_AfterSelect(object sender, TreeViewEventArgs e)
	{
		if (CardManager.SelectedNode != null)
		{
			SelectedTag = (ModCards.CardTag)CardManager.SelectedNode.Tag;
			bool flag = SelectedTag.ObjType == ModCards.NodeType.Val;
			bool flag2 = SelectedTag.ObjType == ModCards.NodeType.Arr;
			bool flag3 = SelectedTag.ObjType == ModCards.NodeType.Obj;
			bool flag4 = SelectedTag.ObjType == ModCards.NodeType.Log;
			bool flag5 = flag3 | flag4;
			ref string selectedCategory = ref SelectedCategory;
			string text;
			if (flag)
			{
				text = CardManager.SelectedNode.Parent.Name;
			}
			else
			{
				text = (flag2 ? CardManager.SelectedNode.Name : "");
			}
			selectedCategory = text;
			CP_Add.Enabled = !flag4;
			CP_Remove.Enabled = !flag4;
			CP_Sort.Enabled = true;
			CP_Unlist.Enabled = flag;
			CP_ShiftDown.Enabled = !flag5 && CardManager.SelectedNode.Parent.Nodes.Count > 1;
			CP_ShiftUp.Enabled = !flag5 && CardManager.SelectedNode.Parent.Nodes.Count > 1;
			CP_PriorityUp.Enabled = flag | (CardManager.SelectedNode.Nodes.Count > 0);
			CP_PriorityDown.Enabled = flag | (CardManager.SelectedNode.Nodes.Count > 0);
			CP_View.Enabled = flag | (flag4 & (CardManager.SelectedNode.ForeColor != ModColors.MenuColors.Warning));
			CP_Edit.Enabled = flag;
			CP_Rename.Enabled = flag | flag3;
			NewDeckToolStripMenuItem1.Enabled = CP_AddDeck.Enabled;
			NewCardToolStripMenuItem1.Enabled = CP_Add.Enabled;
			RemoveToolStripMenuItem.Enabled = CP_Remove.Enabled;
			EditCardToolStripMenuItem.Enabled = CP_Edit.Enabled;
			MoveToTopToolStripMenuItem.Enabled = CP_Sort.Enabled;
			MoveUpToolStripMenuItem.Enabled = CP_ShiftUp.Enabled;
			MoveDownToolStripMenuItem.Enabled = CP_ShiftDown.Enabled;
			MoveToBottomToolStripMenuItem.Enabled = CP_Unlist.Enabled;
			IncPriorityToolStripMenuItem.Enabled = CP_PriorityUp.Enabled;
			DecPriorityToolStripMenuItem.Enabled = CP_PriorityDown.Enabled;
			RenameToolStripMenuItem.Enabled = CP_Rename.Enabled;
			DisplayToolTip(SelectedTag);
		}
	}

	private void ListMods_AfterSelect(object sender, TreeViewEventArgs e)
	{
		if (ListMods.SelectedNode != null)
		{
			ModCards.ModTag modTag = (ModCards.ModTag)ListMods.SelectedNode.Tag;
			LoadedMod = modTag.FileName;
			LoadModDirectory = modTag.Directory;
			CP_Remove.Enabled = !modTag.Workshop;
			CP_Edit.Enabled = !modTag.Workshop;
			CP_Rename.Enabled = !modTag.Workshop;
			CP_View.Enabled = true;
			RemoveToolStripMenuItem.Enabled = !modTag.Workshop;
			EditCardToolStripMenuItem.Enabled = !modTag.Workshop;
			RenameToolStripMenuItem.Enabled = !modTag.Workshop;
			CP_View.Enabled = true;
			DisplayToolTip(modTag);
		}
	}

	private void ListMods_AfterCheck(object sender, TreeViewEventArgs e)
	{
		CP_Refresh.Enabled = true;
	}

	private void RemovedCards_AfterSelect(object sender, TreeViewEventArgs e)
	{
		if (CardManager.Visible)
		{
			CP_Restore.Enabled = RemovedCards.SelectedNode != null;
			CP_Delete.Enabled = RemovedCards.SelectedNode != null;
			if (RemovedCards.SelectedNode != null)
			{
				DeletedTag = (ModCards.CardTag)RemovedCards.SelectedNode.Tag;
				DisplayToolTip(DeletedTag);
			}
		}
	}

	private string GetEnabledMods()
	{
		string result;
		if (File.Exists(ModEnums.MOD_DIR + "/enabled.txt"))
		{
			try
			{
				JObject jObject = JObject.Parse(File.ReadAllText(ModEnums.MOD_DIR + "/enabled.txt"));
				result = ((jObject["Enabled"] != null) ? jObject["Enabled"].ToString() : "");
			}
			catch (JsonReaderException ex)
			{
				ProjectData.SetProjectError(ex);
				JsonReaderException ex2 = ex;
				DossierDialog.MsgBox("Error reading enabled.txt:\r\n" + ex2.Message, MsgBoxStyle.Exclamation);
				result = "";
				ProjectData.ClearProjectError();
			}
		}
		else
		{
			result = "";
		}
		return result;
	}

	private string GetEnabledListMods()
	{
		JArray jArray = new JArray();
		foreach (TreeNode node in ListMods.Nodes)
		{
			if (node.Checked)
			{
				jArray.Add(RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(node.Tag, null, "WorkshopID", new object[0], null, null, null)));
			}
		}
		if (MySettingsProperty.Settings.CrossEnable)
		{
			JObject jObject = new JObject { { "Enabled", jArray } };
			MakerHelp.Write(ModEnums.MOD_DIR + "/enabled.txt", jObject.ToString());
		}
		return jArray.ToString();
	}

	public string GetCurrentMod()
	{
		return LoadedMod;
	}

	private void LoadMod()
	{
		if (new DialogLoader
		{
			Purpose = ModEnums.CardType.MadCard,
			ModDir = LoadModDirectory
		}.ShowDialog() == DialogResult.Yes)
		{
			ListMods.Dock = DockStyle.Left;
			CardManager.Visible = true;
			CardManager.Enabled = true;
			RemovedCards.Nodes.Clear();
			SidePanel.Visible = false;
			SideControls.Visible = true;
			SideLabel.Text = "Removed";
			ListMods.Visible = false;
			ListMods.Enabled = false;
			CardManager.Visible = true;
			CP_AddDeck.Visible = !ViewOnly;
			CP_Add.Enabled = !ViewOnly;
			CP_Remove.Enabled = !ViewOnly;
			CP_Edit.Enabled = !ViewOnly;
			CP_Rename.Enabled = !ViewOnly;
			CP_Add.Visible = !ViewOnly;
			CP_Add.Enabled = false;
			CP_Remove.Visible = !ViewOnly;
			CP_Remove.Enabled = false;
			CP_Edit.Visible = !ViewOnly;
			CP_Edit.Enabled = false;
			CP_Rename.Visible = !ViewOnly;
			CP_Rename.Enabled = false;
			CP_ShiftDown.Visible = !ViewOnly;
			CP_ShiftDown.Enabled = false;
			CP_ShiftUp.Visible = !ViewOnly;
			CP_ShiftUp.Enabled = false;
			CP_Sort.Visible = !ViewOnly;
			CP_Sort.Enabled = false;
			CP_Unlist.Visible = !ViewOnly;
			CP_Unlist.Enabled = false;
			CP_PriorityDown.Visible = !ViewOnly;
			CP_PriorityDown.Enabled = false;
			CP_PriorityUp.Visible = !ViewOnly;
			CP_PriorityUp.Enabled = false;
			CP_View.Visible = true;
			CP_View.Enabled = false;
			CP_Refresh.Visible = true;
			CP_Refresh.Enabled = true;
			CP_Save.Visible = !ViewOnly;
			CP_Save.Enabled = false;
			ModDisplay.Text = (ViewOnly ? "Deck Viewer" : "Deck Manager");
			NewDeckToolStripMenuItem1.Enabled = !ViewOnly;
			NewCardToolStripMenuItem1.Enabled = false;
			RemoveToolStripMenuItem.Enabled = false;
			ViewToolStripMenuItem.Enabled = false;
			EditCardToolStripMenuItem.Enabled = false;
			RebuildDirectory(GlobalBuild: false, RebuildNodes: true);
		}
	}

	private void ReadMods(bool FirstLoad)
	{
		LoadedMod = "";
		LoadModDirectory = "";
		DialogLoader dialogLoader = new DialogLoader
		{
			Purpose = ModEnums.CardType.ModFolder
		};
		if (FirstLoad)
		{
			EnabledMods = GetEnabledMods();
		}
		dialogLoader.EnabledModToken = EnabledMods;
		if (dialogLoader.ShowDialog() == DialogResult.Yes)
		{
			RebuildMods();
		}
	}

	public void RebuildMods()
	{
		Loading = true;
		ListMods.Nodes.Clear();
		foreach (ModCards.ModTag item in ModEnums.MODSORT)
		{
			TreeNode treeNode = new TreeNode(item.DisplayName)
			{
				Name = item.FileName,
				Tag = item,
				Checked = item.Enabled
			};
			if (item.Workshop)
			{
				treeNode.ForeColor = ModColors.MenuColors.Highlight;
			}
			ListMods.Nodes.Add(treeNode);
		}
		RemovedCards.Nodes.Clear();
		RebuildDirectory(GlobalBuild: true);
	}

	public void RebuildDirectory(bool GlobalBuild, bool RebuildNodes = false)
	{
		Loading = true;
		if (GlobalBuild)
		{
			ModEnums.MC_DIR.Clear();
			ModEnums.MC_VER.Clear();
			foreach (string key in ModEnums.SC_DIR.Keys)
			{
				ModEnums.MC_DIR.Add(key, new SortedDictionary<string, JObject>(StringComparer.OrdinalIgnoreCase));
			}
		}
		foreach (string item in ModEnums.DECKADD)
		{
			if (RebuildNodes)
			{
				AddDeck(item);
			}
		}
		ModEnums.DECKADD.Clear();
		string[] source = new string[3] { "Melee", "Thrown", "Ranged" };
		string[] source2 = new string[5] { "Melee", "Thrown", "Ranged", "Collectible", "Armor" };
		foreach (ModCards.CardTag item2 in ModEnums.DECKSORT)
		{
			if (RebuildNodes)
			{
				AddCard(CardManager.Nodes[item2.AttachedDeck], item2);
			}
			if (GlobalBuild)
			{
				AddToLoadOrder(item2, ModEnums.MC_DIR[item2.Type].ContainsKey(item2.FileName));
				if (ModEnums.MC_DIR[item2.Type].ContainsKey(item2.FileName))
				{
					ModEnums.MC_DIR[item2.Type][item2.FileName] = item2.Info;
				}
				else
				{
					ModEnums.MC_DIR[item2.Type].Add(item2.FileName, item2.Info);
				}
				if (source.Contains(item2.Type))
				{
					if (ModEnums.MC_DIR["Weapon"].ContainsKey(item2.FileName))
					{
						ModEnums.MC_DIR["Weapon"][item2.FileName] = item2.Info;
					}
					else
					{
						ModEnums.MC_DIR["Weapon"].Add(item2.FileName, item2.Info);
					}
				}
				if (source2.Contains(item2.Type))
				{
					if (ModEnums.MC_DIR["Item"].ContainsKey(item2.FileName))
					{
						ModEnums.MC_DIR["Item"][item2.FileName] = item2.Info;
					}
					else
					{
						ModEnums.MC_DIR["Item"].Add(item2.FileName, item2.Info);
					}
				}
				ModNodes.AddCardVersion(item2);
			}
			JObject jObject = ModNodes.FinalVersion(item2.Type, item2.Info);
			if (jObject["Name"] != null)
			{
				item2.DisplayName = jObject["Name"].ToString();
			}
			if (jObject["Description"] != null)
			{
				item2.Description = jObject["Description"].ToString();
			}
		}
		ModEnums.DECKSORT.Clear();
		if (RebuildNodes & (ModEnums.CURRLOG.Length > 0))
		{
			AddLog(ModEnums.CURRLOG);
		}
		ModEnums.CURRLOG = "";
		ModEnums.LOGSTATUS = ModEnums.LogType.None;
		CardManager.Refresh();
		Loading = false;
	}

	public void AddToLoadOrder(ModCards.CardTag Card, bool MarkAsOverride)
	{
		TreeNode treeNode = new TreeNode(Card.Type + ": " + Card.FileName);
		treeNode.Nodes.Add("Mod: " + Card.AttachedMod);
		treeNode.Nodes.Add("Deck: " + Card.AttachedDeck);
		treeNode.Nodes.Add("Root: " + Card.RootName);
		if (Card.Priority != 0)
		{
			treeNode.Nodes.Add("Priority: " + Conversions.ToString(Card.Priority));
		}
		if (Card.Converter)
		{
			treeNode.Nodes.Add("Converter: " + Conversions.ToString(Card.Converter));
		}
		treeNode.ForeColor = (ModEnums.SC_DIR[Card.Type].ContainsKey(Card.FileName) ? Color.GreenYellow : Color.Green);
		if (MarkAsOverride)
		{
			treeNode.ForeColor = Color.Yellow;
		}
		if (ModNodes.PriorityConflict(Card))
		{
			treeNode.ForeColor = Color.OrangeRed;
		}
		RemovedCards.Nodes.Insert(0, treeNode);
	}

	public void ReloadAllDecks(bool WriteInstead = false)
	{
		foreach (TreeNode node in CardManager.Nodes)
		{
			if (Operators.ConditionalCompareObjectEqual(NewLateBinding.LateGet(node.Tag, null, "ObjType", new object[0], null, null, null), ModCards.NodeType.Log, TextCompare: false))
			{
				continue;
			}
			JObject jObject = new JObject();
			foreach (TreeNode node2 in node.Nodes)
			{
				JArray jArray = new JArray();
				foreach (TreeNode node3 in node2.Nodes)
				{
					jArray.Add(RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(node3.Tag, null, "Info", new object[0], null, null, null)));
					if (!WriteInstead)
					{
						ModEnums.DECKSORT.Add((ModCards.CardTag)node3.Tag);
					}
				}
				jObject.Add(node2.Name, jArray);
			}
			if (!WriteInstead)
			{
				ModEnums.DECKADD.Add(node.Name);
			}
			if (MySettingsProperty.Settings.Autosave | WriteInstead)
			{
				MakerHelp.Write(Path.Combine(ModEnums.MOD_DIR + LoadedMod + "\\", node.Name + ".json"), MakerStudio.EncodeDeck(jObject).ToString());
				CP_Save.Enabled = false;
			}
		}
		if (WriteInstead)
		{
			return;
		}
		RebuildDirectory(GlobalBuild: false);
		IEnumerable<FormEditor> enumerable = from t in Application.OpenForms.OfType<FormEditor>()
			where t.ObjEditor.Nodes.Count > 0
			select t;
		foreach (FormEditor item in enumerable)
		{
			item.RefreshCard();
		}
	}

	public void AddDeck(string DeckName)
	{
		TreeNode treeNode = new TreeNode(DeckName);
		treeNode.Name = treeNode.Text;
		treeNode.Tag = new ModCards.CardTag(treeNode.Name, ModCards.NodeType.Obj);
		CardManager.Nodes.Add(treeNode);
	}

	public TreeNode AddDeckCategory(TreeNode deck, string CatType)
	{
		TreeNode treeNode = new TreeNode(CatType)
		{
			Name = CatType,
			Tag = new ModCards.CardTag(CatType, ModCards.NodeType.Arr)
		};
		deck.Nodes.Add(treeNode);
		return treeNode;
	}

	public bool AddCard(TreeNode ToDeck, ModCards.CardTag NewTag, bool SelectAfter = false, int BasePriority = 0)
	{
		TreeNode rootNode = ModNodes.GetRootNode(ToDeck);
		TreeNode treeNode = ((rootNode.Nodes[NewTag.Type] == null) ? AddDeckCategory(rootNode, NewTag.Type) : rootNode.Nodes[NewTag.Type]);
		int num = BasePriority;
		TreeNode treeNode2;
		if (rootNode.Nodes[NewTag.Type].Nodes[NewTag.FileName] != null)
		{
			treeNode2 = rootNode.Nodes[NewTag.Type].Nodes[NewTag.FileName];
			num = Conversions.ToInteger(NewLateBinding.LateGet(treeNode2.Tag, null, "Priority", new object[0], null, null, null));
		}
		else
		{
			treeNode2 = new TreeNode(NewTag.FileName);
			treeNode.Nodes.Add(treeNode2);
		}
		treeNode2.Name = NewTag.FileName;
		NewTag.AttachedDeck = rootNode.Name;
		NewTag.AttachedMod = LoadedMod;
		ModNodes.AddCardVersion(NewTag);
		if (!ModEnums.MC_DIR[NewTag.Type].ContainsKey(NewTag.FileName))
		{
			ModEnums.MC_DIR[NewTag.Type].Add(NewTag.FileName, NewTag.Info);
		}
		if (new string[3] { "Melee", "Thrown", "Ranged" }.Contains(NewTag.Type) && !ModEnums.MC_DIR["Weapon"].ContainsKey(NewTag.FileName))
		{
			ModEnums.MC_DIR["Weapon"].Add(NewTag.FileName, NewTag.Info);
		}
		if (new string[5] { "Melee", "Thrown", "Ranged", "Armor", "Collectible" }.Contains(NewTag.Type) && !ModEnums.MC_DIR["Item"].ContainsKey(NewTag.FileName))
		{
			ModEnums.MC_DIR["Item"].Add(NewTag.FileName, NewTag.Info);
		}
		treeNode2.Tag = NewTag;
		if (num != 0)
		{
			ShiftPriority(treeNode2, num);
		}
		if (NewTag.Converter)
		{
			UnlistAllValues(treeNode2);
		}
		if (SelectAfter)
		{
			CardManager.SelectedNode = treeNode2;
			SelectedTag = NewTag;
		}
		if (!Loading)
		{
			ReloadAllDecks();
		}
		return true;
	}

	public void AddLog(string loginfo)
	{
		TreeNode treeNode = new TreeNode("ASSETS");
		treeNode.Name = "ASSETS";
		string text = "";
		switch (ModEnums.LOGSTATUS)
		{
		case ModEnums.LogType.Enabled:
			treeNode.ForeColor = ModColors.MenuColors.Note;
			text = "\r\n{All assets are enabled from last play and are up to date.}";
			break;
		case ModEnums.LogType.Logged:
			treeNode.ForeColor = ModColors.MenuColors.Highlight;
			text = "\r\n{All assets read from attached log and may be out of date.}";
			break;
		case ModEnums.LogType.Unread:
			treeNode.ForeColor = ModColors.MenuColors.Warning;
			text = "\r\n{Assets are detected, but no log exists.}\r\n{If operating in a local space, you'll need to create a mobj.log file in your mod.}\r\n{If operating in MPN space, you can autogenerate them by enabling your local mods and running the game.}";
			break;
		}
		ModCards.CardTag tag = new ModCards.CardTag("Bundle Log", ModCards.NodeType.Log)
		{
			Info = ((loginfo.Length > 0) ? JObject.Parse(loginfo) : new JObject()),
			Description = "A list of all Unity Resources attached to this mod." + text
		};
		treeNode.Tag = tag;
		CardManager.Nodes.Add(treeNode);
	}

	public void RemoveAllCards(TreeNode fromSelected)
	{
		if (fromSelected == null)
		{
			return;
		}
		if (Operators.ConditionalCompareObjectEqual(NewLateBinding.LateGet(fromSelected.Tag, null, "ObjType", new object[0], null, null, null), ModCards.NodeType.Val, TextCompare: false))
		{
			TreeNode treeNode = fromSelected.Parent;
			fromSelected.Remove();
			ModNodes.RemoveCardVersion((ModCards.CardTag)fromSelected.Tag);
			if (!ModEnums.MC_VER.ContainsKey(Conversions.ToString(NewLateBinding.LateGet(fromSelected.Tag, null, "ID", new object[0], null, null, null))))
			{
				ModEnums.MC_DIR[Conversions.ToString(NewLateBinding.LateGet(fromSelected.Tag, null, "Type", new object[0], null, null, null))].Remove(Conversions.ToString(NewLateBinding.LateGet(fromSelected.Tag, null, "FileName", new object[0], null, null, null)));
				if (new string[3] { "Melee", "Thrown", "Ranged" }.Contains(Conversions.ToString(NewLateBinding.LateGet(fromSelected.Tag, null, "Type", new object[0], null, null, null))))
				{
					ModEnums.MC_DIR["Weapon"].Remove(Conversions.ToString(NewLateBinding.LateGet(fromSelected.Tag, null, "FileName", new object[0], null, null, null)));
				}
				if (new string[5] { "Melee", "Thrown", "Ranged", "Armor", "Collectible" }.Contains(Conversions.ToString(NewLateBinding.LateGet(fromSelected.Tag, null, "Type", new object[0], null, null, null))))
				{
					ModEnums.MC_DIR["Item"].Remove(Conversions.ToString(NewLateBinding.LateGet(fromSelected.Tag, null, "FileName", new object[0], null, null, null)));
				}
			}
			RemovedCards.Nodes.Add(fromSelected);
			fromSelected.Name = "";
			if (treeNode.Nodes.Count == 0)
			{
				treeNode.Remove();
			}
		}
		else
		{
			while (fromSelected.Nodes.Count > 0)
			{
				RemoveAllCards(fromSelected.Nodes[0]);
			}
			fromSelected.Remove();
			if (Operators.ConditionalCompareObjectEqual(NewLateBinding.LateGet(fromSelected.Tag, null, "ObjType", new object[0], null, null, null), ModCards.NodeType.Obj, TextCompare: false) && File.Exists(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(ModEnums.MOD_DIR + "/" + LoadedMod + "/", NewLateBinding.LateGet(fromSelected.Tag, null, "FileName", new object[0], null, null, null)), ".json"))))
			{
				MyProject.Computer.FileSystem.DeleteFile(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(ModEnums.MOD_DIR + "/" + LoadedMod + "/", NewLateBinding.LateGet(fromSelected.Tag, null, "FileName", new object[0], null, null, null)), ".json")), UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);
			}
		}
	}

	private void ShiftPriority(TreeNode node, int Amount)
	{
		ModCards.CardTag cardTag = (ModCards.CardTag)node.Tag;
		checked
		{
			if (cardTag.ObjType == ModCards.NodeType.Val)
			{
				cardTag.Priority += Amount;
				if (cardTag.Priority == 0)
				{
					if (cardTag.Info["Priority"] != null)
					{
						cardTag.Info.Remove("Priority");
					}
				}
				else
				{
					if (cardTag.Info["Priority"] == null)
					{
						cardTag.Info.Add("Priority", cardTag.Priority);
					}
					cardTag.Info["Priority"] = new JValue(cardTag.Priority);
				}
				ModNodes.AddCardVersion(cardTag);
				return;
			}
			foreach (TreeNode node2 in node.Nodes)
			{
				ShiftPriority(node2, Amount);
			}
		}
	}

	private void UnlistAllValues(TreeNode node, bool @switch = false)
	{
		ModCards.CardTag cardTag = (ModCards.CardTag)node.Tag;
		if (cardTag.ObjType == ModCards.NodeType.Val)
		{
			if (@switch)
			{
				cardTag.Converter = !cardTag.Converter;
			}
			if (!cardTag.Converter)
			{
				if (cardTag.Info["ConvertEnums"] != null)
				{
					cardTag.Info.Remove("ConvertEnums");
				}
				node.ForeColor = ModColors.MenuColors.Text;
			}
			else
			{
				if (cardTag.Info["ConvertEnums"] == null)
				{
					cardTag.Info.Add("ConvertEnums", true);
				}
				cardTag.Info["ConvertEnums"] = new JValue(cardTag.Converter);
				node.ForeColor = ModColors.MenuColors.Highlight;
			}
			CardManager.Refresh();
			return;
		}
		foreach (TreeNode node2 in node.Nodes)
		{
			UnlistAllValues(node2);
		}
	}

	private void DisplayToolTip(ModCards.CardTag displayTag)
	{
		if (displayTag.ObjType == ModCards.NodeType.Arr)
		{
			if (ModEnums.TT_PROPERTIES[SelectedCategory]["Description"] != null)
			{
				string text = ModEnums.TT_PROPERTIES[SelectedCategory]["Description"].ToString();
				Info.Text = "[" + SelectedCategory + "]\r\n" + text;
				ModColors.HighlightToolTips(Info, "\\[.+\\]:", ModColors.MenuColors.Highlight);
				ModColors.HighlightToolTips(Info, "\\(.+\\)", ModColors.MenuColors.Note);
				ModColors.HighlightToolTips(Info, "\\{.+\\}", ModColors.MenuColors.Warning);
				ModColors.HighlightToolTips(Info, "\\[[^\\[]+\\]", ModColors.MenuColors.Highlight);
			}
			return;
		}
		string text2 = ((displayTag.ObjType == ModCards.NodeType.Val) ? ModNodes.TraceRootPath(displayTag) : "");
		string text3 = ((displayTag.ObjType == ModCards.NodeType.Val) ? ModNodes.PriorityWarning(displayTag) : "");
		if (displayTag.Converter)
		{
			text3 += "\r\nThis card will register any enumerator values that may be considered unsafe.";
		}
		Info.Text = "";
		Info.Text = displayTag.Type + ": [" + displayTag.DisplayName + "]" + ((text2.Length > 0) ? (" (Extends " + text2 + ")") : "") + text3 + "\r\n" + displayTag.Description;
		Info.Select(0, displayTag.Type.Length);
		Info.SelectionColor = ((displayTag.ObjType == ModCards.NodeType.Val) ? Color.PaleGreen : Color.LightSkyBlue);
		checked
		{
			Info.Select(displayTag.Type.Length + 2, displayTag.DisplayName.Length + 2);
			Info.SelectionColor = ModColors.MenuColors.Highlight;
			if (text3.Count() > 0)
			{
				Info.Select(Info.Text.Count() - displayTag.Description.Length - text3.Length - 1, text3.Length);
				Info.SelectionColor = ModColors.MenuColors.Warning;
			}
			if (text2.Length > 0)
			{
				Info.Select(displayTag.Type.Length + displayTag.DisplayName.Length + 5, text2.Length + 10);
				Info.SelectionColor = Color.DarkGray;
			}
		}
	}

	private void DisplayToolTip(ModCards.ModTag displayTag)
	{
		Info.Text = "";
		Info.Text = "[" + displayTag.DisplayName + "] " + displayTag.FileName + "\r\n" + displayTag.Description;
		checked
		{
			Info.Select(0, displayTag.DisplayName.Length + 2);
			Info.SelectionColor = ModColors.MenuColors.Highlight;
			Info.Select(displayTag.DisplayName.Length + 2, displayTag.FileName.Length + 2);
			Info.SelectionColor = Color.DarkGray;
			ModColors.HighlightToolTips(Info, "\\[.+\\]:", ModColors.MenuColors.Highlight);
			ModColors.HighlightToolTips(Info, "\\(.+\\)", ModColors.MenuColors.Note);
			ModColors.HighlightToolTips(Info, "\\{.+\\}", ModColors.MenuColors.Warning);
			ModColors.HighlightToolTips(Info, "\\[[^\\[]+\\]", ModColors.MenuColors.Highlight);
		}
	}

	private void SettingsToolStripMenuItem1_Click(object sender, EventArgs e)
	{
		new FormSettings().ShowDialog();
	}

	private void NewDeckToolStripMenuItem1_Click(object sender, EventArgs e)
	{
		if (CP_AddDeck.Visible)
		{
			CP_AddDeck.PerformClick();
		}
	}

	private void NewCardToolStripMenuItem1_Click(object sender, EventArgs e)
	{
		if (CP_Add.Visible)
		{
			CP_Add.PerformClick();
		}
	}

	private void RemoveToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (CP_Remove.Visible)
		{
			CP_Remove.PerformClick();
		}
	}

	private void SaveAllToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (CP_Save.Visible)
		{
			CP_Save.PerformClick();
		}
	}

	private void ReturnToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (CP_Refresh.Visible)
		{
			CP_Refresh.PerformClick();
		}
	}

	private void EditCardToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (CP_Edit.Visible)
		{
			CP_Edit.PerformClick();
		}
	}

	private void RenameToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (CP_Rename.Visible)
		{
			CP_Rename.PerformClick();
		}
	}

	private void MoveToTopToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (CP_Sort.Visible)
		{
			CP_Sort.PerformClick();
		}
	}

	private void MoveUpToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (CP_ShiftUp.Visible)
		{
			CP_ShiftUp.PerformClick();
		}
	}

	private void MoveDownToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (CP_ShiftDown.Visible)
		{
			CP_ShiftDown.PerformClick();
		}
	}

	private void MoveToBottomToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (CP_Unlist.Visible)
		{
			CP_Unlist.PerformClick();
		}
	}

	private void IncPriorityToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (CP_PriorityUp.Visible)
		{
			CP_PriorityUp.PerformClick();
		}
	}

	private void DecPriorityToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (CP_PriorityDown.Visible)
		{
			CP_PriorityDown.PerformClick();
		}
	}

	private void ViewToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (CP_View.Visible)
		{
			CP_View.PerformClick();
		}
	}

	private void OpenManualToolStripMenuItem_Click(object sender, EventArgs e)
	{
		Process.Start("https://docs.google.com/document/d/1FrfLtZlpJ1_1yBQv6O9hQXwFCtXSi-wQamLU_523dt4/edit?usp=drive_link");
	}
}
