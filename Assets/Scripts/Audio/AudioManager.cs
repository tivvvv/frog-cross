using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Audio Clips")]
    public AudioClip bgmClip;
    public AudioClip jumpClip;
    public AudioClip longJumpClip;
    public AudioClip deadClip;

    [Header("Audio Source")]
    public AudioSource bgm;
    public AudioSource fx;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this);

        bgm.clip = bgmClip;
        PlayMusic();
    }

    /// <summary>
    /// 设置跳跃的音效片段
    /// </summary>
    /// <param name="type">0:小跳 1:大跳</param>
    public void SetJumpClip(int type)
    {
        fx.clip = type == 0 ? jumpClip : longJumpClip;
    }

    public void PlayJumpFx()
    {
        fx.Play();
    }

    public void PlayMusic()
    {
        if (!bgm.isPlaying)
        {
            bgm.Play();
        }
    }
}
