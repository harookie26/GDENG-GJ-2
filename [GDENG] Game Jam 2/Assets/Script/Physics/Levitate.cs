using Unity.VisualScripting;
using UnityEngine;
using static EventNames;

public class Levitate : MonoBehaviour
{
    [SerializeField] private float levitateHeight = 2f;
    [SerializeField] private float rotationSpeed = 30f;
    [SerializeField] private Vector3 rotationAxis = new Vector3(0.5f, 1f, 0.2f);

    private Vector3 initialPosition;
    private Quaternion initialRotation;

    private bool isLevitating;
    private Vector3 targetPosition;

    private void Awake()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        targetPosition = new Vector3(initialPosition.x, initialPosition.y + levitateHeight, initialPosition.z);
    }

    private void OnEnable()
    {
        EventBroadcaster.Instance.AddObserver(EnvironmentEvents.ON_ENVIRONMENT_DELIRIOUS_MODE, LevitateBroadcast);
        EventBroadcaster.Instance.AddObserver(EnvironmentEvents.ON_ENVIRONMENT_RESET, ResetObject);
    }

    private void OnDisable()
    {
        EventBroadcaster.Instance.RemoveActionAtObserver(EnvironmentEvents.ON_ENVIRONMENT_DELIRIOUS_MODE, LevitateBroadcast);
        EventBroadcaster.Instance.RemoveActionAtObserver(EnvironmentEvents.ON_ENVIRONMENT_RESET, ResetObject);
    }

    void Update()
    {
        if (isLevitating)
        {
            LevitateObject();
        }
        else
        {
            // Reset position and rotation if not levitating
            if (transform.position != initialPosition)
            {
                transform.position = initialPosition;
                transform.rotation = initialRotation;
            }
        }
    }

    private void LevitateBroadcast()
    {
        isLevitating = true;
        // Instantly move to the target levitation height
        transform.position = targetPosition;
    }

    private void ResetObject()
    {
        isLevitating = false;
    }

    private void LevitateObject()
    {
        // Rotate the object around the specified axis
        if (rotationAxis.magnitude > 0)
        {
            transform.Rotate(rotationAxis.normalized, rotationSpeed * Time.deltaTime);
        }
    }
}
