using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowHolder : MonoBehaviour
{
    List<BirdBrain> crows = new List<BirdBrain>();
    public GameObject CrowPrefab;
    //public GameObject CrowTargetPrefab;
    public CrowTarget CrowTarget;

    public Vector3 averageCrowPosition
    {
        get
        {
            // TODO: Average of all crow positions.
            return new Vector3(0, 0, 0);
        }
    }

    public void AddCrowTargetIfNoneExists(BirdseedController birdseedToFeastUpon)//Vector3 feast)
    {

        //Instantiate(CrowTargetPrefab,feast, Quaternion.identity);

        if (!CrowTarget.GetComponent<CrowTarget>().isActiveTarget)
        {
            CrowTarget.transform.position = birdseedToFeastUpon.transform.position;
            CrowTarget.GetComponent<CrowTarget>().SetActiveTarget();
            ServiceLocator.Get<GameManager>().activeBirdseed = birdseedToFeastUpon;
        }
        

        //foreach (BirdBrain i in crows)
        //{
        //    i.stateMachine.Enter("CrowScatterState");
        //}
    }

    public void DestroyCrows()
    {
        foreach (BirdBrain i in crows)
        {
            Destroy(i.gameObject);
        }
        crows.Clear();
    }
    public void SpawnCrows(List<CrowRestPoint> _crowRestPoints)
    {
        if (_crowRestPoints.Count > 0)
        {
            foreach (CrowRestPoint i in _crowRestPoints)
            {
                AddCrow(i);
            }
        }
        
    }

    private void AddCrow(CrowRestPoint _crowRestPoint)
    {
        GameObject birdBrain = Instantiate(CrowPrefab, transform);
        crows.Add(birdBrain.GetComponent<BirdBrain>());
        birdBrain.GetComponent<BirdBrain>().crowHolder = this;
        birdBrain.GetComponent<BirdBrain>().SetRestPoint(_crowRestPoint);
    }
}
