using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phone_Get : MonoBehaviour
{
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
        if (collision.gameObject.tag.Equals("Player"))
        {
            Cutscene_Control.phoneCollected= true;
            this.gameObject.SetActive(false);
        }
    }
}
