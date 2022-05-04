using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//объект, перемещающийся по заданному пути
public class FollowingPath: ModeDependentBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Vector3[] pathPoints;

    [SerializeField]private int nextPoint = 0;
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.mass = float.PositiveInfinity;
    }

    private void Update()
    {
        if (pathPoints.Length < 2)
            return;
        CallModeMethods();
    }
    void FixedUpdate()
    {
        if (inSandbox || pathPoints.Length < 2)
            return;
        rb.velocity = (pathPoints[nextPoint] - transform.position).normalized * speed;
        if ((transform.position - pathPoints[nextPoint]).magnitude <= 0.1f)
            MoveNextPoint();
    }

    private void MoveNextPoint()
    {
        if(nextPoint+1 >= pathPoints.Length)
        {
            nextPoint = 0;
            return;
        }
        nextPoint++;
    }

    public override void OnSandboxEnable()
    {
        transform.position = pathPoints[0];
        nextPoint = 0;
        rb.velocity = Vector3.zero;
    }
}
