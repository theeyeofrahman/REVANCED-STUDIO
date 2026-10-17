using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.VisualBasic.CompilerServices;

namespace MPNDossier;

[StandardModule]
public sealed class ModCards
{
	public enum NodeType
	{
		Val,
		Arr,
		Obj
	}

	public enum PropType
	{
		Bool,
		Int,
		Float,
		Str,
		Color,
		Ref,
		Ord,
		Obj
	}

	public class PropertyControl
	{
		public string PropName;

		public PropType PropType;

		public bool IsList;

		public object Selection;

		public string ObjectName;

		public bool Fixed;
	}

	public class NodeTag
	{
		public PropertyControl LinkedControl;

		public object Value;

		public NodeType NodeType;

		public NodeTag(PropertyControl Control, NodeType type)
		{
			LinkedControl = Control;
			NodeType = type;
		}

		public override string ToString()
		{
			if (LinkedControl.PropType != PropType.Obj)
			{
				return Value.ToString();
			}
			return LinkedControl.ObjectName;
		}
	}

	public class ColorRGBA
	{
		public double r;

		public double g;

		public double b;

		public double a;

		public ColorRGBA()
		{
			r = 1.0;
			g = 1.0;
			b = 1.0;
			a = 1.0;
		}

		public override string ToString()
		{
			return ColorTranslator.ToHtml(checked(Color.FromArgb((int)Math.Round(Math.Round(a * 255.0)), (int)Math.Round(Math.Round(r * 255.0)), (int)Math.Round(Math.Round(g * 255.0)), (int)Math.Round(Math.Round(b * 255.0)))));
		}
	}

	public delegate PropertyControl[] PropertyList();

	public static PropertyControl[] Obj_ArmorList()
	{
		return new PropertyControl[1] { NewControl("Armor", PropType.Ref, list: true, Nullable(ModEnums.DB_GEAR.Keys.ToArray())) };
	}

	public static PropertyControl[] Obj_UniformPicks()
	{
		return new PropertyControl[1] { NewControl("Armor", PropType.Ref, list: true, ModEnums.DB_GEAR.Keys.ToArray()) };
	}

	public static PropertyControl[] Obj_VectorList()
	{
		return new PropertyControl[2]
		{
			NewControl("x", PropType.Float),
			NewControl("y", PropType.Float)
		};
	}

	public static PropertyControl[] Obj_Vector3List()
	{
		return new PropertyControl[3]
		{
			NewControl("x", PropType.Float),
			NewControl("y", PropType.Float),
			NewControl("z", PropType.Float)
		};
	}

	public static PropertyControl[] Obj_UniformList()
	{
		return new PropertyControl[6]
		{
			NewControl("ArmorPicks", PropType.Obj, list: false, Obj_UniformPicks(), "UniformList"),
			NewControl("Tint", PropType.Color),
			NewControl("Tint2", PropType.Color),
			NewControl("Glass", PropType.Color),
			NewControl("AltMaterialIndex", PropType.Int),
			NewControl("AltMeshIndex", PropType.Int)
		};
	}

	public static PropertyControl[] Obj_MaterialSet()
	{
		return new PropertyControl[1] { NewControl("Materials", PropType.Str, list: true) };
	}

	public static PropertyControl[] Obj_MaterialSwaps()
	{
		return new PropertyControl[2]
		{
			NewControl("parts", PropType.Ord, list: true, Enum.GetValues(typeof(ModEnums.ModelParts))),
			NewControl("materials", PropType.Ref, list: true, ModEnums.MATERIALS.ToArray())
		};
	}

	public static PropertyControl[] Obj_AnimationClip()
	{
		return new PropertyControl[2]
		{
			NewControl("originalClip", PropType.Ref, list: false, ModEnums.ANIMATIONCLIPS.ToArray()),
			NewControl("overrideClip", PropType.Ref, list: false, ModEnums.ANIMATIONCLIPS.ToArray())
		};
	}

	public static PropertyControl[] Obj_ExecutionClip()
	{
		return new PropertyControl[2]
		{
			NewControl("Leader", PropType.Ref, list: false, ModEnums.ANIMATIONCLIPS.ToArray()),
			NewControl("Target", PropType.Ref, list: false, ModEnums.ANIMATIONCLIPS.ToArray())
		};
	}

	public static PropertyControl[] Obj_DeathEvent()
	{
		return new PropertyControl[17]
		{
			NewControl("PreventRegen", PropType.Ord, list: true, Enum.GetValues(typeof(ModEnums.DeathFlags))),
			NewControl("ChanceToFail", PropType.Float),
			NewControl("Retries", PropType.Int),
			NewControl("RegenMaxHealth", PropType.Int),
			NewControl("ReplaceFinalCorpus", PropType.Ref, list: false, Nullable(ModEnums.DB_CORPUS.Keys.ToArray())),
			NewControl("RezInertRagdoll", PropType.Bool),
			NewControl("PreventDeathAnimation", PropType.Bool),
			NewControl("NoAdvanceOnFail", PropType.Bool),
			NewControl("LastCorpusOnly", PropType.Bool),
			NewControl("Invincibility", PropType.Float),
			NewControl("InvincibilityType", PropType.Int),
			NewControl("GiveSkills", PropType.Ref, list: false, Nullable(ModEnums.DB_SKILLGROUPS.Keys.ToArray())),
			NewControl("DestroyLinkedArmor", PropType.Ord, list: true, Enum.GetValues(typeof(ModEnums.ArmorSlots))),
			NewControl("RecoverAnimation", PropType.Ref, list: false, Nullable(ModEnums.DB_ANIMATIONS.Keys.ToArray())),
			NewControl("FlashColor", PropType.Color),
			NewControl("FlashTimes", PropType.Int),
			NewControl("playSFX", PropType.Str)
		};
	}

	public static PropertyControl[] Obj_BonusStats()
	{
		return new PropertyControl[6]
		{
			NewControl("Name", PropType.Str),
			NewControl("Description", PropType.Str),
			NewControl("Type", PropType.Ord, list: false, Enum.GetValues(typeof(ModEnums.BonusType))),
			NewControl("Amount", PropType.Float),
			NewControl("amPercent", PropType.Bool),
			NewControl("markAsGood", PropType.Bool)
		};
	}

	public static PropertyControl[] Obj_BuffEffect()
	{
		return new PropertyControl[3]
		{
			NewControl("Effect", PropType.Ord, list: false, Enum.GetValues(typeof(ModEnums.BuffEffects))),
			NewControl("Chance", PropType.Float),
			NewControl("Duration", PropType.Float)
		};
	}

