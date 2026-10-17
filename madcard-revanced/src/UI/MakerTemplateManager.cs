using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
namespace MadCardMaker;
public static class MakerTemplateStore
{
 public static JArray Combine(JArray original,JArray imported)
 {
  var result=(JArray)original.DeepClone();foreach(var entry in imported){if(!(entry is JObject item)||string.IsNullOrWhiteSpace((string)item["Name"])||!(item["Card"] is JObject card)||MakerStudio.Check((string)item["Type"],card).Any(i=>i.Error))throw new FormatException("A template needs Name, a supported Type and a valid Card for this database.");var copy=(JObject)item.DeepClone();string name=(string)copy["Name"];int n=2;while(result.Any(p=>(string)p["Name"]==(string)copy["Name"]))copy["Name"]=name+" ("+(n++)+")";result.Add(copy);}return result;
 }
 public static void Save(JArray templates,string path=null)=>MakerHelp.Write(path??MakerTools.PresetPath,templates.ToString());
 public static JArray Rename(JArray original,int index,string name)
 {
  if(index<0||index>=original.Count||string.IsNullOrWhiteSpace(name))throw new ArgumentException("Select a template and enter a name.");var result=(JArray)original.DeepClone();result[index]["Name"]=name.Trim();return result;
 }
 public static JArray Remove(JArray original,int index){if(index<0||index>=original.Count)throw new ArgumentException("Select a template first.");var result=(JArray)original.DeepClone();result.RemoveAt(index);return result;}
}
public sealed class MakerTemplateManager:Form
{
 JArray data;readonly ListBox list=new ListBox{Dock=DockStyle.Fill};readonly TextBox preview=new TextBox{Dock=DockStyle.Fill,Multiline=true,ReadOnly=true,ScrollBars=ScrollBars.Both,WordWrap=false};
 public MakerTemplateManager()
 {
  AutoScaleMode=AutoScaleMode.Dpi;AutoScaleDimensions=new SizeF(96,96);Text="Template manager";Size=new Size(1000,650);MinimumSize=new Size(800,480);StartPosition=FormStartPosition.CenterParent;data=MakerTools.ReadPresets();
  var root=new TableLayoutPanel{Dock=DockStyle.Fill,Padding=new Padding(20),ColumnCount=2,RowCount=2};root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,35));root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,65));root.RowStyles.Add(new RowStyle(SizeType.Percent,100));root.RowStyles.Add(new RowStyle(SizeType.Absolute,48));root.Controls.Add(list,0,0);root.Controls.Add(preview,1,0);var tools=new FlowLayoutPanel{Dock=DockStyle.Fill,WrapContents=false};
  void Button(string text,Action action){var b=new DossierButton{Text=text,Width=135,Height=36};b.Click+=(_,__)=>{try{action();}catch(Exception ex){DossierDialog.Show(this,ex.Message,"Templates");}};tools.Controls.Add(b);}
  void Commit(JArray updated){MakerTemplateStore.Save(updated);data=updated;RefreshList();}
  Button("Rename…",()=>{Selected();var name=MakerStudio.Prompt(this,"Rename template","Template name",(string)data[list.SelectedIndex]["Name"]);if(name!=null)Commit(MakerTemplateStore.Rename(data,list.SelectedIndex,name));});
  Button("Remove…",()=>{Selected();if(DossierDialog.Show(this,"Remove this reusable template? Existing cards are unaffected. A .bak copy of the library is kept.","Remove template",MessageBoxButtons.YesNo)==DialogResult.Yes)Commit(MakerTemplateStore.Remove(data,list.SelectedIndex));});
  Button("Export selected…",()=>{Selected();using(var dialog=new DossierSaveFileDialog{Title="Export reusable template",FileName="MCM-template.json",Filter="Template JSON (*.json)|*.json",DefaultExt="json"})if(dialog.ShowDialog(this)==DialogResult.OK)MakerHelp.Write(dialog.FileName,new JArray(data[list.SelectedIndex].DeepClone()).ToString());});
  Button("Import templates…",()=>{using(var dialog=new DossierOpenFileDialog{Title="Import reusable templates",Filter="Template JSON (*.json)|*.json"})if(dialog.ShowDialog(this)==DialogResult.OK){var imported=JArray.Parse(File.ReadAllText(dialog.FileName));Commit(MakerTemplateStore.Combine(data,imported));}});
  root.Controls.Add(tools,0,1);root.SetColumnSpan(tools,2);Controls.Add(root);DossierStyle.Apply(this);preview.Font=DossierFonts.Create("Consolas",10);list.SelectedIndexChanged+=(_,__)=>preview.Text=list.SelectedIndex>=0?data[list.SelectedIndex].ToString():"";RefreshList();
 }
 void Selected(){if(list.SelectedIndex<0)throw new InvalidOperationException("Select a template first. Save one from a card's Review menu if the library is empty.");}
 void RefreshList(){list.Items.Clear();preview.Clear();foreach(var item in data)list.Items.Add(item["Type"]+" · "+item["Name"]);if(list.Items.Count>0)list.SelectedIndex=0;}
}
