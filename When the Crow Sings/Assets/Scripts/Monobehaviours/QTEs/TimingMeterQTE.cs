using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public QTEInteractable qteInteractable;

    [HideInInspector]
    public int winCount;
    public int winCounter;

    private bool movingRight = true; //Meter movement direction
    public bool meterActive = false;

    private void Start()
    {
        qteInteractable = FindObjectOfType<QTEInteractable>();

        SetTargetRangeMarkers();
        RandomizeMeter();
        //leave out when implementation added
        StartQTE();
    }

    // Update is called once per frame
    void Update()
    {
        if (meterActive)
        {
            MoveMeter();
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Joystick1Button1))
            {
                CheckSuccess();
            }
        }
    }

    public override void StartQTE()
    {
        Debug.Log("Started!");
        //timingMeterAnimator.SetBool("isOpen", true);
        meterActive = true;
        SetTargetRangeMarkers();
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
        //meterActive = false;

        if (sliderMeter.value >= targetMin && sliderMeter.value <= targetMax)
        {
            winCount++;
            Debug.Log(winCount + "/" + winCounter);
            if (winCount >= winCounter)
            {
                Debug.Log("Successful QTE");
                //RandomizeMeter();
                qteInteractable.audioSource.PlayOneShot(qteInteractable.successSound);
                SucceedQTE();
            }
            else if (winCounter > winCount)
            {
                Debug.Log("Else ifed");
                RandomizeMeter();
                SetTargetRangeMarkers();
                qteInteractable.audioSource.PlayOneShot(qteInteractable.successSound);
            }
        }
        else
        {
            Debug.Log("Failed QTE");
            //SetTargetRangeMarkers();
            //RandomizeMeter();
            qteInteractable.audioSource.PlayOneShot(qteInteractable.failSound);
            FailQTE();
        }
    }

    public override void SucceedQTE()
    {
        //timingMeterAnimator.SetBool("isOpen", false);
        //meterActive = false;
        SignalArguments args = new SignalArguments();
        args.boolArgs.Add(true);
        globalFinishedQteSignal.Emit(args);
    }
    public override void FailQTE()
    {
        SignalArguments args = new SignalArguments();
        args.boolArgs.Add(false);
        globalFinishedQteSignal.Emit(args);
    }

    public void RandomizeMeter()
    {
        /*targetValue = Random.Range(0.1f, 0.9f);
        targetMin = targetValue - 0.1f;
        targetMax = targetValue + 0.1f;*/

        //change these range values for accessibility settings
        targetMin = Random.Range(0.3f, 0.4f);
        targetMax = Random.Range(0.6f, 0.7f);
        meterActive = true;
        Debug.Log("Randomized");
        
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
        //Debug.Log(targetRangeHighlight.sizeDelta + targetRangeHighlight.anchoredPosition);

        Debug.Log("Markers reset");
    }


}
