using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdseedController : MonoBehaviour
{
    public Transform pfCrowsTemp;
    public GameObject throwVisual;
    public GameObject landedVisual;

    private bool _isLanded = false;
    public bool isLanded { get {return _isLanded; }
        set
        {
            throwVisual.SetActive(!value);
            landedVisual.SetActive(value);
            _isLanded = value;
        }
}

    
    public static BirdseedController Create(BirdseedController prefab, Transform throwPosition, Vector3 direction)
        // Factory Pattern-ish thing. TODO: Should this be centralized?
    {
        BirdseedController birdseed = Instantiate(prefab, throwPosition.position, Quaternion.identity);
        birdseed.Init(direction);
        return birdseed;
    }

    private IEnumerator SpawnCrows()
    {

        yield return new WaitForSeconds(1.5f);
        Instantiate(pfCrowsTemp,transform.position, Quaternion.identity);
        Destroy(gameObject, 1.5f);
    }

    private void Init(Vector3 direction)
    {
        isLanded = false;
        transform.eulerAngles = new Vector3(0,0,Utilities.GetAngleFromVector_Deg(direction));
        Shoot(direction);
        
    }

    private void Shoot(Vector3 direction)
    {
        GetComponent<Rigidbody>().velocity = direction*10;
    }


    bool firstTime = false;
    private void OnCollisionEnter(Collision collision)
    {
        if (!firstTime)
        {
            firstTime = true;
            isLanded = true;
            StartCoroutine(SpawnCrows());
        }
    }


}
