using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace MadCardMaker;

public static class MakerWorkbench
{
 public static string Compare(JObject inherited,JObject current)
 {
  var rows=new List<string>();foreach(var key in inherited.Properties().Select(p=>p.Name).Union(current.Properties().Select(p=>p.Name)).OrderBy(k=>k,StringComparer.OrdinalIgnoreCase)){
   if(new[]{"Filename","Root","Priority","Mod_Name"}.Contains(key)||JToken.DeepEquals(inherited[key],current[key]))continue;
   rows.Add(key+"\r\n  Inherited: "+(inherited[key]?.ToString(Formatting.None)??"(absent)")+"\r\n  Current:   "+(current[key]?.ToString(Formatting.None)??"(absent)"));
  }return rows.Count==0?"No value differences from the inherited card.":string.Join("\r\n\r\n",rows);
 }
 public static string[] ChatterEvents=>MakerTools.Schema("Chatter").Where(p=>p.IsList&&p.PropType==ModCards.PropType.Str).Select(p=>p.PropName).ToArray();
 public static string Lines(JToken value)=>value is JArray a?string.Join("\r\n",a.Where(v=>v.Type==JTokenType.String&&v.ToString()!="empty").Select(v=>v.ToString())):"";
 public static JArray ParseLines(string text)=>new JArray(text.Replace("\r\n","\n").Replace('\r','\n').Split('\n').Where(s=>!string.IsNullOrWhiteSpace(s)));
 public static JObject PatchChatter(JObject original,JObject changes)
 {
  var copy=(JObject)original.DeepClone();foreach(var p in changes.Properties()){
   if(!ChatterEvents.Contains(p.Name)||!(p.Value is JArray list)||list.Any(v=>v.Type!=JTokenType.String))throw new FormatException("Chatter changes must contain event lists of text lines.");
   if(list.Any(v=>v.ToString()=="empty"))throw new FormatException("The word 'empty' is reserved by the deck format. Leave the event blank to clear its lines.");
   copy[p.Name]=list.DeepClone();
  }return copy;
 }
}

public sealed class MakerChatterWriter:Form
{
 readonly JObject source,changes=new JObject();readonly ListBox events=new ListBox{Dock=DockStyle.Fill};readonly TextBox lines=new TextBox{Dock=DockStyle.Fill,Multiline=true,AcceptsReturn=true,ScrollBars=ScrollBars.Both,WordWrap=false};readonly Label count=new Label{Dock=DockStyle.Fill};string selected;bool loading;
 public JObject Changes=>(JObject)changes.DeepClone();
 public MakerChatterWriter(JObject full)
 {
  AutoScaleMode=AutoScaleMode.Dpi;AutoScaleDimensions=new SizeF(96,96);
  source=(JObject)full.DeepClone();Text="Chatter writer";Size=new Size(980,650);MinimumSize=new Size(740,480);StartPosition=FormStartPosition.CenterParent;
  var root=new TableLayoutPanel{Dock=DockStyle.Fill,Padding=new Padding(20),ColumnCount=2,RowCount=4};root.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute,170));root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,100));root.RowStyles.Add(new RowStyle(SizeType.Absolute,55));root.RowStyles.Add(new RowStyle(SizeType.Percent,100));root.RowStyles.Add(new RowStyle(SizeType.Absolute,32));root.RowStyles.Add(new RowStyle(SizeType.Absolute,46));
  var guide=new Label{Text="Choose an event. Write one speech bubble per line. Only changed events are applied.\r\nBlanking an event clears its inherited lines; Cancel leaves the card untouched.",Dock=DockStyle.Fill};root.Controls.Add(guide,0,0);root.SetColumnSpan(guide,2);root.Controls.Add(events,0,1);root.Controls.Add(lines,1,1);root.Controls.Add(count,1,2);
  events.Items.AddRange(MakerWorkbench.ChatterEvents.Cast<object>().ToArray());events.SelectedIndexChanged+=(_,__)=>{loading=true;selected=events.SelectedItem as string;lines.Text=MakerWorkbench.Lines(changes[selected]??source[selected]);loading=false;UpdateCount();};
  lines.TextChanged+=(_,__)=>{if(loading||selected==null)return;var value=MakerWorkbench.ParseLines(lines.Text);if(JToken.DeepEquals(value,MakerWorkbench.ParseLines(MakerWorkbench.Lines(source[selected]))))changes.Remove(selected);else changes[selected]=value;UpdateCount();};
  var buttons=new FlowLayoutPanel{Dock=DockStyle.Fill};var apply=new DossierButton{Text="Apply to draft",Width=155,Height=36};apply.Click+=(_,__)=>{try{MakerWorkbench.PatchChatter(source,changes);DialogResult=DialogResult.OK;Close();}catch(Exception ex){DossierDialog.Show(this,ex.Message);}};buttons.Controls.Add(apply);buttons.Controls.Add(new DossierButton{Text="Cancel",Width=100,Height=36,DialogResult=DialogResult.Cancel});root.Controls.Add(buttons,1,3);Controls.Add(root);DossierStyle.Apply(this);lines.Font=DossierFonts.Create("Play",11);events.SelectedIndex=0;
 }
 void UpdateCount(){count.Text=MakerWorkbench.ParseLines(lines.Text).Count+" lines · "+changes.Count+" changed events";}
}

