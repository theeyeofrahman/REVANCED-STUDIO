using System;
using System.Linq;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;
using Newtonsoft.Json.Linq;

namespace MadCardMaker;

[StandardModule]
public sealed class ModPrice
{
	public static long Market_StorePrice(long price)
	{
		if (price < 1)
		{
			return 1L;
		}
		if (price < 100)
		{
			return price;
		}
		if (price < 1000)
		{
			return Convert.ToInt64(decimal.Multiply(Math.Floor(decimal.Divide(new decimal(price), 5m)), 5m));
		}
		return Convert.ToInt64(decimal.Multiply(Math.Floor(decimal.Divide(new decimal(price), 10m)), 10m));
	}

	public static decimal Market_BonusMultiplier(TreeNode Bonuses)
	{
		decimal num = 1m;
		foreach (TreeNode node in Bonuses.Nodes)
		{
			decimal num2 = 0m;
			string text = NewLateBinding.LateGet(node.Nodes[2].Tag, null, "Value", new object[0], null, null, null).ToString();
			decimal d = decimal.Parse(Conversions.ToString(NewLateBinding.LateGet(node.Nodes[3].Tag, null, "Value", new object[0], null, null, null)));
			bool flag = bool.Parse(Conversions.ToString(NewLateBinding.LateGet(node.Nodes[4].Tag, null, "Value", new object[0], null, null, null)));
			bool flag2 = bool.Parse(Conversions.ToString(NewLateBinding.LateGet(node.Nodes[5].Tag, null, "Value", new object[0], null, null, null)));
			switch (text)
			{
			case "ExtraMags_Handgun":
				num2 = 0.2m;
				break;
			case "ExtraMags_Standard":
				num2 = 0.3m;
				break;
			case "ExtraMags_Heavy":
			case "ExtraMags_Thrown":
				num2 = 0.4m;
				break;
			case "ExtraMags":
				num2 = 0.6m;
				break;
			case "Stat_Strength":
			case "Stat_Tactics":
				num2 = 2m;
				break;
			case "Stat_Force":
			case "Stat_Senses":
				num2 = 0.4m;
				break;
			default:
				num2 = 0.1m;
				break;
			}
			num2 = decimal.Multiply(Math.Abs(decimal.Multiply(decimal.Multiply(num2, d), flag ? 20m : 1m)), flag2 ? 1m : (-0.25m));
			num = decimal.Add(num, num2);
		}
		return num;
	}

	public static long Market_ArmorPrice(TreeNodeCollection Armor)
	{
		int value = int.Parse(Conversions.ToString(NewLateBinding.LateGet(Armor["Tier"].Tag, null, "Value", new object[0], null, null, null)));
		decimal d = decimal.Parse(Conversions.ToString(NewLateBinding.LateGet(Armor["Encumbrance"].Tag, null, "Value", new object[0], null, null, null)));
		decimal num = decimal.Parse(Conversions.ToString(NewLateBinding.LateGet(Armor["Protection"].Tag, null, "Value", new object[0], null, null, null)));
		string left = NewLateBinding.LateGet(Armor["Slot"].Tag, null, "Value", new object[0], null, null, null).ToString();
		decimal d2 = new decimal(Convert.ToDouble(decimal.Multiply(100m, decimal.Add(1m, decimal.Divide(new decimal(value), (Operators.CompareString(left, "Hair", TextCompare: false) == 0) ? 2m : 5m)))) + Math.Pow(Convert.ToDouble(num), 3.25));
		decimal d3 = decimal.Add(0.5m, decimal.Multiply(0.1m, decimal.Divide(decimal.Add(1.5m, decimal.Multiply(num, 2m)), decimal.Add(d, 0.1m))));
		return Market_StorePrice(Convert.ToInt64(Math.Ceiling(decimal.Multiply(decimal.Multiply(d2, d3), Market_BonusMultiplier(Armor[10])))));
	}

