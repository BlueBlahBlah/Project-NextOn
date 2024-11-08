using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class SceneChangeObjectEnding : MonoBehaviour
{
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

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (SceneContainer.instance != null)
            {
                SceneContainer.instance.nextScene = "Ending Scene";
                TriggerFadeIn();
                Invoke("DoChangeScene", 2f);
            }
        }
    }

    private void DoChangeScene()
    {
        LoadingManager.ToLoadScene();
    }

    private void TriggerFadeIn()
    {
        volumeController.TriggerFadeIn();
    }
}
