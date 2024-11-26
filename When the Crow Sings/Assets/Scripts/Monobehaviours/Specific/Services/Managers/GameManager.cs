using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour, IService
{
    // Likely where we keep track of general stuff going on in the game. Possibly birdseed.
    [HideInInspector]
    public List<DynamicEnable> dynamicEnables = new List<DynamicEnable>();
    [HideInInspector]
    public List<BirdseedController> landedBirdseed = new List<BirdseedController>(); // Birbseeb
    public CrowHolder crowHolder;

    [HideInInspector]
    public List<CrowRestPoint> crowRestPoints = new List<CrowRestPoint>();
    
    [HideInInspector]
    public BirdseedController activeBirdseed;

    public GameStateManager gameStateManager;

    private void Awake()
    {
        RegisterSelfAsService();
    }
    public void RegisterSelfAsService()
    {
        ServiceLocator.Register(this);
    }

    private void Update()
    {
        DynamicEnableLogic();
    }



    #region CrowLogic
    public void OnBirdseedLanded(SignalArguments args)
    {
        landedBirdseed.Add((BirdseedController)args.objectArgs[0]);
        crowHolder.AddCrowTargetIfNoneExists((BirdseedController)args.objectArgs[0]);
    }

    public void RegisterCrowRestPoint(CrowRestPoint crowRestPoint)
    {
        crowRestPoints.Add(crowRestPoint);
    }
    public void UnregisterCrowRestPoint(CrowRestPoint crowRestPoint)
    {
        crowRestPoints.Remove(crowRestPoint);
    }
    #endregion

    private void DynamicEnableLogic()
    {
        foreach (DynamicEnable i in dynamicEnables)
        {
            switch (i.valueType)
            {
                case DynamicEnable.VALUE_TYPE.BOOL:
                    if (SaveDataAccess.boolFlags[i.associatedDataKey] == i.boolValue)
                    {
                        i.gameObject.SetActive(true);
                    }
                    else
                    {
                        i.gameObject.SetActive(false);
                    }
                    break;
                case DynamicEnable.VALUE_TYPE.INT:
                    if (SaveDataAccess.intFlags[i.associatedDataKey] == i.intValue)
                    {
                        i.gameObject.SetActive(true);
                    }
                    else
                    {
                        i.gameObject.SetActive(false);
                    }
                    break;
                case DynamicEnable.VALUE_TYPE.STRING:
                    if (SaveDataAccess.stringFlags[i.associatedDataKey] == i.stringValue)
                    {
                        i.gameObject.SetActive(true);
                    }
                    else
                    {
                        i.gameObject.SetActive(false);
                    }
                    break;
            }
        }
    }

    public void PopupImage(SignalArguments args)
    {
        Debug.Log("Popped up image!");
    }

    public void OnEnemyCaughtPlayer()
    {
        gameStateManager.ReloadCurrentScene(0);
    }
}
