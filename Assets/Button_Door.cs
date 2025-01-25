using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Door : MonoBehaviour
{
    public Collider2D myCol;
    public List<GameObject> buttons;
    
    void ButtonHit(GameObject button) //called in button detect script
    {
        Color buttonSprColor = button.GetComponent<SpriteRenderer>().color;
        buttonSprColor.a = 0.5f;
        button.GetComponent<SpriteRenderer>().color = buttonSprColor;
        bool willOpen = true;
        foreach (Transform child in transform)
        {
            if (child.gameObject.GetComponent<SpriteRenderer>().color.a > 0.5f)
            {
                willOpen = false;
            }
        }
        if (willOpen)
        {
            Open();
        }
    }

    void Open()
    {
        myCol.enabled = false;
    }
}
