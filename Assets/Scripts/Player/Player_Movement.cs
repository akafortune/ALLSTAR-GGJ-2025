using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

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

    public Directions[] directions = new Directions[2]; // 0 = x 1 = y
    public float walkForce, vDashForce, hDashForce, dashTime, momentumFalloff, leftoverMomentum;
    public bool canDash = false, dashing = false;
    public Rigidbody2D playerRB;
    public Vector2 velocity;
    public Ground_Checker groundCheck;
    private int x =0, y = 0;
    private float dashTimer;

    public GameObject StateCtrl;
    public GameObject cutsceneSpot;
    bool hasPhone = false;

    // Start is called before the first frame update
    void Start()
    {
        groundCheck = this.GetComponentInChildren<Ground_Checker>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Game_State_Ctrl.curState == Game_State_Ctrl.GameState.Play)
        {
            velocity = playerRB.velocity;
            InputCheck();
            DashCheck();
        }
        else if (hasPhone)
        {
            MoveToRest();
        }
        else
        {
            playerRB.velocity = new Vector2(0f, 0f);
        }
        
    }

    private void FixedUpdate()
    {
        if (Game_State_Ctrl.curState == Game_State_Ctrl.GameState.Play)
        {
            WalkCheck();
        }
    }


    void MoveToRest()
    {
        if (Vector3.Distance(transform.position, cutsceneSpot.transform.position) > 0.2f)
        {
            transform.position = Vector3.MoveTowards(transform.position, cutsceneSpot.transform.position, Time.deltaTime * 10f);
        }
        else
        {
            StateCtrl.SendMessage("StartCutscene");
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

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("Phone"))
        {
            hasPhone = true;
            col.gameObject.SetActive(false);
            StateCtrl.SendMessage("HasPhone");
        }
        if (col.gameObject.tag.Equals("Finish"))
        {
            StateCtrl.SendMessage("ChangeState", Game_State_Ctrl.GameState.Cutscene);
        }
    }
}
