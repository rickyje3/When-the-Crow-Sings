using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdseedController : MonoBehaviour//StateMachineComponent
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


    // Factory Pattern-ish thing. TODO: Should this be centralized?
    public static BirdseedController Create(BirdseedController prefab, Transform throwPosition, Vector3 direction)
    {
        BirdseedController birdseed = Instantiate(prefab, throwPosition.position, Quaternion.identity);
        birdseed.Init(direction);
        return birdseed;
    }

    private void Awake()
    {
        //stateMachine = new StateMachine(this); // TODO: Figure out how to make this line "required" so it's type-safe and shows the error at compile-time.
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

    }


    bool firstTime = false;
    private void OnCollisionEnter(Collision collision)
    {
        //if(collision.gameObject.tag == "FloorTemp")
        //{
        //    isLanded = true;
        //    Destroy(gameObject, 1.5f);
        //}
        if (!firstTime)
        {
            firstTime = true;
            isLanded = true;
            StartCoroutine(SpawnCrows());
        }
    }


}
