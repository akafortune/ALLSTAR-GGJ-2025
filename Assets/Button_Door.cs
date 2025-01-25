using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Door : MonoBehaviour
{
    public Collider2D myCol;
    public List<GameObject> buttons;
    
    void ButtonHit(GameObject button) //called in button detect script
    {
        button.SetActive(false);
        bool willOpen = true;
        foreach (Transform child in transform)
        {
            if (child.gameObject.activeInHierarchy)
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
        this.gameObject.SetActive(false);
    }
}
