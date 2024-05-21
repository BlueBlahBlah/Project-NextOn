using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager
{
    private int volume_BGM = 100; // 배경음악 크기 (0~100)
    private int volume_SE = 100; // 효과음 크기 (0~100)

    private bool mute_BGM; // 배경음악 음소거
    private bool mute_SE; // 효과음 음소거

    AudioSource[] audioSources = new AudioSource[10]; // 생성가능한 사운드 수 => 배열크기
    Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();


}
