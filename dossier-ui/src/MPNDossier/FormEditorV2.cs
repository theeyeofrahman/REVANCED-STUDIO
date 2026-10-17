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
using MPNDossier.My;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MPNDossier;

[DesignerGenerated]
public partial class FormEditorV2 : Form
{
	private IContainer components;

	[AccessedThroughProperty("EditorStrip")]
	private MenuStrip _EditorStrip;

	[AccessedThroughProperty("FileToolStripMenuItem")]
	private ToolStripMenuItem _FileToolStripMenuItem;

	[AccessedThroughProperty("NewToolStripMenuItem")]
	private ToolStripMenuItem _NewToolStripMenuItem;

	[AccessedThroughProperty("AnimationSetToolStripMenuItem")]
	private ToolStripMenuItem _AnimationSetToolStripMenuItem;

	[AccessedThroughProperty("CharacterToolStripMenuItem")]
	private ToolStripMenuItem _CharacterToolStripMenuItem;

	[AccessedThroughProperty("ChatterToolStripMenuItem")]
	private ToolStripMenuItem _ChatterToolStripMenuItem;

	[AccessedThroughProperty("CorpusToolStripMenuItem")]
	private ToolStripMenuItem _CorpusToolStripMenuItem;

	[AccessedThroughProperty("LoadoutToolStripMenuItem")]
	private ToolStripMenuItem _LoadoutToolStripMenuItem;

	[AccessedThroughProperty("OriginToolStripMenuItem")]
	private ToolStripMenuItem _OriginToolStripMenuItem;

	[AccessedThroughProperty("SkinToolStripMenuItem")]
	private ToolStripMenuItem _SkinToolStripMenuItem;

	[AccessedThroughProperty("SkillGroupToolStripMenuItem")]
	private ToolStripMenuItem _SkillGroupToolStripMenuItem;

	[AccessedThroughProperty("SwatchToolStripMenuItem")]
	private ToolStripMenuItem _SwatchToolStripMenuItem;

	[AccessedThroughProperty("WardrobeToolStripMenuItem")]
	private ToolStripMenuItem _WardrobeToolStripMenuItem;

	[AccessedThroughProperty("SaveToolStripMenuItem")]
	private ToolStripMenuItem _SaveToolStripMenuItem;

	[AccessedThroughProperty("LoadToolStripMenuItem")]
	private ToolStripMenuItem _LoadToolStripMenuItem;

	[AccessedThroughProperty("ListObjects")]
	private ListBox _ListObjects;

	[AccessedThroughProperty("Param")]
	private TextBox _Param;

	[AccessedThroughProperty("ActionButton")]
	private Button _ActionButton;

	[AccessedThroughProperty("ColorWheel")]
	private ColorDialog _ColorWheel;

	[AccessedThroughProperty("ObjEditor")]
	private TreeView _ObjEditor;

	[AccessedThroughProperty("AnimationToolStripMenuItem")]
	private ToolStripMenuItem _AnimationToolStripMenuItem;

	[AccessedThroughProperty("ExecutionToolStripMenuItem")]
	private ToolStripMenuItem _ExecutionToolStripMenuItem;

	[AccessedThroughProperty("StageToolStripMenuItem")]
	private ToolStripMenuItem _StageToolStripMenuItem;

	[AccessedThroughProperty("StoryToolStripMenuItem")]
	private ToolStripMenuItem _StoryToolStripMenuItem;

	[AccessedThroughProperty("ArenaToolStripMenuItem")]
	private ToolStripMenuItem _ArenaToolStripMenuItem;

	[AccessedThroughProperty("ViewToolStripMenuItem")]
	private ToolStripMenuItem _ViewToolStripMenuItem;

	[AccessedThroughProperty("ColorWheelToolStripMenuItem")]
	private ToolStripMenuItem _ColorWheelToolStripMenuItem;

	[AccessedThroughProperty("ExpandCurrentToolStripMenuItem")]
	private ToolStripMenuItem _ExpandCurrentToolStripMenuItem;

	[AccessedThroughProperty("CollapseCurrentToolStripMenuItem")]
	private ToolStripMenuItem _CollapseCurrentToolStripMenuItem;

	[AccessedThroughProperty("ExpandAllToolStripMenuItem")]
	private ToolStripMenuItem _ExpandAllToolStripMenuItem;

	[AccessedThroughProperty("CollapseAllToolStripMenuItem")]
	private ToolStripMenuItem _CollapseAllToolStripMenuItem;

	[AccessedThroughProperty("ItemToolStripMenuItem")]
	private ToolStripMenuItem _ItemToolStripMenuItem;

	[AccessedThroughProperty("ArmorToolStripMenuItem")]
	private ToolStripMenuItem _ArmorToolStripMenuItem;

	[AccessedThroughProperty("ToolStripSeparator1")]
	private ToolStripSeparator _ToolStripSeparator1;

	[AccessedThroughProperty("WeaponToolStripMenuItem")]
	private ToolStripMenuItem _WeaponToolStripMenuItem;

	[AccessedThroughProperty("ModdedToolStripMenuItem")]
	private ToolStripMenuItem _ModdedToolStripMenuItem;

	[AccessedThroughProperty("WeaponAddonToolStripMenuItem")]
	private ToolStripMenuItem _WeaponAddonToolStripMenuItem;

	[AccessedThroughProperty("MunitionToolStripMenuItem")]
	private ToolStripMenuItem _MunitionToolStripMenuItem;

	[AccessedThroughProperty("ThrownToolStripMenuItem")]
	private ToolStripMenuItem _ThrownToolStripMenuItem;

	[AccessedThroughProperty("RangedToolStripMenuItem")]
	private ToolStripMenuItem _RangedToolStripMenuItem;

	[AccessedThroughProperty("ExplosionToolStripMenuItem")]
	private ToolStripMenuItem _ExplosionToolStripMenuItem;

	[AccessedThroughProperty("DoTEffectToolStripMenuItem")]
	private ToolStripMenuItem _DoTEffectToolStripMenuItem;

	[AccessedThroughProperty("ToolStripSeparator2")]
	private ToolStripSeparator _ToolStripSeparator2;

	[AccessedThroughProperty("AddonGroupToolStripMenuItem")]
	private ToolStripMenuItem _AddonGroupToolStripMenuItem;

	[AccessedThroughProperty("DuplicateObjectToolStripMenuItem")]
	private ToolStripMenuItem _DuplicateObjectToolStripMenuItem;

	[AccessedThroughProperty("BatchUpdateToolStripMenuItem")]
	private ToolStripMenuItem _BatchUpdateToolStripMenuItem;

	[AccessedThroughProperty("FileName")]
	private TextBox _FileName;

	[AccessedThroughProperty("Price")]
	private Label _Price;

	[AccessedThroughProperty("LoadingProgress")]
	private ProgressBar _LoadingProgress;

	[AccessedThroughProperty("TransferNewToolStripMenuItem")]
	private ToolStripMenuItem _TransferNewToolStripMenuItem;

	[AccessedThroughProperty("ToolStripSeparator3")]
	private ToolStripSeparator _ToolStripSeparator3;

	[AccessedThroughProperty("NewTransferToolStripMenuItem")]
	private ToolStripMenuItem _NewTransferToolStripMenuItem;

	[AccessedThroughProperty("NormalToolStripMenuItem")]
	private ToolStripMenuItem _NormalToolStripMenuItem;

	[AccessedThroughProperty("StoryToolStripMenuItem1")]
	private ToolStripMenuItem _StoryToolStripMenuItem1;

	private string SaveFolder;

	private ModCards.PropertyList SavedList;

	private bool SaveAsWeapon;

	private ModCards.PropertyControl[] ControlScheme;

	private Dictionary<string, JObject> LoadableEntries;

	private ModCards.NodeTag SelectedTag;

	private bool Loading;

	private bool NotifyAddedProperties;

	private string BatchDiagnose;

	private Dictionary<string, TreeNode> Diagnostics;

	private Dictionary<string, TreeNode> RepairLine;

	private Dictionary<string, string> RepairDirectories;

	internal virtual MenuStrip EditorStrip
	{
		get
		{
			return _EditorStrip;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			_EditorStrip = value;
		}
	}

	internal virtual ToolStripMenuItem FileToolStripMenuItem
	{
		get
		{
			return _FileToolStripMenuItem;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			_FileToolStripMenuItem = value;
		}
	}

	internal virtual ToolStripMenuItem NewToolStripMenuItem
	{
		get
		{
			return _NewToolStripMenuItem;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			_NewToolStripMenuItem = value;
		}
	}

	internal virtual ToolStripMenuItem AnimationSetToolStripMenuItem
	{
		get
		{
			return _AnimationSetToolStripMenuItem;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			EventHandler value2 = AnimationSetToolStripMenuItem_Click;
			if (_AnimationSetToolStripMenuItem != null)
			{
				_AnimationSetToolStripMenuItem.Click -= value2;
			}
			_AnimationSetToolStripMenuItem = value;
			if (_AnimationSetToolStripMenuItem != null)
			{
				_AnimationSetToolStripMenuItem.Click += value2;
			}
		}
	}

	internal virtual ToolStripMenuItem CharacterToolStripMenuItem
	{
		get
		{
			return _CharacterToolStripMenuItem;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			EventHandler value2 = CharacterToolStripMenuItem_Click;
			if (_CharacterToolStripMenuItem != null)
			{
				_CharacterToolStripMenuItem.Click -= value2;
			}
			_CharacterToolStripMenuItem = value;
			if (_CharacterToolStripMenuItem != null)
			{
				_CharacterToolStripMenuItem.Click += value2;
			}
		}
	}

	internal virtual ToolStripMenuItem ChatterToolStripMenuItem
	{
		get
		{
			return _ChatterToolStripMenuItem;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			EventHandler value2 = ChatterToolStripMenuItem_Click;
			if (_ChatterToolStripMenuItem != null)
			{
				_ChatterToolStripMenuItem.Click -= value2;
			}
			_ChatterToolStripMenuItem = value;
			if (_ChatterToolStripMenuItem != null)
			{
				_ChatterToolStripMenuItem.Click += value2;
			}
		}
	}

	internal virtual ToolStripMenuItem CorpusToolStripMenuItem
	{
		get
		{
			return _CorpusToolStripMenuItem;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			EventHandler value2 = CorpusToolStripMenuItem_Click;
			if (_CorpusToolStripMenuItem != null)
			{
				_CorpusToolStripMenuItem.Click -= value2;
			}
			_CorpusToolStripMenuItem = value;
			if (_CorpusToolStripMenuItem != null)
			{
				_CorpusToolStripMenuItem.Click += value2;
			}
		}
	}

	internal virtual ToolStripMenuItem LoadoutToolStripMenuItem
	{
		get
		{
			return _LoadoutToolStripMenuItem;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			_LoadoutToolStripMenuItem = value;
		}
	}

	internal virtual ToolStripMenuItem OriginToolStripMenuItem
	{
		get
		{
			return _OriginToolStripMenuItem;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			EventHandler value2 = OriginToolStripMenuItem_Click;
			if (_OriginToolStripMenuItem != null)
			{
				_OriginToolStripMenuItem.Click -= value2;
			}
			_OriginToolStripMenuItem = value;
			if (_OriginToolStripMenuItem != null)
			{
				_OriginToolStripMenuItem.Click += value2;
			}
		}
	}

	internal virtual ToolStripMenuItem SkinToolStripMenuItem
	{
		get
		{
			return _SkinToolStripMenuItem;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			EventHandler value2 = SkinToolStripMenuItem_Click;
			if (_SkinToolStripMenuItem != null)
			{
				_SkinToolStripMenuItem.Click -= value2;
			}
			_SkinToolStripMenuItem = value;
			if (_SkinToolStripMenuItem != null)
			{
				_SkinToolStripMenuItem.Click += value2;
			}
		}
	}

	internal virtual ToolStripMenuItem SkillGroupToolStripMenuItem
	{
		get
		{
			return _SkillGroupToolStripMenuItem;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			EventHandler value2 = SkillGroupToolStripMenuItem_Click;
			if (_SkillGroupToolStripMenuItem != null)
			{
				_SkillGroupToolStripMenuItem.Click -= value2;
			}
			_SkillGroupToolStripMenuItem = value;
			if (_SkillGroupToolStripMenuItem != null)
			{
				_SkillGroupToolStripMenuItem.Click += value2;
			}
		}
	}

