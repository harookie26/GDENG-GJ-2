using UnityEngine;

public class Levitate : MonoBehaviour
{
    [SerializeField] private float levitateRadius = 5f;
    [SerializeField] private float levitateForce = 10f;
    [SerializeField] private LayerMask cubeLayer;

    void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            LevitateNearbyCubes();
        }
    }

    private void LevitateNearbyCubes()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, levitateRadius, cubeLayer);
        foreach (var collider in colliders)
        {
            Rigidbody rb = collider.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(Vector3.up * levitateForce, ForceMode.Force);

                // Add a small random torque for realistic rotation
                Vector3 randomTorque = new Vector3(
                    Random.Range(-1f, 1f),
                    Random.Range(-1f, 1f),
                    Random.Range(-1f, 1f)
                ) * 2f; // Adjust multiplier for more/less rotation
                rb.AddTorque(randomTorque, ForceMode.Force);
            }
        }
    }
}
