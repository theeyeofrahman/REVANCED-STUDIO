using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using MPNDossier.My;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Newtonsoft.Json.Linq;

namespace MPNDossier;

[StandardModule]
public sealed class ModEnums
{
	public enum Faction
	{
		Player = 0,
		TwoShoes = 1,
		AAHW = 2,
		Nexus = 3,
		Mercs = 4,
		Bandits = 5,
		Daisies = 6,
		Zeds = 7,
		None = 8,
		Environment = 9,
		Neutral = 10,
		Enemy = 11,
		Toughs = 12,
		Vendevice = 13,
		Vampires = 14,
		N51 = 15,
		PrivSec = 16,
		Machine = 17,
		GruntArmy = 21,
		Boes = 22,
		Junklanders = 23,
		Deserters = 24,
		PointTakers = 25,
		ACRA = 26,
		Workshop = 27,
		NEWTAB = 28,
		Zeal = 29,
		ZeroVec = 30,
		Diorthos = 31,
		Any = 100
	}

	public enum Behavior
	{
		Mindless,
		Hunter,
		Coward,
		Personal,
		Turtle,
		Evasive,
		Enraged,
		ItchyFinger,
		WhiteNinja,
		GrenadeLover,
		ChargeUpLover,
		ChargeUpRanged,
		FavorDual,
		Runner,
		Accurate,
		SlowWitted,
		Tactical,
		Pack_UNUSED,
		ChargeUpASAP,
		Defensive,
		ThrowsOften,
		Focused,
		ADD,
		DynamicSwap,
		Indirect,
		Bubble,
		HotFoot,
		Camper,
		Sniper,
		NoSprint,
		Sharky,
		FreeAttacker,
		TargetShy,
		HoldCover,
		SprintOnlyInPursuit,
		Deadly,
		GetAwayFromMe,
		LastManStanding,
		ScoutAhead_UNUSED,
		TattleTale,
		ChargeAttack_Angry,
		NeverStow,
		TurtleForever,
		RearSmash,
		NoWakeupAlertAllies,
		Networked_UNUSED,
		GrappleMasher,
		ForeverAutofire,
		ButtonMasher,
		Catlike,
		LeapSlam,
		LeapGrappler,
		ChargeAttack_Bullrush,
		ChargeAttack_Grapple,
		Teleport_Flank,
		Teleport_Pursue,
		NoPathing,
		CounterAttacks,
		LeapPursue,
		NoCombat,
		Teleport_Panic,
		NeverHeavyH2H,
		NoMeleePursue,
		NoAttack,
		JamAttack,
		UsePatrolPaths,
		UseGuardPosts,
		XRay,
		MagicWandAllAutos,
		ChargeAttackMax,
		IgnoreSiege,
		SpreadsJoy
	}

	public enum Trait
	{
		Zed,
		NoPain,
		Hero,
		AutoDodgeThrown,
		NoCombo,
		BulletsAutoHit,
		NoShoot,
		NotLeader_UNUSED,
		TrickyHead,
		BotMind_UNUSED,
		SpawnDual,
		BowlingPin,
		ContactExecute,
		NoMelee,
		WalkPulse,
		TogetherSpawn,
		NoGore,
		StunRagdolls,
		InfiniteAmmo,
		NoDisarm,
		VanishDeath,
		VanishDeactivate,
		Blind,
		ZeroGravityDeath,
		EffectOnDeactivate,
		GrappleKiller,
		NoCombatLock,
		Unphased,
		DizzyRagdoll,
		StunFromH2H,
		TurretSpin,
		IgnoreTac_UNUSED,
		Mushy,
		StartWounds,
		NoDizzy,
		H2HAngry,
		NoH2HSnap,
		ImmuneAutoExecute,
		WeakHands,
		ItemsGoneOnDeath,
		StaggerOnLowHealth,
		DualWieldOverride,
		Lumbering,
		Sluggish,
		TwoHandOverride,
		Clumsy,
		Deaf,
		RemotePickup,
		GrappleAlwaysThrow,
		MegaKnockback,
		AlwaysGoreDeath,
		Heavy,
		Uncounterable,
		Oblivious,
		NoCombatRagdoll,
		ThrowForward,
		DashAttacker,
		CantBeExecuted,
		Unconvertable,
		GrappleAttack,
		Robotic,
		IgnoreAutoEmbed,
		StableRanged,
		HeftyOverride,
		Incorporeal,
		Invisible,
		IncorporealFaction,
		BurnDeath,
		NoMagazines,
		NinjaSpawn,
		NoElementalDeath,
		NoDrop,
		NoSwap,
		AlwaysBadThrow,
		Soulless,
		LeaveToPlayer,
		InvinicbleStagger,
		NoFallDamage
	}

	public enum Vocation
	{
		Nimble = 0,
		Demolition = 1,
		Hacker = 2,
		Dissonant = 3,
		Zed = 4,
		Mag = 5,
		Medical = 6,
		Service = 7,
		Swimmer = 8,
		Civillian = 9,
		Excavator = 10,
		Ghost = 11,
		Occult = 12,
		Loner = 13,
		Mechanic = 14,
		Security = 15,
		Digital = 16,
		Clockwork = 17,
		NONE = 100,
		IMPOSSIBLE = 101
	}

