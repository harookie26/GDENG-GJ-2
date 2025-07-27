using UnityEngine;
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

    }

    void Update()
    {
        if (deliriousMode)
        {
            sanityMeter -= sanityDepletionRate * Time.deltaTime;
           // Debug.Log($"Sanity: {sanityMeter}");

            // Check if sanity has dropped below the critical threshold
            if (!isSanityCritical && (sanityMeter / maxSanity) * 100f <= sanityCriticalThreshold)
            {
                isSanityCritical = true;
                EventBroadcaster.Instance.PostEvent(EnvironmentEvents.ON_SANITY_CRITICAL);
                // Debug.Log("Sanity has reached a critical level!");
            }

            if (sanityMeter <= 0)
            {
                sanityMeter = 0;
                deliriousMode = false;
                EventBroadcaster.Instance.PostEvent(EnvironmentEvents.ON_ENVIRONMENT_RESET);
                //Debug.Log("Sanity depleted, returning to normal.");
            }
        }
        else
        {
            if (sanityMeter < maxSanity)
            {
                sanityMeter += sanityRegenRate * Time.deltaTime;
                sanityMeter = Mathf.Min(sanityMeter, maxSanity); // Ensure sanity doesn't exceed max
                //Debug.Log($"Sanity (Regenerating): {sanityMeter}");

                // Reset critical state once sanity has regenerated above the threshold
                if (isSanityCritical && (sanityMeter / maxSanity) * 100f > sanityCriticalThreshold)
                {
                    isSanityCritical = false;
                }
            }
        }
    }
}
