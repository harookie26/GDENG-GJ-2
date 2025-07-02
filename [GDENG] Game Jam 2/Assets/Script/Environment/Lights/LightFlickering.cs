using UnityEngine;

[RequireComponent(typeof(Light))]
public class LightFlickering : MonoBehaviour
{
    [Header("Flicker Time Range (Global Bounds)")]
    public float minFlickerRange = 0.05f;
    public float maxFlickerRange = 0.3f;

    private Light flickerLight;
    private float minFlickerTime;
    private float maxFlickerTime;

    void Start()
    {
        flickerLight = GetComponent<Light>();

        // Randomize unique flicker times for this instance
        minFlickerTime = Random.Range(minFlickerRange, maxFlickerRange * 0.7f);
        maxFlickerTime = Random.Range(minFlickerTime + 0.01f, maxFlickerRange);

        StartCoroutine(Flicker());
    }

    System.Collections.IEnumerator Flicker()
    {
        while (true)
        {
            flickerLight.enabled = !flickerLight.enabled;
            float waitTime = Random.Range(minFlickerTime, maxFlickerTime);
            yield return new WaitForSeconds(waitTime);
        }
    }
}
