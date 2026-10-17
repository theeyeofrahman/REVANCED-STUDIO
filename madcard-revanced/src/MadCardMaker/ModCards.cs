using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.VisualBasic.CompilerServices;
using Newtonsoft.Json.Linq;

namespace MadCardMaker;

[StandardModule]
public sealed class ModCards
{
	public enum NodeType
	{
		Val,
		Arr,
		Obj,
		Log
	}

	public enum PropType
	{
		Bool,
		Int,
		Float,
		Str,
		Color,
		Ref,
		Obj
	}

	public class NodeSorter : IComparer
	{
		public int Compare(object x, object y)
		{
			return string.Compare(Conversions.ToString(NewLateBinding.LateGet(x, null, "Text", new object[0], null, null, null)), Conversions.ToString(NewLateBinding.LateGet(y, null, "Text", new object[0], null, null, null)));
		}

		int IComparer.Compare(object x, object y)
		{
			//ILSpy generated this explicit interface implementation from .override directive in Compare
			return this.Compare(x, y);
		}
	}

	public class PropertyControl
	{
		public string PropName;

		public PropType PropType;

		public bool IsList;

		public object Selection;

		public string ObjectName;

		public bool Fixed;

		public bool WriteNewValues;

		public PropertyControl[] Defines;

		public string OverrideDesc;

		public PropertyControl()
		{
			OverrideDesc = "";
		}
	}

	public class NodeTag
	{
		public PropertyControl LinkedControl;

		public object Value;

		public NodeType NodeType;

		public bool Edited;

		public NodeTag(PropertyControl Control, NodeType type)
		{
			Edited = false;
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

	public class CardTag
	{
		public string FileName;

		public NodeType ObjType;

		public string Type;

		public string RootName;

		public int Priority;

		public bool Converter;

		public string DisplayName;

		public string Description;

		public string DisplayPrice;

		public string AttachedDeck;

		public string AttachedMod;

		public JObject Info;

		public CardTag(CardTag tag)
		{
			FileName = "";
			Type = "";
			RootName = "";
			Priority = 0;
			Converter = false;
			DisplayName = "";
			Description = "";
			DisplayPrice = "";
			AttachedDeck = "";
			AttachedMod = "";
			FileName = tag.FileName;
			ObjType = tag.ObjType;
			Type = tag.Type;
			RootName = tag.RootName;
			Priority = tag.Priority;
			DisplayName = tag.DisplayName;
			Description = tag.Description;
			DisplayPrice = tag.DisplayPrice;
			Info = tag.Info;
		}

		public CardTag(string T, NodeType C)
		{
			FileName = "";
			Type = "";
			RootName = "";
			Priority = 0;
			Converter = false;
			DisplayName = "";
			Description = "";
			DisplayPrice = "";
			AttachedDeck = "";
			AttachedMod = "";
			FileName = T;
			DisplayName = T;
			ObjType = C;
			Type = C switch
			{
				NodeType.Obj => "Deck",
				NodeType.Log => "Assets",
				_ => "Category",
			};
		}

		public CardTag(string File, string T, string Root, JObject Obj)
		{
			FileName = "";
			Type = "";
			RootName = "";
			Priority = 0;
			Converter = false;
			DisplayName = "";
			Description = "";
			DisplayPrice = "";
			AttachedDeck = "";
			AttachedMod = "";
			FileName = File;
			Type = T;
			RootName = Root;
			Info = Obj;
			ObjType = NodeType.Val;
		}

		public bool CardEquals(CardTag obj, bool ignoresPriority = false)
		{
			return (Operators.CompareString(obj.FileName, FileName, TextCompare: false) == 0) & (Operators.CompareString(obj.Type, Type, TextCompare: false) == 0) & (Operators.CompareString(obj.AttachedMod, AttachedMod, TextCompare: false) == 0) & (Operators.CompareString(obj.AttachedDeck, AttachedDeck, TextCompare: false) == 0) & (ignoresPriority || obj.Priority == Priority);
		}

		public override string ToString()
		{
			return ID() + "_" + Loc();
		}

		public string ID()
		{
			return Type + ":" + FileName;
		}

		public string RootID()
		{
			return Type + ":" + RootName;
		}

		public string Loc()
		{
			return AttachedMod + ":" + AttachedDeck;
		}
	}

	public class ModTag
	{
		public string FileName;

		public string WorkshopID;

		public string DisplayName;

		public string Description;

		public string Authors;

		public string[] ModTags;

		public bool Enabled;

		public bool Workshop;

		public string Directory;

		public ModTag(string dir, string name, string author, string desc, string[] tags, string wid, string path, bool loadonly)
		{
			FileName = "";
			WorkshopID = "";
			DisplayName = "";
			Description = "";
			Authors = "";
			Enabled = false;
			Workshop = false;
			Directory = "";
			FileName = dir;
			DisplayName = name;
			Authors = author;
			Description = desc;
			ModTags = tags;
			WorkshopID = wid;
			Directory = path;
			Workshop = loadonly;
		}

		public override string ToString()
		{
			return FileName;
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

		public Color AsColor()
		{
			return checked(Color.FromArgb((int)Math.Round(a * 255.0), (int)Math.Round(r * 255.0), (int)Math.Round(g * 255.0), (int)Math.Round(b * 255.0)));
		}

		public override string ToString()
		{
			return ColorTranslator.ToHtml(checked(Color.FromArgb((int)Math.Round(a * 255.0), (int)Math.Round(r * 255.0), (int)Math.Round(g * 255.0), (int)Math.Round(b * 255.0))));
		}
	}

	public delegate PropertyControl[] PropertyList();

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
			NewControl("ArmorPicks", PropType.Ref, list: true, Nullable(Options("Armor")), "Armor"),
			NewControl("TintPrimary", PropType.Color),
			NewControl("TintSecondary", PropType.Color),
			NewControl("TintGlass", PropType.Color),
			NewControl("AltMaterial", PropType.Int),
			NewControl("AltMesh", PropType.Int)
		};
	}

	public static PropertyControl[] Obj_MaterialSwaps()
	{
		return new PropertyControl[2]
		{
			NewControl("AssignedParts", PropType.Ref, list: true, Ord("ModelParts"), "ModelParts"),
			NewControl("Materials", PropType.Ref, list: true, Asset("Material"), "Material")
		};
	}

	public static PropertyControl[] Obj_AnimationClip()
	{
		return new PropertyControl[2]
		{
			NewControl("Clip1", PropType.Ref, list: false, Asset("AnimationClip"), "AnimationClip"),
			NewControl("Clip2", PropType.Ref, list: false, Asset("AnimationClip"), "AnimationClip")
		};
	}

	public static PropertyControl[] Obj_Reskin()
	{
		return new PropertyControl[2]
		{
			NewControl("Old", PropType.Str),
			NewControl("New", PropType.Ref, list: false, Asset("Material"), "Material")
		};
	}

	public static PropertyControl[] Obj_DeathEvent()
	{
		return new PropertyControl[17]
		{
			NewControl("PreventRegen", PropType.Ref, list: true, Ord("DeathFlags"), "DeathFlags"),
			NewControl("Chance", PropType.Float),
			NewControl("Retries", PropType.Int),
			NewControl("RegenHealth", PropType.Int),
			NewControl("ReplaceCorpus", PropType.Ref, list: false, Nullable(Options("Corpus")), "Corpus"),
			NewControl("InertRagdoll", PropType.Bool),
			NewControl("PreventDeathAnimation", PropType.Bool),
			NewControl("NoAdvanceOnFail", PropType.Bool),
			NewControl("LastCorpusOnly", PropType.Bool),
			NewControl("InvincibileTime", PropType.Float),
			NewControl("InvincibleType", PropType.Ref, list: false, Ord("Invincibility"), "Invincibility"),
			NewControl("GiveSkills", PropType.Ref, list: false, Nullable(Options("SkillGroup")), "SkillGroups"),
			NewControl("DestroyArmor", PropType.Ref, list: true, Ord("ArmorSlot"), "ArmorSlot", IsFixed: false, CustomVal: true),
			NewControl("RecoveryAnimation", PropType.Ref, list: false, Nullable(Options("Animations")), "Animations"),
			NewControl("FlashColor", PropType.Color),
			NewControl("FlashTimes", PropType.Int),
			NewControl("RezSFX", PropType.Ref, list: false, Asset("SoundEvent"), "SoundEvent", IsFixed: false, CustomVal: true)
		};
	}

	public static PropertyControl[] Obj_BonusStats()
	{
		return new PropertyControl[6]
		{
			NewControl("Name", PropType.Str),
			NewControl("Description", PropType.Str),
			NewControl("Type", PropType.Ref, list: false, Ord("BonusTypes"), "BonusTypes"),
			NewControl("Amount", PropType.Float),
			NewControl("AmPercent", PropType.Bool),
			NewControl("ShowAsGood", PropType.Bool)
		};
	}

