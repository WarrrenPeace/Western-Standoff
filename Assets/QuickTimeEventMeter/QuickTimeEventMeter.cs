using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuickTimeEventMeter : MonoBehaviour
{
    public static QuickTimeEventMeter instance;
    public static event Action OnSuccessfulHit; //Hit within green
    public static event Action OnFailedHit; //Hit within red

    public bool finalInSequenceReached; //The very last standoff
    public static event Action OnSequenceCompleted; //Hit every safezone in all events
    public bool SequenceCompleted = false; //Should be public method that asks but i wont touch this

    Slider eventMeter;
    [SerializeField] Material eventMeterGraphicMaterial;
    [SerializeField] GameObject eventMeterGraphic;
    [SerializeField] Image eventMeterPointer;
    [SerializeField] Image spacebarPrompt;
    bool isEventHappening = false;
    public List<QuickTimeEventObject> eventSequenceOrder;
    public int eventSequenceCount = 0;
    void Awake()
    {
        instance = this;
    }
    void OnEnable()
    {
        Countdown.CountDownOver += StartQuickTimeEvent;
        Countdown.CountDownAlmostOver += ShowQuickTimeEventGraphic;
    }
    void OnDisable()
    {
        Countdown.CountDownOver -= StartQuickTimeEvent;
        Countdown.CountDownAlmostOver -= ShowQuickTimeEventGraphic;
    }

    void Start()
    {
        eventMeter = GetComponent<Slider>();
        eventMeter.value = 0;
        eventSequenceCount = 0;
    }
    void ShowQuickTimeEventGraphic() //Adding this because seeing the graphic beforehand i hope would feel better
    {
        if(!SequenceCompleted) //If Sequence is not completed keep looping
        {
            eventMeter.value = 0;

            eventMeterGraphic.SetActive(true);
            eventMeterPointer.enabled = true;
            SetupEventMeterGraphic();
        }
    }
    void StartQuickTimeEvent()
    {
        if(!SequenceCompleted) //If Sequence is not completed keep looping
        {
            eventMeter.value = 0;

            eventMeterGraphic.SetActive(true);
            eventMeterPointer.enabled = true;
            spacebarPrompt.enabled = true;
            SetupEventMeterGraphic();
            isEventHappening = true;
        }
    }    

    void Update()
    {
        if(isEventHappening)
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
        if(Keyboard.current.spaceKey.isPressed || Mouse.current.leftButton.isPressed)
        {
            isEventHappening = false;
            eventMeterGraphic.SetActive(false);
            eventMeterPointer.enabled = false;
            spacebarPrompt.enabled = false;

            //Calculate where quicktime event meter is
            if(CheckIfPlayerHitWithinSafeZone())
            { //PASSED
                PlayerHitSafeZone();
            }
            else
            { //FAILED
                PlayerHitRedZone();
            }
        }
    }
    bool CheckIfPlayerHitWithinSafeZone()
    {
        if(eventMeter.value>= eventSequenceOrder[eventSequenceCount].min_Max_For_SafeZone.x && eventMeter.value <= eventSequenceOrder[eventSequenceCount].min_Max_For_SafeZone.y)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    void TickUpMeter()
    {
        eventMeter.value += eventSequenceOrder[eventSequenceCount].speed * Time.deltaTime;
        if (eventMeter.value >= eventMeter.maxValue)
        {
            EventHasReachedMaxValue(); //End automatically
        }
    }
    void EventHasReachedMaxValue()
    {
        isEventHappening = false;
        eventMeterGraphic.SetActive(false);
        eventMeterPointer.enabled = false;
        spacebarPrompt.enabled = false;

        if(finalInSequenceReached)
        {
            PlayerHitSafeZone();
        }
        else
        {
            PlayerHitRedZone();
        }
        
        
    }
    void PlayerHitSafeZone()
    {
        isEventHappening = false;
        SetUpNextInSequence(); //Need to set up next sequence before calling event so i can use the proper 'QuickTimeEventObject'
        OnSuccessfulHit.Invoke();
    }
    void PlayerHitRedZone()
    {
        isEventHappening = false;
        OnFailedHit.Invoke(); //Trigger death
    }
    public void ResetEventSequence() //Call this to reset the chain of QT events
    {
        eventSequenceCount = 0;
    }
    void SetUpNextInSequence() //ENtering with 3
    {
        if(eventSequenceCount + 1 < eventSequenceOrder.Count) //Loop standoff
        {
            eventSequenceCount ++;
            if(eventSequenceCount == eventSequenceOrder.Count - 1) //This would be the LAST standoff
            {
                LastInSequenceReached();
            }
        }
        else //4+1 is 5 so neither condition is satisfied
        {
            eventSequenceCount ++;
            if(eventSequenceCount == eventSequenceOrder.Count)
            {
                LastInSequenceCompleted();
            }
        } 

    }
    void LastInSequenceReached()
    {
        finalInSequenceReached = true;
    }
    void LastInSequenceCompleted()
    {
        SequenceCompleted = true;
        OnSequenceCompleted.Invoke();
    }
}
