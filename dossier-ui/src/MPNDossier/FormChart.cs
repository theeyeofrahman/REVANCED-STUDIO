using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Microsoft.VisualBasic.CompilerServices;

namespace MPNDossier;

[DesignerGenerated]
public class FormChart : Form
{
	private IContainer components;

	[AccessedThroughProperty("DossierChart")]
	private Chart _DossierChart;

	internal virtual Chart DossierChart
	{
		get
		{
			return _DossierChart;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			_DossierChart = value;
		}
	}

	public FormChart()
	{
		Load += FormChart_Load;
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
		System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
		System.Windows.Forms.DataVisualization.Charting.Legend legend = new System.Windows.Forms.DataVisualization.Charting.Legend();
		System.Windows.Forms.DataVisualization.Charting.Series series = new System.Windows.Forms.DataVisualization.Charting.Series();
		System.Windows.Forms.DataVisualization.Charting.Title title = new System.Windows.Forms.DataVisualization.Charting.Title();
		this.DossierChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
		((System.ComponentModel.ISupportInitialize)this.DossierChart).BeginInit();
		this.SuspendLayout();
		this.DossierChart.BackColor = System.Drawing.Color.Black;
		this.DossierChart.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
		this.DossierChart.BackImageTransparentColor = System.Drawing.Color.Black;
		this.DossierChart.BackSecondaryColor = System.Drawing.Color.Black;
		this.DossierChart.BorderlineColor = System.Drawing.Color.Red;
		this.DossierChart.BorderSkin.BackColor = System.Drawing.Color.DimGray;
		chartArea.AxisX.IsLabelAutoFit = false;
		chartArea.AxisX.LabelStyle.Font = new System.Drawing.Font("Impact", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		chartArea.AxisX.LabelStyle.ForeColor = System.Drawing.Color.Salmon;
		chartArea.AxisX.LineColor = System.Drawing.Color.Maroon;
		chartArea.AxisX.LineWidth = 2;
		chartArea.AxisX.MajorGrid.LineColor = System.Drawing.Color.Maroon;
		chartArea.AxisX.MajorTickMark.LineColor = System.Drawing.Color.Red;
		chartArea.AxisX.MinorGrid.LineColor = System.Drawing.Color.Maroon;
		chartArea.AxisX.MinorTickMark.LineColor = System.Drawing.Color.Red;
		chartArea.AxisX.Title = "Effectiveness";
		chartArea.AxisX.TitleFont = new System.Drawing.Font("Impact", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		chartArea.AxisX.TitleForeColor = System.Drawing.Color.White;
		chartArea.AxisY.InterlacedColor = System.Drawing.Color.Transparent;
		chartArea.AxisY.IsLabelAutoFit = false;
		chartArea.AxisY.LabelStyle.Font = new System.Drawing.Font("Impact", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		chartArea.AxisY.LabelStyle.ForeColor = System.Drawing.Color.White;
		chartArea.AxisY.LineColor = System.Drawing.Color.Maroon;
		chartArea.AxisY.MajorGrid.LineColor = System.Drawing.Color.Maroon;
		chartArea.AxisY.MajorTickMark.LineColor = System.Drawing.Color.Red;
		chartArea.AxisY.MinorGrid.LineColor = System.Drawing.Color.Maroon;
		chartArea.AxisY.MinorTickMark.Enabled = true;
		chartArea.AxisY.MinorTickMark.LineColor = System.Drawing.Color.Maroon;
		chartArea.AxisY.ScaleBreakStyle.LineColor = System.Drawing.Color.Maroon;
		chartArea.AxisY.Title = "Survivability";
		chartArea.AxisY.TitleFont = new System.Drawing.Font("Impact", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		chartArea.AxisY.TitleForeColor = System.Drawing.Color.White;
		chartArea.BackColor = System.Drawing.Color.Maroon;
		chartArea.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.TopBottom;
		chartArea.BackSecondaryColor = System.Drawing.Color.Black;
		chartArea.BorderColor = System.Drawing.Color.Maroon;
		chartArea.Name = "ChartArea1";
		this.DossierChart.ChartAreas.Add(chartArea);
		legend.BackColor = System.Drawing.Color.Black;
		legend.Font = new System.Drawing.Font("Impact", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		legend.ForeColor = System.Drawing.Color.White;
		legend.IsTextAutoFit = false;
		legend.LegendStyle = System.Windows.Forms.DataVisualization.Charting.LegendStyle.Column;
		legend.Name = "Measured";
		this.DossierChart.Legends.Add(legend);
		System.Windows.Forms.DataVisualization.Charting.Chart dossierChart = this.DossierChart;
		System.Drawing.Point location = new System.Drawing.Point(0, 1);
		dossierChart.Location = location;
		this.DossierChart.Name = "DossierChart";
		series.ChartArea = "ChartArea1";
		series.Font = new System.Drawing.Font("Impact", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		series.LabelBackColor = System.Drawing.Color.White;
		series.LabelBorderColor = System.Drawing.Color.White;
		series.LabelForeColor = System.Drawing.Color.White;
		series.Legend = "Measured";
		series.Name = "Characters";
		this.DossierChart.Series.Add(series);
		System.Windows.Forms.DataVisualization.Charting.Chart dossierChart2 = this.DossierChart;
		System.Drawing.Size size = new System.Drawing.Size(900, 500);
		dossierChart2.Size = size;
		this.DossierChart.TabIndex = 0;
		this.DossierChart.Text = "Magnitude";
		title.BackColor = System.Drawing.Color.Red;
		title.Font = new System.Drawing.Font("Impact", 18f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		title.Name = "Title1";
		title.Text = "Power Magnitude Chart";
		this.DossierChart.Titles.Add(title);
		System.Drawing.SizeF sizeF = new System.Drawing.SizeF(6f, 13f);
		this.AutoScaleDimensions = sizeF;
		this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
		size = new System.Drawing.Size(898, 497);
		this.ClientSize = size;
		this.Controls.Add(this.DossierChart);
		this.Name = "FormChart";
		this.Text = "Power Magnification Chart";
		((System.ComponentModel.ISupportInitialize)this.DossierChart).EndInit();
		this.ResumeLayout(false);
	}

	private void FormChart_Load(object sender, EventArgs e)
	{
		DataTable dataTable = new DataTable();
		dataTable.Columns.Add("Name", typeof(string));
		dataTable.Columns.Add("Total Corpus", typeof(int));
		dataTable.Columns.Add("Total Tac", typeof(double));
		dataTable.Columns.Add("Avg Armor", typeof(double));
		dataTable.Columns.Add("Survivability", typeof(double));
		dataTable.Columns.Add("Level", typeof(int));
		dataTable.Columns.Add("Avg Att.", typeof(double));
		dataTable.Columns.Add("Effectiveness", typeof(double));
		dataTable.Columns.Add("Magnitude", typeof(double));
		checked
		{
			foreach (string key in ModEnums.DB_CHAR.Keys)
			{
				int num = ModEnums.DB_CHAR[key]["Health"].ToObject<int>() * ModEnums.DB_CHAR[key]["CorpusBlocks"].Count();
				double num2 = Math.Round(Math.Pow(ModEnums.DB_CHAR[key]["Attributes"]["Dexterity"].ToObject<int>(), 0.9) * (double)ModEnums.DB_CHAR[key]["TacBar"].ToObject<int>(), 1);
				double num3 = (double)ModEnums.DB_CHAR[key]["ArmorHead"]["Defense"].ToObject<int>() / (double)ModEnums.DB_CHAR[key]["ArmorHead"]["Coverage"].ToObject<int>();
				double num4 = (double)ModEnums.DB_CHAR[key]["ArmorBody"]["Defense"].ToObject<int>() / (double)ModEnums.DB_CHAR[key]["ArmorBody"]["Coverage"].ToObject<int>();
				double num5 = num3 + num4 / 2.0;
				double num6 = (double)num * num5 + num2;
				int num7 = ModEnums.DB_CHAR[key]["Level"].ToObject<int>();
				int num8 = ModEnums.DB_CHAR[key]["Attributes"]["Strength"].ToObject<int>();
				int num9 = ModEnums.DB_CHAR[key]["Attributes"]["Endurance"].ToObject<int>();
				int num10 = ModEnums.DB_CHAR[key]["Attributes"]["Dexterity"].ToObject<int>();
				int num11 = ModEnums.DB_CHAR[key]["Attributes"]["Dexterity"].ToObject<int>();
				double num12 = (double)(num8 + num9 + num10 + num11) / 4.0;
				double num13 = ((double)num7 + num12) / 2.0;
				double num14 = Math.Sqrt(num6 * num6 + num13 * num13);
				dataTable.Rows.Add(key, num, num2, num5, num6, num7, num12, num13, num14);
			}
			DossierChart.Dock = DockStyle.Bottom;
			DossierChart.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
		}
	}
}
