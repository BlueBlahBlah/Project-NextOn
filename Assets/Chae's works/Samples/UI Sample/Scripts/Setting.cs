using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
    [SerializeField]
    private Scrollbar BgmScrollBar;
    [SerializeField]
    private Scrollbar SEScrollBar;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetBgmVolume()
    {
        if (0 <= BgmScrollBar.value && BgmScrollBar.value <= 0.125f)
        {
            SoundManager.instance.SetMusicVolume(0.06f);
        }
        else if (BgmScrollBar.value <= 0.375f)
        {
            SoundManager.instance.SetMusicVolume(0.12f);
        }
        else if (BgmScrollBar.value <= 0.625f)
        {
            SoundManager.instance.SetMusicVolume(0.18f);
        }
        else if (BgmScrollBar.value <= 0.875f)
        {
            SoundManager.instance.SetMusicVolume(0.24f);
        }
        else
        {
            SoundManager.instance.SetMusicVolume(0.3f);
        }
    }

    public void SetBgmMute()
    {
        if (!SoundManager.instance.isBgmMute)
        {
            BgmScrollBar.interactable = false;
            SoundManager.instance.SetMusicVolume(0f);
            SoundManager.instance.isBgmMute = true;
        }
        else
        {
            BgmScrollBar.interactable = true;
            SetBgmVolume();
            SoundManager.instance.isBgmMute = false;
        }
    }

    public void SetSEVolume()
    {

    }

    public void SetSEMute()
    {
        
    }
}
