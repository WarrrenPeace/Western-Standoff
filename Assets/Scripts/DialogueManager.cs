using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI CharacterTextBox;
    [SerializeField] TextMeshProUGUI DialogueTextBox;
    [SerializeField] TextMeshProUGUI DescriptionTextBox;
    
    public void FillDialogue(QuickTimeEventObject standoff)
    {
        CharacterTextBox.text = standoff.Character.ToString();
        DialogueTextBox.text = standoff.Dialogue.ToString();
        DescriptionTextBox.text = standoff.Description.ToString();
    }
}
