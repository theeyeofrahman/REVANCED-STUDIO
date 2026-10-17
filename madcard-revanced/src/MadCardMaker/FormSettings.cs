using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using MadCardMaker.My;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace MadCardMaker;

[DesignerGenerated]
public class FormSettings : Form
{
	private IContainer components;

	[field: AccessedThroughProperty("Label1")]
	internal virtual Label Label1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ButtonPanel")]
	internal virtual TableLayoutPanel ButtonPanel
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Abort")]
	internal virtual Button Abort
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = Abort_Click;
			Button button = field;
			if (button != null)
			{
				button.Click -= value2;
			}
			field = value;
			button = field;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("Apply")]
	internal virtual Button Apply
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = Apply_Click;
			Button button = field;
			if (button != null)
			{
				button.Click -= value2;
			}
			field = value;
			button = field;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("GroupBox1")]
	internal virtual GroupBox GroupBox1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("RadioGlobal")]
	internal virtual RadioButton RadioGlobal
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("RadioLocal")]
	internal virtual RadioButton RadioLocal
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ManagerPanel")]
	internal virtual TableLayoutPanel ManagerPanel
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Label3")]
	internal virtual Label Label3
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("CheckAutosave")]
	internal virtual CheckBox CheckAutosave
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("CheckFullscreen")]
	internal virtual CheckBox CheckFullscreen
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Label4")]
	internal virtual Label Label4
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("CheckEditorPrice")]
	internal virtual CheckBox CheckEditorPrice
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("CheckEditorInfo")]
	internal virtual CheckBox CheckEditorInfo
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ToolTip")]
	internal virtual ToolTip ToolTip
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("WorkShopDir")]
	internal virtual TextBox WorkShopDir
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("CheckCrossEnable")]
	internal virtual CheckBox CheckCrossEnable
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	public FormSettings()
	{
		Load += FormSettings_Load;
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
		this.components = new System.ComponentModel.Container();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MadCardMaker.FormSettings));
		this.Label1 = new System.Windows.Forms.Label();
		this.ButtonPanel = new System.Windows.Forms.TableLayoutPanel();
		this.Abort = new System.Windows.Forms.Button();
		this.Apply = new System.Windows.Forms.Button();
		this.GroupBox1 = new System.Windows.Forms.GroupBox();
		this.RadioGlobal = new System.Windows.Forms.RadioButton();
		this.RadioLocal = new System.Windows.Forms.RadioButton();
		this.ManagerPanel = new System.Windows.Forms.TableLayoutPanel();
		this.Label3 = new System.Windows.Forms.Label();
		this.CheckFullscreen = new System.Windows.Forms.CheckBox();
		this.Label4 = new System.Windows.Forms.Label();
		this.CheckEditorPrice = new System.Windows.Forms.CheckBox();
		this.CheckEditorInfo = new System.Windows.Forms.CheckBox();
		this.CheckAutosave = new System.Windows.Forms.CheckBox();
		this.ToolTip = new System.Windows.Forms.ToolTip(this.components);
		this.WorkShopDir = new System.Windows.Forms.TextBox();
		this.CheckCrossEnable = new System.Windows.Forms.CheckBox();
		this.ButtonPanel.SuspendLayout();
		this.GroupBox1.SuspendLayout();
		this.ManagerPanel.SuspendLayout();
		base.SuspendLayout();
		this.Label1.AutoSize = true;
		this.Label1.Dock = System.Windows.Forms.DockStyle.Top;
		this.Label1.Font = new System.Drawing.Font("Impact", 18f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.Label1.Location = new System.Drawing.Point(0, 0);
		this.Label1.Name = "Label1";
		this.Label1.Size = new System.Drawing.Size(93, 29);
		this.Label1.TabIndex = 0;
		this.Label1.Text = "Settings";
		this.ButtonPanel.ColumnCount = 2;
		this.ButtonPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50f));
		this.ButtonPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50f));
		this.ButtonPanel.Controls.Add(this.Abort, 1, 0);
		this.ButtonPanel.Controls.Add(this.Apply, 0, 0);
		this.ButtonPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.ButtonPanel.Location = new System.Drawing.Point(0, 285);
		this.ButtonPanel.Name = "ButtonPanel";
		this.ButtonPanel.RowCount = 1;
		this.ButtonPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50f));
		this.ButtonPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50f));
		this.ButtonPanel.Size = new System.Drawing.Size(424, 46);
		this.ButtonPanel.TabIndex = 4;
		this.Abort.BackColor = System.Drawing.Color.Black;
		this.Abort.Dock = System.Windows.Forms.DockStyle.Fill;
		this.Abort.ForeColor = System.Drawing.Color.Salmon;
		this.Abort.Location = new System.Drawing.Point(215, 3);
		this.Abort.Name = "Abort";
		this.Abort.Size = new System.Drawing.Size(206, 40);
		this.Abort.TabIndex = 1;
		this.Abort.Text = "Abort";
		this.Abort.UseVisualStyleBackColor = false;
		this.Apply.BackColor = System.Drawing.Color.Black;
		this.Apply.Dock = System.Windows.Forms.DockStyle.Fill;
		this.Apply.ForeColor = System.Drawing.Color.PaleGreen;
		this.Apply.Location = new System.Drawing.Point(3, 3);
		this.Apply.Name = "Apply";
		this.Apply.Size = new System.Drawing.Size(206, 40);
		this.Apply.TabIndex = 0;
		this.Apply.Text = "Apply";
		this.Apply.UseVisualStyleBackColor = false;
		this.GroupBox1.Controls.Add(this.RadioGlobal);
		this.GroupBox1.Controls.Add(this.RadioLocal);
		this.GroupBox1.Dock = System.Windows.Forms.DockStyle.Top;
		this.GroupBox1.ForeColor = System.Drawing.Color.DarkGray;
		this.GroupBox1.Location = new System.Drawing.Point(0, 29);
		this.GroupBox1.Name = "GroupBox1";
		this.GroupBox1.Size = new System.Drawing.Size(424, 96);
		this.GroupBox1.TabIndex = 6;
		this.GroupBox1.TabStop = false;
		this.GroupBox1.Text = "Mod Directory";
		this.RadioGlobal.AutoSize = true;
		this.RadioGlobal.Dock = System.Windows.Forms.DockStyle.Top;
		this.RadioGlobal.ForeColor = System.Drawing.Color.White;
		this.RadioGlobal.Location = new System.Drawing.Point(3, 54);
		this.RadioGlobal.Name = "RadioGlobal";
		this.RadioGlobal.Size = new System.Drawing.Size(418, 27);
		this.RadioGlobal.TabIndex = 1;
		this.RadioGlobal.TabStop = true;
		this.RadioGlobal.Text = "MPN";
		this.RadioGlobal.UseVisualStyleBackColor = true;
		this.RadioLocal.AutoSize = true;
		this.RadioLocal.Dock = System.Windows.Forms.DockStyle.Top;
		this.RadioLocal.ForeColor = System.Drawing.Color.White;
		this.RadioLocal.Location = new System.Drawing.Point(3, 27);
		this.RadioLocal.Name = "RadioLocal";
		this.RadioLocal.Size = new System.Drawing.Size(418, 27);
		this.RadioLocal.TabIndex = 0;
		this.RadioLocal.TabStop = true;
		this.RadioLocal.Text = "Local";
		this.RadioLocal.UseVisualStyleBackColor = true;
		this.ManagerPanel.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
		this.ManagerPanel.ColumnCount = 2;
		this.ManagerPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50f));
		this.ManagerPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50f));
		this.ManagerPanel.Controls.Add(this.Label3, 0, 0);
		this.ManagerPanel.Controls.Add(this.CheckFullscreen, 0, 1);
		this.ManagerPanel.Controls.Add(this.Label4, 0, 2);
		this.ManagerPanel.Controls.Add(this.CheckEditorPrice, 0, 3);
		this.ManagerPanel.Controls.Add(this.CheckEditorInfo, 1, 3);
		this.ManagerPanel.Controls.Add(this.CheckAutosave, 1, 1);
		this.ManagerPanel.Controls.Add(this.CheckCrossEnable, 1, 0);
		this.ManagerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
		this.ManagerPanel.Location = new System.Drawing.Point(0, 125);
		this.ManagerPanel.Name = "ManagerPanel";
		this.ManagerPanel.RowCount = 4;
		this.ManagerPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25f));
		this.ManagerPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25f));
		this.ManagerPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25f));
		this.ManagerPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25f));
		this.ManagerPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.ManagerPanel.Size = new System.Drawing.Size(424, 129);
		this.ManagerPanel.TabIndex = 7;
		this.Label3.AutoSize = true;
		this.Label3.ForeColor = System.Drawing.Color.DarkGray;
		this.Label3.Location = new System.Drawing.Point(4, 1);
		this.Label3.Name = "Label3";
		this.Label3.Size = new System.Drawing.Size(150, 23);
		this.Label3.TabIndex = 0;
		this.Label3.Text = "Manager Settings";
		this.CheckFullscreen.AutoSize = true;
		this.CheckFullscreen.Location = new System.Drawing.Point(4, 36);
		this.CheckFullscreen.Name = "CheckFullscreen";
		this.CheckFullscreen.Size = new System.Drawing.Size(177, 25);
		this.CheckFullscreen.TabIndex = 2;
		this.CheckFullscreen.Text = "Fullscreen on Start";
		this.CheckFullscreen.UseVisualStyleBackColor = true;
		this.Label4.AutoSize = true;
		this.Label4.ForeColor = System.Drawing.Color.DarkGray;
		this.Label4.Location = new System.Drawing.Point(4, 65);
		this.Label4.Name = "Label4";
		this.Label4.Size = new System.Drawing.Size(125, 23);
		this.Label4.TabIndex = 5;
		this.Label4.Text = "Editor Settings";
		this.CheckEditorPrice.AutoSize = true;
		this.CheckEditorPrice.Location = new System.Drawing.Point(4, 100);
		this.CheckEditorPrice.Name = "CheckEditorPrice";
		this.CheckEditorPrice.Size = new System.Drawing.Size(174, 25);
		this.CheckEditorPrice.TabIndex = 6;
		this.CheckEditorPrice.Text = "Always Show Price";
		this.CheckEditorPrice.UseVisualStyleBackColor = true;
		this.CheckEditorInfo.AutoSize = true;
		this.CheckEditorInfo.Location = new System.Drawing.Point(215, 100);
		this.CheckEditorInfo.Name = "CheckEditorInfo";
		this.CheckEditorInfo.Size = new System.Drawing.Size(164, 25);
		this.CheckEditorInfo.TabIndex = 7;
		this.CheckEditorInfo.Text = "Always Show Info";
		this.CheckEditorInfo.UseVisualStyleBackColor = true;
		this.CheckAutosave.AutoSize = true;
		this.CheckAutosave.Location = new System.Drawing.Point(215, 36);
		this.CheckAutosave.Name = "CheckAutosave";
		this.CheckAutosave.Size = new System.Drawing.Size(100, 25);
		this.CheckAutosave.TabIndex = 1;
		this.CheckAutosave.Text = "Autosave";
		this.CheckAutosave.UseVisualStyleBackColor = true;
		this.ToolTip.BackColor = System.Drawing.Color.Black;
		this.ToolTip.ForeColor = System.Drawing.Color.Salmon;
		this.WorkShopDir.BackColor = System.Drawing.Color.Black;
		this.WorkShopDir.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.WorkShopDir.ForeColor = System.Drawing.Color.White;
		this.WorkShopDir.Location = new System.Drawing.Point(0, 254);
		this.WorkShopDir.Name = "WorkShopDir";
		this.WorkShopDir.Size = new System.Drawing.Size(424, 31);
		this.WorkShopDir.TabIndex = 8;
		this.WorkShopDir.Text = "Specify SteamApps Folder";
		this.CheckCrossEnable.AutoSize = true;
		this.CheckCrossEnable.Location = new System.Drawing.Point(215, 4);
		this.CheckCrossEnable.Name = "CheckCrossEnable";
		this.CheckCrossEnable.Size = new System.Drawing.Size(146, 25);
		this.CheckCrossEnable.TabIndex = 9;
		this.CheckCrossEnable.Text = "Cross Enabling";
		this.CheckCrossEnable.UseVisualStyleBackColor = true;
		base.AutoScaleDimensions = new System.Drawing.SizeF(96f, 96f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
		this.BackColor = System.Drawing.Color.FromArgb(64, 64, 64);
		base.ClientSize = new System.Drawing.Size(424, 331);
		base.Controls.Add(this.ManagerPanel);
		base.Controls.Add(this.WorkShopDir);
		base.Controls.Add(this.GroupBox1);
		base.Controls.Add(this.ButtonPanel);
		base.Controls.Add(this.Label1);
		this.Font = new System.Drawing.Font("Impact", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.ForeColor = System.Drawing.Color.White;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
		base.Name = "FormSettings";
		this.Text = "Settings";
		this.ButtonPanel.ResumeLayout(false);
		this.GroupBox1.ResumeLayout(false);
		this.GroupBox1.PerformLayout();
		this.ManagerPanel.ResumeLayout(false);
		this.ManagerPanel.PerformLayout();
		base.ResumeLayout(false);
		base.PerformLayout();
	}

	private void FormSettings_Load(object sender, EventArgs e)
	{
		CheckFullscreen.Checked = MySettingsProperty.Settings.Fullscreen;
		CheckEditorInfo.Checked = MySettingsProperty.Settings.ShowInfo;
		CheckEditorPrice.Checked = MySettingsProperty.Settings.ShowPrice;
		CheckAutosave.Checked = MySettingsProperty.Settings.Autosave;
		CheckCrossEnable.Checked = MySettingsProperty.Settings.CrossEnable;
		RadioLocal.Checked = MySettingsProperty.Settings.LocalWorkspace;
		RadioGlobal.Checked = !MySettingsProperty.Settings.LocalWorkspace;
		WorkShopDir.Text = (Directory.Exists(MySettingsProperty.Settings.WorkshopDir + "/" + ModEnums.WORKSHOP_DIR) ? MySettingsProperty.Settings.WorkshopDir : "Specify SteamApps Folder");
		RadioGlobal.Enabled = Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)) + "Low/Gibbing Tree/Madness Project Nexus/Mods/");
		ToolTip.SetToolTip(RadioLocal, "New mods will be created in local app directory.\r\n(Mods will need to be manually transferred to game)");
		ToolTip.SetToolTip(RadioGlobal, "New mods will be created in game directory and automatically loaded on game startup.\r\n(Game must be installed and run at least once)");
		ToolTip.SetToolTip(CheckFullscreen, "Editor will start maximized");
		ToolTip.SetToolTip(CheckAutosave, "Deck Manager will automatically write all decks when adding/editing/removing new decks/cards");
		ToolTip.SetToolTip(CheckCrossEnable, "list of enabled/disabled mods will be carried over to your ingame presets");
		ToolTip.SetToolTip(CheckEditorInfo, "Card Editor will have info tab visible on startup");
		ToolTip.SetToolTip(CheckEditorPrice, "Card Editor will have price tab visible on startup");
	}

	private void Apply_Click(object sender, EventArgs e)
	{
		MySettingsProperty.Settings.Fullscreen = CheckFullscreen.Checked;
		MySettingsProperty.Settings.ShowInfo = CheckEditorInfo.Checked;
		MySettingsProperty.Settings.ShowPrice = CheckEditorPrice.Checked;
		MySettingsProperty.Settings.Autosave = CheckAutosave.Checked;
		MySettingsProperty.Settings.CrossEnable = CheckCrossEnable.Checked;
		if ((MySettingsProperty.Settings.LocalWorkspace != RadioLocal.Checked) | (Operators.CompareString(MySettingsProperty.Settings.WorkshopDir, WorkShopDir.Text, TextCompare: false) != 0))
		{
			DossierDialog.MsgBox("Your workspace will change upon resetting.", MsgBoxStyle.Information, "Restart needed");
		}
		MySettingsProperty.Settings.WorkshopDir = WorkShopDir.Text;
		MySettingsProperty.Settings.LocalWorkspace = RadioLocal.Checked;
		Close();
	}

	private void Abort_Click(object sender, EventArgs e)
	{
		Close();
	}
}