	public enum Skill
	{
		Unarmed1 = 10,
		Unarmed2 = 11,
		Unarmed3 = 12,
		Melee1 = 20,
		Melee2 = 21,
		Melee3 = 22,
		Ranged1 = 30,
		Ranged2 = 31,
		Ranged3 = 32,
		Acrobatic1 = 40,
		Acrobatic2 = 41,
		Acrobatic3 = 42,
		LightPunch = 220,
		CritPunch = 221,
		SpecialStrike = 222,
		UNUSED1 = 223,
		ImprovWeapon = 224,
		FocusPunch = 225,
		BetterSpecialStrike = 226,
		StunningFist = 227,
		CritPunchBonus = 228,
		PowerFinish = 229,
		ExposeCover = 230,
		Revelator = 231,
		MegaFinish = 232,
		Footwork = 240,
		UNUSED3 = 241,
		PunchAblate = 242,
		Counter = 243,
		Grapple = 244,
		UNUSED4 = 245,
		DashAttack = 246,
		DashHeavyAttack = 247,
		PunchEnerv = 248,
		MeleeCounter = 249,
		UNUSED5 = 250,
		BasicGuard = 260,
		BetterGuard = 261,
		GuardAiming = 262,
		UNUSED6 = 263,
		UNUSED7 = 264,
		AdvancedGuard = 265,
		UNUSED8 = 266,
		Reciprocate = 267,
		GuardMobility = 268,
		CounterCharging = 269,
		MeleeGuard = 270,
		AdvancedGuardMobility = 271,
		LightSwing = 420,
		UNUSED2 = 421,
		CritSwing = 422,
		UNUSED9 = 423,
		BluntKnockback = 424,
		FocusSwing = 425,
		FollowThrough = 426,
		CritSwingBonus = 427,
		UNUSED10 = 428,
		UNUSED11 = 429,
		BigWeapons = 430,
		BlockBreaker = 431,
		CarryOver = 432,
		Positioning = 440,
		EdgedTiming = 441,
		MinorLeague = 442,
		Unbalance = 443,
		UNUSED13 = 444,
		DualMelee = 445,
		EdgedSpeed = 446,
		UNUSED14 = 447,
		AdvancedDualMelee = 448,
		QuickUnsheath = 449,
		ZipAttack = 450,
		UNUSED15 = 451,
		BasicBlock = 460,
		BetterBlock = 461,
		BlockAiming = 462,
		BlockMobility = 463,
		Riposte = 464,
		Parry = 465,
		AdvancedBlock = 466,
		BlockBullets = 467,
		DeflectBullets = 468,
		ReflectBullets = 469,
		UNUSED16 = 470,
		AdvancedBlockMobility = 471,
		StationaryRecovery = 620,
		Unphased = 621,
		DirectionTracking = 622,
		CoverBracing = 623,
		AutoFireComp = 624,
		Quickdraw = 625,
		Highground = 626,
		Unwavering = 627,
		MobileAcc = 628,
		HeavyArms = 629,
		AutoFireCorrect = 630,
		PerfectSights = 631,
		Headshot = 640,
		GreatShot = 641,
		Unobstructed = 642,
		DualRanged = 643,
		PistolWhip = 644,
		ZeroTolerance = 645,
		DisarmingShot = 646,
		MagicBullet = 647,
		ShotgunBlastoff = 648,
		AdvancedDualRanged = 649,
		UNUSED17 = 650,
		OffHandAim = 660,
		SweetSpots = 661,
		AimAssist = 662,
		UNUSED18 = 663,
		UNUSED19 = 664,
		UNUSED20 = 665,
		CriticalShot = 666,
		UNUSED21 = 667,
		MobileAim = 668,
		UNUSED22 = 669,
		UNUSED23 = 670,
		PrecisionShooter = 671,
		PinDown = 672,
		Deadeye = 673,
		TacticalNovice = 820,
		MovingTarget = 821,
		TacticalJourneyman = 822,
		DodgeTackle = 823,
		TacHustle = 824,
		ShootDodge = 825,
		PickupKick = 826,
		PickupLeaping = 827,
		TacticalMaster = 828,
		HeadLeap = 829,
		DashFire = 830,
		UNUSED24 = 831,
		UNUSED25 = 840,
		PreciseThrow = 841,
		CatchThrown = 842,
		ShurikenThrow = 843,
		MadarangDisarm = 844,
		DeadlyThrow = 845,
		UNUSED26 = 846,
		UNUSED27 = 847,
		ShurikenArc = 848,
		LethalBluntThrow = 849,
		Lawnmower = 850,
		ThrowBig = 851,
		Dash = 860,
		UNUSED28 = 861,
		FastGetup = 862,
		UNUSED29 = 863,
		UNUSED30 = 864,
		Plummet_WasImprovedClimb = 865,
		TimedKipUp = 866,
		FallTumble = 867,
		TacDash = 868,
		UNUSED31 = 869,
		UNUSED32 = 870,
		FallRoll = 871,
		Tinkerer = 1000,
		Scary = 1001,
		BribeEnemy = 1002,
		ImaginaryFriend = 1003,
		Infiltrator = 1004,
		FreeExecute = 1005,
		Hungry = 1006,
		Sturdy = 1007,
		Vanisher = 1008,
		ThrowMaster = 1009,
		Corruption = 1010,
		Arcane = 1011,
		Operative = 1012
	}

	public enum SkillTree
	{
		None = -1,
		C_Unarmed,
		C_Melee,
		C_Ranged,
		C_Acrobatic,
		O_Origin
	}

	public enum ItemGenre
	{
		Improvised = 0,
		Tool = 1,
		Common = 2,
		Professional = 3,
		CuttingEdge = 4,
		AlphaWare = 5,
		Artillery = 6,
		VideoGame = 7,
		Magic = 8,
		None = 100
	}

	public enum ArmorSlots
	{
		Hat = 0,
		Face = 1,
		Mouth = 2,
		Armor = 3,
		Outfit = 4,
		Back = 5,
		Belt = 6,
		Gloves = 7,
		Boots = 8,
		FullHead = 9,
		Ear = 10,
		Shoulders = 11,
		Hair = 25,
		Augment = 50,
		None = 100
	}

	public enum ArmorTraits
	{
		DestroyOnUnequip,
		Deflects,
		UNUSED,
		NoKnockoff,
		NeverUnequip,
		TiesHands,
		MeleeRemove,
		NoGore,
		NeverSell
	}

	public enum WeaponCategories
	{
		Pistol = 0,
		Revolver = 1,
		SMG = 2,
		Shotgun = 3,
		Rifle = 4,
		Heavy = 5,
		Melee = 6,
		Unarmed = 7,
		Thrown = 8,
		Shield = 9,
		Wand = 10,
		None = 100
	}

	public enum WeaponTraits
	{
		TwoHands,
		ShowInOffHand,
		AmmoFromPool_REMOVED,
		RemoveOnEmpty,
		Shuriken,
		DisarmHit,
		MultiHitUNUSED,
		FastThrow,
		LeanBackHold,
		NoStow,
		NotAWeapon,
		CannotPickUp,
		AutoEmbed,
		ReturnThrown,
		Roped,
		TooHeftyToWield,
		ThrowOnly,
		SolidOnFloor,
		StableHold,
		RegenAmmo,
		NoUnequip,
		SpearThrow,
		NeverEmbed,
		OverheadCarry,
		NoPistolWhip,
		InstaExecute,
		DropOnRagdoll,
		DontShowAsPickup,
		DropOnAttack,
		HoldInOffHand,
		DecayOnFloor,
		NoRankUpgrade,
		NoBullets,
		GoreyKill,
		AmHuge,
		BreakOnDecay,
		NoImpactBounce,
		NoBladeLock,
		SidewaysThrow,
		ZeroGravityThrow,
		NoTeamDamage,
		MagicWand,
		NoCrit,
		BreakOnCollision,
		MindlessUsable,
		ThrowAllowed,
		ForceStowOnBack,
		Lootable,
		NoTinkerer,
		CountAllAmmoValues,
		SweepRagdolls
	}

	public enum WeaponAddons
	{
		None,
		BarrelCap,
		Scope,
		RedDot,
		Core,
		Flashlight,
		Bayonet,
		Extension,
		Stock,
		Magazine
	}

	public enum AddonLocations
	{
		Barrel,
		Butt,
		Rail,
		Mag,
		Cap,
		Extension
	}

	public enum ReloadRules
	{
		Normal = 0,
		Health = 1,
		Dizzy = 2,
		Ragdoll = 3,
		Bloodlust = 4,
		NoReload = 100
	}

	public enum AddonConfig
	{
		Normal,
		Wand
	}

