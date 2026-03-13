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
    bool isEventOver;
    [SerializeField] List<QuickTimeEventObject> eventSequence;
    [SerializeField] private int eventSequenceCount = 0;


    void Start()
    {
        eventMeter = GetComponent<Slider>();
        eventSequenceCount = 0;
    }

    void Update()
    {
        if(!isEventOver)
        {
            TickUpMeter();
            CheckPlayerInput();
        }
    }
    void CheckPlayerInput()
    {
        if(Keyboard.current.spaceKey.isPressed)
        {
            isEventOver = true;
            Debug.Log(eventMeter.value);
            //Calculate where quicktime event meter is


            ResetEventMeter();
        }
    }
    void TickUpMeter()
    {
        eventMeter.value += eventSequence[eventSequenceCount].speed * Time.deltaTime;
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
    public void ResetEventMeter()
    {
        SetUpNextInSequence();
        eventMeter.value = 0;
        isEventOver = false;
        enabled = false;
    }
    void SetUpNextInSequence()
    {
        if(eventSequence.Count + eventSequenceCount < eventSequence.Count)
        {
            eventSequenceCount++;
        }
        else
        {
            Debug.Log("REACHED END");
        }
    }
}
