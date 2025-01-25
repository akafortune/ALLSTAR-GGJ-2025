using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSBubbleScript : MonoBehaviour
{
    public enum LaunchDirections 
    {
        UP,
        UP_RIGHT,
        UP_LEFT,
        DOWN,
        DOWN_LEFT,
        DOWN_RIGHT,
        LEFT,
        RIGHT
    }

    public LaunchDirections[] launchDirections = new LaunchDirections[1];
    //private Bubble_Drag bubble;

    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (bubble.bubbleState == Bubble_Drag.BubbleState.OCCUPIED)
        {
            DragPlayer();
            DashOut();
        }
        */ 
    }
}
