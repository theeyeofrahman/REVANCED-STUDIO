using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using MPNDossier.My;
using Microsoft.VisualBasic.CompilerServices;
using Newtonsoft.Json.Linq;

namespace MPNDossier;

[DesignerGenerated]
public partial class FormPNDossier : Form
{
	private IContainer components;

	[AccessedThroughProperty("Tabs")]
	private TabControl _Tabs;

	[AccessedThroughProperty("PgOverview")]
	private TabPage _PgOverview;

	[AccessedThroughProperty("PgAttributes")]
	private TabPage _PgAttributes;

	[AccessedThroughProperty("CharDescription1")]
	private RichTextBox _CharDescription1;

	[AccessedThroughProperty("CharSelect")]
	private ComboBox _CharSelect;

	[AccessedThroughProperty("BtnConvert")]
	private Button _BtnConvert;

	[AccessedThroughProperty("BtnGlossary")]
	private Button _BtnGlossary;

	[AccessedThroughProperty("CharSubAtt1")]
	private GroupBox _CharSubAtt1;

	[AccessedThroughProperty("CharForce1")]
	private Label _CharForce1;

	[AccessedThroughProperty("CharStrive1")]
	private Label _CharStrive1;

	[AccessedThroughProperty("CharCritical1")]
	private Label _CharCritical1;

	[AccessedThroughProperty("CharLethal1")]
	private Label _CharLethal1;

	[AccessedThroughProperty("CharSenses1")]
	private Label _CharSenses1;

	[AccessedThroughProperty("CharAthletics1")]
	private Label _CharAthletics1;

	[AccessedThroughProperty("CharAttr1")]
	private GroupBox _CharAttr1;

	[AccessedThroughProperty("CharStrength1")]
	private Label _CharStrength1;

	[AccessedThroughProperty("CharTactics1")]
	private Label _CharTactics1;

	[AccessedThroughProperty("CharDexterity1")]
	private Label _CharDexterity1;

	[AccessedThroughProperty("CharEndurance1")]
	private Label _CharEndurance1;

	[AccessedThroughProperty("CharOver1")]
	private GroupBox _CharOver1;

	[AccessedThroughProperty("CharLevel1")]
	private Label _CharLevel1;

	[AccessedThroughProperty("CharTacScl1")]
	private Label _CharTacScl1;

	[AccessedThroughProperty("CharSizeScl1")]
	private Label _CharSizeScl1;

	[AccessedThroughProperty("CharCorpus1")]
	private Label _CharCorpus1;

	[AccessedThroughProperty("CharFaction1")]
	private Label _CharFaction1;

	[AccessedThroughProperty("CharTitle1")]
	private Label _CharTitle1;

	[AccessedThroughProperty("CharName1")]
	private Label _CharName1;

	[AccessedThroughProperty("ValSizeScl1")]
	private Label _ValSizeScl1;

	[AccessedThroughProperty("ValCorpus1")]
	private Label _ValCorpus1;

	[AccessedThroughProperty("ValLevel1")]
	private Label _ValLevel1;

	[AccessedThroughProperty("ValSenses1")]
	private Label _ValSenses1;

	[AccessedThroughProperty("ValAthletics1")]
	private Label _ValAthletics1;

	[AccessedThroughProperty("ValLethality1")]
	private Label _ValLethality1;

	[AccessedThroughProperty("ValStrive1")]
	private Label _ValStrive1;

	[AccessedThroughProperty("ValCritical1")]
	private Label _ValCritical1;

	[AccessedThroughProperty("ValForce1")]
	private Label _ValForce1;

	[AccessedThroughProperty("ValTactics1")]
	private Label _ValTactics1;

	[AccessedThroughProperty("ValDexterity1")]
	private Label _ValDexterity1;

	[AccessedThroughProperty("ValEndurance1")]
	private Label _ValEndurance1;

	[AccessedThroughProperty("ValStrength1")]
	private Label _ValStrength1;

	[AccessedThroughProperty("ValTacScl1")]
	private Label _ValTacScl1;

	[AccessedThroughProperty("PgSkills")]
	private TabPage _PgSkills;

	[AccessedThroughProperty("CharListDesc1")]
	private RichTextBox _CharListDesc1;

	[AccessedThroughProperty("CharSkills1")]
	private Label _CharSkills1;

	[AccessedThroughProperty("CharLists1")]
	private TreeView _CharLists1;

	[AccessedThroughProperty("PgDefense")]
	private TabPage _PgDefense;

	[AccessedThroughProperty("CharArmorList1")]
	private Label _CharArmorList1;

	[AccessedThroughProperty("CharListArmor1")]
	private TreeView _CharListArmor1;

	[AccessedThroughProperty("CharDisplay1")]
	private PictureBox _CharDisplay1;

	[AccessedThroughProperty("PgOffense")]
	private TabPage _PgOffense;

	[AccessedThroughProperty("CharListWeap1")]
	private TreeView _CharListWeap1;

	[AccessedThroughProperty("CharFavored1")]
	private Label _CharFavored1;

	[AccessedThroughProperty("PgPersonality")]
	private TabPage _PgPersonality;

	[AccessedThroughProperty("CharArmorDesc1")]
	private RichTextBox _CharArmorDesc1;

	[AccessedThroughProperty("CharWeaponDesc1")]
	private RichTextBox _CharWeaponDesc1;

	[AccessedThroughProperty("CharChatter1")]
	private Label _CharChatter1;

	[AccessedThroughProperty("CharLstChatter1")]
	private TreeView _CharLstChatter1;

	[AccessedThroughProperty("Label1")]
	private Label _Label1;

	[AccessedThroughProperty("CharLstAttr1")]
	private TreeView _CharLstAttr1;

	[AccessedThroughProperty("CharAttDesc1")]
	private RichTextBox _CharAttDesc1;

	private int[] Attributes;

	private int[] SubAttributes;

	private Dictionary<string, double> AttributeActions;

	internal virtual TabControl Tabs
	{
		get
		{
			return _Tabs;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			_Tabs = value;
		}
	}

	internal virtual TabPage PgOverview
	{
		get
		{
			return _PgOverview;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			_PgOverview = value;
		}
	}

	internal virtual TabPage PgAttributes
	{
		get
		{
			return _PgAttributes;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			_PgAttributes = value;
		}
	}

	internal virtual RichTextBox CharDescription1
	{
		get
		{
			return _CharDescription1;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			_CharDescription1 = value;
		}
	}

	internal virtual ComboBox CharSelect
	{
		get
		{
			return _CharSelect;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			EventHandler value2 = CharSelect_SelectedIndexChanged;
			if (_CharSelect != null)
			{
				_CharSelect.SelectedIndexChanged -= value2;
			}
			_CharSelect = value;
			if (_CharSelect != null)
			{
				_CharSelect.SelectedIndexChanged += value2;
			}
		}
	}

	internal virtual Button BtnConvert
	{
		get
		{
			return _BtnConvert;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			EventHandler value2 = BtnConvert_Click;
			if (_BtnConvert != null)
			{
				_BtnConvert.Click -= value2;
			}
			_BtnConvert = value;
			if (_BtnConvert != null)
			{
				_BtnConvert.Click += value2;
			}
		}
	}

	internal virtual Button BtnGlossary
	{
		get
		{
			return _BtnGlossary;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			EventHandler value2 = BtnGlossary_Click;
			if (_BtnGlossary != null)
			{
				_BtnGlossary.Click -= value2;
			}
			_BtnGlossary = value;
			if (_BtnGlossary != null)
			{
				_BtnGlossary.Click += value2;
			}
		}
	}

	internal virtual GroupBox CharSubAtt1
	{
		get
		{
			return _CharSubAtt1;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			_CharSubAtt1 = value;
		}
	}

	internal virtual Label CharForce1
	{
		get
		{
			return _CharForce1;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			_CharForce1 = value;
		}
	}

	internal virtual Label CharStrive1
	{
		get
		{
			return _CharStrive1;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			_CharStrive1 = value;
		}
	}

	internal virtual Label CharCritical1
	{
		get
		{
			return _CharCritical1;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			_CharCritical1 = value;
		}
	}

	internal virtual Label CharLethal1
	{
		get
		{
			return _CharLethal1;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			_CharLethal1 = value;
		}
	}

	internal virtual Label CharSenses1
	{
		get
		{
			return _CharSenses1;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			_CharSenses1 = value;
		}
	}

	internal virtual Label CharAthletics1
	{
		get
		{
			return _CharAthletics1;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			_CharAthletics1 = value;
		}
	}

	internal virtual GroupBox CharAttr1
	{
		get
		{
			return _CharAttr1;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			_CharAttr1 = value;
		}
	}

	internal virtual Label CharStrength1
	{
		get
		{
			return _CharStrength1;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			_CharStrength1 = value;
		}
	}

	internal virtual Label CharTactics1
	{
		get
		{
			return _CharTactics1;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			_CharTactics1 = value;
		}
	}

	internal virtual Label CharDexterity1
	{
		get
		{
			return _CharDexterity1;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			_CharDexterity1 = value;
		}
	}

	internal virtual Label CharEndurance1
	{
		get
		{
			return _CharEndurance1;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			_CharEndurance1 = value;
		}
	}

	internal virtual GroupBox CharOver1
	{
		get
		{
			return _CharOver1;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			_CharOver1 = value;
		}
	}

	internal virtual Label CharLevel1
	{
		get
		{
			return _CharLevel1;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			_CharLevel1 = value;
		}
	}

	internal virtual Label CharTacScl1
	{
		get
		{
			return _CharTacScl1;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			_CharTacScl1 = value;
		}
	}

	internal virtual Label CharSizeScl1
	{
		get
		{
			return _CharSizeScl1;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			_CharSizeScl1 = value;
		}
	}

	internal virtual Label CharCorpus1
	{
		get
		{
			return _CharCorpus1;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			_CharCorpus1 = value;
		}
	}

	internal virtual Label CharFaction1
	{
		get
		{
			return _CharFaction1;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			_CharFaction1 = value;
		}
	}

	internal virtual Label CharTitle1
	{
		get
		{
			return _CharTitle1;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			_CharTitle1 = value;
		}
	}

	internal virtual Label CharName1
	{
		get
		{
			return _CharName1;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			_CharName1 = value;
		}
	}

	internal virtual Label ValSizeScl1
	{
		get
		{
			return _ValSizeScl1;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			_ValSizeScl1 = value;
		}
	}

	internal virtual Label ValCorpus1
	{
		get
		{
			return _ValCorpus1;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			_ValCorpus1 = value;
		}
	}

	internal virtual Label ValLevel1
	{
		get
		{
			return _ValLevel1;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			_ValLevel1 = value;
		}
	}

	internal virtual Label ValSenses1
	{
		get
		{
			return _ValSenses1;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			_ValSenses1 = value;
		}
	}

	internal virtual Label ValAthletics1
	{
		get
		{
			return _ValAthletics1;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			_ValAthletics1 = value;
		}
	}

	internal virtual Label ValLethality1
	{
		get
		{
			return _ValLethality1;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			_ValLethality1 = value;
		}
	}

	internal virtual Label ValStrive1
	{
		get
		{
			return _ValStrive1;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			_ValStrive1 = value;
		}
	}

	internal virtual Label ValCritical1
	{
		get
		{
			return _ValCritical1;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			_ValCritical1 = value;
		}
	}

	internal virtual Label ValForce1
	{
		get
		{
			return _ValForce1;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			_ValForce1 = value;
		}
	}

	internal virtual Label ValTactics1
	{
		get
		{
			return _ValTactics1;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			_ValTactics1 = value;
		}
	}

	internal virtual Label ValDexterity1
	{
		get
		{
			return _ValDexterity1;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			_ValDexterity1 = value;
		}
	}

	internal virtual Label ValEndurance1
	{
		get
		{
			return _ValEndurance1;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			_ValEndurance1 = value;
		}
	}

	internal virtual Label ValStrength1
	{
		get
		{
			return _ValStrength1;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			_ValStrength1 = value;
		}
	}

	internal virtual Label ValTacScl1
	{
		get
		{
			return _ValTacScl1;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			_ValTacScl1 = value;
		}
	}

	internal virtual TabPage PgSkills
	{
		get
		{
			return _PgSkills;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			_PgSkills = value;
		}
	}

	internal virtual RichTextBox CharListDesc1
	{
		get
		{
			return _CharListDesc1;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			_CharListDesc1 = value;
		}
	}

	internal virtual Label CharSkills1
	{
		get
		{
			return _CharSkills1;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			_CharSkills1 = value;
		}
	}

	internal virtual TreeView CharLists1
	{
		get
		{
			return _CharLists1;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			TreeViewEventHandler value2 = CharLists1_AfterSelect;
			if (_CharLists1 != null)
			{
				_CharLists1.AfterSelect -= value2;
			}
			_CharLists1 = value;
			if (_CharLists1 != null)
			{
				_CharLists1.AfterSelect += value2;
			}
		}
	}

	internal virtual TabPage PgDefense
	{
		get
		{
			return _PgDefense;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			_PgDefense = value;
		}
	}

	internal virtual Label CharArmorList1
	{
		get
		{
			return _CharArmorList1;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			_CharArmorList1 = value;
		}
	}

	internal virtual TreeView CharListArmor1
	{
		get
		{
			return _CharListArmor1;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			TreeViewEventHandler value2 = CharListArmor1_AfterSelect;
			if (_CharListArmor1 != null)
			{
				_CharListArmor1.AfterSelect -= value2;
			}
			_CharListArmor1 = value;
			if (_CharListArmor1 != null)
			{
				_CharListArmor1.AfterSelect += value2;
			}
		}
	}

	internal virtual PictureBox CharDisplay1
	{
		get
		{
			return _CharDisplay1;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			_CharDisplay1 = value;
		}
	}

	internal virtual TabPage PgOffense
	{
		get
		{
			return _PgOffense;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			_PgOffense = value;
		}
	}

	internal virtual TreeView CharListWeap1
	{
		get
		{
			return _CharListWeap1;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			TreeViewEventHandler value2 = CharListWeap1_AfterSelect;
			if (_CharListWeap1 != null)
			{
				_CharListWeap1.AfterSelect -= value2;
			}
			_CharListWeap1 = value;
			if (_CharListWeap1 != null)
			{
				_CharListWeap1.AfterSelect += value2;
			}
		}
	}