public sealed class MakerReferenceLibrary:Form
{
 readonly CheckBox favoritesOnly=new CheckBox{Text="Favorites only",AutoSize=true};readonly DossierButton favorite=new DossierButton{Text="Add favorite",Width=150,Height=34};readonly string favoritesPath;HashSet<string> favorites;
 readonly ListBox cards=new ListBox{Dock=DockStyle.Fill,HorizontalScrollbar=true};readonly TextBox preview=new TextBox{Dock=DockStyle.Fill,ReadOnly=true,Multiline=true,ScrollBars=ScrollBars.Both,WordWrap=false};readonly ComboBox types=new ComboBox{Dock=DockStyle.Fill,DropDownStyle=ComboBoxStyle.DropDownList};readonly TextBox search=new TextBox{Dock=DockStyle.Fill,AccessibleName="Search reference IDs, names and descriptions"};readonly Label summary=new Label{Dock=DockStyle.Fill};readonly List<KeyValuePair<string,JObject>> visible=new List<KeyValuePair<string,JObject>>();
 public string CardType,RootId;
 public MakerReferenceLibrary(bool canCreate,string favoritesFile=null)
 {
  favoritesPath=favoritesFile;favorites=MakerFavorites.Read(favoritesPath);
  AutoScaleMode=AutoScaleMode.Dpi;AutoScaleDimensions=new SizeF(96,96);
  Text="MCM reference library";Size=new Size(1080,730);MinimumSize=new Size(800,540);StartPosition=FormStartPosition.CenterParent;
  var root=new TableLayoutPanel{Dock=DockStyle.Fill,Padding=new Padding(20),ColumnCount=2,RowCount=4};root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,38));root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,62));root.RowStyles.Add(new RowStyle(SizeType.Absolute,44));root.RowStyles.Add(new RowStyle(SizeType.Percent,100));root.RowStyles.Add(new RowStyle(SizeType.Absolute,44));root.RowStyles.Add(new RowStyle(SizeType.Absolute,46));root.Controls.Add(types,0,0);root.Controls.Add(search,1,0);root.Controls.Add(cards,0,1);root.Controls.Add(preview,1,1);root.Controls.Add(summary,1,2);
  var favoriteTools=new FlowLayoutPanel{Dock=DockStyle.Fill,WrapContents=false};favoriteTools.Controls.Add(favoritesOnly);favoriteTools.Controls.Add(favorite);root.Controls.Add(favoriteTools,0,2);favoritesOnly.CheckedChanged+=(_,__)=>Filter();favorite.Click+=(_,__)=>{if(cards.SelectedIndex<0)return;try{var id=visible[cards.SelectedIndex].Key;MakerFavorites.Toggle(types.Text,id,favoritesPath);favorites=MakerFavorites.Read(favoritesPath);Filter();int at=visible.FindIndex(p=>p.Key==id);if(at>=0)cards.SelectedIndex=at;}catch(Exception ex){DossierDialog.Show(this,ex.Message,"Could not save favorite");}};
  var create=new DossierButton{Text=canCreate?"Create variant from selection":"Open a mod and select a deck to create variants",Width=430,Height=36,Enabled=canCreate};create.Click+=(_,__)=>{if(cards.SelectedIndex<0)return;CardType=types.Text;RootId=visible[cards.SelectedIndex].Key;DialogResult=DialogResult.OK;Close();};root.Controls.Add(create,1,3);Controls.Add(root);DossierStyle.Apply(this);preview.Font=DossierFonts.Create("Consolas",10);
  types.Items.AddRange(ModEnums.CARDTYPES.Where(t=>ModEnums.SC_DIR.TryGetValue(t,out var data)&&data.Count>0).Cast<object>().ToArray());types.SelectedIndexChanged+=(_,__)=>Filter();search.TextChanged+=(_,__)=>Filter();cards.SelectedIndexChanged+=(_,__)=>{preview.Text=cards.SelectedIndex>=0?visible[cards.SelectedIndex].Value.ToString():"";favorite.Enabled=cards.SelectedIndex>=0;favorite.Text=cards.SelectedIndex>=0&&favorites.Contains(types.Text+":"+visible[cards.SelectedIndex].Key)?"Remove favorite":"Add favorite";};if(types.Items.Count>0)types.SelectedIndex=types.Items.IndexOf("Character")>=0?types.Items.IndexOf("Character"):0;
 }
 void Filter(){visible.Clear();cards.BeginUpdate();cards.Items.Clear();if(ModEnums.SC_DIR.TryGetValue(types.Text,out var data))foreach(var p in data){bool saved=favorites.Contains(types.Text+":"+p.Key);string label=p.Key+" · "+(string)p.Value["Name"];if(favoritesOnly.Checked&&!saved||(label+" "+(string)p.Value["Description"]).IndexOf(search.Text,StringComparison.OrdinalIgnoreCase)<0)continue;visible.Add(p);cards.Items.Add((saved?"★ ":"")+label);}cards.EndUpdate();summary.Text=visible.Count+" reference cards · "+(favoritesOnly.Checked?"Saved favorites":"Original assets");preview.Clear();favorite.Enabled=cards.Items.Count>0;if(cards.Items.Count>0)cards.SelectedIndex=0;}
}
