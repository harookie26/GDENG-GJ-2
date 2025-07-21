using UnityEngine;
using System.Collections;
using static EventNames;

public class CameraShake : MonoBehaviour
{
    [Header("Shake Parameters")]
    [SerializeField] private float shakeMagnitude = 0.1f;
    [SerializeField] private float shakeRotationMagnitude = 1.0f;

    private bool isShaking = false;
    private float elapsed = 0.0f;
    private float randomStartX, randomStartY, randomStartZ;

    // Variables to store the last applied offset
    private Vector3 lastPositionalOffset;
    private Quaternion lastRotationalOffset = Quaternion.identity;

    private void Awake()
    {
        EventBroadcaster.Instance.AddObserver(EnvironmentEvents.ON_SANITY_CRITICAL, OnCameraShake);
        EventBroadcaster.Instance.AddObserver(EnvironmentEvents.ON_ENVIRONMENT_RESET, ResetCamera);
    }

    private void OnDestroy()
    {
        EventBroadcaster.Instance.RemoveActionAtObserver(EnvironmentEvents.ON_SANITY_CRITICAL, OnCameraShake);
        EventBroadcaster.Instance.RemoveActionAtObserver(EnvironmentEvents.ON_ENVIRONMENT_RESET, ResetCamera);
    }

    private void OnCameraShake()
    {
        if (!isShaking)
        {
            isShaking = true;
            elapsed = 0.0f;
            randomStartX = Random.Range(-100f, 100f);
            randomStartY = Random.Range(-100f, 100f);
            randomStartZ = Random.Range(-100f, 100f);
            Debug.Log("Camera shaking!");
        }
    }

    private void ResetCamera()
    {
        if (isShaking)
        {
            isShaking = false;
        }
    }

    private void LateUpdate()
    {
        // Revert the previous frame's shake
        transform.localPosition -= lastPositionalOffset;
        transform.localRotation *= Quaternion.Inverse(lastRotationalOffset);

        // If shaking, calculate and apply the new shake for this frame
        if (isShaking)
        {
            elapsed += Time.deltaTime;

            // Calculate shake offsets
            float x = Mathf.PerlinNoise(randomStartX + elapsed * 10f, 0) * 2f - 1f;
            float y = Mathf.PerlinNoise(0, randomStartY + elapsed * 10f) * 2f - 1f;
            float z = Mathf.PerlinNoise(randomStartZ, randomStartZ + elapsed * 10f) * 2f - 1f;
            lastPositionalOffset = new Vector3(x, y, z) * shakeMagnitude;

            float rotX = Mathf.PerlinNoise(randomStartY + elapsed * 10f, randomStartZ + elapsed * 10f) * 2f - 1f;
            float rotY = Mathf.PerlinNoise(randomStartZ + elapsed * 10f, randomStartX + elapsed * 10f) * 2f - 1f;
            float rotZ = Mathf.PerlinNoise(randomStartX + elapsed * 10f, randomStartY + elapsed * 10f) * 2f - 1f;
            lastRotationalOffset = Quaternion.Euler(new Vector3(rotX, rotY, rotZ) * shakeRotationMagnitude);

            // Apply the new shake as an offset
            transform.localPosition += lastPositionalOffset;
            transform.localRotation *= lastRotationalOffset;
        }
        else
        {
            // If not shaking, ensure offsets are zeroed out
            lastPositionalOffset = Vector3.zero;
            lastRotationalOffset = Quaternion.identity;
        }
    }
}