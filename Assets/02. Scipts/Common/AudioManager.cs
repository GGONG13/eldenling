using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] BgmClips;
    public float BgmVolume = 1;
    AudioSource BgmPlayer;

    public AudioClip[] SfxClips;
    public float SfxVolume;
    public int Channels;
    AudioSource[] SfxPlayer;
    int channelIndex;

    public enum Bgm {LobbyScene, EndingScene, }
    public enum Sfx {Walk, Run, Sword, Shield}

    public static AudioManager instance;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
        Init();
    }
    private void Init()
    {
        GameObject bgmObject = new GameObject("BgmPlayer");
        bgmObject.transform.parent = transform;
        BgmPlayer = bgmObject.AddComponent<AudioSource>();
        BgmPlayer.playOnAwake = false;
        BgmPlayer.loop = true;
        BgmPlayer.volume = BgmVolume;

        GameObject SfxObject = new GameObject("SfxPlayer");
        SfxObject.transform.parent = transform;
        SfxPlayer = new AudioSource[Channels];

        for (int i = 0; i < SfxPlayer.Length; i++)
        {
            SfxPlayer[i] = SfxObject.AddComponent<AudioSource>();
            SfxPlayer[i].playOnAwake = false;
            SfxPlayer[i].volume = SfxVolume;
        }
    }
    public void PlayBgm(Bgm bgm)
    {
        BgmPlayer.clip = BgmClips[(int)bgm];
        BgmPlayer.Play();
    }

    public void PlaySfx(Sfx sfx)
    {
        if (sfx == Sfx.Walk || sfx == Sfx.Run) // 발자국 소리일 때만 루프 재생
        {
            int loopIndex = (channelIndex + 1) % Channels;

            if (!SfxPlayer[loopIndex].isPlaying)
            {
                SfxPlayer[loopIndex].clip = SfxClips[(int)sfx];
                SfxPlayer[loopIndex].loop = true;
                SfxPlayer[loopIndex].Play();
                channelIndex = loopIndex;
            }
        }
        for (int i = 0; i < SfxPlayer.Length; i++)
        {
            int loopIndex = (i + channelIndex) % SfxPlayer.Length;

            if (SfxPlayer[loopIndex].isPlaying)
            {
                continue;
            }
            channelIndex = loopIndex;
            SfxPlayer[loopIndex].clip = SfxClips[(int)sfx];
            SfxPlayer[loopIndex].Play();
            break;
        }
    }
    public void StopBgm()
    {
        if (BgmPlayer.isPlaying)
        {
            BgmPlayer.Stop();
        }
    }
    public void StopSfx(Sfx sfx)
    {
        for (int i = 0; i < SfxPlayer.Length; i++)
        {
            if (SfxPlayer[i].clip == SfxClips[(int)sfx])
            {
                SfxPlayer[i].Stop();
            }
        }
    }
}