	public static PropertyControl[] Obj_BuffEffect()
	{
		return new PropertyControl[3]
		{
			NewControl("Effect", PropType.Ref, list: false, Ord("BuffEffects"), "BuffEffects"),
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
			NewControl("Character", PropType.Ref, list: false, Options("Character"), "Character"),
			NewControl("Loadout", PropType.Ref, list: false, Nullable(Options("Loadout")), "Loadout"),
			NewControl("Wardrobe", PropType.Ref, list: false, Nullable(Options("Wardrobe")), "Wardrobe")
		};
	}

	public static PropertyControl[] Obj_WaveChar()
	{
		return new PropertyControl[4]
		{
			NewControl("Type", PropType.Ref, list: false, Options("Character"), "Character"),
			NewControl("Total", PropType.Int),
			NewControl("GenerationScale", PropType.Float),
			NewControl("AddUnitsPerWave", PropType.Float)
		};
	}

	public static PropertyControl[] Obj_WeaponLoadout()
	{
		return new PropertyControl[2]
		{
			NewControl("Loadout", PropType.Ref, list: false, Options("Loadout")),
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
			NewControl("Type", PropType.Ref, list: false, Options("Character"), "Character"),
			NewControl("Total", PropType.Int),
			NewControl("GenerationScale", PropType.Float)
		};
	}

