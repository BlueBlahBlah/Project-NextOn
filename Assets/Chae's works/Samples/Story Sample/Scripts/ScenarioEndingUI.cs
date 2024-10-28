using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScenarioEndingUI : MonoBehaviour
{
    [SerializeField]
    Image BlackFadeInOut;

    private VolumeController volumeController;

    private void Start()
    {
        if (volumeController == null)
        {
            // 새로운 GameObject를 만들고 VolumeController를 추가
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

    public void ChangeImage()
    {

    }

    public void Fadein()
    {
        volumeController.TriggerFadeIn();
    }

    public void FadeOut()
    {
        volumeController.TriggerFadeOut();
    }
}
