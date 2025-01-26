using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBillboard : MonoBehaviour
{
    private void LateUpdate()
    {
        if(Camera.main != null)
        transform.LookAt(Camera.main.transform.position);
    }
}
