using System;
using System.Collections.Generic;
using System.IO;
using System.Media;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace MadCardMaker;

public static class DossierFeedback
{
    public static bool MutedForTests { get; set; }
    private static readonly ConditionalWeakTable<object,object> Wired=new ConditionalWeakTable<object,object>();
    private static readonly Dictionary<string,Tuple<MemoryStream,SoundPlayer>> Players=new Dictionary<string,Tuple<MemoryStream,SoundPlayer>>();
    private static DateTime _last=DateTime.MinValue;
    private static DateTime _lastType=DateTime.MinValue;
    public static void PrepareSettings(){if(MutedForTests||DossierPreferences.SuppressPersistenceForTests)return;var prefs=DossierPreferences.Current;if(prefs.QuietFeedbackVersion>=1)return;prefs.Sounds=true;prefs.TypingSounds=true;prefs.Volume=.12;prefs.QuietFeedbackVersion=1;prefs.Persist();}
    public static byte[] BuildWave(string kind,double volume)
    {
        var frequency=kind=="save"?880:kind=="error"?180:kind=="tab"?580:kind=="type"?1350:720;
        var duration=kind=="save"?.065:kind=="error"?.06:kind=="type"?.008:.018;
        var count=(int)(22050*duration);var stream=new MemoryStream();var writer=new BinaryWriter(stream);
        writer.Write(System.Text.Encoding.ASCII.GetBytes("RIFF"));writer.Write(36+count*2);writer.Write(System.Text.Encoding.ASCII.GetBytes("WAVEfmt "));
        writer.Write(16);writer.Write((short)1);writer.Write((short)1);writer.Write(22050);writer.Write(44100);writer.Write((short)2);writer.Write((short)16);
        writer.Write(System.Text.Encoding.ASCII.GetBytes("data"));writer.Write(count*2);
        var gain=Math.Max(0,Math.Min(1,double.IsNaN(volume)?0:volume))*(kind=="type"?.024:.04);
        for(int i=0;i<count;i++)
        {
            var phase=(double)i/count;var envelope=Math.Sin(Math.PI*phase)*Math.Exp(-phase*4);
            var wave=Math.Sin(2*Math.PI*frequency*i/22050d);
            writer.Write((short)(32767*gain*envelope*wave));
        }
        writer.Flush();return stream.ToArray();
    }
    public static void Play(string kind="click")
    {
        if(MutedForTests||!DossierPreferences.Current.Sounds||DossierPreferences.Current.Volume<=0)return;
        if(kind=="type"){if((DateTime.UtcNow-_lastType).TotalMilliseconds<45)return;_lastType=DateTime.UtcNow;}else{if((DateTime.UtcNow-_last).TotalMilliseconds<75)return;_last=DateTime.UtcNow;}
        var key=kind+":"+DossierPreferences.Current.Volume.ToString("0.00",System.Globalization.CultureInfo.InvariantCulture);
        try
        {
            Tuple<MemoryStream,SoundPlayer> sound;
            if(!Players.TryGetValue(key,out sound))
            {
                var stream=new MemoryStream(BuildWave(kind,DossierPreferences.Current.Volume));var player=new SoundPlayer(stream);player.Load();
                sound=Tuple.Create(stream,player);Players[key]=sound;
            }
            sound.Item2.Play();
        }
        catch { /* Audio device availability must never interrupt an edit. */ }
    }
    public static void Attach(Control root)
    {
        if(!Wired.TryGetValue(root,out _))
        {
            Wired.Add(root,new object());
            if(root is ButtonBase button)button.Click+=(_,__)=>Play();
            if(root is TabControl tabs)tabs.MouseDown+=(_,__)=>Play("tab");
            if(root is ComboBox combo)combo.SelectionChangeCommitted+=(_,__)=>Play("tab");
            if(root is TreeView tree)tree.AfterSelect+=(_,__)=>{if(tree.Focused)Play("tab");};
            if(root is TextBoxBase text)text.KeyPress+=(_,e)=>{if(text.Focused&&!text.ReadOnly&&!e.Handled&&(e.KeyChar>=' '||e.KeyChar=='\b')&&DossierPreferences.Current.TypingSounds)Play("type");};
            if(root is ListBox list)list.MouseDown+=(_,__)=>Play();
            if(root is ListView files)files.MouseDown+=(_,__)=>Play();
            if(root is ToolStrip strip){strip.ItemClicked+=(_,__)=>Play();foreach(ToolStripItem item in strip.Items)if(item is ToolStripDropDownItem drop&&drop.HasDropDownItems)Attach(drop.DropDown);}
            root.ControlAdded+=(_,e)=>Attach(e.Control);
        }
        foreach(Control child in root.Controls)Attach(child);
    }
}
