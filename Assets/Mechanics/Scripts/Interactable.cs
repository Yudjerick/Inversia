using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : ModeDependentBehaviour
{

    public bool lockHandRotation;
    public bool makeKinematic;
    [SerializeField] private GameObject sandboxUI;

    private Vector3 defaultPos;
    private Quaternion defaultRot;
    private Rigidbody rb;
    void Start()
    {
        defaultPos = transform.position;
        defaultRot = transform.rotation;

        rb = GetComponent<Rigidbody>();

        rb.isKinematic = makeKinematic;
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
        if (sandboxUI)
            sandboxUI.SetActive(true);
    }

    public override void OnSandboxDisable()
    {
        rb.isKinematic = false;
        if (sandboxUI)
            sandboxUI.SetActive(false);

        Debug.Log("sandbox mode disabled");
    }
}
