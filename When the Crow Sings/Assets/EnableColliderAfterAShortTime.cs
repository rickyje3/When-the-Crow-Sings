using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableColliderAfterAShortTime : MonoBehaviour
{
    public float timeToWait = .05f;

    private void Start()
    {
        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(timeToWait);
        GetComponent<Collider>().enabled = true;
    }
}