	public enum BonusType
	{
		___WeaponStats___ = 100,
		RangedDamage = 101,
		MeleeDamage = 102,
		UnarmedDamage = 103,
		PistolDamage = 104,
		SlashingDamage = 105,
		BluntDamage = 106,
		H2HSpeed = 107,
		MeleeSpeed = 108,
		AllDamage = 109,
		ExplosionDamage = 110,
		UnarmedSpeed = 111,
		RateOfFireNon_Automatics = 112,
		RecoilRecovery = 113,
		RangedAccuracy = 114,
		ExtraMags = 120,
		ExtraMags_Handgun = 121,
		ExtraMags_Standard = 122,
		ExtraMags_Heavy = 123,
		ExtraMags_Thrown = 124,
		___ArmorStats___ = 200,
		REMOVED_ArmorDefense = 201,
		EncumbranceMax = 202,
		___CharacterStats___ = 300,
		ResistDizzy = 301,
		TacDefense = 302,
		TraumaRegen = 303,
		MoveSpeed = 304,
		WakeupDist = 305,
		___DamageSources___ = 400,
		DamageFromH2H = 401,
		DamageFromFire = 402,
		DamageFromShock = 403,
		DamageFromNega = 404,
		DamageFromStun = 405,
		DamageFromElements = 406,
		DamageFromPhysical = 407,
		DamageFromCold = 408,
		DamageFromAcid = 409,
		___Mission_World___ = 500,
		CashBonus = 501,
		REMOVED_InteractSpeed = 502,
		REMOVED_BountyTargets = 503,
		___Squad___ = 600,
		REMOVED_SquadAttack = 601,
		MaxHirelingLevel = 602,
		__Attributes__ = 700,
		Stat_Strength = 701,
		Stat_Dexterity = 702,
		Stat_Endurance = 703,
		Stat_Tactics = 704,
		Stat_Force = 705,
		Stat_Lethality = 706,
		Stat_Critical = 707,
		Stat_Athletics = 708,
		Stat_Strive = 709,
		Stat_Senses = 710
	}

	public enum DamageType
	{
		Ballistic = 0,
		Slashing = 1,
		Blunt = 2,
		Explosive = 3,
		Burn = 4,
		Piercing = 5,
		Biting = 6,
		Shock = 7,
		Stun = 8,
		Negative = 9,
		DeRes = 10,
		Cold = 11,
		Caustic = 12,
		Force = 13,
		Prime = 14,
		None = 100
	}

	public enum BuffEffects
	{
		None = 0,
		Bloodlust = 50,
		Riposting = 51,
		Kinetic = 52,
		Bleeding = 53,
		Dizzied = 100,
		Distracted = 101,
		Slowed = 102,
		Drugged = 103,
		InfiniteAmmo = 125,
		DoubleDamage = 126,
		SpeedBoost = 127,
		Invulnerable = 128,
		ChargeAttack = 150,
		Irradiate = 175,
		Cold = 176,
		Acid = 177,
		Doom = 178,
		Bribed = 200
	}

	public enum DeathFlags
	{
		None = 0,
		MajorDamage = 1,
		Headshot = 2,
		HeadDestroyed = 3,
		Executed = 4,
		BodyDestroyed = 5,
		Obliterated = 6,
		Bodyshot = 7,
		Electrocuted = 8,
		Burned = 9,
		Nega = 10,
		Frozen = 11,
		GodKilled = 100,
		Permaquiet = 101
	}

	public enum Market
	{
		QuarterMaster = 0,
		BlackMarket = 1,
		BarberShop = 2,
		Arcane = 3,
		NONE = 100
	}

	public enum ModelParts
	{
		Head,
		Body,
		RHand,
		LHand,
		RFoot,
		LFoot
	}

	public enum ExecuteConditions
	{
		Front = 0,
		Back = 1,
		Melee = 10,
		Unarmed = 11,
		Ranged = 12,
		InExecute = 40,
		Ragdolled = 41,
		OnHoldHeavy = 60,
		Sprint = 61,
		PlayerBotsOnly = 62,
		PlayerHold_0 = 63,
		PlayerHold_1 = 64,
		LastCorpusOnly = 65,
		NonEssentials = 66,
		FirstCorpus = 67,
		PlayerOnly = 68
	}

	public enum ExecuteSource
	{
		Dizzy,
		Backstab,
		Auto
	}

	public enum ExecuteRules
	{
		None,
		Bribe,
		ImprovWeapon
	}

	public enum DotResolutions
	{
		DodgeLanding,
		Ragdoll,
		Water
	}

	public enum DoTEffects
	{
		CrazyRagdoll
	}

	public enum SelectionColors
	{
		Selection = 0,
		Highlight = 1,
		Invalid = 2,
		Important = 3,
		NONE = 100
	}

	public static Dictionary<string, Image> IMAGES = new Dictionary<string, Image>();

	public static Dictionary<string, JObject> DB_CHAR = new Dictionary<string, JObject>(StringComparer.OrdinalIgnoreCase);

	public static Dictionary<string, JObject> DB_SKILLS = new Dictionary<string, JObject>(StringComparer.OrdinalIgnoreCase);

	public static Dictionary<string, JObject> DB_SKILLGROUPS = new Dictionary<string, JObject>(StringComparer.OrdinalIgnoreCase);

	public static Dictionary<string, JObject> DB_SKILLTREES = new Dictionary<string, JObject>(StringComparer.OrdinalIgnoreCase);

	public static Dictionary<string, JObject> DB_GEAR = new Dictionary<string, JObject>(StringComparer.OrdinalIgnoreCase);

	public static Dictionary<string, JObject> DB_CHATTER = new Dictionary<string, JObject>(StringComparer.OrdinalIgnoreCase);

	public static Dictionary<string, JObject> DB_CORPUS = new Dictionary<string, JObject>(StringComparer.OrdinalIgnoreCase);

	public static Dictionary<string, JObject> DB_ANIMSETS = new Dictionary<string, JObject>(StringComparer.OrdinalIgnoreCase);

	public static Dictionary<string, JObject> DB_VOICESETS = new Dictionary<string, JObject>(StringComparer.OrdinalIgnoreCase);

	public static Dictionary<string, JObject> DB_WARDROBE = new Dictionary<string, JObject>(StringComparer.OrdinalIgnoreCase);

	public static Dictionary<string, JObject> DB_LOADOUTS = new Dictionary<string, JObject>(StringComparer.OrdinalIgnoreCase);

	public static Dictionary<string, JObject> DB_STORYLOADOUTS = new Dictionary<string, JObject>(StringComparer.OrdinalIgnoreCase);

	public static Dictionary<string, JObject> DB_SKINS = new Dictionary<string, JObject>(StringComparer.OrdinalIgnoreCase);

	public static Dictionary<string, JObject> DB_SWATCHES = new Dictionary<string, JObject>(StringComparer.OrdinalIgnoreCase);

	public static Dictionary<string, JObject> DB_ANIMATIONS = new Dictionary<string, JObject>(StringComparer.OrdinalIgnoreCase);

	public static Dictionary<string, JObject> DB_EXECUTION = new Dictionary<string, JObject>(StringComparer.OrdinalIgnoreCase);

	public static Dictionary<string, JObject> DB_EXPLOSION = new Dictionary<string, JObject>(StringComparer.OrdinalIgnoreCase);

	public static Dictionary<string, JObject> DB_ORIGINS = new Dictionary<string, JObject>(StringComparer.OrdinalIgnoreCase);

	public static Dictionary<string, JObject> DB_STORIES = new Dictionary<string, JObject>(StringComparer.OrdinalIgnoreCase);

	public static Dictionary<string, JObject> DB_ARENAS = new Dictionary<string, JObject>(StringComparer.OrdinalIgnoreCase);

	public static Dictionary<string, JObject> DB_COLLECTIBLES = new Dictionary<string, JObject>(StringComparer.OrdinalIgnoreCase);

	public static Dictionary<string, JObject> DB_WEAPONS = new Dictionary<string, JObject>(StringComparer.OrdinalIgnoreCase);

	public static Dictionary<string, JObject> DB_BASEWEAPONS = new Dictionary<string, JObject>(StringComparer.OrdinalIgnoreCase);

