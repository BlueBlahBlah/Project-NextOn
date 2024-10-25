using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectObject : MonoBehaviour
{
    [SerializeField]
    private string stageName;

    [SerializeField]
    private SelectUI selectCanvas;


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (SceneContainer.instance != null)
            {
                SceneContainer.instance.nextScene = stageName;
                selectCanvas.OpenSelectUI(stageName);
            }            
        }
        
    }
}
