using System.Collections;
using System.Collections.Generic;
using System.Numerics;
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
        RELEASE,
        LAUNCHING,
        FINISHED
    }

    public LaunchDirections[] launchDirections = new LaunchDirections[1];
    public GameObject bubbleRadius;
    public float dashForce;
    public float dashTime;
    private float dashTimer;

    private SlingStates slingState;
    public Bubble_Manager bubble;
    public float windSpeed;
    public float windDistance;
    public UnityEngine.Vector2 windVector;

    public float[] sTarget;
    //private Vector2 dashForceVector; 

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
        if (bubble.bubbleState == Bubble_Manager.BubbleStates.OCCUPIED ||
                slingState == SlingStates.LAUNCHING || 
                slingState == SlingStates.RELEASE ) {
            slingShotManager();
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
        sTarget = windTarget; //For launching later
        windVector = new UnityEngine.Vector2(windTarget[0] + this.gameObject.transform.position.x ,windTarget[1] + this.gameObject.transform.position.y); //Movetowards target
    }

    private void slingShotManager() {
        switch (slingState)
        {
            case SlingStates.WINDING:
                //Move player in selected Direction
                windup();
                break;
            case SlingStates.RELEASE:
                //Launch the player & check for dash input. 
                //Add velocity to player depending on timing score.
                launchPlayer();
                break;
            case SlingStates.LAUNCHING:
                handleLaunch();
                break;
            default:
                break;
        }
    }

    private void windup() {
        this.gameObject.transform.position = UnityEngine.Vector2.MoveTowards(this.gameObject.transform.position, windVector, windSpeed * Time.deltaTime);

        //Has reached destination
        if(this.transform.position.x == windVector.x && this.transform.position.y == windVector.y) {
            slingState = SlingStates.RELEASE;
        }
    }

    private void launchPlayer() {

        //Inverse the direction. The base force is weak. Force is added depending on player's
        //input timing to the center
        float xval = this.gameObject.transform.position.x + -sTarget[0];
        float yval = this.gameObject.transform.position.y + -sTarget[1];

        Debug.Log(xval + " + " + yval);

        //dashForceVector = new Vector2(xval,yval);
        sTarget[0] = xval;
        sTarget[1] = yval;

        Rigidbody2D player = bubble.playerRB;
        //TODO: may need a new state. I just need all competing bubble logic to cease.
        //A launch API would also be kinda swell.
        bubble.bubbleState = Bubble_Manager.BubbleStates.POPPED; 

        //player.AddForce(dashForceVector, ForceMode2D.Impulse);
        slingState = SlingStates.LAUNCHING;
        player.gravityScale = 0;
    }

    private void handleLaunch(){
        Rigidbody2D player = bubble.playerRB;
        dashTimer += Time.deltaTime;

        //Vector2 dashForceVector = new Vector2(sTarget[0]*dashForce,sTarget[1]*dashForce);
        UnityEngine.Vector2 dashForceVector = new UnityEngine.Vector2(0,1);


        player.AddForce(dashForceVector, ForceMode2D.Impulse);
        Debug.Log("Handling Launch");
        bubble.playerGO.GetComponent<Player_Movement>().
        if(dashTimer >= dashTime)
        {
            dashTimer = 0;
            slingState= SlingStates.FINISHED;
            player.gravityScale = 1;
            Debug.Log("Launch Finished");
        }
        
    }


}
