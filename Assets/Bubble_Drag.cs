using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble_Drag : MonoBehaviour
{
    public enum BubbleState
    {
        UNOCCUPIED,
        OCCUPIED,
        POPPED
    }

    public float dragSpeed;
    public BubbleState bubbleState = BubbleState.UNOCCUPIED;
    public Rigidbody2D playerRB;
    public GameObject playerGameObject;
    public GameObject bubbleRadius;

   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if(bubbleState == BubbleState.UNOCCUPIED)
        {
            IndicatorCheck();
        }

        if (bubbleState == BubbleState.OCCUPIED)
        {
            DragPlayer();
            DashOut();
        }
    }

    void IndicatorCheck()
    {
        if(bubbleRadius.GetComponent<Bubble_Area>().inRange)
        {
            this.gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
        } else
        {
            this.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    void DragPlayer()
    {
        //TOUCH UP
        playerRB.velocity = new Vector2(0,0);
        playerRB.gravityScale = 0;
        playerGameObject.transform.position = Vector2.MoveTowards(playerGameObject.transform.position, this.gameObject.transform.position, dragSpeed *Time.deltaTime); ;
    }

    void DashOut()
    {
        if (Input.GetButtonDown("Dash"))
        {
            playerRB.gravityScale = 1;
            bubbleState = BubbleState.POPPED;
            playerGameObject.GetComponent<Player_Movement>().DashCheck();
            playerRB = null;
            playerGameObject = null;
            Destroy(bubbleRadius);
            this.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            playerGameObject = collision.gameObject;
            playerRB = playerGameObject.GetComponent<Rigidbody2D>();
            bubbleState= BubbleState.OCCUPIED;
            playerGameObject.GetComponent<Player_Movement>().canDash = true;
        }
    }
}
