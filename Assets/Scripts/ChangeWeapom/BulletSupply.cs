using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSupply : MonoBehaviour
{

    [SerializeField] private Rifle rifle;
    [SerializeField] private Shotgun shotgun;
    [SerializeField] private Sniper sniper;
    [SerializeField] private GrenadeLauncher grenadeLauncher;
    [SerializeField] private MachineGun machineGun;

    [SerializeField] private FireGun fireGun;
    // Start is called before the first frame update
    void Start()
    {
        /*rifle = GameObject.FindObjectOfType<Rifle>();
        shotgun = GameObject.FindObjectOfType<Shotgun>();
        sniper = GameObject.FindObjectOfType<Sniper>();
        grenadeLauncher = GameObject.FindObjectOfType<GrenadeLauncher>();
        machineGun = GameObject.FindObjectOfType<MachineGun>();
        fireGun = GameObject.FindObjectOfType<FireGun>();*/
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
           
            if (rifle != null && rifle.gameObject.activeSelf)
            {
                Debug.Log("라이플 탄약보충");
                rifle.maxBulletCount += 60; // 60발 추가
            }

            if (shotgun != null && shotgun.gameObject.activeSelf)
            {
                Debug.Log("샷건 탄약보충");
                shotgun.maxBulletCount += 60; // 60발 추가
            }

            if (sniper != null && sniper.gameObject.activeSelf)
            {
                Debug.Log("저격총 탄약보충");
                sniper.maxBulletCount += 10; // 10발 추가
            }

            if (grenadeLauncher != null && grenadeLauncher.gameObject.activeSelf)
            {
                Debug.Log("유탄발사기 탄약보충");
                grenadeLauncher.maxBulletCount += 60; // 60발 추가
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
