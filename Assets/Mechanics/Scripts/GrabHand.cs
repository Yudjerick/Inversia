using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabHand : ModeDependentBehaviour
{
    private Rigidbody rb;
    public Rigidbody selectedObj;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void  Update()
    {
        if (!inSandbox)
        {
            //Replace with vr controller movement
            float ZMov = 0;
            if (Input.GetKey(KeyCode.X))
            {
                ZMov = -1;
            }
            if (Input.GetKey(KeyCode.Z))
            {
                ZMov = 1;
            }
            rb.velocity = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), ZMov);
            //
            if (Input.GetKey(KeyCode.LeftShift))
            {
                Grab(selectedObj);
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                UnGrab();
            }
        }
        else
        {
            UnGrab();
            //activate sandboxArm gameobject , deactivate this gameobject
        }
    }

    public void Grab(Rigidbody body)
    {
        if(body == null || GetComponent<FixedJoint>() != null)
        {
            return;
        }
        FixedJoint fj = gameObject.AddComponent<FixedJoint>();
        fj.connectedBody = body;
    }

    public void UnGrab()
    {
        FixedJoint fj = GetComponent<FixedJoint>();
        if (fj != null)
        {
            Destroy(fj);
        }
    }



    public void OnTriggerEnter(Collider other)
    {
        selectedObj = other.gameObject.GetComponentInParent<Rigidbody>();
    }

    private void OnTriggerExit(Collider other)
    {
        selectedObj = null;
    }
}
