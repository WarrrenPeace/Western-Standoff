using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class DeathMenu : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI Title;
    [SerializeField] TextMeshProUGUI Subtitle;
    
    public void FillMoralityText(QuickTimeEventObject standoff)
    {
        Subtitle.text = "Killed by " + standoff.Character.ToString();
    }
}
