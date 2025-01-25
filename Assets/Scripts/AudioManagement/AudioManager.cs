using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioSource _music;
    private bool _musicPlaying = false;

    private void Awake()
    {
        //  = GetComponent<AudioSource>();

        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;

        DontDestroyOnLoad(gameObject);

        Debug.Log("Audio Manager awake...");
    }

    /*public void PlayLevelMusic()
    {
        PlayMusic();
    }*/

    public void PlayMusic()
    {
        if (!_musicPlaying)
        {
            _music.Play();
            _musicPlaying = true;
        }
    }

    public void StopMusic()
    {
        _music.Stop();
        _musicPlaying = false;
    }
}
