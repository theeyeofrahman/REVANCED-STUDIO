using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using MadCardMaker.My;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MadCardMaker;

[DesignerGenerated]
public class DialogLoader : Form
{
	private IContainer components;

	private string currentFolder;

	private long FileAmount;

	private long FilesCounted;

	private List<string> AddedDecks;

	private List<ModCards.CardTag> AddedCards;

	private List<ModCards.ModTag> AddedMods;

	private List<string> EnabledMods;

	private int CardTypePriority;

	private string LoggedMod;

	private ModEnums.LogType LoggedStatus;

	[field: AccessedThroughProperty("Loading")]
	internal virtual ProgressBar Loading
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("folderLabel")]
	internal virtual Label folderLabel
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Loader")]
	internal virtual BackgroundWorker Loader
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			DoWorkEventHandler value2 = Loader_DoWork;
			ProgressChangedEventHandler value3 = Loader_ProgressChanged;
			RunWorkerCompletedEventHandler value4 = Loader_WorkerCompleted;
			BackgroundWorker backgroundWorker = field;
			if (backgroundWorker != null)
			{
				backgroundWorker.DoWork -= value2;
				backgroundWorker.ProgressChanged -= value3;
				backgroundWorker.RunWorkerCompleted -= value4;
			}
			field = value;
			backgroundWorker = field;
			if (backgroundWorker != null)
			{
				backgroundWorker.DoWork += value2;
				backgroundWorker.ProgressChanged += value3;
				backgroundWorker.RunWorkerCompleted += value4;
			}
		}
	}

	public ModEnums.CardType Purpose { get; set; }

	public string Mod_Folder { get; set; }

	public string ModDir { get; set; }

	public string EnabledModToken { get; set; }

	public DialogLoader()
	{
		Load += DialogLoader_Load;
		currentFolder = "";
		FilesCounted = 0L;
		CardTypePriority = 0;
		LoggedMod = "";
		LoggedStatus = ModEnums.LogType.None;
		EnabledModToken = "";
		InitializeComponent();
        DossierStyle.Apply(this);
	}

	[DebuggerNonUserCode]
	protected override void Dispose(bool disposing)
	{
		try
		{
			if (disposing && components != null)
			{
				components.Dispose();
			}
		}
		finally
		{
			base.Dispose(disposing);
		}
	}

	[System.Diagnostics.DebuggerStepThrough]
	private void InitializeComponent()
	{
		this.Loading = new System.Windows.Forms.ProgressBar();
		this.folderLabel = new System.Windows.Forms.Label();
		this.Loader = new System.ComponentModel.BackgroundWorker();
		base.SuspendLayout();
		this.Loading.Location = new System.Drawing.Point(12, 55);
		this.Loading.Name = "Loading";
		this.Loading.Size = new System.Drawing.Size(410, 31);
		this.Loading.Step = 1;
		this.Loading.TabIndex = 0;
		this.folderLabel.AutoSize = true;
		this.folderLabel.Font = new System.Drawing.Font("Impact", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.folderLabel.ForeColor = System.Drawing.SystemColors.Info;
		this.folderLabel.Location = new System.Drawing.Point(12, 32);
		this.folderLabel.Name = "folderLabel";
		this.folderLabel.Size = new System.Drawing.Size(57, 20);
		this.folderLabel.TabIndex = 1;
		this.folderLabel.Text = "loading";
		this.folderLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.Loader.WorkerReportsProgress = true;
		base.AutoScaleDimensions = new System.Drawing.SizeF(96f, 96f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
		this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
		base.ClientSize = new System.Drawing.Size(434, 111);
		base.ControlBox = false;
		base.Controls.Add(this.folderLabel);
		base.Controls.Add(this.Loading);
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "DialogLoader";
		this.Text = "Loading";
		base.ResumeLayout(false);
		base.PerformLayout();
	}

	private void DialogLoader_Load(object sender, EventArgs e)
	{
		Loading.Maximum = 100;
		ModColors.ApplyMenuColors(this);
		Loader.RunWorkerAsync();
	}

	private void Loader_DoWork(object sender, DoWorkEventArgs e)
	{
		switch (Purpose)
		{
		case ModEnums.CardType.StatCard:
			FileAmount = GetDatabaseSize(StudioModule.BaseDirectory + "/db");
			loadDatabase("base", ModEnums.SC_DIR);
			break;
		case ModEnums.CardType.MadCard:
			AddedDecks = new List<string>();
			AddedCards = new List<ModCards.CardTag>();
			FileAmount = GetDatabaseSize(ModDir);
			LoadDecks(ModDir + "/");
			break;
		case ModEnums.CardType.ModFolder:
		{
			AddedMods = new List<ModCards.ModTag>();
			AddedDecks = new List<string>();
			AddedCards = new List<ModCards.CardTag>();
			EnabledMods = new List<string>();
			FileAmount = 0L;
			if (EnabledModToken.Length > 0)
			{
				JArray jArray = JArray.Parse(EnabledModToken);
				foreach (JToken item in jArray.Children())
				{
					EnabledMods.Add((string)item);
				}
			}
			Console.WriteLine("About to log");
			WriteLogs(File.Exists(ModEnums.MOD_DIR + "\\directories.log") ? File.ReadAllText(ModEnums.MOD_DIR + "\\directories.log") : "", File.Exists(ModEnums.MOD_DIR + "\\mods.log") ? File.ReadAllText(ModEnums.MOD_DIR + "\\mods.log") : "");
			Console.WriteLine("Done logging");
			string text = MySettingsProperty.Settings.WorkshopDir + "\\" + ModEnums.WORKSHOP_DIR;
			FileAmount = checked(GetDatabaseSize(ModEnums.MOD_DIR) + (Directory.Exists(text) ? GetDatabaseSize(text) : 0));
			Console.WriteLine("ABout to read: " + ModEnums.MOD_DIR);
			LoadMods(ModEnums.MOD_DIR);
			if (Directory.Exists(text))
			{
				LoadMods(text, LoadOnly: true);
			}
			break;
		}
		}
	}

	private void Loader_ProgressChanged(object sender, ProgressChangedEventArgs e)
	{
		folderLabel.Text = currentFolder;
		Loading.Value = Math.Max(0,Math.Min(100,e.ProgressPercentage));
	}

	private void Loader_WorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
	{
        if(e.Error!=null){DossierDialog.Show("Could not load the workspace.\n"+e.Error.Message,"Loading error");DialogResult=DialogResult.No;Close();return;}
		switch (Purpose)
		{
		case ModEnums.CardType.StatCard:
			folderLabel.Text = "Loading Extra Lists";
			ModEnums.InitializeData();
			break;
		case ModEnums.CardType.MadCard:
			ModEnums.DECKADD = AddedDecks;
			ModEnums.DECKSORT = AddedCards;
			ModEnums.CURRLOG = LoggedMod;
			ModEnums.LOGSTATUS = LoggedStatus;
			break;
		case ModEnums.CardType.ModFolder:
			AddedCards.Sort([SpecialName] (ModCards.CardTag x, ModCards.CardTag y) => (Operators.CompareString(y.Type, x.Type, TextCompare: false) != 0) ? ModEnums.TypePriority[x.Type].CompareTo(ModEnums.TypePriority[y.Type]) : x.Priority.CompareTo(y.Priority));
			ModEnums.MODSORT = AddedMods;
			ModEnums.DECKADD = AddedDecks;
			ModEnums.DECKSORT = AddedCards;
			break;
		}
		DialogResult = DialogResult.Yes;
		Close();
	}

	public long GetDatabaseSize(string Dir)
	{
		return Directory.Exists(Dir)?Directory.GetFiles(Dir, "*.json", SearchOption.AllDirectories).LongLength:0;
	}

	public void loadDatabase(string folder, Dictionary<string, SortedDictionary<string, JObject>> CardDir)
	{
		LoadDirectory(folder, "Animations", CardDir);
		LoadDirectory(folder, "VoiceSet", CardDir);
		LoadDirectory(folder, "SoundPack", CardDir);
		LoadDirectory(folder, "SkillGroup", CardDir);
		LoadDirectory(folder, "Swatch", CardDir);
		LoadDirectory(folder, "Skin", CardDir);
		LoadDirectory(folder, "Chatter", CardDir);
		LoadDirectory(folder, "DoT", CardDir);
		LoadDirectory(folder, "VisualStockpile", CardDir);
		LoadDirectory(folder, "Skill", CardDir);
		LoadDirectory(folder, "AnimationProfile", CardDir);
		LoadDirectory(folder, "Corpus", CardDir);
		LoadDirectory(folder, "Execution", CardDir);
		LoadDirectory(folder, "Explosion", CardDir);
		CardDir.Add("Item", new SortedDictionary<string, JObject>(StringComparer.OrdinalIgnoreCase));
		CardDir.Add("Weapon", new SortedDictionary<string, JObject>(StringComparer.OrdinalIgnoreCase));
		LoadDirectory(folder, "Armor", CardDir, ModEnums.ItemType.Item);
		LoadDirectory(folder, "Keycard", CardDir, ModEnums.ItemType.Item);
		LoadDirectory(folder, "Melee", CardDir, ModEnums.ItemType.Weapon);
		LoadDirectory(folder, "Ammo", CardDir);
		LoadDirectory(folder, "Addon", CardDir);
		LoadDirectory(folder, "Thrown", CardDir, ModEnums.ItemType.Weapon);
		LoadDirectory(folder, "AddonGroup", CardDir);
		LoadDirectory(folder, "Ranged", CardDir, ModEnums.ItemType.Weapon);
		LoadDirectory(folder, "Character", CardDir);
		LoadDirectory(folder, "Origin", CardDir);
		LoadDirectory(folder, "Loadout", CardDir);
		LoadDirectory(folder, "StoryLoadout", CardDir, ModEnums.ItemType.Loadout);
		LoadDirectory(folder, "Wardrobe", CardDir);
		LoadDirectory(folder, "Event", CardDir);
		LoadDirectory(folder, "Cutscene", CardDir);
		LoadDirectory(folder, "Driver", CardDir);
		LoadDirectory(folder, "Mission", CardDir);
		LoadDirectory(folder, "Stage_Arena", CardDir);
		LoadDirectory(folder, "Stage_Story", CardDir);
		LoadDirectory(folder, "Stage_Custom", CardDir);
		LoadDirectory(folder, "Campaign", CardDir);
	}

	public void LoadDirectory(string folder, string Dir, Dictionary<string, SortedDictionary<string, JObject>> AllCards, ModEnums.ItemType AddToFolder = ModEnums.ItemType.None)
	{
		string path = StudioModule.BaseDirectory + "/db/" + folder + "/" + Dir + "/";
		checked
		{
			try
			{
				currentFolder = "Loading " + Dir;
				string[] files = Directory.GetFiles(path, "*.json", SearchOption.AllDirectories);
				SortedDictionary<string, JObject> sortedDictionary = new SortedDictionary<string, JObject>(StringComparer.OrdinalIgnoreCase);
				string[] array = files;
				foreach (string path2 in array)
				{
					string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(path2);
					string json = File.ReadAllText(path2);
					try
					{
						JObject value = JObject.Parse(json);
						if (!sortedDictionary.ContainsKey(fileNameWithoutExtension))
						{
							sortedDictionary.Add(fileNameWithoutExtension, value);
						}
						else
						{
							sortedDictionary[fileNameWithoutExtension] = value;
						}
						switch (AddToFolder)
						{
						case ModEnums.ItemType.Item:
							if (!AllCards["Item"].ContainsKey(fileNameWithoutExtension))
							{
								AllCards["Item"].Add(fileNameWithoutExtension, value);
							}
							break;
						case ModEnums.ItemType.Weapon:
							if (!AllCards["Item"].ContainsKey(fileNameWithoutExtension))
							{
								AllCards["Item"].Add(fileNameWithoutExtension, value);
							}
							if (!AllCards["Weapon"].ContainsKey(fileNameWithoutExtension))
							{
								AllCards["Weapon"].Add(fileNameWithoutExtension, value);
							}
							break;
						case ModEnums.ItemType.Loadout:
							if (!AllCards["Loadout"].ContainsKey(fileNameWithoutExtension))
							{
								AllCards["Loadout"].Add(fileNameWithoutExtension, value);
							}
							break;
						}
					}
					catch (JsonReaderException ex)
					{
						ProjectData.SetProjectError(ex);
						JsonReaderException ex2 = ex;
						DossierDialog.MsgBox("Error adding " + fileNameWithoutExtension + "to list:\r\n" + ex2.Message, MsgBoxStyle.Exclamation);
						ProjectData.ClearProjectError();
					}
					FilesCounted++;
					Loader.ReportProgress((int)Math.Round((double)FilesCounted / (double)Math.Max(1,FileAmount) * 100.0));
				}
				AllCards.Add(Dir, sortedDictionary);
				ModEnums.TypePriority.Add(Dir, CardTypePriority);
				CardTypePriority++;
			}
			catch (DirectoryNotFoundException ex3)
			{
				ProjectData.SetProjectError(ex3);
				DirectoryNotFoundException ex4 = ex3;
				Console.WriteLine("Directory Not Found: " + Dir);
				ProjectData.ClearProjectError();
			}
		}
	}

	public void LoadMods(string Directory, bool LoadOnly = false)
	{
		checked
		{
			try
			{
				string[] files = System.IO.Directory.GetFiles(Directory, "info.txt", SearchOption.AllDirectories);
				foreach (string text in files)
				{
					try
					{
						JObject jObject = JObject.Parse(File.ReadAllText(text));
						if (!((jObject["Name"] != null) & (jObject["Description"] != null) & (jObject["Author"] != null) & (jObject["Tags"] != null)))
						{
							continue;
						}
						string fileName = Path.GetFileName(System.IO.Directory.GetParent(text).ToString());
						string[] array = text.Split('\\', '/');
						string text2 = (array.Contains("488860") ? array[Array.IndexOf(array, "488860") + 1] : fileName);
						ModCards.ModTag modTag = new ModCards.ModTag(fileName, jObject["Name"].ToString(), jObject["Author"].ToString(), jObject["Description"].ToString(), jObject["Tags"].ToObject<string[]>(), text2, System.IO.Directory.GetParent(text).ToString(), LoadOnly);
						if (ModEnums.LogByMod.ContainsKey(fileName) & !LoadOnly)
						{
							File.WriteAllText(Directory + "\\" + fileName + "\\mobj.log", ModEnums.LogByMod[fileName].ToString());
						}
						if (!ModEnums.FolderByMod.ContainsKey(text2))
						{
							ModEnums.FolderByMod.Add(text2, modTag.DisplayName);
						}
						currentFolder = "Loading " + fileName;
						Mod_Folder = fileName;
						modTag.Enabled = EnabledMods.Contains(text2);
						AddedMods.Add(modTag);
						if (EnabledMods.Contains(text2))
						{
							LoadDecks(System.IO.Directory.GetParent(text).ToString());
							if ((LoggedMod.Length > 0) & (LoggedStatus > ModEnums.LogType.Unread))
							{
								LoadObjs(JObject.Parse(LoggedMod), modTag.DisplayName);
								LoggedMod = "";
							}
						}
						else
						{
							FilesCounted += GetDatabaseSize(System.IO.Directory.GetParent(text).ToString());
							Loader.ReportProgress((int)Math.Round((double)FilesCounted / (double)Math.Max(1,FileAmount) * 100.0));
						}
					}
					catch (JsonReaderException ex)
					{
						ProjectData.SetProjectError(ex);
						JsonReaderException ex2 = ex;
						DossierDialog.MsgBox("Error reading mod info for " + text + ":\r\n" + ex2.Message, MsgBoxStyle.Exclamation);
						ProjectData.ClearProjectError();
					}
				}
			}
			catch (DirectoryNotFoundException ex3)
			{
				ProjectData.SetProjectError(ex3);
				DirectoryNotFoundException ex4 = ex3;
				Console.WriteLine("Where the fuck?");
				ProjectData.ClearProjectError();
			}
		}
	}

	public void LoadObjs(JObject bundle, string ModName)
	{
		ModEnums.AddListOf(ModEnums.ASSETS, bundle, ModName);
	}

	public void LoadDecks(string Directory)
	{
		checked
		{
			try
			{
				string[] files = System.IO.Directory.GetFiles(Directory, "*.json", SearchOption.AllDirectories);
				foreach (string path in files)
				{
					string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(path);
					currentFolder = "Loading " + fileNameWithoutExtension;
					string str = File.ReadAllText(path);
					try
					{
						JObject deck = JObject.Parse(ModColors.Cleaned(str));
						SortDeck(fileNameWithoutExtension, deck);
					}
					catch (JsonReaderException ex)
					{
						ProjectData.SetProjectError(ex);
						JsonReaderException ex2 = ex;
						DossierDialog.MsgBox("Error adding " + fileNameWithoutExtension + " to list:\r\n" + ex2.Message, MsgBoxStyle.Exclamation);
						ProjectData.ClearProjectError();
					}
					AddedDecks.Add(fileNameWithoutExtension);
					FilesCounted++;
					Loader.ReportProgress((int)Math.Round((double)FilesCounted / (double)Math.Max(1,FileAmount) * 100.0));
				}
				string[] array = Directory.Substring(0, Directory.Length - 1).Split('\\', '/');
				string key = (array.Contains("488860") ? array[Array.IndexOf(array, "488860") + 1] : array.Last());
				if (ModEnums.LogByMod.ContainsKey(key))
				{
					LoggedMod = ModEnums.LogByMod[key].ToString();
					LoggedStatus = ModEnums.LogType.Enabled;
				}
				else if (File.Exists(Directory + "\\mobj.log"))
				{
					LoggedMod = File.ReadAllText(Directory + "\\mobj.log");
					LoggedStatus = ModEnums.LogType.Logged;
				}
				else if (System.IO.Directory.GetFiles(Directory, "*.mobj", SearchOption.AllDirectories).Count() > 0)
				{
					LoggedStatus = ModEnums.LogType.Unread;
				}
			}
			catch (DirectoryNotFoundException ex3)
			{
				ProjectData.SetProjectError(ex3);
				DirectoryNotFoundException ex4 = ex3;
				ProjectData.ClearProjectError();
			}
		}
	}

	public void SortDeck(string DeckName, JObject deck)
	{
		foreach (JProperty item in deck.Properties())
		{
			if (!(ModEnums.SC_DIR.ContainsKey(item.Name) & item.Value.HasValues))
			{
				continue;
			}
			foreach (JToken item2 in item.Value.Children())
			{
				JObject jObject = JObject.Parse(item2.ToString());
				if ((jObject["Filename"] != null) & (jObject["Root"] != null))
				{
					ModCards.CardTag cardTag = new ModCards.CardTag(jObject["Filename"].ToString(), item.Name, jObject["Root"].ToString(), jObject);
					if (jObject["Priority"] != null)
					{
						int.TryParse(jObject["Priority"].ToString(), out cardTag.Priority);
					}
					if (jObject["ConvertEnums"] != null)
					{
						bool.TryParse(jObject["ConvertEnums"].ToString(), out cardTag.Converter);
					}
					cardTag.AttachedMod = Mod_Folder;
					cardTag.AttachedDeck = DeckName;
					AddedCards.Add(cardTag);
				}
			}
		}
	}

	public void WriteLogs(string log, string ord)
	{
		ModEnums.LogByMod.Clear();
		ModEnums.AssetByMod.Clear();
		ModEnums.FolderByMod.Clear();
		ModEnums.ClearAllLists();
		ModEnums.WriteDefaultLists();
		checked
		{
			if (log.Length > 0)
			{
				Dictionary<string, Dictionary<string, List<string>>> dictionary = new Dictionary<string, Dictionary<string, List<string>>>();
				string[] array = ModColors.Logged(log);
				for (int i = 0; i < array.Length; i++)
				{
					string[] array2 = array[i].Split('|');
					string item = array2[0];
					string key = array2[1].Replace("ect - ", "_");
					string key2 = (array2[2].Contains("\\") ? array2[2].Substring(array2[2].LastIndexOf("\\") + 1) : array2[2]);
					if (!dictionary.ContainsKey(key2))
					{
						dictionary.Add(key2, new Dictionary<string, List<string>>());
					}
					if (!dictionary[key2].ContainsKey(key))
					{
						dictionary[key2].Add(key, new List<string>());
					}
					dictionary[key2][key].Add(item);
				}
				foreach (string key3 in dictionary.Keys)
				{
					JObject jObject = new JObject();
					Dictionary<string, List<string>> dictionary2 = dictionary[key3];
					foreach (string key4 in dictionary2.Keys)
					{
						JArray jArray = new JArray();
						List<string> list = dictionary2[key4];
						foreach (string item3 in list)
						{
							jArray.Add(new JValue(item3));
						}
						jObject.Add(key4, jArray);
					}
					ModEnums.LogByMod.Add(key3, jObject);
				}
			}
			else if (!MySettingsProperty.Settings.LocalWorkspace)
			{
				DossierDialog.MsgBox("Asset Logging and tracking is disabled until you run MPN again.", MsgBoxStyle.Exclamation, "Can't log yet.");
			}
			if (ord.Length <= 0)
			{
				return;
			}
			string[] array3 = ModColors.Ordinals(ord);
			for (int j = 0; j < array3.Length; j++)
			{
				string[] array4 = array3[j].Split('|');
				string item2 = array4[0];
				string text = array4[1];
				foreach (string key5 in ModEnums.ENUMS.Keys)
				{
					if (text.StartsWith(key5.Substring(0, key5.Length - 1)))
					{
						ModEnums.ENUMS[key5].Add(item2);
					}
				}
			}
		}
	}
}