	public static PropertyControl[] Obj_SingleCharacter()
	{
		return new PropertyControl[7]
		{
			NewControl("SerialNumber", PropType.Str),
			NewControl("Type", PropType.Ref, list: false, Options("Character"), "Character"),
			NewControl("Weapon", PropType.Ref, list: false, Nullable(Options("Weapon")), "Weapon"),
			NewControl("Sidearm", PropType.Ref, list: false, Nullable(Options("Weapon")), "Weapon"),
			NewControl("Throwable", PropType.Ref, list: false, Nullable(Options("Thrown")), "Weapon"),
			NewControl("Key", PropType.Ref, list: false, Nullable(Options("Keycard")), "Keycard"),
			NewControl("Gear", PropType.Ref, list: true, Options("Armor"), "Armor")
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

	public static PropertyControl[] Obj_SQSquad()
	{
		return new PropertyControl[1] { NewControl("Troops", PropType.Obj, list: true, Obj_SQTroop(), "Trooper") };
	}

	public static PropertyControl[] Obj_SQTroop()
	{
		return new PropertyControl[2]
		{
			NewControl("Unit", PropType.Ref, list: false, Options("Character"), "Character"),
			NewControl("Loadout", PropType.Ref, list: false, Nullable(Options("Loadout")), "Loadout")
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
			NewControl("Units", PropType.Obj, list: true, Obj_WaveChar(), "WaveCharacter"),
			NewControl("Bosses", PropType.Obj, list: true, Obj_SingleCharacter(), "SingleCharacter")
		};
	}

	public static PropertyControl[] Event_ParamObj()
	{
		return new PropertyControl[3]
		{
			NewControl("Target", PropType.Ref, list: false, Ord("TargetType"), "TargetType"),
			NewControl("SerialNumber", PropType.Str),
			NewControl("StartVelocity", PropType.Obj, list: false, Obj_Vector3List(), "Vec3")
		};
	}

	public static PropertyControl[] Event_ParamItem()
	{
		PropertyControl[] second = new PropertyControl[4]
		{
			NewControl("MadCard", PropType.Ref, list: false, Options("Item"), "Item"),
			NewControl("AmQuest", PropType.Bool),
			NewControl("Registered", PropType.Bool),
			NewControl("RandomSpawn", PropType.Bool)
		};
		return Event_ParamObj().Union(second).ToArray();
	}

	public static PropertyControl[] Event_ParamChar()
	{
		PropertyControl[] second = new PropertyControl[3]
		{
			NewControl("MadCard", PropType.Ref, list: false, Options("Character"), "Character"),
			NewControl("Faction", PropType.Ref, list: false, Ord("Factions"), "Factions", IsFixed: false, CustomVal: true),
			NewControl("Awake", PropType.Bool)
		};
		return Event_ParamObj().Union(second).ToArray();
	}

	public static PropertyControl[] Purp_EventBase()
	{
		return new PropertyControl[5]
		{
			NewControl("SerialNumber", PropType.Str),
			NewControl("TrackedEvent", PropType.Str),
			NewControl("Repeating", PropType.Bool),
			NewControl("RunTaskEvent", PropType.Bool),
			NewControl("PopupComplete", PropType.Str, list: true)
		};
	}

	public static PropertyControl[] Purp_EventArea()
	{
		PropertyControl[] second = new PropertyControl[7]
		{
			NewControl("TrackPlayer", PropType.Bool),
			NewControl("TrackSerialNumber", PropType.Str),
			NewControl("TrackFaction", PropType.Ref, list: false, Ord("Factions"), "Factions", IsFixed: false, CustomVal: true),
			NewControl("TimerName", PropType.Str),
			NewControl("TimerLength", PropType.Float),
			NewControl("TrackOutsideArea", PropType.Bool),
			NewControl("DisplayTimer", PropType.Bool)
		};
		return Purp_EventBase().Union(second).ToArray();
	}

	public static PropertyControl[] Purp_EventInteract()
	{
		PropertyControl[] second = new PropertyControl[12]
		{
			NewControl("HighlightText", PropType.Str),
			NewControl("UseText", PropType.Str),
			NewControl("UseColor", PropType.Ref, list: false, Ord("ExecuteColors"), "ExecuteColors"),
			NewControl("Icon", PropType.Ref, list: false, Nullable(Asset("Sprite")), "Sprite"),
			NewControl("TimeGoal", PropType.Float),
			NewControl("TimeDisuse", PropType.Float),
			NewControl("TimeDelay", PropType.Float),
			NewControl("TimeStartBonus", PropType.Float),
			NewControl("SuccessEffect", PropType.Ref, list: false, Nullable(Asset("ParticleSystem")), "ParticleSystem"),
			NewControl("Priority", PropType.Int),
			NewControl("AutoEvent", PropType.Ref, list: false, Ord("InteractiveEvents"), "InteractiveEvents"),
			NewControl("Locked", PropType.Bool)
		};
		return Purp_EventBase().Union(second).ToArray();
	}

	public static PropertyControl[] Purp_EventTarget()
	{
		PropertyControl[] second = new PropertyControl[3]
		{
			NewControl("Heal", PropType.Bool),
			NewControl("Heal_Wait", PropType.Float),
			NewControl("Heal_Speed", PropType.Float)
		};
		return Purp_EventBase().Union(second).ToArray();
	}

	public static PropertyControl[] Event_Parameter()
	{
		return new PropertyControl[21]
		{
			NewControl("Amount", PropType.Int),
			NewControl("Location", PropType.Str),
			NewControl("SerialNumber", PropType.Str),
			NewControl("Operator", PropType.Ref, list: false, Ord("RelationalOps"), "RelationalOps"),
			NewControl("BoolOp", PropType.Ref, list: false, Ord("BoolOps"), "BoolOps"),
			NewControl("MathOp", PropType.Ref, list: false, Ord("MathOps"), "MathOps"),
			NewControl("Int1", PropType.Int),
			NewControl("Int2", PropType.Int),
			NewControl("String1", PropType.Str),
			NewControl("String2", PropType.Str),
			NewControl("String3", PropType.Str),
			NewControl("Bool1", PropType.Bool),
			NewControl("Bool2", PropType.Bool),
			NewControl("CardRef", PropType.Ref, list: false, new string[0]),
			NewControl("Enum", PropType.Ref, list: false, new string[0]),
			NewControl("CharParam1", PropType.Obj, list: false, Event_ParamChar(), "CharParam"),
			NewControl("CharParam2", PropType.Obj, list: false, Event_ParamChar(), "CharParam"),
			NewControl("ItemParam", PropType.Obj, list: false, Event_ParamItem(), "ItemParam"),
			NewControl("TaskInteractive", PropType.Obj, list: false, Purp_EventInteract(), "InteractParam"),
			NewControl("TaskDestroy", PropType.Obj, list: false, Purp_EventTarget(), "TargetParam"),
			NewControl("TaskZone", PropType.Obj, list: false, Purp_EventArea(), "AreaParam")
		};
	}

	public static PropertyControl[] Event_Action()
	{
		return new PropertyControl[1] { NewControl("Type", PropType.Ref, list: false, Ord("ActionTypes"), "ActionTypes", IsFixed: false, CustomVal: false, Event_Parameter()) };
	}

	public static PropertyControl[] Event_Condition()
	{
		return new PropertyControl[3]
		{
			NewControl("AmCheckpoint", PropType.Bool),
			NewControl("TrueForever", PropType.Bool),
			NewControl("Type", PropType.Ref, list: false, Ord("ConditionTypes"), "ConditionTypes", IsFixed: false, CustomVal: false, Event_Parameter())
		};
	}

	public static PropertyControl[] StatCard_Event()
	{
		return new PropertyControl[3]
		{
			NewControl("RepeatEvent", PropType.Bool),
			NewControl("Conditions", PropType.Obj, list: true, Event_Condition(), "EventCondition"),
			NewControl("Actions", PropType.Obj, list: true, Event_Action(), "EventAction")
		};
	}

	public static PropertyControl[] Cutscene_Step()
	{
		return new PropertyControl[15]
		{
			NewControl("ActorSerial", PropType.Str),
			NewControl("Blurb", PropType.Str),
			NewControl("Timespan", PropType.Float),
			NewControl("Emote", PropType.Ref, list: false, Nullable(Asset("AnimationClip")), "AnimationClip"),
			NewControl("SpeakingTo", PropType.Str),
			NewControl("Voice", PropType.Ref, list: false, Ord("VoiceTypes"), "VoiceTypes"),
			NewControl("StartSound", PropType.Ref, list: false, Asset("SoundEvent"), "SoundEvent", IsFixed: false, CustomVal: true),
			NewControl("ViewCamera", PropType.Int),
			NewControl("Mark", PropType.Int),
			NewControl("FocusOnSpeakerAmount", PropType.Float),
			NewControl("MoveOrderPermanent", PropType.Bool),
			NewControl("SkipOverride", PropType.Bool),
			NewControl("Action", PropType.Ref, list: false, Ord("StepActionTypes"), "StepActionTypes"),
			NewControl("CutsceneEvent", PropType.Ref, list: false, Nullable(Options("Event")), "Event"),
			NewControl("TrackedEvent", PropType.Str)
		};
	}

	public static PropertyControl[] StatCard_Cutscene()
	{
		return new PropertyControl[1] { NewControl("Steps", PropType.Obj, list: true, Cutscene_Step(), "Step") };
	}

	public static PropertyControl[] Vessel_Actions()
	{
		return new PropertyControl[3]
		{
			NewControl("ParamName", PropType.Str),
			NewControl("Check", PropType.Ref, list: false, Ord("DriverChecks"), "DriverChecks"),
			NewControl("CheckDir", PropType.Ref, list: false, Ord("DriverDir"), "DriverDir")
		};
	}

	public static PropertyControl[] StatCard_Driver()
	{
		return new PropertyControl[1] { NewControl("Actions", PropType.Obj, list: true, Vessel_Actions(), "DriverAction") };
	}

	public static PropertyControl[] Mission_Objective()
	{
		return new PropertyControl[6]
		{
			NewControl("Name", PropType.Str),
			NewControl("CanFail", PropType.Bool),
			NewControl("Hidden", PropType.Bool),
			NewControl("ShowProgress", PropType.Bool),
			NewControl("Conditions_Completed", PropType.Obj, list: true, Event_Condition(), "EventCondition"),
			NewControl("Actions_Completed", PropType.Obj, list: true, Event_Action(), "EventAction")
		};
	}

	public static PropertyControl[] Mission_Act()
	{
		return new PropertyControl[5]
		{
			NewControl("Name", PropType.Str),
			NewControl("Description", PropType.Str),
			NewControl("Actions_Start", PropType.Obj, list: true, Event_Action(), "EventAction"),
			NewControl("Actions_Completed", PropType.Obj, list: true, Event_Action(), "EventAction"),
			NewControl("Objectives", PropType.Obj, list: true, Mission_Objective(), "MissionObjective")
		};
	}

	public static PropertyControl[] Statcard_Mission()
	{
		return new PropertyControl[13]
		{
			NewControl("SerialNumber", PropType.Str),
			NewControl("Name", PropType.Str),
			NewControl("Description", PropType.Str),
			NewControl("ShowMission", PropType.Bool),
			NewControl("EndText", PropType.Str),
			NewControl("XP", PropType.Int),
			NewControl("Cash", PropType.Int),
			NewControl("Conditions_Start", PropType.Obj, list: true, Event_Condition(), "EventCondition"),
			NewControl("Actions_Exists", PropType.Obj, list: true, Event_Action(), "EventAction"),
			NewControl("Actions_Start", PropType.Obj, list: true, Event_Action(), "EventAction"),
			NewControl("Actions_Success", PropType.Obj, list: true, Event_Action(), "EventAction"),
			NewControl("Actions_Failure", PropType.Obj, list: true, Event_Action(), "EventAction"),
			NewControl("Acts", PropType.Obj, list: true, Mission_Act(), "MissionAct")
		};
	}

	public static PropertyControl[] StatCard_Item(string[] @ref, string ObjRef)
	{
		return new PropertyControl[14]
		{
			NewControl("Name", PropType.Str),
			NewControl("NameShort", PropType.Str),
			NewControl("Description", PropType.Str),
			NewControl("ObjRef", PropType.Ref, list: false, @ref, ObjRef),
			NewControl("BreakObj", PropType.Ref, list: false, Nullable(Asset("ParticleSystem")), "ParticleSystem"),
			NewControl("Reskins", PropType.Obj, list: true, Obj_Reskin(), "Reskin"),
			NewControl("Size", PropType.Float),
			NewControl("Weight", PropType.Float),
			NewControl("Tier", PropType.Int),
			NewControl("Genre", PropType.Ref, list: false, Ord("ItemGenres"), "ItemGenres", IsFixed: false, CustomVal: true),
			NewControl("Store", PropType.Ref, list: false, Ord("Markets"), "Markets"),
			NewControl("CustomPrice", PropType.Int),
			NewControl("Buffs", PropType.Obj, list: true, Obj_BonusStats(), "BonusStat"),
			NewControl("MenuIcon", PropType.Ref, list: false, Asset("Sprite"), "Sprite")
		};
	}

	public static PropertyControl[] Statcard_Weapon(string[] @ref, string objRef)
	{
		PropertyControl[] second = new PropertyControl[25]
		{
			NewControl("Category", PropType.Ref, list: false, Ord("WeaponCategories"), "WeaponCategories"),
			NewControl("Traits", PropType.Ref, list: true, Ord("WeaponTraits"), "WeaponTraits"),
			NewControl("Scale", PropType.Float),
			NewControl("UsageAnimation", PropType.Ref, list: false, Nullable(Options("Animations")), "Animations"),
			NewControl("UsageAnimation2Hand", PropType.Ref, list: false, Nullable(Options("Animations")), "Animations"),
			NewControl("UsageAnimationDual", PropType.Ref, list: false, Nullable(Options("Animations")), "Animations"),
			NewControl("UseageAnimLevel", PropType.Int),
			NewControl("ThrowAnimation", PropType.Ref, list: false, Nullable(Options("Animations")), "Animations"),
			NewControl("PistolWhip", PropType.Ref, list: false, Nullable(Options("Animations")), "Animations"),
			NewControl("UsageSounds", PropType.Ref, list: false, Nullable(Options("SoundPack")), "SoundPack"),
			NewControl("ExecutionsMainHand", PropType.Ref, list: true, Options("Execution"), "Execution"),
			NewControl("ExecutionsOffHand", PropType.Ref, list: true, Options("Execution"), "Execution"),
			NewControl("BrokenReplacement", PropType.Ref, list: false, Nullable(Options("Weapon")), "Weapon"),
			NewControl("BrokenExplosion", PropType.Ref, list: false, Nullable(Options("Explosion")), "Explosion"),
			NewControl("MeleeAmmo", PropType.Int),
			NewControl("MeleeDamage", PropType.Float),
			NewControl("MeleeDamageType", PropType.Ref, list: false, Ord("DamageTypes"), "DamageTypes"),
			NewControl("MeleeArmorPiercing", PropType.Float),
			NewControl("MeleeDefense", PropType.Float),
			NewControl("MeleeDoT", PropType.Ref, list: false, Nullable(Options("DoT")), "DoT"),
			NewControl("SweepWidth", PropType.Float),
			NewControl("Trail", PropType.Ref, list: false, Nullable(Asset("Material")), "Material"),
			NewControl("TrailHeavy", PropType.Ref, list: false, Nullable(Asset("Material")), "Material"),
			NewControl("WallHit", PropType.Ref, list: false, Nullable(Asset("ParticleSystem")), "ParticleSystem"),
			NewControl("UseEffects", PropType.Obj, list: false, Obj_BuffEffect(), "BuffEffect")
		};
		return StatCard_Item(@ref, objRef).Union(second).ToArray();
	}

	public static PropertyControl[] StatCard_Collectible()
	{
		PropertyControl[] second = new PropertyControl[1] { NewControl("CollectSound", PropType.Ref, list: false, Asset("SoundEvent"), "SoundEvent", IsFixed: false, CustomVal: true) };
		return StatCard_Item(Ord("GameObject"), "GameObject").Union(second).ToArray();
	}

	public static PropertyControl[] StatCard_Melee()
	{
		return Statcard_Weapon(Asset("MadObj_Melee"), "MadObj_Melee");
	}

	public static PropertyControl[] StatCard_Ammo()
	{
		return new PropertyControl[33]
		{
			NewControl("Name", PropType.Str),
			NewControl("NameShort", PropType.Str),
			NewControl("Description", PropType.Str),
			NewControl("Size", PropType.Float),
			NewControl("Bullet", PropType.Ref, list: false, Asset("MadObj_Projectile"), "GameObject"),
			NewControl("Flare", PropType.Ref, list: false, Nullable(Asset("Particle_Flare")), "ParticleSystem"),
			NewControl("Casing", PropType.Ref, list: false, Nullable(Asset("MadObj_Casing")), "ParticleSystem"),
			NewControl("Hit", PropType.Ref, list: false, Nullable(Asset("ParticleSystem")), "ParticleSystem"),
			NewControl("Impact", PropType.Ref, list: false, Nullable(Asset("ParticleSystem")), "ParticleSystem"),
			NewControl("Deflect", PropType.Ref, list: false, Nullable(Asset("ParticleSystem")), "ParticleSystem"),
			NewControl("Accuracy", PropType.Float),
			NewControl("ArmorPiercing", PropType.Float),
			NewControl("Damage", PropType.Float),
			NewControl("Loudness", PropType.Float),
			NewControl("Range", PropType.Float),
			NewControl("Recoil", PropType.Float),
			NewControl("Shots", PropType.Int),
			NewControl("Spread", PropType.Float),
			NewControl("ShotSpeed", PropType.Float),
			NewControl("SpeedBoost", PropType.Obj, list: false, Obj_VectorList(), "Vec2"),
			NewControl("GravityMult", PropType.Float),
			NewControl("HeatSeek", PropType.Float),
			NewControl("AOERange", PropType.Float),
			NewControl("UseEffects", PropType.Obj, list: false, Obj_BuffEffect(), "BuffEffect"),
			NewControl("DamageType", PropType.Ref, list: false, Ord("DamageTypes"), "DamageTypes"),
			NewControl("DoT", PropType.Ref, list: false, Nullable(Options("DoT")), "DoT"),
			NewControl("Explosion", PropType.Ref, list: false, Nullable(Options("Explosion")), "Explosion"),
			NewControl("ExplodeTimer", PropType.Float),
			NewControl("CasingSound", PropType.Ref, list: false, Asset("SoundEvent"), "SoundEvent", IsFixed: false, CustomVal: true),
			NewControl("ImpactSound", PropType.Ref, list: false, Asset("SoundEvent"), "SoundEvent", IsFixed: false, CustomVal: true),
			NewControl("BecomeDebris", PropType.Bool),
			NewControl("UseBulletNormal", PropType.Bool),
			NewControl("NoSFXOnHit", PropType.Bool)
		};
	}

	public static PropertyControl[] StatCard_Weapon_Stats(string[] references, string objRef)
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
			NewControl("AmmoType", PropType.Ref, list: false, Nullable(Options("Ammo")), "Ammo"),
			NewControl("BurstFire", PropType.Int),
			NewControl("SoundPath", PropType.Ref, list: false, Asset("SoundEvent"), "SoundEvent", IsFixed: false, CustomVal: true),
			NewControl("ROF", PropType.Float),
			NewControl("Recovery", PropType.Float),
			NewControl("Reliability", PropType.Float),
			NewControl("Reload", PropType.Float),
			NewControl("Stability", PropType.Float),
			NewControl("BonusAOERange", PropType.Float),
			NewControl("BonusGravity", PropType.Float),
			NewControl("BonusHeatseek", PropType.Float),
			NewControl("BonusRounds", PropType.Int),
			NewControl("BonusShots", PropType.Int),
			NewControl("BonusSpeed", PropType.Float),
			NewControl("BonusSpread", PropType.Float),
			NewControl("BonusSpeedBoost", PropType.Obj, list: false, Obj_VectorList(), "Vec2")
		};
		return Statcard_Weapon(references, objRef).Union(second).ToArray();
	}

