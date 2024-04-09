using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.UI;


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
    [SerializeField] private Button attackBtn;

    public void ChangeWeapon(WeaponType Wt, GameObject Weapon)
    {
        if (Wt == WeaponType.closeType)
        {
            player_LongWeapon.SetActive(false);
            player_NonWeapon.SetActive(false);
            player_CloseWeapon.SetActive(true);
            //근접무기의 경우 무기에서 버튼 이벤트를 등록하는 것이 아니기에 근접공격 모션을 여기서 등록
            attackBtn.onClick.AddListener(player_CloseWeapon.GetComponent<PlayerScriptOneHand>().OnAttackButtonClick);
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
