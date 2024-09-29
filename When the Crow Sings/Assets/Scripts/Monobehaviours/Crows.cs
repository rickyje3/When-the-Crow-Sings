using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Crows : MonoBehaviour
{
    public float durationInSeconds = 6.0f;


    private void Awake()
    {
        GetComponent<NavMeshObstacle>().enabled = false;
        StartCoroutine(delayAvoidance());
    }

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, durationInSeconds);
    }

    private IEnumerator delayAvoidance() // TODO: Maybe reset this until an OnTriggerExit()?
    {
        yield return new WaitForSeconds(.1f);
        GetComponent<NavMeshObstacle>().enabled = true;
    }
}
