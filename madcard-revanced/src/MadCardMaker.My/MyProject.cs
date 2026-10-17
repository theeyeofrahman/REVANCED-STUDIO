using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.ApplicationServices;
using Microsoft.VisualBasic.CompilerServices;

namespace MadCardMaker.My;

[StandardModule]
[HideModuleName]
[GeneratedCode("MyTemplate", "11.0.0.0")]
internal sealed class MyProject
{
	[EditorBrowsable(EditorBrowsableState.Never)]
	[MyGroupCollection("System.Windows.Forms.Form", "Create__Instance__", "Dispose__Instance__", "My.MyProject.Forms")]
	internal sealed class MyForms
	{
		[ThreadStatic]
		private static Hashtable m_FormBeingCreated;

		[EditorBrowsable(EditorBrowsableState.Never)]
		public DialogCreateEntry m_DialogCreateEntry;

		[EditorBrowsable(EditorBrowsableState.Never)]
		public DialogLoader m_DialogLoader;

		[EditorBrowsable(EditorBrowsableState.Never)]
		public DialogModInfo m_DialogModInfo;

		[EditorBrowsable(EditorBrowsableState.Never)]
		public DialogNameEntry m_DialogNameEntry;

		[EditorBrowsable(EditorBrowsableState.Never)]
		public DialogNullReport m_DialogNullReport;

		[EditorBrowsable(EditorBrowsableState.Never)]
		public DialogTextEditor m_DialogTextEditor;

		[EditorBrowsable(EditorBrowsableState.Never)]
		public FormDeckManager m_FormDeckManager;

		[EditorBrowsable(EditorBrowsableState.Never)]
		public FormEditor m_FormEditor;

		[EditorBrowsable(EditorBrowsableState.Never)]
		public FormSettings m_FormSettings;

		[EditorBrowsable(EditorBrowsableState.Never)]
		public FormViewer m_FormViewer;

		public DialogCreateEntry DialogCreateEntry
		{
			get
			{
				m_DialogCreateEntry = Create__Instance__(m_DialogCreateEntry);
				return m_DialogCreateEntry;
			}
			set
			{
				if (value != m_DialogCreateEntry)
				{
					if (value != null)
					{
						throw new ArgumentException("Property can only be set to Nothing");
					}
					Dispose__Instance__(ref m_DialogCreateEntry);
				}
			}
		}

		public DialogLoader DialogLoader
		{
			get
			{
				m_DialogLoader = Create__Instance__(m_DialogLoader);
				return m_DialogLoader;
			}
			set
			{
				if (value != m_DialogLoader)
				{
					if (value != null)
					{
						throw new ArgumentException("Property can only be set to Nothing");
					}
					Dispose__Instance__(ref m_DialogLoader);
				}
			}
		}

		public DialogModInfo DialogModInfo
		{
			get
			{
				m_DialogModInfo = Create__Instance__(m_DialogModInfo);
				return m_DialogModInfo;
			}
			set
			{
				if (value != m_DialogModInfo)
				{
					if (value != null)
					{
						throw new ArgumentException("Property can only be set to Nothing");
					}
					Dispose__Instance__(ref m_DialogModInfo);
				}
			}
		}

		public DialogNameEntry DialogNameEntry
		{
			get
			{
				m_DialogNameEntry = Create__Instance__(m_DialogNameEntry);
				return m_DialogNameEntry;
			}
			set
			{
				if (value != m_DialogNameEntry)
				{
					if (value != null)
					{
						throw new ArgumentException("Property can only be set to Nothing");
					}
					Dispose__Instance__(ref m_DialogNameEntry);
				}
			}
		}

		public DialogNullReport DialogNullReport
		{
			get
			{
				m_DialogNullReport = Create__Instance__(m_DialogNullReport);
				return m_DialogNullReport;
			}
			set
			{
				if (value != m_DialogNullReport)
				{
					if (value != null)
					{
						throw new ArgumentException("Property can only be set to Nothing");
					}
					Dispose__Instance__(ref m_DialogNullReport);
				}
			}
		}

		public DialogTextEditor DialogTextEditor
		{
			get
			{
				m_DialogTextEditor = Create__Instance__(m_DialogTextEditor);
				return m_DialogTextEditor;
			}
			set
			{
				if (value != m_DialogTextEditor)
				{
					if (value != null)
					{
						throw new ArgumentException("Property can only be set to Nothing");
					}
					Dispose__Instance__(ref m_DialogTextEditor);
				}
			}
		}

		public FormDeckManager FormDeckManager
		{
			get
			{
				m_FormDeckManager = Create__Instance__(m_FormDeckManager);
				return m_FormDeckManager;
			}
			set
			{
				if (value != m_FormDeckManager)
				{
					if (value != null)
					{
						throw new ArgumentException("Property can only be set to Nothing");
					}
					Dispose__Instance__(ref m_FormDeckManager);
				}
			}
		}

		public FormEditor FormEditor
		{
			get
			{
				m_FormEditor = Create__Instance__(m_FormEditor);
				return m_FormEditor;
			}
			set
			{
				if (value != m_FormEditor)
				{
					if (value != null)
					{
						throw new ArgumentException("Property can only be set to Nothing");
					}
					Dispose__Instance__(ref m_FormEditor);
				}
			}
		}

