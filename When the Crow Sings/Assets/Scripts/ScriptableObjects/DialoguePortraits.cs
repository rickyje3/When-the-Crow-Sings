using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu]
public class DialoguePortraits : ScriptableObject
{
    public List<Sprite> chancePortraits;
    public List<Sprite> theodorePortraits;
    public List<Sprite> philomenaPortraits;
    public List<Sprite> faridaPortraits;
    public List<Sprite> angelPortraits;
    public List<Sprite> calebPortraits;
    public List<Sprite> beauPortraits;
    public List<Sprite> quinnPortraits;
    public List<Sprite> jazmynePortraits;
    public List<Sprite> fransiscoPortraits;
    public List<Sprite> yulePortraits;

    public Sprite GetPortrait(string characterName, Constants.EMOTIONS emotion)
    {
        List<Sprite> portraits = null;
        switch (characterName)
        {
            case "Chance":
                portraits = chancePortraits;
                break;
            case "Theodore":
                portraits = theodorePortraits;
                break;
            case "Philomena":
                portraits = philomenaPortraits;
                break;
            case "Farida":
                portraits = faridaPortraits;
                break;
            case "Angel":
                portraits = angelPortraits;
                break;
            case "Caleb":
                portraits = calebPortraits;
                break;
            case "Beau":
                portraits = beauPortraits;
                break;
            case "Quinn":
                portraits = quinnPortraits;
                break;
            case "Jazmyne":
                portraits = jazmynePortraits;
                break;
            case "Fransisco":
                portraits = fransiscoPortraits;
                break;
            case "Yule":
                portraits = yulePortraits;
                break;
            default:
                portraits = new List<Sprite>();
                break;
        }
        if (portraits.Count != 0)
        {
            return portraits[(int)emotion];
        }
        else
        {
            return null;
        }
        
    }
}