	public static Dictionary<string, JObject> DB_WEAPONADDONS = new Dictionary<string, JObject>(StringComparer.OrdinalIgnoreCase);

	public static Dictionary<string, JObject> DB_ADDONGROUPS = new Dictionary<string, JObject>(StringComparer.OrdinalIgnoreCase);

	public static Dictionary<string, JObject> DB_MODDEDWEAPONS = new Dictionary<string, JObject>(StringComparer.OrdinalIgnoreCase);

	public static Dictionary<string, JObject> DB_RANGED = new Dictionary<string, JObject>(StringComparer.OrdinalIgnoreCase);

	public static Dictionary<string, JObject> DB_THROWN = new Dictionary<string, JObject>(StringComparer.OrdinalIgnoreCase);

	public static Dictionary<string, JObject> DB_AMMO = new Dictionary<string, JObject>(StringComparer.OrdinalIgnoreCase);

	public static Dictionary<string, JObject> DB_DOT = new Dictionary<string, JObject>(StringComparer.OrdinalIgnoreCase);

	public static Dictionary<string, JObject> DB_SOUNDPACKS = new Dictionary<string, JObject>(StringComparer.OrdinalIgnoreCase);

	public static Dictionary<string, JObject> DB_TEXTURES = new Dictionary<string, JObject>(StringComparer.OrdinalIgnoreCase);

	public static List<string> ALTERNATEPARTS = new List<string>();

	public static List<string> MATERIALS = new List<string>();

	public static List<string> ANIMATIONCLIPS = new List<string>();

	public static List<string> SPRITES = new List<string>();

	public static List<string> MUSIC = new List<string>();

	public static List<string> MODULES = new List<string>();

	public static List<string> ONESHOTS = new List<string>();

	public static List<string> CUSTOMTEX = new List<string>();

	public static Dictionary<Behavior, string> BEHAVIORS = new Dictionary<Behavior, string>();

	public static Dictionary<Trait, string> TRAITS = new Dictionary<Trait, string>();

	public static Dictionary<Skill, JObject> SKILLS = new Dictionary<Skill, JObject>();

	public static Dictionary<Vocation, string> VOCATIONS = new Dictionary<Vocation, string>();

	public static Dictionary<WeaponTraits, string> WEAPTRAITS = new Dictionary<WeaponTraits, string>();

	public static Dictionary<ArmorTraits, string> ARMTRAITS = new Dictionary<ArmorTraits, string>();

	public static Dictionary<string, string> ATTRIBUTEDESC = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

	public static string DIR_TRANSFER = "";

	public static void Behaviors_Load()
	{
		BEHAVIORS.Add(Behavior.Mindless, "Ignore fearful or tactical decisions.");
		BEHAVIORS.Add(Behavior.Hunter, "Track potential targets when no active target.");
		BEHAVIORS.Add(Behavior.Coward, "Stay behind allies, rarely approach target, run away if approached.");
		BEHAVIORS.Add(Behavior.Personal, "Prefers being close to attack, even with a ranged weapon.");
		BEHAVIORS.Add(Behavior.Turtle, "Seek cover much more often, especially when fired upon.");
		BEHAVIORS.Add(Behavior.Evasive, "Move away from melee range with a ranged weapon, perform dodges away from enemy attacks.");
		BEHAVIORS.Add(Behavior.Enraged, "Increase attack frequency with damage sustained.");
		BEHAVIORS.Add(Behavior.ItchyFinger, "Shoot much more aggressively, continue to swing at targets who've moved out of range.");
		BEHAVIORS.Add(Behavior.WhiteNinja, "Charge into melee range regardless of who's attacking first.");
		BEHAVIORS.Add(Behavior.GrenadeLover, "Frequently throw weapons in grenade slot if any.");
		BEHAVIORS.Add(Behavior.ChargeUpLover, "Frequently use charge up attacks.");
		BEHAVIORS.Add(Behavior.ChargeUpRanged, "Charge up from a distance, let the charge attack close in.");
		BEHAVIORS.Add(Behavior.FavorDual, "Favor a weapon in each hand.");
		BEHAVIORS.Add(Behavior.Runner, "Sprint everywhere.");
		BEHAVIORS.Add(Behavior.Accurate, "Frequently Brace with off hand when shooting.");
		BEHAVIORS.Add(Behavior.SlowWitted, "Delay before performing melee attack.");
		BEHAVIORS.Add(Behavior.Tactical, "Accompany suqad leader, coordinate movememnt with other tactical units.");
		BEHAVIORS.Add(Behavior.Pack_UNUSED, "Prioritize targets of nearby allies.");
		BEHAVIORS.Add(Behavior.ChargeUpASAP, "Perform a charge attack immediately.");
		BEHAVIORS.Add(Behavior.Defensive, "Frequently block enemy attacks.");
		BEHAVIORS.Add(Behavior.ThrowsOften, "Throw Weapon in hand if not highly ranked to engage in melee; only throw if there's no ammo.");
		BEHAVIORS.Add(Behavior.Focused, "Stick with current target even if engaged by another.");
		BEHAVIORS.Add(Behavior.ADD, "Constantly change targets.");
		BEHAVIORS.Add(Behavior.DynamicSwap, "Dynamically swap between ranged and melee weapon (weapon storage must be between the two).");
		BEHAVIORS.Add(Behavior.Indirect, "Stagger towards target when approaching.");
		BEHAVIORS.Add(Behavior.Bubble, "Space out from allies when stationary or using ranged weapon.");
		BEHAVIORS.Add(Behavior.HotFoot, "Never hold still in combat, keep moving around.");
		BEHAVIORS.Add(Behavior.Camper, "Find a good shooting position and stay there.");
		BEHAVIORS.Add(Behavior.Sniper, "Ocassionally Perform special attack that ignores tac bar with ranged weapon.");
		BEHAVIORS.Add(Behavior.NoSprint, "Never sprint.");
		BEHAVIORS.Add(Behavior.Sharky, "Orbit around opponent before closing in to attack.");
		BEHAVIORS.Add(Behavior.FreeAttacker, "Will attack freely if in range of target regardless of attacking allies.");
		BEHAVIORS.Add(Behavior.TargetShy, "Navigate away from enemy sight lines.");
		BEHAVIORS.Add(Behavior.HoldCover, "Hold cover position when near desired location, vacate if new location is farther away.");
		BEHAVIORS.Add(Behavior.SprintOnlyInPursuit, "Only sprint when chasing after enemy.");
		BEHAVIORS.Add(Behavior.Deadly, "Perform executions when applicable.");
		BEHAVIORS.Add(Behavior.GetAwayFromMe, "Frequently perform shoves, kicks, and rifle bashes on target if skills are available.");
		BEHAVIORS.Add(Behavior.LastManStanding, "Hang Back from main force if squad leader.");
		BEHAVIORS.Add(Behavior.ScoutAhead_UNUSED, "Move ahead of squad formation, don't return to leader if called.");
		BEHAVIORS.Add(Behavior.TattleTale, "Regroup on nearby squadmate or leader on contact with the enemy, notifies the rest of the squad.");
		BEHAVIORS.Add(Behavior.ChargeAttack_Angry, "Get fired up and charge at enemy for a sprint attack.");
		BEHAVIORS.Add(Behavior.ChargeAttack_Bullrush, "Charge around and headbutt people until we either tire out or hit a wall.");
		BEHAVIORS.Add(Behavior.ChargeAttack_Grapple, "Charge at a target to grapple them..");
		BEHAVIORS.Add(Behavior.NeverStow, "Do not holster weapons even if out of combat.");
		BEHAVIORS.Add(Behavior.TurtleForever, "Never move from our cover spot. Used for scripted sequences only.");
		BEHAVIORS.Add(Behavior.RearSmash, "Perform a special rear melee attack for targets directly behind it.");
		BEHAVIORS.Add(Behavior.NoWakeupAlertAllies, "Do not announce to squad that enemy has been spotted.");
		BEHAVIORS.Add(Behavior.Networked_UNUSED, "Alert entire squad to enemy regardless of position or line of sight.");
		BEHAVIORS.Add(Behavior.GrappleMasher, "Easily break out of grapples.");
		BEHAVIORS.Add(Behavior.ForeverAutofire, "Fire weapon as long as we have a target, regardless of where we're aiming.");
		BEHAVIORS.Add(Behavior.ButtonMasher, "Perform melee attacks much more frantically.");
		BEHAVIORS.Add(Behavior.Catlike, "Is able to kip up before hitting the ground after ragdolling.");
		BEHAVIORS.Add(Behavior.LeapSlam, "Is able to perform a slam attack by leaping at a target.");
		BEHAVIORS.Add(Behavior.LeapGrappler, "Is able to pounce on targets and repeatedly attack them.");
		BEHAVIORS.Add(Behavior.LeapPursue, "Leap towards target when chasing.");
		BEHAVIORS.Add(Behavior.CounterAttacks, "Can break out of melee stunlocks.");
		BEHAVIORS.Add(Behavior.NeverHeavyH2H, "Never perform heavy unarmed attacks.");
		BEHAVIORS.Add(Behavior.NoMeleePursue, "Do not chase after targets within melee range.");
		BEHAVIORS.Add(Behavior.NoPathing, "Never move");
		BEHAVIORS.Add(Behavior.NoCombat, "Never apply combat modules");
		BEHAVIORS.Add(Behavior.NoAttack, "Do not attack enemies; does not apply to special strikes and Sniper module");
		BEHAVIORS.Add(Behavior.Teleport_Flank, "Attempt to teleport behind the target to perform an attack.");
		BEHAVIORS.Add(Behavior.Teleport_Panic, "Teleport erratically to avoid being targeted.");
		BEHAVIORS.Add(Behavior.Teleport_Pursue, "Teleport to target if too far away.");
		BEHAVIORS.Add(Behavior.JamAttack, "Always decide to attack, disregard normal cooldown rules");
		BEHAVIORS.Add(Behavior.UsePatrolPaths, "If Patrol paths on map exist, use them");
		BEHAVIORS.Add(Behavior.UseGuardPosts, "Find Guard Post and hang there");
		BEHAVIORS.Add(Behavior.XRay, "LOS is always considered valid on target");
		BEHAVIORS.Add(Behavior.MagicWandAllAutos, "Oscillate aim with any weapon that has the Magic Wand trait (fires in a wave)");
		BEHAVIORS.Add(Behavior.ChargeAttackMax, "Always charge up to maximum on charge attacks");
		BEHAVIORS.Add(Behavior.IgnoreSiege, "Don't wait at staging areas, charge ahead immediately.");
		BEHAVIORS.Add(Behavior.SpreadsJoy, "After killing/ragdolling an opponent, celebrate with a squadmade of equal size if one's nearby");
	}

