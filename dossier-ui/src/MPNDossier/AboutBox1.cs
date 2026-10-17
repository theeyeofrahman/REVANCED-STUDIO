using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using MPNDossier.My;
using Microsoft.VisualBasic.CompilerServices;

namespace MPNDossier;

[DesignerGenerated]
public class AboutBox1 : Form
{
	[AccessedThroughProperty("TableLayoutPanel")]
	private TableLayoutPanel _TableLayoutPanel;

	[AccessedThroughProperty("LogoPictureBox")]
	private PictureBox _LogoPictureBox;

	[AccessedThroughProperty("LabelProductName")]
	private Label _LabelProductName;

	[AccessedThroughProperty("LabelVersion")]
	private Label _LabelVersion;

	[AccessedThroughProperty("LabelCompanyName")]
	private Label _LabelCompanyName;

	[AccessedThroughProperty("TextBoxDescription")]
	private TextBox _TextBoxDescription;

	[AccessedThroughProperty("OKButton")]
	private Button _OKButton;

	[AccessedThroughProperty("LabelCopyright")]
	private Label _LabelCopyright;

	private IContainer components;

	internal virtual TableLayoutPanel TableLayoutPanel
	{
		get
		{
			return _TableLayoutPanel;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			_TableLayoutPanel = value;
		}
	}

	internal virtual PictureBox LogoPictureBox
	{
		get
		{
			return _LogoPictureBox;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			_LogoPictureBox = value;
		}
	}

	internal virtual Label LabelProductName
	{
		get
		{
			return _LabelProductName;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			_LabelProductName = value;
		}
	}

	internal virtual Label LabelVersion
	{
		get
		{
			return _LabelVersion;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			_LabelVersion = value;
		}
	}

	internal virtual Label LabelCompanyName
	{
		get
		{
			return _LabelCompanyName;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			_LabelCompanyName = value;
		}
	}

	internal virtual TextBox TextBoxDescription
	{
		get
		{
			return _TextBoxDescription;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			_TextBoxDescription = value;
		}
	}

	internal virtual Button OKButton
	{
		get
		{
			return _OKButton;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			EventHandler value2 = OKButton_Click;
			if (_OKButton != null)
			{
				_OKButton.Click -= value2;
			}
			_OKButton = value;
			if (_OKButton != null)
			{
				_OKButton.Click += value2;
			}
		}
	}

	internal virtual Label LabelCopyright
	{
		get
		{
			return _LabelCopyright;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			_LabelCopyright = value;
		}
	}

