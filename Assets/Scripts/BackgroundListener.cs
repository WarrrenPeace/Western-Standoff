using UnityEngine;
using UnityEngine.UI;

public class BackgroundListener : MonoBehaviour
{
    Animator AM;
    Image IM;
    [SerializeField] Sprite townOfBluebonnet;
    [SerializeField] Sprite theMirror;
    void OnEnable()
    {
        QuickTimeEventMeter.OnFailedHit += FadeToBlack;
        GameStateManager.OnFinalStandoff += SetToMirror;
        GameStateManager.OnRetryCurrentEvent += Reset;
    }
    void OnDisable()
    {
        QuickTimeEventMeter.OnFailedHit -= FadeToBlack;
        GameStateManager.OnFinalStandoff -= SetToMirror;
        GameStateManager.OnRetryCurrentEvent -= Reset;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        IM = GetComponent<Image>();
        AM = GetComponent<Animator>();

    }
    // Update is called once per frame
    void FadeToBlack()
    {
        AM.SetTrigger("Fade");
    }
    void SetToMirror()
    {
        IM.sprite =theMirror;
    }
    void Reset()
    {
        AM.SetTrigger("Reset");
    }
}