	public static PropertyControl[] StatCard_Thrown()
	{
		PropertyControl[] second = new PropertyControl[1] { NewControl("AmHeld", PropType.Bool) };
		return StatCard_Weapon_Stats(Asset("MadObj_Ranged"), "MadObj_Ranged").Union(second).ToArray();
	}

	public static PropertyControl[] StatCard_WeaponAddon()
	{
		PropertyControl[] second = new PropertyControl[1] { NewControl("AddonType", PropType.Ref, list: false, Ord("AddonTypes"), "AddonTypes") };
		return StatCard_Weapon_Stats(Asset("MadObj_Addon"), "MadObj_Addon").Union(second).ToArray();
	}

	public static PropertyControl[] StatCard_Ranged()
	{
		PropertyControl[] second = new PropertyControl[12]
		{
			NewControl("AmAutomatic", PropType.Bool),
			NewControl("AmBreach", PropType.Bool),
			NewControl("InfiniteReloads", PropType.Bool),
			NewControl("AutomaticWarmup", PropType.Int),
			NewControl("EmptyMagSFX", PropType.Ref, list: false, Asset("SoundEvent"), "SoundEvent", IsFixed: false, CustomVal: true),
			NewControl("DefaultAddons", PropType.Ref, list: true, Options("Addon"), "Addon"),
			NewControl("DefaultGroups", PropType.Ref, list: true, Options("AddonGroup"), "AddonGroup"),
			NewControl("CoreAddons", PropType.Ref, list: true, Options("Addon"), "Addon"),
			NewControl("CoreGroups", PropType.Ref, list: true, Options("AddonGroup"), "AddonGroup"),
			NewControl("AllowedAddons", PropType.Ref, list: true, Options("Addon"), "Addon"),
			NewControl("AllowedGroups", PropType.Ref, list: true, Options("AddonGroup"), "AddonGroup"),
			NewControl("ReloadRule", PropType.Ref, list: false, Ord("ReloadRules"), "ReloadRules")
		};
		return StatCard_Weapon_Stats(Asset("MadObj_Ranged"), "MadObj_Ranged").Union(second).ToArray();
	}

	public static PropertyControl[] Statcard_Armor()
	{
		PropertyControl[] second = new PropertyControl[12]
		{
			NewControl("Protection", PropType.Float),
			NewControl("Encumbrance", PropType.Float),
			NewControl("Slot", PropType.Ref, list: false, Ord("ArmorSlots"), "ArmorSlots", IsFixed: false, CustomVal: true),
			NewControl("DisablePartOnEquip", PropType.Bool),
			NewControl("Traits", PropType.Ref, list: true, Ord("ArmorTraits"), "ArmorTraits"),
			NewControl("TintsPrimary", PropType.Color, list: true),
			NewControl("TintsSecondary", PropType.Color, list: true),
			NewControl("TintsGlass", PropType.Color, list: true),
			NewControl("Swatch", PropType.Ref, list: false, Nullable(Options("Swatch")), "Swatch"),
			NewControl("Executions", PropType.Ref, list: true, Options("Execution"), "Execution"),
			NewControl("DeathEvents", PropType.Obj, list: true, Obj_DeathEvent(), "DeathEvent"),
			NewControl("AltDamageType", PropType.Ref, list: false, Ord("DamageTypes"), "DamageTypes")
		};
		return StatCard_Item(Asset("MadObj_Armor"), "MadObj_Armor").Union(second).ToArray();
	}

	public static PropertyControl[] StatCard_AddonGroup()
	{
		return new PropertyControl[5]
		{
			NewControl("Name", PropType.Str),
			NewControl("NameShort", PropType.Str),
			NewControl("Description", PropType.Str),
			NewControl("Addons", PropType.Ref, list: true, Options("Addon"), "Addon"),
			NewControl("TetherTo", PropType.Ref, list: true, Options("AddonGroup"), "AddonGroup")
		};
	}

	public static PropertyControl[] Statcard_Animation()
	{
		return new PropertyControl[9]
		{
			NewControl("Name", PropType.Str),
			NewControl("NameShort", PropType.Str),
			NewControl("Description", PropType.Str),
			NewControl("Clips", PropType.Ref, list: true, Asset("AnimationClip"), "AnimationClip"),
			NewControl("ClipPairs", PropType.Obj, list: true, Obj_AnimationClip(), "AnimationPair"),
			NewControl("LinkedGroups", PropType.Ref, list: true, Options("Animations"), "Animations"),
			NewControl("RequiredCore", PropType.Int),
			NewControl("BoolVals", PropType.Bool, list: true, null, "Object", IsFixed: true),
			NewControl("FloatVals", PropType.Float, list: true, null, "Object", IsFixed: true)
		};
	}

	public static PropertyControl[] Statcard_Skill()
	{
		return new PropertyControl[13]
		{
			NewControl("Name", PropType.Str),
			NewControl("NameShort", PropType.Str),
			NewControl("Description", PropType.Str),
			NewControl("Instructions", PropType.Str),
			NewControl("Tier", PropType.Int),
			NewControl("Category", PropType.Int),
			NewControl("SkillTree", PropType.Ref, list: false, Ord("SkillTrees"), "SkillTrees"),
			NewControl("PreReqSkills", PropType.Ref, list: true, Options("Skill"), "Skill"),
			NewControl("Skill", PropType.Ref, list: false, Ord("Skills"), "Skills"),
			NewControl("XP_Cost", PropType.Int),
			NewControl("ProficientOnly", PropType.Bool),
			NewControl("PrioritySkill", PropType.Bool),
			NewControl("CoreSkill", PropType.Bool)
		};
	}

