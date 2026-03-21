using UnityEngine;

public class RevolverListener : MonoBehaviour
{
    Animator AM;
    void OnEnable()
    {
        Countdown.CountDownAlmostOver += ReadyGun;
        QuickTimeEventMeter.OnSuccessfulHit += FireGun;
        QuickTimeEventMeter.OnFailedHit += HideGun;
        GameStateManager.OnRetryCurrentEvent += Reset;
    }
    void OnDisable()
    {
        Countdown.CountDownAlmostOver -= ReadyGun;
        QuickTimeEventMeter.OnSuccessfulHit -= FireGun;
        QuickTimeEventMeter.OnFailedHit -= HideGun;
        GameStateManager.OnRetryCurrentEvent -= Reset;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AM = GetComponent<Animator>();
    }

    // Update is called once per frame
    void ReadyGun()
    {
        AM.SetTrigger("Ready");
    }
    void FireGun()
    {
        AM.SetTrigger("Shoot");
        Invoke("PutAwayGun",1f); //Temporary
    }
    void PutAwayGun()
    {
        AM.SetTrigger("PutAway");
    }
    void HideGun()
    {
        AM.SetTrigger("Hide");
    }
    void Reset()
    {
        AM.SetTrigger("Reset");
    }
}
