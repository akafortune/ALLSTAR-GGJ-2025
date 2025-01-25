using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


//https://medium.com/@jonbednez/how-to-create-typewriter-effect-in-ui-with-unity-a-step-by-step-tutorial-bcd6d33d86d4

public class Game_State_Ctrl : MonoBehaviour
{
    public enum GameState { Play, Cutscene };
    public static GameState curState;

    public TMP_Text textObj;

    int sceneNum = 0;
    int diaNum = 0;

    KeyCode diaKey;

    List<List<string>> allScenes;
    List<string> sceneA;
    List<string> sceneB;
    List<string> sceneC;
    List<string> curScene;

    float typeSpd = 0.1f;
    float timePerChar = 2f;
    float timer = 0;
    int charIndex = 0;

    bool sceneUnlocked = false;

    void Start()
    {
        curState = GameState.Play;

        diaKey = KeyCode.LeftShift;

        sceneA = new List<string> { "first line" , "second line", "third line", ""};
        sceneB = new List<string>();
        sceneC = new List<string>();
        allScenes = new List<List<string>> { sceneA, sceneB, sceneC };

        curScene = allScenes[sceneNum];
    }

    void Update()
    {
        RunState();
    }

    void ChangeState(GameState state)
    {
        curState = state;
    }

    void RunState()
    {
        switch (curState)
        {
            case GameState.Cutscene:
                if (sceneUnlocked)
                {
                    TypeControl();
                }
                //else: end level
                break;
            default: //Play
                break;
        }
    }

    void HasPhone()
    {
        sceneUnlocked = true;
    }

    void TypeControl() //Typewriter effect
    {
        curScene = allScenes[sceneNum];
        RunDialogue();
        if (timer < timePerChar)
        {
            timer += typeSpd;
        }
        else if (charIndex < curScene[diaNum].Length)
        {
            charIndex++;
            timer = 0f;
        }
    }

    void RunDialogue() //Display and input
    {
        string displDia = curScene[diaNum].Substring(0, charIndex);
        textObj.text = displDia;

        if (diaNum < curScene.Count - 1)
        {
            if (Input.GetKeyDown(diaKey) && displDia.Length == curScene[diaNum].Length)
            {
                diaNum++;
                charIndex = 0;
                timer = 0;
            }
            else if (Input.GetKeyDown(diaKey)) //skip to end of line
            {
                charIndex = curScene[diaNum].Length;
                timer = 0;
            }
        }
        else //end level
        {
            Debug.Log("End Level");
        }
    }

}