	public AboutBox1()
	{
		Load += AboutBox1_Load;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MPNDossier.AboutBox1));
		this.TableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
		this.LogoPictureBox = new System.Windows.Forms.PictureBox();
		this.LabelProductName = new System.Windows.Forms.Label();
		this.LabelVersion = new System.Windows.Forms.Label();
		this.LabelCopyright = new System.Windows.Forms.Label();
		this.LabelCompanyName = new System.Windows.Forms.Label();
		this.TextBoxDescription = new System.Windows.Forms.TextBox();
		this.OKButton = new DossierButton();
		this.TableLayoutPanel.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.LogoPictureBox).BeginInit();
		this.SuspendLayout();
		this.TableLayoutPanel.ColumnCount = 2;
		this.TableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33f));
		this.TableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 67f));
		this.TableLayoutPanel.Controls.Add(this.LogoPictureBox, 0, 0);
		this.TableLayoutPanel.Controls.Add(this.LabelProductName, 1, 0);
		this.TableLayoutPanel.Controls.Add(this.LabelVersion, 1, 1);
		this.TableLayoutPanel.Controls.Add(this.LabelCopyright, 1, 2);
		this.TableLayoutPanel.Controls.Add(this.LabelCompanyName, 1, 3);
		this.TableLayoutPanel.Controls.Add(this.TextBoxDescription, 1, 4);
		this.TableLayoutPanel.Controls.Add(this.OKButton, 1, 5);
		this.TableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
		System.Windows.Forms.TableLayoutPanel tableLayoutPanel = this.TableLayoutPanel;
		System.Drawing.Point location = new System.Drawing.Point(9, 9);
		tableLayoutPanel.Location = location;
		this.TableLayoutPanel.Name = "TableLayoutPanel";
		this.TableLayoutPanel.RowCount = 6;
		this.TableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10f));
		this.TableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10f));
		this.TableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10f));
		this.TableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10f));
		this.TableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50f));
		this.TableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10f));
		System.Windows.Forms.TableLayoutPanel tableLayoutPanel2 = this.TableLayoutPanel;
		System.Drawing.Size size = new System.Drawing.Size(396, 258);
		tableLayoutPanel2.Size = size;
		this.TableLayoutPanel.TabIndex = 0;
		this.LogoPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
		this.LogoPictureBox.Image = (System.Drawing.Image)resources.GetObject("LogoPictureBox.Image");
		System.Windows.Forms.PictureBox logoPictureBox = this.LogoPictureBox;
		location = new System.Drawing.Point(3, 3);
		logoPictureBox.Location = location;
		this.LogoPictureBox.Name = "LogoPictureBox";
		this.TableLayoutPanel.SetRowSpan(this.LogoPictureBox, 6);
		System.Windows.Forms.PictureBox logoPictureBox2 = this.LogoPictureBox;
		size = new System.Drawing.Size(124, 252);
		logoPictureBox2.Size = size;
		this.LogoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.LogoPictureBox.TabIndex = 0;
		this.LogoPictureBox.TabStop = false;
		this.LabelProductName.Dock = System.Windows.Forms.DockStyle.Fill;
		System.Windows.Forms.Label labelProductName = this.LabelProductName;
		location = new System.Drawing.Point(136, 0);
		labelProductName.Location = location;
		System.Windows.Forms.Label labelProductName2 = this.LabelProductName;
		System.Windows.Forms.Padding margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
		labelProductName2.Margin = margin;
		System.Windows.Forms.Label labelProductName3 = this.LabelProductName;
		size = new System.Drawing.Size(0, 17);
		labelProductName3.MaximumSize = size;
		this.LabelProductName.Name = "LabelProductName";
		System.Windows.Forms.Label labelProductName4 = this.LabelProductName;
		size = new System.Drawing.Size(257, 17);
		labelProductName4.Size = size;
		this.LabelProductName.TabIndex = 0;
		this.LabelProductName.Text = "Product Name";
		this.LabelProductName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.LabelVersion.Dock = System.Windows.Forms.DockStyle.Fill;
		System.Windows.Forms.Label labelVersion = this.LabelVersion;
		location = new System.Drawing.Point(136, 25);
		labelVersion.Location = location;
		System.Windows.Forms.Label labelVersion2 = this.LabelVersion;
		margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
		labelVersion2.Margin = margin;
		System.Windows.Forms.Label labelVersion3 = this.LabelVersion;
		size = new System.Drawing.Size(0, 17);
		labelVersion3.MaximumSize = size;
		this.LabelVersion.Name = "LabelVersion";
		System.Windows.Forms.Label labelVersion4 = this.LabelVersion;
		size = new System.Drawing.Size(257, 17);
		labelVersion4.Size = size;
		this.LabelVersion.TabIndex = 0;
		this.LabelVersion.Text = "Version";
		this.LabelVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.LabelCopyright.Dock = System.Windows.Forms.DockStyle.Fill;
		System.Windows.Forms.Label labelCopyright = this.LabelCopyright;
		location = new System.Drawing.Point(136, 50);
		labelCopyright.Location = location;
		System.Windows.Forms.Label labelCopyright2 = this.LabelCopyright;
		margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
		labelCopyright2.Margin = margin;
		System.Windows.Forms.Label labelCopyright3 = this.LabelCopyright;
		size = new System.Drawing.Size(0, 17);
		labelCopyright3.MaximumSize = size;
		this.LabelCopyright.Name = "LabelCopyright";
		System.Windows.Forms.Label labelCopyright4 = this.LabelCopyright;
		size = new System.Drawing.Size(257, 17);
		labelCopyright4.Size = size;
		this.LabelCopyright.TabIndex = 0;
		this.LabelCopyright.Text = "Copyright";
		this.LabelCopyright.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.LabelCompanyName.Dock = System.Windows.Forms.DockStyle.Fill;
		System.Windows.Forms.Label labelCompanyName = this.LabelCompanyName;
		location = new System.Drawing.Point(136, 75);
		labelCompanyName.Location = location;
		System.Windows.Forms.Label labelCompanyName2 = this.LabelCompanyName;
		margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
		labelCompanyName2.Margin = margin;
		System.Windows.Forms.Label labelCompanyName3 = this.LabelCompanyName;
		size = new System.Drawing.Size(0, 17);
		labelCompanyName3.MaximumSize = size;
		this.LabelCompanyName.Name = "LabelCompanyName";
		System.Windows.Forms.Label labelCompanyName4 = this.LabelCompanyName;
		size = new System.Drawing.Size(257, 17);
		labelCompanyName4.Size = size;
		this.LabelCompanyName.TabIndex = 0;
		this.LabelCompanyName.Text = "Company Name";
		this.LabelCompanyName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.TextBoxDescription.Dock = System.Windows.Forms.DockStyle.Fill;
		System.Windows.Forms.TextBox textBoxDescription = this.TextBoxDescription;
		location = new System.Drawing.Point(136, 103);
		textBoxDescription.Location = location;
		System.Windows.Forms.TextBox textBoxDescription2 = this.TextBoxDescription;
		margin = new System.Windows.Forms.Padding(6, 3, 3, 3);
		textBoxDescription2.Margin = margin;
		this.TextBoxDescription.Multiline = true;
		this.TextBoxDescription.Name = "TextBoxDescription";
		this.TextBoxDescription.ReadOnly = true;
		this.TextBoxDescription.ScrollBars = System.Windows.Forms.ScrollBars.Both;
		System.Windows.Forms.TextBox textBoxDescription3 = this.TextBoxDescription;
		size = new System.Drawing.Size(257, 123);
		textBoxDescription3.Size = size;
		this.TextBoxDescription.TabIndex = 0;
		this.TextBoxDescription.TabStop = false;
		this.TextBoxDescription.Text = resources.GetString("TextBoxDescription.Text");
		this.OKButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.OKButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		System.Windows.Forms.Button oKButton = this.OKButton;
		location = new System.Drawing.Point(318, 232);
		oKButton.Location = location;
		this.OKButton.Name = "OKButton";
		System.Windows.Forms.Button oKButton2 = this.OKButton;
		size = new System.Drawing.Size(75, 23);
		oKButton2.Size = size;
		this.OKButton.TabIndex = 0;
		this.OKButton.Text = "&OK";
		System.Drawing.SizeF sizeF = new System.Drawing.SizeF(6f, 13f);
		this.AutoScaleDimensions = sizeF;
		this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.CancelButton = this.OKButton;
		size = new System.Drawing.Size(414, 276);
		this.ClientSize = size;
		this.Controls.Add(this.TableLayoutPanel);
		this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		this.MaximizeBox = false;
		this.MinimizeBox = false;
		this.Name = "AboutBox1";
		margin = new System.Windows.Forms.Padding(9);
		this.Padding = margin;
		this.ShowInTaskbar = false;
		this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "About";
		this.TableLayoutPanel.ResumeLayout(false);
		this.TableLayoutPanel.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.LogoPictureBox).EndInit();
		this.ResumeLayout(false);
	}

	[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
	private void AboutBox1_Load(object sender, EventArgs e)
	{
		string arg = ((Operators.CompareString(MyProject.Application.Info.Title, "", TextCompare: false) == 0) ? Path.GetFileNameWithoutExtension(MyProject.Application.Info.AssemblyName) : MyProject.Application.Info.Title);
		Text = $"About {arg}";
		LabelProductName.Text = MyProject.Application.Info.ProductName;
		LabelVersion.Text = $"Version {MyProject.Application.Info.Version.ToString()}";
		LabelCopyright.Text = MyProject.Application.Info.Copyright;
		LabelCompanyName.Text = MyProject.Application.Info.CompanyName;
		TextBoxDescription.Text = "A rewrite of the older Dossier tool. Original product credits and attribution are retained.\r\n\r\nField explanations: MadCard Maker's supplied tooltip reference.\r\n\r\nPlay font: Jonas Hecksher / Playtypes / e-types AS, SIL Open Font License 1.1.\r\n\r\n" + MyProject.Application.Info.Description;
	}

	private void OKButton_Click(object sender, EventArgs e)
	{
		Close();
	}
}
