using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeaponChange_FlameGun : MonoBehaviour
{
    public WeaponChange.WeaponType WeaponType = WeaponChange.WeaponType.longType;
    public GameObject self;
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
            other.GetComponent<WeaponChange>().ChangeWeapon(WeaponType, self);
        }
    }
}
