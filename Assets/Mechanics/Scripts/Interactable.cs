using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : ModeDependentBehaviour
{
    [SerializeField] private bool blockedX;
    [SerializeField] private bool blockedY;
    [SerializeField] private bool blockedZ;

    [SerializeField] private bool usesGravity;

    private Vector3 defaultPos;
    private Quaternion defaultRot;
    private Rigidbody rb;
    void Start()
    {
        defaultPos = transform.position;
        defaultRot = transform.rotation;
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if (inSandbox)
        {
            transform.position = defaultPos;
            transform.rotation = defaultRot;
            rb.velocity = Vector3.zero;
            rb.useGravity = false;
            //rb.constraints = RigidbodyConstraints.FreezeAll;
        }
        else
        {
            rb.useGravity = usesGravity;
        }
    }

    public override void OnSandboxEnable()
    {
        base.OnSandboxEnable();
        transform.position = defaultPos;
        transform.rotation = defaultRot;
    }
}
