using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;
using Newtonsoft.Json.Linq;

namespace MadCardMaker;

[StandardModule]
public sealed class ModNodes
{
	public static TreeNode GetRootNode(TreeNode node)
	{
		if (node.Parent != null)
		{
			return GetRootNode(node.Parent);
		}
		return node;
	}

	public static bool LoadedPrior(string type, string card, int priority)
	{
		return ModEnums.SC_DIR[type].ContainsKey(card) | (!ModEnums.MC_DIR[type].ContainsKey(card) || priority > LowestPriority(type + ":" + card));
	}

	public static int GetRootPriority(string cardType, string cardRoot, string[] names)
	{
		int result = 0;
		if (ModEnums.MC_DIR[cardType].ContainsKey(cardRoot))
		{
			JObject jObject = ModEnums.MC_DIR[cardType][cardRoot];
			if (jObject["Priority"] != null)
			{
				int.TryParse(jObject["Priority"].ToString(), out result);
			}
		}
		else if (names.Count() > 0)
		{
			foreach (string text in names)
			{
				if (ModEnums.MC_VER.ContainsKey(cardType + ":" + text))
				{
					result = Math.Max(result, LowestPriority(cardType + ":" + text));
				}
			}
		}
		return checked(result + 1);
	}

	public static void AddCardVersion(ModCards.CardTag card)
	{
		if (!ModEnums.MC_VER.ContainsKey(card.ID()))
		{
			ModEnums.MC_VER.Add(card.ID(), new SortedDictionary<string, ModCards.CardTag>(StringComparer.OrdinalIgnoreCase));
		}
		if (ModEnums.MC_VER[card.ID()].ContainsKey(card.Loc()))
		{
			ModEnums.MC_VER[card.ID()][card.Loc()] = card;
		}
		else
		{
			ModEnums.MC_VER[card.ID()].Add(card.Loc(), card);
		}
	}

	public static void RemoveCardVersion(ModCards.CardTag card)
	{
		if (ModEnums.MC_VER.ContainsKey(card.ID()))
		{
			if (ModEnums.MC_VER[card.ID()].ContainsKey(card.Loc()))
			{
				ModEnums.MC_VER[card.ID()].Remove(card.Loc());
			}
			if (ModEnums.MC_VER[card.ID()].Count == 0)
			{
				ModEnums.MC_VER.Remove(card.ID());
			}
		}
	}

	public static JObject ReturnCardInfo(string ID, int priority)
	{
		List<ModCards.CardTag> list = new List<ModCards.CardTag>();
		foreach (ModCards.CardTag value in ModEnums.MC_VER[ID].Values)
		{
			if (value.Priority < priority)
			{
				list.Add(value);
			}
		}
		list.Sort([SpecialName] (ModCards.CardTag x, ModCards.CardTag y) => x.Priority.CompareTo(y.Priority));
		return list.Last().Info;
	}

	public static int ReturnVersionCount(string ID, int priority)
	{
		if (ModEnums.MC_VER.ContainsKey(ID))
		{
			List<ModCards.CardTag> list = new List<ModCards.CardTag>();
			foreach (ModCards.CardTag value in ModEnums.MC_VER[ID].Values)
			{
				if (value.Priority < priority)
				{
					list.Add(value);
				}
			}
			return list.Count;
		}
		return -1;
	}

	public static string ReturnIDLocations(string ID)
	{
		if (ModEnums.MC_VER.ContainsKey(ID))
		{
			foreach (string key in ModEnums.MC_VER[ID].Keys)
			{
				_ = key;
			}
		}
		return Conversions.ToString(Value: false);
	}

	public static bool PriorityConflict(ModCards.CardTag card)
	{
		if (ModEnums.MC_VER.ContainsKey(card.ID()))
		{
			foreach (ModCards.CardTag value in ModEnums.MC_VER[card.ID()].Values)
			{
				if (value.Priority == card.Priority)
				{
					return true;
				}
			}
		}
		return false;
	}

	public static int LowestPriority(string ID)
	{
		int num = 0;
		if (ModEnums.MC_VER.ContainsKey(ID))
		{
			num = int.MaxValue;
			foreach (ModCards.CardTag value in ModEnums.MC_VER[ID].Values)
			{
				num = Math.Min(num, value.Priority);
			}
		}
		return num;
	}

	public static string TraceRootPath(ModCards.CardTag card)
	{
		string text = "";
		Stack<ModCards.CardTag> stack = FullRootOf(card.RootID(), card.Priority, new Stack<ModCards.CardTag>());
		if (stack.Count > 0)
		{
			while (stack.Count > 0)
			{
				text = stack.Pop().RootName + ((text.Length > 0) ? ", " : "") + text;
			}
		}
		return card.RootName + ((text.Length > 0) ? ", " : "") + text;
	}

