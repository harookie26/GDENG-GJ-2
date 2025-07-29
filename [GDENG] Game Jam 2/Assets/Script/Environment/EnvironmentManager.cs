using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal; // Use URP namespace
using static EventNames;

public class EnvironmentManager : MonoBehaviour
{
    private bool deliriousMode;
    private float sanityMeter;
    private bool isSanityCritical = false;

    [SerializeField] private float maxSanity = 100f;
    [SerializeField] private float sanityDepletionRate = 10f;
    [SerializeField] private float sanityRegenRate = 5f;
    [SerializeField] private float sanityCriticalThreshold = 20f;

    [Header("Post Processing")]
    [SerializeField] private Volume volume;
    private Vignette vignette;

    private void Awake()
    {
        EventBroadcaster.Instance.AddObserver(EnvironmentEvents.ON_ENVIRONMENT_DELIRIOUS_MODE, () => deliriousMode = true);
        EventBroadcaster.Instance.AddObserver(EnvironmentEvents.ON_ENVIRONMENT_RESET, () => deliriousMode = false);
    }

    private void OnDestroy()
    {
        EventBroadcaster.Instance.RemoveActionAtObserver(EnvironmentEvents.ON_ENVIRONMENT_DELIRIOUS_MODE, () => deliriousMode = true);
        EventBroadcaster.Instance.RemoveActionAtObserver(EnvironmentEvents.ON_ENVIRONMENT_RESET, () => deliriousMode = false);
    }
    void Start()
    {
        deliriousMode = false;
        sanityMeter = maxSanity;
        EventBroadcaster.Instance.PostEvent(EnvironmentEvents.ON_ENVIRONMENT_RESET);

        // Get the Vignette effect from the profile
        if (volume != null && volume.profile.TryGet<Vignette>(out vignette))
        {
            vignette.intensity.value = 0f; // Start with no vignette
            vignette.color.value = Color.red;
        }
    }

    void Update()
    {
        if (deliriousMode)
        {
            sanityMeter -= sanityDepletionRate * Time.deltaTime;

            if (!isSanityCritical && (sanityMeter / maxSanity) * 100f <= sanityCriticalThreshold)
            {
                isSanityCritical = true;
                EventBroadcaster.Instance.PostEvent(EnvironmentEvents.ON_SANITY_CRITICAL);
            }

            if (sanityMeter <= 0)
            {
                sanityMeter = 0;
                deliriousMode = false;
                EventBroadcaster.Instance.PostEvent(EnvironmentEvents.ON_ENVIRONMENT_RESET);
            }

            if (vignette != null)
            {
                float t = Mathf.Clamp01(sanityMeter / maxSanity);
                vignette.intensity.value = Mathf.Lerp(0.15f, 0.5f, 0.75f - t); // More intense as sanity drops
                //vignette.smoothness.value = 1f;
                //vignette.rounded.value = true;
            }
        }
        else
        {
            if (sanityMeter < maxSanity)
            {
                sanityMeter += sanityRegenRate * Time.deltaTime;
                sanityMeter = Mathf.Min(sanityMeter, maxSanity);

                if (isSanityCritical && (sanityMeter / maxSanity) * 100f > sanityCriticalThreshold)
                {
                    isSanityCritical = false;
                }
            }

            if (vignette != null)
            {
                vignette.intensity.value = 0f;
                vignette.smoothness.value = 1f;
                vignette.rounded.value = true;
            }
        }
    }
}
