using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoSingleton<SoundManager>
{
    public AudioSource BGMSource;
    public AudioSource soundSource;
    [Header("0 TITLE BGM / 1 GAME BGM")]
    public AudioClip[] BGMAudioClips;
    [Header("0 FISHING SOUND / 1 THROWROD / 2 INCLUDING WATER / 3 DECLUDING WATER")]
    public AudioClip[] EffectAudioClips;

    public enum BGM
    {
        TITLE,
        GAME
    }
    public enum EFFECT
    {
        FISHING,
        THROWROD,
        INCLUDEINGWATER,
        DECLUDINGWATER,
    }

    private void Awake()
    {
        BGMSource.playOnAwake = true;
        BGMSource.loop = true;
    }
    public void TurnAudio(BGM bgm)
    {
        BGMSource.clip = BGMAudioClips[(int)bgm];
        BGMSource.Play();
    }
    public void TurnAudio(EFFECT effect)
    {
        soundSource.clip = EffectAudioClips[(int)effect];
        soundSource.Play();
    }
    
    public void TurnScene(GameManager.STATE state)
    {
        BGM bgm = ChangeEnum(state);
        if (BGMSource.clip == BGMAudioClips[(int)bgm]) return;
        else
        {
            TurnAudio(bgm);
        }
    }
    private BGM ChangeEnum(GameManager.STATE state) => state switch
    {
        GameManager.STATE.GAME => BGM.GAME,
        _ => BGM.TITLE
    };
}
