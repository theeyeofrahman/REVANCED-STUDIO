using System;
using System.Linq;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MPNDossier;

public partial class FormGlossary
{
    private void BuildModernLayout()
    {
        SuspendLayout(); AutoScaleMode = AutoScaleMode.Dpi; AutoScaleDimensions = new SizeF(96, 96); Controls.Clear(); MinimumSize = new Size(850, 600); Size = new Size(1100, 760);
        var root = new Panel { Dock = DockStyle.Fill, Padding = new Padding(20) };
        var split = new SplitContainer { Size = new Size(1000, 600), Dock = DockStyle.Fill, Panel1MinSize = 180, Panel2MinSize = 350, SplitterDistance = 250, SplitterWidth = 8 };
        GlossaryDef.Dock = DockStyle.Fill; split.Panel1.Controls.Add(GlossaryDef);
        var details = new SplitContainer { Size = new Size(700, 600), Dock = DockStyle.Fill, Orientation = Orientation.Horizontal, Panel1MinSize = 180, Panel2MinSize = 140, SplitterDistance = 360, SplitterWidth = 8 };
        GlossaryItem.Dock = DockStyle.Fill; GlossaryDesc1.Dock = DockStyle.Fill; GlossaryDesc1.ReadOnly = true;
        details.Panel1.Controls.Add(GlossaryItem); details.Panel2.Padding = new Padding(0, 10, 0, 0); details.Panel2.Controls.Add(GlossaryDesc1); split.Panel2.Controls.Add(details);
        GlossarySelect.Dock = DockStyle.Top; GlossarySelect.Height = 48; root.Controls.Add(split); root.Controls.Add(GlossarySelect); Controls.Add(root);
        var searchPanel=new FlowLayoutPanel{Dock=DockStyle.Top,Height=42,WrapContents=false};
        var search=new TextBox{Width=350,AccessibleName="Find a glossary entry"};
        var find=new DossierButton{Text="Find next",Width=120,Height=30};
        searchPanel.Controls.Add(search);searchPanel.Controls.Add(find);root.Controls.Add(searchPanel);searchPanel.BringToFront();
        find.Click+=(_,__)=>FindGlossaryEntry(search.Text);
        search.KeyDown+=(_,e)=>{if(e.KeyCode==Keys.Enter){FindGlossaryEntry(search.Text);e.SuppressKeyPress=true;}};
        DossierStyle.Apply(this); GlossarySelect.Font = DossierFonts.Create(DossierStyle.Theme.FontFamily, 18, FontStyle.Bold);
        ResumeLayout(true);
    }
    private static IEnumerable<TreeNode> WalkGlossary(TreeNodeCollection nodes)
    {foreach(TreeNode node in nodes){yield return node;foreach(var child in WalkGlossary(node.Nodes))yield return child;}}
    private void FindGlossaryEntry(string text)
    {
        if(string.IsNullOrWhiteSpace(text))return;
        var all=WalkGlossary(GlossaryDef.Nodes).ToArray();int start=Array.IndexOf(all,GlossaryDef.SelectedNode);
        for(int i=1;i<=all.Length;i++){var node=all[(start+i+all.Length)%all.Length];if(node.Text.IndexOf(text,StringComparison.OrdinalIgnoreCase)<0)continue;GlossaryDef.SelectedNode=node;node.EnsureVisible();return;}
        GlossaryDesc1.Text="No glossary entries match ‘"+text+"’.";
    }
}
