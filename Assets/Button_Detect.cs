using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//https://gamedev.stackexchange.com/questions/151670/how-to-detect-collision-occurring-on-a-child-object-from-a-parent-script
public class Button_Detect : MonoBehaviour
{
    Collider2D myCol;
    public SpriteRenderer sr;
    public Sprite on, off;

    void Start()
    {
        myCol = GetComponent<Collider2D>();
        sr.sprite = off;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("Player"))
        {
            transform.parent.gameObject.SendMessage("ButtonHit", this.gameObject.GetComponent<SpriteRenderer>());
            sr.sprite = on;
            myCol.enabled = false;
        }
    }
}