	public static void Traits_Load()
	{
		WEAPTRAITS.Add(WeaponTraits.TwoHands, "This weapon always requires two hands to use.");
		WEAPTRAITS.Add(WeaponTraits.ShowInOffHand, "This weapon will appear as being held in the off hand when equipped in the main hand (typically excludes dual wielding).");
		WEAPTRAITS.Add(WeaponTraits.RemoveOnEmpty, "Weapon is destroyed when ammo is depleted (e.g throwables)");
		WEAPTRAITS.Add(WeaponTraits.Shuriken, "This weapon benefits from the shuriken arc skill.");
		WEAPTRAITS.Add(WeaponTraits.DisarmHit, "Shots from this weapon can disarm their opponent.");
		WEAPTRAITS.Add(WeaponTraits.FastThrow, "Weapon is thrown incredibly quickly.");
		WEAPTRAITS.Add(WeaponTraits.LeanBackHold, "User will lean back when holding this weapon.");
		WEAPTRAITS.Add(WeaponTraits.NoStow, "When holstering attempted, will simply fall to floor.");
		WEAPTRAITS.Add(WeaponTraits.NotAWeapon, "Character holding this will still use unarmed attacks, and will not benefit from it in any way.");
		WEAPTRAITS.Add(WeaponTraits.CannotPickUp, "Item becomes debris when dropped, and can never be picked up ever again.");
		WEAPTRAITS.Add(WeaponTraits.AutoEmbed, "Will wedge into anyone it was thrown against.");
		WEAPTRAITS.Add(WeaponTraits.ReturnThrown, "Will return to the user after being thrown (requires ShurikenArc).");
		WEAPTRAITS.Add(WeaponTraits.Roped, "Ties a string to the weapon when thrown (requires ShurikenArc).");
		WEAPTRAITS.Add(WeaponTraits.TooHeftyToWield, "User must be of size scale 2.5+ to use this weapon.");
		WEAPTRAITS.Add(WeaponTraits.ThrowOnly, "Attacking with this weapon will throw it.");
		WEAPTRAITS.Add(WeaponTraits.SolidOnFloor, "Item is considered a solid object, and can be collided with when not being held.");
		WEAPTRAITS.Add(WeaponTraits.StableHold, "Weapon will follow the character's pivoting, used for heavy weapons.");
		WEAPTRAITS.Add(WeaponTraits.RegenAmmo, "Ammo slowly regenerates to maximum overtime.");
		WEAPTRAITS.Add(WeaponTraits.NoUnequip, "This item can never be dropped, only removed using the inventory menu.");
		WEAPTRAITS.Add(WeaponTraits.SpearThrow, "Item will be precisely thrown with forward facing.");
		WEAPTRAITS.Add(WeaponTraits.NeverEmbed, "Bounce off walls.");
		WEAPTRAITS.Add(WeaponTraits.OverheadCarry, "Weapon will be held over user's head, pairs well with weapons that can only be thrown.");
		WEAPTRAITS.Add(WeaponTraits.NoPistolWhip, "Deny Pistol Whip attacks.");
		WEAPTRAITS.Add(WeaponTraits.InstaExecute, "Does not require the enemy to be dizzy to allow execution (requires ImprovedWeapon skill).");
		WEAPTRAITS.Add(WeaponTraits.DropOnRagdoll, "Drop this weapon if user is ragdolled.");
		WEAPTRAITS.Add(WeaponTraits.DontShowAsPickup, "Omits weapons from showing up with a prompt to pick them up; they can still be picked up by events however.");
		WEAPTRAITS.Add(WeaponTraits.HoldInOffHand, "Will actually hold this item in the offhand, main hand checks will fail because of this.");
		WEAPTRAITS.Add(WeaponTraits.DecayOnFloor, "Fades away when dropped on the floor, typical for weapons out of ammo or broken hilts.");
		WEAPTRAITS.Add(WeaponTraits.BreakOnDecay, "Weapon breaks instead of decaying.");
		WEAPTRAITS.Add(WeaponTraits.NoBullets, "Weapon does not emit bullets, instead emits a single particle emitter (flamethrowers, sludge throwers, laser beams).");
		WEAPTRAITS.Add(WeaponTraits.GoreyKill, "Weapon will almost always gore slain targets.");
		WEAPTRAITS.Add(WeaponTraits.AmHuge, "Weapon will always use encumbered swing animations.");
		WEAPTRAITS.Add(WeaponTraits.NoImpactBounce, "Don't bounce off a surface that has been swung at.");
		WEAPTRAITS.Add(WeaponTraits.NoBladeLock, "Don't initiate a blade lock with this weapon.");
		WEAPTRAITS.Add(WeaponTraits.SidewaysThrow, "Weapon will be thrown sideways regardless of lawnmower skill.");
		WEAPTRAITS.Add(WeaponTraits.ZeroGravityThrow, "Weapon does not drop when thrown (used for boomerange type weapons)");
		WEAPTRAITS.Add(WeaponTraits.NoTeamDamage, "Enemies of the same faction cannot be hurt by this weapon (namely explosions).");
		WEAPTRAITS.Add(WeaponTraits.MagicWand, "NPCs (not bots) will weave this gun back and forth when firing.");
		WEAPTRAITS.Add(WeaponTraits.NoCrit, "Cannot land critical hits in sweetspot or with timing.");
		WEAPTRAITS.Add(WeaponTraits.BreakOnCollision, "Weapon Breaks at any time it hits a solid surface.");
		WEAPTRAITS.Add(WeaponTraits.MindlessUsable, "NPCs with the Mindless behavior can use this weapon (throwables).");
		WEAPTRAITS.Add(WeaponTraits.ThrowAllowed, "Weapon can be thrown with NotAweapon trait attached.");
		WEAPTRAITS.Add(WeaponTraits.ForceStowOnBack, "Never store this item on back.");
		WEAPTRAITS.Add(WeaponTraits.Lootable, "Can be picked up to restore throwable ammo.");
		WEAPTRAITS.Add(WeaponTraits.CountAllAmmoValues, "All Addons contribute ammo modifiers to this weapon, not just the magazine");
		WEAPTRAITS.Add(WeaponTraits.NoTinkerer, "Tinkerer cannot use this.");
		WEAPTRAITS.Add(WeaponTraits.SweepRagdolls, "All heavy attacks ragdoll.");
		ARMTRAITS.Add(ArmorTraits.Deflects, "Bullets deflect off of this armor piece when shot at (does not account for actual armor value).");
		ARMTRAITS.Add(ArmorTraits.DestroyOnUnequip, "Armor destroys itself when removed from inventory (automatically applied).");
		ARMTRAITS.Add(ArmorTraits.MeleeRemove, "Removes the abilities to use melee weapons while wearing.");
		ARMTRAITS.Add(ArmorTraits.NeverSell, "Cannot sell these items to shops.");
		ARMTRAITS.Add(ArmorTraits.NeverUnequip, "Cannot ever remove this item after putting it on.");
		ARMTRAITS.Add(ArmorTraits.NoGore, "Attached body part cannot be gored while wearing.");
		ARMTRAITS.Add(ArmorTraits.NoKnockoff, "Cannot be dislodged from character on death (excludes gibbing)");
		ARMTRAITS.Add(ArmorTraits.TiesHands, "Forces character to use HandsTied_Unarmed attacks in animation set");
		ARMTRAITS.Add(ArmorTraits.UNUSED, "UNUSED");
		TRAITS.Add(Trait.Zed, "");
		TRAITS.Add(Trait.NoPain, "Cannot be stunned by damage.");
		TRAITS.Add(Trait.Hero, "Temporary invincibility on corpus break.");
		TRAITS.Add(Trait.AutoDodgeThrown, "Always dodge from thrown weapons.");
		TRAITS.Add(Trait.NoCombo, "Randomly chosen attacks over combo sequence.");
		TRAITS.Add(Trait.BulletsAutoHit, "Bullets always hit regardless of tac bar");
		TRAITS.Add(Trait.NoShoot, "Perform unarmed attacks with ranged weapon, never use it to shoot.");
		TRAITS.Add(Trait.NotLeader_UNUSED, "Will never be chosen as squad leader for squad; If so, will not coordinate allies.");
		TRAITS.Add(Trait.TrickyHead, "Constantly twitch and spasm head.");
		TRAITS.Add(Trait.BotMind_UNUSED, "Use special bot behaviors instead of standard behaviors (what does this mean?)");
		TRAITS.Add(Trait.SpawnDual, "Always spawn with two of the same weapon.");
		TRAITS.Add(Trait.BowlingPin, "Dodging into me causes me to be knocked down.");
		TRAITS.Add(Trait.ContactExecute, "Perform an execution when directly in front of the enemy.");
		TRAITS.Add(Trait.NoMelee, "Perform Unarmed Attacks with a melee weapon, never actually use it.");
		TRAITS.Add(Trait.WalkPulse, "Sluggishly speed up and slow down when moving.");
		TRAITS.Add(Trait.TogetherSpawn, "Squad always spawns from the same door.");
		TRAITS.Add(Trait.NoGore, "No Gore effects when damaged.");
		TRAITS.Add(Trait.StunRagdolls, "Any hit causes me to be ragdolled.");
		TRAITS.Add(Trait.InfiniteAmmo, "Never run out of magazines.");
		TRAITS.Add(Trait.NoDisarm, "Cannot be disarmed.");
		TRAITS.Add(Trait.VanishDeath, "Body disappears on death.");
		TRAITS.Add(Trait.VanishDeactivate, "Body disappeares on effect performed after death.");
		TRAITS.Add(Trait.Blind, "Cannot see target, relies on sound and damage.");
		TRAITS.Add(Trait.ZeroGravityDeath, "Float in the air on death.");
		TRAITS.Add(Trait.EffectOnDeactivate, "Perform an effect after death.");
		TRAITS.Add(Trait.GrappleAttack, "Grapple locks unit in place until unit breaks free or dies.");
		TRAITS.Add(Trait.GrappleKiller, "Can keep attacking as long as target is grappled.");
		TRAITS.Add(Trait.NoCombatLock, "Does not lock down the room when present.");
		TRAITS.Add(Trait.Unphased, "Cannot be intimidated by allies being executed, reacts instantaneously to targets.");
		TRAITS.Add(Trait.DizzyRagdoll, "Fall over when dizzied.");
		TRAITS.Add(Trait.StunFromH2H, "OBSOLETE");
		TRAITS.Add(Trait.TurretSpin, "turning speed based on dexterity.");
		TRAITS.Add(Trait.IgnoreTac_UNUSED, "OBSOLETE");
		TRAITS.Add(Trait.Mushy, "Can take rending bodily damage while still alive.");
		TRAITS.Add(Trait.StartWounds, "Start with random wounds.");
		TRAITS.Add(Trait.NoDizzy, "Cannot be dizzied.");
		TRAITS.Add(Trait.H2HAngry, "When hit in rapid succession, throw a tantrum and swing at enemy. Attack leaves me open for an execution in the back.");
		TRAITS.Add(Trait.NoH2HSnap, "Do not turn to face target while performing unarmed/melee attack.");
		TRAITS.Add(Trait.CantBeExecuted, "Cannot be executed.");
		TRAITS.Add(Trait.WeakHands, "Unarmed attacks don't stun enemy.");
		TRAITS.Add(Trait.ItemsGoneOnDeath, "Do not drop items on death.");
		TRAITS.Add(Trait.StaggerOnLowHealth, "When trauma meter is full, go into a looping stun that allows me to be executed.");
		TRAITS.Add(Trait.DualWieldOverride, "Does not need the dual welding skill to dual wield (Why even have this?)");
		TRAITS.Add(Trait.Lumbering, "People I bump into when sprinting, check against knockback (against same size if angry charger, against anyone smaller in size otherwise).");
		TRAITS.Add(Trait.Sluggish, "Perform all actions slower.");
		TRAITS.Add(Trait.TwoHandOverride, "Disregard weapon size, can hold anything in one hand.");
		TRAITS.Add(Trait.Clumsy, "Always ragdoll when falling from any height.");
		TRAITS.Add(Trait.Deaf, "Cannot hear sounds, reacts by sight and damage only.");
		TRAITS.Add(Trait.RemotePickup, "Can pickup things from a distance.");
		TRAITS.Add(Trait.GrappleAlwaysThrow, "When grappling with a target, always choose to throw them off instead of executing them.");
		TRAITS.Add(Trait.MegaKnockback, "Amplify knockback on enemies.");
		TRAITS.Add(Trait.AlwaysGoreDeath, "Explode into chunks on death.");
		TRAITS.Add(Trait.Heavy, "Don't get knocked back as far.");
		TRAITS.Add(Trait.NoCombatRagdoll, "Never Ragdoll from attacks.");
		TRAITS.Add(Trait.Uncounterable, "Cannot have its attacks countered.");
		TRAITS.Add(Trait.Unconvertable, "Cannot be converted into a zed.");
		TRAITS.Add(Trait.Oblivious, "Don't wake up unless attacked.");
		TRAITS.Add(Trait.Robotic, "Can only turn or move, not both at the same time.");
		TRAITS.Add(Trait.ThrowForward, "Only throw things straight forward.");
		TRAITS.Add(Trait.DashAttacker, "Always attacks at the end of a dash regardless of target.");
		TRAITS.Add(Trait.IgnoreAutoEmbed, "Auto-embed objects like Sanford's hook don't work");
		TRAITS.Add(Trait.StableRanged, "Ignore recoil entirely.");
		TRAITS.Add(Trait.HeftyOverride, "Ignore weight entirely.");
		TRAITS.Add(Trait.Incorporeal, "No collision of any kind.");
		TRAITS.Add(Trait.Invisible, "Cannot be detected.");
		TRAITS.Add(Trait.IncorporealFaction, "Don't collide with allies.");
		TRAITS.Add(Trait.BurnDeath, "Don't shoot! Let'em burn!");
		TRAITS.Add(Trait.NoMagazines, "Start with no additional magazines for weapon.");
		TRAITS.Add(Trait.NinjaSpawn, "When spawning in, show up directly behind the player.");
		TRAITS.Add(Trait.NoElementalDeath, "Do not apply elemental effects from weapons upon death");
		TRAITS.Add(Trait.NoDrop, "Never drop weapons, even if out of ammo");
		TRAITS.Add(Trait.NoSwap, "Never swap to other weapon, even if out of ammo");
		TRAITS.Add(Trait.AlwaysBadThrow, "Use Encumbered throw regardless of strength");
		TRAITS.Add(Trait.Soulless, "Cannot serve the offering as a spectre");
	}

