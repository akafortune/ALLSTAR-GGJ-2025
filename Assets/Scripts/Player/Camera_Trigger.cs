using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Trigger : MonoBehaviour
{
    public Camera mainCam;
    public Collider2D myCol;
    public int timerBound;
    int timer = 0;

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
    }
}
