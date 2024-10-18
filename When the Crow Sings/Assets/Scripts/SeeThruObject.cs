using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeeThruObject : MonoBehaviour
{
    public Transform targetObject; //target behind the wall
    public LayerMask wallMask; //only see thru things tagged wall.
    private Camera mainCam;

    private void Awake()
    {
        mainCam = GetComponent<Camera>();
    }

    private void Update()
    {
        //WorldToViewportPoint converts world space to screen space normalized 
        //between (0, 0) and (1, 1)
        Vector2 cutoutPos = mainCam.WorldToViewportPoint(targetObject.position);
        cutoutPos.y /= (Screen.width / Screen.height);


        //Calculate the offset vector between the cam and target
        Vector3 offset = targetObject.position - transform.position;
        //Start the raycast at the camera in that direction
        RaycastHit[] hitObjects = Physics.RaycastAll(transform.position, offset, offset.magnitude, wallMask);

        for (int i = 0; i < hitObjects.Length; ++i)
        {
            //Get the list of materials
            Material[] materials = hitObjects[i].transform.GetComponent<Renderer>().materials;
            //For each material, set the cutout pos, size, and falloff size
            for (int m = 0; m < materials.Length; ++m)
            {
                materials[m].SetVector("_CutoutPos", cutoutPos);
                materials[m].SetFloat("_CutoutSize", 0.1f);
                materials[m].SetFloat("_FalloffSize", 0.05f);
            }
        }

    }
}
