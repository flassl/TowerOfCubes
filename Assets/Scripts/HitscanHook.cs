using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using MouseButton = UnityEngine.UIElements.MouseButton;

public class HitscanHook : MonoBehaviour
{
    public MouseButton mouseButton;
    public GameObject player;
    public Transform originTransform;
    public Transform cameraTransform;
    public int maxDistance;
    public float force;
    public Texture[] crossHairs;
    public GameObject CrossHairCanvas;
    public ParticleSystem hookHitParticles;
    private bool hooked = false;
    private GameObject target;
    private Vector3 localHitPoint;
    private int pointerStatus = 0;


    // Update is called once per frame
    void FixedUpdate()
    {
        int layermask = 1 << 8;
        layermask = ~layermask;

        RaycastHit hit = new RaycastHit();

        Vector3 cameraPointVector = cameraTransform.rotation * Vector3.forward;

        if (Physics.Raycast(originTransform.position, cameraTransform.rotation * Vector3.forward, out hit, maxDistance, layermask))
        {
            if (pointerStatus == 0)
            {
                CrossHairCanvas.GetComponent<CanvasRenderer>().SetTexture(crossHairs[2]);
                pointerStatus = 2;
            }

            if (Input.GetMouseButton((int)mouseButton))
            {
                if (!hooked)
                {
                        target = hit.collider.gameObject;
                        localHitPoint = target.transform.InverseTransformDirection(hit.point - target.transform.position);
                        Quaternion particleBasis = new Quaternion();
                        particleBasis.SetLookRotation(hit.normal);
                        Instantiate(hookHitParticles, target.transform.position + target.transform.rotation * localHitPoint, particleBasis, target.transform);
                        CrossHairCanvas.GetComponent<CanvasRenderer>().SetTexture(crossHairs[1]);
                        pointerStatus = 1;
                        hooked = true;
                }
            }
            
        }
        
        else if (!hooked)
        {
            if (pointerStatus != 0)
            {
                CrossHairCanvas.GetComponent<CanvasRenderer>().SetTexture(crossHairs[0]);
                pointerStatus = 0;
            }
        }

        if (hooked)
        {
            Vector3 hookedPoint = target.transform.position + target.transform.rotation * localHitPoint;
            Vector3 player_target_vector = (hookedPoint - player.transform.position).normalized;
            target.GetComponent<Rigidbody>().AddForceAtPosition(player_target_vector * -force, hookedPoint, ForceMode.Force);
            player.GetComponent<Rigidbody>().AddForce(player_target_vector * force, ForceMode.Force);

        }

        if (!Input.GetMouseButton((int)mouseButton))
        {
            hooked = false;
        }

    }
}
