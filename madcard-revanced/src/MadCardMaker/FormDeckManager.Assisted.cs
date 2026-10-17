using System;
using System.Windows.Forms;
namespace MadCardMaker;
public partial class FormDeckManager
{
    void NewAssistedCharacter()
    {
        if (!StudioDeckReady() || ViewOnly) return;
        var deck = ModNodes.GetRootNode(CardManager.SelectedNode);
        using (var wizard = new MakerAssistedCharacterDialog(candidate => deck.Nodes["Character"]?.Nodes[candidate] != null))
        {
            if (wizard.ShowDialog(this) != DialogResult.OK) return;
            string id = (string)wizard.Card["Filename"];
            if (deck.Nodes["Character"]?.Nodes[id] != null) throw new FormatException("That character ID already exists in this deck. Choose another ID.");
            StudioModule.ShowDocument(new FormEditor { Manager = this, PreloadType = "Character", PreloadRoot = (string)wizard.Card["Root"], PreloadFile = id, PreloadFileInfo = wizard.Card.ToString() }, this);
        }
    }
}
