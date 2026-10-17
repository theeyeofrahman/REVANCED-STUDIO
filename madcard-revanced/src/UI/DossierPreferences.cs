using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace MadCardMaker;

public sealed class DossierPreferences
{
    public bool Sounds { get; set; } = true;
    public bool TypingSounds { get; set; } = true;
    public bool Animations { get; set; } = true;
    public int QuietFeedbackVersion { get; set; }
    public string RoomFolder { get; set; }
    public bool MainWindowMaximized { get; set; } = true;
    public int MainWindowWidth { get; set; } = 1320;
    public int MainWindowHeight { get; set; } = 850;
    public List<string> RecentProjects { get; set; } = new List<string>();
    public List<string> RecentFileFolders { get; set; } = new List<string>();

    public double Volume { get; set; } = .12;
    public List<string> Favorites { get; set; } = new List<string>();
    public List<string> Recent { get; set; } = new List<string>();
    public Dictionary<string,List<string>> FieldChoices { get; set; } = new Dictionary<string,List<string>>();
    public static string SettingsPath => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MadCardMakerRevanced", "interface.json");
    public static DossierPreferences Current { get; } = Load();
    private static DossierPreferences Load()
    {
        try
        {
            var settings = File.Exists(SettingsPath) ? JsonConvert.DeserializeObject<DossierPreferences>(File.ReadAllText(SettingsPath)) : null;
            if(settings==null)return new DossierPreferences();
            settings.Favorites=settings.Favorites??new List<string>();settings.Recent=settings.Recent??new List<string>();
            settings.FieldChoices=settings.FieldChoices??new Dictionary<string,List<string>>();
            settings.RecentProjects=settings.RecentProjects??new List<string>();
            settings.RecentFileFolders=settings.RecentFileFolders??new List<string>();
            settings.Volume=double.IsNaN(settings.Volume)||double.IsInfinity(settings.Volume) ? .25 : Math.Max(0,Math.Min(1,settings.Volume));
            return settings;
        }
        catch { return new DossierPreferences(); }
    }
    public void Save()
    {
        Directory.CreateDirectory(Path.GetDirectoryName(SettingsPath));var temporary=SettingsPath+".tmp";
        File.WriteAllText(temporary,JsonConvert.SerializeObject(this,Formatting.Indented));
        if(File.Exists(SettingsPath))File.Replace(temporary,SettingsPath,SettingsPath+".bak");else File.Move(temporary,SettingsPath);
    }
    public static bool SuppressPersistenceForTests { get; set; }
    public void Persist(){if(SuppressPersistenceForTests)return;try{Save();}catch(IOException){ }catch(UnauthorizedAccessException){ }}
}
