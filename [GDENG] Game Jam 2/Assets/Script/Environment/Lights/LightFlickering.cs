using UnityEngine;
using System.Collections.Generic;
using static EventNames;

public class LightFlickering : MonoBehaviour
{
    [Header("Flicker Time Range (Global Bounds)")]
    public float minFlickerRange = 0.05f;
    public float maxFlickerRange = 0.3f;

    // Store flicker coroutines for each light
    private Dictionary<Light, Coroutine> flickerCoroutines = new();

    private void OnEnable()
    {
        EventBroadcaster.Instance.AddObserver(EnvironmentEvents.ON_ENVIRONMENT_DELIRIOUS_MODE, StartFlickeringAll);
        EventBroadcaster.Instance.AddObserver(EnvironmentEvents.ON_ENVIRONMENT_RESET, StopFlickeringAll);
    }

    private void OnDisable()
    {
        EventBroadcaster.Instance.RemoveActionAtObserver(EnvironmentEvents.ON_ENVIRONMENT_DELIRIOUS_MODE, StartFlickeringAll);
        EventBroadcaster.Instance.RemoveActionAtObserver(EnvironmentEvents.ON_ENVIRONMENT_RESET, StopFlickeringAll);
    }

    private void StartFlickeringAll()
    {
        foreach (var lightObj in GameObject.FindGameObjectsWithTag("canFlicker"))
        {
            var light = lightObj.GetComponent<Light>();
            if (light != null && !flickerCoroutines.ContainsKey(light))
            {
                // Try to find an unlit model if present
                GameObject unlitLight = null;
                var unlitTransform = lightObj.transform.Find("UnlitLight");
                if (unlitTransform != null)
                    unlitLight = unlitTransform.gameObject;

                Coroutine coroutine = StartCoroutine(Flicker(light, unlitLight));
                flickerCoroutines[light] = coroutine;
            }
        }
    }

    private void StopFlickeringAll()
    {
        foreach (var kvp in flickerCoroutines)
        {
            if (kvp.Value != null)
                StopCoroutine(kvp.Value);

            kvp.Key.enabled = true; // Ensure light is on
            // Try to find and disable unlit model if present
            var unlitTransform = kvp.Key.transform.parent?.Find("UnlitLight");
            if (unlitTransform != null)
                unlitTransform.gameObject.SetActive(false);
        }
        flickerCoroutines.Clear();
    }

    System.Collections.IEnumerator Flicker(Light flickerLight, GameObject unlitLight)
    {
        // Randomize unique flicker times for this instance
        float minFlickerTime = Random.Range(minFlickerRange, maxFlickerRange * 0.7f);
        float maxFlickerTime = Random.Range(minFlickerTime + 0.01f, maxFlickerRange);

        if (unlitLight != null)
            unlitLight.SetActive(false);

        while (true)
        {
            flickerLight.enabled = !flickerLight.enabled;
            if (unlitLight != null)
                unlitLight.SetActive(!flickerLight.enabled);

            float waitTime = Random.Range(minFlickerTime, maxFlickerTime);
            yield return new WaitForSeconds(waitTime);
        }
    }
}
