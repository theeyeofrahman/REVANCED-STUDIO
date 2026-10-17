using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

namespace MPNDossier;

public sealed class DossierSummary:Control
{
    public string SubjectId { get; private set; }="";
    public JObject Data { get; private set; }
    public string CompareId { get; set; }
    public string Error { get; set; }
    public DossierSummary(){DoubleBuffered=true;BackColor=DossierStyle.Background;MinimumSize=new Size(400,700);}
    public void Bind(string id,JObject data){SubjectId=id;Data=data;Error=null;Invalidate();}
    private static string Value(JObject data,string key,string fallback="—")=>data?[key]?.ToString()??fallback;
    private static double Number(JObject data,string key){double value;return double.TryParse(Value(data,key),System.Globalization.NumberStyles.Float,System.Globalization.CultureInfo.InvariantCulture,out value)?value:0;}
    private static int Attribute(JObject data,int index){int value;return int.TryParse(data?["Attributes"]?.ElementAtOrDefault(index)?.ToString(),out value)?value:0;}
    public string Brief()=>Data==null?"No subject selected":Value(Data,"FullName")+" ["+SubjectId+"]\r\n"+Value(Data,"Title")+"\r\nLevel "+Value(Data,"Level")+"  ·  Health "+Value(Data,"Health")+"  ·  Scale "+Value(Data,"Scale")+"\r\n\r\n"+Value(Data,"Description","");
    public void ExportCard(string path)
    {
        int logicalHeight=Math.Max(860,(int)Math.Ceiling(Height*96d/DeviceDpi));
        using(var image=new Bitmap(3000,logicalHeight*3))
        {image.SetResolution(288,288);using(var graphics=Graphics.FromImage(image)){graphics.ScaleTransform(3,3);PaintCard(graphics,1000,logicalHeight);image.Save(path,ImageFormat.Png);}}
    }
    protected override void OnPaint(PaintEventArgs e){base.OnPaint(e);float scale=DeviceDpi/96f;var state=e.Graphics.Save();try{e.Graphics.ScaleTransform(scale,scale);PaintCard(e.Graphics,(int)(Width/scale),(int)(Height/scale));}finally{e.Graphics.Restore(state);}}
    private void PaintCard(Graphics g,int width,int height)
    {
        g.Clear(DossierStyle.Background);g.SmoothingMode=SmoothingMode.AntiAlias;g.TextRenderingHint=System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
        using(var font=DossierFonts.Logical(DossierStyle.Theme.FontFamily,10))using(var small=DossierFonts.Logical(DossierStyle.Theme.FontFamily,9))using(var title=DossierFonts.Logical(DossierStyle.Theme.HeadingFont,26,FontStyle.Bold))using(var valueFont=DossierFonts.Logical(DossierStyle.Theme.HeadingFont,24,FontStyle.Bold))
        using(var normal=new SolidBrush(DossierStyle.Text))using(var muted=new SolidBrush(DossierStyle.Muted))using(var accent=new SolidBrush(DossierStyle.Accent))using(var cyan=new SolidBrush(DossierStyle.Cyan))using(var surface=new SolidBrush(DossierStyle.Surface))using(var line=new Pen(DossierStyle.Line))
        {
            g.FillRectangle(accent,24,24,34,3);g.DrawString("ID: "+SubjectId,small,muted,70,18);
            if(Data==null){g.DrawString(Error??"SELECT A SUBJECT TO BEGIN",title,normal,new RectangleF(24,85,width-48,160));return;}
            g.DrawString(Value(Data,"FullName"),title,normal,new RectangleF(22,55,width-44,68));
            string faction;try{faction=((ModEnums.Faction)(int)Number(Data,"DefaultFaction")).ToString();}catch{faction="Unknown";}
            g.DrawString(Value(Data,"Title")+"  /  "+faction.ToUpperInvariant(),font,cyan,24,119);
            int cardWidth=(width-78)/4;
            var captions=new[]{"Level","Health × corpus","Scale","Tac bar"};
            var values=new[]{Value(Data,"Level"),Value(Data,"Health")+" × "+(Data["CorpusBlocks"]?.Count()??0),"× "+Value(Data,"Scale"),"× "+Value(Data,"TacBar")};
            for(int i=0;i<4;i++)
            {
                var rect=new Rectangle(24+i*(cardWidth+10),163,cardWidth,105);
                g.DrawLine(line,rect.Left,rect.Bottom,rect.Right,rect.Bottom);
                g.DrawString(captions[i],small,muted,rect.X+13,rect.Y+14);g.DrawString(values[i],valueFont,i==0?accent:normal,rect.X+12,rect.Y+42);
            }
            g.DrawString("Base attributes",small,cyan,24,296);
            var labels=new[]{"Strength","Endurance","Dexterity","Tactics"};
            for(int i=0;i<4;i++)
            {
                int y=332+i*43;var value=Attribute(Data,i);
                g.DrawString(labels[i],font,normal,24,y);g.DrawString(value.ToString(),font,normal,width-60,y);
                g.FillRectangle(surface,150,y+5,Math.Max(1,width-238),7);g.FillRectangle(accent,150,y+5,(float)(Math.Max(1,width-238)*Math.Min(1,Math.Max(0,value)/100d)),7);
            }
            g.DrawString("Description",small,cyan,24,520);
            var description=Value(Data,"Description","");if(string.IsNullOrWhiteSpace(description))description="No field notes supplied in this record.";
            g.DrawString(description,font,normal,new RectangleF(24,555,width-48,125));
            JObject comparison;
            if(!string.IsNullOrEmpty(CompareId)&&ModEnums.DB_CHAR.TryGetValue(CompareId,out comparison))
            {
                g.DrawString("Compared with "+CompareId,small,cyan,24,700);
                for(int i=0;i<4;i++)
                {
                    int delta=Attribute(Data,i)-Attribute(comparison,i);int x=24+i*(width-48)/4;
                    g.DrawString(labels[i],font,muted,x,738);g.DrawString((delta>0?"+":"")+delta,valueFont,delta>0?cyan:delta<0?accent:normal,x,765);
                }
            }
        }
    }
}

