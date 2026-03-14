using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class QuickTimeEventMeter : MonoBehaviour
{
    public static event Action OnSuccessfulHit; //Hit within green
    public static event Action OnFailedHit; //Hit within red

    Slider eventMeter;
    [SerializeField] Material eventMeterGraphicMaterial;
    bool isEventOver;
    [SerializeField] List<QuickTimeEventObject> eventSequenceOrder;
    [SerializeField] private int eventSequenceCount = 0;


    void Start()
    {
        eventMeter = GetComponent<Slider>();
        eventMeter.value = 0;
        eventSequenceCount = 0;

        SetupEventMeterGraphic();
    }

    void Update()
    {
        if(!isEventOver)
        {
            TickUpMeter();
            CheckPlayerInput();
        }
    }
    void SetupEventMeterGraphic()
    {
        eventMeterGraphicMaterial.SetFloat("_MinValue",eventSequenceOrder[eventSequenceCount].min_Max_For_SafeZone.x);
        eventMeterGraphicMaterial.SetFloat("_MaxValue",eventSequenceOrder[eventSequenceCount].min_Max_For_SafeZone.y);
    }
    void CheckPlayerInput()
    {
        if(Keyboard.current.spaceKey.isPressed)
        {
            isEventOver = true;
            Debug.Log(eventMeter.value);
            //Calculate where quicktime event meter is
            if(eventMeter.value>= eventSequenceOrder[eventSequenceCount].min_Max_For_SafeZone.x && eventMeter.value <= eventSequenceOrder[eventSequenceCount].min_Max_For_SafeZone.y)
            { //PASSED
                Debug.Log("SUCCESS");
                ResetEventMeter();
            }
            else
            { //FAILED
                Debug.Log("FAILED");
                
            }
        }
    }
    void TickUpMeter()
    {
        eventMeter.value += eventSequenceOrder[eventSequenceCount].speed * Time.deltaTime;
        if (eventMeter.value >= eventMeter.maxValue)
        {
            EventHasReachedMaxValue();
        }
    }
    void EventHasReachedMaxValue()
    {
        Debug.Log("OVER");
        isEventOver = true;
        OnFailedHit.Invoke();
    }
    void PlayerHitSafeZone()
    {
        Debug.Log("WON");
        isEventOver = true;
        OnSuccessfulHit.Invoke();
    }
    void PlayerHitRedZone()
    {
        Debug.Log("LOST");
        isEventOver = true;
        OnFailedHit.Invoke();
    }
    public void ResetEventMeter() //Call this to reset the meter for the NEXT QT event
    {
        eventMeter.value = 0;
        isEventOver = false;
        enabled = false;
        SetUpNextInSequence();
    }
    public void ResetEventSequence() //Call this to reset the chain of QT events
    {
        eventSequenceCount = 0; //BROKE UNITY BY FLIPPING THESE LMAOOOOO
    }
    void SetUpNextInSequence()
    {
        if(eventSequenceCount + 1 < eventSequenceOrder.Count)
        {
            eventSequenceCount ++;
            SetupEventMeterGraphic();
        }
        else if (eventSequenceCount + 1 >= eventSequenceOrder.Count)
        {
            Debug.Log("REACHED END");
            ResetEventSequence();
        }
    }
}
