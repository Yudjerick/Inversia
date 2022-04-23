using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeDependentBehaviour : MonoBehaviour
{
    public static bool inSandbox;
    protected bool inSandboxFrameAgo;

    protected void CallModeMethods()
    {
        if (inSandbox && !inSandboxFrameAgo)
        {
            OnSandboxEnable();
        }
        if (!inSandbox && inSandboxFrameAgo)
        {
            OnSandboxDisable();
        }
        inSandboxFrameAgo = inSandbox;
    }

    public virtual void OnSandboxEnable() { }
    public virtual void OnSandboxDisable() { }
}
