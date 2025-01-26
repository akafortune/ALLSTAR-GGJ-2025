using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble_Square : MonoBehaviour
{
    bool touched = false, resetting = false;
    public float resetTime;
    private float resetTimer = 0;
    public bool justRan = false;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        justRan= true;

        if (touched)
        {
            bubbleKill();
        }

        if (resetting)
        {
            bubbleReset();
        }

        
        anim.SetBool("justRan", justRan);
    }

    void bubbleKill()
    {
        anim.SetBool("popped", true);
        this.GetComponent<BoxCollider2D>().enabled = false;
        touched = false;
        resetting = true;
        justRan= false;
    }

    void bubbleReset()
    {
        resetTimer += Time.deltaTime;

        if(resetTimer >= resetTime)
        {
            anim.SetBool("popped", false);
            resetting = false;
            resetTimer = 0;
            this.GetComponent<BoxCollider2D>().enabled = true;
            justRan= false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        touched = true;
    }
}
