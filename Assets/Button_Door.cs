using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Door : MonoBehaviour
{
    public Collider2D myCol;
    public GameObject moveTarget;

    public float doorSpeed;

    List<SpriteRenderer> buttons;
    bool moveEnd = false;

    void Start()
    {
        buttons = new List<SpriteRenderer>();
        foreach (Transform child in transform)
        {
            buttons.Add(child.gameObject.GetComponent<SpriteRenderer>());
        }
    }
    
    void ButtonHit(SpriteRenderer curButton) //called in button detect script
    {
        Color buttonSprColor = curButton.color;
        buttonSprColor.a = 0.5f;
        curButton.color = buttonSprColor;

        Open();
    }

    void Open()
    {
        bool willOpen = true;

        foreach (SpriteRenderer button in buttons)
        {
            if (button.color.a > 0.5f)
            {
                willOpen = false;
            }
        }
        if (willOpen)
        {
            transform.DetachChildren();
            myCol.enabled = false;
        }
    }

    void Update()
    {
        if (!myCol.enabled && !moveEnd)
        {
            transform.position = Vector3.MoveTowards(transform.position, moveTarget.transform.position, Time.deltaTime * doorSpeed);
            if (Vector3.Distance(transform.position, moveTarget.transform.position) < 0.2f)
            {
                moveEnd = true;
            }
        }
    }
}
