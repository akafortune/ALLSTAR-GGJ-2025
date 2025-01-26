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

    public static GameState gameState;
    public CutsceneState csState;
    public static bool phoneCollected = false;
    public Transform enterMark, exitMark, levelEntry;
    public List<string> dialogue;
    private int dialogueIndex = 0;
    public List<bool> isBabble;
    private bool coroutineStarted = false;
    public float walkSpeed, exitSpeed, fadeTime;
    public GameObject player;
    public Rigidbody2D playerRB;
    private bool doneTyping = false;
    public SpriteRenderer fader;
    private float fadeTimer = 0;
    public TextMeshProUGUI dialogueDisplay;
    public float speakSpeed;
    public string sceneToLoad;
    // Start is called before the first frame update
    void Start()
    {
        gameState = GameState.CUTSCENE;
        csState= CutsceneState.FLYIN;
        player = GameObject.FindGameObjectWithTag("Player");
        playerRB = player.GetComponent<Rigidbody2D>();
        phoneCollected= false;
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
        }

        if(csState == CutsceneState.TALK)
        {
            if(!coroutineStarted)
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
        }

        if(csState == CutsceneState.FLYIN)
        {
            MoveToMark(levelEntry, false);
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

        if (Mathf.Abs(Vector2.Distance(player.transform.position, mark.position)) <= 0.3)
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
