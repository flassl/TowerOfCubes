using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CamYMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float rotationSpeed = 1;
    public Transform playerTransform;
    public Transform pivotTransform;
    [Range(0.01f, 1f)]
    public float smoothFactor = 0.5f;
    private Vector3 _yTrailPosition;
    private float rotationY;
    private float lastRotationY;
    void Start()
    {
        _yTrailPosition = new Vector3(0, 0, -10);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rotationY += Input.GetAxis("Mouse Y") * rotationSpeed;
        rotationY = Mathf.Clamp(rotationY, -90f, 90f);
        float deltaRotationY = rotationY - lastRotationY;
        lastRotationY = rotationY;

        transform.localRotation = pivotTransform.rotation;
        Quaternion cameraTurnYAngle = Quaternion.AngleAxis(deltaRotationY, transform.right * -1);
        //Quaternion cameraTurnYAngle = Quaternion.Euler(rotationY, 0, 0); 
        _yTrailPosition = cameraTurnYAngle * _yTrailPosition;
        Quaternion cameraTurnXAngle = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * rotationSpeed, transform.up);
        //Quaternion cameraTurnXAngle = Quaternion.Euler(0, rotationX, 0);
        _yTrailPosition = cameraTurnXAngle * _yTrailPosition;
        Vector3 newPosition = pivotTransform.position + _yTrailPosition;
        transform.localPosition = Vector3.Slerp(transform.position, newPosition, smoothFactor);
        transform.localRotation *= cameraTurnYAngle;
        float angleDifferenceY = Vector3.Angle(transform.up, Vector3.Cross(transform.right * -1, (playerTransform.position - transform.position).normalized));
        if (transform.position.y > playerTransform.position.y)
        {
            transform.Rotate(angleDifferenceY, 0, 0);
        }
        else
        {
            transform.Rotate(-angleDifferenceY, 0, 0);
        }






    }
    
}
