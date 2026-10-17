using System;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

namespace MPNDossier;

[StandardModule]
public sealed class ModPrice
{
	public static int Market_StorePrice(int price)
	{
		if (price < 1)
		{
			return 1;
		}
		if (price < 100)
		{
			return price;
		}
		if (price < 1000)
		{
			return Convert.ToInt32(decimal.Multiply(Math.Floor(decimal.Divide(new decimal(price), 5m)), 5m));
		}
		return Convert.ToInt32(decimal.Multiply(Math.Floor(decimal.Divide(new decimal(price), 10m)), 10m));
	}

	public static decimal Market_BonusMultiplier(TreeNode Bonuses)
	{
		decimal num = 1m;
		foreach (TreeNode node in Bonuses.Nodes)
		{
			decimal num2 = 0m;
			ModEnums.BonusType bonusType = (ModEnums.BonusType)int.Parse(Conversions.ToString(NewLateBinding.LateGet(node.Nodes[2].Tag, null, "Value", new object[0], null, null, null)));
			decimal d = decimal.Parse(Conversions.ToString(NewLateBinding.LateGet(node.Nodes[3].Tag, null, "Value", new object[0], null, null, null)));
			int num3 = int.Parse(Conversions.ToString(NewLateBinding.LateGet(node.Nodes[4].Tag, null, "Value", new object[0], null, null, null)));
			int num4 = int.Parse(Conversions.ToString(NewLateBinding.LateGet(node.Nodes[5].Tag, null, "Value", new object[0], null, null, null)));
			ModEnums.BonusType bonusType2 = bonusType;
			switch (bonusType2)
			{
			case ModEnums.BonusType.ExtraMags_Handgun:
				num2 = 0.2m;
				break;
			case ModEnums.BonusType.ExtraMags_Standard:
				num2 = 0.3m;
				break;
			case ModEnums.BonusType.ExtraMags_Heavy:
			case ModEnums.BonusType.ExtraMags_Thrown:
				num2 = 0.4m;
				break;
			case ModEnums.BonusType.ExtraMags:
				num2 = 0.6m;
				break;
			case ModEnums.BonusType.Stat_Strength:
			case ModEnums.BonusType.Stat_Dexterity:
			case ModEnums.BonusType.Stat_Endurance:
			case ModEnums.BonusType.Stat_Tactics:
				num2 = 2m;
				break;
			default:
				num2 = ((bonusType2 < ModEnums.BonusType.Stat_Force || bonusType2 > ModEnums.BonusType.Stat_Senses) ? 0.1m : 0.4m);
				break;
			}
			num2 = decimal.Multiply(Math.Abs(decimal.Multiply(decimal.Multiply(num2, d), (num3 > 0) ? 20m : 1m)), (num4 > 0) ? 1m : (-0.25m));
			num = decimal.Add(num, num2);
		}
		return num;
	}

	public static int Market_ArmorPrice(TreeNodeCollection Armor)
	{
		int value = int.Parse(Conversions.ToString(NewLateBinding.LateGet(Armor[7].Tag, null, "Value", new object[0], null, null, null)));
		decimal d = decimal.Parse(Conversions.ToString(NewLateBinding.LateGet(Armor[13].Tag, null, "Value", new object[0], null, null, null)));
		decimal num = decimal.Parse(Conversions.ToString(NewLateBinding.LateGet(Armor[11].Nodes[0].Tag, null, "Value", new object[0], null, null, null)));
		ModEnums.ArmorSlots armorSlots = (ModEnums.ArmorSlots)int.Parse(Conversions.ToString(NewLateBinding.LateGet(Armor[14].Tag, null, "Value", new object[0], null, null, null)));
		decimal d2 = new decimal(Convert.ToDouble(decimal.Multiply(100m, decimal.Add(1m, decimal.Divide(new decimal(value), (armorSlots == ModEnums.ArmorSlots.Hair) ? 2m : 5m)))) + Math.Pow(Convert.ToDouble(num), 3.25));
		decimal d3 = decimal.Add(0.5m, decimal.Multiply(0.1m, decimal.Divide(decimal.Add(1.5m, decimal.Multiply(num, 2m)), decimal.Add(d, 0.1m))));
		return Market_StorePrice(Convert.ToInt32(Math.Ceiling(decimal.Multiply(decimal.Multiply(d2, d3), Market_BonusMultiplier(Armor[10])))));
	}

