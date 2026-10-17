using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Reflection;
using System.Runtime.InteropServices;
namespace MadCardMaker;
public static class DossierFonts
{
 static readonly PrivateFontCollection Fonts=new PrivateFontCollection();
 static readonly List<IntPtr> Memory=new List<IntPtr>();
 [DllImport("gdi32.dll")]static extern IntPtr AddFontMemResourceEx(IntPtr data,uint size,IntPtr reserved,ref uint count);
 static DossierFonts()
 {
  foreach(var name in new[]{"Play-Regular.ttf","Play-Bold.ttf"})
  using(var stream=typeof(DossierFonts).Assembly.GetManifestResourceStream("Dossier.Fonts."+name))
  {
   if(stream==null)continue;var bytes=new byte[stream.Length];stream.Read(bytes,0,bytes.Length);var memory=Marshal.AllocCoTaskMem(bytes.Length);Marshal.Copy(bytes,0,memory,bytes.Length);Memory.Add(memory);Fonts.AddMemoryFont(memory,bytes.Length);uint count=0;AddFontMemResourceEx(memory,(uint)bytes.Length,IntPtr.Zero,ref count);
  }
 }
 public static Font Create(string family,float size,FontStyle style=FontStyle.Regular,GraphicsUnit unit=GraphicsUnit.Point)
 {foreach(var font in Fonts.Families)if(font.Name.Equals(family,StringComparison.OrdinalIgnoreCase))return new Font(font,size,style,unit);return new Font(family,size,style,unit);}
 public static Font Logical(string family,float points,FontStyle style=FontStyle.Regular)=>Create(family,points*96f/72f,style,GraphicsUnit.Pixel);
}