	public static PropertyControl[] Obj_ColorPair()
	{
		return new PropertyControl[3]
		{
			NewControl("Name", PropType.Str),
			NewControl("Color1", PropType.Color),
			NewControl("Color2", PropType.Color)
		};
	}

	public static PropertyControl[] Obj_ColorSolo()
	{
		return new PropertyControl[2]
		{
			NewControl("Name", PropType.Str),
			NewControl("Color1", PropType.Color)
		};
	}

	public static PropertyControl[] Obj_CharacterSet()
	{
		return new PropertyControl[3]
		{
			NewControl("MyCharacter", PropType.Ref, list: false, ModEnums.DB_CHAR.Keys.ToArray()),
			NewControl("DefaultLoadout", PropType.Ref, list: false, Nullable(ModEnums.DB_STORYLOADOUTS.Keys.ToArray())),
			NewControl("DefaultWardrobe", PropType.Ref, list: false, Nullable(ModEnums.DB_WARDROBE.Keys.ToArray()))
		};
	}

	public static PropertyControl[] Obj_WaveChar()
	{
		return new PropertyControl[4]
		{
			NewControl("Type", PropType.Ref, list: false, ModEnums.DB_CHAR.Keys.ToArray()),
			NewControl("Total", PropType.Int),
			NewControl("GenerationScale", PropType.Float),
			NewControl("AddUnitsPerWave", PropType.Float)
		};
	}

	public static PropertyControl[] Obj_SingleChar()
	{
		return new PropertyControl[7]
		{
			NewControl("Type", PropType.Ref, list: false, ModEnums.DB_CHAR.Keys.ToArray()),
			NewControl("Weapon", PropType.Ref, list: false, Nullable(ModEnums.DB_WEAPONS.Keys.ToArray())),
			NewControl("Sidearm", PropType.Ref, list: false, Nullable(ModEnums.DB_WEAPONS.Keys.ToArray())),
			NewControl("Throwable", PropType.Ref, list: false, Nullable(ModEnums.DB_THROWN.Keys.ToArray())),
			NewControl("Collectible", PropType.Ref, list: false, Nullable(ModEnums.DB_COLLECTIBLES.Keys.ToArray())),
			NewControl("Gear", PropType.Ref, list: true, ModEnums.DB_GEAR.Keys.ToArray()),
			NewControl("Patterns", PropType.Obj, list: true, Obj_BossPatern(), "Pattern")
		};
	}

	public static PropertyControl[] Obj_WeaponLoadout()
	{
		return new PropertyControl[2]
		{
			NewControl("Loadout", PropType.Ref, list: false, ModEnums.DB_LOADOUTS.Keys.ToArray()),
			NewControl("Count", PropType.Int)
		};
	}

	public static PropertyControl[] Obj_CustomSquad()
	{
		return new PropertyControl[2]
		{
			NewControl("UnitSpawn", PropType.Obj, list: true, Obj_SingleCharacter(), "Character"),
			NewControl("SpawnOrder", PropType.Int)
		};
	}

	public static PropertyControl[] Obj_WaveCharacter()
	{
		return new PropertyControl[3]
		{
			NewControl("Type", PropType.Ref, list: false, ModEnums.DB_CHAR.Keys.ToArray()),
			NewControl("Total", PropType.Int),
			NewControl("GenerationScale", PropType.Float)
		};
	}

	public static PropertyControl[] Obj_SingleCharacter()
	{
		return new PropertyControl[6]
		{
			NewControl("Type", PropType.Ref, list: false, ModEnums.DB_CHAR.Keys.ToArray()),
			NewControl("Weapon", PropType.Ref, list: false, Nullable(ModEnums.DB_WEAPONS.Keys.ToArray())),
			NewControl("Sidearm", PropType.Ref, list: false, Nullable(ModEnums.DB_WEAPONS.Keys.ToArray())),
			NewControl("Throwable", PropType.Ref, list: false, Nullable(ModEnums.DB_THROWN.Keys.ToArray())),
			NewControl("Collectible", PropType.Ref, list: false, Nullable(ModEnums.DB_COLLECTIBLES.Keys.ToArray())),
			NewControl("Gear", PropType.Ref, list: true, ModEnums.DB_GEAR.Keys.ToArray())
		};
	}

	public static PropertyControl[] Obj_Squad()
	{
		return new PropertyControl[7]
		{
			NewControl("MaxSquadSize", PropType.Int),
			NewControl("MaxUnitCount", PropType.Int),
			NewControl("WeaponStock_RandomSeed", PropType.Int),
			NewControl("UnitTypes", PropType.Obj, list: true, Obj_WaveCharacter(), "Unit"),
			NewControl("WeaponStock", PropType.Obj, list: true, Obj_WeaponLoadout(), "Loadout"),
			NewControl("Bosses", PropType.Obj, list: true, Obj_SingleCharacter(), "Boss"),
			NewControl("CustomSquads", PropType.Obj, list: true, Obj_CustomSquad(), "Squad")
		};
	}

	public static PropertyControl[] Obj_ArenaSequence()
	{
		return new PropertyControl[7]
		{
			NewControl("TotalWaves", PropType.Int),
			NewControl("Cash", PropType.Int),
			NewControl("XP", PropType.Int),
			NewControl("WaveMult_Cash", PropType.Float),
			NewControl("WaveMult_XP", PropType.Float),
			NewControl("Units", PropType.Obj, list: true, Obj_WaveChar(), "Unit"),
			NewControl("Bosses", PropType.Obj, list: true, Obj_SingleChar(), "Boss")
		};
	}

	public static PropertyControl[] Obj_BossPatern()
	{
		return new PropertyControl[11]
		{
			NewControl("PatternName", PropType.Str),
			NewControl("BossSays", PropType.Str),
			NewControl("ConstantModules", PropType.Ref, list: true, ModEnums.MODULES.ToArray()),
			NewControl("PhaseModules", PropType.Ref, list: true, ModEnums.MODULES.ToArray()),
			NewControl("RemoveModules", PropType.Ref, list: true, ModEnums.MODULES.ToArray()),
			NewControl("OneShots", PropType.Ref, list: true, ModEnums.ONESHOTS.ToArray()),
			NewControl("BehaviorAdd", PropType.Ord, list: true, Enum.GetValues(typeof(ModEnums.Behavior))),
			NewControl("BehaviorRemove", PropType.Ord, list: true, Enum.GetValues(typeof(ModEnums.Behavior))),
			NewControl("TraitAdd", PropType.Ord, list: true, Enum.GetValues(typeof(ModEnums.Trait))),
			NewControl("TraitRemove", PropType.Ord, list: true, Enum.GetValues(typeof(ModEnums.Trait))),
			NewControl("PatternBonuses", PropType.Obj, list: true, Obj_BonusStats(), "Bonus")
		};
	}

