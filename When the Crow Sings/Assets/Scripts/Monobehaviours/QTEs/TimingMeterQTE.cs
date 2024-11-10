using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class TimingMeterQTE : QuickTimeEvent
{
    public Slider sliderMeter;
    public float speed = 2f;
    public float targetMin; //Range for successful hit
    public float targetMax;
    //public float targetValue;
    public Animator timingMeterAnimator;

    public RectTransform targetMinMarker;  
    public RectTransform targetMaxMarker;
    public RectTransform targetRangeHighlight;

    private bool movingRight = true; //Meter movement direction
    public bool meterActive = false;

    private void Start()
    {
        SetTargetRangeMarkers();
        RandomizeMeter();

        StartQTE();
        Debug.Log(meterActive);
    }

    // Update is called once per frame
    void Update()
    {
        if (meterActive)
        {
            MoveMeter();
            if (Input.GetKeyDown(KeyCode.Space) || (Input.GetKeyDown(KeyCode.Joystick1Button1)))
            {
                CheckSuccess();
            }
        }
    }

    public override void StartQTE()
    {
        //meterActive = true;
        if (meterActive)
        {
            Debug.Log("Started!");
            //timingMeterAnimator.SetBool("isOpen", true);
            SetTargetRangeMarkers();
        }
    }

        //Moves the handle up and down
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

    //Check if qte was successful
    private void CheckSuccess()
    {
        meterActive = false;

        if(sliderMeter.value >= targetMin && sliderMeter.value <= targetMax)
        {
            Debug.Log("Successful QTE");
            //RandomizeMeter();
            EndQTE();
        }
        else
        {
            Debug.Log("Failed QTE");
            //SetTargetRangeMarkers();
            //RandomizeMeter();
            EndQTE();
        }
    }

    public void EndQTE()
    {
        //timingMeterAnimator.SetBool("isOpen", false);
        meterActive = false;
        globalFinishedQteSignal.Emit();
    }

    public void RandomizeMeter()
    {
        /*targetValue = Random.Range(0.1f, 0.9f);
        targetMin = targetValue - 0.1f;
        targetMax = targetValue + 0.1f;*/

        //change these range values for accessibility settings
        targetMin = Random.Range(0.3f, 0.49f);
        targetMax = Random.Range(0.51f, 0.7f);
        //meterActive = true;
        
        //EndQTE();
    }

    //Set the target markers based off target range
    public void SetTargetRangeMarkers()
    {
        // Get the width of the slider.
        float sliderWidth = sliderMeter.GetComponent<RectTransform>().rect.width;

        // Calculate the X positions of the markers based on the target range (0 to 1 range).
        float minXPos = sliderWidth * targetMin;
        float maxXPos = sliderWidth * targetMax;

        // Set the positions of the target markers relative to the Fill Area.
        targetMinMarker.anchoredPosition = new Vector2(minXPos, targetMinMarker.anchoredPosition.y);
        targetMaxMarker.anchoredPosition = new Vector2(maxXPos, targetMaxMarker.anchoredPosition.y);

        // Set the size and position of the highlight area
        float highlightWidth = maxXPos - minXPos;  // Width of the highlighted area
        targetRangeHighlight.sizeDelta = new Vector2(highlightWidth, targetRangeHighlight.sizeDelta.y); // Adjust width
        targetRangeHighlight.anchoredPosition = new Vector2(minXPos + 0.5f, targetRangeHighlight.anchoredPosition.y); // Adjust position
        Debug.Log(targetRangeHighlight.sizeDelta + targetRangeHighlight.anchoredPosition);
    }


}