	internal virtual Label CharFavored1
	{
		get
		{
			return _CharFavored1;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			_CharFavored1 = value;
		}
	}

	internal virtual TabPage PgPersonality
	{
		get
		{
			return _PgPersonality;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			_PgPersonality = value;
		}
	}

	internal virtual RichTextBox CharArmorDesc1
	{
		get
		{
			return _CharArmorDesc1;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			_CharArmorDesc1 = value;
		}
	}

	internal virtual RichTextBox CharWeaponDesc1
	{
		get
		{
			return _CharWeaponDesc1;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			_CharWeaponDesc1 = value;
		}
	}

	internal virtual Label CharChatter1
	{
		get
		{
			return _CharChatter1;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			_CharChatter1 = value;
		}
	}

	internal virtual TreeView CharLstChatter1
	{
		get
		{
			return _CharLstChatter1;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			_CharLstChatter1 = value;
		}
	}

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

	internal virtual TreeView CharLstAttr1
	{
		get
		{
			return _CharLstAttr1;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			TreeViewEventHandler value2 = CharLstAttr1_AfterSelect;
			if (_CharLstAttr1 != null)
			{
				_CharLstAttr1.AfterSelect -= value2;
			}
			_CharLstAttr1 = value;
			if (_CharLstAttr1 != null)
			{
				_CharLstAttr1.AfterSelect += value2;
			}
		}
	}

	internal virtual RichTextBox CharAttDesc1
	{
		get
		{
			return _CharAttDesc1;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			_CharAttDesc1 = value;
		}
	}

