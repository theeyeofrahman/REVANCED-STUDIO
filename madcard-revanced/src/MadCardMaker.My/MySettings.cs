using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.VisualBasic.CompilerServices;

namespace MadCardMaker.My;

[CompilerGenerated]
[GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "17.9.0.0")]
[EditorBrowsable(EditorBrowsableState.Advanced)]
internal sealed class MySettings : ApplicationSettingsBase
{
	private static MySettings defaultInstance = (MySettings)SettingsBase.Synchronized(new MySettings());

	private static bool addedHandler;

	private static object addedHandlerLockObject = RuntimeHelpers.GetObjectValue(new object());

	public static MySettings Default
	{
		get
		{
			if (!addedHandler)
			{
				object obj = addedHandlerLockObject;
				ObjectFlowControl.CheckForSyncLockOnValueType(obj);
				bool lockTaken = false;
				try
				{
					Monitor.Enter(obj, ref lockTaken);
					if (!addedHandler)
					{
						MyProject.Application.Shutdown += AutoSaveSettings;
						addedHandler = true;
					}
				}
				finally
				{
					if (lockTaken)
					{
						Monitor.Exit(obj);
					}
				}
			}
			return defaultInstance;
		}
	}

	[UserScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("True")]
	public bool Autosave
	{
		get
		{
			return Conversions.ToBoolean(this["Autosave"]);
		}
		set
		{
			this["Autosave"] = value;
		}
	}

	[UserScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("True")]
	public bool LocalWorkspace
	{
		get
		{
			return Conversions.ToBoolean(this["LocalWorkspace"]);
		}
		set
		{
			this["LocalWorkspace"] = value;
		}
	}

	[UserScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("False")]
	public bool Fullscreen
	{
		get
		{
			return Conversions.ToBoolean(this["Fullscreen"]);
		}
		set
		{
			this["Fullscreen"] = value;
		}
	}

	[UserScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("True")]
	public bool ShowPrice
	{
		get
		{
			return Conversions.ToBoolean(this["ShowPrice"]);
		}
		set
		{
			this["ShowPrice"] = value;
		}
	}

	[UserScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("True")]
	public bool ShowInfo
	{
		get
		{
			return Conversions.ToBoolean(this["ShowInfo"]);
		}
		set
		{
			this["ShowInfo"] = value;
		}
	}

	[UserScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("")]
	public string Directory
	{
		get
		{
			return Conversions.ToString(this["Directory"]);
		}
		set
		{
			this["Directory"] = value;
		}
	}

	[UserScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("Default")]
	public string Colors
	{
		get
		{
			return Conversions.ToString(this["Colors"]);
		}
		set
		{
			this["Colors"] = value;
		}
	}

	[UserScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("")]
	public string WorkshopDir
	{
		get
		{
			return Conversions.ToString(this["WorkshopDir"]);
		}
		set
		{
			this["WorkshopDir"] = value;
		}
	}

	[UserScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("False")]
	public bool CrossEnable
	{
		get
		{
			return Conversions.ToBoolean(this["CrossEnable"]);
		}
		set
		{
			this["CrossEnable"] = value;
		}
	}

	[DebuggerNonUserCode]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	private static void AutoSaveSettings(object sender, EventArgs e)
	{
		if (MyProject.Application.SaveMySettingsOnExit)
		{
			MySettingsProperty.Settings.Save();
		}
	}
}
