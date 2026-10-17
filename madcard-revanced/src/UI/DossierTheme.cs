using System;
using System.IO;
using System.Drawing;
using Newtonsoft.Json;
namespace MadCardMaker;
public sealed class DossierTheme
{
 public string Background{get;set;}="#111114";
 public string Surface{get;set;}="#1C1B20";
 public string Input{get;set;}="#101014";
 public string Text{get;set;}="#E1DFD9";
 public string Muted{get;set;}="#AAA9A1";
 public string Accent{get;set;}="#D95460";
 public string Cyan{get;set;}="#D7B4AE";
 public string Line{get;set;}="#3F3036";
 public string FontFamily{get;set;}="Play";
 public string HeadingFont{get;set;}="Play";
 public float InterfaceScale{get;set;}=1.15f;
 public static string ThemePath=>StudioModule.SharedThemePath??Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),"MadCardMakerRevanced","theme.json");
 public void Validate()
 {
  if(float.IsNaN(InterfaceScale)||float.IsInfinity(InterfaceScale)||InterfaceScale<1||InterfaceScale>1.4f)throw new FormatException("Interface size must be between 100% and 140%.");
  foreach(var p in typeof(DossierTheme).GetProperties())if(p.CanWrite&&p.PropertyType==typeof(string)&&p.Name!="FontFamily"&&p.Name!="HeadingFont"){
   var v=(string)p.GetValue(this);if(v==null||v.Length!=7||v[0]!='#')throw new FormatException(p.Name+" must be a #RRGGBB color.");
   int.Parse(v.Substring(1),System.Globalization.NumberStyles.HexNumber);
  }
  using(var font=DossierFonts.Create(FontFamily,10))if(!font.Name.Equals(FontFamily,StringComparison.OrdinalIgnoreCase))throw new FormatException("Font is not installed: "+FontFamily);
  using(var font=DossierFonts.Create(HeadingFont,10))if(!font.Name.Equals(HeadingFont,StringComparison.OrdinalIgnoreCase))throw new FormatException("Font is not installed: "+HeadingFont);
 }
 public static DossierTheme Read(string path){var theme=JsonConvert.DeserializeObject<DossierTheme>(File.ReadAllText(path));if(theme==null)throw new FormatException("Theme is empty.");theme.Validate();return theme;}
 public static DossierTheme Load(){try{return File.Exists(ThemePath)?Read(ThemePath):new DossierTheme();}catch{return new DossierTheme();}}
 public void Write(string path){Validate();Directory.CreateDirectory(Path.GetDirectoryName(Path.GetFullPath(path)));string temp=path+".tmp";File.WriteAllText(temp,JsonConvert.SerializeObject(this,Formatting.Indented));if(File.Exists(path))File.Replace(temp,path,path+".bak");else File.Move(temp,path);}
 public Color ColorFor(string property)=>ColorTranslator.FromHtml((string)GetType().GetProperty(property).GetValue(this));
}
