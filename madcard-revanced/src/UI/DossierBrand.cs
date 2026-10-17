using System.Drawing;
namespace MadCardMaker;
internal static class DossierBrand
{
 internal const string Name="MadCard Maker REVANCED";
 internal static readonly Icon Icon=LoadIcon();
 static Icon LoadIcon(){using(var stream=typeof(DossierBrand).Assembly.GetManifestResourceStream("Dossier.AppIcon"))using(var icon=new Icon(stream))return (Icon)icon.Clone();}
}
