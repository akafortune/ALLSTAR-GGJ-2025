using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public float walkForce;
    public bool canDash = false;
    public Rigidbody2D playerRB;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        WalkCheck();

        DashCheck();
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

    void DashCheck()
    {

    }
}