	public static PropertyControl[] Statcard_Chatter()
	{
		return new PropertyControl[13]
		{
			NewControl("Font", PropType.Ref, list: false, ModEnums.DB_CHATTER.Keys.ToArray()),
			NewControl("Style", PropType.Int),
			NewControl("Size", PropType.Int),
			NewControl("Color", PropType.Color),
			NewControl("WakeUp", PropType.Str, list: true),
			NewControl("Spawned", PropType.Str, list: true),
			NewControl("Chasing", PropType.Str, list: true),
			NewControl("Waiting", PropType.Str, list: true),
			NewControl("Attacking", PropType.Str, list: true),
			NewControl("Wounded", PropType.Str, list: true),
			NewControl("Died", PropType.Str, list: true),
			NewControl("FriendDied", PropType.Str, list: true),
			NewControl("EnemyDown", PropType.Str, list: true)
		};
	}

	public static PropertyControl[] Statcard_Armor()
	{
		return new PropertyControl[23]
		{
			NewControl("FullName", PropType.Str),
			NewControl("ShortName", PropType.Str),
			NewControl("Description", PropType.Str),
			NewControl("myRef", PropType.Ref, list: false, ModEnums.DB_GEAR.Keys.ToArray()),
			NewControl("BreakDebris", PropType.Ref, list: false, ModEnums.DB_GEAR.Keys.ToArray()),
			NewControl("Size", PropType.Float),
			NewControl("Weight", PropType.Float),
			NewControl("Tier", PropType.Int),
			NewControl("Genre", PropType.Ord, list: false, Enum.GetValues(typeof(ModEnums.ItemGenre))),
			NewControl("ForSale", PropType.Ord, list: false, Enum.GetValues(typeof(ModEnums.Market))),
			NewControl("myBuffs", PropType.Obj, list: true, Obj_BonusStats(), "BonusStat"),
			NewControl("ArmorRating", PropType.Obj, list: false, Obj_VectorList(), "Vec2"),
			NewControl("AlternateDamage", PropType.Ord, list: false, Enum.GetValues(typeof(ModEnums.DamageType))),
			NewControl("Encumbrance", PropType.Float),
			NewControl("Slot", PropType.Ord, list: false, Enum.GetValues(typeof(ModEnums.ArmorSlots))),
			NewControl("DisablePartOnEquip", PropType.Bool),
			NewControl("Traits", PropType.Ord, list: true, Enum.GetValues(typeof(ModEnums.ArmorTraits))),
			NewControl("Tint_Primary", PropType.Color, list: true),
			NewControl("Tint_Secondary", PropType.Color, list: true),
			NewControl("Tint_Glass", PropType.Color, list: true),
			NewControl("MaterialSets", PropType.Obj, list: true, Obj_MaterialSet(), "MaterialSet"),
			NewControl("Executions", PropType.Ref, list: true, ModEnums.DB_EXECUTION.Keys.ToArray()),
			NewControl("DeathEvents", PropType.Obj, list: true, Obj_DeathEvent(), "DeathEvent")
		};
	}

	public static PropertyControl[] Statcard_Animation()
	{
		return new PropertyControl[9]
		{
			NewControl("FullName", PropType.Str),
			NewControl("ShortName", PropType.Str),
			NewControl("Description", PropType.Str),
			NewControl("myClips", PropType.Ref, list: true, ModEnums.ANIMATIONCLIPS.ToArray()),
			NewControl("myPairs", PropType.Obj, list: true, Obj_AnimationClip(), "AnimPair"),
			NewControl("myGroups", PropType.Ref, list: true, ModEnums.DB_ANIMATIONS.Keys.ToArray()),
			NewControl("RequiredCore", PropType.Int),
			NewControl("ArbitraryBools", PropType.Bool, list: true, null, "Object", IsFixed: true),
			NewControl("ArbitraryFloats", PropType.Float, list: true, null, "Object", IsFixed: true)
		};
	}

	public static PropertyControl[] Statcard_Corpus()
	{
		return new PropertyControl[9]
		{
			NewControl("FullName", PropType.Str),
			NewControl("ShortName", PropType.Str),
			NewControl("Description", PropType.Str),
			NewControl("BorderColor", PropType.Color),
			NewControl("Icons", PropType.Ref, list: true, ModEnums.SPRITES.ToArray()),
			NewControl("DamageMultiplier", PropType.Float),
			NewControl("myBuffs", PropType.Obj, list: true, Obj_BonusStats(), "Bonus"),
			NewControl("DeathEvents", PropType.Obj, list: true, Obj_DeathEvent(), "DeathEvent"),
			NewControl("BonusTraits", PropType.Ord, list: true, Enum.GetValues(typeof(ModEnums.Trait)))
		};
	}

	public static PropertyControl[] Statcard_DoT()
	{
		return new PropertyControl[17]
		{
			NewControl("FullName", PropType.Str),
			NewControl("ShortName", PropType.Str),
			NewControl("Description", PropType.Str),
			NewControl("AttachPart", PropType.Ord, list: false, Enum.GetValues(typeof(ModEnums.ModelParts))),
			NewControl("Buffs", PropType.Ord, list: true, Enum.GetValues(typeof(ModEnums.BuffEffects))),
			NewControl("Chance", PropType.Float),
			NewControl("ConditionsToEnd", PropType.Ord, list: true, Enum.GetValues(typeof(ModEnums.DotResolutions))),
			NewControl("ConditionsToReduce", PropType.Ord, list: true, Enum.GetValues(typeof(ModEnums.DotResolutions))),
			NewControl("ConstantEffect", PropType.Ref, list: false, ModEnums.DB_DOT.Keys.ToArray()),
			NewControl("DamageEffect", PropType.Ref, list: false, ModEnums.DB_DOT.Keys.ToArray()),
			NewControl("Damage", PropType.Float),
			NewControl("DamageFrameBuffer", PropType.Float),
			NewControl("Effects", PropType.Ord, list: true, Enum.GetValues(typeof(ModEnums.DoTEffects))),
			NewControl("FlashColor", PropType.Color),
			NewControl("Icon", PropType.Ref, list: false, ModEnums.DB_DOT.Keys.ToArray()),
			NewControl("TimeInFrames", PropType.Float),
			NewControl("Type", PropType.Ord, list: false, Enum.GetValues(typeof(ModEnums.DamageType)))
		};
	}

