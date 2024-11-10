using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowHolder : MonoBehaviour
{
    List<BirdBrain> crows = new List<BirdBrain>();
    public GameObject CrowPrefab;
    //public GameObject CrowTargetPrefab;
    public GameObject CrowTarget;

    public Vector3 averageCrowPosition
    {
        get
        {
            // TODO: Average of all crow positions.
            return new Vector3(0, 0, 0);
        }
    }


    private void Start()
    {
        SpawnCrows(25);
    }

    public void AddCrowTargetIfNoneExists(Vector3 feast)
    {

        //Instantiate(CrowTargetPrefab,feast, Quaternion.identity);

        if (!CrowTarget.GetComponent<CrowTarget>().isActiveTarget)
        {
            CrowTarget.transform.position = feast;
            CrowTarget.GetComponent<CrowTarget>().SetActiveTarget();
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
    public void SpawnCrows(int quantity)
    {
        for (int i = 0; i < quantity; i++)
        {
            AddCrow();
        }
    }

    private void AddCrow()
    {
        GameObject birdBrain = Instantiate(CrowPrefab, transform);
        crows.Add(birdBrain.GetComponent<BirdBrain>());
        birdBrain.GetComponent <BirdBrain>().crowHolder = this;
    }
}
