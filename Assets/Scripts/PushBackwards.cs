using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PushBackwards : MonoBehaviour
{
    public MouseButton mouseButton;
    public Transform cameraTransform;
    public Transform playerTransform;
    public float pushDistance;
    public float pushRadius;
    public float pushForce;
    public float force;
    void Update()
    {
        if (Input.GetMouseButtonDown(((int)mouseButton)))
        {
            Vector3 forceVector = cameraTransform.forward * - force;
            GetComponent<Rigidbody>().AddForce(forceVector, ForceMode.Impulse);
            Collider[] colliders = Physics.OverlapCapsule(playerTransform.position + cameraTransform.forward, playerTransform.position + cameraTransform.forward * pushDistance, pushRadius);
            foreach (Collider collider in colliders)
            {
                if (collider == GetComponent<Collider>())
                {
                }
                else if (collider.attachedRigidbody)
                {
                    collider.attachedRigidbody.AddExplosionForce(pushForce, playerTransform.position, pushDistance);
                }
            }
        }
    }
}
