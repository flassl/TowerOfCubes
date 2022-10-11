using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreciseCopyPosition : MonoBehaviour
{
    [SerializeField]
    GameObject target;
    public Vector3 offset;
    private Rigidbody rigidBody;
    [Range(0.01f, 1f)]
    public float smoothFactor = 0.5f;

    private void Start()
    {
        rigidBody = target.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector3.Slerp(transform.position, rigidBody.position + offset, smoothFactor);
    }
}
