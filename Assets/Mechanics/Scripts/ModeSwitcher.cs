using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeSwitcher : MonoBehaviour
{
    //������������� ������, ������ ���� ���������� �� ������ ������
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            ModeDependentBehaviour.inSandbox = !ModeDependentBehaviour.inSandbox;
        }
    }
}