	public static PropertyControl[] Statcard_AnimSet()
	{
		return new PropertyControl[46]
		{
			NewControl("Idle", PropType.Ref, list: false, Options("Animations"), "Animations"),
			NewControl("Walk", PropType.Ref, list: false, Nullable(Options("Animations")), "Animations"),
			NewControl("Run", PropType.Ref, list: false, Nullable(Options("Animations")), "Animations"),
			NewControl("Dash", PropType.Ref, list: false, Nullable(Options("Animations")), "Animations"),
			NewControl("Pickup", PropType.Ref, list: false, Nullable(Options("Animations")), "Animations"),
			NewControl("LandFall", PropType.Ref, list: false, Nullable(Options("Animations")), "Animations"),
			NewControl("Stand", PropType.Ref, list: false, Nullable(Options("Animations")), "Animations"),
			NewControl("EmoteIdle", PropType.Ref, list: false, Nullable(Options("Animations")), "Animations"),
			NewControl("EmoteHunt", PropType.Ref, list: false, Nullable(Options("Animations")), "Animations"),
			NewControl("EmoteWakeUp", PropType.Ref, list: false, Nullable(Options("Animations")), "Animations"),
			NewControl("EmoteCombat", PropType.Ref, list: false, Nullable(Options("Animations")), "Animations"),
			NewControl("EmoteMad", PropType.Ref, list: false, Nullable(Options("Animations")), "Animations"),
			NewControl("OverrideGrapple", PropType.Ref, list: false, Nullable(Options("Animations")), "Animations"),
			NewControl("OverrideUnarmed", PropType.Ref, list: false, Nullable(Options("Animations")), "Animations"),
			NewControl("OverrideCoreUnarmed", PropType.Ref, list: true, Nullable(Options("Animations")), "Animations"),
			NewControl("OverrideMelee", PropType.Ref, list: false, Nullable(Options("Animations")), "Animations"),
			NewControl("OverrideCoreMelee", PropType.Ref, list: true, Nullable(Options("Animations")), "Animations"),
			NewControl("OverrideMelee2h", PropType.Ref, list: false, Nullable(Options("Animations")), "Animations"),
			NewControl("OverrideCoreMelee2h", PropType.Ref, list: true, Nullable(Options("Animations")), "Animations"),
			NewControl("OverrideCoreMeleeDual", PropType.Ref, list: true, Nullable(Options("Animations")), "Animations"),
			NewControl("OverrideRanged", PropType.Ref, list: false, Nullable(Options("Animations")), "Animations"),
			NewControl("OverrideBlock", PropType.Ref, list: false, Nullable(Options("Animations")), "Animations"),
			NewControl("OverrideDashAttack", PropType.Ref, list: false, Nullable(Options("Animations")), "Animations"),
			NewControl("OverrideDashSpecial", PropType.Ref, list: false, Nullable(Options("Animations")), "Animations"),
			NewControl("OverrideDodge", PropType.Ref, list: false, Nullable(Options("Animations")), "Animations"),
			NewControl("OverrideSnapDodge", PropType.Ref, list: false, Nullable(Options("Animations")), "Animations"),
			NewControl("OverrideCoreDodge", PropType.Ref, list: true, Nullable(Options("Animations")), "Animations"),
			NewControl("OverrideChargeUp", PropType.Ref, list: false, Nullable(Options("Animations")), "Animations"),
			NewControl("OverrideShove", PropType.Ref, list: false, Nullable(Options("Animations")), "Animations"),
			NewControl("OverrideHeadLeap", PropType.Ref, list: false, Nullable(Options("Animations")), "Animations"),
			NewControl("OverrideCounterMvM", PropType.Ref, list: false, Nullable(Options("Animations")), "Animations"),
			NewControl("OverrideCounterMvU", PropType.Ref, list: false, Nullable(Options("Animations")), "Animations"),
			NewControl("OverrideCounterUvM", PropType.Ref, list: false, Nullable(Options("Animations")), "Animations"),
			NewControl("OverrideCounterUvU", PropType.Ref, list: false, Nullable(Options("Animations")), "Animations"),
			NewControl("UnarmedTiedUp", PropType.Ref, list: false, Nullable(Options("Animations")), "Animations"),
			NewControl("RearSmash", PropType.Ref, list: false, Nullable(Options("Animations")), "Animations"),
			NewControl("Bullrush", PropType.Ref, list: false, Options("Animations"), "Animations"),
			NewControl("SpawnIn", PropType.Ref, list: false, Options("Animations"), "Animations"),
			NewControl("SpecialAngry", PropType.Ref, list: false, Nullable(Options("Animations")), "Animations"),
			NewControl("Congratulate", PropType.Ref, list: false, Nullable(Options("Animations")), "Animations"),
			NewControl("StunBody", PropType.Ref, list: false, Nullable(Options("Animations")), "Animations"),
			NewControl("StunHead", PropType.Ref, list: false, Nullable(Options("Animations")), "Animations"),
			NewControl("DeathBody", PropType.Ref, list: false, Nullable(Options("Animations")), "Animations"),
			NewControl("DeathHead", PropType.Ref, list: false, Nullable(Options("Animations")), "Animations"),
			NewControl("FootstepSFX", PropType.Ref, list: true, Asset("SoundEvent"), "SoundEvent", IsFixed: false, CustomVal: true),
			NewControl("FallLandSFX", PropType.Ref, list: true, Asset("SoundEvent"), "SoundEvent", IsFixed: false, CustomVal: true)
		};
	}

