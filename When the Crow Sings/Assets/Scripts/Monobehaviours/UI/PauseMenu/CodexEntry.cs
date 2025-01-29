using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CodexEntry : MonoBehaviour
{

    public Sprite undiscoveredImage;
    public Sprite discoveredImage;

    public string undiscoveredName = "Unknown";



    public string characterName = "missingno.";

    [TextArea]
    public string undiscoveredDescription = "NoMessageWritten.";
    [TextArea]
    public string characterDescription = "NoMessageWritten";
    [TextArea]
    public string additionalDescription = "NoMessageWritten";

    public string hasBeenHeardOf = "TestingFlag1";
    public string hasBeenMetFlag = "TestingFlag1";
    public string hasBeenFinishedFlag = "TestingFlag2";

    public TextMeshProUGUI characterNameLabel;
    public TextMeshProUGUI characterDescriptionLabel;

    private void OnEnable()
    {
        if (SaveDataAccess.saveData.boolFlags[hasBeenFinishedFlag])
        {
            characterNameLabel.text = characterName;
            characterDescriptionLabel.text = characterDescription;
        }
        else
        {
            characterNameLabel.text = undiscoveredName;
            characterDescriptionLabel.text = undiscoveredDescription;
        }
    }

}
