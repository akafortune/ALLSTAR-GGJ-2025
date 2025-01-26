using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AudioManager;

public class MusicPlayer : MonoBehaviour
{
    public int level;
    // Start is called before the first frame update
    void Start()
    {
        if(level== 0)
        {
            Audio.PlayMusic(MUSIC.TITLE);
        }

        if(level == 1)
        {
            Audio.PlayMusic(MUSIC.LEVEL_1);
        }

        if(level == 2)
        {
            Audio.PlayMusic(MUSIC.LEVEL_2);
        }

        if(level == 3)
        {
            Audio.PlayMusic(MUSIC.LEVEL_3);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
