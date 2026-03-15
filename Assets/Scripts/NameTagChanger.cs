using System;
using TMPro;
using UnityEngine;

public class NameTagChanger : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nametag;

    void OnEnable()
    {
        GameStateManager.OnStartNextStandoff += ChangeNameOnNameTag;
    }
    void OnDisable()
    {
        GameStateManager.OnStartNextStandoff -= ChangeNameOnNameTag;
    }
    void Start()
    {
        ChangeNameOnNameTag();
    }

    void ChangeNameOnNameTag()
    {
        nametag.text = QuickTimeEventMeter.instance.eventSequenceOrder[QuickTimeEventMeter.instance.eventSequenceCount].Character.ToString();
    }
}
