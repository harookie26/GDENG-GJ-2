using System;
using NUnit.Framework.Internal;
using UnityEngine;

public class AddForce : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    private float gravityMultiplier;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gravityMultiplier = UnityEngine.Random.Range(0.0f, 2.0f);
    }

    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (rb != null)
        {
            // Apply extra gravity
            rb.AddForce(Physics.gravity * gravityMultiplier); // 2x normal gravity
        }
    }

}
