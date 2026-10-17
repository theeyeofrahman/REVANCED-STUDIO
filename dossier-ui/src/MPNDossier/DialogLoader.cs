using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using MPNDossier.My;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MPNDossier;

[DesignerGenerated]
public class DialogLoader : Form
{
	private IContainer components;

	[AccessedThroughProperty("Loading")]
	private ProgressBar _Loading;

	[AccessedThroughProperty("folderLabel")]
	private Label _folderLabel;

	[AccessedThroughProperty("Loader")]
	private BackgroundWorker _Loader;

	private string currentFolder;

	private long FileAmount;

	private long FilesCounted;

	internal virtual ProgressBar Loading
	{
		get
		{
			return _Loading;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			_Loading = value;
		}
	}

	internal virtual Label folderLabel
	{
		get
		{
			return _folderLabel;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			_folderLabel = value;
		}
	}

	internal virtual BackgroundWorker Loader
	{
		get
		{
			return _Loader;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			ProgressChangedEventHandler value2 = Loader_ProgressChanged;
			DoWorkEventHandler value3 = Loader_DoWork;
			RunWorkerCompletedEventHandler value4 = Loader_WorkerCompleted;
			if (_Loader != null)
			{
				_Loader.ProgressChanged -= value2;
				_Loader.DoWork -= value3;
				_Loader.RunWorkerCompleted -= value4;
			}
			_Loader = value;
			if (_Loader != null)
			{
				_Loader.ProgressChanged += value2;
				_Loader.DoWork += value3;
				_Loader.RunWorkerCompleted += value4;
			}
		}
	}