	internal virtual ToolStripMenuItem SwatchToolStripMenuItem
	{
		get
		{
			return _SwatchToolStripMenuItem;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			EventHandler value2 = SwatchToolStripMenuItem_Click;
			if (_SwatchToolStripMenuItem != null)
			{
				_SwatchToolStripMenuItem.Click -= value2;
			}
			_SwatchToolStripMenuItem = value;
			if (_SwatchToolStripMenuItem != null)
			{
				_SwatchToolStripMenuItem.Click += value2;
			}
		}
	}

	internal virtual ToolStripMenuItem WardrobeToolStripMenuItem
	{
		get
		{
			return _WardrobeToolStripMenuItem;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			EventHandler value2 = WardrobeToolStripMenuItem_Click;
			if (_WardrobeToolStripMenuItem != null)
			{
				_WardrobeToolStripMenuItem.Click -= value2;
			}
			_WardrobeToolStripMenuItem = value;
			if (_WardrobeToolStripMenuItem != null)
			{
				_WardrobeToolStripMenuItem.Click += value2;
			}
		}
	}

	internal virtual ToolStripMenuItem SaveToolStripMenuItem
	{
		get
		{
			return _SaveToolStripMenuItem;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			EventHandler value2 = SaveToolStripMenuItem_Click;
			if (_SaveToolStripMenuItem != null)
			{
				_SaveToolStripMenuItem.Click -= value2;
			}
			_SaveToolStripMenuItem = value;
			if (_SaveToolStripMenuItem != null)
			{
				_SaveToolStripMenuItem.Click += value2;
			}
		}
	}

	internal virtual ToolStripMenuItem LoadToolStripMenuItem
	{
		get
		{
			return _LoadToolStripMenuItem;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			EventHandler value2 = LoadToolStripMenuItem_Click;
			if (_LoadToolStripMenuItem != null)
			{
				_LoadToolStripMenuItem.Click -= value2;
			}
			_LoadToolStripMenuItem = value;
			if (_LoadToolStripMenuItem != null)
			{
				_LoadToolStripMenuItem.Click += value2;
			}
		}
	}

	internal virtual ListBox ListObjects
	{
		get
		{
			return _ListObjects;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			EventHandler value2 = ListObjects_SelectedIndexChanged;
			EventHandler value3 = ObjList_MouseEnter;
			if (_ListObjects != null)
			{
				_ListObjects.SelectedIndexChanged -= value2;
				_ListObjects.MouseEnter -= value3;
			}
			_ListObjects = value;
			if (_ListObjects != null)
			{
				_ListObjects.SelectedIndexChanged += value2;
				_ListObjects.MouseEnter += value3;
			}
		}
	}

	internal virtual TextBox Param
	{
		get
		{
			return _Param;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			EventHandler value2 = Param_TextChanged;
			if (_Param != null)
			{
				_Param.TextChanged -= value2;
			}
			_Param = value;
			if (_Param != null)
			{
				_Param.TextChanged += value2;
			}
		}
	}

	internal virtual Button ActionButton
	{
		get
		{
			return _ActionButton;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			EventHandler value2 = ActionButton_Click;
			if (_ActionButton != null)
			{
				_ActionButton.Click -= value2;
			}
			_ActionButton = value;
			if (_ActionButton != null)
			{
				_ActionButton.Click += value2;
			}
		}
	}

	internal virtual ColorDialog ColorWheel
	{
		get
		{
			return _ColorWheel;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			_ColorWheel = value;
		}
	}

	internal virtual TreeView ObjEditor
	{
		get
		{
			return _ObjEditor;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			TreeViewEventHandler value2 = ObjEditor_AfterSelect;
			if (_ObjEditor != null)
			{
				_ObjEditor.AfterSelect -= value2;
			}
			_ObjEditor = value;
			if (_ObjEditor != null)
			{
				_ObjEditor.AfterSelect += value2;
			}
		}
	}

	internal virtual ToolStripMenuItem AnimationToolStripMenuItem
	{
		get
		{
			return _AnimationToolStripMenuItem;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			EventHandler value2 = AnimationToolStripMenuItem_Click;
			if (_AnimationToolStripMenuItem != null)
			{
				_AnimationToolStripMenuItem.Click -= value2;
			}
			_AnimationToolStripMenuItem = value;
			if (_AnimationToolStripMenuItem != null)
			{
				_AnimationToolStripMenuItem.Click += value2;
			}
		}
	}

	internal virtual ToolStripMenuItem ExecutionToolStripMenuItem
	{
		get
		{
			return _ExecutionToolStripMenuItem;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			EventHandler value2 = ExecutionToolStripMenuItem_Click;
			if (_ExecutionToolStripMenuItem != null)
			{
				_ExecutionToolStripMenuItem.Click -= value2;
			}
			_ExecutionToolStripMenuItem = value;
			if (_ExecutionToolStripMenuItem != null)
			{
				_ExecutionToolStripMenuItem.Click += value2;
			}
		}
	}

	internal virtual ToolStripMenuItem StageToolStripMenuItem
	{
		get
		{
			return _StageToolStripMenuItem;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			_StageToolStripMenuItem = value;
		}
	}

	internal virtual ToolStripMenuItem StoryToolStripMenuItem
	{
		get
		{
			return _StoryToolStripMenuItem;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			EventHandler value2 = StoryToolStripMenuItem_Click;
			if (_StoryToolStripMenuItem != null)
			{
				_StoryToolStripMenuItem.Click -= value2;
			}
			_StoryToolStripMenuItem = value;
			if (_StoryToolStripMenuItem != null)
			{
				_StoryToolStripMenuItem.Click += value2;
			}
		}
	}

	internal virtual ToolStripMenuItem ArenaToolStripMenuItem
	{
		get
		{
			return _ArenaToolStripMenuItem;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			EventHandler value2 = ArenaToolStripMenuItem_Click;
			if (_ArenaToolStripMenuItem != null)
			{
				_ArenaToolStripMenuItem.Click -= value2;
			}
			_ArenaToolStripMenuItem = value;
			if (_ArenaToolStripMenuItem != null)
			{
				_ArenaToolStripMenuItem.Click += value2;
			}
		}
	}

	internal virtual ToolStripMenuItem ViewToolStripMenuItem
	{
		get
		{
			return _ViewToolStripMenuItem;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			_ViewToolStripMenuItem = value;
		}
	}

	internal virtual ToolStripMenuItem ColorWheelToolStripMenuItem
	{
		get
		{
			return _ColorWheelToolStripMenuItem;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			EventHandler value2 = ColorWheelToolStripMenuItem_Click;
			if (_ColorWheelToolStripMenuItem != null)
			{
				_ColorWheelToolStripMenuItem.Click -= value2;
			}
			_ColorWheelToolStripMenuItem = value;
			if (_ColorWheelToolStripMenuItem != null)
			{
				_ColorWheelToolStripMenuItem.Click += value2;
			}
		}
	}

	internal virtual ToolStripMenuItem ExpandCurrentToolStripMenuItem
	{
		get
		{
			return _ExpandCurrentToolStripMenuItem;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			EventHandler value2 = ExpandCurrrentToolStripMenuItem_Click;
			if (_ExpandCurrentToolStripMenuItem != null)
			{
				_ExpandCurrentToolStripMenuItem.Click -= value2;
			}
			_ExpandCurrentToolStripMenuItem = value;
			if (_ExpandCurrentToolStripMenuItem != null)
			{
				_ExpandCurrentToolStripMenuItem.Click += value2;
			}
		}
	}

	internal virtual ToolStripMenuItem CollapseCurrentToolStripMenuItem
	{
		get
		{
			return _CollapseCurrentToolStripMenuItem;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			EventHandler value2 = CollapseCurrentToolStripMenuItem_Click;
			if (_CollapseCurrentToolStripMenuItem != null)
			{
				_CollapseCurrentToolStripMenuItem.Click -= value2;
			}
			_CollapseCurrentToolStripMenuItem = value;
			if (_CollapseCurrentToolStripMenuItem != null)
			{
				_CollapseCurrentToolStripMenuItem.Click += value2;
			}
		}
	}

	internal virtual ToolStripMenuItem ExpandAllToolStripMenuItem
	{
		get
		{
			return _ExpandAllToolStripMenuItem;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			EventHandler value2 = ExpandAllToolStripMenuItem_Click;
			if (_ExpandAllToolStripMenuItem != null)
			{
				_ExpandAllToolStripMenuItem.Click -= value2;
			}
			_ExpandAllToolStripMenuItem = value;
			if (_ExpandAllToolStripMenuItem != null)
			{
				_ExpandAllToolStripMenuItem.Click += value2;
			}
		}
	}

	internal virtual ToolStripMenuItem CollapseAllToolStripMenuItem
	{
		get
		{
			return _CollapseAllToolStripMenuItem;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			EventHandler value2 = CollapseAllToolStripMenuItem_Click;
			if (_CollapseAllToolStripMenuItem != null)
			{
				_CollapseAllToolStripMenuItem.Click -= value2;
			}
			_CollapseAllToolStripMenuItem = value;
			if (_CollapseAllToolStripMenuItem != null)
			{
				_CollapseAllToolStripMenuItem.Click += value2;
			}
		}
	}

	internal virtual ToolStripMenuItem ItemToolStripMenuItem
	{
		get
		{
			return _ItemToolStripMenuItem;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			_ItemToolStripMenuItem = value;
		}
	}

	internal virtual ToolStripMenuItem ArmorToolStripMenuItem
	{
		get
		{
			return _ArmorToolStripMenuItem;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			EventHandler value2 = ArmorToolStripMenuItem_Click;
			if (_ArmorToolStripMenuItem != null)
			{
				_ArmorToolStripMenuItem.Click -= value2;
			}
			_ArmorToolStripMenuItem = value;
			if (_ArmorToolStripMenuItem != null)
			{
				_ArmorToolStripMenuItem.Click += value2;
			}
		}
	}

	internal virtual ToolStripSeparator ToolStripSeparator1
	{
		get
		{
			return _ToolStripSeparator1;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			_ToolStripSeparator1 = value;
		}
	}

	internal virtual ToolStripMenuItem WeaponToolStripMenuItem
	{
		get
		{
			return _WeaponToolStripMenuItem;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			EventHandler value2 = WeaponToolStripMenuItem_Click;
			if (_WeaponToolStripMenuItem != null)
			{
				_WeaponToolStripMenuItem.Click -= value2;
			}
			_WeaponToolStripMenuItem = value;
			if (_WeaponToolStripMenuItem != null)
			{
				_WeaponToolStripMenuItem.Click += value2;
			}
		}
	}

	internal virtual ToolStripMenuItem ModdedToolStripMenuItem
	{
		get
		{
			return _ModdedToolStripMenuItem;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			EventHandler value2 = ModdedWeaponToolStripMenuItem_Click;
			if (_ModdedToolStripMenuItem != null)
			{
				_ModdedToolStripMenuItem.Click -= value2;
			}
			_ModdedToolStripMenuItem = value;
			if (_ModdedToolStripMenuItem != null)
			{
				_ModdedToolStripMenuItem.Click += value2;
			}
		}
	}

	internal virtual ToolStripMenuItem WeaponAddonToolStripMenuItem
	{
		get
		{
			return _WeaponAddonToolStripMenuItem;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			EventHandler value2 = WeaponAddonToolStripMenuItem_Click;
			if (_WeaponAddonToolStripMenuItem != null)
			{
				_WeaponAddonToolStripMenuItem.Click -= value2;
			}
			_WeaponAddonToolStripMenuItem = value;
			if (_WeaponAddonToolStripMenuItem != null)
			{
				_WeaponAddonToolStripMenuItem.Click += value2;
			}
		}
	}

	internal virtual ToolStripMenuItem MunitionToolStripMenuItem
	{
		get
		{
			return _MunitionToolStripMenuItem;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			EventHandler value2 = MunitionToolStripMenuItem_Click;
			if (_MunitionToolStripMenuItem != null)
			{
				_MunitionToolStripMenuItem.Click -= value2;
			}
			_MunitionToolStripMenuItem = value;
			if (_MunitionToolStripMenuItem != null)
			{
				_MunitionToolStripMenuItem.Click += value2;
			}
		}
	}

	internal virtual ToolStripMenuItem ThrownToolStripMenuItem
	{
		get
		{
			return _ThrownToolStripMenuItem;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			EventHandler value2 = ThrownToolStripMenuItem_Click;
			if (_ThrownToolStripMenuItem != null)
			{
				_ThrownToolStripMenuItem.Click -= value2;
			}
			_ThrownToolStripMenuItem = value;
			if (_ThrownToolStripMenuItem != null)
			{
				_ThrownToolStripMenuItem.Click += value2;
			}
		}
	}