		public FormSettings FormSettings
		{
			get
			{
				m_FormSettings = Create__Instance__(m_FormSettings);
				return m_FormSettings;
			}
			set
			{
				if (value != m_FormSettings)
				{
					if (value != null)
					{
						throw new ArgumentException("Property can only be set to Nothing");
					}
					Dispose__Instance__(ref m_FormSettings);
				}
			}
		}

		public FormViewer FormViewer
		{
			get
			{
				m_FormViewer = Create__Instance__(m_FormViewer);
				return m_FormViewer;
			}
			set
			{
				if (value != m_FormViewer)
				{
					if (value != null)
					{
						throw new ArgumentException("Property can only be set to Nothing");
					}
					Dispose__Instance__(ref m_FormViewer);
				}
			}
		}

		[DebuggerHidden]
		private static T Create__Instance__<T>(T Instance) where T : Form, new()
		{
			if (Instance == null || Instance.IsDisposed)
			{
				if (m_FormBeingCreated != null)
				{
					if (m_FormBeingCreated.ContainsKey(typeof(T)))
					{
						throw new InvalidOperationException(Utils.GetResourceString("WinForms_RecursiveFormCreate"));
					}
				}
				else
				{
					m_FormBeingCreated = new Hashtable();
				}
				m_FormBeingCreated.Add(typeof(T), null);
				try
				{
					return new T();
				}
				catch (TargetInvocationException ex) when (((Func<bool>)delegate
				{
					// Could not convert BlockContainer to single expression
					ProjectData.SetProjectError(ex);
					return ex.InnerException != null;
				}).Invoke())
				{
					throw new InvalidOperationException(Utils.GetResourceString("WinForms_SeeInnerException", ex.InnerException.Message), ex.InnerException);
				}
				finally
				{
					m_FormBeingCreated.Remove(typeof(T));
				}
			}
			return Instance;
		}

		[DebuggerHidden]
		private void Dispose__Instance__<T>(ref T instance) where T : Form
		{
			instance.Dispose();
			instance = null;
		}

		[DebuggerHidden]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public MyForms()
		{
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object o)
		{
			return base.Equals(RuntimeHelpers.GetObjectValue(o));
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		internal new Type GetType()
		{
			return typeof(MyForms);
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}
	}

	[EditorBrowsable(EditorBrowsableState.Never)]
	[MyGroupCollection("System.Web.Services.Protocols.SoapHttpClientProtocol", "Create__Instance__", "Dispose__Instance__", "")]
	internal sealed class MyWebServices
	{
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DebuggerHidden]
		public override bool Equals(object o)
		{
			return base.Equals(RuntimeHelpers.GetObjectValue(o));
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		[DebuggerHidden]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		[DebuggerHidden]
		internal new Type GetType()
		{
			return typeof(MyWebServices);
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		[DebuggerHidden]
		public override string ToString()
		{
			return base.ToString();
		}

		[DebuggerHidden]
		private static T Create__Instance__<T>(T instance) where T : new()
		{
			if (instance == null)
			{
				return new T();
			}
			return instance;
		}

		[DebuggerHidden]
		private void Dispose__Instance__<T>(ref T instance)
		{
			instance = default;
		}

		[DebuggerHidden]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public MyWebServices()
		{
		}
	}

	[EditorBrowsable(EditorBrowsableState.Never)]
	[ComVisible(false)]
	internal sealed class ThreadSafeObjectProvider<T> where T : new()
	{
		[CompilerGenerated]
		[ThreadStatic]
		private static T m_ThreadStaticValue;

		internal T GetInstance
		{
			[DebuggerHidden]
			get
			{
				if (m_ThreadStaticValue == null)
				{
					m_ThreadStaticValue = new T();
				}
				return m_ThreadStaticValue;
			}
		}

		[DebuggerHidden]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public ThreadSafeObjectProvider()
		{
		}
	}

	private static readonly ThreadSafeObjectProvider<MyComputer> m_ComputerObjectProvider = new ThreadSafeObjectProvider<MyComputer>();

	private static readonly ThreadSafeObjectProvider<MyApplication> m_AppObjectProvider = new ThreadSafeObjectProvider<MyApplication>();

	private static readonly ThreadSafeObjectProvider<User> m_UserObjectProvider = new ThreadSafeObjectProvider<User>();

	private static ThreadSafeObjectProvider<MyForms> m_MyFormsObjectProvider = new ThreadSafeObjectProvider<MyForms>();

	private static readonly ThreadSafeObjectProvider<MyWebServices> m_MyWebServicesObjectProvider = new ThreadSafeObjectProvider<MyWebServices>();

	[HelpKeyword("My.Computer")]
	internal static MyComputer Computer
	{
		[DebuggerHidden]
		get
		{
			return m_ComputerObjectProvider.GetInstance;
		}
	}

	[HelpKeyword("My.Application")]
	internal static MyApplication Application
	{
		[DebuggerHidden]
		get
		{
			return m_AppObjectProvider.GetInstance;
		}
	}

	[HelpKeyword("My.User")]
	internal static User User
	{
		[DebuggerHidden]
		get
		{
			return m_UserObjectProvider.GetInstance;
		}
	}

	[HelpKeyword("My.Forms")]
	internal static MyForms Forms
	{
		[DebuggerHidden]
		get
		{
			return m_MyFormsObjectProvider.GetInstance;
		}
	}

	[HelpKeyword("My.WebServices")]
	internal static MyWebServices WebServices
	{
		[DebuggerHidden]
		get
		{
			return m_MyWebServicesObjectProvider.GetInstance;
		}
	}
}