	public static Color PriorityLabel(ModCards.CardTag card)
	{
		Color result = ModColors.MenuColors.Note;
		Stack<ModCards.CardTag> stack = FullRootOf(card.RootID(), card.Priority, new Stack<ModCards.CardTag>());
		string key = ((stack.Count > 0) ? stack.Peek().RootName : card.RootName);
		if (!ModEnums.SC_DIR[card.Type].ContainsKey(key))
		{
			return ModColors.MenuColors.Warning;
		}
		while (stack.Count > 0)
		{
			ModCards.CardTag cardTag = stack.Pop();
			cardTag.ID();
			if (ModEnums.MC_VER.ContainsKey(cardTag.ID()))
			{
				int num = ModEnums.MC_VER[cardTag.ID()].Count;
				foreach (ModCards.CardTag value in ModEnums.MC_VER[cardTag.ID()].Values)
				{
					if ((card.Priority <= value.Priority) & (Operators.CompareString(card.FileName, value.FileName, TextCompare: false) != 0))
					{
						result = ModColors.MenuColors.Highlight;
						num = checked(num - 1);
					}
				}
				if (num <= 0)
				{
					return ModColors.MenuColors.Warning;
				}
				continue;
			}
			return ModColors.MenuColors.Warning;
		}
		return result;
	}

	public static string PriorityWarning(ModCards.CardTag card)
	{
		Stack<ModCards.CardTag> stack = FullRootOf(card.RootID(), card.Priority, new Stack<ModCards.CardTag>());
		string text = ((stack.Count > 0) ? stack.Peek().RootName : card.RootName);
		while (stack.Count > 0)
		{
			ModCards.CardTag cardTag = stack.Pop();
			cardTag.ID();
			if (ModEnums.MC_VER.ContainsKey(cardTag.ID()))
			{
				int num = ModEnums.MC_VER[cardTag.ID()].Count;
				foreach (ModCards.CardTag value in ModEnums.MC_VER[cardTag.ID()].Values)
				{
					if (card.Priority <= value.Priority)
					{
						num = checked(num - 1);
					}
				}
				if (num <= 0)
				{
					return "\r\n{NO COMPATIBLE PRIORITIES FOUND FOR " + cardTag.FileName + "!}";
				}
				continue;
			}
			return "\r\n{ROOT " + cardTag.FileName + " DOES NOT EXIST!}";
		}
		if (!ModEnums.SC_DIR[card.Type].ContainsKey(text))
		{
			return "\r\n{CARD DOES NOT TRACE BACK TO INTENDED ROOT: " + text + "}";
		}
		return "";
	}

	public static Stack<ModCards.CardTag> FullRootOf(string rootID, int Priority, Stack<ModCards.CardTag> history)
	{
		if (ModEnums.MC_VER.ContainsKey(rootID))
		{
			List<ModCards.CardTag> list = new List<ModCards.CardTag>();
			foreach (ModCards.CardTag value in ModEnums.MC_VER[rootID].Values)
			{
				if (value.Priority < Priority)
				{
					list.Add(value);
				}
			}
			if (list.Count > 0)
			{
				list.Sort([SpecialName] (ModCards.CardTag x, ModCards.CardTag y) => x.Priority.CompareTo(y.Priority));
				history.Push(list.First());
				return FullRootOf(list.First().RootID(), list.First().Priority, history);
			}
		}
		return history;
	}

	public static JObject FinalVersion(string MCType, JObject Info, bool IncludeModSource = false)
	{
		JObject result = JObject.Parse(Info.ToString());
		if ((Info["Root"] == null) | !ModEnums.MC_DIR.ContainsKey(MCType))
		{
			return result;
		}
		string text = Info["Root"].ToString();
		string rootID = MCType + ":" + text;
		int result2 = 0;
		if (Info["Priority"] != null)
		{
			int.TryParse(Info["Priority"].ToString(), NumberStyles.Integer, CultureInfo.InvariantCulture, out result2);
		}
		Stack<ModCards.CardTag> stack = FullRootOf(rootID, result2, new Stack<ModCards.CardTag>());
		string key = ((stack.Count > 0) ? stack.Peek().RootName : text);
		if (ModEnums.SC_DIR[MCType].ContainsKey(key))
		{
			JObject jObject = JObject.Parse(ModEnums.SC_DIR[MCType][key].ToString());
			while (stack.Count > 0)
			{
				ModCards.CardTag cardTag = stack.Pop();
				JObject info = cardTag.Info;
				foreach (JProperty item in info.Properties())
				{
					if (jObject[item.Name] != null)
					{
						jObject.Property(item.Name).Value = item.Value;
					}
				}
				if (IncludeModSource & (jObject["Mod_Name"] == null))
				{
					jObject.Add("Mod_Name", cardTag.AttachedMod);
				}
			}
			foreach (JProperty item2 in Info.Properties())
			{
				if (jObject[item2.Name] != null)
				{
					jObject.Property(item2.Name).Value = item2.Value;
				}
			}
			return jObject;
		}
		return result;
	}
}
