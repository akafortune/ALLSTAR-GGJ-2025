using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;
using static AudioManager;

public class Player_Movement : MonoBehaviour
{
    public enum Directions
    {
        NONE,
        UP,
        DOWN,
        RIGHT,
        LEFT
    }

    public enum MovementState
    {
        STAND,
        WALK,
        DASH,
        DEATH,
        HOVER,
        FALL,
        JUMP,
        CALLING
    }

    public MovementState movementState;
    public Directions[] directions = new Directions[2]; // 0 = x 1 = y
    public float walkForce, vDashForce, hDashForce, dashTime, momentumFalloff, leftoverMomentum, deathTime, walkThresh;
    public bool canDash = false, dashing = false, inBubble = true, dead = false;
    public Rigidbody2D playerRB;
    public Vector2 velocity;
    public Ground_Checker groundCheck;
    public int animatorValue = 0;
    public bool playedSfx = false, stoppedSfx = false;
    private int x =0, y = 0;
    private float dashTimer = 0, deathTimer = 0;
    private Animator anim;
    private SpriteRenderer sr;
    private Cutscene_Control csc;
    // Start is called before the first frame update
    void Start()
    {
        groundCheck = this.GetComponentInChildren<Ground_Checker>();
        anim = this.GetComponent<Animator>();
        sr = this.GetComponent<SpriteRenderer>();
        csc = this.GetComponent<Cutscene_Control>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!dead && Cutscene_Control.gameState != Cutscene_Control.GameState.CUTSCENE)
        {
            velocity = playerRB.velocity;
            InputCheck();
            DashCheck();
            StateDeterminer();
        } else if (dead)
        {
            DeathClock();
        }
    }

    private void FixedUpdate()
    {
        if (!dead && Cutscene_Control.gameState == Cutscene_Control.GameState.GAMEPLAY)
        {
            WalkCheck();
        }
    }

    void StateDeterminer()
    {
        if(groundCheck.grounded)
        {
            if(Mathf.Abs(playerRB.velocity.x) <=walkThresh)
            {
                movementState = MovementState.STAND;
            } else
            {
                movementState = MovementState.WALK;
            }
        } else
        {
            if(inBubble)
            {
                movementState = MovementState.HOVER;
            }
            else
            {
                if(dashing)
                {
                    movementState = MovementState.DASH;
                } else
                {
                    if(playerRB.velocity.y > 0)
                    {
                        movementState = MovementState.JUMP;
                    } else
                    {
                        movementState = MovementState.FALL;
                    }
                }
            }
        }

        if(playerRB.velocity.x > 0)
        {
            sr.flipX = true;
        } else
        {
            sr.flipX = false;
        }

        animatorValue = (int)movementState;
        anim.SetInteger("animVal", animatorValue);
    }

    void DeathClock()
    {
        deathTimer += Time.deltaTime;
        movementState = MovementState.DEATH;

        if(deathTimer > deathTime)
        {
            this.transform.position = Respawn_Manager.currRespawnPos.position;
            deathTimer = 0;
            //this.playerRB.velocity = new Vector2(0, 0);
            dead = false;
            this.GetComponent<BoxCollider2D>().enabled = true;
        }
    }


    void WalkCheck()
    {
        if (Input.GetAxis("Horizontal") != 0)
        {
            int walkDir = 0;

            if (Input.GetAxis("Horizontal") > 0)
            {
                walkDir = 1;
            }
            else
            {
                walkDir = -1;
            }

            if (!dashing)
            {
                playerRB.velocity = new Vector2((walkDir * walkForce * Time.deltaTime), playerRB.velocity.y) ;
            }
        }
        else
        {
            if(!dashing)
            {
                if(!groundCheck.grounded)
                {
                    playerRB.velocity = new Vector2(playerRB.velocity.x, playerRB.velocity.y);
                } else
                {
                    playerRB.velocity = new Vector2(0, playerRB.velocity.y);
                }
                
            }
        }
    }

    void InputCheck()
    {
        if(Input.GetAxis("Horizontal") > 0)
        {
            directions[0] = Directions.RIGHT;
            x = 1;
        } else if (Input.GetAxis("Horizontal") < 0)
        {
            directions[0] = Directions.LEFT;
            x = -1;
        } else
        {
            directions[0] = Directions.NONE;
            x = 0;
        }

        if (Input.GetAxis("Vertical") > 0)
        {
            directions[1] = Directions.UP;
            y = 1;
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            directions[1] = Directions.DOWN;
            y = -1;
        }
        else
        {
            directions[1] = Directions.NONE;
            y = 0;
        }
    }

    public void DashCheck()
    {
        if(Input.GetButtonDown("Dash") && canDash)
        {
            UnityEngine.Debug.Log("Ran");

            Vector2 dashForceVector;

            if (directions[1] == Directions.UP)
            {
                dashForceVector = new Vector2(x * vDashForce, y * vDashForce);
            } else
            {
                dashForceVector = new Vector2(x* hDashForce, y* hDashForce);
            }
            

            this.playerRB.AddForce(dashForceVector, ForceMode2D.Impulse);

            canDash = false;

            dashing= true;

            playerRB.gravityScale = 0;
        }

        if (dashing)
        {
            dashTimer += Time.deltaTime;

            if(dashTimer >= dashTime)
            {
                dashTimer = 0;
                dashing= false;
                playerRB.gravityScale = 1;
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Hazard"))
        {
            dead = true;
            this.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
