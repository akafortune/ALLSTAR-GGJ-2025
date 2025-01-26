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
    [Header("Music")]
    [SerializeField] private AudioSource _levelOneMusic;
    [SerializeField] private AudioSource _levelTwoMusic;
    [SerializeField] private AudioSource _levelThreeMusic;
    private bool _musicPlaying = false;


    [Header("Bubble")]
    [SerializeField] private AudioSource _bubblePop;
    [SerializeField] private AudioSource _bubbleEntry;
    [SerializeField] private AudioSource _bubbleExit;
    [SerializeField] private AudioSource _bubbleReform;
    [SerializeField] private AudioSource _squareBounce;

    [Header("Phone")]
    [SerializeField] private AudioSource _phoneRing;
    private bool _phoneRingPlaying = false;
    [SerializeField] private AudioSource _phoneTalk;
    [SerializeField] private AudioSource _pickUpClick;
    [SerializeField] private AudioSource _hangUpClick;
    [SerializeField] private AudioSource _hangUpTone;
    private bool _hangUpTonePlaying = false;

    [Header("Movement")]
    [SerializeField] private AudioSource _fall;
    private bool _fallPlaying = false;
    [SerializeField] private AudioSource _cloudLand;
    [SerializeField] private AudioSource _scuttle;
    private bool _scuttlePlaying = false;
    #endregion

    #region Awake
    private void Awake()
    {
        if (Audio != null)
            Destroy(gameObject);
        else
            Audio = this;

        DontDestroyOnLoad(gameObject);

        Debug.Log("Audio Manager awake...");
    }
    #endregion

    #region MusicCalls
    public void PlayMusic(MUSIC music)
    {
        switch(music)
        {
            case MUSIC.LEVEL_1:
                if (!_musicPlaying)
                {
                    _levelOneMusic.Play();
                    _musicPlaying = true;
                }
                else
                {
                    StopMusic();
                    _levelOneMusic.Play();
                    _musicPlaying = true;
                }
                break;
            case MUSIC.LEVEL_2:
                if (!_musicPlaying)
                {
                    _levelTwoMusic.Play();
                    _musicPlaying = true;
                }
                else
                {
                    StopMusic();
                    _levelTwoMusic.Play();
                    _musicPlaying = true;
                }
                break;
            case MUSIC.LEVEL_3:
                if(!_musicPlaying)
                {
                    _levelThreeMusic.Play();
                    _musicPlaying = true;
                }
                else
                {
                    StopMusic();
                    _levelThreeMusic.Play();
                    _musicPlaying = true;
                }
                break;
        }
    }

    public void StopMusic()
    {
        _levelOneMusic.Stop();
        _levelTwoMusic.Stop();
        _levelThreeMusic.Stop();
        _musicPlaying = false;
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
            case SFX.BUBBLE_REFORM:
                _bubbleReform.Play();
                break;
            case SFX.PHONE_TALK:
                PlayPhoneTalk();
                break;
            case SFX.PICK_UP_CLICK:
                if (_phoneRingPlaying)
                {
                    _phoneRing.Stop();
                    _phoneRingPlaying = false;
                }
                _pickUpClick.Play();
                break;
            case SFX.HANG_UP_TONE:
                _hangUpTone.Play();
                _hangUpTonePlaying = true;
                break;
            case SFX.HANG_UP_CLICK:
                if(_hangUpTonePlaying)
                {
                    _hangUpTone.Stop();
                    _hangUpTonePlaying = false;
                }
                _hangUpClick.Play();
                break;
            case SFX.PHONE_RING:
                if (!_phoneRingPlaying)
                {
                    _phoneRing.Play();
                    _phoneRingPlaying = true;
                } else
                {
                    _phoneRing.Stop();
                    _phoneRingPlaying = false;
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
            case SFX.CLOUD_LAND:
                if (_fallPlaying)
                {
                    _fall.Stop();
                    _fallPlaying = false;
                }
                _cloudLand.Play();
                break;
            case SFX.SQUARE_BOUNCE:
                _squareBounce.Play();
                break;
            case SFX.BURGER_TALK:
                PlayBurgerTalk();
                break;
            case SFX.SCUTTLE:
                if(!_scuttlePlaying)
                {
                    _scuttle.Play();
                    _scuttlePlaying = true;
                }
                else
                {
                    _scuttle.Stop();
                    _scuttlePlaying = false;
                }
                break;
                
        }
    }

    private void PlayPhoneTalk()
    {
        int rnd = UnityEngine.Random.Range(0, 3);
        Debug.Log(rnd);

        if(rnd == 0)
        {
            _phoneTalk.clip = Resources.Load<AudioClip>("SFX/PhoneSounds/PHONE_TALK_1");
            Debug.Log("1");
        }
        else if(rnd == 1)
        {
            _phoneTalk.clip = Resources.Load<AudioClip>("SFX/PhoneSounds/PHONE_TALK_2");
            Debug.Log("2");
        }
        else
        {
            _phoneTalk.clip = Resources.Load<AudioClip>("SFX/PhoneSounds/PHONE_TALK_3");
            Debug.Log("3");
        }
        _phoneTalk.Play();
    }

    private void PlayBurgerTalk()
    {
        string path = "SFX/PhoneSounds/CharacterVoices/Burger/";
        int rnd = UnityEngine.Random.Range(0, 4);
        Debug.Log(rnd);

        if (rnd == 0)
        {
            _phoneTalk.clip = Resources.Load<AudioClip>(path + "BURGER_TALK_1");
            Debug.Log("1");
        }
        else if (rnd == 1)
        {
            _phoneTalk.clip = Resources.Load<AudioClip>(path + "BURGER_TALK_2");
            Debug.Log("2");
        }
        else if (rnd == 2)
        {
            _phoneTalk.clip = Resources.Load<AudioClip>(path + "BURGER_TALK_3");
            Debug.Log("3");
        }
        else
        {
            _phoneTalk.clip = Resources.Load<AudioClip>(path + "BURGER_TALK_4");
            Debug.Log("4");
        }
        _phoneTalk.Play();
    }
    #endregion

    #region Enums
    public enum SFX { PHONE_RING, BUBBLE_ENTRY, BUBBLE_EXIT, BUBBLE_POP, BUBBLE_REFORM, FALL, 
        CLOUD_LAND, PHONE_TALK, HANG_UP_TONE, PICK_UP_CLICK, HANG_UP_CLICK, SQUARE_BOUNCE,
        BURGER_TALK, SCUTTLE }
    public enum MUSIC { LEVEL_1, LEVEL_2, LEVEL_3 }
    #endregion
}
