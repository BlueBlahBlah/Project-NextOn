using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DropItemPosition : MonoBehaviour
{
    [SerializeField] private List<GameObject> Positions;


    public enum ItemList
    {
        BulletSupply,
        ChangeWeaponDemacia,
        ChangeWeaponFantasyAxe,
        ChangeWeaponFlameGun,
        ChangeWeaponGranade,
        ChangeWeaponMachineGun,
        ChangeWeaponRifle,
        ChangeWeaponShotgun,
        ChangeWeaponSilver,
        ChangeWeaponSniper,
        ChangeWeaponStatic,
        ChangeWeaponStreamOfEdge
    }
    
    //떨어지는 무기교체 아이템들
    [SerializeField] private GameObject BulletSupply;
    [SerializeField] private GameObject ChangeWeaponDemacia;
    [SerializeField] private GameObject ChangeWeaponFantasyAxe;
    [SerializeField] private GameObject ChangeWeaponFlameGun;
    [SerializeField] private GameObject ChangeWeaponGranade;
    [SerializeField] private GameObject ChangeWeaponMachineGun;
    [SerializeField] private GameObject ChangeWeaponRifle;
    [SerializeField] private GameObject ChangeWeaponShotgun;
    [SerializeField] private GameObject ChangeWeaponSilver;
    [SerializeField] private GameObject ChangeWeaponSniper;
    [SerializeField] private GameObject ChangeWeaponStatic;
    [SerializeField] private GameObject ChangeWeaponStreamOfEdge;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    //8개의 자리중 하나를 랜덤으로 정하는 함수
    private GameObject ReturnRandomPosition()
    {
        int num = 0;
        num = Random.Range(0, 8);
        return Positions[num];
    }

    //떨어지는 아이템에 떨어지는 코드 WeaponChangeGravity 를 추가하는 함수
    private T InitComponent<T>(GameObject gameObject) where T : MonoBehaviour
    {
        return gameObject.AddComponent<T>();
    }
    
     //아이템을 드랍하는 함수 - 인자는 드랍하고자 하는 아이템
    public void DropItem(ItemList s)
    {
        GameObject Item = null;
        GameObject DropPosition = ReturnRandomPosition();

        switch (s)
        {
            case ItemList.BulletSupply:
                Item = Instantiate(BulletSupply, DropPosition.transform.position, DropPosition.transform.rotation);
                break;
            case ItemList.ChangeWeaponDemacia:
                Item = Instantiate(ChangeWeaponDemacia, DropPosition.transform.position, DropPosition.transform.rotation);
                break;
            case ItemList.ChangeWeaponFantasyAxe:
                Item = Instantiate(ChangeWeaponFantasyAxe, DropPosition.transform.position, DropPosition.transform.rotation);
                break;
            case ItemList.ChangeWeaponFlameGun:
                Item = Instantiate(ChangeWeaponFlameGun, DropPosition.transform.position, DropPosition.transform.rotation);
                break;
            case ItemList.ChangeWeaponGranade:
                Item = Instantiate(ChangeWeaponGranade, DropPosition.transform.position, DropPosition.transform.rotation);
                break;
            case ItemList.ChangeWeaponMachineGun:
                Item = Instantiate(ChangeWeaponMachineGun, DropPosition.transform.position, DropPosition.transform.rotation);
                break;
            case ItemList.ChangeWeaponRifle:
                Item = Instantiate(ChangeWeaponRifle, DropPosition.transform.position, DropPosition.transform.rotation);
                break;
            case ItemList.ChangeWeaponShotgun:
                Item = Instantiate(ChangeWeaponShotgun, DropPosition.transform.position, DropPosition.transform.rotation);
                break;
            case ItemList.ChangeWeaponSilver:
                Item = Instantiate(ChangeWeaponSilver, DropPosition.transform.position, DropPosition.transform.rotation);
                break;
            case ItemList.ChangeWeaponSniper:
                Item = Instantiate(ChangeWeaponSniper, DropPosition.transform.position, DropPosition.transform.rotation);
                break;
            case ItemList.ChangeWeaponStatic:
                Item = Instantiate(ChangeWeaponStatic, DropPosition.transform.position, DropPosition.transform.rotation);
                break;
            case ItemList.ChangeWeaponStreamOfEdge:
                Item = Instantiate(ChangeWeaponStreamOfEdge, DropPosition.transform.position, DropPosition.transform.rotation);
                break;
            default:
                Debug.LogError("ItemDrop 인자 오류");
                break;
        }
        InitComponent<WeaponChangeGravity>(Item);
    }

}
