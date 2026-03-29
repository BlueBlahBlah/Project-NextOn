using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DropItemPosition : MonoBehaviour
{
    [SerializeField] private List<GameObject> Positions;
    private int PreviousItemPosition;       //?êÎ≤à ?∞ÏÜç Í∞ôÏ? ?êÎ¶¨?êÏÑú ?ÑÏù¥?úÏù¥ ?®Ïñ¥ÏßÄÏßÄ ?äÎèÑÎ°??òÎäî Î≥Ä??
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
        ChangeWeaponStreamOfEdge,
        SkillBomb,
        SkillHeilcopter,
        SkillTurret,
        SkillRandom,
    }
    
    //?®Ïñ¥ÏßÄ??Î¨¥Í∏∞ÍµêÏ≤¥ ?ÑÏù¥?úÎì§
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
    
    //?§ÌÇ¨?ÑÏù¥?úÎì§
    [SerializeField] private GameObject SkillBomb;
    [SerializeField] private GameObject SkillHeilcopter;
    [SerializeField] private GameObject SkillTurret;
    
    
    // Start is called before the first frame update
    void Start()
    {
        PreviousItemPosition = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
     
    //8Í∞úÏùò ?êÎ¶¨Ï§??òÎÇòÎ•??úÎç§?ºÎ°ú ?ïÌïò???®Ïàò
    //?¥Ï†Ñ ?∏Ï∂ú???ïÌï¥Ïß??êÎ¶¨Í∞Ä Î∞îÎ°ú ?§Ïùå???òÏò§ÏßÄ ?äÏùå
    private GameObject ReturnRandomPosition()
    {
        int num = 0;
        do
        {
            num = Random.Range(0, 8);
        } while (PreviousItemPosition == num);
        PreviousItemPosition = num;
        return Positions[num];
    }

    //?®Ïñ¥ÏßÄ???ÑÏù¥?úÏóê ?®Ïñ¥ÏßÄ??ÏΩîÎìú WeaponChangeGravity Î•?Ï∂îÍ??òÎäî ?®Ïàò
    private T InitComponent<T>(GameObject gameObject) where T : MonoBehaviour
    {
        return gameObject.AddComponent<T>();
    }
    
     //?ÑÏù¥?úÏùÑ ?úÎûç?òÎäî ?®Ïàò - ?∏Ïûê???úÎûç?òÍ≥†???òÎäî ?ÑÏù¥??    public GameObject DropItem(ItemList s)
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
            case ItemList.SkillBomb:
                Item = Instantiate(SkillBomb, DropPosition.transform.position, DropPosition.transform.rotation);
                break;
            case ItemList.SkillHeilcopter:
                Item = Instantiate(SkillHeilcopter, DropPosition.transform.position, DropPosition.transform.rotation);
                break;
            case ItemList.SkillRandom:
                int i = Random.Range(0, 3);
                if(i == 0)
                    Item = Instantiate(SkillBomb, DropPosition.transform.position, DropPosition.transform.rotation);
                else if(i == 1)
                    Item = Instantiate(SkillHeilcopter, DropPosition.transform.position, DropPosition.transform.rotation);
                else if(i == 2)
                    Item = Instantiate(SkillTurret, DropPosition.transform.position, DropPosition.transform.rotation);
                else
                    Item = Instantiate(SkillTurret, DropPosition.transform.position, DropPosition.transform.rotation);
                break;
            default:
                Debug.LogError("ItemDrop ?∏Ïûê ?§Î•ò");
                break;
        }
        InitComponent<WeaponChangeGravity>(Item);
        Item.GetComponent<WeaponChangeGravity>().TypeSelf = s;
        return Item;
    }

}
