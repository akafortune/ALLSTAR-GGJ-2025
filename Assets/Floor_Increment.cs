using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor_Increment : MonoBehaviour
{
    public Respawn_Manager respawnManager;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        respawnManager.respawnIndex++;
        this.gameObject.SetActive(false);
    }
}
