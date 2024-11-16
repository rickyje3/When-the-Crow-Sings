using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnparentScale : MonoBehaviour
{
    public Transform parent; //Parented object you dont want your object to scale with
    private Transform newParent;

    // Start is called before the first frame update
    void Start()
    {
        parent = GetComponentInParent<Transform>();

        var originalScale = transform.localScale;
        transform.parent = newParent;
        transform.localScale = originalScale;
    }
}
