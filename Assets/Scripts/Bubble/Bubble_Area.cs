using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble_Area : MonoBehaviour
{
    public bool inRange = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            collision.gameObject.GetComponent<Player_Movement>().canDash = true;
            inRange= true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            collision.gameObject.GetComponent<Player_Movement>().canDash = false;
            inRange= false;
        }
    }
}
