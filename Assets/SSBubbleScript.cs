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
    public float vDashForce;
    public float hDashForce;
    public float dashTime;
    private float dashTimer;

    private SlingStates slingState;
    public Bubble_Manager bubble;
    public float windSpeed;
    public float returnSpeed;
    public float windDistance;
    public UnityEngine.Vector2 windVector;
    public UnityEngine.Vector2 releaseVector;
    private UnityEngine.Vector2 bubbleOrigin;

    //private Vector2 dashForceVector; 

    // Start is called before the first frame update
    void Start()
    {
        bubbleOrigin = new UnityEngine.Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.y);
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
        if(bubble.bubbleState != Bubble_Manager.BubbleStates.POPPED) {
            //Begin Winding up when player enters the bubble
            slingState = SlingStates.WINDING;
        }
        //hello        
    }

    private void calculateWindDistance() {
        //Currently based on raw radius. Additional calculations can happen here as needed
        windDistance = bubbleRadius.GetComponent<CircleCollider2D>().radius;
    }

    private void setWindingTarget() {
        float[] windTarget = new float[2]; //x,y coordinate pair
        float[] releaseTarget = new float[2];

        switch (launchDirections[0])
        {
            case LaunchDirections.UP:
                windTarget[0] = 0;
                windTarget[1] = windDistance;
                releaseTarget[0] = 0;
                releaseTarget[1] = -windDistance;
                break;

            case LaunchDirections.UP_RIGHT:
                windTarget[0] = windDistance;
                windTarget[1] =windDistance;
                releaseTarget[0] = windDistance;
                releaseTarget[1] = -windDistance;
                break;      

            case LaunchDirections.UP_LEFT:
                windTarget[0] = -windDistance;
                windTarget[1] = windDistance;
                releaseTarget[0] = windDistance;
                releaseTarget[1] = -windDistance;
                break;  

            case LaunchDirections.DOWN:
                windTarget[0] = 0;
                windTarget[1] = -windDistance;
                releaseTarget[0] = 0;
                releaseTarget[1] = windDistance;
                break;

            case LaunchDirections.DOWN_LEFT:
                windTarget[0] = -windDistance;
                windTarget[1] = -windDistance;
                releaseTarget[0] = windDistance;
                releaseTarget[1] = windDistance;
                break;  
            
            case LaunchDirections.DOWN_RIGHT:
                windTarget[0] = windDistance;
                windTarget[1] = -windDistance;
                releaseTarget[0] = -windDistance;
                releaseTarget[1] = windDistance;
                break;

            case LaunchDirections.LEFT:
                windTarget[0] = -windDistance;
                windTarget[1] = 0;
                releaseTarget[0] = windDistance;
                releaseTarget[1] = 0;
                break;

            case LaunchDirections.RIGHT:
                windTarget[0] = windDistance;
                windTarget[1] = 0;
                releaseTarget[0] = -windDistance;
                releaseTarget[1] = 0;
                break;     
            default:
                Debug.Log("Please set a Launch Direction in the editor!");
                break;
        }
        //sTarget = windTarget; //For launching later
        windVector = new UnityEngine.Vector2(windTarget[0] + this.gameObject.transform.position.x ,windTarget[1] + this.gameObject.transform.position.y); //Movetowards target
        releaseVector = new UnityEngine.Vector2((releaseTarget[0] * hDashForce), (releaseTarget[1] * vDashForce));
    }
//hello
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
        Rigidbody2D player = bubble.playerRB;

        bubble.bubbleState = Bubble_Manager.BubbleStates.POPPED; 
        slingState = SlingStates.LAUNCHING;
        player.gravityScale = 0;

        player.AddForce(releaseVector, ForceMode2D.Impulse);
        bubble.playerGO.GetComponent<Player_Movement>().dashing = true;

        bubble.playerGO.GetComponent<Player_Movement>().inFlight = true;
    }

    private void handleLaunch(){
        //return the bubble to origin
        this.gameObject.transform.position = UnityEngine.Vector2.MoveTowards(this.gameObject.transform.position, bubbleOrigin, returnSpeed * Time.deltaTime);

        Rigidbody2D player = bubble.playerRB;
        dashTimer += Time.deltaTime;
        if(dashTimer >= dashTime && this.transform.position.x == bubbleOrigin.x && this.transform.position.y == bubbleOrigin.y)
        {
            dashTimer = 0;
            slingState= SlingStates.FINISHED;
        }
        
    }


}
