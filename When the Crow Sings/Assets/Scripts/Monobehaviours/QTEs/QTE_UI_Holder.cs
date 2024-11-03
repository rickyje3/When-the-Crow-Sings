using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class QTE_UI_Holder : MonoBehaviour
{
    public void LoadQte(QuickTimeEvent qte)
    {
        //Debug.Log(qte);
        Instantiate(qte).transform.SetParent(transform,false);

    }
}
