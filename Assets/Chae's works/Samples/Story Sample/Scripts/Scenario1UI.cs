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
            // ���ο� GameObject�� ����� VolumeController�� �߰�
            GameObject volumeControllerObject = new GameObject("VolumeController");
            volumeController = volumeControllerObject.AddComponent<VolumeController>();
        }
    }

    public void SetLittleDark()
    {
        // ������ ������ ���� (��ο�)
        Darkness.color = new Color(Darkness.color.r, Darkness.color.g, Darkness.color.b, 0.5f);
    }

    public void SetTotallyDark()
    {
        // ȭ���� ���� ����
        Darkness.color = new Color(Darkness.color.r, Darkness.color.g, Darkness.color.b, 0.8f);
    }

    public void SetLight()
    {
        // ������ ������ ���� (����)
        Darkness.color = new Color(Darkness.color.r, Darkness.color.g, Darkness.color.b, 0f);
    }

    public void SparkEvent()
    {
        // ���� ���� ���Ⱑ �帣�� ����
    }

    public void StopCharacter()
    {
        // ĳ������ ����
        characterPixel.GetComponent<Animator>().enabled = false;
        characterPixel.sprite = Resources.Load($"UI/Image/Characters/Devin/coding 1", typeof(Sprite)) as Sprite;
    }

    public void Fadein()
    {
        volumeController.TriggerFadeIn();
    }
}
