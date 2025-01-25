using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { Play, Cutscene};

public class Game_State_Ctrl : MonoBehaviour
{
    GameState curState;

    int sceneNum = 0;
    int diaNum = 0;

    KeyCode diaKey;

    List<List<string>> allScenes;
    List<string> sceneA;
    List<string> sceneB;
    List<string> sceneC;

    void Start()
    {
        curState = GameState.Cutscene;

        diaKey = KeyCode.LeftShift;

        sceneA = new List<string> { "first line" , "second line"};
        sceneB = new List<string>();
        sceneC = new List<string>();
        allScenes = new List<List<string>> { sceneA, sceneB, sceneC };
    }

    void Update()
    {
        RunState();
    }

    void RunState()
    {
        switch (curState)
        {
            case GameState.Cutscene:
                RunDialogue();
                break;
            default: //Play
                break;
        }
    }

    void RunDialogue()
    {
        if (Input.GetKeyDown(diaKey))
        {
            List<string> curScene = allScenes[sceneNum];
            Debug.Log(curScene[diaNum]);  //placeholder
            diaNum++;
        }
    }
}