	public static void Vocations_Load()
	{
		VOCATIONS.Add(Vocation.Nimble, "Can climb through vents");
		VOCATIONS.Add(Vocation.Demolition, "Can arm bombs");
		VOCATIONS.Add(Vocation.Hacker, "Can operate computers");
		VOCATIONS.Add(Vocation.Dissonant, "Can handle dissonance-related objects");
		VOCATIONS.Add(Vocation.Zed, "Can appear from unexpected entries (ground, ledges, water)");
		VOCATIONS.Add(Vocation.Mag, "Too big for things, can lift heavy objects from the environment");
		VOCATIONS.Add(Vocation.Medical, "Equipped with special dartgun that shoots nauseating syringes");
		VOCATIONS.Add(Vocation.Service, "Can hold dinner plates");
		VOCATIONS.Add(Vocation.Swimmer, "Can swim out of water");
		VOCATIONS.Add(Vocation.Civillian, "Can access getaway doors");
		VOCATIONS.Add(Vocation.Excavator, "Can smash down barricades");
		VOCATIONS.Add(Vocation.Ghost, "Can phase through walls");
		VOCATIONS.Add(Vocation.Occult, "Can summon otherworldly abominations");
		VOCATIONS.Add(Vocation.Loner, "Does not interact with staging areas");
		VOCATIONS.Add(Vocation.Mechanic, "Can fix machinery");
		VOCATIONS.Add(Vocation.Security, "Can dismantle vault picking tools");
	}

