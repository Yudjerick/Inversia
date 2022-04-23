using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeSwitcher : MonoBehaviour
{
    //переключатель режима, должен быть прикреплен на пустой объект
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            ModeDependentBehaviour.inSandbox = !ModeDependentBehaviour.inSandbox;
        }
    }
}