	public static int Market_WeaponPrice(TreeNodeCollection Weapon)
	{
		decimal num = decimal.Parse(Conversions.ToString(NewLateBinding.LateGet(Weapon[7].Tag, null, "Value", new object[0], null, null, null)));
		ModEnums.Market market = (ModEnums.Market)int.Parse(Conversions.ToString(NewLateBinding.LateGet(Weapon[10].Tag, null, "Value", new object[0], null, null, null)));
		string key = NewLateBinding.LateGet(Weapon[12].Tag, null, "Value", new object[0], null, null, null).ToString();
		int num2 = int.Parse(Conversions.ToString(NewLateBinding.LateGet(Weapon[17].Tag, null, "Value", new object[0], null, null, null)));
		decimal d = decimal.Parse(Conversions.ToString(NewLateBinding.LateGet(Weapon[18].Tag, null, "Value", new object[0], null, null, null)));
		decimal d2 = decimal.Parse(Conversions.ToString(NewLateBinding.LateGet(Weapon[20].Tag, null, "Value", new object[0], null, null, null)));
		decimal d3 = decimal.Parse(Conversions.ToString(NewLateBinding.LateGet(Weapon[21].Tag, null, "Value", new object[0], null, null, null)));
		decimal num3 = new decimal(25.0 + Math.Pow(Convert.ToDouble(decimal.Add(decimal.Add(decimal.Multiply(d, 2m), d3), d2)), 2.0));
		if (decimal.Compare(d, 5m) > 0)
		{
			num3 = decimal.Multiply(num3, (num2 > 0) ? decimal.Divide(new decimal(num2), 100m) : 5m);
		}
		if (market == ModEnums.Market.BlackMarket)
		{
			num3 = new decimal(Math.Max(Convert.ToDouble(num3), Math.Pow(Convert.ToDouble(decimal.Multiply(num3, 4m)), 0.9)));
		}
		if (ModEnums.DB_WEAPONS.ContainsKey(key))
		{
			num3 = new decimal(Convert.ToDouble(num3) * 1.5);
		}
		if (decimal.Compare(num, 0m) > 0)
		{
			num3 = new decimal(Convert.ToDouble(decimal.Divide(num3, 1m)) + Math.Pow(Convert.ToDouble(num), 1.85) / 15.0 / 100.0);
		}
		return Market_StorePrice(Convert.ToInt32(Math.Ceiling(num3)));
	}

