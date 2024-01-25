using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBtn : MonoBehaviour
{
    public bool BtnDown;
   
    void Update()
    {
        
        GameObject.Find("SM_Wep_MachineGun_01").GetComponent<MachineGun>().fireBtnDowing = BtnDown;
        
    }
        
        
    public void PointerDown()
    {
        BtnDown = true;
    }
    
    public void PointerUp()
    {
        BtnDown = false;
    }
    
   
}
