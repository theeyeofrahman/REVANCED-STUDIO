using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;
using Newtonsoft.Json.Linq;

namespace MadCardMaker;

[DesignerGenerated]
public class FormViewer : Form
{
	private IContainer components;

	[field: AccessedThroughProperty("CardName")]
	internal virtual Label CardName
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Properties")]
	internal virtual TreeView Properties
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	public JObject Card { get; set; }

	public FormViewer()
	{
		Load += FormViewer_Load;
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
		this.CardName = new System.Windows.Forms.Label();
		this.Properties = new System.Windows.Forms.TreeView();
		base.SuspendLayout();
		this.CardName.AutoSize = true;
		this.CardName.Dock = System.Windows.Forms.DockStyle.Top;
		this.CardName.Location = new System.Drawing.Point(0, 0);
		this.CardName.Name = "CardName";
		this.CardName.Size = new System.Drawing.Size(105, 23);
		this.CardName.TabIndex = 1;
		this.CardName.Text = "Card Viewer";
		this.Properties.BackColor = System.Drawing.Color.Black;
		this.Properties.Dock = System.Windows.Forms.DockStyle.Fill;
		this.Properties.ForeColor = System.Drawing.Color.White;
		this.Properties.Location = new System.Drawing.Point(0, 23);
		this.Properties.Name = "Properties";
		this.Properties.Size = new System.Drawing.Size(543, 247);
		this.Properties.TabIndex = 2;
		base.AutoScaleDimensions = new System.Drawing.SizeF(96f, 96f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
		this.BackColor = System.Drawing.Color.DimGray;
		base.ClientSize = new System.Drawing.Size(543, 270);
		base.Controls.Add(this.Properties);
		base.Controls.Add(this.CardName);
		this.Font = new System.Drawing.Font("Impact", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.ForeColor = System.Drawing.Color.White;
		base.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
		base.Name = "FormViewer";
		this.Text = "Card Viewer";
		base.ResumeLayout(false);
		base.PerformLayout();
	}

	private void FormViewer_Load(object sender, EventArgs e)
	{
		if (Card != null)
		{
			LoadObject(Properties.Nodes, Card);
		}
	}

	private void LoadObject(TreeNodeCollection nodes, JObject jobject)
	{
		foreach (JProperty item in jobject.Properties())
		{
			nodes.Add(NewPropertyNode(item.Name, item.Value));
		}
	}

	private TreeNode NewPropertyNode(string name, JToken prop)
	{
		TreeNode treeNode = new TreeNode();
		if (prop.Type == JTokenType.Array)
		{
			treeNode.Text = name;
			foreach (JToken item in prop.Children())
			{
				treeNode.Nodes.Add(NewPropertyNode("", item));
			}
		}
		else if (prop.Type == JTokenType.Object)
		{
			treeNode.Text = ((name.Length > 0) ? name : "Object");
			JObject jObject = JObject.Parse(prop.ToString());
			if ((jObject["r"] != null) & (jObject["g"] != null) & (jObject["b"] != null))
			{
				ModCards.ColorRGBA colorRGBA = NewColorFromHex(Color.White);
				double.TryParse(jObject["r"].ToString(), out colorRGBA.r);
				double.TryParse(jObject["g"].ToString(), out colorRGBA.g);
				double.TryParse(jObject["b"].ToString(), out colorRGBA.b);
				treeNode.Text = ((name.Length > 0) ? (name + ": ") : "") + colorRGBA.ToString();
				treeNode.ForeColor = colorRGBA.AsColor();
			}
			else
			{
				LoadObject(treeNode.Nodes, JObject.Parse(prop.ToString()));
			}
		}
		else
		{
			treeNode.Text = ((name.Length > 0) ? (name + ": ") : "") + prop.ToString();
		}
		return treeNode;
	}

	public ModCards.ColorRGBA NewColorFromHex(Color color)
	{
		return new ModCards.ColorRGBA
		{
			r = Math.Round((double)(int)color.R / 255.0, 7),
			g = Math.Round((double)(int)color.G / 255.0, 7),
			b = Math.Round((double)(int)color.B / 255.0, 7),
			a = Math.Round((double)(int)color.A / 255.0, 7)
		};
	}
}
