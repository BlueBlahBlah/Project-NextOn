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
            //洹쇱젒臾닿린??寃쎌슦 臾닿린?먯꽌 踰꾪듉 ?대깽?몃? ?깅줉?섎뒗 寃껋씠 ?꾨땲湲곗뿉 洹쇱젒怨듦꺽 紐⑥뀡???ш린???깅줉
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

        if (Wt == WeaponType.closeType)
        {
            player_CloseWeapon.GetComponent<PlayerScriptOneHand>().WeaponSynchronization();  //?꾩옱 ?≪? 臾닿린 ?ㅼ떆 ?먯깋
        }
        else if (Wt == WeaponType.longType)
        {
            player_LongWeapon.GetComponent<PlayerScriptRifle>().WeaponSynchronization();    //?꾩옱 ?≪? 臾닿린 ?ㅼ떆 ?먯깋
        }
    }
}
