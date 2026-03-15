using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI CharacterTextBox;
    [SerializeField] TextMeshProUGUI DialogueTextBox;
    
    public void FillDialogue(QuickTimeEventObject standoff)
    {
        CharacterTextBox.text = standoff.Character.ToString() + ":";
        DialogueTextBox.text = standoff.Dialogue.ToString();
    }
}