	public static PropertyControl[] Statcard_Execution()
	{
		return new PropertyControl[13]
		{
			NewControl("FullName", PropType.Str),
			NewControl("ShortName", PropType.Str),
			NewControl("Description", PropType.Str),
			NewControl("Source", PropType.Ord, list: false, Enum.GetValues(typeof(ModEnums.ExecuteSource))),
			NewControl("Conditions", PropType.Ord, list: true, Enum.GetValues(typeof(ModEnums.ExecuteConditions))),
			NewControl("Priority", PropType.Int),
			NewControl("ScaleMin", PropType.Float),
			NewControl("ScaleMax", PropType.Float),
			NewControl("RequiredItem", PropType.Ref, list: false, Nullable(ModEnums.DB_WEAPONS.Keys.ToArray())),
			NewControl("Special", PropType.Ord, list: false, Enum.GetValues(typeof(ModEnums.ExecuteRules))),
			NewControl("SelectColor", PropType.Ord, list: false, Enum.GetValues(typeof(ModEnums.SelectionColors))),
			NewControl("Animations", PropType.Obj, list: true, Obj_ExecutionClip(), "AnimPair"),
			NewControl("Animations_NoKill", PropType.Obj, list: true, Obj_ExecutionClip(), "AnimPair")
		};
	}

	public static PropertyControl[] Statcard_Explosion()
	{
		return new PropertyControl[16]
		{
			NewControl("FullName", PropType.Str),
			NewControl("ShortName", PropType.Str),
			NewControl("Description", PropType.Str),
			NewControl("ArmorPiercing", PropType.Float),
			NewControl("Damage", PropType.Float),
			NewControl("DamageOverTimeEffect", PropType.Ref, list: false, Nullable(ModEnums.DB_DOT.Keys.ToArray())),
			NewControl("Dropoff", PropType.Float),
			NewControl("ExplosionEffect", PropType.Ref, list: false, ModEnums.DB_EXPLOSION.Keys.ToArray()),
			NewControl("ExplosionSound", PropType.Str),
			NewControl("FloorScar", PropType.Ref, list: false, ModEnums.DB_TEXTURES.Keys.ToArray()),
			NewControl("HitThroughWalls", PropType.Bool),
			NewControl("Ragdoll", PropType.Float),
			NewControl("RagdollYMultiplier", PropType.Float),
			NewControl("Range", PropType.Float),
			NewControl("Type", PropType.Ord, list: false, Enum.GetValues(typeof(ModEnums.DamageType))),
			NewControl("VictimsImmuneFallDamage", PropType.Bool)
		};
	}

	public static PropertyControl[] Statcard_Loadout()
	{
		return new PropertyControl[5]
		{
			NewControl("FullName", PropType.Str),
			NewControl("Tier", PropType.Int),
			NewControl("WeaponsStockpile", PropType.Ref, list: true, Nullable(ModEnums.DB_WEAPONS.Keys.ToArray())),
			NewControl("BorrowedLoadouts", PropType.Ref, list: true, ModEnums.DB_LOADOUTS.Keys.ToArray()),
			NewControl("LockedToCharacter", PropType.Ref, list: true, ModEnums.DB_CHAR.Keys.ToArray())
		};
	}

	public static PropertyControl[] Statcard_Wardrobe()
	{
		return new PropertyControl[10]
		{
			NewControl("FullName", PropType.Str),
			NewControl("ShortName", PropType.Str),
			NewControl("ArmorHead", PropType.Obj, list: false, Obj_VectorList(), "Vec2"),
			NewControl("ArmorBody", PropType.Obj, list: false, Obj_VectorList(), "Vec2"),
			NewControl("OutfitPieces", PropType.Ref, list: true, ModEnums.DB_GEAR.Keys.ToArray()),
			NewControl("UniformPieces", PropType.Obj, list: true, Obj_UniformList(), "Uniform"),
			NewControl("EmptySlots", PropType.Ord, list: true, Enum.GetValues(typeof(ModEnums.ArmorSlots))),
			NewControl("BonusCorpus", PropType.Ref, list: true, ModEnums.DB_CORPUS.Keys.ToArray()),
			NewControl("AllowedUsers", PropType.Ref, list: true, ModEnums.DB_CHAR.Keys.ToArray()),
			NewControl("WipeDefaultCorpus", PropType.Bool)
		};
	}

	public static PropertyControl[] Statcard_Swatch()
	{
		return new PropertyControl[4]
		{
			NewControl("FullName", PropType.Str),
			NewControl("TokenCost", PropType.Int),
			NewControl("ColorPairs", PropType.Obj, list: true, Obj_ColorPair(), "ColorPair"),
			NewControl("Colors", PropType.Obj, list: true, Obj_ColorSolo(), "ColorSolo")
		};
	}

	public static PropertyControl[] Statcard_Skin()
	{
		return new PropertyControl[4]
		{
			NewControl("FullName", PropType.Str),
			NewControl("Cost", PropType.Int),
			NewControl("AltModels", PropType.Ref, list: true, ModEnums.ALTERNATEPARTS.ToArray()),
			NewControl("AltMaterials", PropType.Obj, list: true, Obj_MaterialSwaps(), "MatSwap")
		};
	}

	public static PropertyControl[] Statcard_Origin()
	{
		return new PropertyControl[17]
		{
			NewControl("FullName", PropType.Str),
			NewControl("ShortName", PropType.Str),
			NewControl("Description", PropType.Str),
			NewControl("SkillFocus", PropType.Ord, list: false, Enum.GetValues(typeof(ModEnums.SkillTree))),
			NewControl("SkillWeakness", PropType.Ord, list: false, Enum.GetValues(typeof(ModEnums.SkillTree))),
			NewControl("CharacterType", PropType.Ref, list: false, ModEnums.DB_CHAR.Keys.ToArray()),
			NewControl("Icon", PropType.Ref, list: false, ModEnums.SPRITES.ToArray()),
			NewControl("ImprintsTilUnlock", PropType.Int),
			NewControl("DefaultArmor", PropType.Ref, list: true, ModEnums.DB_GEAR.Keys.ToArray()),
			NewControl("DefaultGear", PropType.Obj, list: true, Obj_UniformList(), "Uniform"),
			NewControl("myBuffs", PropType.Obj, list: true, Obj_BonusStats(), "BonusStat"),
			NewControl("AttributeBonuses", PropType.Int, list: true, null, "Object", IsFixed: true),
			NewControl("SpecialSkills", PropType.Ref, list: true, ModEnums.DB_SKILLS.Keys.ToArray()),
			NewControl("Swatch_Pairs", PropType.Obj, list: true, Obj_ColorPair(), "ColorPair"),
			NewControl("Swatch_Solos", PropType.Obj, list: true, Obj_ColorSolo(), "ColorSolo"),
			NewControl("WarchestStarterGear", PropType.Ref, list: true, ModEnums.DB_WEAPONS.Keys.Concat(ModEnums.DB_GEAR.Keys).ToArray()),
			NewControl("ExpandNowhere", PropType.Bool)
		};
	}

