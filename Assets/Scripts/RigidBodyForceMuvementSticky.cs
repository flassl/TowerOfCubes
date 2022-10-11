
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class RigidBodyForceMuvementSticky : MonoBehaviour
{
    public float force;
    public Transform cameraTransform;
    public GameObject worldUpTransform;
    private Rigidbody rigidBody;
    private bool inContact = false;
    private Vector3 collisionNormal;
    private float startTime;


    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }
    private void OnCollisionStay(Collision collision)
    {
        if (!inContact)
        {
            startTime = Time.time;
        }

        inContact = true;
        collisionNormal = collision.GetContact(0).normal;
        rigidBody.AddForce(-collisionNormal * Time.deltaTime * force /20, ForceMode.Force);
        collision.rigidbody.AddForceAtPosition(collisionNormal * Time.deltaTime * force /20, collision.GetContact(0).point, ForceMode.Force);
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        if (direction.magnitude > 0.01f)
        {
            Quaternion cameraBasis = cameraTransform.rotation.normalized;
            direction = cameraBasis * direction;
            GetComponent<Rigidbody>().AddForce(direction * force * Time.deltaTime, ForceMode.Force);
        }
        worldUpTransform.transform.rotation = Quaternion.Slerp(worldUpTransform.transform.rotation, Quaternion.FromToRotation(Vector3.up, collisionNormal), Time.deltaTime);
        //worldUpTransform.transform.rotation = Quaternion.FromToRotation(Vector3.up, collisionNormal);
    }

    private void OnCollisionExit(Collision collision)
    {
        inContact=false;
    }
}