	public FormPNDossier()
	{
		FormClosing += FormPNDossier_Close;
		Load += FormPNDossier_Load;
		Attributes = new int[5];
		SubAttributes = new int[7];
		AttributeActions = new Dictionary<string, double>();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MPNDossier.FormPNDossier));
		this.Tabs = new DossierTabs();
		this.PgOverview = new System.Windows.Forms.TabPage();
		this.CharFaction1 = new System.Windows.Forms.Label();
		this.CharTitle1 = new System.Windows.Forms.Label();
		this.CharName1 = new System.Windows.Forms.Label();
		this.CharSubAtt1 = new DossierGroupBox();
		this.ValSenses1 = new System.Windows.Forms.Label();
		this.ValAthletics1 = new System.Windows.Forms.Label();
		this.ValLethality1 = new System.Windows.Forms.Label();
		this.ValStrive1 = new System.Windows.Forms.Label();
		this.ValCritical1 = new System.Windows.Forms.Label();
		this.ValForce1 = new System.Windows.Forms.Label();
		this.CharForce1 = new System.Windows.Forms.Label();
		this.CharStrive1 = new System.Windows.Forms.Label();
		this.CharCritical1 = new System.Windows.Forms.Label();
		this.CharLethal1 = new System.Windows.Forms.Label();
		this.CharSenses1 = new System.Windows.Forms.Label();
		this.CharAthletics1 = new System.Windows.Forms.Label();
		this.CharAttr1 = new DossierGroupBox();
		this.ValTactics1 = new System.Windows.Forms.Label();
		this.ValDexterity1 = new System.Windows.Forms.Label();
		this.ValEndurance1 = new System.Windows.Forms.Label();
		this.ValStrength1 = new System.Windows.Forms.Label();
		this.CharStrength1 = new System.Windows.Forms.Label();
		this.CharTactics1 = new System.Windows.Forms.Label();
		this.CharDexterity1 = new System.Windows.Forms.Label();
		this.CharEndurance1 = new System.Windows.Forms.Label();
		this.CharOver1 = new DossierGroupBox();
		this.ValTacScl1 = new System.Windows.Forms.Label();
		this.ValSizeScl1 = new System.Windows.Forms.Label();
		this.ValCorpus1 = new System.Windows.Forms.Label();
		this.ValLevel1 = new System.Windows.Forms.Label();
		this.CharLevel1 = new System.Windows.Forms.Label();
		this.CharTacScl1 = new System.Windows.Forms.Label();
		this.CharSizeScl1 = new System.Windows.Forms.Label();
		this.CharCorpus1 = new System.Windows.Forms.Label();
		this.CharDescription1 = new System.Windows.Forms.RichTextBox();
		this.PgAttributes = new System.Windows.Forms.TabPage();
		this.CharAttDesc1 = new System.Windows.Forms.RichTextBox();
		this.Label1 = new System.Windows.Forms.Label();
		this.CharLstAttr1 = new System.Windows.Forms.TreeView();
		this.PgPersonality = new System.Windows.Forms.TabPage();
		this.CharChatter1 = new System.Windows.Forms.Label();
		this.CharLstChatter1 = new System.Windows.Forms.TreeView();
		this.PgDefense = new System.Windows.Forms.TabPage();
		this.CharArmorDesc1 = new System.Windows.Forms.RichTextBox();
		this.CharArmorList1 = new System.Windows.Forms.Label();
		this.CharListArmor1 = new System.Windows.Forms.TreeView();
		this.PgOffense = new System.Windows.Forms.TabPage();
		this.CharWeaponDesc1 = new System.Windows.Forms.RichTextBox();
		this.CharListWeap1 = new System.Windows.Forms.TreeView();
		this.CharFavored1 = new System.Windows.Forms.Label();
		this.PgSkills = new System.Windows.Forms.TabPage();
		this.CharLists1 = new System.Windows.Forms.TreeView();
		this.CharListDesc1 = new System.Windows.Forms.RichTextBox();
		this.CharSkills1 = new System.Windows.Forms.Label();
		this.CharSelect = new System.Windows.Forms.ComboBox();
		this.BtnConvert = new DossierButton();
		this.BtnGlossary = new DossierButton();
		this.CharDisplay1 = new System.Windows.Forms.PictureBox();
		this.Tabs.SuspendLayout();
		this.PgOverview.SuspendLayout();
		this.CharSubAtt1.SuspendLayout();
		this.CharAttr1.SuspendLayout();
		this.CharOver1.SuspendLayout();
		this.PgAttributes.SuspendLayout();
		this.PgPersonality.SuspendLayout();
		this.PgDefense.SuspendLayout();
		this.PgOffense.SuspendLayout();
		this.PgSkills.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.CharDisplay1).BeginInit();
		this.SuspendLayout();
		this.Tabs.Controls.Add(this.PgOverview);
		this.Tabs.Controls.Add(this.PgAttributes);
		this.Tabs.Controls.Add(this.PgPersonality);
		this.Tabs.Controls.Add(this.PgDefense);
		this.Tabs.Controls.Add(this.PgOffense);
		this.Tabs.Controls.Add(this.PgSkills);
		this.Tabs.Font = new System.Drawing.Font("Impact", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		System.Windows.Forms.TabControl tabs = this.Tabs;
		System.Drawing.Size itemSize = new System.Drawing.Size(62, 18);
		tabs.ItemSize = itemSize;
		System.Windows.Forms.TabControl tabs2 = this.Tabs;
		System.Drawing.Point location = new System.Drawing.Point(418, 44);
		tabs2.Location = location;
		this.Tabs.Name = "Tabs";
		System.Windows.Forms.TabControl tabs3 = this.Tabs;
		location = new System.Drawing.Point(16, 3);
		tabs3.Padding = location;
		this.Tabs.SelectedIndex = 0;
		System.Windows.Forms.TabControl tabs4 = this.Tabs;
		itemSize = new System.Drawing.Size(500, 500);
		tabs4.Size = itemSize;
		this.Tabs.TabIndex = 0;
		this.PgOverview.BackColor = System.Drawing.Color.FromArgb(64, 64, 64);
		this.PgOverview.Controls.Add(this.CharFaction1);
		this.PgOverview.Controls.Add(this.CharTitle1);
		this.PgOverview.Controls.Add(this.CharName1);
		this.PgOverview.Controls.Add(this.CharSubAtt1);
		this.PgOverview.Controls.Add(this.CharAttr1);
		this.PgOverview.Controls.Add(this.CharOver1);
		this.PgOverview.Controls.Add(this.CharDescription1);
		this.PgOverview.ForeColor = System.Drawing.SystemColors.Control;
		System.Windows.Forms.TabPage pgOverview = this.PgOverview;
		location = new System.Drawing.Point(4, 22);
		pgOverview.Location = location;
		this.PgOverview.Name = "PgOverview";
		System.Windows.Forms.TabPage pgOverview2 = this.PgOverview;
		System.Windows.Forms.Padding padding = new System.Windows.Forms.Padding(3);
		pgOverview2.Padding = padding;
		System.Windows.Forms.TabPage pgOverview3 = this.PgOverview;
		itemSize = new System.Drawing.Size(492, 474);
		pgOverview3.Size = itemSize;
		this.PgOverview.TabIndex = 0;
		this.PgOverview.Text = "Overview";
		this.CharFaction1.Font = new System.Drawing.Font("Impact", 15.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.CharFaction1.ForeColor = System.Drawing.SystemColors.ControlLight;
		System.Windows.Forms.Label charFaction = this.CharFaction1;
		location = new System.Drawing.Point(11, 68);
		charFaction.Location = location;
		this.CharFaction1.Name = "CharFaction1";
		System.Windows.Forms.Label charFaction2 = this.CharFaction1;
		itemSize = new System.Drawing.Size(227, 26);
		charFaction2.Size = itemSize;
		this.CharFaction1.TabIndex = 7;
		this.CharFaction1.Text = "[Org]";
		this.CharTitle1.BackColor = System.Drawing.Color.Transparent;
		this.CharTitle1.Font = new System.Drawing.Font("Impact", 15.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.CharTitle1.ForeColor = System.Drawing.Color.DarkSalmon;
		System.Windows.Forms.Label charTitle = this.CharTitle1;
		location = new System.Drawing.Point(11, 42);
		charTitle.Location = location;
		this.CharTitle1.Name = "CharTitle1";
		System.Windows.Forms.Label charTitle2 = this.CharTitle1;
		itemSize = new System.Drawing.Size(227, 26);
		charTitle2.Size = itemSize;
		this.CharTitle1.TabIndex = 9;
		this.CharTitle1.Text = "[Title]";
		this.CharName1.BackColor = System.Drawing.Color.Transparent;
		this.CharName1.Font = new System.Drawing.Font("Impact", 24f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.CharName1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
		System.Windows.Forms.Label charName = this.CharName1;
		location = new System.Drawing.Point(9, 3);
		charName.Location = location;
		this.CharName1.Name = "CharName1";
		System.Windows.Forms.Label charName2 = this.CharName1;
		itemSize = new System.Drawing.Size(448, 39);
		charName2.Size = itemSize;
		this.CharName1.TabIndex = 8;
		this.CharName1.Text = "Full Name";
		this.CharSubAtt1.Controls.Add(this.ValSenses1);
		this.CharSubAtt1.Controls.Add(this.ValAthletics1);
		this.CharSubAtt1.Controls.Add(this.ValLethality1);
		this.CharSubAtt1.Controls.Add(this.ValStrive1);
		this.CharSubAtt1.Controls.Add(this.ValCritical1);
		this.CharSubAtt1.Controls.Add(this.ValForce1);
		this.CharSubAtt1.Controls.Add(this.CharForce1);
		this.CharSubAtt1.Controls.Add(this.CharStrive1);
		this.CharSubAtt1.Controls.Add(this.CharCritical1);
		this.CharSubAtt1.Controls.Add(this.CharLethal1);
		this.CharSubAtt1.Controls.Add(this.CharSenses1);
		this.CharSubAtt1.Controls.Add(this.CharAthletics1);
		this.CharSubAtt1.Font = new System.Drawing.Font("Impact", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.CharSubAtt1.ForeColor = System.Drawing.Color.DarkSalmon;
		System.Windows.Forms.GroupBox charSubAtt = this.CharSubAtt1;
		location = new System.Drawing.Point(6, 245);
		charSubAtt.Location = location;
		this.CharSubAtt1.Name = "CharSubAtt1";
		System.Windows.Forms.GroupBox charSubAtt2 = this.CharSubAtt1;
		itemSize = new System.Drawing.Size(476, 105);
		charSubAtt2.Size = itemSize;
		this.CharSubAtt1.TabIndex = 6;
		this.CharSubAtt1.TabStop = false;
		this.CharSubAtt1.Text = "Sub-Attributes";
		this.ValSenses1.Font = new System.Drawing.Font("Impact", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.ValSenses1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
		System.Windows.Forms.Label valSenses = this.ValSenses1;
		location = new System.Drawing.Point(409, 72);
		valSenses.Location = location;
		this.ValSenses1.Name = "ValSenses1";
		System.Windows.Forms.Label valSenses2 = this.ValSenses1;
		itemSize = new System.Drawing.Size(60, 23);
		valSenses2.Size = itemSize;
		this.ValSenses1.TabIndex = 21;
		this.ValSenses1.Text = "0";
		this.ValSenses1.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.ValAthletics1.Font = new System.Drawing.Font("Impact", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.ValAthletics1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
		System.Windows.Forms.Label valAthletics = this.ValAthletics1;
		location = new System.Drawing.Point(409, 44);
		valAthletics.Location = location;
		this.ValAthletics1.Name = "ValAthletics1";
		System.Windows.Forms.Label valAthletics2 = this.ValAthletics1;
		itemSize = new System.Drawing.Size(60, 23);
		valAthletics2.Size = itemSize;
		this.ValAthletics1.TabIndex = 20;
		this.ValAthletics1.Text = "0";
		this.ValAthletics1.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.ValLethality1.Font = new System.Drawing.Font("Impact", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.ValLethality1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
		System.Windows.Forms.Label valLethality = this.ValLethality1;
		location = new System.Drawing.Point(409, 16);
		valLethality.Location = location;
		this.ValLethality1.Name = "ValLethality1";
		System.Windows.Forms.Label valLethality2 = this.ValLethality1;
		itemSize = new System.Drawing.Size(60, 23);
		valLethality2.Size = itemSize;
		this.ValLethality1.TabIndex = 19;
		this.ValLethality1.Text = "0";
		this.ValLethality1.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.ValStrive1.Font = new System.Drawing.Font("Impact", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.ValStrive1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
		System.Windows.Forms.Label valStrive = this.ValStrive1;
		location = new System.Drawing.Point(165, 72);
		valStrive.Location = location;
		this.ValStrive1.Name = "ValStrive1";
		System.Windows.Forms.Label valStrive2 = this.ValStrive1;
		itemSize = new System.Drawing.Size(60, 23);
		valStrive2.Size = itemSize;
		this.ValStrive1.TabIndex = 18;
		this.ValStrive1.Text = "0";
		this.ValStrive1.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.ValCritical1.Font = new System.Drawing.Font("Impact", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.ValCritical1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
		System.Windows.Forms.Label valCritical = this.ValCritical1;
		location = new System.Drawing.Point(165, 44);
		valCritical.Location = location;
		this.ValCritical1.Name = "ValCritical1";
		System.Windows.Forms.Label valCritical2 = this.ValCritical1;
		itemSize = new System.Drawing.Size(60, 23);
		valCritical2.Size = itemSize;
		this.ValCritical1.TabIndex = 17;
		this.ValCritical1.Text = "0";
		this.ValCritical1.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.ValForce1.Font = new System.Drawing.Font("Impact", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.ValForce1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
		System.Windows.Forms.Label valForce = this.ValForce1;
		location = new System.Drawing.Point(165, 16);
		valForce.Location = location;
		this.ValForce1.Name = "ValForce1";
		System.Windows.Forms.Label valForce2 = this.ValForce1;
		itemSize = new System.Drawing.Size(60, 23);
		valForce2.Size = itemSize;
		this.ValForce1.TabIndex = 16;
		this.ValForce1.Text = "0";
		this.ValForce1.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.CharForce1.AutoSize = true;
		this.CharForce1.Font = new System.Drawing.Font("Impact", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.CharForce1.ForeColor = System.Drawing.SystemColors.ControlLight;
		System.Windows.Forms.Label charForce = this.CharForce1;
		location = new System.Drawing.Point(6, 16);
		charForce.Location = location;
		this.CharForce1.Name = "CharForce1";
		System.Windows.Forms.Label charForce2 = this.CharForce1;
		itemSize = new System.Drawing.Size(58, 23);
		charForce2.Size = itemSize;
		this.CharForce1.TabIndex = 14;
		this.CharForce1.Text = "Force:";
		this.CharStrive1.AutoSize = true;
		this.CharStrive1.Font = new System.Drawing.Font("Impact", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.CharStrive1.ForeColor = System.Drawing.SystemColors.ControlLight;
		System.Windows.Forms.Label charStrive = this.CharStrive1;
		location = new System.Drawing.Point(6, 73);
		charStrive.Location = location;
		this.CharStrive1.Name = "CharStrive1";
		System.Windows.Forms.Label charStrive2 = this.CharStrive1;
		itemSize = new System.Drawing.Size(59, 23);
		charStrive2.Size = itemSize;
		this.CharStrive1.TabIndex = 12;
		this.CharStrive1.Text = "Strive:";
		this.CharCritical1.AutoSize = true;
		this.CharCritical1.Font = new System.Drawing.Font("Impact", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.CharCritical1.ForeColor = System.Drawing.SystemColors.ControlLight;
		System.Windows.Forms.Label charCritical = this.CharCritical1;
		location = new System.Drawing.Point(6, 44);
		charCritical.Location = location;
		this.CharCritical1.Name = "CharCritical1";
		System.Windows.Forms.Label charCritical2 = this.CharCritical1;
		itemSize = new System.Drawing.Size(72, 23);
		charCritical2.Size = itemSize;
		this.CharCritical1.TabIndex = 10;
		this.CharCritical1.Text = "Critical:";
		this.CharLethal1.AutoSize = true;
		this.CharLethal1.Font = new System.Drawing.Font("Impact", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.CharLethal1.ForeColor = System.Drawing.SystemColors.ControlLight;
		System.Windows.Forms.Label charLethal = this.CharLethal1;
		location = new System.Drawing.Point(249, 16);
		charLethal.Location = location;
		this.CharLethal1.Name = "CharLethal1";
		System.Windows.Forms.Label charLethal2 = this.CharLethal1;
		itemSize = new System.Drawing.Size(82, 23);
		charLethal2.Size = itemSize;
		this.CharLethal1.TabIndex = 15;
		this.CharLethal1.Text = "Lethality:";
		this.CharSenses1.AutoSize = true;
		this.CharSenses1.Font = new System.Drawing.Font("Impact", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.CharSenses1.ForeColor = System.Drawing.SystemColors.ControlLight;
		System.Windows.Forms.Label charSenses = this.CharSenses1;
		location = new System.Drawing.Point(249, 73);
		charSenses.Location = location;
		this.CharSenses1.Name = "CharSenses1";
		System.Windows.Forms.Label charSenses2 = this.CharSenses1;
		itemSize = new System.Drawing.Size(72, 23);
		charSenses2.Size = itemSize;
		this.CharSenses1.TabIndex = 13;
		this.CharSenses1.Text = "Senses:";
		this.CharAthletics1.AutoSize = true;
		this.CharAthletics1.Font = new System.Drawing.Font("Impact", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.CharAthletics1.ForeColor = System.Drawing.SystemColors.ControlLight;
		System.Windows.Forms.Label charAthletics = this.CharAthletics1;
		location = new System.Drawing.Point(249, 44);
		charAthletics.Location = location;
		this.CharAthletics1.Name = "CharAthletics1";
		System.Windows.Forms.Label charAthletics2 = this.CharAthletics1;
		itemSize = new System.Drawing.Size(83, 23);
		charAthletics2.Size = itemSize;
		this.CharAthletics1.TabIndex = 11;
		this.CharAthletics1.Text = "Athletics:";
		this.CharAttr1.Controls.Add(this.ValTactics1);
		this.CharAttr1.Controls.Add(this.ValDexterity1);
		this.CharAttr1.Controls.Add(this.ValEndurance1);
		this.CharAttr1.Controls.Add(this.ValStrength1);
		this.CharAttr1.Controls.Add(this.CharStrength1);
		this.CharAttr1.Controls.Add(this.CharTactics1);
		this.CharAttr1.Controls.Add(this.CharDexterity1);
		this.CharAttr1.Controls.Add(this.CharEndurance1);
		this.CharAttr1.Font = new System.Drawing.Font("Impact", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.CharAttr1.ForeColor = System.Drawing.Color.DarkSalmon;
		System.Windows.Forms.GroupBox charAttr = this.CharAttr1;
		location = new System.Drawing.Point(250, 108);
		charAttr.Location = location;
		this.CharAttr1.Name = "CharAttr1";
		System.Windows.Forms.GroupBox charAttr2 = this.CharAttr1;
		itemSize = new System.Drawing.Size(232, 130);
		charAttr2.Size = itemSize;
		this.CharAttr1.TabIndex = 5;
		this.CharAttr1.TabStop = false;
		this.CharAttr1.Text = "Attributes";
		this.ValTactics1.Font = new System.Drawing.Font("Impact", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.ValTactics1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
		System.Windows.Forms.Label valTactics = this.ValTactics1;
		location = new System.Drawing.Point(165, 100);
		valTactics.Location = location;
		this.ValTactics1.Name = "ValTactics1";
		System.Windows.Forms.Label valTactics2 = this.ValTactics1;
		itemSize = new System.Drawing.Size(60, 23);
		valTactics2.Size = itemSize;
		this.ValTactics1.TabIndex = 15;
		this.ValTactics1.Text = "0";
		this.ValTactics1.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.ValDexterity1.Font = new System.Drawing.Font("Impact", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.ValDexterity1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
		System.Windows.Forms.Label valDexterity = this.ValDexterity1;
		location = new System.Drawing.Point(165, 72);
		valDexterity.Location = location;
		this.ValDexterity1.Name = "ValDexterity1";
		System.Windows.Forms.Label valDexterity2 = this.ValDexterity1;
		itemSize = new System.Drawing.Size(60, 23);
		valDexterity2.Size = itemSize;
		this.ValDexterity1.TabIndex = 14;
		this.ValDexterity1.Text = "0";
		this.ValDexterity1.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.ValEndurance1.Font = new System.Drawing.Font("Impact", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.ValEndurance1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
		System.Windows.Forms.Label valEndurance = this.ValEndurance1;
		location = new System.Drawing.Point(165, 44);
		valEndurance.Location = location;
		this.ValEndurance1.Name = "ValEndurance1";
		System.Windows.Forms.Label valEndurance2 = this.ValEndurance1;
		itemSize = new System.Drawing.Size(60, 23);
		valEndurance2.Size = itemSize;
		this.ValEndurance1.TabIndex = 13;
		this.ValEndurance1.Text = "0";
		this.ValEndurance1.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.ValStrength1.Font = new System.Drawing.Font("Impact", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.ValStrength1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
		System.Windows.Forms.Label valStrength = this.ValStrength1;
		location = new System.Drawing.Point(165, 16);
		valStrength.Location = location;
		this.ValStrength1.Name = "ValStrength1";
		System.Windows.Forms.Label valStrength2 = this.ValStrength1;
		itemSize = new System.Drawing.Size(60, 23);
		valStrength2.Size = itemSize;
		this.ValStrength1.TabIndex = 12;
		this.ValStrength1.Text = "0";
		this.ValStrength1.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.CharStrength1.AutoSize = true;
		this.CharStrength1.Font = new System.Drawing.Font("Impact", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.CharStrength1.ForeColor = System.Drawing.SystemColors.ControlLight;
		System.Windows.Forms.Label charStrength = this.CharStrength1;
		location = new System.Drawing.Point(6, 16);
		charStrength.Location = location;
		this.CharStrength1.Name = "CharStrength1";
		System.Windows.Forms.Label charStrength2 = this.CharStrength1;
		itemSize = new System.Drawing.Size(83, 23);
		charStrength2.Size = itemSize;
		this.CharStrength1.TabIndex = 7;
		this.CharStrength1.Text = "Strength:";
		this.CharTactics1.AutoSize = true;
		this.CharTactics1.Font = new System.Drawing.Font("Impact", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.CharTactics1.ForeColor = System.Drawing.SystemColors.ControlLight;
		System.Windows.Forms.Label charTactics = this.CharTactics1;
		location = new System.Drawing.Point(6, 100);
		charTactics.Location = location;
		this.CharTactics1.Name = "CharTactics1";
		System.Windows.Forms.Label charTactics2 = this.CharTactics1;
		itemSize = new System.Drawing.Size(70, 23);
		charTactics2.Size = itemSize;
		this.CharTactics1.TabIndex = 6;
		this.CharTactics1.Text = "Tactics:";
		this.CharDexterity1.AutoSize = true;
		this.CharDexterity1.Font = new System.Drawing.Font("Impact", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.CharDexterity1.ForeColor = System.Drawing.SystemColors.ControlLight;
		System.Windows.Forms.Label charDexterity = this.CharDexterity1;
		location = new System.Drawing.Point(6, 72);
		charDexterity.Location = location;
		this.CharDexterity1.Name = "CharDexterity1";
		System.Windows.Forms.Label charDexterity2 = this.CharDexterity1;
		itemSize = new System.Drawing.Size(85, 23);
		charDexterity2.Size = itemSize;
		this.CharDexterity1.TabIndex = 5;
		this.CharDexterity1.Text = "Dexterity:";
		this.CharEndurance1.AutoSize = true;
		this.CharEndurance1.Font = new System.Drawing.Font("Impact", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.CharEndurance1.ForeColor = System.Drawing.SystemColors.ControlLight;
		System.Windows.Forms.Label charEndurance = this.CharEndurance1;
		location = new System.Drawing.Point(6, 44);
		charEndurance.Location = location;
		this.CharEndurance1.Name = "CharEndurance1";
		System.Windows.Forms.Label charEndurance2 = this.CharEndurance1;
		itemSize = new System.Drawing.Size(98, 23);
		charEndurance2.Size = itemSize;
		this.CharEndurance1.TabIndex = 4;
		this.CharEndurance1.Text = "Endurance:";
		this.CharOver1.Controls.Add(this.ValTacScl1);
		this.CharOver1.Controls.Add(this.ValSizeScl1);
		this.CharOver1.Controls.Add(this.ValCorpus1);
		this.CharOver1.Controls.Add(this.ValLevel1);
		this.CharOver1.Controls.Add(this.CharLevel1);
		this.CharOver1.Controls.Add(this.CharTacScl1);
		this.CharOver1.Controls.Add(this.CharSizeScl1);
		this.CharOver1.Controls.Add(this.CharCorpus1);
		this.CharOver1.Font = new System.Drawing.Font("Impact", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.CharOver1.ForeColor = System.Drawing.Color.DarkSalmon;
		System.Windows.Forms.GroupBox charOver = this.CharOver1;
		location = new System.Drawing.Point(6, 108);
		charOver.Location = location;
		this.CharOver1.Name = "CharOver1";
		System.Windows.Forms.GroupBox charOver2 = this.CharOver1;
		itemSize = new System.Drawing.Size(232, 130);
		charOver2.Size = itemSize;
		this.CharOver1.TabIndex = 4;
		this.CharOver1.TabStop = false;
		this.CharOver1.Text = "Modifiers";
		this.ValTacScl1.Font = new System.Drawing.Font("Impact", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.ValTacScl1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
		System.Windows.Forms.Label valTacScl = this.ValTacScl1;
		location = new System.Drawing.Point(165, 100);
		valTacScl.Location = location;
		this.ValTacScl1.Name = "ValTacScl1";
		System.Windows.Forms.Label valTacScl2 = this.ValTacScl1;
		itemSize = new System.Drawing.Size(60, 23);
		valTacScl2.Size = itemSize;
		this.ValTacScl1.TabIndex = 11;
		this.ValTacScl1.Text = "0";
		this.ValTacScl1.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.ValSizeScl1.Font = new System.Drawing.Font("Impact", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.ValSizeScl1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
		System.Windows.Forms.Label valSizeScl = this.ValSizeScl1;
		location = new System.Drawing.Point(165, 72);
		valSizeScl.Location = location;
		this.ValSizeScl1.Name = "ValSizeScl1";
		System.Windows.Forms.Label valSizeScl2 = this.ValSizeScl1;
		itemSize = new System.Drawing.Size(60, 23);
		valSizeScl2.Size = itemSize;
		this.ValSizeScl1.TabIndex = 10;
		this.ValSizeScl1.Text = "0";
		this.ValSizeScl1.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.ValCorpus1.Font = new System.Drawing.Font("Impact", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.ValCorpus1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
		System.Windows.Forms.Label valCorpus = this.ValCorpus1;
		location = new System.Drawing.Point(165, 44);
		valCorpus.Location = location;
		this.ValCorpus1.Name = "ValCorpus1";
		System.Windows.Forms.Label valCorpus2 = this.ValCorpus1;
		itemSize = new System.Drawing.Size(60, 23);
		valCorpus2.Size = itemSize;
		this.ValCorpus1.TabIndex = 9;
		this.ValCorpus1.Text = "0";
		this.ValCorpus1.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.ValLevel1.Font = new System.Drawing.Font("Impact", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.ValLevel1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
		System.Windows.Forms.Label valLevel = this.ValLevel1;
		location = new System.Drawing.Point(165, 16);
		valLevel.Location = location;
		this.ValLevel1.Name = "ValLevel1";
		System.Windows.Forms.Label valLevel2 = this.ValLevel1;
		itemSize = new System.Drawing.Size(60, 23);
		valLevel2.Size = itemSize;
		this.ValLevel1.TabIndex = 8;
		this.ValLevel1.Text = "0";
		this.ValLevel1.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.CharLevel1.AutoSize = true;
		this.CharLevel1.Font = new System.Drawing.Font("Impact", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.CharLevel1.ForeColor = System.Drawing.SystemColors.ControlLight;
		System.Windows.Forms.Label charLevel = this.CharLevel1;
		location = new System.Drawing.Point(6, 16);
		charLevel.Location = location;
		this.CharLevel1.Name = "CharLevel1";
		System.Windows.Forms.Label charLevel2 = this.CharLevel1;
		itemSize = new System.Drawing.Size(53, 23);
		charLevel2.Size = itemSize;
		this.CharLevel1.TabIndex = 7;
		this.CharLevel1.Text = "Level:";
		this.CharTacScl1.AutoSize = true;
		this.CharTacScl1.Font = new System.Drawing.Font("Impact", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.CharTacScl1.ForeColor = System.Drawing.SystemColors.ControlLight;
		System.Windows.Forms.Label charTacScl = this.CharTacScl1;
		location = new System.Drawing.Point(6, 100);
		charTacScl.Location = location;
		this.CharTacScl1.Name = "CharTacScl1";
		System.Windows.Forms.Label charTacScl2 = this.CharTacScl1;
		itemSize = new System.Drawing.Size(88, 23);
		charTacScl2.Size = itemSize;
		this.CharTacScl1.TabIndex = 6;
		this.CharTacScl1.Text = "Tac Scale:";
		this.CharSizeScl1.AutoSize = true;
		this.CharSizeScl1.Font = new System.Drawing.Font("Impact", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.CharSizeScl1.ForeColor = System.Drawing.SystemColors.ControlLight;
		System.Windows.Forms.Label charSizeScl = this.CharSizeScl1;
		location = new System.Drawing.Point(6, 72);
		charSizeScl.Location = location;
		this.CharSizeScl1.Name = "CharSizeScl1";
		System.Windows.Forms.Label charSizeScl2 = this.CharSizeScl1;
		itemSize = new System.Drawing.Size(93, 23);
		charSizeScl2.Size = itemSize;
		this.CharSizeScl1.TabIndex = 5;
		this.CharSizeScl1.Text = "Size Scale:";
		this.CharCorpus1.AutoSize = true;
		this.CharCorpus1.Font = new System.Drawing.Font("Impact", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.CharCorpus1.ForeColor = System.Drawing.SystemColors.ControlLight;
		System.Windows.Forms.Label charCorpus = this.CharCorpus1;
		location = new System.Drawing.Point(6, 44);
		charCorpus.Location = location;
		this.CharCorpus1.Name = "CharCorpus1";
		System.Windows.Forms.Label charCorpus2 = this.CharCorpus1;
		itemSize = new System.Drawing.Size(71, 23);
		charCorpus2.Size = itemSize;
		this.CharCorpus1.TabIndex = 4;
		this.CharCorpus1.Text = "Corpus:";
		this.CharDescription1.BackColor = System.Drawing.Color.FromArgb(64, 64, 64);
		this.CharDescription1.Font = new System.Drawing.Font("Impact", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.CharDescription1.ForeColor = System.Drawing.SystemColors.Control;
		System.Windows.Forms.RichTextBox charDescription = this.CharDescription1;
		location = new System.Drawing.Point(6, 356);
		charDescription.Location = location;
		this.CharDescription1.Name = "CharDescription1";
		this.CharDescription1.ReadOnly = true;
		this.CharDescription1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
		System.Windows.Forms.RichTextBox charDescription2 = this.CharDescription1;
		itemSize = new System.Drawing.Size(476, 111);
		charDescription2.Size = itemSize;
		this.CharDescription1.TabIndex = 0;
		this.CharDescription1.Text = "No description available...";
		this.PgAttributes.BackColor = System.Drawing.SystemColors.ControlDarkDark;
		this.PgAttributes.Controls.Add(this.CharAttDesc1);
		this.PgAttributes.Controls.Add(this.Label1);
		this.PgAttributes.Controls.Add(this.CharLstAttr1);
		this.PgAttributes.ForeColor = System.Drawing.SystemColors.Control;
		System.Windows.Forms.TabPage pgAttributes = this.PgAttributes;
		location = new System.Drawing.Point(4, 22);
		pgAttributes.Location = location;
		this.PgAttributes.Name = "PgAttributes";
		System.Windows.Forms.TabPage pgAttributes2 = this.PgAttributes;
		padding = new System.Windows.Forms.Padding(3);
		pgAttributes2.Padding = padding;
		System.Windows.Forms.TabPage pgAttributes3 = this.PgAttributes;
		itemSize = new System.Drawing.Size(492, 474);
		pgAttributes3.Size = itemSize;
		this.PgAttributes.TabIndex = 1;
		this.PgAttributes.Text = "Attributes";
		this.CharAttDesc1.BackColor = System.Drawing.Color.DimGray;
		this.CharAttDesc1.Font = new System.Drawing.Font("Impact", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.CharAttDesc1.ForeColor = System.Drawing.SystemColors.Info;
		System.Windows.Forms.RichTextBox charAttDesc = this.CharAttDesc1;
		location = new System.Drawing.Point(7, 357);
		charAttDesc.Location = location;
		this.CharAttDesc1.Name = "CharAttDesc1";
		this.CharAttDesc1.ReadOnly = true;
		this.CharAttDesc1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
		System.Windows.Forms.RichTextBox charAttDesc2 = this.CharAttDesc1;
		itemSize = new System.Drawing.Size(480, 111);
		charAttDesc2.Size = itemSize;
		this.CharAttDesc1.TabIndex = 17;
		this.CharAttDesc1.Text = "No Descriptions available...";
		this.Label1.AutoSize = true;
		this.Label1.Font = new System.Drawing.Font("Impact", 18f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.Label1.ForeColor = System.Drawing.SystemColors.Control;
		System.Windows.Forms.Label label = this.Label1;
		location = new System.Drawing.Point(7, 5);
		label.Location = location;
		this.Label1.Name = "Label1";
		System.Windows.Forms.Label label2 = this.Label1;
		itemSize = new System.Drawing.Size(109, 29);
		label2.Size = itemSize;
		this.Label1.TabIndex = 16;
		this.Label1.Text = "Attributes";
		this.CharLstAttr1.BackColor = System.Drawing.Color.Black;
		this.CharLstAttr1.Font = new System.Drawing.Font("Impact", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.CharLstAttr1.ForeColor = System.Drawing.SystemColors.Info;
		System.Windows.Forms.TreeView charLstAttr = this.CharLstAttr1;
		location = new System.Drawing.Point(6, 37);
		charLstAttr.Location = location;
		this.CharLstAttr1.Name = "CharLstAttr1";
		System.Windows.Forms.TreeView charLstAttr2 = this.CharLstAttr1;
		itemSize = new System.Drawing.Size(481, 314);
		charLstAttr2.Size = itemSize;
		this.CharLstAttr1.TabIndex = 15;
		this.PgPersonality.BackColor = System.Drawing.Color.DimGray;
		this.PgPersonality.Controls.Add(this.CharChatter1);
		this.PgPersonality.Controls.Add(this.CharLstChatter1);
		this.PgPersonality.ForeColor = System.Drawing.SystemColors.Control;
		System.Windows.Forms.TabPage pgPersonality = this.PgPersonality;
		location = new System.Drawing.Point(4, 22);
		pgPersonality.Location = location;
		this.PgPersonality.Name = "PgPersonality";
		System.Windows.Forms.TabPage pgPersonality2 = this.PgPersonality;
		itemSize = new System.Drawing.Size(492, 474);
		pgPersonality2.Size = itemSize;
		this.PgPersonality.TabIndex = 9;
		this.PgPersonality.Text = "Personality";
		this.CharChatter1.AutoSize = true;
		this.CharChatter1.Font = new System.Drawing.Font("Impact", 18f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.CharChatter1.ForeColor = System.Drawing.SystemColors.Control;
		System.Windows.Forms.Label charChatter = this.CharChatter1;
		location = new System.Drawing.Point(7, 7);
		charChatter.Location = location;
		this.CharChatter1.Name = "CharChatter1";
		System.Windows.Forms.Label charChatter2 = this.CharChatter1;
		itemSize = new System.Drawing.Size(55, 29);
		charChatter2.Size = itemSize;
		this.CharChatter1.TabIndex = 14;
		this.CharChatter1.Text = "Sets";
		this.CharLstChatter1.BackColor = System.Drawing.Color.Black;
		this.CharLstChatter1.Font = new System.Drawing.Font("Impact", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.CharLstChatter1.ForeColor = System.Drawing.SystemColors.Info;
		System.Windows.Forms.TreeView charLstChatter = this.CharLstChatter1;
		location = new System.Drawing.Point(6, 39);
		charLstChatter.Location = location;
		this.CharLstChatter1.Name = "CharLstChatter1";
		System.Windows.Forms.TreeView charLstChatter2 = this.CharLstChatter1;
		itemSize = new System.Drawing.Size(481, 432);
		charLstChatter2.Size = itemSize;
		this.CharLstChatter1.TabIndex = 13;
		this.PgDefense.BackColor = System.Drawing.Color.DimGray;
		this.PgDefense.Controls.Add(this.CharArmorDesc1);
		this.PgDefense.Controls.Add(this.CharArmorList1);
		this.PgDefense.Controls.Add(this.CharListArmor1);
		System.Windows.Forms.TabPage pgDefense = this.PgDefense;
		location = new System.Drawing.Point(4, 22);
		pgDefense.Location = location;
		this.PgDefense.Name = "PgDefense";
		System.Windows.Forms.TabPage pgDefense2 = this.PgDefense;
		itemSize = new System.Drawing.Size(492, 474);
		pgDefense2.Size = itemSize;
		this.PgDefense.TabIndex = 7;
		this.PgDefense.Text = "Equipment";
		this.CharArmorDesc1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
		this.CharArmorDesc1.Font = new System.Drawing.Font("Impact", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.CharArmorDesc1.ForeColor = System.Drawing.SystemColors.Info;
		System.Windows.Forms.RichTextBox charArmorDesc = this.CharArmorDesc1;
		location = new System.Drawing.Point(6, 357);
		charArmorDesc.Location = location;
		this.CharArmorDesc1.Name = "CharArmorDesc1";
		this.CharArmorDesc1.ReadOnly = true;
		this.CharArmorDesc1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
		System.Windows.Forms.RichTextBox charArmorDesc2 = this.CharArmorDesc1;
		itemSize = new System.Drawing.Size(480, 111);
		charArmorDesc2.Size = itemSize;
		this.CharArmorDesc1.TabIndex = 14;
		this.CharArmorDesc1.Text = "No Descriptions available...";
		this.CharArmorList1.AutoSize = true;
		this.CharArmorList1.Font = new System.Drawing.Font("Impact", 18f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.CharArmorList1.ForeColor = System.Drawing.SystemColors.Control;
		System.Windows.Forms.Label charArmorList = this.CharArmorList1;
		location = new System.Drawing.Point(7, 7);
		charArmorList.Location = location;
		this.CharArmorList1.Name = "CharArmorList1";
		System.Windows.Forms.Label charArmorList2 = this.CharArmorList1;
		itemSize = new System.Drawing.Size(120, 29);
		charArmorList2.Size = itemSize;
		this.CharArmorList1.TabIndex = 12;
		this.CharArmorList1.Text = "Armor Sets";
		this.CharListArmor1.BackColor = System.Drawing.Color.Black;
		this.CharListArmor1.Font = new System.Drawing.Font("Impact", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.CharListArmor1.ForeColor = System.Drawing.SystemColors.Info;
		System.Windows.Forms.TreeView charListArmor = this.CharListArmor1;
		location = new System.Drawing.Point(8, 39);
		charListArmor.Location = location;
		this.CharListArmor1.Name = "CharListArmor1";
		System.Windows.Forms.TreeView charListArmor2 = this.CharListArmor1;
		itemSize = new System.Drawing.Size(481, 312);
		charListArmor2.Size = itemSize;
		this.CharListArmor1.TabIndex = 11;
		this.PgOffense.BackColor = System.Drawing.Color.DimGray;
		this.PgOffense.Controls.Add(this.CharWeaponDesc1);
		this.PgOffense.Controls.Add(this.CharListWeap1);
		this.PgOffense.Controls.Add(this.CharFavored1);
		System.Windows.Forms.TabPage pgOffense = this.PgOffense;
		location = new System.Drawing.Point(4, 22);
		pgOffense.Location = location;
		this.PgOffense.Name = "PgOffense";
		System.Windows.Forms.TabPage pgOffense2 = this.PgOffense;
		itemSize = new System.Drawing.Size(492, 474);
		pgOffense2.Size = itemSize;
		this.PgOffense.TabIndex = 8;
		this.PgOffense.Text = "Armaments";
		this.CharWeaponDesc1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
		this.CharWeaponDesc1.Font = new System.Drawing.Font("Impact", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.CharWeaponDesc1.ForeColor = System.Drawing.SystemColors.Info;
		System.Windows.Forms.RichTextBox charWeaponDesc = this.CharWeaponDesc1;
		location = new System.Drawing.Point(6, 357);
		charWeaponDesc.Location = location;
		this.CharWeaponDesc1.Name = "CharWeaponDesc1";
		this.CharWeaponDesc1.ReadOnly = true;
		this.CharWeaponDesc1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
		System.Windows.Forms.RichTextBox charWeaponDesc2 = this.CharWeaponDesc1;
		itemSize = new System.Drawing.Size(480, 111);
		charWeaponDesc2.Size = itemSize;
		this.CharWeaponDesc1.TabIndex = 21;
		this.CharWeaponDesc1.Text = "No Descriptions available...";
		this.CharListWeap1.BackColor = System.Drawing.Color.Black;
		this.CharListWeap1.Font = new System.Drawing.Font("Impact", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.CharListWeap1.ForeColor = System.Drawing.SystemColors.Info;
		System.Windows.Forms.TreeView charListWeap = this.CharListWeap1;
		location = new System.Drawing.Point(6, 39);
		charListWeap.Location = location;
		this.CharListWeap1.Name = "CharListWeap1";
		System.Windows.Forms.TreeView charListWeap2 = this.CharListWeap1;
		itemSize = new System.Drawing.Size(480, 312);
		charListWeap2.Size = itemSize;
		this.CharListWeap1.TabIndex = 20;
		this.CharFavored1.AutoSize = true;
		this.CharFavored1.Font = new System.Drawing.Font("Impact", 18f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.CharFavored1.ForeColor = System.Drawing.SystemColors.Control;
		System.Windows.Forms.Label charFavored = this.CharFavored1;
		location = new System.Drawing.Point(7, 7);
		charFavored.Location = location;
		this.CharFavored1.Name = "CharFavored1";
		System.Windows.Forms.Label charFavored2 = this.CharFavored1;
		itemSize = new System.Drawing.Size(182, 29);
		charFavored2.Size = itemSize;
		this.CharFavored1.TabIndex = 18;
		this.CharFavored1.Text = "Favored weapons";
		this.PgSkills.BackColor = System.Drawing.Color.DimGray;
		this.PgSkills.Controls.Add(this.CharLists1);
		this.PgSkills.Controls.Add(this.CharListDesc1);
		this.PgSkills.Controls.Add(this.CharSkills1);
		this.PgSkills.ForeColor = System.Drawing.SystemColors.Control;
		System.Windows.Forms.TabPage pgSkills = this.PgSkills;
		location = new System.Drawing.Point(4, 22);
		pgSkills.Location = location;
		this.PgSkills.Name = "PgSkills";
		System.Windows.Forms.TabPage pgSkills2 = this.PgSkills;
		itemSize = new System.Drawing.Size(492, 474);
		pgSkills2.Size = itemSize;
		this.PgSkills.TabIndex = 4;
		this.PgSkills.Text = "Skills";
		this.CharLists1.BackColor = System.Drawing.Color.Black;
		this.CharLists1.Font = new System.Drawing.Font("Impact", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.CharLists1.ForeColor = System.Drawing.SystemColors.Info;
		System.Windows.Forms.TreeView charLists = this.CharLists1;
		location = new System.Drawing.Point(6, 39);
		charLists.Location = location;
		this.CharLists1.Name = "CharLists1";
		System.Windows.Forms.TreeView charLists2 = this.CharLists1;
		itemSize = new System.Drawing.Size(480, 312);
		charLists2.Size = itemSize;
		this.CharLists1.TabIndex = 10;
		this.CharListDesc1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
		this.CharListDesc1.Font = new System.Drawing.Font("Impact", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.CharListDesc1.ForeColor = System.Drawing.SystemColors.Info;
		System.Windows.Forms.RichTextBox charListDesc = this.CharListDesc1;
		location = new System.Drawing.Point(6, 357);
		charListDesc.Location = location;
		this.CharListDesc1.Name = "CharListDesc1";
		this.CharListDesc1.ReadOnly = true;
		this.CharListDesc1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
		System.Windows.Forms.RichTextBox charListDesc2 = this.CharListDesc1;
		itemSize = new System.Drawing.Size(480, 111);
		charListDesc2.Size = itemSize;
		this.CharListDesc1.TabIndex = 9;
		this.CharListDesc1.Text = "No Descriptions available...";
		this.CharSkills1.AutoSize = true;
		this.CharSkills1.Font = new System.Drawing.Font("Impact", 18f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		System.Windows.Forms.Label charSkills = this.CharSkills1;
		location = new System.Drawing.Point(7, 7);
		charSkills.Location = location;
		this.CharSkills1.Name = "CharSkills1";
		System.Windows.Forms.Label charSkills2 = this.CharSkills1;
		itemSize = new System.Drawing.Size(65, 29);
		charSkills2.Size = itemSize;
		this.CharSkills1.TabIndex = 5;
		this.CharSkills1.Text = "Skills";
		this.CharSelect.BackColor = System.Drawing.SystemColors.InfoText;
		this.CharSelect.Font = new System.Drawing.Font("Impact", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.CharSelect.ForeColor = System.Drawing.SystemColors.Control;
		this.CharSelect.FormattingEnabled = true;
		System.Windows.Forms.ComboBox charSelect = this.CharSelect;
		location = new System.Drawing.Point(12, 12);
		charSelect.Location = location;
		this.CharSelect.Name = "CharSelect";
		System.Windows.Forms.ComboBox charSelect2 = this.CharSelect;
		itemSize = new System.Drawing.Size(400, 25);
		charSelect2.Size = itemSize;
		this.CharSelect.TabIndex = 4;
		this.BtnConvert.BackColor = System.Drawing.SystemColors.ControlDark;
		this.BtnConvert.Font = new System.Drawing.Font("Impact", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.BtnConvert.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
		System.Windows.Forms.Button btnConvert = this.BtnConvert;
		location = new System.Drawing.Point(418, 12);
		btnConvert.Location = location;
		this.BtnConvert.Name = "BtnConvert";
		System.Windows.Forms.Button btnConvert2 = this.BtnConvert;
		itemSize = new System.Drawing.Size(226, 26);
		btnConvert2.Size = itemSize;
		this.BtnConvert.TabIndex = 5;
		this.BtnConvert.Text = "Create New Entry";
		this.BtnConvert.UseVisualStyleBackColor = false;
		this.BtnGlossary.BackColor = System.Drawing.SystemColors.ControlDark;
		this.BtnGlossary.Font = new System.Drawing.Font("Impact", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.BtnGlossary.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
		System.Windows.Forms.Button btnGlossary = this.BtnGlossary;
		location = new System.Drawing.Point(688, 12);
		btnGlossary.Location = location;
		this.BtnGlossary.Name = "BtnGlossary";
		System.Windows.Forms.Button btnGlossary2 = this.BtnGlossary;
		itemSize = new System.Drawing.Size(226, 26);
		btnGlossary2.Size = itemSize;
		this.BtnGlossary.TabIndex = 6;
		this.BtnGlossary.Text = "Open Glossary";
		this.BtnGlossary.UseVisualStyleBackColor = false;
		this.CharDisplay1.BackColor = System.Drawing.Color.Transparent;
		this.CharDisplay1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
		System.Windows.Forms.PictureBox charDisplay = this.CharDisplay1;
		location = new System.Drawing.Point(12, 44);
		charDisplay.Location = location;
		this.CharDisplay1.Name = "CharDisplay1";
		System.Windows.Forms.PictureBox charDisplay2 = this.CharDisplay1;
		itemSize = new System.Drawing.Size(400, 500);
		charDisplay2.Size = itemSize;
		this.CharDisplay1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
		this.CharDisplay1.TabIndex = 1;
		this.CharDisplay1.TabStop = false;
		System.Drawing.SizeF sizeF = new System.Drawing.SizeF(6f, 13f);
		this.AutoScaleDimensions = sizeF;
		this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.AutoSize = true;
		this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
		this.BackColor = System.Drawing.Color.FromArgb(64, 64, 64);
		itemSize = new System.Drawing.Size(924, 561);
		this.ClientSize = itemSize;
		this.Controls.Add(this.BtnGlossary);
		this.Controls.Add(this.BtnConvert);
		this.Controls.Add(this.CharSelect);
		this.Controls.Add(this.Tabs);
		this.Controls.Add(this.CharDisplay1);
		this.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		this.MaximizeBox = false;
		itemSize = new System.Drawing.Size(1350, 600);
		this.MaximumSize = itemSize;
		itemSize = new System.Drawing.Size(940, 600);
		this.MinimumSize = itemSize;
		this.Name = "FormPNDossier";
		this.ShowIcon = false;
		this.Text = "Madness: Project Nexus Dossier";
		this.Tabs.ResumeLayout(false);
		this.PgOverview.ResumeLayout(false);
		this.CharSubAtt1.ResumeLayout(false);
		this.CharSubAtt1.PerformLayout();
		this.CharAttr1.ResumeLayout(false);
		this.CharAttr1.PerformLayout();
		this.CharOver1.ResumeLayout(false);
		this.CharOver1.PerformLayout();
		this.PgAttributes.ResumeLayout(false);
		this.PgAttributes.PerformLayout();
		this.PgPersonality.ResumeLayout(false);
		this.PgPersonality.PerformLayout();
		this.PgDefense.ResumeLayout(false);
		this.PgDefense.PerformLayout();
		this.PgOffense.ResumeLayout(false);
		this.PgOffense.PerformLayout();
		this.PgSkills.ResumeLayout(false);
		this.PgSkills.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.CharDisplay1).EndInit();
		this.ResumeLayout(false);
	}

	private void FormPNDossier_Load(object sender, EventArgs e)
	{
		using var dialogLoader = new DialogLoader();
		if (dialogLoader.ShowDialog(this) != DialogResult.Yes)
		{
			Close();
			return;
		}
		FilterCharacters();
	}

	private void FormPNDossier_Close(object sender, FormClosingEventArgs e)
	{
		if (Application.OpenForms.OfType<FormEditorV2>().Any() && DossierDialog.Show("You still have editors open. Do you want to close without saving?", "Editors still open.", MessageBoxButtons.YesNo) == DialogResult.No)
		{
			e.Cancel = true;
		}
	}

	private void CharSelect_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (CharSelect.SelectedItem == null) return;
		string key = CharSelect.SelectedItem.ToString();
		if (ModEnums.IMAGES.ContainsKey(key))
		{
			CharDisplay1.Image = ModEnums.IMAGES[key];
		}
		else
		{
			CharDisplay1.Image = null;
		}
		if (ModEnums.DB_CHAR.ContainsKey(CharSelect.SelectedItem.ToString()))
		{
			Overview_Populate(ModEnums.DB_CHAR[key]);
			Attributes_Populate(ModEnums.DB_CHAR[key]);
			Personality_Populate(ModEnums.DB_CHAR[key]);
			Equipment_Populate(ModEnums.DB_CHAR[key]);
			Armaments_Populate(ModEnums.DB_CHAR[key]);
			Skills_Populate(ModEnums.DB_CHAR[key]);
		}
        SubjectUpdated(key);
	}

	private int GetSubAttribute(int Att1, int Att2)
	{
		if (Att1 == Att2)
		{
			return Att1;
		}
		int num = Math.Min(Att1, Att2);
		int num2 = Math.Max(Att1, Att2);
		return checked((int)Math.Round(Math.Floor((double)num + Math.Pow(num2 - num, 0.75) - 1.0)));
	}

	private void CalculateAttributeActions(double TacBarMult, double SizeScale)
	{
		AttributeActions.Add("ShovePower", Math.Pow(Attributes[0], 0.9) * 1.25);
		AttributeActions.Add("WeaponSize", 1.7 + (double)Attributes[0] / 15.0);
		AttributeActions.Add("Intimidation", (double)Attributes[0] / 40.0);
		AttributeActions.Add("ResistDizzy", Math.Max(0.0, 1.0 - Math.Pow(Attributes[1], 0.9) / 40.0));
		AttributeActions.Add("ResistShove", Math.Max(0.0, 1.0 - Math.Pow(Attributes[1], 0.9) / 40.0));
		AttributeActions.Add("EncumbranceMax", 0.2 + Math.Pow(Attributes[1], 0.9));
		AttributeActions.Add("MoveSpeed", 5.0 + Math.Pow(Attributes[2], 0.75) * 0.2);
		AttributeActions.Add("TacBarSize", Math.Pow(Attributes[2], 0.9) * TacBarMult);
		AttributeActions.Add("KnockdownRecovery", 180.0 - Math.Pow(Attributes[2], 0.9) * 3.0);
		AttributeActions.Add("WeaponSmallBonus", 0.2 + Math.Pow(Attributes[2], 0.9) / 40.0 * 2.0);
		AttributeActions.Add("TacDamage", 1.0 + Math.Pow(Attributes[3], 0.9) / 75.0);
		AttributeActions.Add("TacRegen", 0.75 + Math.Pow(Attributes[3], 0.9) / 40.0 * 2.0);
		AttributeActions.Add("ReduceRecoil", 0.75 + 5.0 / Math.Pow(SubAttributes[0], 0.9));
		AttributeActions.Add("UnarmedDamage", Math.Pow(SubAttributes[0], 0.9) * 0.2);
		AttributeActions.Add("BluntStun", Math.Pow(SubAttributes[0], 0.9) / 40.0 * 0.5);
		AttributeActions.Add("WeaponBigPenalty", Math.Pow(SubAttributes[0], 0.9) / 40.0 / 2.0);
		AttributeActions.Add("UnarmedPiercing", 0.3 + 0.45 * (Math.Pow(SubAttributes[0], 0.9) / 40.0 / 1.5));
		AttributeActions.Add("UnarmedStun", Math.Pow(SubAttributes[1], 0.9) / 40.0 * 0.5);
		AttributeActions.Add("H2HCritDamage", Math.Max(0.0, 1.0 + Math.Pow(SubAttributes[1], 0.9) / 40.0 * 0.25));
		AttributeActions.Add("SlashStun", Math.Pow(SubAttributes[1], 0.9) / 40.0 * 0.125);
		AttributeActions.Add("TraumaRegen", Math.Pow(SubAttributes[2], 0.9) * 5.0 * Math.Pow(10.0, -6.0));
		AttributeActions.Add("KnockdownChance", Math.Pow(SubAttributes[2], 0.9) / 40.0);
		AttributeActions.Add("RecoilRecovery", 0.75 + 10.0 / Math.Pow(SubAttributes[2], 0.9));
		AttributeActions.Add("MeleeDamage", Math.Pow(SubAttributes[3], 0.9) * 0.25);
		AttributeActions.Add("ThrowDistance", Math.Pow(SubAttributes[3], 0.9) + 15.0);
		AttributeActions.Add("MeleePiercing", 0.3 + 0.45 * (Math.Pow(SubAttributes[3], 0.9) / 40.0 / 1.5));
		AttributeActions.Add("SprintSpeed", 1.25 + Math.Pow(SubAttributes[4], 0.9) * 0.01);
		AttributeActions.Add("BlockPower", 1.0 + -0.5 * Math.Max(1.0, 1.0 - Math.Pow(SubAttributes[4], 0.9) / 40.0));
		AttributeActions.Add("ResistKnockdown", Math.Max(0.0, 1.0 - Math.Pow(SubAttributes[4], 0.9) / 40.0));
		AttributeActions.Add("H2HSpeed", 1.2 + Math.Pow(SubAttributes[4], 0.9) * 0.015);
		AttributeActions.Add("RangedCritDamage", 1.0 + Math.Pow(SubAttributes[5], 0.9) * 0.01);
		AttributeActions.Add("AimWalkSpeed", 0.2 + Math.Pow(SubAttributes[5], 0.9) / 40.0 * 0.5);
		AttributeActions.Add("ReloadSpeed", 0.125 + Math.Pow(SubAttributes[5], 0.9) / 40.0 * 0.35);
		AttributeActions.Add("RangedAccuracy", 1.0 - Math.Pow(SubAttributes[5], 0.75) / 16.0);
	}

	private void Overview_Populate(JObject CharData)
	{
		CharName1.Text = CharData["FullName"].ToString();
		CharTitle1.Text = "[" + CharData["Title"].ToString() + "]";
		CharFaction1.Text = Enum.GetName(typeof(ModEnums.Faction), CharData["DefaultFaction"].ToObject<int>());
		ValLevel1.Text = CharData["Level"].ToString();
		ValCorpus1.Text = CharData["CorpusBlocks"].Children().Count() + "x " + CharData["Health"].ToString();
		ValCorpus1.ForeColor = GetColorFrom((JObject)CharData["Blood"]);
		ValSizeScl1.Text = "x" + CharData["Scale"].ToString();
		ValTacScl1.Text = "x" + CharData["TacBar"].ToString();
		int num = 0;
		foreach (JToken item in CharData["Attributes"].Children())
		{
			Attributes[num] = int.Parse(item.ToString());
			num = checked(num + 1);
		}
		ValStrength1.Text = Conversions.ToString(Attributes[0]);
		ValEndurance1.Text = Conversions.ToString(Attributes[1]);
		ValDexterity1.Text = Conversions.ToString(Attributes[2]);
		ValTactics1.Text = Conversions.ToString(Attributes[3]);
		SubAttributes[0] = GetSubAttribute(Attributes[0], Attributes[1]);
		SubAttributes[1] = GetSubAttribute(Attributes[0], Attributes[3]);
		SubAttributes[2] = GetSubAttribute(Attributes[1], Attributes[3]);
		SubAttributes[3] = GetSubAttribute(Attributes[0], Attributes[2]);
		SubAttributes[4] = GetSubAttribute(Attributes[1], Attributes[2]);
		SubAttributes[5] = GetSubAttribute(Attributes[2], Attributes[3]);
		ValForce1.Text = Conversions.ToString(SubAttributes[0]);
		ValCritical1.Text = Conversions.ToString(SubAttributes[1]);
		ValStrive1.Text = Conversions.ToString(SubAttributes[2]);
		ValLethality1.Text = Conversions.ToString(SubAttributes[3]);
		ValAthletics1.Text = Conversions.ToString(SubAttributes[4]);
		ValSenses1.Text = Conversions.ToString(SubAttributes[5]);
		AttributeActions.Clear();
		CalculateAttributeActions(double.Parse(CharData["TacBar"].ToString()), double.Parse(CharData["Scale"].ToString()));
		CharDescription1.Text = CharData["Description"].ToString();
	}

	private void Attributes_Populate(JObject CharData)
	{
		CharLstAttr1.Nodes.Clear();
		TreeNode treeNode = new TreeNode("Corpus");
		treeNode.Name = "Corpus";
		foreach (JToken item in CharData["CorpusBlocks"].Children())
		{
			if (ModEnums.DB_CORPUS.ContainsKey(item.ToString()))
			{
				TreeNode treeNode2 = new TreeNode(item.ToString());
				treeNode2.ForeColor = GetColorFrom((JObject)ModEnums.DB_CORPUS[item.ToString()]["BorderColor"]);
				treeNode.Nodes.Add(treeNode2);
			}
		}
		TreeNode treeNode3 = new TreeNode("Physical");
		treeNode3.Name = "Physical";
		treeNode3.Nodes.Add("MoveSpeed", "Movement Speed: " + CharData["Speed"]["x"].ToString());
		treeNode3.Nodes.Add("Accel", "Acceleration: x" + CharData["Speed"]["y"].ToString());
		treeNode3.Nodes.Add("Decel", "Deceleration: x" + CharData["Speed"]["z"].ToString());
		treeNode3.Nodes.Add("Speed_SprintBonus", "Sprint Boost: +" + CharData["Speed_SprintBonus"].ToString());
		treeNode3.Nodes.Add("Toughness", "Toughness: +" + CharData["Toughness"].ToString());
		TreeNode treeNode4 = new TreeNode("Attributes");
		treeNode4.Name = "Attributes";
		TreeNode treeNode5 = treeNode4.Nodes.Add("Strength", "Strength: " + Conversions.ToString(Attributes[0]));
		TreeNode treeNode6 = treeNode4.Nodes.Add("Endurance", "Endurance: " + Conversions.ToString(Attributes[1]));
		TreeNode treeNode7 = treeNode4.Nodes.Add("Dexterity", "Dexterity: " + Conversions.ToString(Attributes[2]));
		TreeNode treeNode8 = treeNode4.Nodes.Add("Tactics", "Tactics: " + Conversions.ToString(Attributes[3]));
		TreeNode treeNode9 = new TreeNode("SubAttributes");
		treeNode9.Name = "SubAttributes";
		TreeNode treeNode10 = treeNode9.Nodes.Add("Force", "Force: " + Conversions.ToString(SubAttributes[0]));
		TreeNode treeNode11 = treeNode9.Nodes.Add("Critical", "Critical: " + Conversions.ToString(SubAttributes[1]));
		TreeNode treeNode12 = treeNode9.Nodes.Add("Strive", "Strive: " + Conversions.ToString(SubAttributes[2]));
		TreeNode treeNode13 = treeNode9.Nodes.Add("Lethality", "Lethality: " + Conversions.ToString(SubAttributes[3]));
		TreeNode treeNode14 = treeNode9.Nodes.Add("Athletics", "Athletics: " + Conversions.ToString(SubAttributes[4]));
		TreeNode treeNode15 = treeNode9.Nodes.Add("Senses", "Senses: " + Conversions.ToString(SubAttributes[5]));
		treeNode5.Nodes.Add("ShovePower", "Shove Distance: " + Conversions.ToString(Math.Round(AttributeActions["ShovePower"], 1)) + " Feet");
		treeNode5.Nodes.Add("WeaponSize", "Single Hand Weapon Size: " + Conversions.ToString(Math.Round(AttributeActions["WeaponSize"], 1)));
		treeNode5.Nodes.Add("Intimidation", "Intimidation Chance: " + Conversions.ToString(Math.Min(100.0, Math.Round(AttributeActions["Intimidation"], 2) * 100.0)) + "%");
		treeNode6.Nodes.Add("ResistDizzy", "Resist Dizzy: " + Conversions.ToString(Math.Min(100.0, Math.Round(1.0 - AttributeActions["ResistDizzy"], 2) * 100.0)) + "%");
		treeNode6.Nodes.Add("ResistShove", "Resist Shove: " + Conversions.ToString(Math.Min(100.0, Math.Round(1.0 - AttributeActions["ResistShove"], 2) * 100.0)) + "%");
		treeNode6.Nodes.Add("EncumbranceMax", "Encumbrance Max: " + Conversions.ToString(Math.Round(AttributeActions["EncumbranceMax"])));
		treeNode7.Nodes.Add("MoveSpeed", "Movement Speed: " + Conversions.ToString(Math.Round(AttributeActions["MoveSpeed"], 1)) + "Feet/Second");
		treeNode7.Nodes.Add("TacBarSize", "TacBar Size: " + Conversions.ToString(Math.Round(AttributeActions["TacBarSize"], 1)));
		treeNode7.Nodes.Add("KnockdownRecovery", "Knockdown Recovery: " + Conversions.ToString(AttributeActions["KnockdownRecovery"]));
		treeNode7.Nodes.Add("WeaponSmallBonus", "Weapon Speed: x" + Conversions.ToString(Math.Round(AttributeActions["WeaponSmallBonus"], 2)));
		treeNode8.Nodes.Add("TacDamage", "TacBar Damage: x" + Conversions.ToString(Math.Round(AttributeActions["TacDamage"], 1)));
		treeNode8.Nodes.Add("TacRegen", "TacBar Regeneration: x" + Conversions.ToString(Math.Round(AttributeActions["TacRegen"], 1)));
		treeNode10.Nodes.Add("ReduceRecoil", "Recoil Reduction: " + Conversions.ToString(Math.Round(AttributeActions["ReduceRecoil"], 2)));
		treeNode10.Nodes.Add("UnarmedDamage", "Unarmed Damage: +" + Conversions.ToString(Math.Round(AttributeActions["UnarmedDamage"], 2)));
		treeNode10.Nodes.Add("BluntStun", "Blunt Dizzy Chance: " + Conversions.ToString(Math.Min(100.0, Math.Round(AttributeActions["BluntStun"], 2) * 100.0)) + "%");
		treeNode10.Nodes.Add("WeaponBigPenalty", "Big Weapon Penalty Reduction: " + Conversions.ToString(Math.Round(AttributeActions["WeaponBigPenalty"], 2) * 100.0) + "%");
		treeNode10.Nodes.Add("UnarmedPiercing", "Unarmed Piercing: +" + Conversions.ToString(Math.Round(AttributeActions["UnarmedPiercing"], 2)));
		treeNode11.Nodes.Add("UnarmedStun", "Unarmed Dizzy Chance: " + Conversions.ToString(Math.Min(100.0, Math.Round(AttributeActions["UnarmedStun"], 2) * 100.0)) + "%");
		treeNode11.Nodes.Add("H2HCritDamage", "H2H Critical Damage: x" + Conversions.ToString(Math.Round(AttributeActions["H2HCritDamage"], 2)));
		treeNode11.Nodes.Add("SlashStun", "Slash Dizzy Chance: " + Conversions.ToString(Math.Min(100.0, Math.Round(AttributeActions["SlashStun"], 2) * 100.0)) + "%");
		treeNode12.Nodes.Add("TraumaRegen", "Trauma Recovery: x" + Conversions.ToString(1.0 + Math.Round(AttributeActions["TraumaRegen"] * 1000.0, 2)));
		treeNode12.Nodes.Add("KnockdownChance", "Knockdown Chance: " + Conversions.ToString(Math.Min(100.0, Math.Round(1.0 - AttributeActions["KnockdownChance"], 2) * 100.0)) + "%");
		treeNode12.Nodes.Add("RecoilRecovery", "Recoil Recovery: " + Conversions.ToString(Math.Round(AttributeActions["RecoilRecovery"], 2)));
		treeNode13.Nodes.Add("MeleeDamage", "Melee Damage: +" + Conversions.ToString(Math.Round(AttributeActions["MeleeDamage"], 2)));
		treeNode13.Nodes.Add("ThrowDistance", "Throw Distance: " + Conversions.ToString(Math.Round(AttributeActions["ThrowDistance"], 1)) + " Feet");
		treeNode13.Nodes.Add("MeleePiercing", "Melee Piercing: +" + Conversions.ToString(Math.Round(AttributeActions["MeleePiercing"], 2)));
		treeNode14.Nodes.Add("SprintSpeed", "Sprint Speed Bonus: x" + Conversions.ToString(Math.Round(AttributeActions["SprintSpeed"], 2)));
		treeNode14.Nodes.Add("BlockPower", "Block Damage Reduction: x" + Conversions.ToString(Math.Round(AttributeActions["BlockPower"], 2)));
		treeNode14.Nodes.Add("ResistKnockdown", "Resist Knockdown Chance: " + Conversions.ToString(Math.Min(100.0, Math.Round(AttributeActions["ResistKnockdown"], 2) * 100.0)) + "%");
		treeNode14.Nodes.Add("H2HSpeed", "H2H Speed: x" + Conversions.ToString(Math.Round(AttributeActions["H2HSpeed"], 2)));
		treeNode15.Nodes.Add("RangedCritDamage", "Ranged Critical Damage: x" + Conversions.ToString(Math.Round(AttributeActions["RangedCritDamage"], 2)));
		treeNode15.Nodes.Add("AimWalkSpeed", "Aim Walk Speed: x" + Conversions.ToString(Math.Round(AttributeActions["AimWalkSpeed"], 2)));
		treeNode15.Nodes.Add("ReloadSpeed", "Reload Speed: " + Conversions.ToString(Math.Round(AttributeActions["ReloadSpeed"], 2) * 100.0) + "%");
		treeNode15.Nodes.Add("RangedAccuracy", "Ranged Accuracy: " + Conversions.ToString(Math.Min(100.0, Math.Round(1.0 - AttributeActions["RangedAccuracy"], 2) * 100.0)) + "%");
		CharLstAttr1.Nodes.Add(treeNode);
		CharLstAttr1.Nodes.Add(treeNode3);
		CharLstAttr1.Nodes.Add(treeNode4);
		CharLstAttr1.Nodes.Add(treeNode9);
		treeNode4.Expand();
		treeNode9.Expand();
		if (CharData["CountWornArmor"].ToString().Equals("0"))
		{
			CharLstAttr1.Nodes["Attributes"].Nodes["Endurance"].Nodes["EncumbranceMax"].ForeColor = Color.DimGray;
		}
		CharLstAttr1.Nodes["Attributes"].Nodes["Strength"].Nodes["ShovePower"].ForeColor = Color.DimGray;
		CharLstAttr1.Nodes["Attributes"].Nodes["Dexterity"].Nodes["TacBarSize"].ForeColor = Color.DimGray;
		CharLstAttr1.Nodes["Attributes"].Nodes["Tactics"].Nodes["TacRegen"].ForeColor = Color.DimGray;
		CharLstAttr1.Nodes["SubAttributes"].Nodes["Critical"].Nodes["H2HCritDamage"].ForeColor = Color.DimGray;
		CharLstAttr1.Nodes["SubAttributes"].Nodes["Athletics"].Nodes["SprintSpeed"].ForeColor = Color.DimGray;
		CharLstAttr1.Nodes["SubAttributes"].Nodes["Athletics"].Nodes["BlockPower"].ForeColor = Color.DimGray;
		CharLstAttr1.Nodes["SubAttributes"].Nodes["Senses"].Nodes["AimWalkSpeed"].ForeColor = Color.DimGray;
	}

	private void CharLstAttr1_AfterSelect(object sender, TreeViewEventArgs e)
	{
		string name = CharLstAttr1.SelectedNode.Name;
		CharAttDesc1.Text = "Select an item to view its description.";
		if (ModEnums.ATTRIBUTEDESC.ContainsKey(name))
		{
			CharAttDesc1.Text = name + "\r\n" + ModEnums.ATTRIBUTEDESC[name];
		}
	}

	private void Equipment_Populate(JObject CharData)
	{
		CharListArmor1.Nodes.Clear();
		TreeNode treeNode = new TreeNode(CharData["FullName"].ToString());
		if (CharData["CountWornArmor"].ToString().Equals("0"))
		{
			treeNode.Nodes.Add("Armor: " + CharData["ArmorBody"]["x"].ToString() + "/" + CharData["ArmorHead"]["x"].ToString());
		}
		foreach (JToken item in CharData["DefaultArmor"].Children())
		{
			TreeNode treeNode2 = new TreeNode(GetNameOfObject(item.ToString(), ModEnums.DB_GEAR));
			treeNode2.Name = item.ToString();
			treeNode.Nodes.Add(treeNode2);
		}
		foreach (JToken item2 in CharData["VariableArmor"].Children())
		{
			TreeNode treeNode3 = new TreeNode("Variable");
			foreach (JToken item3 in item2["Armor"].Children())
			{
				TreeNode treeNode4 = new TreeNode(GetNameOfObject(item3.ToString(), ModEnums.DB_GEAR));
				treeNode4.Name = item3.ToString();
				treeNode3.Nodes.Add(treeNode4);
			}
			treeNode.Nodes.Add(treeNode3);
		}
		foreach (JToken item4 in CharData["Uniform"].Children())
		{
			TreeNode treeNode5 = new TreeNode("Uniform");
			TreeNode treeNode6 = new TreeNode("Armor");
			treeNode5.Nodes.Add(treeNode6);
			foreach (JToken item5 in item4["ArmorPicks"]["Armor"].Children())
			{
				TreeNode treeNode7 = new TreeNode(GetNameOfObject(item5.ToString(), ModEnums.DB_GEAR));
				treeNode7.Name = item5.ToString();
				treeNode6.Nodes.Add(treeNode7);
			}
			if (treeNode6.Nodes.Count == 1)
			{
				treeNode5.Text = treeNode6.FirstNode.Text;
				treeNode5.Name = treeNode6.FirstNode.Name;
				treeNode6.Nodes.Clear();
				treeNode5.Nodes.Clear();
			}
			TreeNode treeNode8 = new TreeNode("Color");
			treeNode8.ForeColor = GetColorFrom((JObject)item4["Tint"]);
			treeNode5.Nodes.Add(treeNode8);
			TreeNode treeNode9 = new TreeNode("Accents");
			treeNode9.ForeColor = GetColorFrom((JObject)item4["Tint2"]);
			treeNode5.Nodes.Add(treeNode9);
			TreeNode treeNode10 = new TreeNode("Glow");
			treeNode10.ForeColor = GetColorFrom((JObject)item4["Glass"]);
			treeNode5.Nodes.Add(treeNode10);
			treeNode.Nodes.Add(treeNode5);
		}
		if (CharData["AlternateParts"] != null)
		{
			TreeNode treeNode11 = new TreeNode("AlternateParts");
			foreach (JToken item6 in CharData["AlternateParts"].Children())
			{
				TreeNode treeNode12 = new TreeNode(item6.ToString());
				treeNode12.Name = item6.ToString();
				treeNode11.Nodes.Add(treeNode12);
			}
			treeNode.Nodes.Add(treeNode11);
		}
		if (CharData["SwapMaterials"] != null)
		{
			TreeNode treeNode13 = new TreeNode("SwapMaterials");
			foreach (JToken item7 in CharData["SwapMaterials"].Children())
			{
				string text = "";
				foreach (JToken item8 in item7["parts"].Children())
				{
					text = text + Enum.GetName(typeof(ModEnums.ModelParts), item8.ToObject<int>()) + " ";
				}
				TreeNode treeNode14 = new TreeNode(text);
				foreach (JToken item9 in item7["materials"].Children())
				{
					treeNode14.Nodes.Add(new TreeNode(item9.ToString()));
				}
				treeNode14.Name = text;
				treeNode13.Nodes.Add(treeNode14);
			}
			treeNode.Nodes.Add(treeNode13);
		}
		CharListArmor1.Nodes.Add(treeNode);
		treeNode.Expand();
		CharArmorDesc1.Text = "Select an item to view its description.";
	}

	private void Armaments_Populate(JObject CharData)
	{
		string[] array = new string[10] { "Pistols", "Revolvers", "SMGs", "Shotguns", "Rifles", "Heavy", "Melee", "Unarmed", "Thrown", "Shield" };
		CharListWeap1.Nodes.Clear();
		TreeNode treeNode = CharListWeap1.Nodes.Add("Physical");
		treeNode.Nodes.Add("Fist: " + CharData["Fist"].ToString().Substring(2));
		if (ModEnums.DB_WEAPONS.ContainsKey(CharData["Kick"].ToString()))
		{
			treeNode.Nodes.Add("Kick: " + CharData["Kick"].ToString().Substring(2));
		}
		if (ModEnums.DB_WEAPONS.ContainsKey(CharData["Headbutt"].ToString()))
		{
			treeNode.Nodes.Add("Headbutt: " + CharData["Headbutt"].ToString().Substring(2));
		}
		if (ModEnums.DB_EXPLOSION.ContainsKey(CharData["SlamExplode"].ToString()))
		{
			treeNode.Nodes.Add("SlamExplode: " + CharData["SlamExplode"].ToString());
		}
		int num = int.Parse(CharData["WeaponPreference"].ToString());
		string text = "None";
		if (num < array.Length)
		{
			text = array[num];
		}
		CharListWeap1.Nodes.Add("Weapon Preference: " + text);
		TreeNode treeNode2 = new TreeNode("Available Genres");
		foreach (JToken item in CharData["ItemGenres"].Children())
		{
			TreeNode node = new TreeNode(Enum.GetName(typeof(ModEnums.ItemGenre), item.ToObject<int>()));
			treeNode2.Nodes.Add(node);
		}
		CharListWeap1.Nodes.Add(treeNode2);
		TreeNode treeNode3 = new TreeNode("Default Weapons");
		if (!CharData["DefaultPrimary"].ToString().Equals("None"))
		{
			treeNode3.Nodes.Add(CharData["DefaultPrimary"].ToString(), "Primary: " + GetWeapon(CharData["DefaultPrimary"].ToString()));
		}
		if (!CharData["DefaultSecondary"].ToString().Equals("None"))
		{
			treeNode3.Nodes.Add(CharData["DefaultSecondary"].ToString(), "OffHand: " + GetWeapon(CharData["DefaultSecondary"].ToString()));
		}
		if (!CharData["DefaultSidearm"].ToString().Equals("None"))
		{
			treeNode3.Nodes.Add(CharData["DefaultSidearm"].ToString(), "Sidearm: " + GetWeapon(CharData["DefaultSidearm"].ToString()));
		}
		if (!CharData["UnarmedReplacement"].ToString().Equals("None"))
		{
			treeNode3.Nodes.Add(CharData["UnarmedReplacement"].ToString(), "Replacement: " + GetWeapon(CharData["UnarmedReplacement"].ToString()));
		}
		if (!CharData["DefaultLoadout"].ToString().Equals("None"))
		{
			string text2 = CharData["DefaultLoadout"].ToString();
			JObject value = null;
			if (ModEnums.DB_LOADOUTS.TryGetValue(text2, out value))
			{
				TreeNode treeNode4 = new TreeNode(GetNameOfObject(text2, ModEnums.DB_LOADOUTS));
				treeNode4.Name = text2;
				foreach (JToken item2 in value["WeaponsStockpile"].Children())
				{
					TreeNode treeNode5 = new TreeNode(GetWeapon(item2.ToString()));
					treeNode5.Name = item2.ToString();
					if (!treeNode5.Text.Equals("None"))
					{
						treeNode4.Nodes.Add(treeNode5);
					}
				}
				treeNode3.Nodes.Add(treeNode4);
			}
		}
		CharListWeap1.Nodes.Add(treeNode3);
		treeNode.Expand();
		treeNode3.Expand();
		CharWeaponDesc1.Text = "Select an item to view its description.";
	}

	private void CharListArmor1_AfterSelect(object sender, TreeViewEventArgs e)
	{
		string name = CharListArmor1.SelectedNode.Name;
		JObject value = null;
		if (ModEnums.DB_GEAR.TryGetValue(name, out value))
		{
			CharArmorDesc1.Text = value["FullName"].ToString() + "\r\n" + value["Description"].ToString();
		}
		else
		{
			CharArmorDesc1.Text = "No Description Available.";
		}
	}

	private void CharListWeap1_AfterSelect(object sender, TreeViewEventArgs e)
	{
		string name = CharListWeap1.SelectedNode.Name;
		JObject value = null;
		if (CharListWeap1.SelectedNode.Nodes.Count != 0)
		{
			return;
		}
		if (ModEnums.DB_MODDEDWEAPONS.TryGetValue(name, out value))
		{
			string text = "";
			foreach (JToken item in value["Addons"].Children())
			{
				text = text + GetNameOfObject(item.ToString(), ModEnums.DB_WEAPONADDONS) + "\r\n";
			}
			CharWeaponDesc1.Text = "Modded " + GetNameOfObject(value["LinkedWeapon"].ToString(), ModEnums.DB_WEAPONS) + "\r\n" + text;
		}
		else if (ModEnums.DB_WEAPONS.TryGetValue(name, out value))
		{
			CharWeaponDesc1.Text = value["FullName"].ToString() + "\r\n" + value["Description"].ToString();
		}
		else
		{
			CharWeaponDesc1.Text = "No Description Available.";
		}
	}

	private void Personality_Populate(JObject CharData)
	{
		string[] array = new string[9] { "Spawned", "WakeUp", "Attacking", "Chasing", "Waiting", "EnemyDown", "FriendDied", "Wounded", "Died" };
		CharLstChatter1.Nodes.Clear();
		TreeNode treeNode = new TreeNode("Animation Set: " + CharData["AnimationSet"].ToString());
		JObject value = new JObject();
		if (ModEnums.DB_ANIMSETS.TryGetValue(CharData["AnimationSet"].ToString(), out value))
		{
			foreach (JToken item in value.Children())
			{
				if (ModEnums.DB_ANIMATIONS.ContainsKey(item.Last.ToString()))
				{
					TreeNode node = new TreeNode(item.ToString());
					treeNode.Nodes.Add(node);
				}
			}
		}
		TreeNode treeNode2 = new TreeNode("Text Chatter: " + CharData["TextChatter"].ToString());
		JObject value2 = new JObject();
		if (ModEnums.DB_CHATTER.TryGetValue(CharData["TextChatter"].ToString(), out value2))
		{
			treeNode2.Nodes.Add("Font: " + value2["Font"].ToString());
			treeNode2.Nodes.Add("Size: " + value2["Size"].ToString());
			treeNode2.Nodes.Add("Style: " + value2["Style"].ToString());
			Color colorFrom = GetColorFrom((JObject)value2["Color"]);
			string[] array2 = array;
			foreach (string propertyName in array2)
			{
				TreeNode treeNode3 = new TreeNode(propertyName);
				treeNode3.ForeColor = Color.Silver;
				foreach (JToken item2 in value2[propertyName].Children())
				{
					TreeNode treeNode4 = new TreeNode(item2.ToString());
					treeNode4.ForeColor = colorFrom;
					treeNode3.Nodes.Add(treeNode4);
				}
				if (treeNode3.Nodes.Count > 0)
				{
					treeNode2.Nodes.Add(treeNode3);
				}
			}
		}
		TreeNode node2 = new TreeNode("Voice Set: " + CharData["VoiceSet"].ToString());
		TreeNode treeNode5 = new TreeNode("Executions");
		if (CharData["Executions_Offensive"].HasValues)
		{
			TreeNode treeNode6 = new TreeNode("Offensive");
			foreach (JToken item3 in CharData["Executions_Offensive"].Children())
			{
				TreeNode node3 = new TreeNode(item3.ToString());
				treeNode6.Nodes.Add(node3);
			}
			if (treeNode6.Nodes.Count > 0)
			{
				treeNode5.Nodes.Add(treeNode6);
			}
		}
		if (CharData["Executions_Defensive"].HasValues)
		{
			TreeNode treeNode7 = new TreeNode("Defensive");
			foreach (JToken item4 in CharData["Executions_Defensive"].Children())
			{
				TreeNode node4 = new TreeNode(item4.ToString());
				treeNode7.Nodes.Add(node4);
			}
			if (treeNode7.Nodes.Count > 0)
			{
				treeNode5.Nodes.Add(treeNode7);
			}
		}
		CharLstChatter1.Nodes.Add(treeNode);
		CharLstChatter1.Nodes.Add(treeNode2);
		CharLstChatter1.Nodes.Add(node2);
		if (treeNode5.Nodes.Count > 0)
		{
			CharLstChatter1.Nodes.Add(treeNode5);
		}
	}

	private void Skills_Populate(JObject CharData)
	{
		CharSkills1.Text = CharData["ShortName"].ToString();
		CharLists1.Nodes.Clear();
		TreeNode treeNode = new TreeNode("Behaviors");
		treeNode.Name = "Behaviors";
		foreach (JToken item in CharData["Behaviors"].Children())
		{
			string name = Enum.GetName(typeof(ModEnums.Behavior), item.ToObject<int>());
			treeNode.Nodes.Add(name, name);
		}
		CharLists1.Nodes.Add(treeNode);
		TreeNode treeNode2 = new TreeNode("Traits");
		treeNode2.Name = "Traits";
		foreach (JToken item2 in CharData["Traits"].Children())
		{
			string name2 = Enum.GetName(typeof(ModEnums.Trait), item2.ToObject<int>());
			if (Enum.TryParse<ModEnums.Trait>(Conversions.ToString(item2.ToObject<int>()), out var result))
			{
				CheckAttributesForTrait(result);
			}
			treeNode2.Nodes.Add(name2, name2);
		}
		CharLists1.Nodes.Add(treeNode2);
		TreeNode treeNode3 = new TreeNode("Vocations");
		treeNode3.Name = "Vocations";
		foreach (JToken item3 in CharData["Vocations"].Children())
		{
			string name3 = Enum.GetName(typeof(ModEnums.Vocation), item3.ToObject<int>());
			treeNode3.Nodes.Add(name3, name3);
		}
		CharLists1.Nodes.Add(treeNode3);
		TreeNode group = new TreeNode("Skills");
		group.Name = "Skills";
		SkillTree_Populate(CharData, ref group, CharData["RemoveSkills"]);
		CharLists1.Nodes.Add(group);
		TreeNode treeNode4 = new TreeNode("OMITTED");
		treeNode4.ForeColor = Color.LightSalmon;
		foreach (JToken item4 in (IEnumerable<JToken>)CharData["RemoveSkills"])
		{
			if (!(Enum.TryParse<ModEnums.Skill>(Conversions.ToString(item4.ToObject<int>()), out var result2) & ModEnums.SKILLS.ContainsKey(result2)))
			{
				continue;
			}
			JObject jObject = ModEnums.SKILLS[result2];
			TreeNode treeNode5 = new TreeNode(jObject["FullName"].ToString());
			EnableAttributeWithSkill(result2);
			treeNode5.Name = result2.ToString();
			if (Enum.TryParse<ModEnums.SkillTree>(jObject["SkillTree"].ToString(), out var result3))
			{
				switch (result3)
				{
				case ModEnums.SkillTree.C_Unarmed:
					treeNode5.ForeColor = Color.DarkKhaki;
					break;
				case ModEnums.SkillTree.C_Melee:
					treeNode5.ForeColor = Color.DarkSalmon;
					break;
				case ModEnums.SkillTree.C_Ranged:
					treeNode5.ForeColor = Color.SlateGray;
					break;
				case ModEnums.SkillTree.C_Acrobatic:
					treeNode5.ForeColor = Color.DarkSeaGreen;
					break;
				case ModEnums.SkillTree.O_Origin:
					treeNode5.ForeColor = Color.MediumPurple;
					break;
				}
			}
			treeNode4.Nodes.Add(treeNode5);
		}
		if (treeNode4.Nodes.Count > 0)
		{
			group.Nodes.Add(treeNode4);
		}
		CharListDesc1.Text = "Select an item to view its description.";
	}

	private void SkillTree_Populate(JObject SkillGroups, ref TreeNode group, JToken OmitSkills)
	{
		foreach (JToken item in SkillGroups["Skills"].Children())
		{
			bool flag = false;
			foreach (JToken item2 in (IEnumerable<JToken>)OmitSkills)
			{
				if (item.ToString().Equals(item2.ToString()))
				{
					flag = true;
				}
			}
			if (flag || !(Enum.TryParse<ModEnums.Skill>(Conversions.ToString(item.ToObject<int>()), out var result) & ModEnums.SKILLS.ContainsKey(result)))
			{
				continue;
			}
			JObject jObject = ModEnums.SKILLS[result];
			TreeNode treeNode = new TreeNode(jObject["FullName"].ToString());
			EnableAttributeWithSkill(result);
			treeNode.Name = result.ToString();
			if (Enum.TryParse<ModEnums.SkillTree>(jObject["SkillTree"].ToString(), out var result2))
			{
				switch (result2)
				{
				case ModEnums.SkillTree.C_Unarmed:
					treeNode.ForeColor = Color.DarkKhaki;
					break;
				case ModEnums.SkillTree.C_Melee:
					treeNode.ForeColor = Color.DarkSalmon;
					break;
				case ModEnums.SkillTree.C_Ranged:
					treeNode.ForeColor = Color.SlateGray;
					break;
				case ModEnums.SkillTree.C_Acrobatic:
					treeNode.ForeColor = Color.DarkSeaGreen;
					break;
				case ModEnums.SkillTree.O_Origin:
					treeNode.ForeColor = Color.MediumPurple;
					break;
				}
			}
			group.Nodes.Add(treeNode);
		}
		foreach (JToken item3 in SkillGroups["Groups"].Children())
		{
			string key = item3.ToString();
			if (ModEnums.DB_SKILLGROUPS.ContainsKey(key))
			{
				TreeNode group2 = new TreeNode(key);
				group2.ForeColor = Color.Silver;
				SkillTree_Populate(ModEnums.DB_SKILLGROUPS[key], ref group2, OmitSkills);
				group.Nodes.Add(group2);
			}
		}
	}

	private void CharLists1_AfterSelect(object sender, TreeViewEventArgs e)
	{
		TreeNode selectedNode = CharLists1.SelectedNode;
		while (selectedNode.Parent != null)
		{
			selectedNode = selectedNode.Parent;
		}
		if (CharLists1.SelectedNode.Nodes.Count == 0)
		{
			if (selectedNode.Name.Equals("Behaviors"))
			{
				if (Enum.TryParse<ModEnums.Behavior>(CharLists1.SelectedNode.Name, out var result))
				{
					CharListDesc1.Text = result.ToString() + "\r\n" + ModEnums.BEHAVIORS[result];
				}
			}
			else if (selectedNode.Name.Equals("Traits"))
			{
				if (Enum.TryParse<ModEnums.Trait>(CharLists1.SelectedNode.Name, out var result2))
				{
					CharListDesc1.Text = result2.ToString() + "\r\n" + ModEnums.TRAITS[result2];
				}
			}
			else if (selectedNode.Name.Equals("Vocations"))
			{
				if (Enum.TryParse<ModEnums.Vocation>(CharLists1.SelectedNode.Name, out var result3))
				{
					CharListDesc1.Text = result3.ToString() + "\r\n" + ModEnums.VOCATIONS[result3];
				}
			}
			else
			{
				if (!selectedNode.Name.Equals("Skills") || !(Enum.TryParse<ModEnums.Skill>(CharLists1.SelectedNode.Name, out var result4) & ModEnums.SKILLS.ContainsKey(result4)))
				{
					return;
				}
				JObject jObject = ModEnums.SKILLS[result4];
				string text = "Core";
				if (ModEnums.DB_SKILLTREES.ContainsKey(jObject["SkillTree"].ToString()))
				{
					string[] array = ModEnums.DB_SKILLTREES[jObject["SkillTree"].ToString()]["Categories"].ToObject<string[]>();
					if (jObject["CoreRank"].ToObject<int>() < 0)
					{
						text = array[jObject["Category"].ToObject<int>()];
					}
				}
				string text2 = "";
				if (jObject["Tier"].ToObject<int>() >= 0)
				{
					text2 = Conversions.ToString(checked(jObject["Tier"].ToObject<int>() + 1));
				}
				CharListDesc1.Text = jObject["FullName"].ToString() + ": " + text + " " + text2 + "\r\n" + jObject["Description"].ToString();
			}
		}
		else
		{
			CharListDesc1.Text = CharLists1.SelectedNode.Name;
		}
	}

	private string GetNameOfObject(string Selected, Dictionary<string, JObject> DB)
	{
		if (DB.ContainsKey(Selected) && DB[Selected]["FullName"] != null)
		{
			return DB[Selected]["FullName"].ToString();
		}
		return Selected;
	}

	private string GetWeapon(string Selected)
	{
		if (ModEnums.DB_MODDEDWEAPONS.ContainsKey(Selected))
		{
			return Selected;
		}
		return GetNameOfObject(Selected, ModEnums.DB_WEAPONS);
	}

	private void CheckAttributesForTrait(ModEnums.Trait currTrait)
	{
		switch (currTrait)
		{
		case ModEnums.Trait.TwoHandOverride:
			CharLstAttr1.Nodes["Attributes"].Nodes["Strength"].Nodes["WeaponSize"].ForeColor = Color.DimGray;
			break;
		case ModEnums.Trait.NoDizzy:
			CharLstAttr1.Nodes["Attributes"].Nodes["Endurance"].Nodes["ResistDizzy"].ForeColor = Color.DimGray;
			break;
		case ModEnums.Trait.NoPain:
			CharLstAttr1.Nodes["Attributes"].Nodes["Endurance"].Nodes["ResistShove"].ForeColor = Color.DimGray;
			break;
		case ModEnums.Trait.BulletsAutoHit:
			CharLstAttr1.Nodes["Attributes"].Nodes["Tactics"].Nodes["TacDamage"].ForeColor = Color.DimGray;
			break;
		case ModEnums.Trait.StableRanged:
			CharLstAttr1.Nodes["SubAttributes"].Nodes["Force"].Nodes["ReduceRecoil"].ForeColor = Color.DimGray;
			CharLstAttr1.Nodes["SubAttributes"].Nodes["Strive"].Nodes["RecoilRecovery"].ForeColor = Color.DimGray;
			break;
		case ModEnums.Trait.NoMelee:
			CharLstAttr1.Nodes["Attributes"].Nodes["Dexterity"].Nodes["WeaponSmallBonus"].ForeColor = Color.DimGray;
			CharLstAttr1.Nodes["SubAttributes"].Nodes["Force"].Nodes["WeaponBigPenalty"].ForeColor = Color.DimGray;
			CharLstAttr1.Nodes["SubAttributes"].Nodes["Lethality"].Nodes["MeleeDamage"].ForeColor = Color.DimGray;
			break;
		case ModEnums.Trait.NoCombatRagdoll:
			CharLstAttr1.Nodes["SubAttributes"].Nodes["Athletics"].Nodes["ResistKnockdown"].ForeColor = Color.DimGray;
			break;
		case ModEnums.Trait.Sluggish:
			CharLstAttr1.Nodes["SubAttributes"].Nodes["Senses"].Nodes["ReloadSpeed"].Text = "Reload Speed: " + Conversions.ToString(Math.Round(AttributeActions["ReloadSpeed"] * 0.75, 2) * 100.0) + "%";
			break;
		case ModEnums.Trait.NoShoot:
			CharLstAttr1.Nodes["Attributes"].Nodes["Tactics"].Nodes["TacDamage"].ForeColor = Color.DimGray;
			CharLstAttr1.Nodes["SubAttributes"].Nodes["Force"].Nodes["ReduceRecoil"].ForeColor = Color.DimGray;
			CharLstAttr1.Nodes["SubAttributes"].Nodes["Strive"].Nodes["RecoilRecovery"].ForeColor = Color.DimGray;
			CharLstAttr1.Nodes["SubAttributes"].Nodes["Senses"].Nodes["ReloadSpeed"].ForeColor = Color.DimGray;
			CharLstAttr1.Nodes["SubAttributes"].Nodes["Senses"].Nodes["RangedCritDamage"].ForeColor = Color.DimGray;
			CharLstAttr1.Nodes["SubAttributes"].Nodes["Senses"].Nodes["RangedAccuracy"].ForeColor = Color.DimGray;
			break;
		}
	}

	private void EnableAttributeWithSkill(ModEnums.Skill currSkill)
	{
		switch (currSkill)
		{
		case ModEnums.Skill.SpecialStrike:
		case ModEnums.Skill.DashAttack:
			CharLstAttr1.Nodes["Attributes"].Nodes["Strength"].Nodes["ShovePower"].ForeColor = SystemColors.Info;
			break;
		case ModEnums.Skill.TacticalNovice:
		case ModEnums.Skill.TacticalJourneyman:
		case ModEnums.Skill.TacticalMaster:
			CharLstAttr1.Nodes["Attributes"].Nodes["Dexterity"].Nodes["TacBarSize"].ForeColor = SystemColors.Info;
			CharLstAttr1.Nodes["Attributes"].Nodes["Tactics"].Nodes["TacRegen"].ForeColor = SystemColors.Info;
			break;
		case ModEnums.Skill.CritPunch:
		case ModEnums.Skill.CritSwing:
			CharLstAttr1.Nodes["SubAttributes"].Nodes["Critical"].Nodes["H2HCritDamage"].ForeColor = SystemColors.Info;
			break;
		case ModEnums.Skill.Dash:
			CharLstAttr1.Nodes["SubAttributes"].Nodes["Athletics"].Nodes["SprintSpeed"].ForeColor = SystemColors.Info;
			break;
		case ModEnums.Skill.BasicGuard:
		case ModEnums.Skill.BasicBlock:
			CharLstAttr1.Nodes["SubAttributes"].Nodes["Athletics"].Nodes["BlockPower"].ForeColor = SystemColors.Info;
			break;
		case ModEnums.Skill.GuardMobility:
		case ModEnums.Skill.BlockMobility:
		case ModEnums.Skill.MobileAim:
			CharLstAttr1.Nodes["SubAttributes"].Nodes["Senses"].Nodes["AimWalkSpeed"].ForeColor = SystemColors.Info;
			break;
		}
	}

	private Color GetColorFrom(JObject color)
	{
		checked
		{
			int red = (int)Math.Round(Math.Round(255.0 * double.Parse(color["r"].ToString())));
			int green = (int)Math.Round(Math.Round(255.0 * double.Parse(color["g"].ToString())));
			int blue = (int)Math.Round(Math.Round(255.0 * double.Parse(color["b"].ToString())));
			return Color.FromArgb(red, green, blue);
		}
	}

	private void BtnGlossary_Click(object sender, EventArgs e)
	{
		if (MyProject.Application.OpenForms["FormGlossary"] == null)
		{
			FormGlossary formGlossary = new FormGlossary();
			StudioModule.ShowDocument(formGlossary);
		}
        else { var existing=MyProject.Application.OpenForms["FormGlossary"];existing.WindowState=FormWindowState.Normal;existing.Activate(); }
	}

	private void BtnConvert_Click(object sender, EventArgs e)
	{
		if (MyProject.Application.OpenForms["FormEditorV2"] == null)
		{
			FormEditorV2 formEditorV = new FormEditorV2();
			StudioModule.ShowDocument(formEditorV);
		}
        else { var existing=MyProject.Application.OpenForms["FormEditorV2"];existing.WindowState=FormWindowState.Normal;existing.Activate(); }
	}
}
