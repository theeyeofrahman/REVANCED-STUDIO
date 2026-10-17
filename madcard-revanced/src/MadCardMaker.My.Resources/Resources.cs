using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace MadCardMaker.My.Resources;

[StandardModule]
[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
[DebuggerNonUserCode]
[CompilerGenerated]
[HideModuleName]
internal sealed class Resources
{
	private static ResourceManager resourceMan;

	private static CultureInfo resourceCulture;

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	internal static ResourceManager ResourceManager
	{
		get
		{
			if (object.ReferenceEquals(resourceMan, null))
			{
				resourceMan = new ResourceManager("MadCardMaker.Resources", typeof(Resources).Assembly);
			}
			return resourceMan;
		}
	}

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	internal static CultureInfo Culture
	{
		get
		{
			return resourceCulture;
		}
		set
		{
			resourceCulture = value;
		}
	}

	internal static Bitmap DM_AddCard => (Bitmap)RuntimeHelpers.GetObjectValue(ResourceManager.GetObject("DM_AddCard", resourceCulture));

	internal static Bitmap DM_AddDeck => (Bitmap)RuntimeHelpers.GetObjectValue(ResourceManager.GetObject("DM_AddDeck", resourceCulture));

	internal static Bitmap DM_Delete => (Bitmap)RuntimeHelpers.GetObjectValue(ResourceManager.GetObject("DM_Delete", resourceCulture));

	internal static Bitmap DM_Edit => (Bitmap)RuntimeHelpers.GetObjectValue(ResourceManager.GetObject("DM_Edit", resourceCulture));

	internal static Bitmap DM_PriorityDown => (Bitmap)RuntimeHelpers.GetObjectValue(ResourceManager.GetObject("DM_PriorityDown", resourceCulture));

	internal static Bitmap DM_PriorityUp => (Bitmap)RuntimeHelpers.GetObjectValue(ResourceManager.GetObject("DM_PriorityUp", resourceCulture));

	internal static Bitmap DM_Refresh => (Bitmap)RuntimeHelpers.GetObjectValue(ResourceManager.GetObject("DM_Refresh", resourceCulture));

	internal static Bitmap DM_Remove => (Bitmap)RuntimeHelpers.GetObjectValue(ResourceManager.GetObject("DM_Remove", resourceCulture));

	internal static Bitmap DM_Rename => (Bitmap)RuntimeHelpers.GetObjectValue(ResourceManager.GetObject("DM_Rename", resourceCulture));

	internal static Bitmap DM_Restore => (Bitmap)RuntimeHelpers.GetObjectValue(ResourceManager.GetObject("DM_Restore", resourceCulture));

	internal static Bitmap DM_Return => (Bitmap)RuntimeHelpers.GetObjectValue(ResourceManager.GetObject("DM_Return", resourceCulture));

	internal static Bitmap DM_Root => (Bitmap)RuntimeHelpers.GetObjectValue(ResourceManager.GetObject("DM_Root", resourceCulture));

	internal static Bitmap DM_Save => (Bitmap)RuntimeHelpers.GetObjectValue(ResourceManager.GetObject("DM_Save", resourceCulture));

	internal static Bitmap DM_ShiftBottom => (Bitmap)RuntimeHelpers.GetObjectValue(ResourceManager.GetObject("DM_ShiftBottom", resourceCulture));

	internal static Bitmap DM_ShiftDown => (Bitmap)RuntimeHelpers.GetObjectValue(ResourceManager.GetObject("DM_ShiftDown", resourceCulture));

	internal static Bitmap DM_ShiftTop => (Bitmap)RuntimeHelpers.GetObjectValue(ResourceManager.GetObject("DM_ShiftTop", resourceCulture));

	internal static Bitmap DM_ShiftUp => (Bitmap)RuntimeHelpers.GetObjectValue(ResourceManager.GetObject("DM_ShiftUp", resourceCulture));

	internal static Bitmap DM_Sort => (Bitmap)RuntimeHelpers.GetObjectValue(ResourceManager.GetObject("DM_Sort", resourceCulture));

	internal static Bitmap DM_Unlist => (Bitmap)RuntimeHelpers.GetObjectValue(ResourceManager.GetObject("DM_Unlist", resourceCulture));

	internal static Bitmap DM_View => (Bitmap)RuntimeHelpers.GetObjectValue(ResourceManager.GetObject("DM_View", resourceCulture));

	internal static Icon Maker => (Icon)RuntimeHelpers.GetObjectValue(ResourceManager.GetObject("Maker", resourceCulture));
}
