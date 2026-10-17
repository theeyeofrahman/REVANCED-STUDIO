using System;
using System.IO;
using System.Drawing;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Windows.Forms;

namespace MPNDossier;

public partial class FormEditorV2
{
    private Label _editorStatus;
    private string _lastTransferStatus="";
    private readonly List<string> _history = new List<string>();
    private int _historyIndex=-1;
    private bool _restoring;
    private void ResetHistory()
    {
        _history.Clear();_historyIndex=-1;TrackEditorChange();
        ReportEditorStatus("Ready • "+SaveFolder+" / select a property to edit its value");
    }
    private void TrackEditorChange()
    {
        if(_restoring||Loading||ObjEditor.Nodes.Count==0)return;
        var json=SaveObj(ObjEditor.Nodes).ToString();
        if(_historyIndex>=0&&_history[_historyIndex]==json)return;
        if(_historyIndex+1<_history.Count)_history.RemoveRange(_historyIndex+1,_history.Count-_historyIndex-1);
        _history.Add(json);if(_history.Count>100)_history.RemoveAt(0);_historyIndex=_history.Count-1;
    }
    private void TravelHistory(int delta)
    {
        int next=_historyIndex+delta;if(next<0||next>=_history.Count)return;
        _restoring=true;
        try{var json=JObject.Parse(_history[next]);Reset();LoadObject(ObjEditor.Nodes,json,ControlScheme);_historyIndex=next;DisplayPrice();}
        finally{_restoring=false;}
        ReportEditorStatus(delta<0?"Change undone":"Change restored");
    }
    private void SaveWithPicker()
    {
        if(ObjEditor.Nodes.Count==0){ReportEditorStatus("Create or load an entry first.",true);return;}
        var name=FileName.Text.Trim();
        if(name.Length==0||name.IndexOfAny(Path.GetInvalidFileNameChars())>=0){ReportEditorStatus("Enter a valid entry name before saving.",true);FileName.Focus();return;}
        using(var dialog=new DossierSaveFileDialog{Title="Save "+SaveFolder+" entry",Filter="JSON entry (*.json)|*.json",DefaultExt="json",AddExtension=true,FileName=name+".json",InitialDirectory=_deckSaveDirectory??Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"db","new",SaveFolder),OverwritePrompt=true})
        {
            if(!Directory.Exists(dialog.InitialDirectory))dialog.InitialDirectory=AppDomain.CurrentDomain.BaseDirectory;
            if(dialog.ShowDialog(this)!=DialogResult.OK)return;
            try{FileName.Text=Path.GetFileNameWithoutExtension(dialog.FileName);SaveToDirectory(dialog.FileName);ReportEditorStatus("Saved • "+dialog.FileName+_lastTransferStatus,_lastTransferStatus.Contains("game copy failed"));DossierFeedback.Play("save");}
            catch(Exception ex){ReportEditorStatus("Could not save: "+ex.Message,true);DossierFeedback.Play("error");}
        }
    }
    private static void WriteEntrySafely(string path,string json)
    {
        var temporary=path+".tmp";File.WriteAllText(temporary,json);
        if(File.Exists(path))File.Replace(temporary,path,path+".bak");else File.Move(temporary,path);
    }
    private void ReportEditorStatus(string message, bool error=false)
    {
        if(_editorStatus!=null){_editorStatus.Text=message;_editorStatus.ForeColor=error?Color.Salmon:DossierStyle.Muted;}
    }
}
