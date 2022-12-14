using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoSingleton<SoundManager>
{
    public AudioSource titleBGMSource;
    public AudioClip titleBGMClip;

    public AudioSource bgmSource;
    public AudioClip bgmClip;

    public AudioSource audioSources;
    public AudioClip[] audioClips;

    private void Awake()
    {
        titleBGMSource.playOnAwake = true;
        titleBGMSource.loop = true;
    }
    void TurnAudio(AudioSource source, bool turn)
    {
        if (turn)
        {
            source.Play();
        }
        else
        {
            source.Stop();
        }
    }
    public void TurnScene(GameManager.STATE curState)
    {
        if (curState == GameManager.STATE.TITLE)
        {
            TurnAudio(titleBGMSource, false);
            TurnAudio(bgmSource, true);
        }
        else
        {
            if (!bgmSource.isPlaying)
                TurnAudio(bgmSource, true);
        }
    }
}
