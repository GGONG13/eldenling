using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] BgmClips;
    public float BgmVolume;
    AudioSource BgmPlayer;

    public AudioClip[] SfxClips;
    public float SfxVolume;
    public int Channels;
    AudioSource[] SfxPlayer;
    int channelIndex;

    public enum Bgm { }
    public enum Sfx { }

    public static AudioManager instance;
}
