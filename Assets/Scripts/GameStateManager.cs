using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    [SerializeField] GameObject deathScreen;
    private bool isReadyToResume = false;
    void OnEnable()
    {
        QuickTimeEventMeter.OnSuccessfulHit += OnSuccess;
        QuickTimeEventMeter.OnFailedHit += OnDeath;
    }
    void OnDisable()
    {
        QuickTimeEventMeter.OnSuccessfulHit -= OnSuccess;
        QuickTimeEventMeter.OnFailedHit -= OnDeath;
    }
    void Start()
    {
        //deathScreen = GameObject.FindGameObjectWithTag("DeathUI");
    }
    void OnSuccess()
    {
        //Trigger cutscene

        //Trigger countdown for next standoff
    }

    void OnDeath()
    {
        deathScreen.SetActive(true);
        Invoke("ReadyToResumeCooldown",0.5f);
    }
    void ReadyToResumeCooldown()
    {
        isReadyToResume = true;
    }
    void Update()
    {
        if(Keyboard.current.spaceKey.isPressed && isReadyToResume)
        {
            RestartGame();
        }
    }
    void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}
