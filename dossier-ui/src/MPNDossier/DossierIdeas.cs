using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
namespace MPNDossier;
public sealed class DossierIdeaTask
{
 public string Text{get;set;}="";
 public bool Done{get;set;}
}
public sealed class DossierIdea
{
 public string Id{get;set;}=Guid.NewGuid().ToString("N");
 public string Title{get;set;}="";
 public string Notes{get;set;}="";
 public string Tags{get;set;}="";
 public string Column{get;set;}="Ideas";
 public string Priority{get;set;}="Normal";
 public bool Archived{get;set;}
 public DateTime CreatedUtc{get;set;}=DateTime.UtcNow;
 public DateTime UpdatedUtc{get;set;}=DateTime.UtcNow;
 public List<DossierIdeaTask> Tasks{get;set;}=new List<DossierIdeaTask>();
 public DossierIdea Copy()=>JsonConvert.DeserializeObject<DossierIdea>(JsonConvert.SerializeObject(this));
}
public sealed class DossierIdeas
{
 public static readonly string[] Columns={"Ideas","To do","In progress","Done"};
 public static readonly string[] Priorities={"Low","Normal","High"};
 public List<DossierIdea> Cards{get;set;}=new List<DossierIdea>();
 [JsonIgnore]public string RecoveryNotice{get;private set;}
 public static string StorePath=>Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),"MPNDossier","ideas.json");
 public static DossierIdeas Parse(string json)
 {
  var board=JsonConvert.DeserializeObject<DossierIdeas>(json);if(board==null)throw new FormatException("The idea board is empty.");
  board.Cards=(board.Cards??new List<DossierIdea>()).Where(c=>c!=null).ToList();var ids=new HashSet<string>(StringComparer.Ordinal);
  foreach(var card in board.Cards){
   if(string.IsNullOrWhiteSpace(card.Id)||!ids.Add(card.Id)){card.Id=Guid.NewGuid().ToString("N");ids.Add(card.Id);}
   if(!Columns.Contains(card.Column))card.Column=Columns[0];if(!Priorities.Contains(card.Priority))card.Priority="Normal";
   card.Title=string.IsNullOrWhiteSpace(card.Title)?"Untitled idea":card.Title;card.Notes=card.Notes??"";card.Tags=card.Tags??"";
   card.Tasks=(card.Tasks??new List<DossierIdeaTask>()).Where(t=>t!=null).ToList();foreach(var task in card.Tasks)task.Text=task.Text??"";
  }
  return board;
 }
 public static DossierIdeas Load(string path=null)
 {
  path=path??StorePath;if(!File.Exists(path))return new DossierIdeas();
  try{return Parse(File.ReadAllText(path));}
  catch(Exception first)when(first is IOException||first is JsonException||first is FormatException){
   if(!File.Exists(path+".bak"))throw;
   var recovered=Parse(File.ReadAllText(path+".bak"));recovered.RecoveryNotice="Recovered the previous saved board from backup.";return recovered;
  }
 }
 public string ToJson()=>JsonConvert.SerializeObject(this,Formatting.Indented);
 public void Save(string path=null)
 {
  path=path??StorePath;Directory.CreateDirectory(Path.GetDirectoryName(Path.GetFullPath(path)));string temp=path+".tmp";File.WriteAllText(temp,ToJson());
  if(File.Exists(path))File.Replace(temp,path,path+".bak");else File.Move(temp,path);
 }
 public void Put(DossierIdea idea)
 {
  if(string.IsNullOrWhiteSpace(idea.Title))throw new ArgumentException("Give this idea a title.");
  if(!Columns.Contains(idea.Column)||!Priorities.Contains(idea.Priority))throw new ArgumentException("Choose a valid column and priority.");
  var copy=idea.Copy();copy.Title=copy.Title.Trim();copy.UpdatedUtc=DateTime.UtcNow;int index=Cards.FindIndex(c=>c.Id==copy.Id);if(index<0)Cards.Add(copy);else Cards[index]=copy;
 }
 public void Move(string id,string column,string beforeId=null)
 {
  if(!Columns.Contains(column))throw new ArgumentException("Unknown board column.");var card=Cards.FirstOrDefault(c=>c.Id==id);if(card==null)throw new ArgumentException("This card no longer exists.");
  if(id==beforeId)return;Cards.Remove(card);card.Column=column;card.UpdatedUtc=DateTime.UtcNow;
  int index=beforeId==null?-1:Cards.FindIndex(c=>c.Id==beforeId&&c.Column==column);if(index<0)Cards.Add(card);else Cards.Insert(index,card);
 }
 public IEnumerable<DossierIdea> Filter(string query,bool archived=false)
 {
  var words=(query??"").Split(new[]{' '},StringSplitOptions.RemoveEmptyEntries);
  return Cards.Where(c=>c.Archived==archived&&words.All(w=>(c.Title+" "+c.Notes+" "+c.Tags+" "+string.Join(" ",c.Tasks.Select(t=>t.Text))).IndexOf(w,StringComparison.OrdinalIgnoreCase)>=0));
 }
}
