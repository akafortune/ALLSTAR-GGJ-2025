using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;

public class Cutscene_Control : MonoBehaviour
{
    public enum GameState
    {
        GAMEPLAY,
        CUTSCENE
    }

    public enum CutsceneState
    {
        NONE,
        ENTER,
        TALK,
        EXIT,
        FADING,
        FLYIN
    }

    public enum Speaker
    {
        DOUBLEP,
        FISHNCH,
        SOCRATES
    }

    [SerializeField]
    public static GameState gameState;
    public CutsceneState csState;
    public static bool phoneCollected = false;
    public Transform enterMark, exitMark, levelEntry;
    public List<string> dialogue;
    public int dialogueIndex = 0;
    //public List<bool> isSeal;
    //List<Speaker> speakerList;
    public Speaker curSpeaker;
    private bool coroutineStarted = false;
    public float walkSpeed, exitSpeed, fadeTime;
    public GameObject player;
    public Rigidbody2D playerRB;
    private bool doneTyping = false;
    public SpriteRenderer fader;
    private float fadeTimer = 0;
    public TextMeshProUGUI dialogueDisplay;
    public GameObject UI;
    public float speakSpeed;
    public string sceneToLoad;
    public Animator portraitAnim;
    // Start is called before the first frame update
    void Start()
    {
        gameState = GameState.CUTSCENE;
        csState= CutsceneState.FLYIN;

        player = GameObject.FindGameObjectWithTag("Player");
        playerRB = player.GetComponent<Rigidbody2D>();

        phoneCollected= true;
        UI.SetActive(false);

        //speakerList = new List<Speaker> { Speaker.DOUBLEP, Speaker.FISHNCH, Speaker.SOCRATES };
    }

    // Update is called once per frame
    void Update()
    {
        if(gameState == GameState.CUTSCENE)
        {
            playerRB.velocity = Vector2.zero;
        }

        if(csState== CutsceneState.ENTER)
        {
            MoveToMark(enterMark, true);
            player.GetComponent<Player_Movement>().movementState = Player_Movement.MovementState.WALK;
        }

        if(csState == CutsceneState.TALK)
        {
            UI.SetActive(true);
            player.GetComponent<Player_Movement>().movementState = Player_Movement.MovementState.CALLING;

            //if (speaker)//isSeal[dialogueIndex])
            //{
            //portraitAnim.SetBool("seal", isSeal[dialogueIndex]);
            //}

            switch (curSpeaker)//speakerList[dialogueIndex])
            {
                case Speaker.DOUBLEP:
                    portraitAnim.SetInteger("speakerNum", 1);
                    break;
                case Speaker.FISHNCH:
                    portraitAnim.SetInteger("speakerNum", 2);
                    break;
                default:
                    portraitAnim.SetInteger("speakerNum", 3);
                    break;
            }

            if (!coroutineStarted)
            {
                StartCoroutine(Typewriter());
                coroutineStarted = true;
            }

            if(dialogueDisplay.text == dialogue[dialogueIndex])
            {
                StopCoroutine(Typewriter());
                doneTyping= true;
            }

            if(doneTyping && Input.GetButtonDown("Dash"))
            {
                ResetText();
            }
        }

        if (csState == CutsceneState.EXIT)
        {
            
            MoveToMark(exitMark, false);
            Fade();
            player.GetComponent<Player_Movement>().movementState = Player_Movement.MovementState.DASH;
        }

        if(csState == CutsceneState.FLYIN)
        {
            MoveToMark(levelEntry, false);
            player.GetComponent<Player_Movement>().movementState = Player_Movement.MovementState.DASH;
        }
    }

    void Fade()
    {
        fadeTimer += Time.deltaTime;

        fader.color = new Color (fader.color.r, fader.color.g, fader.color.b,fadeTimer/fadeTime);

        if(fadeTimer >= fadeTime)
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }

    void ResetText()
    {
        dialogueIndex++;
        coroutineStarted = false;
        dialogueDisplay.text = "";
        doneTyping = false;
        if(dialogueIndex >= dialogue.Count)
        {
            csState = CutsceneState.EXIT;
            UI.SetActive(false);
        }
    }

    void MoveToMark(Transform mark, bool walking)
    {
        float speed = 0;

        if(walking)
        {
            speed = walkSpeed;
        } else
        {
            speed = exitSpeed;
        }

        player.transform.position = Vector2.MoveTowards(player.transform.position, mark.position, speed * Time.deltaTime);

        if (Mathf.Abs(Vector2.Distance(player.transform.position, mark.position)) <= 1)
        {
            if(csState == CutsceneState.ENTER)
            {
                if (phoneCollected)
                {
                    csState = CutsceneState.TALK;
                }
                else
                {
                    csState = CutsceneState.EXIT;
                }
            }

            if(csState == CutsceneState.FLYIN)
            {
                gameState = GameState.GAMEPLAY;
                csState = CutsceneState.NONE;
            }
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            Debug.Log("ran dia");
            gameState = GameState.CUTSCENE;
            csState = CutsceneState.ENTER;
            this.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    IEnumerator Typewriter()
    {
        foreach(char letter in dialogue[dialogueIndex])
        {
            dialogueDisplay.text += letter;
            yield return new WaitForSeconds(speakSpeed);
        }
    }
}
