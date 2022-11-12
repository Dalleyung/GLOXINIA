using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static float volume = 1f;
    public static float effectVolume = 1f;
    public static float BGMVolume = 1f;

    public static bool AllMute = false;
    public static bool effectMute = false;
    public static bool BGMMute = false;

    public AudioSource Effect_audioSource;
    public AudioSource BGM_audioSource;
    public AudioSource Tile_audioSource;

    public AudioClip battleBGM;
    public AudioClip titleBGM;
    public AudioClip feverBGM;
    public AudioClip rageBGM;
    public AudioClip victoryBGM;
    public AudioClip defeatBGM;
    public AudioClip cowAttack;
    public AudioClip cowDead;
    public AudioClip demonAttack;
    public AudioClip demonDead1;
    public AudioClip demonDead2;
    public AudioClip normalAttack;
    public AudioClip feverAttack;
    public AudioClip click;
    public AudioClip feverStart;
    public AudioClip heal;
    public AudioClip fadeout;
    public AudioClip credit;
    public AudioClip tileMove;
    // 추가될 예정
    public AudioClip stuck;
    public AudioClip parrying;
    public AudioClip createSword;
    public AudioClip shotSword;


    private void Start()
    {

    }

    private void Update()
    {
        SetEffectVolume();
        SetBGMVolume();
        SetVolume();
    }


    // Effect 사운드 재생
    public void PlayEffectSound(AudioClip clip)
    {
        Effect_audioSource.PlayOneShot(clip);
    }
    public void PlayTileSound(AudioClip clip)
    {
        Tile_audioSource.PlayOneShot(clip);
    }

    public void SetEffectVolume()
    {
        Effect_audioSource.volume = effectVolume;
    }

    public void MuteEffectVolume()
    {
        effectMute = effectMute ? false : true;

        Effect_audioSource.mute = effectMute ? true : false;
    }

    // BGM 사운드
    public void PlayBGMSound(AudioClip clip)
    {
        BGM_audioSource.clip = clip;
        BGM_audioSource.Play();
    }

    public void SetBGMVolume()
    {
        BGM_audioSource.volume = BGMVolume;
    }

    public void MuteBGMVolume()
    {
        BGMMute = BGMMute ? false : true;

        BGM_audioSource.mute = BGMMute ? true : false;
    }

    // 전체 사운드
    public void SetVolume()
    {
        Effect_audioSource.volume = volume * effectVolume;
        BGM_audioSource.volume = volume * BGMVolume;
    }

    public void MuteVolume()
    {
        AllMute = AllMute ? false : true;
        if (AllMute)
        {
            BGM_audioSource.mute = true;
            Effect_audioSource.mute = true;
        }
        else
        {
            BGM_audioSource.mute = BGMMute;
            Effect_audioSource.mute = effectMute;
        }
    }
}