	public static PropertyControl[] Statcard_Skillgroup()
	{
		return new PropertyControl[2]
		{
			NewControl("Skills", PropType.Ord, list: true, Enum.GetValues(typeof(ModEnums.Skill))),
			NewControl("Groups", PropType.Ref, list: true, ModEnums.DB_SKILLGROUPS.Keys.ToArray())
		};
	}

	public static PropertyControl[] Statcard_Story()
	{
		return new PropertyControl[11]
		{
			NewControl("FullName", PropType.Str),
			NewControl("ShortName", PropType.Str),
			NewControl("Description", PropType.Str),
			NewControl("Tier", PropType.Int),
			NewControl("LoadScreenTips", PropType.Str, list: true),
			NewControl("MusicDefault", PropType.Ref, list: true, ModEnums.MUSIC.ToArray()),
			NewControl("RequiredCharacters", PropType.Obj, list: true, Obj_CharacterSet(), "CharSet"),
			NewControl("BonusCharacters", PropType.Obj, list: true, Obj_CharacterSet(), "CharSet"),
			NewControl("Prize_Loadout", PropType.Ref, list: true, ModEnums.DB_LOADOUTS.Keys.ToArray()),
			NewControl("Prize_Wardrobe", PropType.Ref, list: true, ModEnums.DB_WARDROBE.Keys.ToArray()),
			NewControl("WipeSquadAfterVictory", PropType.Bool)
		};
	}

	public static PropertyControl[] Statcard_Arena()
	{
		return new PropertyControl[9]
		{
			NewControl("FullName", PropType.Str),
			NewControl("ShortName", PropType.Str),
			NewControl("Description", PropType.Str),
			NewControl("MusicDefault", PropType.Ref, list: true, ModEnums.MUSIC.ToArray()),
			NewControl("Enemies_MaxUnitsMult", PropType.Float),
			NewControl("Enemies_SquadSizeMult", PropType.Float),
			NewControl("WaveName", PropType.Str),
			NewControl("WaveSequences", PropType.Obj, list: true, Obj_ArenaSequence(), "Sequence"),
			NewControl("FinalInfiniteSequence", PropType.Obj, list: false, Obj_ArenaSequence(), "Sequence")
		};
	}

	public static PropertyControl[] Statcard_WeaponModded()
	{
		return new PropertyControl[6]
		{
			NewControl("FullName", PropType.Str),
			NewControl("ShortName", PropType.Str),
			NewControl("LinkedWeapon", PropType.Ref, list: false, ModEnums.DB_RANGED.Keys.ToArray()),
			NewControl("Addons", PropType.Ref, list: true, ModEnums.DB_WEAPONADDONS.Keys.ToArray()),
			NewControl("EmptySlots", PropType.Ord, list: true, Enum.GetValues(typeof(ModEnums.AddonLocations))),
			NewControl("Traits", PropType.Ord, list: true, Enum.GetValues(typeof(ModEnums.WeaponTraits)))
		};
	}

	public static PropertyControl[] Statcard_AnimSet()
	{
		return new PropertyControl[33]
		{
			NewControl("IdleAnimation", PropType.Ref, list: false, ModEnums.DB_ANIMATIONS.Keys.ToArray()),
			NewControl("WalkAnimation", PropType.Ref, list: false, Nullable(ModEnums.DB_ANIMATIONS.Keys.ToArray())),
			NewControl("RunAnimation", PropType.Ref, list: false, Nullable(ModEnums.DB_ANIMATIONS.Keys.ToArray())),
			NewControl("DashAnimation", PropType.Ref, list: false, Nullable(ModEnums.DB_ANIMATIONS.Keys.ToArray())),
			NewControl("PickupAnimation", PropType.Ref, list: false, Nullable(ModEnums.DB_ANIMATIONS.Keys.ToArray())),
			NewControl("StandUp", PropType.Ref, list: false, Nullable(ModEnums.DB_ANIMATIONS.Keys.ToArray())),
			NewControl("FallLand", PropType.Ref, list: false, Nullable(ModEnums.DB_ANIMATIONS.Keys.ToArray())),
			NewControl("IdleEmotes", PropType.Ref, list: false, Nullable(ModEnums.DB_ANIMATIONS.Keys.ToArray())),
			NewControl("CombatEmotes", PropType.Ref, list: false, Nullable(ModEnums.DB_ANIMATIONS.Keys.ToArray())),
			NewControl("WakeupEmotes", PropType.Ref, list: false, Nullable(ModEnums.DB_ANIMATIONS.Keys.ToArray())),
			NewControl("HuntingEmotes", PropType.Ref, list: false, Nullable(ModEnums.DB_ANIMATIONS.Keys.ToArray())),
			NewControl("AngryEmotes", PropType.Ref, list: false, Nullable(ModEnums.DB_ANIMATIONS.Keys.ToArray())),
			NewControl("SpawnIn", PropType.Ref, list: false, ModEnums.DB_ANIMATIONS.Keys.ToArray()),
			NewControl("Override_Grapple", PropType.Ref, list: false, Nullable(ModEnums.DB_ANIMATIONS.Keys.ToArray())),
			NewControl("Override_Unarmed", PropType.Ref, list: false, Nullable(ModEnums.DB_ANIMATIONS.Keys.ToArray())),
			NewControl("Override_Melee", PropType.Ref, list: false, Nullable(ModEnums.DB_ANIMATIONS.Keys.ToArray())),
			NewControl("Override_Melee2h", PropType.Ref, list: false, Nullable(ModEnums.DB_ANIMATIONS.Keys.ToArray())),
			NewControl("Override_Ranged", PropType.Ref, list: false, Nullable(ModEnums.DB_ANIMATIONS.Keys.ToArray())),
			NewControl("Override_Block", PropType.Ref, list: false, Nullable(ModEnums.DB_ANIMATIONS.Keys.ToArray())),
			NewControl("Override_DashAttack", PropType.Ref, list: false, Nullable(ModEnums.DB_ANIMATIONS.Keys.ToArray())),
			NewControl("Override_DashSpecial", PropType.Ref, list: false, Nullable(ModEnums.DB_ANIMATIONS.Keys.ToArray())),
			NewControl("Override_Dodge", PropType.Ref, list: false, Nullable(ModEnums.DB_ANIMATIONS.Keys.ToArray())),
			NewControl("Override_ChargeUp", PropType.Ref, list: false, Nullable(ModEnums.DB_ANIMATIONS.Keys.ToArray())),
			NewControl("Override_Shove", PropType.Ref, list: false, Nullable(ModEnums.DB_ANIMATIONS.Keys.ToArray())),
			NewControl("HandsTied_Unarmed", PropType.Ref, list: false, Nullable(ModEnums.DB_ANIMATIONS.Keys.ToArray())),
			NewControl("RearSmash", PropType.Ref, list: false, Nullable(ModEnums.DB_ANIMATIONS.Keys.ToArray())),
			NewControl("Bullrush", PropType.Ref, list: false, ModEnums.DB_ANIMATIONS.Keys.ToArray()),
			NewControl("Special_Angry", PropType.Ref, list: false, Nullable(ModEnums.DB_ANIMATIONS.Keys.ToArray())),
			NewControl("Congratulate", PropType.Ref, list: false, Nullable(ModEnums.DB_ANIMATIONS.Keys.ToArray())),
			NewControl("StunAnimations", PropType.Ref, list: true, ModEnums.DB_ANIMATIONS.Keys.ToArray()),
			NewControl("DeathAnimations", PropType.Ref, list: true, ModEnums.DB_ANIMATIONS.Keys.ToArray()),
			NewControl("FootstepAudio", PropType.Str, list: true),
			NewControl("FallLandAudio", PropType.Str, list: true)
		};
	}

