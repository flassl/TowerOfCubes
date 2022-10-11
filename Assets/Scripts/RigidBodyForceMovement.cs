using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RigidBodyForceMovement : MonoBehaviour
{
    public float force;
    public Transform cameraTransform;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        if (direction.magnitude > 0.01f)
        {
            Quaternion cameraBasis = cameraTransform.rotation.normalized;
            direction = cameraBasis * direction;
            GetComponent<Rigidbody>().AddForce(direction * force * Time.deltaTime, ForceMode.Force);
        }
    }
}
