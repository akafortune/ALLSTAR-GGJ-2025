using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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

    private static Dictionary<SFX, Action> SFXCalls;
    private static Dictionary<MUSIC, Action> MusicCalls;

    #region AudioSources
    [Header("Music")]
    [SerializeField] private AudioSource _levelOneMusic;
    [SerializeField] private AudioSource _levelTwoMusic;
    [SerializeField] private AudioSource _levelThreeMusic;
    [SerializeField] private AudioSource _titleMusic;
    private bool _musicPlaying = false;


    [Header("Bubble")]
    [SerializeField] private AudioSource _bubblePop;
    [SerializeField] private AudioSource _bubbleEntry;
    [SerializeField] private AudioSource _bubbleExit;
    [SerializeField] private AudioSource _bubbleReform;
    [SerializeField] private AudioSource _squareBounce;
    [SerializeField] private AudioSource _destructivePop;

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

    [Header("Environment")]
    [SerializeField] private AudioSource _door;

    [Header("UI")]
    [SerializeField] private AudioSource _uiHover;
    [SerializeField] private AudioSource _uiSelect;
    [SerializeField] private AudioSource _gameEntrance;
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

        SFXCalls = new Dictionary<SFX, Action>
        {
            {SFX.BUBBLE_ENTRY, PlayBubbleEntry },
            {SFX.BUBBLE_EXIT, PlayBubbleExit },
            {SFX.BUBBLE_POP, PlayBubblePop },
            {SFX.BUBBLE_REFORM, PlayBubbleReform },
            {SFX.SCUTTLE, PlayScuttle },
            {SFX.CLOUD_LAND, PlayCloudLand },
            {SFX.DOOR, PlayDoor },
            {SFX.BURGER_TALK, PlayBurgerTalk },
            {SFX.FALL, PlayFall },
            {SFX.HANG_UP_CLICK, PlayHangUpClick },
            {SFX.HANG_UP_TONE, PlayHangUpTone },
            {SFX.PHONE_RING, PlayPhoneRing },
            {SFX.SQUARE_BOUNCE, PlaySquareBounce },
            {SFX.PICK_UP_CLICK, PlayPickUpClick },
            {SFX.UI_SELECT, PlayUISelect },
            {SFX.UI_HOVER, PlayUIHover },
            {SFX.GAME_ENTRANCE, PlayGameEntrance },
            {SFX.DESTRUCTIVE_POP, DestructivePop }
        };

        MusicCalls = new Dictionary<MUSIC, Action>
        {
            {MUSIC.LEVEL_1, PlayLevelOneMusic },
            {MUSIC.LEVEL_2, PlayLevelTwoMusic },
            {MUSIC.LEVEL_3, PlayLevelThreeMusic },
            {MUSIC.TITLE, PlayTitleMusic }
        };
    }
    #endregion

    #region MusicCalls
    public void PlayMusic(MUSIC music)
    {
        Action action = MusicCalls[music];
        action.Invoke();
    }

    public void StopMusic()
    {
        _levelOneMusic.Stop();
        _levelTwoMusic.Stop();
        _levelThreeMusic.Stop();
        _titleMusic.Stop();
        _musicPlaying = false;
    }

    private void PlayLevelOneMusic()
    {

        if(_musicPlaying)
        {
            StopMusic();
        }
        _levelOneMusic.Play();
        _musicPlaying = true;
    }

    private void PlayLevelTwoMusic()
    {
        if (_musicPlaying)
        {
            StopMusic();
        }
        _levelTwoMusic.Play();
        _musicPlaying = true;
    }

    private void PlayLevelThreeMusic()
    {
        if (_musicPlaying)
        {
            StopMusic();
        }
        _levelThreeMusic.Play();
        _musicPlaying = true;
    }

    private void PlayTitleMusic()
    {
        if (_musicPlaying)
        {
            StopMusic();
        }
        _titleMusic.Play();
        _musicPlaying = true;
    }
    #endregion

    #region SFX Calls
    public void PlaySFX(SFX sfx)
    {
        Action action = SFXCalls[sfx];
        action.Invoke();
    }

    private void PlayUISelect()
    {
        _uiSelect.Play();
    }

    private void PlayUIHover()
    {
        _uiHover.Play();
    }

    private void PlayGameEntrance()
    {
        _gameEntrance.Play();
    }

    private void DestructivePop()
    {
        _destructivePop.Play();
    }
    
    private void PlayBubblePop()
    {
        _bubblePop.Play();
    }

    private void PlayBubbleEntry()
    {
        if(_fallPlaying)
        {
            _fall.Stop();
            _fallPlaying = false;
        }
        _bubbleEntry.Play();
    }

    private void PlayBubbleExit()
    {
        _bubbleExit.Play();
    }

    private void PlayBubbleReform()
    {
        _bubbleReform.Play();
    }

    private void PlaySquareBounce()
    {
        if (_fallPlaying)
        {
            _fall.Stop();
            _fallPlaying = false;
        }
        _squareBounce.Play();
    }

    private void PlayPhoneRing()
    {
        if(!_phoneRingPlaying)
        {
            _phoneRing.Play();
            _phoneRingPlaying = true;
        }
        else
        {
            _phoneRing.Stop();
            _phoneRingPlaying = false;
        }
    }

    private void PlayPickUpClick()
    {
        if (!_phoneRingPlaying)
        {
            _pickUpClick.Play();
        } else
        {
            PlayPhoneRing();
            _pickUpClick.Play();
        }
    }

    private void PlayHangUpTone()
    {
        if(!_hangUpTonePlaying)
        {
            _hangUpTone.Play();
            _hangUpTonePlaying = true;
        }
        else
        {
            _hangUpTone.Stop();
            _hangUpTonePlaying = false;
        }
    }

    private void PlayHangUpClick()
    {
        if(!_hangUpTonePlaying)
        {
            _hangUpClick.Play();
        }
        else
        {
            PlayHangUpTone();
            _hangUpClick.Play();
        }
    }

    private void PlayFall()
    {
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
    }

    private void PlayCloudLand()
    {
        if (_fallPlaying)
        {
            _fall.Stop();
            _fallPlaying = false;
        }
        _cloudLand.Play();
    }

    private void PlayScuttle()
    {
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
    }

    private void PlayDoor()
    {
        _door.Play();
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
        BURGER_TALK, SCUTTLE, DOOR, UI_HOVER, UI_SELECT, GAME_ENTRANCE, DESTRUCTIVE_POP }
    public enum MUSIC { LEVEL_1, LEVEL_2, LEVEL_3, TITLE }
    #endregion
}
