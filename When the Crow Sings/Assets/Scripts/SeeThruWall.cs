using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeeThruWall : MonoBehaviour
{
    public Material SeeThruMaterial; //assign to seethrumaterial
    public Camera Camera;
    public LayerMask mask; //assign to wall

    public static int PosID = Shader.PropertyToID("_Position");
    public static int SizeID = Shader.PropertyToID("_Size");
    public static int TintID = Shader.PropertyToID("_Tint");

    private void Awake()
    {
        Camera = FindObjectOfType<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        var dir = Camera.transform.position - transform.position;
        var ray = new Ray(transform.position, dir.normalized);

        //if raycast in range, increase the seethru material size
        if (Physics.Raycast(ray, 3000, mask))
        {
            SeeThruMaterial.SetFloat(SizeID, 1);
        }
        else SeeThruMaterial.SetFloat(SizeID, 0);

        var view = Camera.WorldToViewportPoint(transform.position);
        SeeThruMaterial.SetVector(PosID, view);
    }
}
