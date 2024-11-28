using ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdseedController : MonoBehaviour
{
    public GameSignal birdseedLanded;
    public GameObject throwVisual;
    public GameObject landedVisual;
    public GameObject crowTargetPrefab;

    private bool _isLanded = false;

    public float crowDelayInSeconds = .5f;
    public float birdseedLifeAfterGround = 1.5f;

    [HideInInspector]
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

    private IEnumerator SpawnCrows() // This needs to get Eliminated at some point.
    {
        yield return new WaitForSeconds(crowDelayInSeconds);
        //Instantiate(pfCrowsTemp,transform.position, Quaternion.identity);


        SignalArguments args = new SignalArguments();
        args.objectArgs.Add(this);
        birdseedLanded.Emit(args);
        AudioManager.instance.PlayOneShot(FMODEvents.instance.CrowCocophony, this.transform.position);

        if (ServiceLocator.Get<GameManager>().activeBirdseed != this) Destroy(gameObject, birdseedLifeAfterGround);
    }

    private void Init(Vector3 direction)
    {
        isLanded = false;
        transform.eulerAngles = new Vector3(0,0,Utilities.GetAngleFromVector_Deg(direction));
        Shoot(direction);
        
    }

    private void Shoot(Vector3 direction)
    {
        GetComponent<Rigidbody>().velocity = direction*5;
    }


    bool firstTime = false;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 3)
        {
            if (!firstTime)
            {
                firstTime = true;
                isLanded = true;
                GetComponent<Rigidbody>().velocity *= 0.05f;
                AudioManager.instance.PlayOneShot(FMODEvents.instance.SeedHit, this.transform.position);
                StartCoroutine(SpawnCrows());
            }
        }
        
    }
}
