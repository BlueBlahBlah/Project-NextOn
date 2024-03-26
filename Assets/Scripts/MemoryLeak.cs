using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class MemoryLeak : MonoBehaviour
{
    public Slider MemorySlider;
    void Start()
    {
        MemorySlider.value = 0;
    }
}
