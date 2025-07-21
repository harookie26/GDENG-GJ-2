using UnityEngine;
using static EventNames;

public class DisableLight : MonoBehaviour
{
    private void Awake()
    {
        EventBroadcaster.Instance.AddObserver(EnvironmentEvents.ON_ENVIRONMENT_DELIRIOUS_MODE, DisableLightSource);
    }

    private void OnDestroy()
    {
        EventBroadcaster.Instance.RemoveActionAtObserver(EnvironmentEvents.ON_ENVIRONMENT_DELIRIOUS_MODE, DisableLightSource);
    }

    private void DisableLightSource()
    {
        // Disable the light component on this GameObject
        Light lightComponent = GetComponent<Light>();
        if (lightComponent != null)
        {
            lightComponent.enabled = false;
            Debug.Log("Light disabled due to delirius mode.");
        }
        else
        {
            Debug.LogWarning("No Light component found on this GameObject.");
        }
    }
}
