using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponChange : MonoBehaviour
{
    public enum WeaponType
    {
        nonType,
        closeType,
        longType
    }
    [SerializeField] private GameObject player_LongWeapon;
    [SerializeField] private GameObject player_NonWeapon;
    [SerializeField] private GameObject player_CloseWeapon;
    [SerializeField] private List<GameObject> player_WeaponList;

    public void ChangeWeapon(WeaponType Wt, GameObject Weapon)
    {
        if (Wt == WeaponType.closeType)
        {
            player_LongWeapon.SetActive(false);
            player_NonWeapon.SetActive(false);
            player_CloseWeapon.SetActive(true);
        }
        else if (Wt == WeaponType.longType)
        {
            player_LongWeapon.SetActive(true);
            player_NonWeapon.SetActive(false);
            player_CloseWeapon.SetActive(false);
        }
        else if (Wt == WeaponType.nonType)
        {
            player_LongWeapon.SetActive(false);
            player_NonWeapon.SetActive(true);
            player_CloseWeapon.SetActive(false);
        }
        foreach (GameObject g in player_WeaponList)
        {
            if (Weapon == g)
            {
                g.SetActive(true);
            }
            else
            {
                g.SetActive(false);
            }
        }
    }
}
