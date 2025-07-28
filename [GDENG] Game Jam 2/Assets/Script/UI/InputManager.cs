using UnityEngine;
using static EventNames;

public class InputManager : MonoBehaviour
{
    private bool deliriousMode;

    void Start()
    {
        deliriousMode = false;
        EventBroadcaster.Instance.PostEvent(EnvironmentEvents.ON_ENVIRONMENT_RESET);
    }

    void Update()
   {
        if (Input.GetKeyDown(KeyCode.F))
        {
            EventBroadcaster.Instance.PostEvent(ItemEvents.ON_ITEM_INTERACT);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!deliriousMode)
            {
                deliriousMode = true;
                EventBroadcaster.Instance.PostEvent(EnvironmentEvents.ON_ENVIRONMENT_DELIRIOUS_MODE);
            }
            else
            {
                deliriousMode = false;
                EventBroadcaster.Instance.PostEvent(EnvironmentEvents.ON_ENVIRONMENT_RESET);
            }
        }
    }
}