	public static long Market_WeaponPrice(TreeNodeCollection Weapon)
	{
		decimal num = decimal.Parse(Conversions.ToString(NewLateBinding.LateGet(Weapon["Weight"].Tag, null, "Value", new object[0], null, null, null)));
		string left = NewLateBinding.LateGet(Weapon["Store"].Tag, null, "Value", new object[0], null, null, null).ToString();
		string value = NewLateBinding.LateGet(Weapon["BrokenReplacement"].Tag, null, "Value", new object[0], null, null, null).ToString();
		int num2 = int.Parse(Conversions.ToString(NewLateBinding.LateGet(Weapon["MeleeAmmo"].Tag, null, "Value", new object[0], null, null, null)));
		decimal d = decimal.Parse(Conversions.ToString(NewLateBinding.LateGet(Weapon["MeleeDamage"].Tag, null, "Value", new object[0], null, null, null)));
		decimal num3 = new decimal(25.0 + Math.Pow(Convert.ToDouble(decimal.Add(d2: decimal.Parse(Conversions.ToString(NewLateBinding.LateGet(Weapon["MeleeArmorPiercing"].Tag, null, "Value", new object[0], null, null, null))), d1: decimal.Add(d2: decimal.Parse(Conversions.ToString(NewLateBinding.LateGet(Weapon["MeleeDefense"].Tag, null, "Value", new object[0], null, null, null))), d1: decimal.Multiply(d, 2m)))), 2.0));
		if (decimal.Compare(d, 5m) > 0)
		{
			num3 = decimal.Multiply(num3, (num2 > 0) ? decimal.Divide(new decimal(num2), 100m) : 5m);
		}
		if (Operators.CompareString(left, "BlackMarket", TextCompare: false) == 0)
		{
			num3 = new decimal(Math.Max(Convert.ToDouble(num3), Math.Pow(Convert.ToDouble(decimal.Multiply(num3, 4m)), 0.9)));
		}
		if (ModCards.Options("Weapon").Contains(value))
		{
			num3 = new decimal(Convert.ToDouble(num3) * 1.5);
		}
		if (decimal.Compare(num, 0m) > 0)
		{
			num3 = new decimal(Convert.ToDouble(decimal.Divide(num3, 1m)) + Math.Pow(Convert.ToDouble(num), 1.85) / 15.0 / 100.0);
		}
		return Market_StorePrice(Convert.ToInt64(Math.Ceiling(num3)));
	}

	public static decimal Market_StatMultiplier(TreeNodeCollection Weapon, bool amGun = false)
	{
		decimal num = decimal.Parse(Conversions.ToString(NewLateBinding.LateGet(Weapon["Accuracy"].Tag, null, "Value", new object[0], null, null, null)));
		decimal.Parse(Conversions.ToString(NewLateBinding.LateGet(Weapon["ArmorPiercing"].Tag, null, "Value", new object[0], null, null, null)));
		decimal num2 = decimal.Parse(Conversions.ToString(NewLateBinding.LateGet(Weapon["Loudness"].Tag, null, "Value", new object[0], null, null, null)));
		decimal num3 = decimal.Parse(Conversions.ToString(NewLateBinding.LateGet(Weapon["Recoil"].Tag, null, "Value", new object[0], null, null, null)));
		decimal num4 = decimal.Parse(Conversions.ToString(NewLateBinding.LateGet(Weapon["Recovery"].Tag, null, "Value", new object[0], null, null, null)));
		int num5 = int.Parse(Conversions.ToString(NewLateBinding.LateGet(Weapon["Ammo"].Tag, null, "Value", new object[0], null, null, null)));
		decimal value = decimal.Parse(Conversions.ToString(NewLateBinding.LateGet(Weapon["BonusAOERange"].Tag, null, "Value", new object[0], null, null, null)));
		decimal value2 = decimal.Parse(Conversions.ToString(NewLateBinding.LateGet(Weapon["BonusHeatseek"].Tag, null, "Value", new object[0], null, null, null)));
		decimal value3 = decimal.Parse(Conversions.ToString(NewLateBinding.LateGet(Weapon["BonusShots"].Tag, null, "Value", new object[0], null, null, null)));
		decimal value4 = decimal.Parse(Conversions.ToString(NewLateBinding.LateGet(Weapon["BonusSpeed"].Tag, null, "Value", new object[0], null, null, null)));
		decimal value5 = decimal.Parse(Conversions.ToString(NewLateBinding.LateGet(Weapon["BonusSpeedBoost"].Nodes[1].Tag, null, "Value", new object[0], null, null, null)));
		if (amGun)
		{
			num3 = Math.Min(Math.Max(num3, 0m), 100m);
			num4 = Math.Min(Math.Max(num4, 0m), 100m);
			num = Math.Min(num, 100m);
			num2 = Math.Min(Math.Max(num2, 0m), 100m);
		}
		return new decimal(Convert.ToDouble(num) * 0.01 + Convert.ToDouble(num3) * 0.005 + Convert.ToDouble(num2) * 0.005 + Convert.ToDouble(num4) * 0.005 + Convert.ToDouble(value) * (amGun ? 0.5 : 0.005) + Convert.ToDouble(value2) * (amGun ? 0.1 : 0.005) + Convert.ToDouble(value3) * (amGun ? 0.5 : 0.005) + Convert.ToDouble(value4) * (amGun ? 0.2 : 0.005) + Convert.ToDouble(value5) * (amGun ? 0.05 : 0.005) + ((num5 > 0) ? (Math.Pow(num5, 0.8) * 0.025) : 0.0));
	}

