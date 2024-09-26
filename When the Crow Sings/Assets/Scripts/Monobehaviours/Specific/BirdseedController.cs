using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdseedController : MonoBehaviour//StateMachineComponent
{
    public Transform pfCrowsTemp;


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


    private void Init(Vector3 direction)
    {
        transform.eulerAngles = new Vector3(0,0,Utilities.GetAngleFromVector_Deg(direction));
        Shoot(direction);
        Destroy(gameObject, 5.0f);
    }

    private void Shoot(Vector3 direction)
    {

    }




}