	internal virtual ToolStripMenuItem RangedToolStripMenuItem
	{
		get
		{
			return _RangedToolStripMenuItem;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			EventHandler value2 = RangedToolStripMenuItem_Click;
			if (_RangedToolStripMenuItem != null)
			{
				_RangedToolStripMenuItem.Click -= value2;
			}
			_RangedToolStripMenuItem = value;
			if (_RangedToolStripMenuItem != null)
			{
				_RangedToolStripMenuItem.Click += value2;
			}
		}
	}

	internal virtual ToolStripMenuItem ExplosionToolStripMenuItem
	{
		get
		{
			return _ExplosionToolStripMenuItem;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			EventHandler value2 = ExplosionToolStripMenuItem_Click;
			if (_ExplosionToolStripMenuItem != null)
			{
				_ExplosionToolStripMenuItem.Click -= value2;
			}
			_ExplosionToolStripMenuItem = value;
			if (_ExplosionToolStripMenuItem != null)
			{
				_ExplosionToolStripMenuItem.Click += value2;
			}
		}
	}

	internal virtual ToolStripMenuItem DoTEffectToolStripMenuItem
	{
		get
		{
			return _DoTEffectToolStripMenuItem;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			EventHandler value2 = DoTEffectToolStripMenuItem_Click;
			if (_DoTEffectToolStripMenuItem != null)
			{
				_DoTEffectToolStripMenuItem.Click -= value2;
			}
			_DoTEffectToolStripMenuItem = value;
			if (_DoTEffectToolStripMenuItem != null)
			{
				_DoTEffectToolStripMenuItem.Click += value2;
			}
		}
	}

	internal virtual ToolStripSeparator ToolStripSeparator2
	{
		get
		{
			return _ToolStripSeparator2;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			_ToolStripSeparator2 = value;
		}
	}

	internal virtual ToolStripMenuItem AddonGroupToolStripMenuItem
	{
		get
		{
			return _AddonGroupToolStripMenuItem;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			EventHandler value2 = AddonGroupToolStripMenuItem_Click;
			if (_AddonGroupToolStripMenuItem != null)
			{
				_AddonGroupToolStripMenuItem.Click -= value2;
			}
			_AddonGroupToolStripMenuItem = value;
			if (_AddonGroupToolStripMenuItem != null)
			{
				_AddonGroupToolStripMenuItem.Click += value2;
			}
		}
	}

	internal virtual ToolStripMenuItem DuplicateObjectToolStripMenuItem
	{
		get
		{
			return _DuplicateObjectToolStripMenuItem;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			EventHandler value2 = DuplicateObjectToolStripMenuItem_Click;
			if (_DuplicateObjectToolStripMenuItem != null)
			{
				_DuplicateObjectToolStripMenuItem.Click -= value2;
			}
			_DuplicateObjectToolStripMenuItem = value;
			if (_DuplicateObjectToolStripMenuItem != null)
			{
				_DuplicateObjectToolStripMenuItem.Click += value2;
			}
		}
	}

	internal virtual ToolStripMenuItem BatchUpdateToolStripMenuItem
	{
		get
		{
			return _BatchUpdateToolStripMenuItem;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			EventHandler value2 = BatchDiagnoseToolStripMenuItem_Click;
			if (_BatchUpdateToolStripMenuItem != null)
			{
				_BatchUpdateToolStripMenuItem.Click -= value2;
			}
			_BatchUpdateToolStripMenuItem = value;
			if (_BatchUpdateToolStripMenuItem != null)
			{
				_BatchUpdateToolStripMenuItem.Click += value2;
			}
		}
	}

	internal virtual TextBox FileName
	{
		get
		{
			return _FileName;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			_FileName = value;
		}
	}

	internal virtual Label Price
	{
		get
		{
			return _Price;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			_Price = value;
		}
	}

	internal virtual ProgressBar LoadingProgress
	{
		get
		{
			return _LoadingProgress;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			_LoadingProgress = value;
		}
	}

	internal virtual ToolStripMenuItem TransferNewToolStripMenuItem
	{
		get
		{
			return _TransferNewToolStripMenuItem;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			EventHandler value2 = TransferNewToolStripMenuItem_Click;
			if (_TransferNewToolStripMenuItem != null)
			{
				_TransferNewToolStripMenuItem.Click -= value2;
			}
			_TransferNewToolStripMenuItem = value;
			if (_TransferNewToolStripMenuItem != null)
			{
				_TransferNewToolStripMenuItem.Click += value2;
			}
		}
	}

	internal virtual ToolStripSeparator ToolStripSeparator3
	{
		get
		{
			return _ToolStripSeparator3;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			_ToolStripSeparator3 = value;
		}
	}

	internal virtual ToolStripMenuItem NewTransferToolStripMenuItem
	{
		get
		{
			return _NewTransferToolStripMenuItem;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			EventHandler value2 = NewTransferToolStripMenuItem_Click;
			if (_NewTransferToolStripMenuItem != null)
			{
				_NewTransferToolStripMenuItem.Click -= value2;
			}
			_NewTransferToolStripMenuItem = value;
			if (_NewTransferToolStripMenuItem != null)
			{
				_NewTransferToolStripMenuItem.Click += value2;
			}
		}
	}

	internal virtual ToolStripMenuItem NormalToolStripMenuItem
	{
		get
		{
			return _NormalToolStripMenuItem;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			EventHandler value2 = NormalToolStripMenuItem_Click;
			if (_NormalToolStripMenuItem != null)
			{
				_NormalToolStripMenuItem.Click -= value2;
			}
			_NormalToolStripMenuItem = value;
			if (_NormalToolStripMenuItem != null)
			{
				_NormalToolStripMenuItem.Click += value2;
			}
		}
	}

	internal virtual ToolStripMenuItem StoryToolStripMenuItem1
	{
		get
		{
			return _StoryToolStripMenuItem1;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			EventHandler value2 = StoryToolStripMenuItem1_Click;
			if (_StoryToolStripMenuItem1 != null)
			{
				_StoryToolStripMenuItem1.Click -= value2;
			}
			_StoryToolStripMenuItem1 = value;
			if (_StoryToolStripMenuItem1 != null)
			{
				_StoryToolStripMenuItem1.Click += value2;
			}
		}
	}

