using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//скрипт для объекта, перемещещающегося по оси (новая версия с использованием ConfigurableJoint)
public class AxisConstraints2 : MonoBehaviour
{
    enum Axis
    {
        x, y, z
    }

    private Vector3 defaultPos, axisCenter;

    [SerializeField] private Axis axis;
    [SerializeField] private float minPos;
    [SerializeField] private float maxPos;

    private ConfigurableJoint cj;
    private Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        ApplyJointSettings();
    }
    public void ApplyJointSettings()
    {
        if (!GetComponent<ConfigurableJoint>())
        {
            gameObject.AddComponent<ConfigurableJoint>();
        }
        cj = GetComponent<ConfigurableJoint>();
        defaultPos = transform.position;
        
        if (axis == Axis.x)
        {
            cj.xMotion = ConfigurableJointMotion.Limited;
            rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
            axisCenter = new Vector3((maxPos + minPos)/2, transform.position.y, transform.position.z);
        }
        else if (axis == Axis.y)
        {
            cj.yMotion = ConfigurableJointMotion.Limited;
            rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
            axisCenter = new Vector3(transform.position.x, (maxPos + minPos)/2, transform.position.z);
        }
        else
        {
            cj.zMotion = ConfigurableJointMotion.Limited;
            rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
            axisCenter = new Vector3(transform.position.x, transform.position.y, (maxPos + minPos) / 2);
        }

        cj.autoConfigureConnectedAnchor = false;
        //cj.anchor = axisCenter - defaultPos;
        cj.connectedAnchor = axisCenter;
        SoftJointLimit limit = cj.linearLimit;
        limit.limit = Math.Abs(minPos-maxPos)/2;
        cj.linearLimit = limit;
    }
}
