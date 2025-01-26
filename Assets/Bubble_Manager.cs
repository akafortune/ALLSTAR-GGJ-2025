using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Bubble_Drag;
using static AudioManager;

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
    public float bubbleRespawnTime, bubbleHoldTimer;
    public bool scaler;
    private float bHTInitial;
    private float bubbleRespawnTimer = 0;
    private bool resetRan = false;
    private Vector2 initialScale;
    private Animator anim;
    public Animation still, occupied, popped, reform;
    public bool justPlayed = false, playedPopSfx = false;
    private int animVal = 0;
    // Start is called before the first frame update
    void Start()
    {
        initialScale = new Vector2(this.transform.localScale.x, this.transform.localScale.y);
        bHTInitial = bubbleHoldTimer;
        anim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (bubbleState == BubbleStates.OCCUPIED)
        {
            BubbleHoldTimer();
            playerGO.GetComponent<Player_Movement>().inBubble= true;
        }

        if (bubbleState == BubbleStates.POPPED)
        {
            if(!playedPopSfx)
            {
                Audio.PlaySFX(SFX.BUBBLE_POP);
                playedPopSfx=true;
            }

            playerGO.GetComponent<Player_Movement>().inBubble = false;

            if (!resetRan)
            {
                BubbleReset();
                resetRan= true;
            }

            
            BubbleRespawnTimer();
            this.transform.localScale = initialScale;
        } else
        {
            playedPopSfx = false;
        }

        if(animVal != (int)bubbleState)
        {
            animVal = (int)bubbleState;
            anim.SetInteger("enumVal", animVal);
            justPlayed = false;
        } else
        {
            justPlayed = true;
        }

        anim.SetBool("justPlayed", justPlayed);
        //IndicatorCheck();
    }

    void BubbleHoldTimer()
    {
        bubbleHoldTimer -= Time.deltaTime;

        if(scaler)
        {
            float timeRatio = bubbleHoldTimer / bHTInitial;

            if (timeRatio > 0)
            {
                this.transform.localScale = initialScale * timeRatio;
            }
        }
        
        

        if(bubbleHoldTimer <= 0)
        {
            bubbleHoldTimer = bHTInitial;
            this.transform.localScale = initialScale;
            bubbleState = BubbleStates.POPPED;
        }
    }

    void BubbleRespawnTimer()
    {
        bubbleRespawnTimer += Time.deltaTime;
        bubbleRadius.SetActive(false);

        if(scaler)
        {
            this.transform.localScale = initialScale * bubbleRespawnTimer / bubbleRespawnTime;
        }
        

        if(bubbleRespawnTimer> bubbleRespawnTime)
        {
            Audio.PlaySFX(SFX.BUBBLE_REFORM);
            bubbleRespawnTimer = 0;
            bubbleState = BubbleStates.UNOCCUPIED;
            bubbleRadius.SetActive(true);
            this.GetComponent<BoxCollider2D>().enabled = true;
            resetRan = false;
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

        if(bubbleState == BubbleStates.POPPED)
        {
            this.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
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

    public void BubbleReset()
    {
        bubbleState = BubbleStates.POPPED;
        bubbleHoldTimer = bHTInitial;
        bubbleRespawnTimer = 0;
        //playerRB = null;
        //playerGO = null;
        this.GetComponent<BoxCollider2D>().enabled = false;
    }
}
