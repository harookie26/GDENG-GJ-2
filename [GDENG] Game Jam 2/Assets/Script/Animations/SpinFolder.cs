using UnityEngine;

public class SpinFolder : MonoBehaviour
{
    [SerializeField] private float spinSpeed = 10f; // degrees per second
    private Vector3 spinAxis;

    void Awake()
    {
        // Pick a random normalized axis
        spinAxis = Random.onUnitSphere;
    }

    void Update()
    {
        transform.Rotate(spinAxis, spinSpeed * Time.deltaTime);
    }
}
