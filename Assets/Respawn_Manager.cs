using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn_Manager : MonoBehaviour
{
    public static Transform currRespawnPos;

    public int respawnIndex = 0;
    public Transform[] respawnPosArr;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currRespawnPos = respawnPosArr[respawnIndex];
    }
}
