using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ThrowObject : MonoBehaviour
{
    public GameObject throwable;
    public GameObject throwerCollision;
    public GameObject throwerGraphic;
    public Vector3 offset;
    public Transform cameraTransform;
    public float force;
    public MouseButton button;
    public float recoil;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton((int)button))
        {

            Vector3 aimVector = (cameraTransform.rotation * Vector3.forward).normalized;
            GameObject newTrhowable =  Instantiate(throwable, throwerGraphic.transform.position + offset + aimVector * 2, cameraTransform.rotation);
            Vector3 trowerVelocity = throwerCollision.GetComponentInChildren<Rigidbody>().velocity;
            newTrhowable.GetComponent<Rigidbody>().AddForce(trowerVelocity, ForceMode.VelocityChange);
            newTrhowable.GetComponent<Rigidbody>().AddForce(aimVector * force);
            throwerCollision.GetComponent<Rigidbody>().AddForce(aimVector * - recoil);
        }
    }
}