	public static long Market_AddonPrice(TreeNodeCollection Weapon)
	{
		int num = int.Parse(Conversions.ToString(NewLateBinding.LateGet(Weapon["Tier"].Tag, null, "Value", new object[0], null, null, null)));
		string left = NewLateBinding.LateGet(Weapon["Store"].Tag, null, "Value", new object[0], null, null, null).ToString();
		decimal value = decimal.Parse(Conversions.ToString(NewLateBinding.LateGet(Weapon["MeleeDamage"].Tag, null, "Value", new object[0], null, null, null)));
		decimal value2 = decimal.Parse(Conversions.ToString(NewLateBinding.LateGet(Weapon["MeleeArmorPiercing"].Tag, null, "Value", new object[0], null, null, null)));
		decimal num2 = decimal.Parse(Conversions.ToString(NewLateBinding.LateGet(Weapon["Damage"].Tag, null, "Value", new object[0], null, null, null)));
		string left2 = NewLateBinding.LateGet(Weapon["AddonType"].Tag, null, "Value", new object[0], null, null, null).ToString();
		decimal d = new decimal(50.0 * (1.0 + (double)num / 5.0) + Math.Pow(Convert.ToDouble(value) + Convert.ToDouble(value2) * 0.75, 2.5) + Math.Pow(Convert.ToDouble(num2), 2.0));
		decimal num3 = decimal.Add(1m, Math.Max(0m, decimal.Multiply(Market_StatMultiplier(Weapon), 10m)));
		if (Operators.CompareString(left2, "Magazine", TextCompare: false) == 0)
		{
			num3 = new decimal(Convert.ToDouble(decimal.Add(num3, Math.Max(0m, Market_StatMultiplier(Weapon)))) + Math.Pow(Convert.ToDouble(decimal.Divide(num2, 10m)), 2.0));
		}
		d = decimal.Multiply(d, num3);
		if (Operators.CompareString(left, "Arcane", TextCompare: false) == 0)
		{
			d = decimal.Add(d, 500m);
		}
		return Market_StorePrice(Convert.ToInt64(d));
	}

