using UnityEngine;
using static EventNames;

public class EnvironmentManager : MonoBehaviour
{
    private bool isLevitating;

    void Start()
    {   
        isLevitating = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!isLevitating)
            {
                isLevitating = true;
                EventBroadcaster.Instance.PostEvent(EnvironmentEvents.ON_ENVIRONMENT_LEVITATE);
                EventBroadcaster.Instance.PostEvent(EnvironmentEvents.ON_ENVIRONMENT_LIGHTSFLICKERING);
            }
            else
            {
                isLevitating = false;
                EventBroadcaster.Instance.PostEvent(EnvironmentEvents.ON_ENVIRONMENT_RESET);
            }
        }
    }
}
