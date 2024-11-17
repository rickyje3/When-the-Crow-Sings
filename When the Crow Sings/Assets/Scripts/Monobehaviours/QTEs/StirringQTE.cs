using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class StirringQTE : QuickTimeEvent
{
    public GameObject displayBox;
    private int currentStep = 0;
    private bool correctKey;
    private bool countingDown;
    public int score = 1;
    public float timer = 8;

    private KeyCode[] keySequence = { KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D }; // Keyboard sequence
    private Vector2[] joystickSequence = { Vector2.up, Vector2.left, Vector2.down, Vector2.right }; // Joystick sequence
    private float inputThreshold = 0.8f; // Threshold for recognizing a joystick direction

    public QTEInteractable qteInteractable;
    public Slider slider;

    //private bool isControllerConnected;

    private void Update()
    {
        qteInteractable = FindObjectOfType<QTEInteractable>();
        slider = GetComponentInChildren<Slider>();

        if (!countingDown)
        {
            if (InputManager.IsControllerConnected)
            {
                ShowCurrentDirection();
                CheckJoystickInput();
            }
            else
            {
                ShowCurrentKey();
                CheckKeyboardInput();
            }
        }

        // Check for QTE completion
        if (score >= slider.maxValue)
        {
            qteInteractable.audioSource.PlayOneShot(qteInteractable.successSound);
            SucceedQTE();
        }

        // Timer countdown logic
        if (!countingDown && timer > 0)
        {
            timer -= Time.deltaTime;
            UpdateTimer(timer);
        }
        else if (timer <= 0)
        {
            Debug.Log("Time is up, QTE Failed");
            qteInteractable.audioSource.PlayOneShot(qteInteractable.failSound);
            FailQTE();
        }
    }

    private void UpdateTimer(float currentTime)
    {
        float time = Mathf.FloorToInt(currentTime % 60);
    }

    //keyboard
    private void ShowCurrentKey()
    {
        KeyCode currentKey = keySequence[currentStep];
        displayBox.GetComponentInChildren<TextMeshProUGUI>().text = currentKey.ToString();
    }

    //controller
    private void ShowCurrentDirection()
    {
        string direction = joystickSequence[currentStep].ToString();
        displayBox.GetComponentInChildren<TextMeshProUGUI>().text = direction;
    }

    private void CheckKeyboardInput()
    {
        if (Input.anyKeyDown)
        {
            KeyCode expectedKey = keySequence[currentStep];

            if (Input.GetKeyDown(expectedKey))
            {
                correctKey = true;
                StartCoroutine(KeyPressFeedback());
            }
            else
            {
                correctKey = false;
                StartCoroutine(KeyPressFeedback());
            }
        }
    }

    private void CheckJoystickInput()
    {
        Vector2 expectedDirection = joystickSequence[currentStep];
        Vector2 joystickInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (Vector2.Dot(joystickInput.normalized, expectedDirection) > inputThreshold)
        {
            correctKey = true;
            StartCoroutine(KeyPressFeedback());
        }
    }

    private IEnumerator KeyPressFeedback()
    {
        countingDown = true;

        if(correctKey) 
            displayBox.GetComponent<Image>().color = Color.green;

        yield return new WaitForSeconds(0.07f); // Time between transitions (less = faster)

        displayBox.GetComponent<Image>().color = Color.white;

        if (correctKey)
        {
            currentStep = (currentStep + 1) % keySequence.Length;
            score++;
            timer = 7; // Reset timer
        }
        else
        {
            //score = Mathf.Max(0, score - 1);
        }

        correctKey = false;
        countingDown = false; // Ready for next input
    }

    public override void SucceedQTE()
    {
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

    public override void StartQTE()
    {
        throw new System.NotImplementedException();
        //just kinda have this in here cuz quicktimeevent was getting mad at me :(
    }
}



