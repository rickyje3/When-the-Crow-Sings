using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StirringQTE : MonoBehaviour
{
    public GameObject displayBox;
    private int currentStep = 0;
    private bool correctKey;
    private bool countingDown;

    private KeyCode[] keySequence = { KeyCode.W, KeyCode.D, KeyCode.S, KeyCode.A }; // Rotating sequence

    private void Update()
    {
        if (!countingDown)
        {
            ShowCurrentKey();
            CheckInput();
        }
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
        countingDown = true;
        displayBox.GetComponent<Image>().color = correctKey ? Color.green : Color.red;

        yield return new WaitForSeconds(.3f); // Display feedback for 1.5 seconds

        displayBox.GetComponent<Image>().color = Color.white;

        if (correctKey)
        {
            // Move to the next step, cycling back to the start if needed
            currentStep = (currentStep + 1) % keySequence.Length;
        }

        correctKey = false;
        countingDown = false; // Ready for the next input
    }

    private IEnumerator Countdown()
    {
        yield return new WaitForSeconds(10);
        //failstate
    }
}


