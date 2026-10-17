using System;
using System.Collections.Generic;
using System.IO;
using MadCardMaker.My;
using Microsoft.VisualBasic.CompilerServices;
using Newtonsoft.Json.Linq;

namespace MadCardMaker;

[StandardModule]
public sealed class ModEnums
{
	public enum CardType
	{
		StatCard,
		MadCard,
		ModFolder
	}

	public enum LogType
	{
		None,
		Unread,
		Logged,
		Enabled
	}

	public enum ItemType
	{
		None,
		Item,
		Weapon,
		Loadout
	}

	public static string[] CARDTYPES = new string[34]
	{
		"Addon", "AddonGroup", "Ammo", "AnimationProfile", "Animations", "Armor", "Campaign", "Character", "Chatter", "Corpus",
		"Cutscene", "DoT", "Driver", "Event", "Execution", "Explosion", "Keycard", "Loadout", "Melee", "Mission",
		"Origin", "Ranged", "Skill", "SkillGroup", "Skin", "SoundPack", "Stage_Arena", "Stage_Custom", "Stage_Story", "Swatch",
		"Thrown", "VisualStockpile", "VoiceSet", "Wardrobe"
	};

	public static Dictionary<string, SortedDictionary<string, JObject>> SC_DIR = new Dictionary<string, SortedDictionary<string, JObject>>();

	public static Dictionary<string, SortedDictionary<string, JObject>> MC_DIR = new Dictionary<string, SortedDictionary<string, JObject>>();

	public static SortedDictionary<string, SortedDictionary<string, ModCards.CardTag>> MC_VER = new SortedDictionary<string, SortedDictionary<string, ModCards.CardTag>>(StringComparer.OrdinalIgnoreCase);

	public static Dictionary<string, JObject> LogByMod = new Dictionary<string, JObject>();

	public static Dictionary<string, string> AssetByMod = new Dictionary<string, string>();

	public static Dictionary<string, string> FolderByMod = new Dictionary<string, string>();

	public static Dictionary<string, int> TypePriority = new Dictionary<string, int>();

	public static List<string> DECKADD = new List<string>();

	public static List<ModCards.CardTag> DECKSORT = new List<ModCards.CardTag>();

	public static List<ModCards.ModTag> MODSORT = new List<ModCards.ModTag>();

	public static string CURRLOG = "";

	public static LogType LOGSTATUS = LogType.None;

	public static Dictionary<string, List<string>> ENUMS = new Dictionary<string, List<string>>();

	public static Dictionary<string, List<string>> ASSETS = new Dictionary<string, List<string>>();

	public static JObject TT_PROPERTIES = new JObject();

	public static JObject TT_ENUMS = new JObject();

	public static JObject TT_ASSETS = new JObject();

	public static string MOD_DIR = "";

	public static string WORKSHOP_DIR = "workshop\\content\\488860\\";

	public static void AddListOf(Dictionary<string, List<string>> dict, JObject bundle, string ModMap = "")
	{
		try
		{
			foreach (JProperty item in bundle.Properties())
			{
				if (!dict.ContainsKey(item.Name))
				{
					dict.Add(item.Name, new List<string>());
				}
				foreach (JToken item2 in item.Value.Children())
				{
					dict[item.Name].Add(item2.ToString());
					string text = item.Name;
					if (Operators.CompareString(item.Name, "MadObj_Projectile", TextCompare: false) == 0)
					{
						text = "GameObject";
					}
					if ((Operators.CompareString(item.Name, "MadObj_Casing", TextCompare: false) == 0) | (Operators.CompareString(item.Name, "Particle_Flare", TextCompare: false) == 0) | (Operators.CompareString(item.Name, "Particle_Blood", TextCompare: false) == 0))
					{
						text = "ParticleSystem";
					}
					bool flag = (Operators.CompareString(item.Name, "MadObj_Casing", TextCompare: false) == 0) | (Operators.CompareString(item.Name, "Particle_Flare", TextCompare: false) == 0) | (Operators.CompareString(item.Name, "Particle_Blood", TextCompare: false) == 0) | (Operators.CompareString(item.Name, "MadObj_Projectile", TextCompare: false) == 0);
					string key = text + "|" + item2.ToString();
					if (!AssetByMod.ContainsKey(key))
					{
						AssetByMod.Add(key, (Operators.CompareString(ModMap, "", TextCompare: false) == 0) ? "V" : ("{" + ModMap + "}"));
					}
					else if ((ModMap.Length > 0) & flag)
					{
						AssetByMod[key] = AssetByMod[key] + ", {" + ModMap + "}";
					}
				}
			}
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			Console.WriteLine("Error reading list:" + ex2.Message);
			ProjectData.ClearProjectError();
		}
	}

	public static void ClearAllLists()
	{
		ENUMS.Clear();
		ASSETS.Clear();
	}

	public static void WriteDefaultLists()
	{
		try
		{
			AddListOf(ENUMS, JObject.Parse(File.ReadAllText(StudioModule.BaseDirectory + "\\db\\DefaultEnums.json")));
			Console.WriteLine("Done Reading");
			AddListOf(ASSETS, JObject.Parse(File.ReadAllText(StudioModule.BaseDirectory + "\\db\\DefaultAssets.json")));
		}
		catch (DirectoryNotFoundException ex)
		{
			ProjectData.SetProjectError(ex);
			DirectoryNotFoundException ex2 = ex;
			Console.WriteLine("Directory Not Found for Default Lists: ");
			ProjectData.ClearProjectError();
		}
	}

	public static void InitializeData()
	{
		TT_PROPERTIES = JObject.Parse(File.ReadAllText(StudioModule.BaseDirectory + "\\db\\Tooltips_Obj.json"));
		TT_ENUMS = JObject.Parse(File.ReadAllText(StudioModule.BaseDirectory + "\\db\\Tooltips_Enums.json"));
		TT_ASSETS = JObject.Parse(File.ReadAllText(StudioModule.BaseDirectory + "\\db\\DefaultAssets.json"));
	}
}
