using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Note: use "using static AudioManager.SFX" to more easily access SFX enum
 *       "using static AudioManager.Music" to more easily access Music enum
 *       
 *       Perhaps use "using static AudioManager"
 * 
 */

public class AudioManager : MonoBehaviour
{
    public static AudioManager Audio;

    #region AudioSources
    [SerializeField] private AudioSource _music;
    private bool _musicPlaying = false;

    [SerializeField] private AudioSource _bubblePop;
    [SerializeField] private AudioSource _bubbleEntry;
    [SerializeField] private AudioSource _bubbleExit;
    [SerializeField] private AudioSource _phone;
    [SerializeField] private AudioSource _fall;
    private bool _phonePlaying = false;
    private bool _fallPlaying = false;
    #endregion

    #region Awake
    private void Awake()
    {
        //  = GetComponent<AudioSource>();

        if (Audio != null)
            Destroy(gameObject);
        else
            Audio = this;

        DontDestroyOnLoad(gameObject);

        Debug.Log("Audio Manager awake...");
    }
    #endregion

    #region MusicCalls
    public void StopMusic()
    {
        _music.Stop();
        _musicPlaying = false;
    }

    public void PlayMusic(MUSIC music)
    {
        switch(music)
        {
            case MUSIC.LEVEL:
                _music.Play();
                _musicPlaying = true;
                if (!_musicPlaying)
                {
                    _music.Play();
                    _musicPlaying = true;
                }
                else
                {
                    _music.Stop();
                    _musicPlaying = false;
                }
                break;
        }
        if (!_musicPlaying)
        {
            _music.Play();
            _musicPlaying = true;
        }
    }
    #endregion

    #region SFX Calls
    public void PlaySFX(SFX sfx)
    {
        switch (sfx) {
            case SFX.BUBBLE_POP:
                _bubblePop.Play();
                break;
            case SFX.BUBBLE_ENTRY:
                _bubbleEntry.Play();
                break;
            case SFX.BUBBLE_EXIT:
                _bubbleExit.Play();
                break;
            case SFX.PHONE_RING:
                if (!_phonePlaying)
                {
                    _phone.Play();
                    _phonePlaying = true;
                } else
                {
                    _phone.Stop();
                    _phonePlaying = false;
                }
                break;
            case SFX.FALL:
                if(!_fallPlaying)
                {
                    _fall.Play();
                    _fallPlaying = true;
                }
                else
                {
                    _fall.Stop();
                    _fallPlaying = false;
                }
                break;
        }
    }
    #endregion
    
    public enum SFX { PHONE_RING, BUBBLE_ENTRY, BUBBLE_EXIT, BUBBLE_POP, FALL }
    public enum MUSIC { LEVEL }
}