	public static PropertyControl[] Statcard_Character()
	{
		return new PropertyControl[56]
		{
			NewControl("FullName", PropType.Str),
			NewControl("ShortName", PropType.Str),
			NewControl("Title", PropType.Str),
			NewControl("Description", PropType.Str),
			NewControl("DefaultFaction", PropType.Ord, list: false, Enum.GetValues(typeof(ModEnums.Faction))),
			NewControl("Level", PropType.Int),
			NewControl("TacBar", PropType.Float),
			NewControl("Scale", PropType.Float),
			NewControl("Speed", PropType.Obj, list: false, Obj_Vector3List(), "Vec3"),
			NewControl("Speed_SprintBonus", PropType.Float),
			NewControl("Toughness", PropType.Int),
			NewControl("Health", PropType.Int),
			NewControl("CorpusBlocks", PropType.Ref, list: true, ModEnums.DB_CORPUS.Keys.ToArray()),
			NewControl("Attributes", PropType.Int, list: true, null, "Object", IsFixed: true),
			NewControl("ArmorHead", PropType.Obj, list: false, Obj_VectorList(), "Vec2"),
			NewControl("ArmorBody", PropType.Obj, list: false, Obj_VectorList(), "Vec2"),
			NewControl("MyRef", PropType.Ref, list: false, ModEnums.DB_CHAR.Keys.ToArray()),
			NewControl("AlternateParts", PropType.Ref, list: true, ModEnums.ALTERNATEPARTS.ToArray()),
			NewControl("SwapMaterials", PropType.Obj, list: true, Obj_MaterialSwaps(), "MatSwap"),
			NewControl("AnimationSet", PropType.Ref, list: false, ModEnums.DB_ANIMSETS.Keys.ToArray()),
			NewControl("Executions_Offensive", PropType.Ref, list: true, ModEnums.DB_EXECUTION.Keys.ToArray()),
			NewControl("Executions_Defensive", PropType.Ref, list: true, ModEnums.DB_EXECUTION.Keys.ToArray()),
			NewControl("VoiceSet", PropType.Ref, list: false, ModEnums.DB_VOICESETS.Keys.ToArray()),
			NewControl("TextChatter", PropType.Ref, list: false, ModEnums.DB_CHATTER.Keys.ToArray()),
			NewControl("Blood", PropType.Color),
			NewControl("BloodSpurt", PropType.Ref, list: false, ModEnums.DB_CHAR.Keys.ToArray()),
			NewControl("VariableTint", PropType.Color, list: true),
			NewControl("VariableTint2", PropType.Color, list: true),
			NewControl("DefaultArmor", PropType.Ref, list: true, ModEnums.DB_GEAR.Keys.ToArray()),
			NewControl("VariableArmor", PropType.Obj, list: true, Obj_ArmorList(), "ArmorList"),
			NewControl("Uniform", PropType.Obj, list: true, Obj_UniformList(), "Uniform"),
			NewControl("CountWornArmor", PropType.Bool),
			NewControl("Fist", PropType.Ref, list: false, ModEnums.DB_WEAPONS.Keys.ToArray()),
			NewControl("Kick", PropType.Ref, list: false, Nullable(ModEnums.DB_WEAPONS.Keys.ToArray())),
			NewControl("Headbutt", PropType.Ref, list: false, Nullable(ModEnums.DB_WEAPONS.Keys.ToArray())),
			NewControl("SlamExplode", PropType.Ref, list: false, Nullable(ModEnums.DB_EXPLOSION.Keys.ToArray())),
			NewControl("WeaponPreference", PropType.Ord, list: false, Enum.GetValues(typeof(ModEnums.WeaponCategories))),
			NewControl("ItemGenres", PropType.Ord, list: true, Enum.GetValues(typeof(ModEnums.ItemGenre))),
			NewControl("DefaultPrimary", PropType.Ref, list: false, Nullable(ModEnums.DB_WEAPONS.Keys.ToArray())),
			NewControl("DefaultSecondary", PropType.Ref, list: false, Nullable(ModEnums.DB_WEAPONS.Keys.ToArray())),
			NewControl("DefaultSidearm", PropType.Ref, list: false, Nullable(ModEnums.DB_WEAPONS.Keys.ToArray())),
			NewControl("UnarmedReplacement", PropType.Ref, list: false, Nullable(ModEnums.DB_WEAPONS.Keys.ToArray())),
			NewControl("DefaultLoadout", PropType.Ref, list: false, Nullable(ModEnums.DB_LOADOUTS.Keys.ToArray())),
			NewControl("Behaviors", PropType.Ord, list: true, Enum.GetValues(typeof(ModEnums.Behavior))),
			NewControl("Traits", PropType.Ord, list: true, Enum.GetValues(typeof(ModEnums.Trait))),
			NewControl("Vocations", PropType.Ord, list: true, Enum.GetValues(typeof(ModEnums.Vocation))),
			NewControl("Skills", PropType.Ord, list: true, Enum.GetValues(typeof(ModEnums.Skill))),
			NewControl("Groups", PropType.Ref, list: true, ModEnums.DB_SKILLGROUPS.Keys.ToArray()),
			NewControl("RemoveSkills", PropType.Ord, list: true, Enum.GetValues(typeof(ModEnums.Skill))),
			NewControl("FallLand", PropType.Ref, list: false, ModEnums.DB_CHAR.Keys.ToArray()),
			NewControl("LifeEffect", PropType.Ref, list: false, ModEnums.DB_CHAR.Keys.ToArray()),
			NewControl("DeathEffect", PropType.Ref, list: false, ModEnums.DB_CHAR.Keys.ToArray()),
			NewControl("TeleportEffect", PropType.Ref, list: false, ModEnums.DB_CHAR.Keys.ToArray()),
			NewControl("Footprint", PropType.Ref, list: false, ModEnums.DB_CHAR.Keys.ToArray()),
			NewControl("DeactivateTime", PropType.Int),
			NewControl("FlankMe", PropType.Bool)
		};
	}

