using UnityEngine;

public class StrangerListener : MonoBehaviour
{
    [SerializeField] Animator AM;
    void OnEnable()
    {
        Countdown.CountDownAlmostOver += StrangerReadyGun;
        QuickTimeEventMeter.OnSuccessfulHit += StrangerDeath;
        QuickTimeEventMeter.OnFailedHit += StrangerFireGun;
        GameStateManager.OnStartNextStandoff += StrangerReset;
        
    }
    void OnDisable()
    {
        Countdown.CountDownAlmostOver -= StrangerReadyGun;
        QuickTimeEventMeter.OnSuccessfulHit -= StrangerDeath;
        QuickTimeEventMeter.OnFailedHit -= StrangerFireGun;
        GameStateManager.OnStartNextStandoff -= StrangerReset;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AM = GetComponent<Animator>();
    }

    // Update is called once per frame
    void StrangerReadyGun() //Called same time player readies
    {
        AM.SetTrigger("Ready");
    }
    void StrangerFireGun() //Called when player misses
    {
        AM.SetTrigger("Shoot");
        //Invoke("Death",0.75f); //Temporary
    }
    void StrangerDeath() //Called when player hits safezone
    {
        AM.SetTrigger("Death");
    }
    void StrangerReset() //Called after cutscene
    {
        AM.SetTrigger("Reset");
    }
}