public sealed class DossierCharacterBrowser:ListBox,IDossierDpiMetrics
{
    public DossierCharacterBrowser(){DrawMode=DrawMode.OwnerDrawFixed;ItemHeight=48;BorderStyle=BorderStyle.None;IntegralHeight=false;}
    public void RefreshDpiMetrics(){ItemHeight=DossierDpi.Pixels(this,48);Invalidate();}
    protected override void OnDrawItem(DrawItemEventArgs e)
    {
        if(e.Index<0)return;var id=GetItemText(Items[e.Index]);var selected=(e.State&DrawItemState.Selected)!=0;
        using(var background=new SolidBrush(selected?DossierStyle.Surface:DossierStyle.Input))e.Graphics.FillRectangle(background,e.Bounds);
        int P(int value)=>DossierDpi.Pixels(this,value);
        using(var accent=new SolidBrush(DossierStyle.Accent))if(selected)e.Graphics.FillRectangle(accent,e.Bounds.Left,e.Bounds.Top+P(5),P(3),e.Bounds.Height-P(10));
        JObject data;var name=ModEnums.DB_CHAR.TryGetValue(id,out data)?data["FullName"]?.ToString()??id:id;
        var starred=DossierPreferences.Current.Favorites.Contains(id)?"★ ":"";
        using(var title=DossierFonts.Create(DossierStyle.Theme.FontFamily,10,FontStyle.Bold))using(var secondary=DossierFonts.Create(DossierStyle.Theme.FontFamily,8))
        {TextRenderer.DrawText(e.Graphics,starred+name,title,new Rectangle(e.Bounds.X+P(12),e.Bounds.Y+P(5),e.Bounds.Width-P(22),P(21)),DossierStyle.Text,TextFormatFlags.EndEllipsis);TextRenderer.DrawText(e.Graphics,id,secondary,new Rectangle(e.Bounds.X+P(12),e.Bounds.Y+P(27),e.Bounds.Width-P(22),P(16)),DossierStyle.Muted,TextFormatFlags.EndEllipsis);}
    }
}
