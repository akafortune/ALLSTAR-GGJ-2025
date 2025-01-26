using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AudioManager;

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
            Audio.PlaySFX(SFX.YAY);
            Cutscene_Control.phoneCollected= true;
            this.gameObject.SetActive(false);
        }
    }
}
