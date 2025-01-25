using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AudioManager;

public class TEST : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Audio.PlaySFX(SFX.PHONE_TALK);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            Audio.PlaySFX(SFX.PHONE_RING);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            Audio.PlaySFX(SFX.PHONE_PICK_UP);
        }
        if (Input.GetKeyDown(KeyCode.H))
            Audio.PlaySFX(SFX.PHONE_TONE);
        if (Input.GetKeyDown(KeyCode.D))
            Audio.PlaySFX(SFX.PHONE_HANG_UP);

        if (Input.GetKeyDown(KeyCode.A))
            Audio.PlaySFX(SFX.FALL);
        if (Input.GetKeyDown(KeyCode.W))
            Audio.PlaySFX(SFX.CLOUD_LAND);
    }
}
