using UnityEngine;

public class SoundEffectManager : MonoBehaviour
{
    public static SoundEffectManager instance;
    AudioSource AS;
    [SerializeField] AudioClip RevolverHolster;
    [SerializeField] AudioClip RevolverShoot;
    [SerializeField] AudioClip RevolverSpin;
    void Awake()
    {
        instance = this;
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
        Countdown.CountDownAlmostOver += CountDownAlmostOverSFX;
        QuickTimeEventMeter.OnFailedHit -= OnFailedHitSFX;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(!AS)AS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public void CountDownAlmostOverSFX()
    {
        AS.PlayOneShot(RevolverHolster);
    }
    public void OnSuccessfulHitSFX()
    {
        AS.PlayOneShot(RevolverShoot);
    }
    public void OnFailedHitSFX()
    {
        AS.PlayOneShot(RevolverShoot,0.1f);
        AS.PlayOneShot(RevolverSpin);
    }
    
}
