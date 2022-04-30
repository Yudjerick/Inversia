using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxisConstraints: MonoBehaviour
{
    enum Axis
    {
        x,y,z
    }

    [SerializeField] private Axis axis;
    [SerializeField] private float minPos;
    [SerializeField] private float maxPos;
    [SerializeField] private GameObject constraint;
    private Collider[] colliders;
    private GameObject collidersCopy;
    private GameObject blockator1;
    private GameObject blockator2;

    void Start()
    {
        colliders = GetComponentsInChildren<Collider>();
        collidersCopy = Instantiate(new GameObject("partsColliders"), gameObject.transform);
        collidersCopy.layer = 3;
        for(int i = 0; i < colliders.Length; i++)
        {
            
            Component col = collidersCopy.AddComponent(colliders[i].GetType());
            col = colliders[i];
        }
        
        colliders = GetComponentsInChildren<Collider>();
        if (axis == Axis.x)
        {
            blockator1 = Instantiate(constraint, new Vector3(minPos - constraint.GetComponent<SphereCollider>().radius, transform.position.y, transform.position.z), Quaternion.identity);
            blockator2 = Instantiate(constraint, new Vector3(maxPos + constraint.GetComponent<SphereCollider>().radius, transform.position.y, transform.position.z), Quaternion.identity);
        }
        else if (axis == Axis.y)
        {
            blockator1 = Instantiate(constraint, new Vector3(transform.position.x, minPos - constraint.GetComponent<SphereCollider>().radius, transform.position.z), Quaternion.identity);
            blockator2 = Instantiate(constraint, new Vector3(transform.position.x, maxPos + constraint.GetComponent<SphereCollider>().radius, transform.position.z), Quaternion.identity);
        }
        else
        {
            blockator1 = Instantiate(constraint, new Vector3(transform.position.x, transform.position.y, minPos - constraint.GetComponent<SphereCollider>().radius), Quaternion.identity);
            blockator2 = Instantiate(constraint, new Vector3(transform.position.x, transform.position.y, maxPos + constraint.GetComponent<SphereCollider>().radius), Quaternion.identity);
        }
        Deactivate();
    }

    public void Deactivate()
    {
        collidersCopy.SetActive(false);
        blockator1.SetActive(false);
        blockator2.SetActive(false);
    }

    public void Activate()
    {
        collidersCopy.SetActive(true);
        blockator1.SetActive(true);
        blockator2.SetActive(true);
    }
}
