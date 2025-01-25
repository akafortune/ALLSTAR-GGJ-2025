using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble_Dash_Out : MonoBehaviour
{
    public Bubble_Manager manager;
    // Start is called before the first frame update
    void Start()
    {
        manager= GetComponent<Bubble_Manager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(manager.bubbleState == Bubble_Manager.BubbleStates.OCCUPIED)
        {
            DashOut();
        }
    }


    void DashOut()
    {
        if (Input.GetButtonDown("Dash"))
        {
            manager.playerRB.gravityScale = 1;
            manager.bubbleState = Bubble_Manager.BubbleStates.POPPED;
            manager.playerGO.GetComponent<Player_Movement>().DashCheck();
            manager.playerRB = null;
            manager.playerGO = null;
            Destroy(manager.bubbleRadius);
            this.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
