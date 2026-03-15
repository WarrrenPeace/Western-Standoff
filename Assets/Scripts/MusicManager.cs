using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;
    [SerializeField] AudioSource AS;
    [SerializeField] AudioClip StartRun;
    [SerializeField] AudioClip DieDuringRun;
    [SerializeField] AudioClip CompletedRun;

    void Awake()
    {
        if(!instance) 
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void OnEnable()
    {
        //QuickTimeEventMeter.OnSuccessfulHit += OnSuccess;
        QuickTimeEventMeter.OnFailedHit += PlayDieDuringRun;
        QuickTimeEventMeter.OnSequenceCompleted += PlayGameOverTrack;
    }
    void OnDisable()
    {
        //QuickTimeEventMeter.OnSuccessfulHit -= OnSuccess;
        QuickTimeEventMeter.OnFailedHit -= PlayDieDuringRun;
        QuickTimeEventMeter.OnSequenceCompleted -= PlayGameOverTrack;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(!AS)AS = GetComponent<AudioSource>();
        PlayStartRun();
    }
    void PlayStartRun()
    {
        AS.PlayOneShot(StartRun);
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
