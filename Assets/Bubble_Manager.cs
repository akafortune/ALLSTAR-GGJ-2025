using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Bubble_Drag;

public class Bubble_Manager : MonoBehaviour
{
    public enum BubbleStates
    {
        UNOCCUPIED,
        OCCUPIED,
        POPPED
    }

    public BubbleStates bubbleState = BubbleStates.UNOCCUPIED;
    public Rigidbody2D playerRB;
    public GameObject playerGO;
    public GameObject bubbleRadius;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(bubbleState == BubbleStates.UNOCCUPIED || bubbleState == BubbleStates.OCCUPIED)
        {
            IndicatorCheck();
        }
    }

    void IndicatorCheck()
    {
        if (bubbleRadius.GetComponent<Bubble_Area>().inRange)
        {
            this.gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
        }
        else
        {
            this.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            playerGO = collision.gameObject;
            playerRB = playerGO.GetComponent<Rigidbody2D>();
            bubbleState = BubbleStates.OCCUPIED;
            playerGO.GetComponent<Player_Movement>().canDash = true;
        }
    }
}
