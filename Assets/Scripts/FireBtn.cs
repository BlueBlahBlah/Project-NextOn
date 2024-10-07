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
            machineGun.GetComponent<MachineGun>().fireBtnDowing = BtnDown;
        }
        if (fireGun != null && fireGun.gameObject.activeSelf)
        {
            fireGun.GetComponent<FireGun>().fireBtnDowing = BtnDown;
        }
    }
    
        
        
    public void PointerDown()
    {
        BtnDown = true;
        if (machineGun != null && machineGun.gameObject.activeSelf)
        {
            PlayerSoundManager.Instance.MachineGun_Shoot_Sound();
        }
        else if (fireGun != null && fireGun.gameObject.activeSelf)
        {
            PlayerSoundManager.Instance.Flame_Shoot_Sound();
        }
    }
    
    public void PointerUp()
    {
        BtnDown = false;
        if (machineGun != null && machineGun.gameObject.activeSelf)
        {
            PlayerSoundManager.Instance.StopSound(PlayerSoundManager.Instance.MachineGun_Shoot);
        }
        else if (fireGun != null && fireGun.gameObject.activeSelf)
        {
            PlayerSoundManager.Instance.StopSound(PlayerSoundManager.Instance.Flame_Shoot);
        }
    }
    
   
}
