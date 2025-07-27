using System.Collections.Generic;
using UnityEngine;
using static EventNames;

public class DisableOnDelirious : MonoBehaviour
{
    [SerializeField] private List<GameObject> objectsToDisable;

    private void OnEnable()
    {
        EventBroadcaster.Instance.AddObserver(EnvironmentEvents.ON_ENVIRONMENT_DELIRIOUS_MODE, DisableObject);
        EventBroadcaster.Instance.AddObserver(EnvironmentEvents.ON_ENVIRONMENT_RESET, EnableObject);
    }

    private void OnDisable()
    {
        EventBroadcaster.Instance.RemoveActionAtObserver(EnvironmentEvents.ON_ENVIRONMENT_DELIRIOUS_MODE, DisableObject);
        EventBroadcaster.Instance.RemoveActionAtObserver(EnvironmentEvents.ON_ENVIRONMENT_RESET, EnableObject);
    }

    private void DisableObject()
    {
        foreach (var obj in objectsToDisable)
        {
            if (obj != null)
            {
                obj.SetActive(false);
            }
        }
    }

    private void EnableObject()
    {
        foreach (var obj in objectsToDisable)
        {
            if (obj != null)
            {
                obj.SetActive(true);
            }
        }
    }

}
