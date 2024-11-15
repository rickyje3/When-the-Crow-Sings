using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Float : MonoBehaviour
{
    private bool goingUp = false;
    public float speed = .3f;
    private float upperLimit = .2f;
    private float lowerLimit = -.2f;

    private void Start()
    {
        upperLimit = transform.position.y + upperLimit;
        lowerLimit = transform.position.y;
    }

    void Update()
    {
        if (goingUp)
        {
            transform.position += Vector3.up * speed * Time.deltaTime;
            if (transform.position.y > upperLimit)
            {
                goingUp = false;
            }
        }
        else
        {
            transform.position += Vector3.down * speed * Time.deltaTime;
            if (transform.position.y < lowerLimit)
            {
                goingUp = true;
            }
        }

    }
}
