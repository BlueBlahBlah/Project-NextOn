using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenQuitUI : MonoBehaviour
{
    [SerializeField]
    private GameObject quitPanel;

    // Update is called once per frame
    void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                quitPanel.SetActive(true);
            }
        }
    }
}
