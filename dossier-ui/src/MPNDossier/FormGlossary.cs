using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MPNDossier;

[DesignerGenerated]
public partial class FormGlossary : Form
{
	private IContainer components;

	[AccessedThroughProperty("GlossaryDesc1")]
	private RichTextBox _GlossaryDesc1;

	[AccessedThroughProperty("GlossarySelect")]
	private Label _GlossarySelect;

	[AccessedThroughProperty("GlossaryDef")]
	private TreeView _GlossaryDef;

	[AccessedThroughProperty("GlossaryItem")]
	private TreeView _GlossaryItem;

	internal virtual RichTextBox GlossaryDesc1
	{
		get
		{
			return _GlossaryDesc1;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			_GlossaryDesc1 = value;
		}
	}

	internal virtual Label GlossarySelect
	{
		get
		{
			return _GlossarySelect;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			_GlossarySelect = value;
		}
	}

	internal virtual TreeView GlossaryDef
	{
		get
		{
			return _GlossaryDef;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			TreeViewEventHandler value2 = GlossaryDef_AfterSelect;
			if (_GlossaryDef != null)
			{
				_GlossaryDef.AfterSelect -= value2;
			}
			_GlossaryDef = value;
			if (_GlossaryDef != null)
			{
				_GlossaryDef.AfterSelect += value2;
			}
		}
	}

	internal virtual TreeView GlossaryItem
	{
		get
		{
			return _GlossaryItem;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			_GlossaryItem = value;
		}
	}

