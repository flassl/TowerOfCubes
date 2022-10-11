using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddConstantVelocity : MonoBehaviour
{
    [SerializeField]
    Vector3 forceVector;

    [SerializeField]
    float force;

    [SerializeField]
    KeyCode keyPositive;

    [SerializeField]
    KeyCode keyNegaive;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(keyPositive))
            GetComponent<Rigidbody>().velocity += forceVector * force;
        if (Input.GetKey(keyNegaive))
            GetComponent<Rigidbody>().velocity -= forceVector * force;
    }
}
