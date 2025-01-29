using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Trigger : MonoBehaviour
{
    public Camera mainCam;
    public Collider2D myCol;
    public int timerBound;
    int timer = 0;
    private GameObject player;

    void Start() {
        player = GameObject.Find("Player");
    }
    
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("Player"))
        {
            mainCam.SendMessage("NextCam");
            TriggerCooldown();
        }
    }

    void TriggerCooldown()  //starts collider cooldown
    {
        if (myCol.enabled)
        {
            timer = 0;
            myCol.enabled = false;
        }
    }

    void Update() //controls timer for collider cooldown
    {
        if (timer < timerBound && !myCol.enabled)
        {
            timer++;
        }
        else if (!myCol.enabled)
        {
            myCol.enabled = true;
        }

        offScreenCheck();
    }

    void offScreenCheck() {
        if (!myCol.enabled) {
            //camera has just or is still shifting
            return;
        }

        float playerHeight = player.transform.position.y;
        float triggerHeight = this.transform.position.y;
        float screenHeight = 22; //screen height with some allowance

        if (playerHeight > triggerHeight) {
            //player stuck above the camera trigger
            mainCam.SendMessage("NextCam");
            TriggerCooldown();
        } else if (triggerHeight - playerHeight > screenHeight) { 
            //player stuck below camera trigger
            mainCam.SendMessage("PreviousCam");
            TriggerCooldown();
        }

    }
}