	public FormGlossary()
	{
		Load += FormGlossary_Load;
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
		System.Windows.Forms.TreeNode treeNode = new System.Windows.Forms.TreeNode("Words");
		this.GlossaryDesc1 = new System.Windows.Forms.RichTextBox();
		this.GlossarySelect = new System.Windows.Forms.Label();
		this.GlossaryDef = new System.Windows.Forms.TreeView();
		this.GlossaryItem = new System.Windows.Forms.TreeView();
		this.SuspendLayout();
		this.GlossaryDesc1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
		this.GlossaryDesc1.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.GlossaryDesc1.Font = new System.Drawing.Font("Impact", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.GlossaryDesc1.ForeColor = System.Drawing.SystemColors.Control;
		System.Windows.Forms.RichTextBox glossaryDesc = this.GlossaryDesc1;
		System.Drawing.Point location = new System.Drawing.Point(0, 350);
		glossaryDesc.Location = location;
		this.GlossaryDesc1.Name = "GlossaryDesc1";
		this.GlossaryDesc1.ReadOnly = true;
		this.GlossaryDesc1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
		System.Windows.Forms.RichTextBox glossaryDesc2 = this.GlossaryDesc1;
		System.Drawing.Size size = new System.Drawing.Size(624, 111);
		glossaryDesc2.Size = size;
		this.GlossaryDesc1.TabIndex = 14;
		this.GlossaryDesc1.Text = "No description available...";
		this.GlossarySelect.AutoSize = true;
		this.GlossarySelect.Dock = System.Windows.Forms.DockStyle.Top;
		this.GlossarySelect.Font = new System.Drawing.Font("Impact", 18f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.GlossarySelect.ForeColor = System.Drawing.SystemColors.Control;
		System.Windows.Forms.Label glossarySelect = this.GlossarySelect;
		location = new System.Drawing.Point(0, 0);
		glossarySelect.Location = location;
		this.GlossarySelect.Name = "GlossarySelect";
		System.Windows.Forms.Label glossarySelect2 = this.GlossarySelect;
		size = new System.Drawing.Size(166, 29);
		glossarySelect2.Size = size;
		this.GlossarySelect.TabIndex = 13;
		this.GlossarySelect.Text = "Select Category";
		this.GlossaryDef.BackColor = System.Drawing.Color.Black;
		this.GlossaryDef.Dock = System.Windows.Forms.DockStyle.Left;
		this.GlossaryDef.Font = new System.Drawing.Font("Impact", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.GlossaryDef.ForeColor = System.Drawing.SystemColors.Info;
		System.Windows.Forms.TreeView glossaryDef = this.GlossaryDef;
		location = new System.Drawing.Point(0, 29);
		glossaryDef.Location = location;
		this.GlossaryDef.Name = "GlossaryDef";
		System.Windows.Forms.TreeView glossaryDef2 = this.GlossaryDef;
		size = new System.Drawing.Size(238, 321);
		glossaryDef2.Size = size;
		this.GlossaryDef.TabIndex = 16;
		this.GlossaryItem.BackColor = System.Drawing.Color.Black;
		this.GlossaryItem.Dock = System.Windows.Forms.DockStyle.Fill;
		this.GlossaryItem.Font = new System.Drawing.Font("Impact", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.GlossaryItem.ForeColor = System.Drawing.SystemColors.Info;
		System.Windows.Forms.TreeView glossaryItem = this.GlossaryItem;
		location = new System.Drawing.Point(238, 29);
		glossaryItem.Location = location;
		this.GlossaryItem.Name = "GlossaryItem";
		treeNode.Name = "Node1";
		treeNode.Text = "Words";
		this.GlossaryItem.Nodes.AddRange(new System.Windows.Forms.TreeNode[1] { treeNode });
		System.Windows.Forms.TreeView glossaryItem2 = this.GlossaryItem;
		size = new System.Drawing.Size(386, 321);
		glossaryItem2.Size = size;
		this.GlossaryItem.TabIndex = 17;
		System.Drawing.SizeF sizeF = new System.Drawing.SizeF(6f, 13f);
		this.AutoScaleDimensions = sizeF;
		this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.AutoSize = true;
		this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
		this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
		size = new System.Drawing.Size(624, 461);
		this.ClientSize = size;
		this.Controls.Add(this.GlossaryItem);
		this.Controls.Add(this.GlossaryDef);
		this.Controls.Add(this.GlossaryDesc1);
		this.Controls.Add(this.GlossarySelect);
		size = new System.Drawing.Size(640, 500);
		this.MinimumSize = size;
		this.Name = "FormGlossary";
		this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "Madness: Project Nexus Glossary";
		this.ResumeLayout(false);
		this.PerformLayout();
	}

	private void FormGlossary_Load(object sender, EventArgs e)
	{
		loadBaseNode("Ammo", "Ammo", ModEnums.DB_AMMO);
		loadBaseNode("Animations", "Animations", ModEnums.DB_ANIMATIONS);
		loadBaseNode("AnimSets", "Animation Sets", ModEnums.DB_ANIMSETS);
		loadBaseNode("Arenas", "Arenas", ModEnums.DB_ARENAS);
		TreeNode treeNode = new TreeNode("Behavior");
		treeNode.Name = "Behavior";
		foreach (KeyValuePair<ModEnums.Behavior, string> bEHAVIOR in ModEnums.BEHAVIORS)
		{
			TreeNode treeNode2 = new TreeNode(bEHAVIOR.Key.ToString());
			treeNode2.Name = bEHAVIOR.Key.ToString();
			treeNode.Nodes.Add(treeNode2);
		}
		GlossaryDef.Nodes.Add(treeNode);
		TreeNode treeNode3 = new TreeNode("Characters");
		treeNode3.Name = "Characters";
		foreach (KeyValuePair<string, JObject> item in ModEnums.DB_CHAR)
		{
			TreeNode treeNode4 = new TreeNode(item.Value["FullName"].ToString());
			treeNode4.Name = item.Key;
			category(treeNode3, Enum.GetName(typeof(ModEnums.Faction), item.Value["DefaultFaction"].ToObject<int>())).Nodes.Add(treeNode4);
		}
		GlossaryDef.Nodes.Add(treeNode3);
		loadBaseNode("Chatter", "Chatter", ModEnums.DB_CHATTER);
		loadBaseNode("Collectibles", "Collectibles", ModEnums.DB_COLLECTIBLES);
		loadBaseNode("Corpus", "Corpus", ModEnums.DB_CORPUS);
		loadBaseNode("Executions", "Executions", ModEnums.DB_EXECUTION);
		loadBaseNode("Explosions", "Explosions", ModEnums.DB_EXPLOSION);
		TreeNode treeNode5 = new TreeNode("Gear");
		treeNode5.Name = "Gear";
		foreach (KeyValuePair<string, JObject> item2 in ModEnums.DB_GEAR)
		{
			TreeNode treeNode6 = new TreeNode(item2.Value["FullName"].ToString());
			treeNode6.Name = item2.Key;
			int num = Convert.ToInt32(item2.Value["Slot"].ToString());
			if (Enum.IsDefined(typeof(ModEnums.ArmorSlots), num))
			{
				category(treeNode5, Enum.GetName(typeof(ModEnums.ArmorSlots), num)).Nodes.Add(treeNode6);
			}
		}
		GlossaryDef.Nodes.Add(treeNode5);
		loadBaseNode("Loadouts", "Loadouts", ModEnums.DB_LOADOUTS);
		TreeNode treeNode7 = new TreeNode("Origins");
		treeNode7.Name = "Origins";
		foreach (KeyValuePair<string, JObject> dB_ORIGIN in ModEnums.DB_ORIGINS)
		{
			TreeNode treeNode8 = new TreeNode(dB_ORIGIN.Value["FullName"].ToString());
			treeNode8.Name = dB_ORIGIN.Key;
			treeNode7.Nodes.Add(treeNode8);
		}
		GlossaryDef.Nodes.Add(treeNode7);
		TreeNode treeNode9 = new TreeNode("Skills");
		treeNode9.Name = "Skills";
		foreach (KeyValuePair<string, JObject> dB_SKILL in ModEnums.DB_SKILLS)
		{
			TreeNode treeNode10 = new TreeNode(dB_SKILL.Value["FullName"].ToString());
			treeNode10.Name = dB_SKILL.Key;
			category(treeNode9, dB_SKILL.Value["SkillTree"].ToString()).Nodes.Add(treeNode10);
		}
		GlossaryDef.Nodes.Add(treeNode9);
		loadBaseNode("SkillGroups", "Skill Groups", ModEnums.DB_SKILLGROUPS);
		loadBaseNode("Skins", "Skins", ModEnums.DB_SKINS);
		loadBaseNode("Stories", "Stories", ModEnums.DB_STORIES);
		loadBaseNode("Swatches", "Swatches", ModEnums.DB_SWATCHES);
		TreeNode treeNode11 = new TreeNode("Traits");
		treeNode11.Name = "Traits";
		TreeNode treeNode12 = new TreeNode("Character");
		treeNode12.Name = "CharacterTraits";
		foreach (KeyValuePair<ModEnums.Trait, string> tRAIT in ModEnums.TRAITS)
		{
			TreeNode treeNode13 = new TreeNode(tRAIT.Key.ToString());
			treeNode13.Name = tRAIT.Key.ToString();
			treeNode12.Nodes.Add(treeNode13);
		}
		treeNode11.Nodes.Add(treeNode12);
		TreeNode treeNode14 = new TreeNode("Weapon");
		treeNode14.Name = "WeaponTraits";
		foreach (KeyValuePair<ModEnums.WeaponTraits, string> wEAPTRAIT in ModEnums.WEAPTRAITS)
		{
			TreeNode treeNode15 = new TreeNode(wEAPTRAIT.Key.ToString());
			treeNode15.Name = wEAPTRAIT.Key.ToString();
			treeNode14.Nodes.Add(treeNode15);
		}
		treeNode11.Nodes.Add(treeNode14);
		TreeNode treeNode16 = new TreeNode("Armor");
		treeNode16.Name = "ArmorTraits";
		foreach (KeyValuePair<ModEnums.ArmorTraits, string> aRMTRAIT in ModEnums.ARMTRAITS)
		{
			TreeNode treeNode17 = new TreeNode(aRMTRAIT.Key.ToString());
			treeNode17.Name = aRMTRAIT.Key.ToString();
			treeNode16.Nodes.Add(treeNode17);
		}
		treeNode11.Nodes.Add(treeNode16);
		GlossaryDef.Nodes.Add(treeNode11);
		TreeNode treeNode18 = new TreeNode("Vocations");
		treeNode18.Name = "Vocations";
		foreach (KeyValuePair<ModEnums.Vocation, string> vOCATION in ModEnums.VOCATIONS)
		{
			TreeNode treeNode19 = new TreeNode(vOCATION.Key.ToString());
			treeNode19.Name = vOCATION.Key.ToString();
			treeNode18.Nodes.Add(treeNode19);
		}
		GlossaryDef.Nodes.Add(treeNode18);
		loadBaseNode("VoiceSets", "Voice Sets", ModEnums.DB_VOICESETS);
		loadBaseNode("Wardrobe", "Wardrobe", ModEnums.DB_WARDROBE);
		TreeNode treeNode20 = new TreeNode("Weapons");
		treeNode20.Name = "Weapons";
		foreach (KeyValuePair<string, JObject> dB_WEAPON in ModEnums.DB_WEAPONS)
		{
			TreeNode treeNode21 = new TreeNode((dB_WEAPON.Value["FullName"].ToString().Length > 0) ? dB_WEAPON.Value["FullName"].ToString() : dB_WEAPON.Key);
			treeNode21.Name = dB_WEAPON.Key;
			int num2 = ((dB_WEAPON.Value["Category"] != null) ? Convert.ToInt32(dB_WEAPON.Value["Category"].ToString()) : 100);
			if (Enum.IsDefined(typeof(ModEnums.WeaponCategories), num2))
			{
				category(treeNode20, Enum.GetName(typeof(ModEnums.WeaponCategories), num2)).Nodes.Add(treeNode21);
			}
		}
		GlossaryDef.Nodes.Add(treeNode20);
		TreeNode treeNode22 = new TreeNode("Weapon Addons");
		treeNode22.Name = "Weaponaddons";
		foreach (KeyValuePair<string, JObject> dB_WEAPONADDON in ModEnums.DB_WEAPONADDONS)
		{
			TreeNode treeNode23 = new TreeNode(dB_WEAPONADDON.Value["FullName"].ToString());
			treeNode23.Name = dB_WEAPONADDON.Key;
			treeNode22.Nodes.Add(treeNode23);
		}
		GlossaryDef.Nodes.Add(treeNode22);
	}

	private void loadBaseNode(string Name, string Text, Dictionary<string, JObject> Database)
	{
		TreeNode treeNode = new TreeNode(Text);
		treeNode.Name = Name;
		foreach (KeyValuePair<string, JObject> item in Database)
		{
			TreeNode treeNode2 = new TreeNode(item.Key);
			treeNode2.Name = item.Key;
			treeNode.Nodes.Add(treeNode2);
		}
		GlossaryDef.Nodes.Add(treeNode);
	}

	private void GlossaryDef_AfterSelect(object sender, TreeViewEventArgs e)
	{
		GlossaryDesc1.Text = "No Description Available.";
		string name = GlossaryDef.SelectedNode.Name;
		TreeNode selectedNode = GlossaryDef.SelectedNode;
		while (selectedNode.Parent != null)
		{
			selectedNode = selectedNode.Parent;
		}
		GlossaryItem.Nodes.Clear();
		if (GlossaryDef.SelectedNode.Nodes.Count != 0)
		{
			return;
		}
		if (selectedNode.Name.Equals("Characters") & ModEnums.DB_CHAR.Keys.Contains(name))
		{
			defineObject(ModEnums.DB_CHAR[name], GlossaryItem.Nodes);
		}
		else if (selectedNode.Name.Equals("Origins") & ModEnums.DB_ORIGINS.Keys.Contains(name))
		{
			defineObject(ModEnums.DB_ORIGINS[name], GlossaryItem.Nodes);
		}
		else if (selectedNode.Name.Equals("Weapons"))
		{
			if (ModEnums.DB_WEAPONS.Keys.Contains(name))
			{
				defineObject(ModEnums.DB_WEAPONS[name], GlossaryItem.Nodes);
			}
		}
		else if (selectedNode.Name.Equals("Weaponaddons") & ModEnums.DB_WEAPONADDONS.Keys.Contains(name))
		{
			defineObject(ModEnums.DB_WEAPONADDONS[name], GlossaryItem.Nodes);
		}
		else if (selectedNode.Name.Equals("Loadouts") & ModEnums.DB_LOADOUTS.Keys.Contains(name))
		{
			defineObject(ModEnums.DB_LOADOUTS[name], GlossaryItem.Nodes);
		}
		else if (selectedNode.Name.Equals("Gear") & ModEnums.DB_GEAR.Keys.Contains(name))
		{
			defineObject(ModEnums.DB_GEAR[name], GlossaryItem.Nodes);
		}
		else if (selectedNode.Name.Equals("Swatches") & ModEnums.DB_SWATCHES.Keys.Contains(name))
		{
			defineObject(ModEnums.DB_SWATCHES[name], GlossaryItem.Nodes);
		}
		else if (selectedNode.Name.Equals("Wardrobe") & ModEnums.DB_WARDROBE.Keys.Contains(name))
		{
			defineObject(ModEnums.DB_WARDROBE[name], GlossaryItem.Nodes);
		}
		else if (selectedNode.Name.Equals("Collectibles") & ModEnums.DB_COLLECTIBLES.Keys.Contains(name))
		{
			defineObject(ModEnums.DB_COLLECTIBLES[name], GlossaryItem.Nodes);
		}
		else if (selectedNode.Name.Equals("Chatter") & ModEnums.DB_CHATTER.Keys.Contains(name))
		{
			defineObject(ModEnums.DB_CHATTER[name], GlossaryItem.Nodes);
		}
		else if (selectedNode.Name.Equals("Corpus") & ModEnums.DB_CORPUS.Keys.Contains(name))
		{
			defineObject(ModEnums.DB_CORPUS[name], GlossaryItem.Nodes);
		}
		else if (selectedNode.Name.Equals("Skins") & ModEnums.DB_SKINS.Keys.Contains(name))
		{
			defineObject(ModEnums.DB_SKINS[name], GlossaryItem.Nodes);
		}
		else if (selectedNode.Name.Equals("VoiceSets") & ModEnums.DB_VOICESETS.Keys.Contains(name))
		{
			defineObject(ModEnums.DB_VOICESETS[name], GlossaryItem.Nodes);
		}
		else if (selectedNode.Name.Equals("Animations") & ModEnums.DB_ANIMATIONS.Keys.Contains(name))
		{
			defineObject(ModEnums.DB_ANIMATIONS[name], GlossaryItem.Nodes);
		}
		else if (selectedNode.Name.Equals("Explosions") & ModEnums.DB_EXPLOSION.Keys.Contains(name))
		{
			defineObject(ModEnums.DB_EXPLOSION[name], GlossaryItem.Nodes);
		}
		else if (selectedNode.Name.Equals("Executions") & ModEnums.DB_EXECUTION.Keys.Contains(name))
		{
			defineObject(ModEnums.DB_EXECUTION[name], GlossaryItem.Nodes);
		}
		else if (selectedNode.Name.Equals("AnimSets") & ModEnums.DB_ANIMSETS.Keys.Contains(name))
		{
			defineObject(ModEnums.DB_ANIMSETS[name], GlossaryItem.Nodes);
		}
		else if (selectedNode.Name.Equals("Behavior"))
		{
			if (Enum.TryParse<ModEnums.Behavior>(GlossaryDef.SelectedNode.Name, out var result))
			{
				GlossaryDesc1.Text = ModEnums.BEHAVIORS[result];
			}
		}
		else if (selectedNode.Name.Equals("Traits"))
		{
			string name2 = GlossaryDef.SelectedNode.Parent.Name;
			if (Operators.CompareString(name2, "CharacterTraits", TextCompare: false) == 0 && Enum.TryParse<ModEnums.Trait>(GlossaryDef.SelectedNode.Name, out var result2))
			{
				GlossaryDesc1.Text = ModEnums.TRAITS[result2];
			}
			if (Operators.CompareString(name2, "WeaponTraits", TextCompare: false) == 0 && Enum.TryParse<ModEnums.WeaponTraits>(GlossaryDef.SelectedNode.Name, out var result3))
			{
				GlossaryDesc1.Text = ModEnums.WEAPTRAITS[result3];
			}
			if (Operators.CompareString(name2, "ArmorTraits", TextCompare: false) == 0 && Enum.TryParse<ModEnums.ArmorTraits>(GlossaryDef.SelectedNode.Name, out var result4))
			{
				GlossaryDesc1.Text = ModEnums.ARMTRAITS[result4];
			}
		}
		else if (selectedNode.Name.Equals("Vocations"))
		{
			if (Enum.TryParse<ModEnums.Vocation>(GlossaryDef.SelectedNode.Name, out var result5))
			{
				GlossaryDesc1.Text = ModEnums.VOCATIONS[result5];
			}
		}
		else if (selectedNode.Name.Equals("Stories") & ModEnums.DB_STORIES.Keys.Contains(name))
		{
			defineObject(ModEnums.DB_STORIES[name], GlossaryItem.Nodes);
		}
		else if (selectedNode.Name.Equals("Arenas") & ModEnums.DB_ARENAS.Keys.Contains(name))
		{
			defineObject(ModEnums.DB_ARENAS[name], GlossaryItem.Nodes);
		}
		else if (selectedNode.Name.Equals("Skills") & ModEnums.DB_SKILLS.Keys.Contains(name))
		{
			defineObject(ModEnums.DB_SKILLS[name], GlossaryItem.Nodes);
		}
		else if (selectedNode.Name.Equals("SkillGroups") & ModEnums.DB_SKILLGROUPS.Keys.Contains(name))
		{
			defineObject(ModEnums.DB_SKILLGROUPS[name], GlossaryItem.Nodes);
		}
	}

	private void defineObject(JObject o, TreeNodeCollection nodeSpace)
	{
		JTokenReader jTokenReader = (JTokenReader)o.CreateReader();
		TreeNode treeNode = null;
		TreeNodeCollection treeNodeCollection = nodeSpace;
		bool flag = false;
		while (jTokenReader.Read())
		{
			if (jTokenReader.TokenType.Equals(JsonToken.PropertyName))
			{
				if (jTokenReader.Value.Equals("Description"))
				{
					flag = true;
					continue;
				}
				treeNode = new TreeNode(Conversions.ToString(jTokenReader.Value));
				treeNode.Name = Conversions.ToString(jTokenReader.Value);
				treeNodeCollection.Add(treeNode);
			}
			else if (treeNode != null)
			{
				if (jTokenReader.TokenType.Equals(JsonToken.StartArray) | jTokenReader.TokenType.Equals(JsonToken.StartObject))
				{
					if (treeNodeCollection == treeNode.Nodes)
					{
						treeNode = new TreeNode("Object");
						treeNodeCollection.Add(treeNode);
					}
					treeNodeCollection = treeNode.Nodes;
				}
				else if (jTokenReader.TokenType.Equals(JsonToken.EndArray) | jTokenReader.TokenType.Equals(JsonToken.EndObject))
				{
					if (treeNode.Parent != null)
					{
						treeNode = treeNode.Parent;
						treeNodeCollection = treeNode.Nodes;
					}
					else
					{
						treeNode = null;
						treeNodeCollection = nodeSpace;
					}
				}
				else
				{
					if (jTokenReader.Value == null)
					{
						continue;
					}
					if (treeNodeCollection == treeNode.Nodes)
					{
						treeNodeCollection.Add(new TreeNode(Conversions.ToString(jTokenReader.Value)));
						continue;
					}
					treeNode.Text = Conversions.ToString(Operators.ConcatenateObject(treeNode.Text + ":    ", jTokenReader.Value));
					if (treeNode.Parent != null)
					{
						treeNode = treeNode.Parent;
						treeNodeCollection = treeNode.Nodes;
					}
					else
					{
						treeNode = null;
						treeNodeCollection = nodeSpace;
					}
				}
			}
			else if (flag & (jTokenReader.Value != null))
			{
				GlossaryDesc1.Text = Conversions.ToString(jTokenReader.Value);
				flag = false;
			}
		}
	}

	private TreeNode category(TreeNode root, string key)
	{
		foreach (TreeNode node in root.Nodes)
		{
			if (node.Name.Equals(key))
			{
				return node;
			}
		}
		TreeNode treeNode2 = new TreeNode(key);
		treeNode2.Name = key;
		root.Nodes.Add(treeNode2);
		return treeNode2;
	}
}
