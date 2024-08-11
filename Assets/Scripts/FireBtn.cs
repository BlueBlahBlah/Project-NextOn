using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBtn : MonoBehaviour
{
    public bool BtnDown;
    public GameObject machineGun;
    public GameObject fireGun;

    private void Start()
    {
        //machineGun = GameObject.FindObjectOfType<MachineGun>();
        //fireGun = GameObject.FindObjectOfType<FireGun>();
    }

    void Update()
    {
        if (machineGun != null && machineGun.gameObject.activeSelf)
        {
            GameObject.Find("SM_Wep_MachineGun_01").GetComponent<MachineGun>().fireBtnDowing = BtnDown;
        }
        if (fireGun != null && fireGun.gameObject.activeSelf)
        {
            GameObject.Find("FlameGun").GetComponent<FireGun>().fireBtnDowing = BtnDown;
        }
        
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