	public static PropertyControl[] Statcard_Weapon(string[] references)
	{
		return new PropertyControl[35]
		{
			NewControl("FullName", PropType.Str),
			NewControl("ShortName", PropType.Str),
			NewControl("Description", PropType.Str),
			NewControl("myRef", PropType.Ref, list: false, RuntimeHelpers.GetObjectValue(references.Clone())),
			NewControl("BreakDebris", PropType.Ref, list: false, RuntimeHelpers.GetObjectValue(references.Clone())),
			NewControl("Size", PropType.Float),
			NewControl("Scale", PropType.Float),
			NewControl("Weight", PropType.Float),
			NewControl("Tier", PropType.Int),
			NewControl("Genre", PropType.Ord, list: false, Enum.GetValues(typeof(ModEnums.ItemGenre))),
			NewControl("ForSale", PropType.Ord, list: false, Enum.GetValues(typeof(ModEnums.Market))),
			NewControl("myBuffs", PropType.Obj, list: true, Obj_BonusStats(), "BonusStat"),
			NewControl("BrokenReplacement", PropType.Ref, list: false, Nullable(ModEnums.DB_WEAPONS.Keys.ToArray())),
			NewControl("BrokenExplosion", PropType.Ref, list: false, Nullable(ModEnums.DB_EXPLOSION.Keys.ToArray())),
			NewControl("Category", PropType.Ord, list: false, Enum.GetValues(typeof(ModEnums.WeaponCategories))),
			NewControl("Executions_MainHand", PropType.Ref, list: true, ModEnums.DB_EXECUTION.Keys.ToArray()),
			NewControl("Executions_OffHand", PropType.Ref, list: true, ModEnums.DB_EXECUTION.Keys.ToArray()),
			NewControl("MeleeAmmo", PropType.Int),
			NewControl("MeleeDamage", PropType.Float),
			NewControl("MeleeDamageType", PropType.Ord, list: false, Enum.GetValues(typeof(ModEnums.DamageType))),
			NewControl("MeleeArmorPiercing", PropType.Float),
			NewControl("MeleeDefense", PropType.Float),
			NewControl("MeleeDoT", PropType.Ref, list: false, Nullable(ModEnums.DB_DOT.Keys.ToArray())),
			NewControl("MeleeBuffEffect", PropType.Obj, list: false, Obj_BuffEffect(), "Buff"),
			NewControl("MeleeTrail", PropType.Ref, list: false, RuntimeHelpers.GetObjectValue(references.Clone())),
			NewControl("MeleeTrail_Heavy", PropType.Ref, list: false, RuntimeHelpers.GetObjectValue(references.Clone())),
			NewControl("SweepWidth", PropType.Float),
			NewControl("Traits", PropType.Ord, list: true, Enum.GetValues(typeof(ModEnums.WeaponTraits))),
			NewControl("UsageSounds", PropType.Ref, list: false, Nullable(ModEnums.DB_SOUNDPACKS.Keys.ToArray())),
			NewControl("UsageAnimation", PropType.Ref, list: false, Nullable(ModEnums.DB_ANIMATIONS.Keys.ToArray())),
			NewControl("UsageAnimation2xHand", PropType.Ref, list: false, Nullable(ModEnums.DB_ANIMATIONS.Keys.ToArray())),
			NewControl("ThrowAnimation", PropType.Ref, list: false, Nullable(ModEnums.DB_ANIMATIONS.Keys.ToArray())),
			NewControl("PistolWhipOverride", PropType.Ref, list: false, Nullable(ModEnums.DB_ANIMATIONS.Keys.ToArray())),
			NewControl("UseageAnim_CoreLevelRequired", PropType.Int),
			NewControl("WallHit", PropType.Ref, list: false, RuntimeHelpers.GetObjectValue(references.Clone()))
		};
	}

	public static PropertyControl[] StatCard_WeaponBase()
	{
		return Statcard_Weapon(ModEnums.DB_WEAPONS.Keys.ToArray());
	}

