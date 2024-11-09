using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour, IService
{
    // Likely where we keep track of general stuff going on in the game. Possibly birdseed.
    public List<DynamicEnable> dynamicEnables = new List<DynamicEnable>();


    private void Awake()
    {
        RegisterSelfAsService();
    }
    public void RegisterSelfAsService()
    {
        ServiceLocator.Register<GameManager>(this);
    }

    private void Update()
    {
        DynamicEnableLogic();
    }
    private void DynamicEnableLogic()
    {
        foreach (DynamicEnable i in dynamicEnables)
        {
            switch (i.valueType)
            {
                case DynamicEnable.VALUE_TYPE.BOOL:
                    if (SaveData.boolFlags[i.associatedDataKey] == i.boolValue)
                    {
                        i.gameObject.SetActive(false);
                    }
                    else
                    {
                        i.gameObject.SetActive(true);
                    }
                    break;
                case DynamicEnable.VALUE_TYPE.INT:
                    if (SaveData.intFlags[i.associatedDataKey] == i.intValue)
                    {
                        i.gameObject.SetActive(false);
                    }
                    else
                    {
                        i.gameObject.SetActive(true);
                    }
                    break;
                case DynamicEnable.VALUE_TYPE.STRING:
                    if (SaveData.stringFlags[i.associatedDataKey] == i.stringValue)
                    {
                        i.gameObject.SetActive(false);
                    }
                    else
                    {
                        i.gameObject.SetActive(true);
                    }
                    break;
            }
        }

        
    }
}
