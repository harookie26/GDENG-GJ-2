using UnityEngine;
using static EventNames;

[RequireComponent(typeof(Light))]
public class LightFlickering : MonoBehaviour
{
    [Header("Flicker Time Range (Global Bounds)")]
    public float minFlickerRange = 0.05f;
    public float maxFlickerRange = 0.3f;

    [SerializeField] private GameObject unlitLight;

    private Light flickerLight;
    private float minFlickerTime;
    private float maxFlickerTime;

    private Coroutine flickerCoroutine;

    private void OnEnable()
    {
        EventBroadcaster.Instance.AddObserver(EnvironmentEvents.ON_ENVIRONMENT_LIGHTSFLICKERING, StartFlickering);
        EventBroadcaster.Instance.AddObserver(EnvironmentEvents.ON_ENVIRONMENT_RESET, StopFlickering);
    }

    private void OnDisable()
    {
        EventBroadcaster.Instance.RemoveActionAtObserver(EnvironmentEvents.ON_ENVIRONMENT_LIGHTSFLICKERING, StartFlickering);
        EventBroadcaster.Instance.RemoveActionAtObserver(EnvironmentEvents.ON_ENVIRONMENT_RESET, StopFlickering);
    }

    private void StartFlickering()
    {
        if (flickerCoroutine == null)
        {
            flickerCoroutine = StartCoroutine(Flicker());
        }
    }

    private void StopFlickering()
    {
        if (flickerCoroutine != null)
        {
            StopCoroutine(flickerCoroutine);
            flickerCoroutine = null;
        }
        flickerLight.enabled = true; // Ensure light is on when not flickering
        unlitLight.SetActive(false); // Ensure unlit model is off
    }

    void Start()
    {
        unlitLight.SetActive(false); // Ensure the unlit light is off at start

        flickerLight = GetComponent<Light>();

        // Randomize unique flicker times for this instance
        minFlickerTime = Random.Range(minFlickerRange, maxFlickerRange * 0.7f);
        maxFlickerTime = Random.Range(minFlickerTime + 0.01f, maxFlickerRange);
    }

    System.Collections.IEnumerator Flicker()
    {
        while (true)
        {
            flickerLight.enabled = !flickerLight.enabled;
            unlitLight.SetActive(!flickerLight.enabled);

            float waitTime = Random.Range(minFlickerTime, maxFlickerTime);

            yield return new WaitForSeconds(waitTime);
        }
    }
}
