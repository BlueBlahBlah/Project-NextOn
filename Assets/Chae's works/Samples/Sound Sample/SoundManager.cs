using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager
{
    private int volume_BGM = 100; // ������� ũ�� (0~100)
    private int volume_SE = 100; // ȿ���� ũ�� (0~100)

    private bool mute_BGM; // ������� ���Ұ�
    private bool mute_SE; // ȿ���� ���Ұ�

    AudioSource[] audioSources = new AudioSource[10]; // ���������� ���� �� => �迭ũ��
    Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();


}
