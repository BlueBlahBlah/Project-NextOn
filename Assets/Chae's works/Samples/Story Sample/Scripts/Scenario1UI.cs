using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Scenario1UI : MonoBehaviour
{
    [SerializeField]
    private Image Darkness;
    [SerializeField]
    private Image Spark;
    [SerializeField]
    private Image characterPixel;

    private VolumeController volumeController;

    private void Start()
    {
        if (volumeController == null)
        {
            // 새로운 GameObject를 만들고 VolumeController를 추가
            GameObject volumeControllerObject = new GameObject("VolumeController");
            volumeController = volumeControllerObject.AddComponent<VolumeController>();
        }
    }

    public void SetLittleDark()
    {
        // 조명의 깜빡임 연출 (어두움)
        Darkness.color = new Color(Darkness.color.r, Darkness.color.g, Darkness.color.b, 0.5f);
    }

    public void SetTotallyDark()
    {
        // 화면의 암전 연출
        Darkness.color = new Color(Darkness.color.r, Darkness.color.g, Darkness.color.b, 0.8f);
    }

    public void SetLight()
    {
        // 조명의 깜빡임 연출 (밝음)
        Darkness.color = new Color(Darkness.color.r, Darkness.color.g, Darkness.color.b, 0f);
    }

    public void SparkEvent()
    {
        // 순간 강한 전기가 흐르는 연출
    }

    public void StopCharacter()
    {
        // 캐릭터의 정지
        characterPixel.GetComponent<Animator>().enabled = false;
        characterPixel.sprite = Resources.Load($"UI/Image/Characters/Devin/coding 1", typeof(Sprite)) as Sprite;
    }

    public void Fadein()
    {
        volumeController.TriggerFadeIn();
    }
}
