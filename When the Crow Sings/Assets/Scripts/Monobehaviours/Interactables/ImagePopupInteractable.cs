using ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImagePopupInteractable : MonoBehaviour
{
    public Sprite popupImage;

    [Header("Built-In")]
    public GameSignal gameSignal;

    public void EmitImagePopupSignal()
    {
        SignalArguments args = new SignalArguments();
        args.objectArgs.Add(popupImage);
        gameSignal.Emit(args);
    }
}
