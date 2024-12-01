using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerOverlay : MonoBehaviour//, IService
{
    public float dangerAlpha = 0.0f;
    //public void RegisterSelfAsService()
    //{
    //    ServiceLocator.Register(this);
    //}

    // Start is called before the first frame update
    void Awake()
    {
        //RegisterSelfAsService();
    }

    // Update is called once per frame
    void Update()
    {
        if (EnemyController.canSeePlayer) dangerAlpha = .5f;
        else dangerAlpha = 0f;
        GetComponent<CanvasGroup>().alpha = dangerAlpha;
    }
}
