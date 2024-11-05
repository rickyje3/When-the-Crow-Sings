using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StirSlider : MonoBehaviour
{
    public StirringQTE stirringQTE;
    public Image fillImage;
    public Slider slider;

    private void Awake()
    {
        stirringQTE = FindObjectOfType<StirringQTE>();
        fillImage = GetComponentInChildren<Image>();
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        //Sets slider to fill adjacent to stirringqte.score
        float fillValue = stirringQTE.score;
        slider.value = fillValue;
    }
}
