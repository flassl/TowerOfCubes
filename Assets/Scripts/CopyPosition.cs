using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyPosition : MonoBehaviour
{
    [SerializeField]
    Transform targetTransform;
    public Vector3 offset;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = targetTransform.position + offset;
    }
}