	public FormEditorV2()
	{
		Load += FormEditorV2_Load;
		SaveFolder = "";
		SaveAsWeapon = false;
		Loading = false;
		NotifyAddedProperties = false;
		BatchDiagnose = "";
		Diagnostics = new Dictionary<string, TreeNode>();
		RepairLine = new Dictionary<string, TreeNode>();
		RepairDirectories = new Dictionary<string, string>();
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
		this.EditorStrip = new System.Windows.Forms.MenuStrip();
		this.FileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.NewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.AnimationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.AnimationSetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.CharacterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.ChatterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.CorpusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.DoTEffectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.ExecutionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.ExplosionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.ItemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.ArmorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.ToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
		this.WeaponToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.ModdedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.MunitionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.ToolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
		this.WeaponAddonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.ThrownToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.RangedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.LoadoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.OriginToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.SkinToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.SkillGroupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.AddonGroupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.StageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.StoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.ArenaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.SwatchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.WardrobeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.SaveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.LoadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.BatchUpdateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.ToolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
		this.TransferNewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.NewTransferToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.ViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.ColorWheelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.ExpandCurrentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.CollapseCurrentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.ExpandAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.CollapseAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.DuplicateObjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.ListObjects = new System.Windows.Forms.ListBox();
		this.Param = new System.Windows.Forms.TextBox();
		this.ActionButton = new DossierButton();
		this.ColorWheel = new System.Windows.Forms.ColorDialog();
		this.ObjEditor = new System.Windows.Forms.TreeView();
		this.FileName = new System.Windows.Forms.TextBox();
		this.Price = new System.Windows.Forms.Label();
		this.LoadingProgress = new System.Windows.Forms.ProgressBar();
		this.NormalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.StoryToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
		this.EditorStrip.SuspendLayout();
		this.SuspendLayout();
		this.EditorStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[2] { this.FileToolStripMenuItem, this.ViewToolStripMenuItem });
		System.Windows.Forms.MenuStrip editorStrip = this.EditorStrip;
		System.Drawing.Point location = new System.Drawing.Point(0, 0);
		editorStrip.Location = location;
		this.EditorStrip.Name = "EditorStrip";
		System.Windows.Forms.MenuStrip editorStrip2 = this.EditorStrip;
		System.Drawing.Size size = new System.Drawing.Size(784, 24);
		editorStrip2.Size = size;
		this.EditorStrip.TabIndex = 20;
		this.EditorStrip.Text = "MenuStrip1";
		this.FileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[7] { this.NewToolStripMenuItem, this.SaveToolStripMenuItem, this.LoadToolStripMenuItem, this.BatchUpdateToolStripMenuItem, this.ToolStripSeparator3, this.TransferNewToolStripMenuItem, this.NewTransferToolStripMenuItem });
		this.FileToolStripMenuItem.Name = "FileToolStripMenuItem";
		System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem = this.FileToolStripMenuItem;
		size = new System.Drawing.Size(37, 20);
		fileToolStripMenuItem.Size = size;
		this.FileToolStripMenuItem.Text = "File";
		this.NewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[17]
		{
			this.AnimationToolStripMenuItem, this.AnimationSetToolStripMenuItem, this.CharacterToolStripMenuItem, this.ChatterToolStripMenuItem, this.CorpusToolStripMenuItem, this.DoTEffectToolStripMenuItem, this.ExecutionToolStripMenuItem, this.ExplosionToolStripMenuItem, this.ItemToolStripMenuItem, this.LoadoutToolStripMenuItem,
			this.OriginToolStripMenuItem, this.SkinToolStripMenuItem, this.SkillGroupToolStripMenuItem, this.AddonGroupToolStripMenuItem, this.StageToolStripMenuItem, this.SwatchToolStripMenuItem, this.WardrobeToolStripMenuItem
		});
		this.NewToolStripMenuItem.Name = "NewToolStripMenuItem";
		System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem = this.NewToolStripMenuItem;
		size = new System.Drawing.Size(152, 22);
		newToolStripMenuItem.Size = size;
		this.NewToolStripMenuItem.Text = "New";
		this.AnimationToolStripMenuItem.Name = "AnimationToolStripMenuItem";
		System.Windows.Forms.ToolStripMenuItem animationToolStripMenuItem = this.AnimationToolStripMenuItem;
		size = new System.Drawing.Size(152, 22);
		animationToolStripMenuItem.Size = size;
		this.AnimationToolStripMenuItem.Text = "Animation";
		this.AnimationSetToolStripMenuItem.Name = "AnimationSetToolStripMenuItem";
		System.Windows.Forms.ToolStripMenuItem animationSetToolStripMenuItem = this.AnimationSetToolStripMenuItem;
		size = new System.Drawing.Size(152, 22);
		animationSetToolStripMenuItem.Size = size;
		this.AnimationSetToolStripMenuItem.Text = "Animation Set";
		this.CharacterToolStripMenuItem.Name = "CharacterToolStripMenuItem";
		System.Windows.Forms.ToolStripMenuItem characterToolStripMenuItem = this.CharacterToolStripMenuItem;
		size = new System.Drawing.Size(152, 22);
		characterToolStripMenuItem.Size = size;
		this.CharacterToolStripMenuItem.Text = "Character";
		this.ChatterToolStripMenuItem.Name = "ChatterToolStripMenuItem";
		System.Windows.Forms.ToolStripMenuItem chatterToolStripMenuItem = this.ChatterToolStripMenuItem;
		size = new System.Drawing.Size(152, 22);
		chatterToolStripMenuItem.Size = size;
		this.ChatterToolStripMenuItem.Text = "Chatter";
		this.CorpusToolStripMenuItem.Name = "CorpusToolStripMenuItem";
		System.Windows.Forms.ToolStripMenuItem corpusToolStripMenuItem = this.CorpusToolStripMenuItem;
		size = new System.Drawing.Size(152, 22);
		corpusToolStripMenuItem.Size = size;
		this.CorpusToolStripMenuItem.Text = "Corpus";
		this.DoTEffectToolStripMenuItem.Name = "DoTEffectToolStripMenuItem";
		System.Windows.Forms.ToolStripMenuItem doTEffectToolStripMenuItem = this.DoTEffectToolStripMenuItem;
		size = new System.Drawing.Size(152, 22);
		doTEffectToolStripMenuItem.Size = size;
		this.DoTEffectToolStripMenuItem.Text = "DoT Effect";
		this.ExecutionToolStripMenuItem.Name = "ExecutionToolStripMenuItem";
		System.Windows.Forms.ToolStripMenuItem executionToolStripMenuItem = this.ExecutionToolStripMenuItem;
		size = new System.Drawing.Size(152, 22);
		executionToolStripMenuItem.Size = size;
		this.ExecutionToolStripMenuItem.Text = "Execution";
		this.ExplosionToolStripMenuItem.Name = "ExplosionToolStripMenuItem";
		System.Windows.Forms.ToolStripMenuItem explosionToolStripMenuItem = this.ExplosionToolStripMenuItem;
		size = new System.Drawing.Size(152, 22);
		explosionToolStripMenuItem.Size = size;
		this.ExplosionToolStripMenuItem.Text = "Explosion";
		this.ItemToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[9] { this.ArmorToolStripMenuItem, this.ToolStripSeparator1, this.WeaponToolStripMenuItem, this.ModdedToolStripMenuItem, this.MunitionToolStripMenuItem, this.ToolStripSeparator2, this.WeaponAddonToolStripMenuItem, this.ThrownToolStripMenuItem, this.RangedToolStripMenuItem });
		this.ItemToolStripMenuItem.Name = "ItemToolStripMenuItem";
		System.Windows.Forms.ToolStripMenuItem itemToolStripMenuItem = this.ItemToolStripMenuItem;
		size = new System.Drawing.Size(152, 22);
		itemToolStripMenuItem.Size = size;
		this.ItemToolStripMenuItem.Text = "Item";
		this.ArmorToolStripMenuItem.Name = "ArmorToolStripMenuItem";
		System.Windows.Forms.ToolStripMenuItem armorToolStripMenuItem = this.ArmorToolStripMenuItem;
		size = new System.Drawing.Size(166, 22);
		armorToolStripMenuItem.Size = size;
		this.ArmorToolStripMenuItem.Text = "Armor";
		this.ToolStripSeparator1.Name = "ToolStripSeparator1";
		System.Windows.Forms.ToolStripSeparator toolStripSeparator = this.ToolStripSeparator1;
		size = new System.Drawing.Size(163, 6);
		toolStripSeparator.Size = size;
		this.WeaponToolStripMenuItem.Name = "WeaponToolStripMenuItem";
		System.Windows.Forms.ToolStripMenuItem weaponToolStripMenuItem = this.WeaponToolStripMenuItem;
		size = new System.Drawing.Size(166, 22);
		weaponToolStripMenuItem.Size = size;
		this.WeaponToolStripMenuItem.Text = "Weapon";
		this.ModdedToolStripMenuItem.Name = "ModdedToolStripMenuItem";
		System.Windows.Forms.ToolStripMenuItem moddedToolStripMenuItem = this.ModdedToolStripMenuItem;
		size = new System.Drawing.Size(166, 22);
		moddedToolStripMenuItem.Size = size;
		this.ModdedToolStripMenuItem.Text = "Modded Weapon";
		this.MunitionToolStripMenuItem.Name = "MunitionToolStripMenuItem";
		System.Windows.Forms.ToolStripMenuItem munitionToolStripMenuItem = this.MunitionToolStripMenuItem;
		size = new System.Drawing.Size(166, 22);
		munitionToolStripMenuItem.Size = size;
		this.MunitionToolStripMenuItem.Text = "Munition";
		this.ToolStripSeparator2.Name = "ToolStripSeparator2";
		System.Windows.Forms.ToolStripSeparator toolStripSeparator2 = this.ToolStripSeparator2;
		size = new System.Drawing.Size(163, 6);
		toolStripSeparator2.Size = size;
		this.WeaponAddonToolStripMenuItem.Name = "WeaponAddonToolStripMenuItem";
		System.Windows.Forms.ToolStripMenuItem weaponAddonToolStripMenuItem = this.WeaponAddonToolStripMenuItem;
		size = new System.Drawing.Size(166, 22);
		weaponAddonToolStripMenuItem.Size = size;
		this.WeaponAddonToolStripMenuItem.Text = "Weapon Addon";
		this.ThrownToolStripMenuItem.Name = "ThrownToolStripMenuItem";
		System.Windows.Forms.ToolStripMenuItem thrownToolStripMenuItem = this.ThrownToolStripMenuItem;
		size = new System.Drawing.Size(166, 22);
		thrownToolStripMenuItem.Size = size;
		this.ThrownToolStripMenuItem.Text = "Thrown";
		this.RangedToolStripMenuItem.Name = "RangedToolStripMenuItem";
		System.Windows.Forms.ToolStripMenuItem rangedToolStripMenuItem = this.RangedToolStripMenuItem;
		size = new System.Drawing.Size(166, 22);
		rangedToolStripMenuItem.Size = size;
		this.RangedToolStripMenuItem.Text = "Ranged";
		this.LoadoutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[2] { this.NormalToolStripMenuItem, this.StoryToolStripMenuItem1 });
		this.LoadoutToolStripMenuItem.Name = "LoadoutToolStripMenuItem";
		System.Windows.Forms.ToolStripMenuItem loadoutToolStripMenuItem = this.LoadoutToolStripMenuItem;
		size = new System.Drawing.Size(152, 22);
		loadoutToolStripMenuItem.Size = size;
		this.LoadoutToolStripMenuItem.Text = "Loadout";
		this.OriginToolStripMenuItem.Name = "OriginToolStripMenuItem";
		System.Windows.Forms.ToolStripMenuItem originToolStripMenuItem = this.OriginToolStripMenuItem;
		size = new System.Drawing.Size(152, 22);
		originToolStripMenuItem.Size = size;
		this.OriginToolStripMenuItem.Text = "Origin";
		this.SkinToolStripMenuItem.Name = "SkinToolStripMenuItem";
		System.Windows.Forms.ToolStripMenuItem skinToolStripMenuItem = this.SkinToolStripMenuItem;
		size = new System.Drawing.Size(152, 22);
		skinToolStripMenuItem.Size = size;
		this.SkinToolStripMenuItem.Text = "Skin";
		this.SkillGroupToolStripMenuItem.Name = "SkillGroupToolStripMenuItem";
		System.Windows.Forms.ToolStripMenuItem skillGroupToolStripMenuItem = this.SkillGroupToolStripMenuItem;
		size = new System.Drawing.Size(152, 22);
		skillGroupToolStripMenuItem.Size = size;
		this.SkillGroupToolStripMenuItem.Text = "Skill Group";
		this.AddonGroupToolStripMenuItem.Name = "AddonGroupToolStripMenuItem";
		System.Windows.Forms.ToolStripMenuItem addonGroupToolStripMenuItem = this.AddonGroupToolStripMenuItem;
		size = new System.Drawing.Size(152, 22);
		addonGroupToolStripMenuItem.Size = size;
		this.AddonGroupToolStripMenuItem.Text = "Addon Group";
		this.StageToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[2] { this.StoryToolStripMenuItem, this.ArenaToolStripMenuItem });
		this.StageToolStripMenuItem.Name = "StageToolStripMenuItem";
		System.Windows.Forms.ToolStripMenuItem stageToolStripMenuItem = this.StageToolStripMenuItem;
		size = new System.Drawing.Size(152, 22);
		stageToolStripMenuItem.Size = size;
		this.StageToolStripMenuItem.Text = "Stage";
		this.StoryToolStripMenuItem.Name = "StoryToolStripMenuItem";
		System.Windows.Forms.ToolStripMenuItem storyToolStripMenuItem = this.StoryToolStripMenuItem;
		size = new System.Drawing.Size(105, 22);
		storyToolStripMenuItem.Size = size;
		this.StoryToolStripMenuItem.Text = "Story";
		this.ArenaToolStripMenuItem.Name = "ArenaToolStripMenuItem";
		System.Windows.Forms.ToolStripMenuItem arenaToolStripMenuItem = this.ArenaToolStripMenuItem;
		size = new System.Drawing.Size(105, 22);
		arenaToolStripMenuItem.Size = size;
		this.ArenaToolStripMenuItem.Text = "Arena";
		this.SwatchToolStripMenuItem.Name = "SwatchToolStripMenuItem";
		System.Windows.Forms.ToolStripMenuItem swatchToolStripMenuItem = this.SwatchToolStripMenuItem;
		size = new System.Drawing.Size(152, 22);
		swatchToolStripMenuItem.Size = size;
		this.SwatchToolStripMenuItem.Text = "Swatch";
		this.WardrobeToolStripMenuItem.Name = "WardrobeToolStripMenuItem";
		System.Windows.Forms.ToolStripMenuItem wardrobeToolStripMenuItem = this.WardrobeToolStripMenuItem;
		size = new System.Drawing.Size(152, 22);
		wardrobeToolStripMenuItem.Size = size;
		this.WardrobeToolStripMenuItem.Text = "Wardrobe";
		this.SaveToolStripMenuItem.Enabled = false;
		this.SaveToolStripMenuItem.Name = "SaveToolStripMenuItem";
		this.SaveToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.S | System.Windows.Forms.Keys.Control;
		System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem = this.SaveToolStripMenuItem;
		size = new System.Drawing.Size(152, 22);
		saveToolStripMenuItem.Size = size;
		this.SaveToolStripMenuItem.Text = "Save";
		this.LoadToolStripMenuItem.Enabled = false;
		this.LoadToolStripMenuItem.Name = "LoadToolStripMenuItem";
		this.LoadToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.L | System.Windows.Forms.Keys.Control;
		System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem = this.LoadToolStripMenuItem;
		size = new System.Drawing.Size(152, 22);
		loadToolStripMenuItem.Size = size;
		this.LoadToolStripMenuItem.Text = "Load";
		this.BatchUpdateToolStripMenuItem.Enabled = false;
		this.BatchUpdateToolStripMenuItem.Name = "BatchUpdateToolStripMenuItem";
		System.Windows.Forms.ToolStripMenuItem batchUpdateToolStripMenuItem = this.BatchUpdateToolStripMenuItem;
		size = new System.Drawing.Size(152, 22);
		batchUpdateToolStripMenuItem.Size = size;
		this.BatchUpdateToolStripMenuItem.Text = "Batch Update";
		this.ToolStripSeparator3.Name = "ToolStripSeparator3";
		System.Windows.Forms.ToolStripSeparator toolStripSeparator3 = this.ToolStripSeparator3;
		size = new System.Drawing.Size(149, 6);
		toolStripSeparator3.Size = size;
		this.TransferNewToolStripMenuItem.Name = "TransferNewToolStripMenuItem";
		System.Windows.Forms.ToolStripMenuItem transferNewToolStripMenuItem = this.TransferNewToolStripMenuItem;
		size = new System.Drawing.Size(152, 22);
		transferNewToolStripMenuItem.Size = size;
		this.TransferNewToolStripMenuItem.Text = "Transfer New";
		this.NewTransferToolStripMenuItem.Name = "NewTransferToolStripMenuItem";
		System.Windows.Forms.ToolStripMenuItem newTransferToolStripMenuItem = this.NewTransferToolStripMenuItem;
		size = new System.Drawing.Size(152, 22);
		newTransferToolStripMenuItem.Size = size;
		this.NewTransferToolStripMenuItem.Text = "New Transfer";
		this.ViewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[6] { this.ColorWheelToolStripMenuItem, this.ExpandCurrentToolStripMenuItem, this.CollapseCurrentToolStripMenuItem, this.ExpandAllToolStripMenuItem, this.CollapseAllToolStripMenuItem, this.DuplicateObjectToolStripMenuItem });
		this.ViewToolStripMenuItem.Name = "ViewToolStripMenuItem";
		System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem = this.ViewToolStripMenuItem;
		size = new System.Drawing.Size(44, 20);
		viewToolStripMenuItem.Size = size;
		this.ViewToolStripMenuItem.Text = "View";
		this.ColorWheelToolStripMenuItem.Enabled = false;
		this.ColorWheelToolStripMenuItem.Name = "ColorWheelToolStripMenuItem";
		this.ColorWheelToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.C | System.Windows.Forms.Keys.Alt;
		System.Windows.Forms.ToolStripMenuItem colorWheelToolStripMenuItem = this.ColorWheelToolStripMenuItem;
		size = new System.Drawing.Size(234, 22);
		colorWheelToolStripMenuItem.Size = size;
		this.ColorWheelToolStripMenuItem.Text = "Color Wheel";
		this.ExpandCurrentToolStripMenuItem.Enabled = false;
		this.ExpandCurrentToolStripMenuItem.Name = "ExpandCurrentToolStripMenuItem";
		this.ExpandCurrentToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.K | System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Control;
		System.Windows.Forms.ToolStripMenuItem expandCurrentToolStripMenuItem = this.ExpandCurrentToolStripMenuItem;
		size = new System.Drawing.Size(234, 22);
		expandCurrentToolStripMenuItem.Size = size;
		this.ExpandCurrentToolStripMenuItem.Text = "Expand Current";
		this.CollapseCurrentToolStripMenuItem.Enabled = false;
		this.CollapseCurrentToolStripMenuItem.Name = "CollapseCurrentToolStripMenuItem";
		this.CollapseCurrentToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.L | System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Control;
		System.Windows.Forms.ToolStripMenuItem collapseCurrentToolStripMenuItem = this.CollapseCurrentToolStripMenuItem;
		size = new System.Drawing.Size(234, 22);
		collapseCurrentToolStripMenuItem.Size = size;
		this.CollapseCurrentToolStripMenuItem.Text = "Collapse Current";
		this.ExpandAllToolStripMenuItem.Enabled = false;
		this.ExpandAllToolStripMenuItem.Name = "ExpandAllToolStripMenuItem";
		this.ExpandAllToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.K | System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt;
		System.Windows.Forms.ToolStripMenuItem expandAllToolStripMenuItem = this.ExpandAllToolStripMenuItem;
		size = new System.Drawing.Size(234, 22);
		expandAllToolStripMenuItem.Size = size;
		this.ExpandAllToolStripMenuItem.Text = "Expand All";
		this.CollapseAllToolStripMenuItem.Enabled = false;
		this.CollapseAllToolStripMenuItem.Name = "CollapseAllToolStripMenuItem";
		this.CollapseAllToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.L | System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt;
		System.Windows.Forms.ToolStripMenuItem collapseAllToolStripMenuItem = this.CollapseAllToolStripMenuItem;
		size = new System.Drawing.Size(234, 22);
		collapseAllToolStripMenuItem.Size = size;
		this.CollapseAllToolStripMenuItem.Text = "Collapse All";
		this.DuplicateObjectToolStripMenuItem.Enabled = false;
		this.DuplicateObjectToolStripMenuItem.Name = "DuplicateObjectToolStripMenuItem";
		this.DuplicateObjectToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.D | System.Windows.Forms.Keys.Alt;
		System.Windows.Forms.ToolStripMenuItem duplicateObjectToolStripMenuItem = this.DuplicateObjectToolStripMenuItem;
		size = new System.Drawing.Size(234, 22);
		duplicateObjectToolStripMenuItem.Size = size;
		this.DuplicateObjectToolStripMenuItem.Text = "Duplicate Object";
		this.ListObjects.BackColor = System.Drawing.Color.FromArgb(32, 32, 32);
		this.ListObjects.Dock = System.Windows.Forms.DockStyle.Right;
		this.ListObjects.Font = new System.Drawing.Font("Impact", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.ListObjects.ForeColor = System.Drawing.SystemColors.Info;
		this.ListObjects.FormattingEnabled = true;
		this.ListObjects.ItemHeight = 20;
		System.Windows.Forms.ListBox listObjects = this.ListObjects;
		location = new System.Drawing.Point(528, 90);
		listObjects.Location = location;
		this.ListObjects.Name = "ListObjects";
		System.Windows.Forms.ListBox listObjects2 = this.ListObjects;
		size = new System.Drawing.Size(256, 571);
		listObjects2.Size = size;
		this.ListObjects.TabIndex = 22;
		this.Param.BackColor = System.Drawing.SystemColors.InfoText;
		this.Param.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.Param.Enabled = false;
		this.Param.Font = new System.Drawing.Font("Impact", 24f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.Param.ForeColor = System.Drawing.SystemColors.Info;
		System.Windows.Forms.TextBox param = this.Param;
		location = new System.Drawing.Point(0, 614);
		param.Location = location;
		this.Param.Name = "Param";
		System.Windows.Forms.TextBox param2 = this.Param;
		size = new System.Drawing.Size(528, 47);
		param2.Size = size;
		this.Param.TabIndex = 24;
		this.Param.Text = "Parameter";
		this.ActionButton.BackColor = System.Drawing.Color.FromArgb(32, 32, 32);
		this.ActionButton.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.ActionButton.Enabled = false;
		this.ActionButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.ActionButton.Font = new System.Drawing.Font("Impact", 15.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.ActionButton.ForeColor = System.Drawing.SystemColors.Info;
		System.Windows.Forms.Button actionButton = this.ActionButton;
		location = new System.Drawing.Point(0, 581);
		actionButton.Location = location;
		this.ActionButton.Name = "ActionButton";
		System.Windows.Forms.Button actionButton2 = this.ActionButton;
		size = new System.Drawing.Size(528, 33);
		actionButton2.Size = size;
		this.ActionButton.TabIndex = 16;
		this.ActionButton.TextAlign = System.Drawing.ContentAlignment.TopLeft;
		this.ActionButton.UseVisualStyleBackColor = false;
		this.ColorWheel.AnyColor = true;
		this.ObjEditor.BackColor = System.Drawing.Color.Black;
		this.ObjEditor.Dock = System.Windows.Forms.DockStyle.Fill;
		this.ObjEditor.Font = new System.Drawing.Font("Courier New", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.ObjEditor.ForeColor = System.Drawing.Color.White;
		System.Windows.Forms.TreeView objEditor = this.ObjEditor;
		location = new System.Drawing.Point(0, 90);
		objEditor.Location = location;
		this.ObjEditor.Name = "ObjEditor";
		this.ObjEditor.RightToLeft = System.Windows.Forms.RightToLeft.No;
		System.Windows.Forms.TreeView objEditor2 = this.ObjEditor;
		size = new System.Drawing.Size(528, 491);
		objEditor2.Size = size;
		this.ObjEditor.TabIndex = 25;
		this.FileName.BackColor = System.Drawing.SystemColors.InfoText;
		this.FileName.Dock = System.Windows.Forms.DockStyle.Top;
		this.FileName.Enabled = false;
		this.FileName.Font = new System.Drawing.Font("Impact", 24f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.FileName.ForeColor = System.Drawing.SystemColors.Info;
		System.Windows.Forms.TextBox fileName = this.FileName;
		location = new System.Drawing.Point(0, 43);
		fileName.Location = location;
		this.FileName.Name = "FileName";
		System.Windows.Forms.TextBox fileName2 = this.FileName;
		size = new System.Drawing.Size(784, 47);
		fileName2.Size = size;
		this.FileName.TabIndex = 21;
		this.FileName.Text = "Create new card to begin.";
		this.Price.AutoSize = true;
		this.Price.BackColor = System.Drawing.Color.Black;
		this.Price.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.Price.Font = new System.Drawing.Font("Impact", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.Price.ForeColor = System.Drawing.Color.Lime;
		System.Windows.Forms.Label price = this.Price;
		location = new System.Drawing.Point(93, 2);
		price.Location = location;
		this.Price.Name = "Price";
		System.Windows.Forms.Label price2 = this.Price;
		size = new System.Drawing.Size(17, 19);
		price2.Size = size;
		this.Price.TabIndex = 26;
		this.Price.Text = "$";
		this.Price.Visible = false;
		this.LoadingProgress.BackColor = System.Drawing.Color.DarkRed;
		this.LoadingProgress.Dock = System.Windows.Forms.DockStyle.Top;
		this.LoadingProgress.ForeColor = System.Drawing.Color.DarkRed;
		System.Windows.Forms.ProgressBar loadingProgress = this.LoadingProgress;
		location = new System.Drawing.Point(0, 24);
		loadingProgress.Location = location;
		this.LoadingProgress.Name = "LoadingProgress";
		System.Windows.Forms.ProgressBar loadingProgress2 = this.LoadingProgress;
		size = new System.Drawing.Size(784, 19);
		loadingProgress2.Size = size;
		this.LoadingProgress.TabIndex = 27;
		this.NormalToolStripMenuItem.Name = "NormalToolStripMenuItem";
		System.Windows.Forms.ToolStripMenuItem normalToolStripMenuItem = this.NormalToolStripMenuItem;
		size = new System.Drawing.Size(152, 22);
		normalToolStripMenuItem.Size = size;
		this.NormalToolStripMenuItem.Text = "Normal";
		this.StoryToolStripMenuItem1.Name = "StoryToolStripMenuItem1";
		System.Windows.Forms.ToolStripMenuItem storyToolStripMenuItem2 = this.StoryToolStripMenuItem1;
		size = new System.Drawing.Size(152, 22);
		storyToolStripMenuItem2.Size = size;
		this.StoryToolStripMenuItem1.Text = "Story";
		System.Drawing.SizeF sizeF = new System.Drawing.SizeF(6f, 13f);
		this.AutoScaleDimensions = sizeF;
		this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.FromArgb(64, 64, 64);
		size = new System.Drawing.Size(784, 661);
		this.ClientSize = size;
		this.Controls.Add(this.Price);
		this.Controls.Add(this.ObjEditor);
		this.Controls.Add(this.ActionButton);
		this.Controls.Add(this.Param);
		this.Controls.Add(this.ListObjects);
		this.Controls.Add(this.FileName);
		this.Controls.Add(this.LoadingProgress);
		this.Controls.Add(this.EditorStrip);
		this.Name = "FormEditorV2";
		this.Text = "Create an Entry";
		this.EditorStrip.ResumeLayout(false);
		this.EditorStrip.PerformLayout();
		this.ResumeLayout(false);
		this.PerformLayout();
	}

	private void FormEditorV2_Load(object sender, EventArgs e)
	{
		ObjEditor.KeyDown += Action_KeyDown;
		Param.KeyDown += Action_KeyDown;
		ListObjects.KeyDown += Action_KeyDown;
	}

	private void ObjEditor_MouseEnter(object sneder, EventArgs e)
	{
		if (Focused)
		{
			ObjEditor.Focus();
		}
	}

	private void ObjList_MouseEnter(object sneder, EventArgs e)
	{
		if (Focused)
		{
			ListObjects.Focus();
		}
	}

	private void CharacterToolStripMenuItem_Click(object sender, EventArgs e)
	{
		ModCards.PropertyList Schema = ModCards.Statcard_Character;
		NewCard("characters", "Arena", ref ModEnums.DB_CHAR, ref Schema, "My OC");
	}

	private void AnimationSetToolStripMenuItem_Click(object sender, EventArgs e)
	{
		ModCards.PropertyList Schema = ModCards.Statcard_AnimSet;
		NewCard("animsets", "Default", ref ModEnums.DB_ANIMSETS, ref Schema, "My Moves");
	}

	private void ChatterToolStripMenuItem_Click(object sender, EventArgs e)
	{
		ModCards.PropertyList Schema = ModCards.Statcard_Chatter;
		NewCard("chatter", "Arena", ref ModEnums.DB_CHATTER, ref Schema, "My Words");
	}

	private void CorpusToolStripMenuItem_Click(object sender, EventArgs e)
	{
		ModCards.PropertyList Schema = ModCards.Statcard_Corpus;
		NewCard("corpus", "Basic", ref ModEnums.DB_CORPUS, ref Schema, "My Health");
	}

	private void DoTEffectToolStripMenuItem_Click(object sender, EventArgs e)
	{
		ModCards.PropertyList Schema = ModCards.Statcard_DoT;
		NewCard("dot", "Drugged", ref ModEnums.DB_DOT, ref Schema, "My Pain");
	}

	private void NormalToolStripMenuItem_Click(object sender, EventArgs e)
	{
		ModCards.PropertyList Schema = ModCards.Statcard_Loadout;
		NewCard("loadouts", "Warchest_Weapons", ref ModEnums.DB_LOADOUTS, ref Schema, "My Arsenal");
	}

	private void StoryToolStripMenuItem1_Click(object sender, EventArgs e)
	{
		ModCards.PropertyList Schema = ModCards.Statcard_Loadout;
		NewCard("storyloadouts", "S_Ex_Sheriff", ref ModEnums.DB_STORYLOADOUTS, ref Schema, "My Arsenal");
	}

	private void OriginToolStripMenuItem_Click(object sender, EventArgs e)
	{
		ModCards.PropertyList Schema = ModCards.Statcard_Origin;
		NewCard("origins", "None", ref ModEnums.DB_ORIGINS, ref Schema, "My Backstory");
	}

	private void SkillGroupToolStripMenuItem_Click(object sender, EventArgs e)
	{
		ModCards.PropertyList Schema = ModCards.Statcard_Skillgroup;
		NewCard("skillgroups", "AAHW_Base", ref ModEnums.DB_SKILLGROUPS, ref Schema, "My Skillz");
	}

	private void WardrobeToolStripMenuItem_Click(object sender, EventArgs e)
	{
		ModCards.PropertyList Schema = ModCards.Statcard_Wardrobe;
		NewCard("wardrobe", "S_Hank_1", ref ModEnums.DB_WARDROBE, ref Schema, "My Drip");
	}

	private void SwatchToolStripMenuItem_Click(object sender, EventArgs e)
	{
		ModCards.PropertyList Schema = ModCards.Statcard_Swatch;
		NewCard("swatches", "Tint_Ew", ref ModEnums.DB_SWATCHES, ref Schema, "My Swatch");
	}

	private void ArmorToolStripMenuItem_Click(object sender, EventArgs e)
	{
		ModCards.PropertyList Schema = ModCards.Statcard_Armor;
		NewCard("armor", "Outfit_Jumpsuit_TshirtPants", ref ModEnums.DB_GEAR, ref Schema, "My Threads");
	}

	private void WeaponToolStripMenuItem_Click(object sender, EventArgs e)
	{
		ModCards.PropertyList Schema = ModCards.StatCard_WeaponBase;
		NewCard("weapons", "M_Bat", ref ModEnums.DB_BASEWEAPONS, ref Schema, "My Weapon", savetoWeapon: true);
	}

	private void ModdedWeaponToolStripMenuItem_Click(object sender, EventArgs e)
	{
		ModCards.PropertyList Schema = ModCards.Statcard_WeaponModded;
		NewCard("weaponmodded", "[MOD] 9011 Hank", ref ModEnums.DB_MODDEDWEAPONS, ref Schema, "My Modded Gun", savetoWeapon: true);
	}

	private void MunitionToolStripMenuItem_Click(object sender, EventArgs e)
	{
		ModCards.PropertyList Schema = ModCards.StatCard_Ammo;
		NewCard("ammo", "9mm", ref ModEnums.DB_AMMO, ref Schema, "My Bullet");
	}

	private void WeaponAddonToolStripMenuItem_Click(object sender, EventArgs e)
	{
		ModCards.PropertyList Schema = ModCards.StatCard_WeaponAddon;
		NewCard("weaponaddons", "Bayonet_PistolBasic", ref ModEnums.DB_WEAPONADDONS, ref Schema, "My Addon");
	}

	private void ThrownToolStripMenuItem_Click(object sender, EventArgs e)
	{
		ModCards.PropertyList Schema = ModCards.StatCard_Thrown;
		NewCard("thrown", "T_Dagger", ref ModEnums.DB_THROWN, ref Schema, "My Throwable", savetoWeapon: true);
	}

	private void RangedToolStripMenuItem_Click(object sender, EventArgs e)
	{
		ModCards.PropertyList Schema = ModCards.StatCard_Ranged;
		NewCard("ranged", "R_AN15", ref ModEnums.DB_RANGED, ref Schema, "My Gun", savetoWeapon: true);
	}

	private void AddonGroupToolStripMenuItem_Click(object sender, EventArgs e)
	{
		ModCards.PropertyList Schema = ModCards.StatCard_AddonGroup;
		NewCard("weaponaddongroups", "Firearms_Barrels_Core", ref ModEnums.DB_ADDONGROUPS, ref Schema, "My Addons");
	}

	private void SkinToolStripMenuItem_Click(object sender, EventArgs e)
	{
		ModCards.PropertyList Schema = ModCards.Statcard_Skin;
		NewCard("skins", "0_Default", ref ModEnums.DB_SKINS, ref Schema, "My Body");
	}

	private void ExecutionToolStripMenuItem_Click(object sender, EventArgs e)
	{
		ModCards.PropertyList Schema = ModCards.Statcard_Execution;
		NewCard("executions", "0_Standard_U", ref ModEnums.DB_EXECUTION, ref Schema, "My Finishing Move!");
	}

	private void ExplosionToolStripMenuItem_Click(object sender, EventArgs e)
	{
		ModCards.PropertyList Schema = ModCards.Statcard_Explosion;
		NewCard("explosions", "Grenade", ref ModEnums.DB_EXPLOSION, ref Schema, "My Kaboom");
	}

	private void AnimationToolStripMenuItem_Click(object sender, EventArgs e)
	{
		ModCards.PropertyList Schema = ModCards.Statcard_Animation;
		NewCard("animation", "Idle_Gen", ref ModEnums.DB_ANIMATIONS, ref Schema, "My Move");
	}

	private void StoryToolStripMenuItem_Click(object sender, EventArgs e)
	{
		ModCards.PropertyList Schema = ModCards.Statcard_Story;
		NewCard("stories", "S_Outs_0", ref ModEnums.DB_STORIES, ref Schema, "S_Outs_0");
	}

	private void ArenaToolStripMenuItem_Click(object sender, EventArgs e)
	{
		ModCards.PropertyList Schema = ModCards.Statcard_Arena;
		NewCard("arenas", "Arena_0_Tutorial", ref ModEnums.DB_ARENAS, ref Schema, "Arena_0_Tutorial");
	}

	private void Action_KeyDown(object sender, KeyEventArgs e)
	{
		if ((e.KeyCode == Keys.Return) & ActionButton.Enabled)
		{
			ButtonAction();
		}
		else if (((e.KeyCode == Keys.Down) | (e.KeyCode == Keys.Up)) & !ListObjects.Focused)
		{
			ObjEditor.Focus();
		}
		else
		{
			Param.Focus();
		}
	}

	private void ObjEditor_AfterSelect(object sender, TreeViewEventArgs e)
	{
		if (ObjEditor.SelectedNode != null)
		{
			Loading = true;
			ActionButton.Enabled = true;
			ExpandCurrentToolStripMenuItem.Enabled = true;
			CollapseCurrentToolStripMenuItem.Enabled = true;
			SelectedTag = (ModCards.NodeTag)ObjEditor.SelectedNode.Tag;
			ModCards.PropertyControl linkedControl = SelectedTag.LinkedControl;
			string text = linkedControl.PropType.ToString();
			Param.Enabled = (linkedControl.PropType != ModCards.PropType.Bool) & (linkedControl.PropType != ModCards.PropType.Obj);
			if (linkedControl.IsList)
			{
				text = ((SelectedTag.NodeType == ModCards.NodeType.Arr) ? ("List<" + text + ">") : ("[" + Conversions.ToString(ObjEditor.SelectedNode.Index) + "]"));
			}
			ActionButton.Text = "Editing " + linkedControl.PropName + " " + text;
			if (linkedControl.IsList & !linkedControl.Fixed)
			{
				ActionButton.Text += ((SelectedTag.NodeType == ModCards.NodeType.Arr) ? " <Click to add to list>" : "<Click to remove from list>");
			}
			else if (linkedControl.PropType == ModCards.PropType.Color)
			{
				ActionButton.Text += " <Click to open Color Wheel>";
			}
			else if (linkedControl.PropType == ModCards.PropType.Bool)
			{
				ActionButton.Text += " <Click to toggle>";
			}
			ColorWheelToolStripMenuItem.Enabled = linkedControl.PropType == ModCards.PropType.Color;
			DuplicateObjectToolStripMenuItem.Enabled = linkedControl.IsList & !linkedControl.Fixed & (SelectedTag.NodeType != ModCards.NodeType.Arr);
			if (DuplicateObjectToolStripMenuItem.Enabled)
			{
				ActionButton.Text += "<Alt+D to Duplicate>";
			}
			Param.Text = ((Param.Enabled & (SelectedTag.NodeType == ModCards.NodeType.Val)) ? SelectedTag.Value.ToString() : "");
			if ((linkedControl.PropType == ModCards.PropType.Ref) | ((linkedControl.PropType == ModCards.PropType.Ord) & (linkedControl.Selection != null)))
			{
				ListObjects.DataSource = RuntimeHelpers.GetObjectValue(linkedControl.Selection);
			}
			else
			{
				ListObjects.DataSource = null;
			}
			Loading = false;
		}
	}

	private void Param_TextChanged(object sender, EventArgs e)
	{
		if ((SelectedTag != null) & !Loading)
		{
			EvaluateParameter(ObjEditor.SelectedNode, SelectedTag);
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

	internal void NewCard(string folder, string loadEntry, ref Dictionary<string, JObject> entries, ref ModCards.PropertyList Schema, string newName = "", bool savetoWeapon = false)
	{
        if (entries.Count == 0) { ReportEditorStatus("No templates are available for " + folder + ".", true); return; }
        if (!entries.ContainsKey(loadEntry)) loadEntry = entries.Keys.OrderBy(k => k).First();
		Reset();
		SaveFolder = folder;
        _deckSaveDirectory=null;
		LoadableEntries = entries;
		SavedList = Schema;
		ControlScheme = Schema();
		FileName.Text = newName;
		FileName.Enabled = (Operators.CompareString(folder, "stories", TextCompare: false) != 0) & (Operators.CompareString(folder, "arenas", TextCompare: false) != 0);
		LoadToolStripMenuItem.Enabled = true;
		SaveToolStripMenuItem.Enabled = true;
		ExpandAllToolStripMenuItem.Enabled = true;
		CollapseAllToolStripMenuItem.Enabled = true;
		BatchUpdateToolStripMenuItem.Enabled = true;
		NotifyAddedProperties = true;
		SaveAsWeapon = savetoWeapon;
		LoadObject(ObjEditor.Nodes, LoadableEntries[loadEntry], ControlScheme);
		NotifyAddedProperties = false;
		DisplayPrice();
        ResetHistory();
	}

	internal void RefreshCard()
	{
		JObject card = SaveObj(ObjEditor.Nodes);
		Reset();
		ControlScheme = SavedList();
		LoadObject(ObjEditor.Nodes, card, ControlScheme);
	}

	private void LoadToolStripMenuItem_Click(object sender, EventArgs e)
	{
		DialogCreateEntry dialogCreateEntry = new DialogCreateEntry();
		dialogCreateEntry.Text = "Load Entry";
		dialogCreateEntry.CardType = SaveFolder;
		dialogCreateEntry.Database = LoadableEntries.Keys.ToArray();
		dialogCreateEntry.ComboEntrySelect.AutoCompleteMode = AutoCompleteMode.Append;
		dialogCreateEntry.Action = "Load";
		if ((dialogCreateEntry.ShowDialog() == DialogResult.OK) & (dialogCreateEntry.ComboEntrySelect.SelectedItem != null))
		{
			Reset();
			string key = dialogCreateEntry.ComboEntrySelect.SelectedItem.ToString();
			FileName.Text = key;
			NotifyAddedProperties = true;
			LoadObject(ObjEditor.Nodes, LoadableEntries[key], ControlScheme);
			NotifyAddedProperties = false;
			DisplayPrice();
            ResetHistory();
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
	private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
	{
        SaveWithPicker();
	}

	[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
	private void BatchDiagnoseToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (DossierDialog.MsgBox("Run a batch update on all files within db/new/" + SaveFolder + "?", MsgBoxStyle.YesNo) != MsgBoxResult.Yes)
		{
			return;
		}
		string path = StudioModule.BaseDirectory + "/db/new/" + SaveFolder + "/";
		checked
		{
			try
			{
				string[] files = Directory.GetFiles(path, "*.json", SearchOption.AllDirectories);
				LoadingProgress.Maximum = files.Count();
				LoadingProgress.Step = 1;
				LoadingProgress.BackColor = Color.Red;
				int num = 0;
				int num2 = 0;
				int num3 = 0;
				string[] array = files;
				foreach (string text in array)
				{
					string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(text);
					string json = File.ReadAllText(text);
					BatchDiagnose = fileNameWithoutExtension;
					try
					{
						JObject card = JObject.Parse(json);
						TreeNode treeNode = new TreeNode();
						LoadObject(treeNode.Nodes, card, ControlScheme);
						CheckForAllNulls(treeNode.Nodes);
						if (!RepairLine.ContainsKey(fileNameWithoutExtension))
						{
							JObject jObject = SaveObj(treeNode.Nodes);
							File.WriteAllText(text, jObject.ToString());
							if (!LoadableEntries.ContainsKey(fileNameWithoutExtension))
							{
								LoadableEntries.Add(fileNameWithoutExtension, jObject);
							}
							else
							{
								LoadableEntries[fileNameWithoutExtension] = jObject;
							}
							num++;
						}
						else
						{
							if (!RepairDirectories.ContainsKey(fileNameWithoutExtension))
							{
								RepairDirectories.Add(fileNameWithoutExtension, text);
							}
							num3++;
						}
					}
					catch (JsonReaderException ex)
					{
						ProjectData.SetProjectError(ex);
						JsonReaderException ex2 = ex;
						DossierDialog.MsgBox("Error reading " + fileNameWithoutExtension + ":\r\n" + ex2.Message, MsgBoxStyle.Exclamation);
						num2++;
						ProjectData.ClearProjectError();
					}
					catch (Exception ex3)
					{
						ProjectData.SetProjectError(ex3);
						Exception ex4 = ex3;
						DossierDialog.MsgBox("Error loading " + fileNameWithoutExtension + ":\r\n" + ex4.Message, MsgBoxStyle.Exclamation);
						num2++;
						ProjectData.ClearProjectError();
					}
					LoadingProgress.PerformStep();
				}
				LoadingProgress.Value = 0;
				BatchDiagnose = "";
				if (Diagnostics.Count > 0 && DossierDialog.MsgBox("There are " + Conversions.ToString(num3) + " files with null references, would you like to run the diagnostics editor?", MsgBoxStyle.YesNo, "Null Refs Detected") == MsgBoxResult.Yes)
				{
					DialogNullReport dialogNullReport = new DialogNullReport();
					dialogNullReport.ControlScheme = ControlScheme;
					dialogNullReport.LoadableEntries = LoadableEntries;
					foreach (string key in Diagnostics.Keys)
					{
						dialogNullReport.ListRefs.Nodes.Add(Diagnostics[key]);
					}
					dialogNullReport.ListRefs.ExpandAll();
					if (dialogNullReport.ShowDialog() == DialogResult.Yes)
					{
						TreeNodeCollection nodes = dialogNullReport.ListRefs.Nodes;
						foreach (TreeNode item in nodes)
						{
							if (RepairLine.ContainsKey(item.Name))
							{
								num3 = (ReplaceProperties(RepairLine[item.Name].Nodes, item.Nodes) ? (num3 - 1) : num3);
							}
						}
					}
				}
				LoadingProgress.Maximum = RepairLine.Count;
				foreach (string key2 in RepairLine.Keys)
				{
					string path2 = RepairDirectories[key2];
					string fileNameWithoutExtension2 = Path.GetFileNameWithoutExtension(path2);
					try
					{
						JObject jObject2 = SaveObj(RepairLine[key2].Nodes);
						File.WriteAllText(path2, jObject2.ToString());
						if (!LoadableEntries.ContainsKey(fileNameWithoutExtension2))
						{
							LoadableEntries.Add(fileNameWithoutExtension2, jObject2);
						}
						else
						{
							LoadableEntries[fileNameWithoutExtension2] = jObject2;
						}
						num++;
					}
					catch (JsonReaderException ex5)
					{
						ProjectData.SetProjectError(ex5);
						JsonReaderException ex6 = ex5;
						DossierDialog.MsgBox("Error reading " + fileNameWithoutExtension2 + ":\r\n" + ex6.Message, MsgBoxStyle.Exclamation);
						num2++;
						ProjectData.ClearProjectError();
					}
					catch (Exception ex7)
					{
						ProjectData.SetProjectError(ex7);
						Exception ex8 = ex7;
						DossierDialog.MsgBox("Error loading " + fileNameWithoutExtension2 + ":\r\n" + ex8.Message, MsgBoxStyle.Exclamation);
						num2++;
						ProjectData.ClearProjectError();
					}
					LoadingProgress.PerformStep();
				}
				Diagnostics.Clear();
				RepairLine.Clear();
				RepairDirectories.Clear();
				DossierDialog.MsgBox(Conversions.ToString(num) + " files updated.\r\n" + Conversions.ToString(num2) + " files failed to be read." + ((num3 > 0) ? ("\r\n" + Conversions.ToString(num3) + " files with null refs.") : ""), MsgBoxStyle.Information);
				LoadingProgress.Value = 0;
				RefreshCard();
			}
			catch (DirectoryNotFoundException ex9)
			{
				ProjectData.SetProjectError(ex9);
				DirectoryNotFoundException ex10 = ex9;
				ProjectData.ClearProjectError();
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
	private void TransferNewToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (Directory.Exists(ModEnums.DIR_TRANSFER))
		{
			if (DossierDialog.MsgBox("Copy all new files into " + ModEnums.DIR_TRANSFER + "?", MsgBoxStyle.YesNo) == MsgBoxResult.Yes)
			{
				MyProject.Computer.FileSystem.CopyDirectory(StudioModule.BaseDirectory + "\\db\\new\\", ModEnums.DIR_TRANSFER, overwrite: true);
			}
		}
		else
		{
			DossierDialog.MsgBox("Directory Invalid\r\n" + ModEnums.DIR_TRANSFER, MsgBoxStyle.Exclamation);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
	private void NewTransferToolStripMenuItem_Click(object sender, EventArgs e)
	{
        try{using(var settings=new DossierTransferSettings())settings.ShowDialog(this);}catch(Exception ex){ReportEditorStatus(ex.Message,true);}
	}

	private void ColorWheelToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (ColorWheel.ShowDialog() != DialogResult.Cancel)
		{
			Param.Text = ColorTranslator.ToHtml(ColorWheel.Color);
		}
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

	private void DuplicateObjectToolStripMenuItem_Click(object sender, EventArgs e)
	{
		JToken token = SerializeNode(ObjEditor.SelectedNode, SelectedTag);
		ObjEditor.SelectedNode.Parent.Nodes.Add(NewPropertyNode(SelectedTag.LinkedControl, token));
	}

	private void SaveToDirectory(string saveDir)
	{
		JObject jObject = SaveObj(ObjEditor.Nodes);
		WriteEntrySafely(saveDir, jObject.ToString());
		if (!LoadableEntries.ContainsKey(FileName.Text))
		{
			LoadableEntries.Add(FileName.Text, jObject);
		}
		else
		{
			LoadableEntries[FileName.Text] = jObject;
		}
		if (SaveAsWeapon)
		{
			if (!ModEnums.DB_WEAPONS.ContainsKey(FileName.Text))
			{
				ModEnums.DB_WEAPONS.Add(FileName.Text, jObject);
			}
			else
			{
				ModEnums.DB_WEAPONS[FileName.Text] = jObject;
			}
		}
		Focus();
        string deckDirectory=_deckSaveDirectory;
		NewCard(SaveFolder, FileName.Text, ref LoadableEntries, ref SavedList, FileName.Text, SaveAsWeapon);
        _deckSaveDirectory=deckDirectory;
		IEnumerable<FormEditorV2> enumerable = from t in Application.OpenForms.OfType<FormEditorV2>()
			where !t.Focused & (t.ObjEditor.Nodes.Count > 0)
			select t;
		foreach (FormEditorV2 item in enumerable)
		{
			item.RefreshCard();
		}
        _lastTransferStatus=DossierTransfer.AfterSave(saveDir,SaveFolder);
	}

	private JObject SaveObj(TreeNodeCollection nodeSpace)
	{
        JObject extra;
		JObject jObject = _extraProperties.TryGetValue(nodeSpace,out extra)?JObject.Parse(extra.ToString()):new JObject();
		foreach (TreeNode item in nodeSpace)
		{
			jObject.Add(item.Name, SerializeNode(item, (ModCards.NodeTag)item.Tag));
		}
		return jObject;
	}

	private JToken SerializeNode(TreeNode node, ModCards.NodeTag nodetag)
	{
		ModCards.PropertyControl linkedControl = nodetag.LinkedControl;
		switch (nodetag.NodeType)
		{
		case ModCards.NodeType.Val:
			switch (linkedControl.PropType)
			{
			case ModCards.PropType.Ord:
				return new JValue(Conversions.ToInteger(nodetag.Value));
			case ModCards.PropType.Color:
			{
				ModCards.ColorRGBA colorRGBA = (ModCards.ColorRGBA)nodetag.Value;
				JObject jObject = new JObject();
				jObject.Add("r", colorRGBA.r);
				jObject.Add("g", colorRGBA.g);
				jObject.Add("b", colorRGBA.b);
				jObject.Add("a", colorRGBA.a);
				return jObject;
			}
			default:
				return new JValue(RuntimeHelpers.GetObjectValue(nodetag.Value));
			}
		case ModCards.NodeType.Arr:
		{
			JArray jArray = new JArray();
			{
				foreach (TreeNode node2 in node.Nodes)
				{
					jArray.Add(SerializeNode(node2, (ModCards.NodeTag)node2.Tag));
				}
				return jArray;
			}
		}
		case ModCards.NodeType.Obj:
			return SaveObj(node.Nodes);
		default:
			return null;
		}
	}

	private void LoadObject(TreeNodeCollection nodeSpace, JObject card, ModCards.PropertyControl[] scheme)
	{
		var extra=new JObject();var known=new System.Collections.Generic.HashSet<string>(scheme.Select(c=>c.PropName),StringComparer.Ordinal);
        foreach(var property in card.Properties())if(!known.Contains(property.Name))extra.Add(property.Name,property.Value.DeepClone());
        _extraProperties.Remove(nodeSpace);_extraProperties.Add(nodeSpace,extra);
		nodeSpace.Clear();
		int num = 0;
		string text = "";
		checked
		{
			foreach (ModCards.PropertyControl propertyControl in scheme)
			{
				if (DataMatches(propertyControl, card[propertyControl.PropName]))
				{
					nodeSpace.Add(NewPropertyNode(propertyControl, card[propertyControl.PropName], isParent: true));
				}
				else if (IsDeferred(propertyControl, card[propertyControl.PropName]))
				{
					nodeSpace.Add(NewPropertyNode(propertyControl, LoadableEntries[(string)card[propertyControl.PropName]][propertyControl.PropName], isParent: true));
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
                ReportEditorStatus(num + " missing properties were filled from the schema. Review before saving.");
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
		case ModCards.PropType.Bool:
		case ModCards.PropType.Int:
		case ModCards.PropType.Ord:
		{
			string s2 = (string)token;
			CultureInfo invariantCulture2 = CultureInfo.InvariantCulture;
			int result2 = default;
			return int.TryParse(s2, NumberStyles.Integer, invariantCulture2, out result2);
		}
		case ModCards.PropType.Float:
		{
			string s = (string)token;
			CultureInfo invariantCulture = CultureInfo.InvariantCulture;
			double result = default;
			return double.TryParse(s, NumberStyles.Float, invariantCulture, out result);
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
		if (!LoadableEntries.ContainsKey(token.ToString()))
		{
			return false;
		}
		return DataMatches(control, LoadableEntries[token.ToString()][control.PropName]);
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
			LoadObject(treeNode.Nodes, JObject.Parse(token.ToString()), (ModCards.PropertyControl[])control.Selection);
		}
		else
		{
			treeNode.Tag = new ModCards.NodeTag(control, ModCards.NodeType.Val);
			switch (control.PropType)
			{
			case ModCards.PropType.Color:
			{
				ModCards.ColorRGBA colorRGBA = new ModCards.ColorRGBA();
				double.TryParse(token["r"].ToString(), out colorRGBA.r);
				double.TryParse(token["g"].ToString(), out colorRGBA.g);
				double.TryParse(token["b"].ToString(), out colorRGBA.b);
				double.TryParse(token["a"].ToString(), out colorRGBA.a);
				NewLateBinding.LateSetComplex(treeNode.Tag, null, "Value", new object[1] { colorRGBA }, null, null, OptimisticSet: false, RValueBase: true);
				treeNode.ForeColor = ColorTranslator.FromHtml(colorRGBA.ToString());
				break;
			}
			case ModCards.PropType.Float:
			{
				string s2 = token.ToString();
				CultureInfo currentCulture2 = CultureInfo.CurrentCulture;
				object tag = treeNode.Tag;
				double result2 = Conversions.ToDouble(NewLateBinding.LateGet(tag, null, "Value", new object[0], null, null, null));
				double.TryParse(s2, NumberStyles.Float, currentCulture2, out result2);
				NewLateBinding.LateSetComplex(tag, null, "Value", new object[1] { result2 }, null, null, OptimisticSet: true, RValueBase: true);
				break;
			}
			case ModCards.PropType.Bool:
			case ModCards.PropType.Int:
			{
				string s = token.ToString();
				CultureInfo currentCulture = CultureInfo.CurrentCulture;
				object tag = treeNode.Tag;
				int result = Conversions.ToInteger(NewLateBinding.LateGet(tag, null, "Value", new object[0], null, null, null));
				int.TryParse(s, NumberStyles.Integer, currentCulture, out result);
				NewLateBinding.LateSetComplex(tag, null, "Value", new object[1] { result }, null, null, OptimisticSet: true, RValueBase: true);
				break;
			}
			case ModCards.PropType.Ord:
				NewLateBinding.LateSetComplex(treeNode.Tag, null, "Value", new object[1] { RuntimeHelpers.GetObjectValue(Conversion.CTypeDynamic(token.ToString(), NewLateBinding.LateIndexGet(control.Selection, new object[1] { 0 }, null).GetType())) }, null, null, OptimisticSet: false, RValueBase: true);
				break;
			default:
				NewLateBinding.LateSetComplex(treeNode.Tag, null, "Value", new object[1] { token.ToString() }, null, null, OptimisticSet: false, RValueBase: true);
				break;
			}
			CheckNullValue(control, treeNode);
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
				NewLateBinding.LateSetComplex(treeNode.Tag, null, "Value", new object[1] { RuntimeHelpers.GetObjectValue(GetDefaultValue(prop.PropType, RuntimeHelpers.GetObjectValue((prop.Selection != null) ? NewLateBinding.LateIndexGet(prop.Selection, new object[1] { 0 }, null) : null))) }, null, null, OptimisticSet: false, RValueBase: true);
			}
			treeNode.Text = GetDisplayNode(treeNode);
		}
		else if ((ListObjects.SelectedItem != null) & !Loading)
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
		ObjEditor.Nodes.Remove(ObjEditor.SelectedNode);
		ObjEditor.SelectedNode = selectedNode2;
		Loading = false;
	}

	private void CheckForAllNulls(TreeNodeCollection nodeSpace)
	{
		foreach (TreeNode item in nodeSpace)
		{
			if (item.Nodes.Count == 0)
			{
				if (item.ForeColor == Color.Salmon)
				{
					if (!Diagnostics.ContainsKey(BatchDiagnose))
					{
						TreeNode treeNode2 = new TreeNode(BatchDiagnose);
						treeNode2.Name = BatchDiagnose;
						treeNode2.ForeColor = Color.Red;
						Diagnostics.Add(BatchDiagnose, treeNode2);
						RepairLine.Add(BatchDiagnose, (TreeNode)GetRootNode(item));
					}
					TreeNode treeNode3 = new TreeNode(GetNodeDir(item) + ": " + item.Tag.ToString());
					treeNode3.Name = GetNodeDir(item);
					treeNode3.Tag = RuntimeHelpers.GetObjectValue(item.Tag);
					treeNode3.ForeColor = Color.Salmon;
					Diagnostics[BatchDiagnose].Nodes.Add(treeNode3);
				}
			}
			else
			{
				CheckForAllNulls(item.Nodes);
			}
		}
	}

	private bool ReplaceProperties(TreeNodeCollection toCard, TreeNodeCollection newProperties)
	{
		int num = newProperties.Count;
		foreach (TreeNode newProperty in newProperties)
		{
			if (newProperty.ForeColor != Color.Salmon)
			{
				string name = newProperty.Name;
				TreeNode treeNode2 = GoToNode(toCard, name);
				NewLateBinding.LateSetComplex(treeNode2.Tag, null, "Value", new object[1] { RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(newProperty.Tag, null, "Value", new object[0], null, null, null)) }, null, null, OptimisticSet: false, RValueBase: true);
				num = checked(num - 1);
			}
		}
		return num == 0;
	}

	private object GetDefaultValue(ModCards.PropType vtype, object val)
	{
		switch (vtype)
		{
		case ModCards.PropType.Bool:
		case ModCards.PropType.Int:
			return 0;
		case ModCards.PropType.Float:
			return 1m;
		case ModCards.PropType.Str:
			return "";
		case ModCards.PropType.Ref:
		case ModCards.PropType.Ord:
			return val;
		case ModCards.PropType.Color:
			return new ModCards.ColorRGBA();
		default:
			return null;
		}
	}

	private ModCards.ColorRGBA ColorFromHex(Color color)
	{
		ModCards.ColorRGBA colorRGBA = new ModCards.ColorRGBA();
		colorRGBA.r = Math.Round((double)(int)color.R / 255.0, 7);
		colorRGBA.g = Math.Round((double)(int)color.G / 255.0, 7);
		colorRGBA.b = Math.Round((double)(int)color.B / 255.0, 7);
		colorRGBA.a = Math.Round((double)(int)color.A / 255.0, 7);
		return colorRGBA;
	}

	private string GetNodeDir(TreeNode node, string dir = "")
	{
		if (node.Parent == null)
		{
			return dir.Remove(0, 1);
		}
		TreeNode node2 = node.Parent;
		ModCards.NodeTag nodeTag = (ModCards.NodeTag)node.Tag;
		string text = ((nodeTag.LinkedControl.IsList & (nodeTag.NodeType != ModCards.NodeType.Arr)) ? (">" + Conversions.ToString(node.Index)) : ("." + nodeTag.LinkedControl.PropName));
		return GetNodeDir(node2, text + dir);
	}

	private object GetRootNode(TreeNode node)
	{
		if (node.Parent != null)
		{
			return GetRootNode(node.Parent);
		}
		return node;
	}

	private TreeNode GoToNode(TreeNodeCollection nodes, string path)
	{
		string[] array = Strings.Split(path, ".", 2);
		string text = array[0];
		string text2 = "";
		int num = -1;
		if (text.Contains(">"))
		{
			string[] array2 = Strings.Split(text, ">");
			text = array2[0];
			num = int.Parse(array2[1], NumberStyles.Integer);
		}
		if (array.Count() > 1)
		{
			text2 = array[1];
		}
		Console.WriteLine("Accessing " + text + ((num >= 0) ? ("(" + Conversions.ToString(num) + ")") : "") + " to" + text2);
		TreeNode treeNode = ((num >= 0) ? nodes[text].Nodes[num] : nodes[text]);
		if (!string.IsNullOrEmpty(text2))
		{
			return GoToNode(treeNode.Nodes, text2);
		}
		return treeNode;
	}

	private void DisplayPrice()
	{
		if (Operators.CompareString(SaveFolder, "armor", TextCompare: false) == 0)
		{
			Price.Visible = true;
			Price.Text = "$" + ModPrice.Market_ArmorPrice(ObjEditor.Nodes).ToString("N0");
		}
		else if (Operators.CompareString(SaveFolder, "weapons", TextCompare: false) == 0)
		{
			Price.Visible = true;
			Price.Text = "$" + ModPrice.Market_WeaponPrice(ObjEditor.Nodes).ToString("N0");
		}
		else if (Operators.CompareString(SaveFolder, "weaponaddons", TextCompare: false) == 0)
		{
			Price.Visible = true;
			Price.Text = "$" + ModPrice.Market_AddonPrice(ObjEditor.Nodes).ToString("N0");
		}
		else
		{
			Price.Visible = false;
			Price.Text = "";
		}
	}

	private void EvaluateParameter(TreeNode node, ModCards.NodeTag tag)
	{
		if (ListObjects.DataSource != null)
		{
			foreach (object item in ListObjects.Items)
			{
				object objectValue = RuntimeHelpers.GetObjectValue(item);
				if (string.Equals(objectValue.ToString(),Param.Text,StringComparison.OrdinalIgnoreCase))
				{
					ListObjects.SelectedItem = RuntimeHelpers.GetObjectValue(objectValue);
					break;
				}
			}
		}
		else if (tag.NodeType == ModCards.NodeType.Val)
		{
			switch (tag.LinkedControl.PropType)
			{
			case ModCards.PropType.Color:
				try
				{
					Color color = ColorTranslator.FromHtml(Param.Text);
					if (!(color == Color.Empty))
					{
						tag.Value = ColorFromHex(color);
						node.ForeColor = color;
						node.Text = GetDisplayNode(node);
					}
					else if (tag.Value == null)
					{
						tag.Value = ColorFromHex(Color.White);
						node.ForeColor = color;
						node.Text = GetDisplayNode(node);
					}
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception ex2 = ex;
					Console.WriteLine("Color not appropriate");
					ProjectData.ClearProjectError();
				}
				break;
			case ModCards.PropType.Bool:
			case ModCards.PropType.Int:
			{
				string s2 = Param.Text;
				CultureInfo currentCulture2 = CultureInfo.CurrentCulture;
				ModCards.NodeTag nodeTag = tag;
				int result2 = Conversions.ToInteger(nodeTag.Value);
				bool flag2 = int.TryParse(s2, NumberStyles.Integer, currentCulture2, out result2);
				if (flag2)
				{
                    nodeTag.Value = result2;
					node.Text = GetDisplayNode(node);
				}
				break;
			}
			case ModCards.PropType.Float:
			{
				string s = Param.Text;
				CultureInfo currentCulture = CultureInfo.CurrentCulture;
				ModCards.NodeTag nodeTag = tag;
				double result = Conversions.ToDouble(nodeTag.Value);
				bool flag = double.TryParse(s, NumberStyles.Float, currentCulture, out result);
				if (flag)
				{
                    nodeTag.Value = result;
					node.Text = GetDisplayNode(node);
				}
				break;
			}
			case ModCards.PropType.Str:
			case ModCards.PropType.Ref:
				tag.Value = Param.Text;
				node.Text = GetDisplayNode(node);
				break;
			}
		}
		DisplayPrice();
        TrackEditorChange();
	}

	private void EvaluateSelected(TreeNode node, ModCards.NodeTag tag)
	{
		if (tag.NodeType == ModCards.NodeType.Val)
		{
			Loading = true;
			tag.Value = RuntimeHelpers.GetObjectValue(ListObjects.SelectedItem);
			node.Text = GetDisplayNode(node);
			Param.Text = tag.Value.ToString();
			node.ForeColor = ObjEditor.ForeColor;
			Loading = false;
		}
		DisplayPrice();
        TrackEditorChange();
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
					ObjEditor.SelectedNode.Nodes.Add(DefaultNode(linkedControl));
					ObjEditor.SelectedNode.ExpandAll();
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
					SelectedTag.Value = ((!Operators.ConditionalCompareObjectGreater(SelectedTag.Value, 0, TextCompare: false)) ? 1 : 0);
					ObjEditor.SelectedNode.Text = GetDisplayNode(ObjEditor.SelectedNode);
				}
				else if (linkedControl.PropType == ModCards.PropType.Color && ColorWheel.ShowDialog() != DialogResult.Cancel)
				{
					Param.Text = ColorTranslator.ToHtml(ColorWheel.Color);
				}
			}
		}
		DisplayPrice();
        TrackEditorChange();
		ObjEditor.Focus();
	}

	private void CheckNullValue(ModCards.PropertyControl control, TreeNode node)
	{
		bool flag = false;
		if (control.PropType == ModCards.PropType.Ref)
		{
			flag = Array.FindIndex((string[])control.Selection, [SpecialName] (string s) => NewLateBinding.LateGet(node.Tag, null, "Value", new object[0], null, null, null).ToString().Equals(s, StringComparison.OrdinalIgnoreCase)) < 0;
		}
		else if (control.PropType == ModCards.PropType.Ord)
		{
			flag = !Enum.GetNames(NewLateBinding.LateIndexGet(control.Selection, new object[1] { 0 }, null).GetType()).Contains(NewLateBinding.LateGet(node.Tag, null, "Value", new object[0], null, null, null).ToString());
		}
		if (flag)
		{
			node.ForeColor = Color.Salmon;
		}
	}

	private string GetDisplayNode(TreeNode node)
	{
		return ((node.Name.Length > 0) ? (node.Name + ": ") : "") + (Conversions.ToBoolean(Operators.NotObject(Operators.CompareObjectEqual(NewLateBinding.LateGet(node.Tag, null, "NodeType", new object[0], null, null, null), ModCards.NodeType.Arr, TextCompare: false))) ? node.Tag.ToString() : "");
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
		Loading = false;
	}
}
