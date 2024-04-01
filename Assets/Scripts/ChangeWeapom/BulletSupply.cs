using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSupply : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void OnTriggerEnter(Collider other)
    {
        // 충돌한 물체가 Player 태그를 가진 경우
        if (other.CompareTag("Player"))
        {
            Rifle rifle = GameObject.FindObjectOfType<Rifle>();
            Shotgun shotgun = GameObject.FindObjectOfType<Shotgun>();
            Sniper sniper = GameObject.FindObjectOfType<Sniper>();
            GrenadeLauncher grenadeLauncher = GameObject.FindObjectOfType<GrenadeLauncher>();
            MachineGun machineGun = GameObject.FindObjectOfType<MachineGun>();
            FireGun fireGun = GameObject.FindObjectOfType<FireGun>();
            if (rifle != null && rifle.gameObject.activeSelf)
            {
                Debug.Log("라이플 탄약보충");
                rifle.maxBulletCount += 30; // 30발 추가
            }

            if (shotgun != null && shotgun.gameObject.activeSelf)
            {
                Debug.Log("샷건 탄약보충");
                shotgun.maxBulletCount += 30; // 30발 추가
            }

            if (sniper != null && sniper.gameObject.activeSelf)
            {
                Debug.Log("저격총 탄약보충");
                sniper.maxBulletCount += 1; // 30발 추가
            }

            if (grenadeLauncher != null && grenadeLauncher.gameObject.activeSelf)
            {
                Debug.Log("유탄발사기 탄약보충");
                grenadeLauncher.maxBulletCount += 30; // 30발 추가
            }

            if (machineGun != null && machineGun.gameObject.activeSelf)
            {
                Debug.Log("기관총 탄약보충");
                machineGun.maxBulletCount += 100; // 100발 추가
            }

            if (fireGun != null && fireGun.gameObject.activeSelf)
            {
                Debug.Log("화염방사기 탄약보충");
                fireGun.maxBulletCount += 100; // 100발 추가
            }
        }
    }
}
