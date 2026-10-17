using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MadCardMaker;
using Newtonsoft.Json.Linq;
class DeckCheck
{
    [STAThread] static int Main(string[] args)
    {
        try
        {
            string db=Path.GetFullPath(args[0]);var deck=JObject.Parse(File.ReadAllText(args[1]));
            DossierPreferences.SuppressPersistenceForTests=true;DossierFeedback.MutedForTests=true;DossierMotion.SuppressForTests=true;DossierDialog.PreventDialogsForTests=true;
            ModEnums.AddListOf(ModEnums.ENUMS,JObject.Parse(File.ReadAllText(Path.Combine(db,"DefaultEnums.json"))));
            ModEnums.AddListOf(ModEnums.ASSETS,JObject.Parse(File.ReadAllText(Path.Combine(db,"DefaultAssets.json"))));
            ModEnums.TT_PROPERTIES=JObject.Parse(File.ReadAllText(Path.Combine(db,"Tooltips_Obj.json")));
            ModEnums.TT_ENUMS=JObject.Parse(File.ReadAllText(Path.Combine(db,"Tooltips_Enums.json")));
            foreach(string folder in Directory.GetDirectories(Path.Combine(db,"base"))){string type=Path.GetFileName(folder);var cards=new SortedDictionary<string,JObject>(StringComparer.OrdinalIgnoreCase);foreach(string file in Directory.GetFiles(folder,"*.json",SearchOption.AllDirectories))cards[Path.GetFileNameWithoutExtension(file)]=JObject.Parse(File.ReadAllText(file));ModEnums.SC_DIR[type]=cards;ModEnums.MC_DIR[type]=new SortedDictionary<string,JObject>(StringComparer.OrdinalIgnoreCase);}
            foreach(string type in ModEnums.CARDTYPES)if(!ModEnums.SC_DIR.ContainsKey(type)){ModEnums.SC_DIR[type]=new SortedDictionary<string,JObject>();ModEnums.MC_DIR[type]=new SortedDictionary<string,JObject>();}
            foreach(string type in new[]{"Item","Weapon"}){ModEnums.SC_DIR[type]=new SortedDictionary<string,JObject>();ModEnums.MC_DIR[type]=new SortedDictionary<string,JObject>();}
            foreach(string type in new[]{"Armor","Keycard","Melee","Ranged","Thrown"})foreach(var card in ModEnums.SC_DIR[type]){ModEnums.SC_DIR["Item"][card.Key]=card.Value;if(type!="Armor"&&type!="Keycard")ModEnums.SC_DIR["Weapon"][card.Key]=card.Value;}
            int total=0,references=0;var errors=new List<string>();
            foreach(var group in deck.Properties())foreach(JObject card in group.Value){string id=(string)card["Filename"];if(ModEnums.MC_DIR[group.Name].ContainsKey(id))throw new Exception("Duplicate card ID: "+id);ModEnums.MC_DIR[group.Name][id]=card;total++;}
            foreach(var group in deck.Properties())foreach(JObject card in group.Value){string id=(string)card["Filename"];
                errors.AddRange(MakerStudio.Check(group.Name,card).Where(x=>x.Error).Select(x=>id+": "+x));
                foreach(var reference in MakerTools.References(group.Name,card)){references++;if(reference.Type=="Font"&&ModEnums.SC_DIR["Chatter"].Values.Any(c=>(string)c["Font"]==reference.Id))continue;if(!MakerStudio.Exists(reference.Type,reference.Id))errors.Add(id+" / "+reference.Field+" missing "+reference.Type+":"+reference.Id);}
                var schema=MakerTools.Schema(group.Name);foreach(var field in card.Properties())if(!new[]{"Filename","Root","Priority"}.Contains(field.Name)&&!schema.Any(p=>p.PropName==field.Name))errors.Add(id+" has unknown field "+field.Name);
            }
            if(errors.Count>0)throw new Exception(string.Join("\n",errors));
            using(var loader=new DialogLoader()){typeof(DialogLoader).GetField("AddedCards",System.Reflection.BindingFlags.Instance|System.Reflection.BindingFlags.NonPublic).SetValue(loader,new List<ModCards.CardTag>());loader.SortDeck(Path.GetFileNameWithoutExtension(args[1]),deck);var loaded=(List<ModCards.CardTag>)typeof(DialogLoader).GetField("AddedCards",System.Reflection.BindingFlags.Instance|System.Reflection.BindingFlags.NonPublic).GetValue(loader);if(loaded.Count!=total)throw new Exception("Native loader did not recognize every card.");}
            Console.WriteLine("PASS: "+total+" cards recognized by the native MadCard deck loader; "+references+" references checked; IDs, roots and field names valid. No windows or game were opened.");return 0;
        }
        catch(Exception e){Console.Error.WriteLine(e);return 1;}
    }
}
