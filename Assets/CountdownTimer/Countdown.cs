using System;
using System.Runtime.Serialization;
using TMPro;
using UnityEngine;

public class Countdown : MonoBehaviour
{
    public static event Action CountDownOver;
    public static event Action CountDownAlmostOver;
    [SerializeField] TextMeshProUGUI TEXT;
    private bool isFinishedCounting = false;
    [SerializeField] float timeRemaining = 3;
    float seconds;
    bool IsLastSecond = false;

    void OnEnable()
    {
        QuickTimeEventMeter.OnSuccessfulHit += RestartClock;
    }
    void OnDisable()
    {
        QuickTimeEventMeter.OnSuccessfulHit -= RestartClock;
    }
    void Update()
    {
        if(!isFinishedCounting)
        {
            TickDownClock();
        }
    }
    void TickDownClock()
    {
        if (timeRemaining <= 0)
        {
            CountdownCompleted();
        }
        else
        {   
            DisplayTime();
            timeRemaining -= 1 * Time.deltaTime;
        }

        if (timeRemaining <= 1 && !IsLastSecond)
        {
            IsLastSecond = true;
            CountDownAlmostOver.Invoke();
        }
    }
    void CountdownCompleted()
    {
        isFinishedCounting = true;
        TEXT.text = "SHOOT";
        CountDownOver?.Invoke();
    }

    void DisplayTime()
    {
        if(timeRemaining >= 1)
        {
            seconds = Mathf.FloorToInt(timeRemaining % 60);
            TEXT.text = seconds.ToString() + "...";
        }
        else
        {
            TEXT.text = "Ready...";
            
        }
    }

    void RestartClock()
    {
        isFinishedCounting = false;
        timeRemaining = 4;

        IsLastSecond = false;
    }
}
