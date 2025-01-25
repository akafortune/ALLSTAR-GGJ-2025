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
    public float walkForce, dashForce;
    public bool canDash = false;
    public Rigidbody2D playerRB;
    private int x =0, y = 0;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
        InputCheck();
        DashCheck();
    }

    private void FixedUpdate()
    {
        WalkCheck();
        
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

            playerRB.velocity = new Vector2(walkDir * walkForce * Time.deltaTime, playerRB.velocity.y);
        }
        else
        {
            playerRB.velocity = new Vector2(0, playerRB.velocity.y);
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
            directions[1] = Directions.DOWN;
            y = -1;
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            directions[1] = Directions.UP;
            y = 1;
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
            
            Vector2 dashForceVector = new Vector2(x * dashForce, y * dashForce);

            this.playerRB.AddForce(dashForceVector);

            canDash = false;
        }
    }
}