	public DialogLoader()
	{
		Load += DialogLoader_Load;
		currentFolder = "";
		FilesCounted = 0L;
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
		this.SuspendLayout();
		System.Windows.Forms.ProgressBar loading = this.Loading;
		System.Drawing.Point location = new System.Drawing.Point(12, 55);
		loading.Location = location;
		this.Loading.Name = "Loading";
		System.Windows.Forms.ProgressBar loading2 = this.Loading;
		System.Drawing.Size size = new System.Drawing.Size(410, 31);
		loading2.Size = size;
		this.Loading.Step = 1;
		this.Loading.TabIndex = 0;
		this.folderLabel.AutoSize = true;
		this.folderLabel.Font = new System.Drawing.Font("Impact", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.folderLabel.ForeColor = System.Drawing.SystemColors.Info;
		System.Windows.Forms.Label label = this.folderLabel;
		location = new System.Drawing.Point(12, 32);
		label.Location = location;
		this.folderLabel.Name = "folderLabel";
		System.Windows.Forms.Label label2 = this.folderLabel;
		size = new System.Drawing.Size(57, 20);
		label2.Size = size;
		this.folderLabel.TabIndex = 1;
		this.folderLabel.Text = "loading";
		this.folderLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.Loader.WorkerReportsProgress = true;
		System.Drawing.SizeF sizeF = new System.Drawing.SizeF(6f, 13f);
		this.AutoScaleDimensions = sizeF;
		this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
		size = new System.Drawing.Size(434, 111);
		this.ClientSize = size;
		this.Controls.Add(this.folderLabel);
		this.Controls.Add(this.Loading);
		this.MaximizeBox = false;
		this.MinimizeBox = false;
		this.Name = "DialogLoader";
		this.Text = "Loading";
		this.ResumeLayout(false);
		this.PerformLayout();
	}

	private void DialogLoader_Load(object sender, EventArgs e)
	{
		Loading.Maximum = 100;
		FileAmount = GetDatabaseSize();
		Loader.RunWorkerAsync();
	}

	[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
	private void Loader_DoWork(object sender, DoWorkEventArgs e)
	{
		loadDatabase("base");
		if (Directory.Exists(StudioModule.BaseDirectory + "/db/NEM/"))
		{
			loadDatabase("nem");
		}
		if (Directory.Exists(StudioModule.BaseDirectory + "/db/new/"))
		{
			loadDatabase("new");
		}
	}

	private void Loader_ProgressChanged(object sender, ProgressChangedEventArgs e)
	{
		folderLabel.Text = currentFolder;
		Loading.Value = Math.Max(Loading.Minimum, Math.Min(Loading.Maximum, e.ProgressPercentage));
	}

	private void Loader_WorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
	{
		if(e.Error!=null){DossierDialog.Show("The database could not be loaded.\n"+e.Error.Message,"Database error",MessageBoxButtons.OK,MessageBoxIcon.Error);DialogResult=DialogResult.No;Close();return;}
		folderLabel.Text = "Loading Extra Lists";
		try{ModEnums.InitializeData();}
        catch(Exception ex){DossierDialog.Show("Database initialization failed.\n"+ex.Message,"Database error",MessageBoxButtons.OK,MessageBoxIcon.Error);DialogResult=DialogResult.No;Close();return;}
		DialogResult = DialogResult.Yes;
		Close();
	}

	[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
	public long GetDatabaseSize()
	{
		return Directory.GetFiles(StudioModule.BaseDirectory + "/db/", "*.json", SearchOption.AllDirectories).Count();
	}

	public void loadDatabase(string folder)
	{
		LoadDirectory("/db/" + folder + "/animsets/", ref ModEnums.DB_ANIMSETS);
		LoadDirectory("/db/" + folder + "/animation/", ref ModEnums.DB_ANIMATIONS);
		LoadDirectory("/db/" + folder + "/arenas/", ref ModEnums.DB_ARENAS);
		LoadDirectory("/db/" + folder + "/armor/", ref ModEnums.DB_GEAR);
		LoadDirectory("/db/" + folder + "/chatter/", ref ModEnums.DB_CHATTER);
		LoadDirectory("/db/" + folder + "/corpus/", ref ModEnums.DB_CORPUS);
		LoadDirectory("/db/" + folder + "/characters/", ref ModEnums.DB_CHAR);
		LoadDirectory("/db/" + folder + "/collectibles/", ref ModEnums.DB_COLLECTIBLES);
		LoadDirectory("/db/" + folder + "/dot/", ref ModEnums.DB_DOT);
		LoadDirectory("/db/" + folder + "/executions/", ref ModEnums.DB_EXECUTION);
		LoadDirectory("/db/" + folder + "/explosions/", ref ModEnums.DB_EXPLOSION);
		LoadDirectory("/db/" + folder + "/loadouts/", ref ModEnums.DB_LOADOUTS);
		LoadDirectory("/db/" + folder + "/storyloadouts/", ref ModEnums.DB_STORYLOADOUTS, AddsToWeapons: false, AddsToLoadouts: true);
		LoadDirectory("/db/" + folder + "/wardrobe/", ref ModEnums.DB_WARDROBE);
		LoadDirectory("/db/" + folder + "/swatches/", ref ModEnums.DB_SWATCHES);
		LoadDirectory("/db/" + folder + "/origins/", ref ModEnums.DB_ORIGINS);
		LoadDirectory("/db/" + folder + "/skins/", ref ModEnums.DB_SKINS);
		LoadDirectory("/db/" + folder + "/skills/", ref ModEnums.DB_SKILLS);
		LoadDirectory("/db/" + folder + "/skilltrees/", ref ModEnums.DB_SKILLTREES);
		LoadDirectory("/db/" + folder + "/skillgroups/", ref ModEnums.DB_SKILLGROUPS);
		LoadDirectory("/db/" + folder + "/soundpacks/", ref ModEnums.DB_SOUNDPACKS);
		LoadDirectory("/db/" + folder + "/stories/", ref ModEnums.DB_STORIES);
		LoadDirectory("/db/" + folder + "/textures/", ref ModEnums.DB_TEXTURES);
		LoadDirectory("/db/" + folder + "/voicesets/", ref ModEnums.DB_VOICESETS);
		LoadDirectory("/db/" + folder + "/weapons/", ref ModEnums.DB_BASEWEAPONS, AddsToWeapons: true);
		LoadDirectory("/db/" + folder + "/weaponaddons/", ref ModEnums.DB_WEAPONADDONS);
		LoadDirectory("/db/" + folder + "/weaponaddongroups/", ref ModEnums.DB_ADDONGROUPS);
		LoadDirectory("/db/" + folder + "/ammo/", ref ModEnums.DB_AMMO);
		LoadDirectory("/db/" + folder + "/ranged/", ref ModEnums.DB_RANGED, AddsToWeapons: true);
		LoadDirectory("/db/" + folder + "/thrown/", ref ModEnums.DB_THROWN, AddsToWeapons: true);
		LoadDirectory("/db/" + folder + "/weaponmodded/", ref ModEnums.DB_MODDEDWEAPONS, AddsToWeapons: true);
	}

	[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
	public void LoadDirectory(string Dir, ref Dictionary<string, JObject> Database, bool AddsToWeapons = false, bool AddsToLoadouts = false)
	{
		string path = StudioModule.BaseDirectory + Dir;
		checked
		{
			try
			{
				currentFolder = "Loading " + Dir;
				string[] files = Directory.GetFiles(path, "*.json", SearchOption.AllDirectories);
				string[] array = files;
				foreach (string path2 in array)
				{
					string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(path2);
					string json = File.ReadAllText(path2);
					try
					{
						JObject value = JObject.Parse(json);
						if (!Database.ContainsKey(fileNameWithoutExtension))
						{
							Database.Add(fileNameWithoutExtension, value);
						}
						else
						{
							Database[fileNameWithoutExtension] = value;
						}
						if (AddsToWeapons)
						{
							if (ModEnums.DB_WEAPONS.ContainsKey(fileNameWithoutExtension))
							{
								ModEnums.DB_WEAPONS[fileNameWithoutExtension] = value;
							}
							else
							{
								ModEnums.DB_WEAPONS.Add(fileNameWithoutExtension, value);
							}
						}
						if (AddsToLoadouts)
						{
							if (ModEnums.DB_LOADOUTS.ContainsKey(fileNameWithoutExtension))
							{
								ModEnums.DB_LOADOUTS[fileNameWithoutExtension] = value;
							}
							else
							{
								ModEnums.DB_LOADOUTS.Add(fileNameWithoutExtension, value);
							}
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
					Loader.ReportProgress((int)Math.Round(Math.Round((double)FilesCounted / (double)FileAmount * 100.0)));
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
}
