using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBtn : MonoBehaviour
{
    public bool BtnDown;

    public void PointerDown()
    {
        BtnDown = true;
    }
    
    public void PointerUp()
    {
        BtnDown = false;
    }
}
