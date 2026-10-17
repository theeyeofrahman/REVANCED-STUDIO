using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

namespace MadCardMaker;

[StandardModule]
public sealed class ModColors
{
	public class ColorPack
	{
		public string Name;

		public Color Form;

		public Color Panel;

		public Color Text;

		public Color Highlight;

		public Color Note;

		public Color Warning;

		public ColorPack(string n, Color fcol, Color pcol, Color tcol, Color hcol, Color ncol, Color wcol)
		{
			Name = "";
			Form = Color.DarkGray;
			Panel = RGB(32, 32, 32);
			Text = Color.White;
			Highlight = Color.Yellow;
			Note = Color.PaleGreen;
			Warning = Color.Salmon;
			Name = n;
			Form = fcol;
			Panel = pcol;
			Text = tcol;
			Highlight = hcol;
			Note = ncol;
			Warning = wcol;
		}

		public override string ToString()
		{
			return Name;
		}
	}

	public static ColorPack COLORS_DEFAULT = new ColorPack("Default", RGB(64, 64, 64), RGB(32, 32, 32), Color.White, Color.Yellow, Color.PaleGreen, Color.Salmon);

	public static ColorPack COLORS_AAHW = new ColorPack("AAHW", RGB(38, 38, 38), RGB(19, 19, 19), Color.Gray, Color.Yellow, Color.White, Color.Red);

	public static ColorPack COLORS_BANDIT = new ColorPack("Bandit", RGB(89, 86, 75), RGB(48, 39, 36), RGB(242, 233, 194), Color.Lime, Color.White, RGB(255, 123, 51));

	public static ColorPack COLORS_MERC = new ColorPack("Merc", RGB(48, 48, 48), RGB(109, 77, 29), RGB(255, 218, 171), RGB(255, 216, 51), RGB(51, 213, 255), RGB(255, 172, 171));

	public static ColorPack COLORS_NEXUS = new ColorPack("Nexus", RGB(64, 0, 0), RGB(19, 19, 19), RGB(255, 98, 98), RGB(195, 212, 207), RGB(131, 214, 255), Color.Magenta);

	public static ColorPack MenuColors = COLORS_DEFAULT;

	private static Color RGB(int r, int g, int b)
	{
		return Color.FromArgb(r, g, b);
	}

	public static void ApplyMenuColors(Form form, bool colorDisabled=false)
 {
  MenuColors=new ColorPack("REVANCED",DossierStyle.Background,DossierStyle.Surface,DossierStyle.Text,DossierStyle.Accent,DossierStyle.Cyan,Color.Salmon);
  DossierStyle.Apply(form);
 }

public static void HighlightToolTips(RichTextBox InfoTab, string expression, Color hcolor)
	{
		MatchCollection matchCollection = Regex.Matches(InfoTab.Text.ToLower(), expression);
		foreach (Match item in matchCollection)
		{
			InfoTab.Select(item.Index, item.Length);
			InfoTab.SelectionColor = hcolor;
			InfoTab.SelectionFont = new Font(InfoTab.Font.FontFamily, InfoTab.Font.Size, (hcolor == MenuColors.Highlight) ? (InfoTab.SelectionFont?.Style??FontStyle.Regular) : FontStyle.Italic);
		}
	}

	public static string Cleaned(string str)
	{
		return Regex.Replace(Regex.Replace(str, "\\[\\s*null\\s*\\]", "[]"), "\\[\\s*\"empty\"\\s*\\]", "[]");
	}

	public static string[] Logged(string str)
	{
		List<string> list = new List<string>();
		MatchCollection matchCollection = Regex.Matches(str, "(.+) \\((.+)\\) from Dir: (.+)");
		foreach (Match item2 in matchCollection)
		{
			string item = Regex.Replace(item2.Value, "(.+) \\((.+)\\) from Dir: (.+)", "$1|$2|$3");
			list.Add(item);
		}
		return list.ToArray();
	}

	public static string[] Ordinals(string str)
	{
		List<string> list = new List<string>();
		MatchCollection matchCollection = Regex.Matches(str, "> CUSTENUM: (\\w+) to (\\w+) at index");
		foreach (Match item2 in matchCollection)
		{
			string item = Regex.Replace(item2.Value, "> CUSTENUM: (\\w+) to (\\w+) at index", "$1|$2");
			list.Add(item);
		}
		return list.ToArray();
	}
}
