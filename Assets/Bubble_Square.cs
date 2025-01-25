using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble_Square : MonoBehaviour
{
    bool touched = false, resetting = false;
    public float resetTime;
    private float resetTimer = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (touched)
        {
            bubbleKill();
        }

        if (resetting)
        {
            bubbleReset();
        }
    }

    void bubbleKill()
    {
        this.GetComponent<BoxCollider2D>().enabled = false;
        touched = false;
        resetting = true;
    }

    void bubbleReset()
    {
        resetTimer += Time.deltaTime;

        if(resetTimer >= resetTime)
        {
            resetting = false;
            resetTimer = 0;
            this.GetComponent<BoxCollider2D>().enabled = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        touched = true;
    }
}
