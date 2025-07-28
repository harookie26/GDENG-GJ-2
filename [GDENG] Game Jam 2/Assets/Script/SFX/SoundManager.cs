using UnityEngine;
using UnityEngine.Rendering;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("SFX Clips")]
    public AudioClip WalkingSFX;
    public AudioClip DeliriousSFX;
    public AudioClip OutOfBreathSFX;
    public AudioClip DoorOpeningSFX;
    public AudioClip DoorClosingSFX;
    public AudioClip DoorKeyUnlockSFX;
    public AudioClip DoorLockedSFX;
    public AudioClip ToiletFlushSFX;
    public AudioClip FaucetOpenSFX;
    public AudioClip LeverPullSFX;
    public AudioClip GeneratorSFX;
    public AudioClip MorgueDrawerOpenSFX;
    public AudioClip MorgueDrawerCloseSFX;
    public AudioClip PaperRufflingSFX;
    public AudioClip AsylumAmbientSFX;

    private AudioSource audioSource;
    private AudioSource loopAudioSource;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(gameObject);
        }

        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        //Looping SFX
        loopAudioSource = gameObject.AddComponent<AudioSource>();
        loopAudioSource.loop = true;
        loopAudioSource.playOnAwake = false;    //Prevent SFX from playing immediately
    }

    public void PlaySFX(AudioClip clip, float volume)
    {
        if (clip != null)
        {
            audioSource.PlayOneShot(clip, Mathf.Clamp01(volume));
        }

        else
        {
            Debug.LogWarning("Null AudioClip.");
        }
    }

    public void PlayWalkingSFX() => PlaySFX(WalkingSFX, 0.5f); //50% volume
    public void PlayeDeliriousSFX() => PlaySFX(DeliriousSFX, 0.65f); //65% volume
    public void PlayOutOfBreathSFX() => PlaySFX(OutOfBreathSFX, 0.6f); //60% volume
    public void PlayDoorOpeningSFX() => PlaySFX(DoorOpeningSFX, 0.7f); //70% volume
    public void PlayDoorClosingSFX() => PlaySFX(DoorClosingSFX, 0.7f); //70% volume
    public void PlayDoorKeyUnlockSFX() => PlaySFX(DoorKeyUnlockSFX, 0.8f); //80% volume
    public void PlayDoorLockedSFX() => PlaySFX(DoorLockedSFX, 0.7f); //70% volume
    public void PlayToiletFlushSFX() => PlaySFX(ToiletFlushSFX, 0.6f); //60% volume
    public void PlayFaucetOpenSFX() => PlaySFX(FaucetOpenSFX, 0.75f); //75% volume
    public void PlayLeverPullSFX() => PlaySFX(LeverPullSFX, 0.75f); //75% volume
    public void PlayGeneratorSFX() => PlaySFX(GeneratorSFX, 0.5f); //50% volume
    public void PlayMorgueDrawerOpenSFX() => PlaySFX(MorgueDrawerOpenSFX, 0.65f); //65% volume
    public void PlayMorgueDrawerCloseSFX() => PlaySFX(MorgueDrawerCloseSFX, 0.65f); //65% volume
    public void PlayPaperRufflingSFX() => PlaySFX(PaperRufflingSFX, 0.75f); //75% volume
    public void PlayAsylumAmbientSFX() => PlaySFX(AsylumAmbientSFX, 0.55f); //55% volume

    //public void StartBelow20HPSFXLoop()
    //{
    //    if (Below20HPSFX != null && !loopAudioSource.isPlaying)
    //    {
    //        loopAudioSource.clip = Below20HPSFX;
    //        loopAudioSource.volume = 0.5f;      //50% volume (adjust as needed)
    //        loopAudioSource.Play();
    //    }
    //}

    //public void StopBelow20HPSFXLoop()
    //{
    //    if (loopAudioSource.isPlaying)
    //    {
    //        loopAudioSource.Stop();
    //    }
    //}
}