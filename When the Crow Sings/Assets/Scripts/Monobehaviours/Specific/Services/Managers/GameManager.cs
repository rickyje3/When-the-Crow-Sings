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
            bool newValue = false;
            switch (i.valueType)
            {
                case DynamicEnable.VALUE_TYPE.BOOL:
                    newValue = SaveDataAccess.saveData.boolFlags[i.associatedDataKey] == i.boolValue;
                    break;
                case DynamicEnable.VALUE_TYPE.INT:
                    newValue = SaveDataAccess.saveData.intFlags[i.associatedDataKey] == i.intValue;
                    break;
                case DynamicEnable.VALUE_TYPE.STRING:
                newValue = SaveDataAccess.saveData.stringFlags[i.associatedDataKey] == i.stringValue;
                break;
            }
            if (newValue == false
                && i.gameObject.activeInHierarchy
                && gameStateManager.canLoad
                && i.playPickupSoundOnDisable)
            {
                // Play "pickup" sound
                Debug.Log("Play 'pickup' sound");
            }
            i.gameObject.SetActive(newValue);
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
