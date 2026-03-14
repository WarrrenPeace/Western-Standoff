using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    [SerializeField] GameObject deathScreen;
    private bool isReadyToResume = false;
    void OnEnable()
    {
        QuickTimeEventMeter.OnFailedHit += OnDeath;
    }
    void OnDisable()
    {
        QuickTimeEventMeter.OnFailedHit -= OnDeath;
    }

    void OnDeath()
    {
        deathScreen.SetActive(true);
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
        SceneManager.LoadScene("Shootout");
    }
}
