using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CamXMovement : MonoBehaviour
{
    public float rotationSpeed = 1;
    [Range(0.01f, 1f)]
    public float smoothFactor = 0.5f;
    public Transform worldUp;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        //Quaternion worldUpRotation = Quaternion.Slerp(transform.rotation, Quaternion.FromToRotation(Vector3.up, worldUp.up), Time.deltaTime);
        //transform.rotation = worldUpRotation;

        Quaternion cameraTurnAngle = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * rotationSpeed, Vector3.up);
        transform.rotation *= cameraTurnAngle;
        

        
    }
}
