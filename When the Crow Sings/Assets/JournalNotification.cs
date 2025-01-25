using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JournalNotification : MonoBehaviour
{
    [Header("InitializationThings")]
    public string message = "MSG_Not_Found";

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(disappearAfterDelay());
    }

    IEnumerator disappearAfterDelay()
    {
        yield return new WaitForSeconds(5f);
        GetComponent<Animator>().SetTrigger("GoOut");
    }
}
