using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoSingleton<SoundManager>
{
    public AudioSource BGMSource;
    public AudioSource soundSource;
    [Header("0 TITLE BGM / 1 GAME BGM")]
    public AudioClip[] BGMAudioClips;
    [Header("0 FISHING SOUND / 1 THROWROD / 2 INCLUDING WATER / 3 DECLUDING WATER")]
    public AudioClip[] EffectAudioClips;

    public Slider BGMSlider;
    public Slider effectSlider;

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

        BGMSlider.maxValue = BGMSource.maxDistance = 100f;
        BGMSlider.value = PlayerPrefs.GetFloat("BGMSOUND", 100f);
        BGMSource.volume = Mathf.Lerp(0f, 0.5f, BGMSlider.value / BGMSlider.maxValue);
        BGMSlider.minValue = BGMSource.minDistance = 0f;
        effectSlider.maxValue = soundSource.maxDistance = 100f;
        effectSlider.value = PlayerPrefs.GetFloat("FXSOUND", 100f);
        soundSource.volume = effectSlider.value / effectSlider.maxValue;
        effectSlider.minValue = soundSource.minDistance = 0f;

        TurnAudio(BGM.TITLE);
    }
    public void SetBGMVolume()
    {
        BGMSource.volume = Mathf.Lerp(0f, 0.5f, BGMSlider.value / BGMSlider.maxValue);

        PlayerPrefs.SetFloat("BGMSOUND", BGMSlider.value);
    }
    public void SetFXVolume()
    {
        soundSource.volume = effectSlider.value / effectSlider.maxValue;

        PlayerPrefs.SetFloat("FXSOUND", effectSlider.value);
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
