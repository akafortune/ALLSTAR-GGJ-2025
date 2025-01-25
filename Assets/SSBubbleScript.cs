using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    private enum SlingStates
    {
        WINDING,
        LAUNCHING
    }

    public LaunchDirections[] launchDirections = new LaunchDirections[1];
    public GameObject bubbleRadius;

    private SlingStates slingState;
    public Bubble_Manager bubble;
    public float windSpeed;
    public float windDistance;
    public Vector2 windVector; 

    // Start is called before the first frame update
    void Start()
    {
        bubble = this.GetComponent<Bubble_Manager>();

        //sets the windup distance based on collider radius
        calculateWindDistance();

        //Target based on selected direction
        setWindingTarget();
    }

    // Update is called once per frame
    void Update()
    {
        if (bubble.bubbleState == Bubble_Manager.BubbleStates.OCCUPIED) {
            slingShot();
        }   
    }
//
    private void OnTriggerEnter2D(Collider2D collision){
        //Begin Winding up when player enters the bubble
        slingState = SlingStates.WINDING;
    }

    private void calculateWindDistance() {
        //Currently based on raw radius. Additional calculations can happen here as needed
        windDistance = bubbleRadius.GetComponent<CircleCollider2D>().radius;
    }

    private void setWindingTarget() {
        float[] windTarget = new float[2]; //x,y coordinate pair
        switch (launchDirections[0])
        {
            case LaunchDirections.UP:
                windTarget[0] = 0;
                windTarget[1] = windDistance;
                break;

            case LaunchDirections.UP_RIGHT:
                windTarget[0] = windDistance;
                windTarget[1] = windDistance;
                break;      

            case LaunchDirections.UP_LEFT:
                windTarget[0] = -windDistance;
                windTarget[1] = windDistance;
                break;  

            case LaunchDirections.DOWN:
                windTarget[0] = 0;
                windTarget[1] = -windDistance;
                break;

            case LaunchDirections.DOWN_LEFT:
                windTarget[0] = -windDistance;
                windTarget[1] = -windDistance;
                break;  
            
            case LaunchDirections.DOWN_RIGHT:
                windTarget[0] = windDistance;
                windTarget[1] = -windDistance;
                break;

            case LaunchDirections.LEFT:
                windTarget[0] = -windDistance;
                windTarget[1] = 0;
                break;

            case LaunchDirections.RIGHT:
                windTarget[0] = windDistance;
                windTarget[1] = 0;
                break;     
            default:
                Debug.Log("Please set a Launch Direction in the editor!");
                break;
        }

        windVector = new Vector2(windTarget[0] + this.gameObject.transform.position.x ,windTarget[1] + this.gameObject.transform.position.y); //Movetowards target
    }

    private void slingShot() {
        switch (slingState)
        {
            case SlingStates.WINDING:
                //Move player in selected Direction
                windup();
                break;
            case SlingStates.LAUNCHING:
                //Launch the player & check for dash input. 
                //Add velocity to player depending on timing score.
                launchPlayer();
                break;
            default:
                break;
        }
    }

    private void windup() {
        Debug.Log("Moving Player :3");
        this.gameObject.transform.position = Vector2.MoveTowards(this.gameObject.transform.position, windVector, windSpeed * Time.deltaTime);
    }

    private void launchPlayer() {
        Debug.Log("Not Ready Yet!");
    }


}
