using System;
using System.IO;
using System.Windows.Forms;
namespace RevancedStudio;
internal static class Program
{
 [STAThread]static void Main(){Application.EnableVisualStyles();Application.SetCompatibleTextRenderingDefault(false);try{Application.Run(new StudioWindow());}catch(Exception ex){MadCardMaker.DossierDialog.Show("REVANCED Studio could not start.\r\n"+ex.Message,"Startup error");}}
}
