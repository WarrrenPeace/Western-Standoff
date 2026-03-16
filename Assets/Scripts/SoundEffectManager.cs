using UnityEngine;

public class SoundEffectManager : MonoBehaviour
{
    public static SoundEffectManager instance;
    [SerializeField] AudioSource AS;
    [SerializeField] AudioClip RevolverHolster;
    [SerializeField] AudioClip RevolverShoot;
    [SerializeField] AudioClip RevolverSpin;
    [SerializeField] AudioClip DialogueVoice;
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
        QuickTimeEventMeter.OnSuccessfulHit += OnSuccessfulHitSFX;
        Countdown.CountDownAlmostOver += CountDownAlmostOverSFX;
        QuickTimeEventMeter.OnFailedHit += OnFailedHitSFX;
    }
    void OnDisable()
    {
        QuickTimeEventMeter.OnSuccessfulHit -= OnSuccessfulHitSFX;
        Countdown.CountDownAlmostOver -= CountDownAlmostOverSFX;
        QuickTimeEventMeter.OnFailedHit -= OnFailedHitSFX;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public void CountDownAlmostOverSFX()
    {
        AS.PlayOneShot(RevolverHolster);
    }
    public void OnSuccessfulHitSFX()
    {
        AS.PlayOneShot(RevolverShoot,0.4f);
    }
    public void OnFailedHitSFX()
    {
        AS.PlayOneShot(RevolverShoot,0.2f);
        AS.PlayOneShot(RevolverSpin);
    }
    public void OnOpenDialogue()
    {
        AS.PlayOneShot(DialogueVoice);

    }
    
}
