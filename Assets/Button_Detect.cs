using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//https://gamedev.stackexchange.com/questions/151670/how-to-detect-collision-occurring-on-a-child-object-from-a-parent-script
public class Button_Detect : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("Player"))
        {
            transform.parent.gameObject.SendMessage("ButtonHit", this.gameObject);
        }
    }
}
