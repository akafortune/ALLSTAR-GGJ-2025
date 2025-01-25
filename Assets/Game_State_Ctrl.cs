using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { Play, Cutscene};

public class Game_State_Ctrl : MonoBehaviour
{
    GameState curState = GameState.Play;

    int sceneNum = 0;
    int diaNum = 0;

    KeyCode diaKey = KeyCode.Shift;

    List<List<string>> allScenes;
    List<string> sceneA;
    List<string> sceneB;
    List<string> sceneC;

    void Start()
    {
        sceneA = new List<string>();
        sceneB = new List<string>();
        sceneC = new List<string>();
        allScenes = new List<sceneA, sceneB, sceneC>();
    }

    void RunDialogue()
    {
        if (Input.GetKeyDown(diaKey))
        {
            diaNum++;
        }
    }
}
