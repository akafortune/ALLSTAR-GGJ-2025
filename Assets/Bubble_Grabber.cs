using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Bubble_Drag;

public class Bubble_Grabber : MonoBehaviour
{
    public Bubble_Manager manager;
    public float dragSpeed;
    // Start is called before the first frame update
    void Start()
    {
        manager = this.GetComponent<Bubble_Manager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(manager.bubbleState == Bubble_Manager.BubbleStates.OCCUPIED)
        {
            DragPlayer();
        }
    }




    void DragPlayer()
    {
        //TOUCH UP
        manager.playerRB.velocity = new Vector2(0, 0);
        //manager.playerRB.gravityScale = 0;
        manager.playerGO.transform.position = Vector2.MoveTowards(manager.playerGO.transform.position, this.gameObject.transform.position, dragSpeed * Time.deltaTime); ;
    }

    
}
