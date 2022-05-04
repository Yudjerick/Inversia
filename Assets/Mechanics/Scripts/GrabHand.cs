using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//скрипт для захвата объектов (руки), нужно связать с VR контроллером с помощбю HingeJoint
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
            //Test movement
            PCmovementForTesting();
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

    void PCmovementForTesting()
    {
        float ZMov = 0;
        if (Input.GetKey(KeyCode.X))
        {
            ZMov = -1;
        }
        if (Input.GetKey(KeyCode.Z))
        {
            ZMov = 1;
        }
        rb.velocity = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), ZMov)*2;
    }

    public void Grab(Rigidbody body)
    {
        if(body == null || GetComponent<FixedJoint>() != null)
        {
            return;
        }
        body.isKinematic = false;
        FixedJoint fj = gameObject.AddComponent<FixedJoint>();
        fj.connectedBody = body;

        AxisConstraints axisConstraints = body.gameObject.GetComponentInParent<AxisConstraints>();
        if (axisConstraints)
        {
            axisConstraints.Activate();
        }
    }

    public void UnGrab()
    {
        FixedJoint fj = GetComponent<FixedJoint>();
        if (!fj)
            return;

        Interactable interactable = fj.connectedBody.gameObject.GetComponentInParent<Interactable>();
        if(interactable && interactable.makeKinematic)
        {
            fj.connectedBody.gameObject.GetComponentInParent<Rigidbody>().isKinematic = true;
        }
        
        AxisConstraints axisConstraints = fj.connectedBody.gameObject.GetComponentInParent<AxisConstraints>();
        if (axisConstraints)
        {
            axisConstraints.Deactivate();
        }

        Destroy(fj);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (GetComponent<FixedJoint>())
            return;
        
        Interactable interactable = other.gameObject.GetComponentInParent<Interactable>();
        if (interactable)
        {
            selectedObj = other.gameObject.GetComponentInParent<Rigidbody>();
            if (interactable.lockHandRotation)
            {
                rb.constraints = RigidbodyConstraints.FreezeRotation;
                Debug.Log("rotation locked");
            }
            else
            {
                rb.constraints = RigidbodyConstraints.None;
                Debug.Log("rotation unlocked");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        selectedObj = null;   
    }
}
