using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryLine : MonoBehaviour
{
    public ThrowTarget throwTarget;
    LineRenderer lineRenderer;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        for(int i = 0; i < lineRenderer.positionCount;  i++)
        {
            lineRenderer.SetPosition(i, transform.position+new Vector3 (0,1,0));
        }
        lineRenderer.SetPosition(0,transform.position);
        lineRenderer.SetPosition(2,throwTarget.transform.position);
    }
}
