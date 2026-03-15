using UnityEngine;

public class BackgroundListener : MonoBehaviour
{
    Animator AM;
    void OnEnable()
    {
        QuickTimeEventMeter.OnFailedHit += FadeToBlack;
    }
    void OnDisable()
    {
        QuickTimeEventMeter.OnFailedHit -= FadeToBlack;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AM = GetComponent<Animator>();
    }
    // Update is called once per frame
    void FadeToBlack()
    {
        AM.SetTrigger("Fade");
    }
}
