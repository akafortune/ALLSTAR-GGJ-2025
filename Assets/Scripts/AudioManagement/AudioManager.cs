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
    [SerializeField] private AudioSource _music;
    private bool _musicPlaying = false;


    [Header("Bubble")]
    [SerializeField] private AudioSource _bubblePop;
    [SerializeField] private AudioSource _bubbleEntry;
    [SerializeField] private AudioSource _bubbleExit;
    [SerializeField] private AudioSource _bubbleReform;

    [Header("Phone")]
    [SerializeField] private AudioSource _phoneRing;
    private bool _phoneRingPlaying = false;
    [SerializeField] private AudioSource _phoneTalk;
    [SerializeField] private AudioSource _phonePickUp;
    [SerializeField] private AudioSource _phoneHangUp;
    [SerializeField] private AudioSource _phoneTone;
    private bool _phoneTonePlaying = false;

    [Header("Movement")]
    [SerializeField] private AudioSource _fall;
    private bool _fallPlaying = false;
    [SerializeField] private AudioSource _cloudLand;
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
            case SFX.BUBBLE_REFORM:
                _bubbleReform.Play();
                break;
            case SFX.PHONE_TALK:
                PlayPhoneTalk();
                break;
            case SFX.PHONE_PICK_UP:
                if (_phoneRingPlaying)
                {
                    _phoneRing.Stop();
                    _phoneRingPlaying = false;
                }
                _phonePickUp.Play();
                break;
            case SFX.PHONE_TONE:
                _phoneTone.Play();
                _phoneTonePlaying = true;
                break;
            case SFX.PHONE_HANG_UP:
                if(_phoneTonePlaying)
                {
                    _phoneTone.Stop();
                    _phoneTonePlaying = false;
                }
                _phoneHangUp.Play();
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
    #endregion

    public enum SFX { PHONE_RING, BUBBLE_ENTRY, BUBBLE_EXIT, BUBBLE_POP, BUBBLE_REFORM, FALL, 
        CLOUD_LAND, PHONE_TALK, PHONE_TONE, PHONE_PICK_UP, PHONE_HANG_UP }
    public enum MUSIC { LEVEL }
}
