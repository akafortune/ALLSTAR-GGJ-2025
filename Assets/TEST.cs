using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AudioManager;

public class TEST : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Audio.PlayMusic(MUSIC.LEVEL_2);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
            Audio.PlayMusic(MUSIC.LEVEL_3);
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Audio.PlaySFX(SFX.BURGER_TALK);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            Audio.PlaySFX(SFX.PHONE_RING);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            Audio.PlaySFX(SFX.PICK_UP_CLICK);
        }
        if (Input.GetKeyDown(KeyCode.H))
            Audio.PlaySFX(SFX.HANG_UP_TONE);
        if (Input.GetKeyDown(KeyCode.D))
            Audio.PlaySFX(SFX.HANG_UP_CLICK);

        if (Input.GetKeyDown(KeyCode.A))
            Audio.PlaySFX(SFX.FALL);
        if (Input.GetKeyDown(KeyCode.W))
            Audio.PlaySFX(SFX.CLOUD_LAND);
        if (Input.GetKeyDown(KeyCode.S))
            Audio.StopMusic();
        if (Input.GetKeyDown(KeyCode.V))
            Audio.PlaySFX(SFX.SCUTTLE);
    }
}
