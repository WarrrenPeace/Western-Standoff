using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    public static event Action OnStartNextStandoff; //Call after dialogue box to reset enemy and start countdown
    public static event Action OnFinalStandoff; 
    [SerializeField] DeathMenu deathScreen;
    [SerializeField] DialogueManager dialogueScreen;
    [SerializeField] GameObject creditsScreen;
    private bool isReadyToRetry = false;
    private bool isReadyToContinue = false;
    float timebetweenStandoffs = 2f;
    void OnEnable()
    {
        QuickTimeEventMeter.OnSuccessfulHit += OnSuccess;
        QuickTimeEventMeter.OnFailedHit += OnDeath;
        QuickTimeEventMeter.OnSequenceCompleted += TriggerCreditsScreen;
    }
    void OnDisable()
    {
        QuickTimeEventMeter.OnSuccessfulHit -= OnSuccess;
        QuickTimeEventMeter.OnFailedHit -= OnDeath;
        QuickTimeEventMeter.OnSequenceCompleted -= TriggerCreditsScreen;
    }
    void OnSuccess()
    {
        if(!QuickTimeEventMeter.instance.SequenceCompleted)
        {
            if(!QuickTimeEventMeter.instance.finalInSequenceReached)
            {
                Invoke("AfterDelayStartDialogue",timebetweenStandoffs); //I want to delay the time between the gunshot and the popup so dead body is visible
            }
            else
            {
                AfterDelayStartDialogue();//For final standoff i want dialogue instantly
            }
        }
        else
        {
            
        }
        
    }
    void AfterDelayStartDialogue()
    {
        //Trigger cutscene
        dialogueScreen.gameObject.SetActive(true);
        dialogueScreen.FillDialogue(QuickTimeEventMeter.instance.eventSequenceOrder[QuickTimeEventMeter.instance.eventSequenceCount]);
        
        //Change background for final round
        if(QuickTimeEventMeter.instance.finalInSequenceReached) {OnFinalStandoff.Invoke();}
        Invoke("ReadyToResumeCooldown",0.5f);
        //Trigger countdown for next standoff
    }
    void OnDeath()
    {
        Invoke("AllowFadeOutBeforeDeathMenu",1);
        
    }
    void AllowFadeOutBeforeDeathMenu()
    {
        deathScreen.gameObject.SetActive(true);
        deathScreen.FillMoralityText(QuickTimeEventMeter.instance.eventSequenceOrder[QuickTimeEventMeter.instance.eventSequenceCount]);
        Invoke("ReadyToRetryCooldown",0.5f);
    }
    void ReadyToRetryCooldown() //Ready to restart entire game
    {
        isReadyToRetry = true;
    }
    void ReadyToResumeCooldown() //Ready to continue to next event
    {
        isReadyToContinue = true;
    }
    void Update()
    {
        if(Keyboard.current.spaceKey.isPressed && isReadyToRetry)
        {
            RestartGame();
        }
        else if(Keyboard.current.spaceKey.isPressed && isReadyToContinue)
        {
            StartNextStandoff();
        }
    }
    void RestartGame()
    {
        isReadyToRetry = false;
        SceneManager.LoadScene(1);
    }
    void StartNextStandoff()
    {
        if(!QuickTimeEventMeter.instance.SequenceCompleted)
        {
            isReadyToContinue = false;
            dialogueScreen.gameObject.SetActive(false);
            OnStartNextStandoff.Invoke();
        }
        else //Dont start another standoff if the sequence is completed
        {
            Debug.Log("Continue with ending");
            //TriggerCreditsScreen();
        }
        
    }
    void TriggerCreditsScreen()
    {
        creditsScreen.SetActive(true);
    }
}