	public static PropertyControl[] Statcard_Chatter()
	{
		return new PropertyControl[16]
		{
			NewControl("Name", PropType.Str),
			NewControl("NameShort", PropType.Str),
			NewControl("Description", PropType.Str),
			NewControl("Font", PropType.Ref, list: false, Ord("Font"), "Font"),
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

	public static PropertyControl[] Statcard_Corpus()
	{
		return new PropertyControl[9]
		{
			NewControl("Name", PropType.Str),
			NewControl("NameShort", PropType.Str),
			NewControl("Description", PropType.Str),
			NewControl("Color", PropType.Color),
			NewControl("Icon", PropType.Ref, list: false, Asset("Sprite"), "Sprite"),
			NewControl("Buffs", PropType.Obj, list: true, Obj_BonusStats(), "BonusStat"),
			NewControl("DamageMultiplier", PropType.Float),
			NewControl("DeathEvents", PropType.Obj, list: true, Obj_DeathEvent(), "DeathEvent"),
			NewControl("BonusTraits", PropType.Ref, list: true, Ord("Traits"), "Traits")
		};
	}

	public static PropertyControl[] Statcard_DoT()
	{
		return new PropertyControl[17]
		{
			NewControl("Name", PropType.Str),
			NewControl("NameShort", PropType.Str),
			NewControl("Description", PropType.Str),
			NewControl("Damage", PropType.Float),
			NewControl("DamageTick", PropType.Float),
			NewControl("Duration", PropType.Float),
			NewControl("Chance", PropType.Float),
			NewControl("FlashColor", PropType.Color),
			NewControl("DamageType", PropType.Ref, list: false, Ord("DamageTypes"), "DamageTypes"),
			NewControl("NoBlood", PropType.Bool),
			NewControl("Debuffs", PropType.Ref, list: true, Ord("BuffEffects"), "BuffEffects"),
			NewControl("Effects", PropType.Ref, list: true, Ord("DoTEffects"), "DoTEffects"),
			NewControl("EffectDamage", PropType.Ref, list: false, Asset("ParticleSystem"), "ParticleSystem"),
			NewControl("EffectConstant", PropType.Ref, list: false, Asset("ParticleSystem"), "ParticleSystem"),
			NewControl("AttachPart", PropType.Ref, list: false, Ord("ModelParts"), "ModelParts"),
			NewControl("EndActions", PropType.Ref, list: true, Ord("DoTResolutions"), "DoTResolutions"),
			NewControl("ReduceActions", PropType.Ref, list: true, Ord("DoTResolutions"), "DoTResolutions")
		};
	}

	public static PropertyControl[] Statcard_Execution()
	{
		return new PropertyControl[13]
		{
			NewControl("Name", PropType.Str),
			NewControl("NameShort", PropType.Str),
			NewControl("Description", PropType.Str),
			NewControl("Source", PropType.Ref, list: false, Ord("ExecuteSource"), "ExecuteSource"),
			NewControl("Conditions", PropType.Ref, list: true, Ord("ExecuteConditions"), "ExecuteConditions"),
			NewControl("Urgency", PropType.Int),
			NewControl("ScaleDifference", PropType.Float, list: true, null, "Float", IsFixed: true),
			NewControl("Keycard", PropType.Ref, list: false, Nullable(Options("Weapon")), "Weapon"),
			NewControl("CheckOffHand", PropType.Bool),
			NewControl("SpecialRule", PropType.Ref, list: false, Ord("ExecuteRules"), "ExecuteRules"),
			NewControl("SelectColor", PropType.Ref, list: false, Ord("ExecuteColors"), "ExecuteColors"),
			NewControl("ClipPairs", PropType.Obj, list: true, Obj_AnimationClip(), "AnimationPair"),
			NewControl("NoKillPairs", PropType.Obj, list: true, Obj_AnimationClip(), "AnimationPair")
		};
	}

	public static PropertyControl[] Statcard_Explosion()
	{
		return new PropertyControl[16]
		{
			NewControl("Name", PropType.Str),
			NewControl("NameShort", PropType.Str),
			NewControl("Description", PropType.Str),
			NewControl("Effect", PropType.Ref, list: false, Nullable(Asset("ParticleSystem")), "ParticleSystem"),
			NewControl("Crater", PropType.Ref, list: false, Nullable(Options("VisualStockpile")), "VisualStockpile"),
			NewControl("ExplosionSound", PropType.Ref, list: false, Asset("SoundEvent"), "SoundEvent", IsFixed: false, CustomVal: true),
			NewControl("Damage", PropType.Float),
			NewControl("DamageType", PropType.Ref, list: false, Ord("DamageTypes")),
			NewControl("DoT", PropType.Ref, list: false, Nullable(Options("DoT")), "DoT"),
			NewControl("ArmorPiercing", PropType.Float),
			NewControl("Range", PropType.Float),
			NewControl("Dropoff", PropType.Float),
			NewControl("HitThroughWalls", PropType.Bool),
			NewControl("Ragdoll", PropType.Float),
			NewControl("RagdollYMultiplier", PropType.Float),
			NewControl("VictimsImmuneFallDamage", PropType.Bool)
		};
	}

	public static PropertyControl[] Statcard_Loadout()
	{
		return new PropertyControl[6]
		{
			NewControl("Name", PropType.Str),
			NewControl("NameShort", PropType.Str),
			NewControl("Description", PropType.Str),
			NewControl("Tier", PropType.Int),
			NewControl("Weapons", PropType.Ref, list: true, Nullable(Options("Weapon")), "Weapon"),
			NewControl("BorrowLoadouts", PropType.Ref, list: true, Options("Loadout"), "Loadout")
		};
	}

	public static PropertyControl[] Statcard_Wardrobe()
	{
		return new PropertyControl[8]
		{
			NewControl("Name", PropType.Str),
			NewControl("NameShort", PropType.Str),
			NewControl("Description", PropType.Str),
			NewControl("Gear", PropType.Ref, list: true, Options("Armor"), "Armor"),
			NewControl("Uniform", PropType.Obj, list: true, Obj_UniformList(), "Uniform"),
			NewControl("EmptySlots", PropType.Ref, list: true, Ord("ArmorSlots"), "ArmorSlots", IsFixed: false, CustomVal: true),
			NewControl("BonusCorpus", PropType.Ref, list: true, Options("Corpus"), "Corpus"),
			NewControl("WipeDefaultCorpus", PropType.Bool)
		};
	}

	public static PropertyControl[] Statcard_Origin()
	{
		return new PropertyControl[19]
		{
			NewControl("Name", PropType.Str),
			NewControl("NameShort", PropType.Str),
			NewControl("Description", PropType.Str),
			NewControl("Imprints", PropType.Int),
			NewControl("Icon", PropType.Ref, list: false, Asset("Sprite"), "Sprite"),
			NewControl("LinkedCharacter", PropType.Ref, list: false, Options("Character"), "Character"),
			NewControl("Buffs", PropType.Obj, list: true, Obj_BonusStats(), "BonusStat"),
			NewControl("SkillFocus", PropType.Ref, list: false, Ord("SkillTrees"), "SkillTrees"),
			NewControl("SkillWeakness", PropType.Ref, list: false, Ord("SkillTrees"), "SkillTrees"),
			NewControl("AttributeBonuses", PropType.Int, list: true, null, "Object", IsFixed: true),
			NewControl("SpecialSkills", PropType.Ref, list: true, Options("Skill"), "Skill"),
			NewControl("DefaultArmor", PropType.Ref, list: true, Options("Armor"), "Armor"),
			NewControl("DefaultUniforms", PropType.Obj, list: true, Obj_UniformList(), "Uniform"),
			NewControl("Swatch", PropType.Ref, list: false, Nullable(Options("Swatch")), "Swatch"),
			NewControl("WarchestWeapons", PropType.Ref, list: true, Options("Weapon"), "Weapon"),
			NewControl("WarchestGear", PropType.Ref, list: true, Options("Armor"), "Armor"),
			NewControl("ExpandNowhere", PropType.Bool),
			NewControl("Swatch_Pairs", PropType.Obj, list: true, Obj_ColorPair(), "ColorPair"),
			NewControl("Swatch_Solos", PropType.Obj, list: true, Obj_ColorSolo(), "ColorSolo")
		};
	}

	public static PropertyControl[] Statcard_Skillgroup()
	{
		return new PropertyControl[2]
		{
			NewControl("Skills", PropType.Ref, list: true, Ord("Skills"), "Skills"),
			NewControl("IncludeGroup", PropType.Ref, list: true, Options("SkillGroup"), "SkillGroup")
		};
	}

	public static PropertyControl[] Statcard_Skin()
	{
		return new PropertyControl[9]
		{
			NewControl("Name", PropType.Str),
			NewControl("NameShort", PropType.Str),
			NewControl("Description", PropType.Str),
			NewControl("CanBuy", PropType.Bool),
			NewControl("Cost", PropType.Int),
			NewControl("MenuIcon", PropType.Ref, list: false, Asset("Sprite"), "Sprite"),
			NewControl("MenuColor", PropType.Color),
			NewControl("AltParts", PropType.Ref, list: true, Asset("MadObj_BodyPart"), "MadObj_BodyPart"),
			NewControl("AltMaterials", PropType.Obj, list: true, Obj_MaterialSwaps(), "MatSwap")
		};
	}

	public static PropertyControl[] Statcard_SoundPack()
	{
		return new PropertyControl[11]
		{
			NewControl("Name", PropType.Str),
			NewControl("NameShort", PropType.Str),
			NewControl("Description", PropType.Str),
			NewControl("Hit", PropType.Ref, list: false, Asset("SoundEvent"), "SoundEvent", IsFixed: false, CustomVal: true),
			NewControl("HitLight", PropType.Ref, list: false, Asset("SoundEvent"), "SoundEvent", IsFixed: false, CustomVal: true),
			NewControl("Swing", PropType.Ref, list: false, Asset("SoundEvent"), "SoundEvent", IsFixed: false, CustomVal: true),
			NewControl("SwingLight", PropType.Ref, list: false, Asset("SoundEvent"), "SoundEvent", IsFixed: false, CustomVal: true),
			NewControl("Block", PropType.Ref, list: false, Asset("SoundEvent"), "SoundEvent", IsFixed: false, CustomVal: true),
			NewControl("Drop", PropType.Ref, list: false, Asset("SoundEvent"), "SoundEvent", IsFixed: false, CustomVal: true),
			NewControl("Equip", PropType.Ref, list: false, Asset("SoundEvent"), "SoundEvent", IsFixed: false, CustomVal: true),
			NewControl("Stow", PropType.Ref, list: false, Asset("SoundEvent"), "SoundEvent", IsFixed: false, CustomVal: true)
		};
	}

	public static PropertyControl[] Statcard_Swatch()
	{
		return new PropertyControl[7]
		{
			NewControl("Name", PropType.Str),
			NewControl("NameShort", PropType.Str),
			NewControl("Description", PropType.Str),
			NewControl("CanBuy", PropType.Bool),
			NewControl("TokenCost", PropType.Int),
			NewControl("ColorPairs", PropType.Obj, list: true, Obj_ColorPair(), "ColorPair"),
			NewControl("ColorSolos", PropType.Obj, list: true, Obj_ColorSolo(), "ColorSolo")
		};
	}

	public static PropertyControl[] Statcard_Texture()
	{
		return new PropertyControl[12]
		{
			NewControl("Textures1", PropType.Ref, list: true, Asset("Texture2D"), "Texture2D"),
			NewControl("Textures2", PropType.Ref, list: true, Asset("Texture2D"), "Texture2D"),
			NewControl("Textures3", PropType.Ref, list: true, Asset("Texture2D"), "Texture2D"),
			NewControl("Textures4", PropType.Ref, list: true, Asset("Texture2D"), "Texture2D"),
			NewControl("Materials1", PropType.Ref, list: true, Asset("Material"), "Material"),
			NewControl("Materials2", PropType.Ref, list: true, Asset("Material"), "Material"),
			NewControl("Materials3", PropType.Ref, list: true, Asset("Material"), "Material"),
			NewControl("Materials4", PropType.Ref, list: true, Asset("Material"), "Material"),
			NewControl("Objects1", PropType.Ref, list: true, Asset("ParticleSystem"), "GameObject"),
			NewControl("Objects2", PropType.Ref, list: true, Asset("ParticleSystem"), "GameObject"),
			NewControl("Sprites1", PropType.Ref, list: true, Asset("Sprite"), "Sprite"),
			NewControl("Colors", PropType.Color, list: true)
		};
	}

	public static PropertyControl[] Statcard_VoiceSet()
	{
		return new PropertyControl[17]
		{
			NewControl("Name", PropType.Str),
			NewControl("NameShort", PropType.Str),
			NewControl("Description", PropType.Str),
			NewControl("Angry", PropType.Ref, list: false, Asset("SoundEvent"), "SoundEvent", IsFixed: false, CustomVal: true),
			NewControl("Attack", PropType.Ref, list: false, Asset("SoundEvent"), "SoundEvent", IsFixed: false, CustomVal: true),
			NewControl("BigAttack", PropType.Ref, list: false, Asset("SoundEvent"), "SoundEvent", IsFixed: false, CustomVal: true),
			NewControl("Cheer", PropType.Ref, list: false, Asset("SoundEvent"), "SoundEvent", IsFixed: false, CustomVal: true),
			NewControl("Laugh", PropType.Ref, list: false, Asset("SoundEvent"), "SoundEvent", IsFixed: false, CustomVal: true),
			NewControl("Affirmative", PropType.Ref, list: false, Asset("SoundEvent"), "SoundEvent", IsFixed: false, CustomVal: true),
			NewControl("Dialogue", PropType.Ref, list: false, Asset("SoundEvent"), "SoundEvent", IsFixed: false, CustomVal: true),
			NewControl("Grunt", PropType.Ref, list: false, Asset("SoundEvent"), "SoundEvent", IsFixed: false, CustomVal: true),
			NewControl("Pain", PropType.Ref, list: false, Asset("SoundEvent"), "SoundEvent", IsFixed: false, CustomVal: true),
			NewControl("Death", PropType.Ref, list: false, Asset("SoundEvent"), "SoundEvent", IsFixed: false, CustomVal: true),
			NewControl("Gasp", PropType.Ref, list: false, Asset("SoundEvent"), "SoundEvent", IsFixed: false, CustomVal: true),
			NewControl("Buy", PropType.Ref, list: false, Asset("SoundEvent"), "SoundEvent", IsFixed: false, CustomVal: true),
			NewControl("Sell", PropType.Ref, list: false, Asset("SoundEvent"), "SoundEvent", IsFixed: false, CustomVal: true),
			NewControl("Interact", PropType.Ref, list: false, Asset("SoundEvent"), "SoundEvent", IsFixed: false, CustomVal: true)
		};
	}

	public static PropertyControl[] Statcard_Story()
	{
		return new PropertyControl[11]
		{
			NewControl("Name", PropType.Str),
			NewControl("NameShort", PropType.Str),
			NewControl("Description", PropType.Str),
			NewControl("Tier", PropType.Int),
			NewControl("LoadScreenTips", PropType.Str, list: true),
			NewControl("Music", PropType.Ref, list: true, Asset("Music"), "Music", IsFixed: false, CustomVal: true),
			NewControl("RequiredCharacters", PropType.Obj, list: true, Obj_CharacterSet(), "CharacterSet"),
			NewControl("BonusCharacters", PropType.Obj, list: true, Obj_CharacterSet(), "CharacterSet"),
			NewControl("PrizeLoadout", PropType.Ref, list: true, Options("Loadout"), "Loadout"),
			NewControl("PrizeWardrobe", PropType.Ref, list: true, Options("Wardrobe"), "Wardrobe"),
			NewControl("WipeSquadOnVictory", PropType.Bool)
		};
	}

	public static PropertyControl[] Statcard_CustomStage()
	{
		PropertyControl[] second = new PropertyControl[23]
		{
			NewControl("Enabled", PropType.Bool),
			NewControl("SceneName", PropType.Str),
			NewControl("Missions", PropType.Ref, list: true, Options("Mission"), "Mission"),
			NewControl("AdditionalObjectives", PropType.Str, list: true),
			NewControl("Snapshot", PropType.Ref, list: false, Asset("Sprite"), "Sprite"),
			NewControl("PrereqStages", PropType.Ref, list: true, Options("Stage_Custom"), "Stage_Custom"),
			NewControl("PrereqStages_ToSee", PropType.Ref, list: true, Options("Stage_Custom"), "Stage_Custom"),
			NewControl("LinkedStageVictory", PropType.Ref, list: true, Options("Stage_Custom"), "Stage_Custom"),
			NewControl("AmHub", PropType.Bool),
			NewControl("IgnorePlayerDeathLoss", PropType.Bool),
			NewControl("HubDifficulty", PropType.Ref, list: false, Ord("GameDifficulty"), "GameDifficulty"),
			NewControl("LinkedOverworld", PropType.Ref, list: false, Options("Stage_Custom"), "Stage_Custom"),
			NewControl("Leave_Default_ToStage", PropType.Ref, list: false, Nullable(Options("Stage_Custom")), "Stage_Custom"),
			NewControl("Leave_Default_ToDoor", PropType.Str),
			NewControl("Leave_NeverWon_ToStage", PropType.Ref, list: false, Nullable(Options("Stage_Custom")), "Stage_Custom"),
			NewControl("Leave_NeverWon_ToDoor", PropType.Str),
			NewControl("Leave_Forbid_ToStage", PropType.Ref, list: false, Nullable(Options("Stage_Custom")), "Stage_Custom"),
			NewControl("Leave_Forbid_ToDoor", PropType.Str),
			NewControl("Leave_Forbid_WorldChange", PropType.Str),
			NewControl("Leave_Prereq_ToStage", PropType.Ref, list: false, Nullable(Options("Stage_Custom")), "Stage_Custom"),
			NewControl("Leave_Prereq_ToDoor", PropType.Str),
			NewControl("Leave_Victory_ToStage", PropType.Ref, list: false, Nullable(Options("Stage_Custom")), "Stage_Custom"),
			NewControl("Leave_Victory_ToDoor", PropType.Str)
		};
		return Statcard_Story().Union(second).ToArray();
	}

	public static PropertyControl[] Statcard_Campaign()
	{
		return new PropertyControl[9]
		{
			NewControl("Name", PropType.Str),
			NewControl("NameShort", PropType.Str),
			NewControl("Description", PropType.Str),
			NewControl("StartStage", PropType.Ref, list: false, Options("Stage_Custom"), "Stage_Custom"),
			NewControl("Hubs", PropType.Ref, list: true, Options("Stage_Custom"), "Stage_Custom"),
			NewControl("ColdOpenDifficulty", PropType.Ref, list: false, Ord("GameDifficulty"), "GameDifficulty"),
			NewControl("MenuIcon", PropType.Ref, list: false, Asset("Sprite"), "Sprite"),
			NewControl("LoadScreenTips", PropType.Str, list: true),
			NewControl("UnlockLoadouts", PropType.Bool)
		};
	}

	public static PropertyControl[] Statcard_Arena()
	{
		return new PropertyControl[24]
		{
			NewControl("Name", PropType.Str),
			NewControl("NameShort", PropType.Str),
			NewControl("Description", PropType.Str),
			NewControl("Tier", PropType.Int),
			NewControl("SceneName", PropType.Str),
			NewControl("LoadScreenTips", PropType.Str, list: true),
			NewControl("Music", PropType.Ref, list: true, Asset("Music"), "Music", IsFixed: false, CustomVal: true),
			NewControl("MaxEnemyMult", PropType.Float),
			NewControl("EnemySquadMult", PropType.Float),
			NewControl("WaveName", PropType.Str),
			NewControl("ArenaMode", PropType.Ref, list: false, Ord("ArenaMode"), "ArenaMode"),
			NewControl("ReturnOnWin", PropType.Ref, list: false, Ord("StartLocation"), "StartLocation"),
			NewControl("ReturnOnDie", PropType.Ref, list: false, Ord("StartLocation"), "StartLocation"),
			NewControl("ReturnOnQuit", PropType.Ref, list: false, Ord("StartLocation"), "StartLocation"),
			NewControl("DeathIsFake", PropType.Bool),
			NewControl("SceneOnWin", PropType.Str),
			NewControl("DoorOnWin", PropType.Str),
			NewControl("MaxHirelings", PropType.Int),
			NewControl("TrooperOverride", PropType.Obj, list: true, Obj_SQSquad(), "Reinforce"),
			NewControl("TrooperChatterOverride", PropType.Str, list: true),
			NewControl("WaveSequences", PropType.Obj, list: true, Obj_ArenaSequence(), "WaveSequence"),
			NewControl("InfiniteSequence", PropType.Obj, list: false, Obj_ArenaSequence(), "WaveSequence"),
			NewControl("PrereqStages", PropType.Ref, list: true, Options("Stage_Arena"), "Stage_Arena"),
			NewControl("WorldChangeOnVictory", PropType.Str)
		};
	}

	public static PropertyControl[] Statcard_Character()
	{
		return new PropertyControl[65]
		{
			NewControl("Name", PropType.Str),
			NewControl("NameShort", PropType.Str),
			NewControl("Description", PropType.Str),
			NewControl("Enabled", PropType.Bool),
			NewControl("Title", PropType.Str),
			NewControl("Level", PropType.Int),
			NewControl("TacBar", PropType.Float),
			NewControl("Scale", PropType.Float),
			NewControl("Speed", PropType.Obj, list: false, Obj_Vector3List(), "Vec3"),
			NewControl("SprintBonus", PropType.Float),
			NewControl("Toughness", PropType.Int),
			NewControl("Health", PropType.Int),
			NewControl("CorpusBlocks", PropType.Ref, list: true, Options("Corpus"), "Corpus"),
			NewControl("Attributes", PropType.Int, list: true, null, "Object", IsFixed: true),
			NewControl("ArmorHead", PropType.Float),
			NewControl("ArmorBody", PropType.Float),
			NewControl("ObjRef", PropType.Ref, list: false, Asset("MadObj_Character"), "MadObj_Character"),
			NewControl("AltParts", PropType.Ref, list: true, Asset("MadObj_BodyPart"), "MadObj_BodyPart"),
			NewControl("AltMaterials", PropType.Obj, list: true, Obj_MaterialSwaps(), "MatSwap"),
			NewControl("Reskins", PropType.Obj, list: true, Obj_Reskin(), "Reskin"),
			NewControl("TintsPrimary", PropType.Color, list: true),
			NewControl("TintsSecondary", PropType.Color, list: true),
			NewControl("TintEffect", PropType.Color),
			NewControl("AnimationSet", PropType.Ref, list: false, Options("AnimationProfile"), "AnimationProfile"),
			NewControl("ExecutionsOffensive", PropType.Ref, list: true, Options("Execution"), "Execution"),
			NewControl("ExecutionsDefensive", PropType.Ref, list: true, Options("Execution"), "Execution"),
			NewControl("VoiceSet", PropType.Ref, list: false, Options("VoiceSet"), "VoiceSet"),
			NewControl("TextChatter", PropType.Ref, list: false, Options("Chatter"), "Chatter"),
			NewControl("Blood", PropType.Color),
			NewControl("BloodSpray", PropType.Ref, list: false, Options("VisualStockpile"), "VisualStockpile"),
			NewControl("BloodSpurt", PropType.Ref, list: false, Asset("Particle_Blood").ToArray(), "ParticleSystem"),
			NewControl("Gibs", PropType.Ref, list: false, Nullable(Options("VisualStockpile")), "VisualStockpile"),
			NewControl("HitSFX", PropType.Ref, list: true, Asset("SoundEvent"), "SoundEvent", IsFixed: false, CustomVal: true),
			NewControl("FallPuff", PropType.Ref, list: false, Asset("ParticleSystem"), "ParticleSystem"),
			NewControl("DefaultArmor", PropType.Ref, list: true, Options("Armor"), "Armor"),
			NewControl("Uniform", PropType.Obj, list: true, Obj_UniformList(), "Uniform"),
			NewControl("CountWornArmor", PropType.Bool),
			NewControl("CountAmmoLikePlayer", PropType.Bool),
			NewControl("Fist", PropType.Ref, list: false, Options("Melee"), "Melee"),
			NewControl("Kick", PropType.Ref, list: false, Nullable(Options("Melee")), "Melee"),
			NewControl("Headbutt", PropType.Ref, list: false, Nullable(Options("Melee")), "Melee"),
			NewControl("SlamExplode", PropType.Ref, list: false, Nullable(Options("Explosion")), "Explosion"),
			NewControl("WeaponPref", PropType.Ref, list: false, Ord("WeaponCategories"), "WeaponCategories"),
			NewControl("WeaponGenres", PropType.Ref, list: true, Ord("ItemGenres"), "ItemGenres", IsFixed: false, CustomVal: true),
			NewControl("DefaultWeapon", PropType.Ref, list: false, Nullable(Options("Weapon")), "Weapon"),
			NewControl("DefaultOffHand", PropType.Ref, list: false, Nullable(Options("Weapon")), "Weapon"),
			NewControl("DefaultSidearm", PropType.Ref, list: false, Nullable(Options("Weapon")), "Weapon"),
			NewControl("DefaultThrown", PropType.Ref, list: false, Nullable(Options("Thrown")), "Weapon"),
			NewControl("UnarmedReplacement", PropType.Ref, list: false, Nullable(Options("Weapon")), "Weapon"),
			NewControl("DefaultLoadout", PropType.Ref, list: false, Nullable(Options("Loadout")), "Loadout"),
			NewControl("Faction", PropType.Ref, list: false, Ord("Factions"), "Factions", IsFixed: false, CustomVal: true),
			NewControl("DefaultControl", PropType.Ref, list: false, Ord("ControllerTypes"), "ControllerTypes"),
			NewControl("Behaviors", PropType.Ref, list: true, Ord("Behaviors"), "Behaviors"),
			NewControl("Traits", PropType.Ref, list: true, Ord("Traits"), "Traits"),
			NewControl("Vocations", PropType.Ref, list: true, Ord("Vocations"), "Vocations", IsFixed: false, CustomVal: true),
			NewControl("Skills", PropType.Ref, list: true, Ord("Skills"), "Skills"),
			NewControl("SkillGroups", PropType.Ref, list: true, Options("SkillGroup"), "SkillGroup"),
			NewControl("RemovedSkills", PropType.Ref, list: true, Ord("Skills"), "Skills"),
			NewControl("SpecialSkills", PropType.Ref, list: true, Options("Skill"), "Skill"),
			NewControl("FlankMe", PropType.Bool),
			NewControl("LifeEffect", PropType.Ref, list: false, Nullable(Asset("Particle_Life")), "Particle_Life"),
			NewControl("DeathEffect", PropType.Ref, list: false, Nullable(Asset("Particle_Death")), "Particle_Death"),
			NewControl("TeleportEffect", PropType.Ref, list: true, Asset("ParticleSystem"), "ParticleSystem"),
			NewControl("Footprint", PropType.Ref, list: false, Asset("Material"), "Material"),
			NewControl("DeactivateTime", PropType.Int)
		};
	}

	public static string[] Nullable(IEnumerable<string> list)
	{
		List<string> list2 = new List<string>();
		list2.Add("None");
		list2.AddRange(list);
		return list2.ToArray();
	}

	public static string[] Options(string types)
	{
		List<string> list = new List<string>();
		list.AddRange(ModEnums.SC_DIR[types].Keys);
		foreach (string key in ModEnums.MC_DIR[types].Keys)
		{
			if (!ModEnums.SC_DIR[types].ContainsKey(key))
			{
				list.Add(key);
			}
		}
		return list.ToArray();
	}

	public static string[] Ord(string key)
	{
		if (!ModEnums.ENUMS.ContainsKey(key))
		{
			return new string[0];
		}
		return ModEnums.ENUMS[key].ToArray();
	}

	public static string[] Asset(string key)
	{
		if (!ModEnums.ASSETS.ContainsKey(key))
		{
			return new string[0];
		}
		return ModEnums.ASSETS[key].ToArray();
	}

	public static PropertyControl[] Filtered(IEnumerable<PropertyControl> list, Dictionary<string, string> filter, Dictionary<string, string[]> newSelections, bool unlisted)
	{
		List<PropertyControl> list2 = new List<PropertyControl>();
		foreach (PropertyControl item in list)
		{
			if (filter.Keys.Contains(item.PropName))
			{
				object obj = null;
				string overrideObject = "Object";
				if (newSelections.ContainsKey(item.PropName))
				{
					overrideObject = filter[item.PropName].Split(' ')[0];
					obj = newSelections[item.PropName];
				}
				list2.Add(NewControl(item, filter[item.PropName], RuntimeHelpers.GetObjectValue(obj), overrideObject, unlisted));
			}
		}
		return list2.ToArray();
	}

	private static PropertyControl NewControl(string Prop, PropType FieldType, bool list = false, object Choices = null, string ObjName = "Object", bool IsFixed = false, bool CustomVal = false, PropertyControl[] DefinedBy = null)
	{
		PropertyControl propertyControl = new PropertyControl();
		propertyControl.PropName = Prop;
		propertyControl.PropType = FieldType;
		propertyControl.IsList = list;
		propertyControl.Selection = RuntimeHelpers.GetObjectValue(Choices);
		propertyControl.ObjectName = ObjName;
		propertyControl.Fixed = IsFixed;
		propertyControl.WriteNewValues = CustomVal;
		propertyControl.Defines = DefinedBy;

		return propertyControl;
	}

	private static PropertyControl NewControl(PropertyControl copy, string overrideDesc, object overrideSelection = null, string overrideObject = "", bool allowUnlisted = false)
	{
		PropertyControl propertyControl;
		PropertyControl result = (propertyControl = new PropertyControl());
		propertyControl.PropName = copy.PropName;
		propertyControl.PropType = copy.PropType;
		propertyControl.IsList = copy.IsList;
		propertyControl.Selection = RuntimeHelpers.GetObjectValue(copy.Selection);
		propertyControl.ObjectName = copy.ObjectName;
		propertyControl.Fixed = false;
		propertyControl.WriteNewValues = allowUnlisted;
		propertyControl.Defines = null;
		propertyControl.OverrideDesc = overrideDesc;
		if (overrideSelection != null)
		{
			propertyControl.Selection = RuntimeHelpers.GetObjectValue(overrideSelection);
			propertyControl.ObjectName = overrideObject;
		}
		propertyControl = null;
		return result;
	}
}