	public static long Market_RangedPrice(TreeNodeCollection Weapon, bool amThrown = false)
	{
		int num = int.Parse(Conversions.ToString(NewLateBinding.LateGet(Weapon["Tier"].Tag, null, "Value", new object[0], null, null, null)));
		string left = NewLateBinding.LateGet(Weapon["Store"].Tag, null, "Value", new object[0], null, null, null).ToString();
		string left2 = (amThrown ? "thrown" : NewLateBinding.LateGet(Weapon["Category"].Tag, null, "Value", new object[0], null, null, null).ToString());
		bool num2 = !amThrown && bool.Parse(Conversions.ToString(NewLateBinding.LateGet(Weapon["AmAutomatic"].Tag, null, "Value", new object[0], null, null, null)));
		decimal num3 = decimal.Parse(Conversions.ToString(NewLateBinding.LateGet(Weapon["Damage"].Tag, null, "Value", new object[0], null, null, null)));
		decimal num4 = decimal.Parse(Conversions.ToString(NewLateBinding.LateGet(Weapon["Weight"].Tag, null, "Value", new object[0], null, null, null)));
		int num5 = Math.Max(int.Parse(Conversions.ToString(NewLateBinding.LateGet(Weapon["Ammo"].Tag, null, "Value", new object[0], null, null, null))), 1);
		decimal d = new decimal(Math.Max(Convert.ToDouble(decimal.Parse(Conversions.ToString(NewLateBinding.LateGet(Weapon["ROF"].Tag, null, "Value", new object[0], null, null, null)))), 10.0));
		string key = NewLateBinding.LateGet(Weapon["AmmoType"].Tag, null, "Value", new object[0], null, null, null).ToString();
		bool flag = false;
		int num6 = 1;
		decimal d2 = 0m;
		decimal d3 = 0m;
		decimal d4 = 0m;
		if (ModEnums.SC_DIR["Ammo"].ContainsKey(key))
		{
			JObject jObject = ModEnums.SC_DIR["Ammo"][key];
			if (jObject["Shots"] != null)
			{
				num6 = Math.Max(1, int.Parse(jObject["Shots"].ToString()));
			}
			if (jObject["Damage"] != null)
			{
				num3 = decimal.Add(num3, decimal.Multiply(decimal.Parse(jObject["Damage"].ToString()), new decimal(Math.Max(checked(num6 - 1), 1))));
				num3 = decimal.Divide(num3, new decimal(Math.Max(num6, 1)));
			}
			if (jObject["Explosion"] != null && ModEnums.SC_DIR["Explosion"].ContainsKey(jObject["Explosion"].ToString()))
			{
				flag = true;
				JObject jObject2 = ModEnums.SC_DIR["Explosion"][jObject["Explosion"].ToString()];
				if (jObject2["Damage"] != null)
				{
					d2 = decimal.Parse(jObject2["Damage"].ToString());
				}
				if (jObject2["DoT"] != null && ModEnums.SC_DIR["DoT"].ContainsKey(jObject2["DoT"].ToString()))
				{
					JObject jObject3 = ModEnums.SC_DIR["DoT"][jObject2["DoT"].ToString()];
					if (jObject3["Damage"] != null)
					{
						d3 = decimal.Parse(jObject3["Damage"].ToString());
					}
				}
			}
			if (jObject["DoT"] != null && ModEnums.SC_DIR["DoT"].ContainsKey(jObject["DoT"].ToString()))
			{
				JObject jObject4 = ModEnums.SC_DIR["DoT"][jObject["DoT"].ToString()];
				if (jObject4["Damage"] != null)
				{
					d4 = decimal.Parse(jObject4["Damage"].ToString());
				}
			}
		}
		decimal num7 = new decimal(10.0 * (1.0 + (double)num / 5.0));
		decimal num8 = 0.5m;
		if (amThrown)
		{
			num8 = new decimal(Convert.ToDouble(num8) * 0.25);
		}
		if (Operators.CompareString(left2, "Heavy", TextCompare: false) == 0)
		{
			num8 = new decimal(Convert.ToDouble(num8) * 0.85);
		}
		if (num2)
		{
			num7 = decimal.Add(num7, 400m);
		}
		num7 = new decimal(Convert.ToDouble(num7) + Math.Pow(Convert.ToDouble(num3), 2.0) * (double)num6 * Math.Pow((double)num5 / 200.0, 0.9) * Convert.ToDouble(decimal.Divide(d, 10m)));
		num7 = decimal.Add(num7, decimal.Add(decimal.Multiply(d2, 50m), decimal.Multiply(d3, 5m)));
		if (amThrown & !flag)
		{
			num7 = new decimal(Convert.ToDouble(num7) * 0.18);
		}
		num7 = decimal.Add(num7, decimal.Multiply(d4, 50m));
		num8 = decimal.Add(num8, Market_StatMultiplier(Weapon, amGun: true));
		num7 = new decimal(Math.Pow(Convert.ToDouble(decimal.Multiply(num7, num8)), 1.25) * 0.5);
		if (Operators.CompareString(left, "BlackMarket", TextCompare: false) == 0)
		{
			num7 = new decimal(Convert.ToDouble(num7) * 2.5);
		}
		if (decimal.Compare(num4, 0m) > 0)
		{
			num7 = new decimal(Convert.ToDouble(num7) / (1.0 + Math.Pow(Convert.ToDouble(num4), 1.85) / 15.0 / 100.0));
		}
		return Market_StorePrice(Convert.ToInt64(num7));
	}
}