	public static PropertyControl[] StatCard_Ammo()
	{
		return new PropertyControl[30]
		{
			NewControl("Size", PropType.Float),
			NewControl("Accuracy", PropType.Float),
			NewControl("ArmorPiercing", PropType.Float),
			NewControl("Damage", PropType.Float),
			NewControl("Loudness", PropType.Float),
			NewControl("Range", PropType.Float),
			NewControl("Recoil", PropType.Float),
			NewControl("AOERange", PropType.Float),
			NewControl("BecomeDebrisOnHit", PropType.Bool),
			NewControl("BuffEffect", PropType.Obj, list: false, Obj_BuffEffect(), "Buff"),
			NewControl("Bullet", PropType.Ref, list: false, ModEnums.DB_AMMO.Keys.ToArray()),
			NewControl("Casing", PropType.Ref, list: false, ModEnums.DB_AMMO.Keys.ToArray()),
			NewControl("CasingSound", PropType.Str),
			NewControl("DamageOverTimeEffect", PropType.Ref, list: false, Nullable(ModEnums.DB_DOT.Keys.ToArray())),
			NewControl("DamageType", PropType.Ord, list: false, Enum.GetValues(typeof(ModEnums.DamageType))),
			NewControl("DefaultPickupAmount", PropType.Int),
			NewControl("ExplodeInSeconds", PropType.Float),
			NewControl("Explosion", PropType.Ref, list: false, Nullable(ModEnums.DB_EXPLOSION.Keys.ToArray())),
			NewControl("Flare", PropType.Ref, list: false, ModEnums.DB_AMMO.Keys.ToArray()),
			NewControl("GravityMultiplier", PropType.Float),
			NewControl("Heatseek", PropType.Float),
			NewControl("HitCharacterSplosion", PropType.Ref, list: false, ModEnums.DB_AMMO.Keys.ToArray()),
			NewControl("ImpactSound", PropType.Str),
			NewControl("ImpactSplosion", PropType.Ref, list: false, ModEnums.DB_AMMO.Keys.ToArray()),
			NewControl("ImpactUsesBulletNormal", PropType.Bool),
			NewControl("NoSFXOnHit", PropType.Bool),
			NewControl("Shots", PropType.Int),
			NewControl("ShotSpread", PropType.Float),
			NewControl("Speed", PropType.Float),
			NewControl("SpeedBoost", PropType.Obj, list: false, Obj_VectorList(), "Vec2")
		};
	}

	public static PropertyControl[] StatCard_Weapon_Stats(string[] references)
	{
		PropertyControl[] second = new PropertyControl[23]
		{
			NewControl("Accuracy", PropType.Float),
			NewControl("ArmorPiercing", PropType.Float),
			NewControl("Damage", PropType.Float),
			NewControl("Loudness", PropType.Float),
			NewControl("Range", PropType.Float),
			NewControl("Recoil", PropType.Float),
			NewControl("Ammo", PropType.Int),
			NewControl("AmmoType", PropType.Ref, list: false, Nullable(ModEnums.DB_AMMO.Keys.ToArray())),
			NewControl("BonusRounds", PropType.Int),
			NewControl("Bonus_AOERange", PropType.Float),
			NewControl("Bonus_GravityMultiplier", PropType.Float),
			NewControl("Bonus_Heatseek", PropType.Float),
			NewControl("Bonus_Shots", PropType.Int),
			NewControl("Bonus_ShotSpread", PropType.Float),
			NewControl("Bonus_Speed", PropType.Float),
			NewControl("Bonus_SpeedBoost", PropType.Obj, list: false, Obj_VectorList(), "Vec2"),
			NewControl("BurstFire", PropType.Int),
			NewControl("mySoundPath", PropType.Str),
			NewControl("RateOfFire", PropType.Float),
			NewControl("Recovery", PropType.Float),
			NewControl("Reliability", PropType.Float),
			NewControl("Reload", PropType.Float),
			NewControl("Stability", PropType.Float)
		};
		return Statcard_Weapon(references).Union(second).ToArray();
	}

	public static PropertyControl[] StatCard_Thrown()
	{
		PropertyControl[] second = new PropertyControl[1] { NewControl("GrenadeSlotOnly", PropType.Bool) };
		return StatCard_Weapon_Stats(ModEnums.DB_THROWN.Keys.ToArray()).Union(second).ToArray();
	}

	public static PropertyControl[] StatCard_WeaponAddon()
	{
		PropertyControl[] second = new PropertyControl[1] { NewControl("Type", PropType.Ord, list: false, Enum.GetValues(typeof(ModEnums.WeaponAddons))) };
		return StatCard_Weapon_Stats(ModEnums.DB_WEAPONADDONS.Keys.ToArray()).Union(second).ToArray();
	}

	public static PropertyControl[] StatCard_Ranged()
	{
		PropertyControl[] second = new PropertyControl[13]
		{
			NewControl("Automatic", PropType.Bool),
			NewControl("Breach", PropType.Bool),
			NewControl("Pump", PropType.Bool),
			NewControl("InfiniteReloads", PropType.Bool),
			NewControl("Sound_EmptyMag", PropType.Str),
			NewControl("AddonConfig", PropType.Ord, list: false, Enum.GetValues(typeof(ModEnums.AddonConfig))),
			NewControl("DefaultAddons", PropType.Ref, list: true, ModEnums.DB_WEAPONADDONS.Keys.ToArray()),
			NewControl("DefaultGroups", PropType.Ref, list: true, ModEnums.DB_ADDONGROUPS.Keys.ToArray()),
			NewControl("CoreAddons", PropType.Ref, list: true, ModEnums.DB_WEAPONADDONS.Keys.ToArray()),
			NewControl("CoreGroups", PropType.Ref, list: true, ModEnums.DB_ADDONGROUPS.Keys.ToArray()),
			NewControl("AllowedAddons", PropType.Ref, list: true, ModEnums.DB_WEAPONADDONS.Keys.ToArray()),
			NewControl("AllowedGroups", PropType.Ref, list: true, ModEnums.DB_ADDONGROUPS.Keys.ToArray()),
			NewControl("ReloadRule", PropType.Ord, list: false, Enum.GetValues(typeof(ModEnums.ReloadRules)))
		};
		return StatCard_Weapon_Stats(ModEnums.DB_RANGED.Keys.ToArray()).Union(second).ToArray();
	}

	public static PropertyControl[] StatCard_AddonGroup()
	{
		return new PropertyControl[4]
		{
			NewControl("FullName", PropType.Str),
			NewControl("ShortName", PropType.Str),
			NewControl("Description", PropType.Str),
			NewControl("AddonStockpile", PropType.Ref, list: true, ModEnums.DB_WEAPONADDONS.Keys.ToArray())
		};
	}

	private static string[] Nullable(IEnumerable<string> list)
	{
		List<string> list2 = new List<string>();
		list2.Add("None");
		list2.AddRange(list);
		return list2.ToArray();
	}

	private static PropertyControl NewControl(string Prop, PropType FieldType, bool list = false, object Choices = null, string ObjName = "Object", bool IsFixed = false)
	{
		PropertyControl propertyControl = new PropertyControl();
		PropertyControl propertyControl2 = propertyControl;
		propertyControl2.PropName = Prop;
		propertyControl2.PropType = FieldType;
		propertyControl2.IsList = list;
		propertyControl2.Selection = RuntimeHelpers.GetObjectValue(Choices);
		propertyControl2.ObjectName = ObjName;
		propertyControl2.Fixed = IsFixed;
		propertyControl2 = null;
		return propertyControl;
	}
}
