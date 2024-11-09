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
    private GameObject Spark;
    [SerializeField]
    private Image characterPixel;

    private VolumeController volumeController;

    private void Start()
    {
        if (volumeController == null)
        {
            // ���ο� GameObject�� ����� VolumeController�� �߰�
            if (GameObject.Find("VolumeController") != null)
            {
                volumeController = GameObject.Find("VolumeController").GetComponent<VolumeController>();
            }
            else
            {
                GameObject volumeControllerObject = new GameObject("VolumeController");
                volumeController = volumeControllerObject.AddComponent<VolumeController>();
            }
            Invoke("FadeOut", 3f);
        }
    }

    public void SetLittleDark()
    {
        // ������ ������ ���� (��ο�)
        Darkness.color = new Color(Darkness.color.r, Darkness.color.g, Darkness.color.b, 0.5f);
    }

    public void SetAlmostDark()
    {
        // ȭ���� ���� ����
        Darkness.color = new Color(Darkness.color.r, Darkness.color.g, Darkness.color.b, 0.8f);
    }

    public void SetLight()
    {
        // ������ ������ ���� (����)
        Darkness.color = new Color(Darkness.color.r, Darkness.color.g, Darkness.color.b, 0f);
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

    public void FadeOut()
    {
        volumeController.TriggerFadeOut();
    }

    public void ElectricShock()
    {
        StartCoroutine(SparkEvent());

        Invoke("Fadein", 2f);
        Invoke("ChangeSceneToSelectionScene", 3f);
    }
    IEnumerator SparkEvent()
    {
        SoundManager.instance.PlayEffectSound("ElectricShock2", 1f);
        Spark.SetActive(true);
        Darkness.color = new Color(Darkness.color.r, Darkness.color.g, Darkness.color.b, 1f);
        yield return new WaitForSeconds(0.2f);
        Spark.SetActive(false);

        // ���� ���� ���Ⱑ �帣�� ����
        yield break;
    }

    private void ChangeSceneToSelectionScene()
    {
        LoadingManager.ToLoadScene();
    }
}
