using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

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
    public AudioClip PowerDownSFX;
    public AudioClip ShortCircuitSFX;
    public AudioClip ComputerStartSFX;

    private AudioSource audioSource;
    private AudioSource loopAudioSource;
    private AudioSource ambientAudioSource;
    private AudioSource walkingAudioSource;
    private AudioSource loopDeliriousSource;

    [SerializeField] private AudioSource generator3DAudioSource;

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

        //Ambient Audio Source
        ambientAudioSource = gameObject.AddComponent<AudioSource>();
        ambientAudioSource.loop = true;
        ambientAudioSource.playOnAwake = false;
        ambientAudioSource.spatialBlend = 0f;       //2D sound

        //Walking Audio Source
        walkingAudioSource = gameObject.AddComponent<AudioSource>();
        walkingAudioSource.loop = true;
        walkingAudioSource.playOnAwake = false;
        walkingAudioSource.spatialBlend = 0f;       //2D sound

        //Delirious Audio Source
        loopDeliriousSource = gameObject.AddComponent<AudioSource>();
        loopDeliriousSource.loop = true;
        loopDeliriousSource.playOnAwake = false;
        loopDeliriousSource.spatialBlend = 0f;     //2D sound
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainGame")
        {
            //Start the ambient loop for the asylum scene
            PlayAsylumAmbientLoop();
        }
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

    public void PlayOutOfBreathSFX() => PlaySFX(OutOfBreathSFX, 0.6f); //60% volume
    public void PlayDoorOpeningSFX() => PlaySFX(DoorOpeningSFX, 0.7f); //70% volume
    public void PlayDoorClosingSFX() => PlaySFX(DoorClosingSFX, 0.7f); //70% volume
    public void PlayDoorKeyUnlockSFX() => PlaySFX(DoorKeyUnlockSFX, 0.8f); //80% volume
    public void PlayDoorLockedSFX() => PlaySFX(DoorLockedSFX, 0.7f); //70% volume
    public void PlayToiletFlushSFX() => PlaySFX(ToiletFlushSFX, 0.6f); //60% volume
    public void PlayFaucetOpenSFX() => PlaySFX(FaucetOpenSFX, 0.75f); //75% volume
    public void PlayLeverPullSFX() => PlaySFX(LeverPullSFX, 0.75f); //75% volume
    public void PlayMorgueDrawerOpenSFX() => PlaySFX(MorgueDrawerOpenSFX, 0.65f); //65% volume
    public void PlayMorgueDrawerCloseSFX() => PlaySFX(MorgueDrawerCloseSFX, 0.65f); //65% volume
    public void PlayPaperRufflingSFX() => PlaySFX(PaperRufflingSFX, 0.55f); //75% volume
    public void PlayAsylumAmbientSFX() => PlaySFX(AsylumAmbientSFX, 0.55f); //55% volume
    public void PlayShortCircuitSFX() => PlaySFX(ShortCircuitSFX, 0.8f); //80% volume
    public void PlayComputerStartSFX() => PlaySFX(ComputerStartSFX, 0.7f); //70% volume


    //public void PlayPowerDownSFX() => PlaySFX(PowerDownSFX, 0.7f); //70% volume
    //public void PlayGeneratorSFX() => PlaySFX(GeneratorSFX, 0.5f); //50% volume
    //public void PlayeDeliriousSFX() => PlaySFX(DeliriousSFX, 0.65f); //65% volume
    //public void PlayWalkingSFX() => PlaySFX(WalkingSFX, 0.5f); //50% volume
    public void Start3DGeneratorLoop()
    {
        if (generator3DAudioSource != null && !generator3DAudioSource.isPlaying)
        {
            generator3DAudioSource.clip = GeneratorSFX;
            generator3DAudioSource.loop = true;
            generator3DAudioSource.volume = 0.5f;
            generator3DAudioSource.spatialBlend = 1f; // Fully 3D
            generator3DAudioSource.Play();
        }
    }

    public void PlayAsylumAmbientLoop()
    {
        if (AsylumAmbientSFX != null && !ambientAudioSource.isPlaying)
        {
            ambientAudioSource.clip = AsylumAmbientSFX;
            ambientAudioSource.loop = true;
            ambientAudioSource.spatialBlend = 0f;       //2D sound
            ambientAudioSource.volume = 0.2f;           //Adjust for subtle ambience
            ambientAudioSource.Play();
        }
    }

    public void StopAsylumAmbientLoop()
    {
        if (ambientAudioSource.isPlaying)
        {
            ambientAudioSource.Stop();
        }
    }

    private bool isWalkingLoopPlaying = false;

    public void StartWalkingLoop(bool isSprinting = false)
    {
        if (!isWalkingLoopPlaying && WalkingSFX != null)
        {
            loopAudioSource.clip = WalkingSFX;
            loopAudioSource.loop = true;
            loopAudioSource.spatialBlend = 0f; // 2D Sound
            loopAudioSource.volume = 0.5f;

            loopAudioSource.pitch = isSprinting ? 1.4f : 1.0f; // Slightly faster pitch for sprinting
            loopAudioSource.Play();
            isWalkingLoopPlaying = true;
        }
        else if (isWalkingLoopPlaying)
        {
            // If already playing, just update the pitch if sprint status changes
            loopAudioSource.pitch = isSprinting ? 1.4f : 1.0f;
        }
    }

    public void StopWalkingLoop()
    {
        if (isWalkingLoopPlaying)
        {
            loopAudioSource.Stop();
            isWalkingLoopPlaying = false;
        }
    }

    public void StartDeliriousLoop()
    {
        if (!loopDeliriousSource.isPlaying && DeliriousSFX != null)
        {
            loopDeliriousSource.clip = DeliriousSFX;
            loopDeliriousSource.loop = true;
            loopDeliriousSource.spatialBlend = 0f;  //2D Sound
            loopDeliriousSource.volume = 0.65f;     //Adjust volume as needed
            loopDeliriousSource.Play();
        }
    }

    public void StopDeliriousLoop()
    {
        if (loopDeliriousSource.isPlaying)
        {
            loopDeliriousSource.Stop();
        }
    }
}