	public static void Skills_Load()
	{
		foreach (KeyValuePair<string, JObject> dB_SKILL in DB_SKILLS)
		{
			if (Enum.TryParse<Skill>(Conversions.ToString(dB_SKILL.Value["Skill"].ToObject<int>()), out var result))
			{
				SKILLS.Add(result, dB_SKILL.Value);
			}
		}
	}

	public static void Attributes_Load()
	{
		ATTRIBUTEDESC.Add("Strength", "Raw natural exertion of power.\r\n(increased with unarmed and melee skills)");
		ATTRIBUTEDESC.Add("Endurance", "Tolerance threshold of physical burden.\r\n(increased with unarmed and acrobatic skills)");
		ATTRIBUTEDESC.Add("Dexterity", "Overall speed and swiftness.\r\n(increased with ranged and acrobatic skills)");
		ATTRIBUTEDESC.Add("Tactics", "Application of strategy.\r\n(increased with melee and ranged skills)");
		ATTRIBUTEDESC.Add("Force", "Strength + Endurance");
		ATTRIBUTEDESC.Add("Critical", "Strength + Tactics");
		ATTRIBUTEDESC.Add("Strive", "Endurance + Tactics");
		ATTRIBUTEDESC.Add("Lethality", "Strength + Dexterity");
		ATTRIBUTEDESC.Add("Athletics", "Endurance + Dexterity");
		ATTRIBUTEDESC.Add("Senses", "Dexterity + Tactics");
		ATTRIBUTEDESC.Add("ShovePower", "The distance of which combatants are pushed back with Special Strikes or Dash Attackss.\r\n(must have the appropriate skill)");
		ATTRIBUTEDESC.Add("WeaponSize", "The maximum size of a weapon that can be carried with one hand.\r\n(See size property of weapons)");
		ATTRIBUTEDESC.Add("Intimidation", "The chance to spook combatants after performing a takedown.");
		ATTRIBUTEDESC.Add("ResistDizzy", "The chance to deny a dizzy effect if one is applied.\r\n(Ignore if has NoDizzy trait)");
		ATTRIBUTEDESC.Add("ResistShove", "Chance to mitigate distance and deny ragdolling after being shoved against.\r\n(Ignore if has NoPain trait)");
		ATTRIBUTEDESC.Add("EncumbranceMax", "The Maximum amount of weight that can be carried without impairing movement.\r\n(Ignore if CountWornArmor is false");
		ATTRIBUTEDESC.Add("MoveSpeed", "The base movement speed of this combatant.");
		ATTRIBUTEDESC.Add("TacBarSize", "The amount of health of this combatant's tac bar.\r\n(Must have the TacticalNovice Skill");
		ATTRIBUTEDESC.Add("KnockdownRecovery", "The amount of time spent on the ground before recovery.");
		ATTRIBUTEDESC.Add("WeaponSmallBonus", "The speed modifier for melee weapons, is multiplied to speed of melee weapon.");
		ATTRIBUTEDESC.Add("TacDamage", "damage multiplier applied to tac bars based on weapon damage.\r\n(Ignore if has BulletsAutoHit trait)");
		ATTRIBUTEDESC.Add("TacRegen", "The amount of Tacbar health restored upon defeating an opponent.\r\n(Only applies if Tacbar available)");
		ATTRIBUTEDESC.Add("ReduceRecoil", "Amount of recoil mitigated per shot when firing weapons, subtract's from weapon's recoil.\r\n(Ignore if has StableRanged trait)");
		ATTRIBUTEDESC.Add("UnarmedDamage", "Amount of unarmed damage applied additively to fists.\r\n(0.5 for regular hands)");
		ATTRIBUTEDESC.Add("BluntStun", "Chance to dizzy enemies per hit with blunt melee weapons\r\n(Heavy attacks only)");
		ATTRIBUTEDESC.Add("WeaponBigPenalty", "Melee speed bonus when holding big weapons that are automatically too heavy to use.");
		ATTRIBUTEDESC.Add("UnarmedStun", "Chance to dizzy enemies per hit with fists.\r\n(Heavy Attacks or Stunning fist/Mega Finisher Skills only)");
		ATTRIBUTEDESC.Add("H2HCritDamage", "Crticial damage multiplier applied to critical unarmed/melee attacks.\r\n(Must have Critical punch or Critical Swing skill)");
		ATTRIBUTEDESC.Add("TraumaRegen", "Exponent applied to trauma recovery per second.");
		ATTRIBUTEDESC.Add("KnockdownChance", "Chance to knock down enemies with shoves or heavy unarmed/blunt attacks.");
		ATTRIBUTEDESC.Add("RecoilRecovery", "Rate of stabilization from recoil after firing a weaponn\r\n(Ignore if has StableRanged trait)");
		ATTRIBUTEDESC.Add("MeleeDamage", "Amount of melee damage applied additively to current melee weapon.\r\n(Ignore if has NoMelee trait)");
		ATTRIBUTEDESC.Add("ThrowDistance", "The effective distance for thrown weapons before damage falloff occurs.");
		ATTRIBUTEDESC.Add("SprintSpeed", "Speed multiplier applied to Move Speed while running.\r\n(Must have the Athletic Sprint skill)");
		ATTRIBUTEDESC.Add("BlockPower", "Damage reduction multiplier applied to weapon's defense percentage.\r\n(Must have Basic Guard skill)");
		ATTRIBUTEDESC.Add("ResistKnockdown", "Chance to resist being knocked down.\r\n(Ignore if has IgnoreCombatRagdoll trait)");
		ATTRIBUTEDESC.Add("H2HSpeed", "Speed multiplier for rate of unarmed and melee attacks.");
		ATTRIBUTEDESC.Add("RangedCritDamage", "Damage multiplier applied to critical hits from ranged weapons.");
		ATTRIBUTEDESC.Add("AimWalkSpeed", "Speed multiplier applied to movement speed if aiming.\r\n(Must have Aim Mobility skill)");
		ATTRIBUTEDESC.Add("ReloadSpeed", "Rate at which hand moves to different points while reloading.\r\n(multiplied by 0.75 with Sluggish trait)");
		ATTRIBUTEDESC.Add("RangedAccuracy", "Accuracy multiplier applied to weapon's accuracy.");
	}

