using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TempJournalConstant : MonoBehaviour
{
    //public MenuButtonHighlightSelector menuButtonHighlightSelector;
    public List<GameObject> gameObjects = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isActiveAndEnabled)
        {
            
        }
    }

    private void OnEnable()
    {
        InputManager.playerInputActions.UI.TEMPPanelSwap.performed += OnPerformed;
    }

    private void OnDisable()
    {
        InputManager.playerInputActions.UI.TEMPPanelSwap.performed -= OnPerformed;
    }

    private int currentSelected = 0;
    private void OnPerformed(InputAction.CallbackContext context)
    {
        //menuButtonHighlightSelector.
        currentSelected += 1;
        if (currentSelected >= 3)
        {
            currentSelected = 0;
        }
        int loop = 0;
        foreach (GameObject go in gameObjects)
        {
            if ( loop == currentSelected)
            {
                go.SetActive(true);
            }
            else
            {
                go.SetActive(false);
            }
            loop++;
        }
    }
}
