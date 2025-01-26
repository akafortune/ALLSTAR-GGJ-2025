using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static AudioManager;

public class Fader : MonoBehaviour
{
    public float fadeTime;
    private float fadeTimer = 0;
    public SpriteRenderer fader;
    public bool isFading = false;
    public string sceneToLoad;
    public Button button;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isFading)
        {
            Fade();
        }

        if (Input.GetKey(KeyCode.Alpha1))
        {
            SceneManager.LoadScene("Level 1");
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            SceneManager.LoadScene("Level 2");
        }
        if (Input.GetKey(KeyCode.Alpha3))
        {
            SceneManager.LoadScene("Level 3");
        }

    }

    public void startFade()
    {
        isFading= true;
        Audio.StopMusic();
        Audio.PlaySFX(SFX.UI_SELECT);
        Audio.PlaySFX(SFX.GAME_ENTRANCE);
    }

    void Fade()
    {
        fadeTimer += Time.deltaTime;

        fader.color = new Color(fader.color.r, fader.color.g, fader.color.b, fadeTimer / fadeTime);

        if(fadeTimer > fadeTime)
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
