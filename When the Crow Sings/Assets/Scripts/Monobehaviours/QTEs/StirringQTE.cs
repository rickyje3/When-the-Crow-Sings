using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StirringQTE : QuickTimeEvent
{
    public GameObject displayBox;
    private int currentStep = 0;
    private bool correctKey;
    private bool countingDown;
    public int score = 1;
    public float timer = 8;


    private KeyCode[] keySequence = { KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D }; // Rotating sequence

    private void Awake()
    {

    }

    private void Update()
    {
        if (!countingDown)
        {
            ShowCurrentKey();
            CheckInput();
        }

        if (score == 50)
        {
            SucceedQTE();
        }

        //If 8 seconds pass 
        if (!countingDown)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
                updateTimer(timer);
            }
            else
            {
                
                timer = 0;
                countingDown = false;
                FailQTE();
            }
        }
    }

    void updateTimer(float currentTime)
    {
        currentTime += 1;
        float time = Mathf.FloorToInt(currentTime % 60);
    }

    // Display the current expected key in the UI
    private void ShowCurrentKey()
    {
        KeyCode currentKey = keySequence[currentStep];
        displayBox.GetComponentInChildren<TextMeshProUGUI>().text = currentKey.ToString();
    }

    // Check if the correct key is pressed
    private void CheckInput()
    {
        if (Input.anyKeyDown) // Only trigger on a key press
        {
            KeyCode expectedKey = keySequence[currentStep];

            if (Input.GetKeyDown(expectedKey))
            {
                Debug.Log($"Correct Key: {expectedKey}");
                correctKey = true;
                StartCoroutine(KeyPressing());
            }
            else
            {
                Debug.Log("Incorrect Key");
                correctKey = false;
                StartCoroutine(KeyPressing());
            }
        }
    }

    // Coroutine to handle feedback and proceed to the next step
    private IEnumerator KeyPressing()
    {
        if (displayBox != null)
        {
            countingDown = true;               //display green if correctkey is true and red if false
            displayBox.GetComponent<Image>().color = correctKey ? Color.green : Color.red;

            yield return new WaitForSeconds(.12f); // Time between transition

            displayBox.GetComponent<Image>().color = Color.white;

            if (correctKey)
            {
                // Move to the next step cycle back to the start if needed
                currentStep = (currentStep + 1) % keySequence.Length;
                score++;
                timer = 7;
            }
            else score--;

            correctKey = false;
            countingDown = false; // Ready for the next input
        }
    }

    public override void StartQTE()
    {
        throw new System.NotImplementedException();
    }

    public override void SucceedQTE()
    {
        //timingMeterAnimator.SetBool("isOpen", false);
        SignalArguments args = new SignalArguments();
        args.boolArgs.Add(true);
        globalFinishedQteSignal.Emit(args);
    }

    public override void FailQTE()
    {
        Debug.Log("Time is up, QTE Failed");
        SignalArguments args = new SignalArguments();
        args.boolArgs.Add(false);
        globalFinishedQteSignal.Emit(args);
    }
}


