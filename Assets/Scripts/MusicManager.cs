using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;
    AudioSource AS;
    [SerializeField] AudioClip StartRun;
    [SerializeField] AudioClip DieDuringRun;
    [SerializeField] AudioClip CompletedRun;

    void Awake()
    {
        instance = this;
    }
    void OnEnable()
    {
        //QuickTimeEventMeter.OnSuccessfulHit += OnSuccess;
        QuickTimeEventMeter.OnFailedHit += PlayDieDuringRun;
    }
    void OnDisable()
    {
        //QuickTimeEventMeter.OnSuccessfulHit -= OnSuccess;
        QuickTimeEventMeter.OnFailedHit -= PlayDieDuringRun;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(!AS)AS = GetComponent<AudioSource>();
    }
    void PlayDieDuringRun()
    {
        AS.PlayOneShot(DieDuringRun);
    }

    void PlayGameOverTrack()
    {
        AS.PlayOneShot(CompletedRun);
    }
}
