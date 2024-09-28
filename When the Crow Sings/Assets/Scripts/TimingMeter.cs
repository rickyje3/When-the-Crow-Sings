using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimingMeter : MonoBehaviour
{
    public Slider sliderMeter;
    public float speed = 2f;
    public float targetMin = 0.4f; //Range for successful hit
    public float targetMax = 0.6f;

    private bool movingUp = true; //Meter movement direction
    private bool meterActive;


    // Update is called once per frame
    void Update()
    {
        
    }
}
