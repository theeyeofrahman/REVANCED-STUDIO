using System;
using System.Linq;
using Newtonsoft.Json.Linq;
namespace MadCardMaker;
public static class MakerFields
{
 public static JObject Envelope(string type,string field,JToken value)=>new JObject{{"MCMFields",1},{"Type",type},{"Fields",new JObject{{field,value.DeepClone()}}}};
 public static JObject Read(string type,JObject envelope)
 {
  if((int?)envelope["MCMFields"]!=1||(string)envelope["Type"]!=type||!(envelope["Fields"] is JObject fields)||fields.Count==0)throw new FormatException("Copy a field from an MCM card of the same type first.");
  var schema=MakerTools.Schema(type);foreach(var field in fields.Properties()){
   var spec=schema.FirstOrDefault(p=>p.PropName==field.Name);if(spec==null||new[]{"Filename","Root","Priority"}.Contains(field.Name))throw new FormatException("The copied field is not editable: "+field.Name);
   Validate(spec,field.Value,field.Name);
  }return (JObject)fields.DeepClone();
 }
 static void Validate(ModCards.PropertyControl p,JToken token,string path,bool item=false)
 {
  if(p.IsList&&!item){if(!(token is JArray list))throw new FormatException(path+" needs a list.");foreach(var child in list)Validate(p,child,path+"[]",true);return;}
  if(p.PropType==ModCards.PropType.Obj||p.PropType==ModCards.PropType.Color||token is JObject&&p.Defines!=null){
   if(!(token is JObject obj))throw new FormatException(path+" needs an object.");
   if(p.PropType==ModCards.PropType.Color){if(obj.Count!=4||new[]{"r","g","b","a"}.Any(k=>obj[k]==null||!Number(obj[k])||(double)obj[k]<0||(double)obj[k]>1))throw new FormatException(path+" needs r, g, b and a values between 0 and 1.");return;}
   var schema=p.Selection as ModCards.PropertyControl[]??p.Defines;if(schema==null)throw new FormatException("Copying this compound field is not supported.");
   foreach(var child in obj.Properties()){var spec=schema.FirstOrDefault(s=>s.PropName==child.Name);if(spec==null)throw new FormatException("Unknown nested field: "+path+"."+child.Name);Validate(spec,child.Value,path+"."+child.Name);}
   if(schema.Any(s=>obj[s.PropName]==null))throw new FormatException(path+" is missing nested fields.");return;
  }
  bool valid=p.PropType==ModCards.PropType.Bool&&token.Type==JTokenType.Boolean||p.PropType==ModCards.PropType.Int&&token.Type==JTokenType.Integer&&(long)token>=int.MinValue&&(long)token<=int.MaxValue||p.PropType==ModCards.PropType.Float&&Number(token)||(p.PropType==ModCards.PropType.Str||p.PropType==ModCards.PropType.Ref)&&token.Type==JTokenType.String;
  if(!valid)throw new FormatException("Invalid copied value for "+path+".");
  if(p.PropType==ModCards.PropType.Ref&&!p.WriteNewValues&&p.ObjectName!="Object"&&!string.IsNullOrEmpty(p.ObjectName)&&!MakerStudio.Exists(p.ObjectName,token.ToString()))throw new FormatException("Reference not found for "+path+": "+token);
 }
 static bool Number(JToken token)=>token!=null&&(token.Type==JTokenType.Float||token.Type==JTokenType.Integer)&&!double.IsInfinity((double)token)&&!double.IsNaN((double)token);
 public static JObject Merge(JObject original,JObject fields){var result=(JObject)original.DeepClone();foreach(var p in fields.Properties())result[p.Name]=p.Value.DeepClone();return result;}
}
