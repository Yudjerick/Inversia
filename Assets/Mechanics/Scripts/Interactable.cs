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
        CallModeMethods();
    }

    public override void OnSandboxEnable()
    {
        Debug.Log("sandbox mode enabled");
        transform.position = defaultPos;
        transform.rotation = defaultRot;
        rb.isKinematic = true;
    }

    public override void OnSandboxDisable()
    {
        rb.isKinematic = false;
        Debug.Log("sandbox mode disabled");
    }
}
