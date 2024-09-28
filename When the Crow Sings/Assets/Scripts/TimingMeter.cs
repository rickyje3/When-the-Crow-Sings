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

    private bool movingRight = true; //Meter movement direction
    public bool meterActive = true;


    // Update is called once per frame
    void Update()
    {
        if (meterActive)
        {
            MoveMeter();
            if (Input.GetKeyDown(KeyCode.Space))
            {
                CheckSuccess();
            }
        }
    }

    private void MoveMeter()
    {
        if (movingRight)
        {
            sliderMeter.value += Time.deltaTime * speed;
            if (sliderMeter.value >= 1f)
            {
                movingRight = false;
            }
        }
        else
        {
            sliderMeter.value -= Time.deltaTime * speed;
            if (sliderMeter.value <= 0f)
            {
                movingRight = true;
            }
        }
    }

    private void CheckSuccess()
    {
        meterActive = false;

        if(sliderMeter.value >= targetMin && sliderMeter.value <= targetMax)
        {
            Debug.Log("Successful QTE");
        }
        else
        {
            Debug.Log("Failed QTE");
        }
    }
}
