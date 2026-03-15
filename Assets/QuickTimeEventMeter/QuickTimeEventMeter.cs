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

    public static event Action OnSequenceCompleted; //Hit every safezone in all events
    bool SequenceCompleted = false;

    Slider eventMeter;
    [SerializeField] Material eventMeterGraphicMaterial;
    [SerializeField] GameObject eventMeterGraphic;
    [SerializeField] Image eventMeterPointer;
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
    }
    void OnDisable()
    {
        Countdown.CountDownOver -= StartQuickTimeEvent;
    }

    void Start()
    {
        eventMeter = GetComponent<Slider>();
        eventMeter.value = 0;
        eventSequenceCount = 0;
    }
    void StartQuickTimeEvent()
    {
        if(!SequenceCompleted) //If Sequence is not completed keep looping
        {
            eventMeter.value = 0;

            eventMeterGraphic.SetActive(true);
            eventMeterPointer.enabled = true;
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
if(Keyboard.current.tabKey.isPressed) //DEBUG
{
    SceneManager.LoadScene(0);
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
            isEventHappening = false;
            eventMeterGraphic.SetActive(false);
            eventMeterPointer.enabled = false;
            //Calculate where quicktime event meter is
            if(eventMeter.value>= eventSequenceOrder[eventSequenceCount].min_Max_For_SafeZone.x && eventMeter.value <= eventSequenceOrder[eventSequenceCount].min_Max_For_SafeZone.y)
            { //PASSED
                PlayerHitSafeZone();
            }
            else
            { //FAILED
                PlayerHitRedZone();
            }
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
        OnFailedHit.Invoke(); //Trigger death
    }
    void PlayerHitSafeZone()
    {
        isEventHappening = false;
        OnSuccessfulHit.Invoke(); //Trigger next cutscene before next event

        SetUpNextInSequence();
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
    void SetUpNextInSequence()
    {
        if(eventSequenceCount + 1 < eventSequenceOrder.Count)
        {
            eventSequenceCount ++;
        }
        else if (eventSequenceCount + 1 >= eventSequenceOrder.Count) //This would be the end of the game
        {
            LastInSequenceReached();
        }
    }
    void LastInSequenceReached()
    {
        SequenceCompleted = true;
        OnSequenceCompleted.Invoke();
    }
}
