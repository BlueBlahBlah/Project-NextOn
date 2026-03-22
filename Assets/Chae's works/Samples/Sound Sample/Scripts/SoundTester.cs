using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTester : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SoundManager.instance.PlayMusic("Redemption");

        
    }
    public void TestClick()
    {
        SoundManager.instance.PlayEffectSound("Click");
    }
    
}