	public static decimal Market_StatMultiplier(TreeNodeCollection Weapon)
	{
		decimal value = decimal.Parse(Conversions.ToString(NewLateBinding.LateGet(Weapon[35].Tag, null, "Value", new object[0], null, null, null)));
		decimal num = decimal.Parse(Conversions.ToString(NewLateBinding.LateGet(Weapon[36].Tag, null, "Value", new object[0], null, null, null)));
		decimal num2 = decimal.Parse(Conversions.ToString(NewLateBinding.LateGet(Weapon[37].Tag, null, "Value", new object[0], null, null, null)));
		decimal value2 = decimal.Parse(Conversions.ToString(NewLateBinding.LateGet(Weapon[38].Tag, null, "Value", new object[0], null, null, null)));
		decimal value3 = decimal.Parse(Conversions.ToString(NewLateBinding.LateGet(Weapon[40].Tag, null, "Value", new object[0], null, null, null)));
		int num3 = int.Parse(Conversions.ToString(NewLateBinding.LateGet(Weapon[41].Tag, null, "Value", new object[0], null, null, null)));
		decimal value4 = decimal.Parse(Conversions.ToString(NewLateBinding.LateGet(Weapon[44].Tag, null, "Value", new object[0], null, null, null)));
		decimal value5 = decimal.Parse(Conversions.ToString(NewLateBinding.LateGet(Weapon[46].Tag, null, "Value", new object[0], null, null, null)));
		decimal value6 = decimal.Parse(Conversions.ToString(NewLateBinding.LateGet(Weapon[47].Tag, null, "Value", new object[0], null, null, null)));
		decimal value7 = decimal.Parse(Conversions.ToString(NewLateBinding.LateGet(Weapon[49].Tag, null, "Value", new object[0], null, null, null)));
		decimal value8 = decimal.Parse(Conversions.ToString(NewLateBinding.LateGet(Weapon[50].Nodes[1].Tag, null, "Value", new object[0], null, null, null)));
		decimal value9 = decimal.Parse(Conversions.ToString(NewLateBinding.LateGet(Weapon[54].Tag, null, "Value", new object[0], null, null, null)));
		return new decimal(Convert.ToDouble(value) * 0.01 + Convert.ToDouble(value3) * 0.005 + Convert.ToDouble(value2) * 0.005 + Convert.ToDouble(value9) * 0.005 + Convert.ToDouble(value4) * 0.5 + Convert.ToDouble(value5) * 0.1 + Convert.ToDouble(value6) * 0.5 + Convert.ToDouble(value7) * 0.1 + Convert.ToDouble(value8) * 0.05 + ((num3 > 0) ? (Math.Pow(num3, 0.08) * 0.025) : 0.0));
	}

	public static int Market_AddonPrice(TreeNodeCollection Weapon)
	{
		int num = int.Parse(Conversions.ToString(NewLateBinding.LateGet(Weapon[8].Tag, null, "Value", new object[0], null, null, null)));
		ModEnums.Market market = (ModEnums.Market)int.Parse(Conversions.ToString(NewLateBinding.LateGet(Weapon[10].Tag, null, "Value", new object[0], null, null, null)));
		decimal value = decimal.Parse(Conversions.ToString(NewLateBinding.LateGet(Weapon[18].Tag, null, "Value", new object[0], null, null, null)));
		decimal value2 = decimal.Parse(Conversions.ToString(NewLateBinding.LateGet(Weapon[20].Tag, null, "Value", new object[0], null, null, null)));
		decimal num2 = decimal.Parse(Conversions.ToString(NewLateBinding.LateGet(Weapon[37].Tag, null, "Value", new object[0], null, null, null)));
		ModEnums.WeaponAddons weaponAddons = (ModEnums.WeaponAddons)int.Parse(Conversions.ToString(NewLateBinding.LateGet(Weapon[58].Tag, null, "Value", new object[0], null, null, null)));
		decimal d = new decimal(50.0 * (1.0 + (double)num / 5.0) + Math.Pow(Convert.ToDouble(value) + Convert.ToDouble(value2) * 0.75, 2.5) + Math.Pow(Convert.ToDouble(num2), 2.0));
		decimal num3 = decimal.Add(1m, Math.Max(0m, decimal.Multiply(Market_StatMultiplier(Weapon), 10m)));
		if (weaponAddons == ModEnums.WeaponAddons.Magazine)
		{
			num3 = new decimal(Convert.ToDouble(decimal.Add(num3, Math.Max(0m, Market_StatMultiplier(Weapon)))) + Math.Pow(Convert.ToDouble(decimal.Divide(num2, 10m)), 2.0));
		}
		d = decimal.Multiply(d, num3);
		if (market == ModEnums.Market.Arcane)
		{
			d = decimal.Add(d, 500m);
		}
		return Market_StorePrice(Convert.ToInt32(d));
	}
}
