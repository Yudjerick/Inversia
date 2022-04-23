using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeDependentBehaviour : MonoBehaviour
{
    public static bool inSandbox;
    [SerializeField] private bool inSandboxFrameAgo;
    private void Update()
    {
        if(inSandbox && !inSandboxFrameAgo)
        {
            OnSandboxEnable();
        }
        if (!inSandbox && inSandboxFrameAgo)
        {
            OnSandboxDisable();
        }
        inSandboxFrameAgo = inSandbox;
    }

    public virtual void OnSandboxEnable()
    {

    }
    public virtual void OnSandboxDisable()
    {

    }
}