	[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
	public static void InitializeData()
	{
		Behaviors_Load();
		Traits_Load();
		Vocations_Load();
		Attributes_Load();
		Skills_Load();
        // Transfer setup is optional. It belongs in File > New Transfer, not in startup.
        string path = Path.Combine(StudioModule.BaseDirectory, "config.json");
        if (File.Exists(path))
        {
            try { DIR_TRANSFER = (string)JObject.Parse(File.ReadAllText(path))["Folder"] ?? ""; }
            catch (Newtonsoft.Json.JsonException) { DIR_TRANSFER = ""; }
            catch (IOException) { DIR_TRANSFER = ""; }
            catch (UnauthorizedAccessException) { DIR_TRANSFER = ""; }
        }
		string path2 = StudioModule.BaseDirectory + "/db/images/";
		try
		{
			string[] files = Directory.GetFiles(path2, "*.jpg", SearchOption.AllDirectories);
			string[] array = files;
			foreach (string text2 in array)
			{
				string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(text2);
				Image value = Image.FromFile(text2);
				IMAGES.Add(fileNameWithoutExtension, value);
			}
		}
		catch (DirectoryNotFoundException ex)
		{
			ProjectData.SetProjectError(ex);
			DirectoryNotFoundException ex2 = ex;
			ProjectData.ClearProjectError();
		}
		try
		{
			string[] files2 = string.IsNullOrWhiteSpace(DIR_TRANSFER) ? new string[0] : Directory.GetFiles(Path.Combine(DIR_TRANSFER, "materials"), "*.png", SearchOption.AllDirectories);
			string[] array2 = files2;
			foreach (string text3 in array2)
			{
				string item = text3.Replace(DIR_TRANSFER + "/materials/", "").Replace(".png", "").Replace("\\", "/");
				if (!CUSTOMTEX.Contains(item))
				{
					CUSTOMTEX.Add(item);
				}
			}
		}
		catch (DirectoryNotFoundException ex3)
		{
			ProjectData.SetProjectError(ex3);
			DirectoryNotFoundException ex4 = ex3;
			ProjectData.ClearProjectError();
		}
		try
		{
			JObject jObject3 = JObject.Parse(File.ReadAllText(StudioModule.BaseDirectory + "/db/Glossary.json"));
			foreach (JToken item2 in jObject3["Misc"]["ModelParts"].Children())
			{
				ALTERNATEPARTS.Add(item2.ToString());
			}
			foreach (JToken item3 in jObject3["Misc"]["Materials"].Children())
			{
				MATERIALS.Add(item3.ToString());
			}
			foreach (JToken item4 in jObject3["Misc"]["Sprites"].Children())
			{
				SPRITES.Add(item4.ToString());
			}
			foreach (JToken item5 in jObject3["Misc"]["AnimationClips"].Children())
			{
				ANIMATIONCLIPS.Add(item5.ToString());
			}
			foreach (JToken item6 in jObject3["Misc"]["Music"].Children())
			{
				MUSIC.Add(item6.ToString());
			}
			foreach (JToken item7 in jObject3["Misc"]["Modules"].Children())
			{
				MODULES.Add(item7.ToString());
			}
			foreach (JToken item8 in jObject3["Misc"]["OneShots"].Children())
			{
				ONESHOTS.Add(item8.ToString());
			}
			MATERIALS.AddRange(CUSTOMTEX);
			SPRITES.AddRange(CUSTOMTEX);
		}
		catch (DirectoryNotFoundException ex5)
		{
			ProjectData.SetProjectError(ex5);
			DirectoryNotFoundException ex6 = ex5;
			ProjectData.ClearProjectError();
		}
	}
}
