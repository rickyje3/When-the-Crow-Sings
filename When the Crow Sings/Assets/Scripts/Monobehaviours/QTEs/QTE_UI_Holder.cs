using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class QTE_UI_Holder : MonoBehaviour
{
    private QuickTimeEvent qte;
    public void LoadQte(QuickTimeEvent _qte)
    {
        qte = Instantiate(_qte);
        qte.transform.SetParent(transform, false);
        //Instantiate(_qte).transform.SetParent(transform,false);
    }

    public void DestroyQte()
    {
        if (qte != null) Destroy(qte.gameObject);
        qte = null;
    }
}
