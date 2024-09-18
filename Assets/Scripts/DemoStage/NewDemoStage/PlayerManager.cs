using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance = null;
    /*[SerializeField] private Rifle rifle = GameObject.FindObjectOfType<Rifle>();
    [SerializeField] private Shotgun shotgun = GameObject.FindObjectOfType<Shotgun>();
    [SerializeField] private Sniper sniper = GameObject.FindObjectOfType<Sniper>();
    [SerializeField] private GrenadeLauncher grenadeLauncher = GameObject.FindObjectOfType<GrenadeLauncher>();
    [SerializeField] private MachineGun machineGun = GameObject.FindObjectOfType<MachineGun>();
    [SerializeField] private FireGun fireGun = GameObject.FindObjectOfType<FireGun>();*/
    
    
    public float TotalHealth;                          //최대체력
    public float Health;                          //현재체력
    public float HealthGen;                     //체젠
    public int DefensivePower;                  //방어력
    public int MovingSpeed;                     //이동속도
    public int PlayerPlainHitDamage;            //평타 공격력
    public int PlayerSkillDamage;               //스킬 공격력
    public int CurrentBullet;                   //현재 잔탄 수
    public int TotalBullet;                     //남은 탄창 수
    
    public float SkillCoolTimeRate;                     //근접무기 쿨타임감소율
    
    public bool Death;
    
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
    
    public DropItemPosition _dropItemPosition;

    
    
    
    private void Awake()
    {
        TotalHealth = 100;  //시작 시 체력 100
        Health = TotalHealth;
        SkillCoolTimeRate = 0;     //시작 시 스킬 쿨타임
        Death = false;
        if (null == instance)
        {
            //이 클래스 인스턴스가 탄생했을 때 전역변수 instance에 게임매니저 인스턴스가 담겨있지 않다면, 자신을 넣어준다.
            instance = this;

            //씬 전환이 되더라도 파괴되지 않게 한다.
            //gameObject만으로도 이 스크립트가 컴포넌트로서 붙어있는 Hierarchy상의 게임오브젝트라는 뜻이지만, 
            //나는 헷갈림 방지를 위해 this를 붙여주기도 한다.
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            //만약 씬 이동이 되었는데 그 씬에도 Hierarchy에 GameMgr이 존재할 수도 있다.
            //그럴 경우엔 이전 씬에서 사용하던 인스턴스를 계속 사용해주는 경우가 많은 것 같다.
            //그래서 이미 전역변수인 instance에 인스턴스가 존재한다면 자신(새로운 씬의 GameMgr)을 삭제해준다.
            Destroy(this.gameObject);
        }
    }
    //게임 매니저 인스턴스에 접근할 수 있는 프로퍼티. static이므로 다른 클래스에서 맘껏 호출할 수 있다.
    public static PlayerManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        player_LongWeapon = GameObject.Find("Check_Sprite_Long");
        player_NonWeapon = GameObject.Find("Check_Sprite");
        player_CloseWeapon = GameObject.Find("Check_Sprite_Short");

        // 각 무기 리스트에 추가할 오브젝트 이름
        string[] longWeaponNames = {
            "FlameGun",
            "SM_Wep_MachineGun_01",
            "SM_Wep_Grenade_Launcher_01",
            "SM_Wep_Sniper_Mil_01",
            "SM_Wep_Rifle_Assault_01",
            "SM_Wep_Shotgun_01"
        };

        string[] closeWeaponNames = {
            "SwordStatic",
            "SwordStreamOfEgde",
            "SwordSilver",
            "SwordDemacia",
            "FantasyAxe_Unity"
        };

        // parent 오브젝트 찾기
        player_LongWeapon = GameObject.Find("Check_Sprite_Long");
        player_NonWeapon = GameObject.Find("Check_Sprite");
        player_CloseWeapon = GameObject.Find("Check_Sprite_Short");

        // 모든 자식 오브젝트들 가져오기
        Transform[] longWeaponChildren = player_LongWeapon.GetComponentsInChildren<Transform>(true);
        Transform[] closeWeaponChildren = player_CloseWeapon.GetComponentsInChildren<Transform>(true);

        // longWeapon 자식 중 무기 이름과 일치하는 오브젝트 추가
        foreach (string weaponName in longWeaponNames)
        {
            foreach (Transform child in longWeaponChildren)
            {
                if (child.name == weaponName)
                {
                    player_WeaponList.Add(child.gameObject);
                    break;
                }
            }
        }

        // closeWeapon 자식 중 무기 이름과 일치하는 오브젝트 추가
        foreach (string weaponName in closeWeaponNames)
        {
            foreach (Transform child in closeWeaponChildren)
            {
                if (child.name == weaponName)
                {
                    player_WeaponList.Add(child.gameObject);
                    break;
                }
            }
        }


        GameObject canvas = GameObject.Find("Canvas");
        attackBtn = canvas.transform.Find("FireBtn").GetComponent<Button>();

        // player_NonWeapon과 같은 깊이의(같은 부모를 가진) 오브젝트를 찾음
        Transform parentTransform = player_NonWeapon.transform.parent;

        // 부모의 자식 오브젝트들 중 DropItemPosition을 가지고 있는 오브젝트 찾기
        foreach (Transform sibling in parentTransform)
        {
            DropItemPosition dropItemPosition = sibling.GetComponent<DropItemPosition>();
    
            if (dropItemPosition != null)
            {
                // DropItemPosition을 가진 오브젝트를 찾았을 때 _dropItemPosition에 연결
                _dropItemPosition = dropItemPosition;
                break;
            }
        }
        
        int i;
        for (i=0; i < player_WeaponList.Count; i++)
        {
            player_WeaponList[i].SetActive(false);
        }
        
        player_LongWeapon.SetActive(false);
        player_CloseWeapon.SetActive(false);
        
    }

    

    // Update is called once per frame
    void Update()
    {
        if (Health <= 0)        //체력이 다 닳은 경우
        {
            Health = 0;         //체력바가 길어지는 것을 방지
        }
        
        //현재 총기류를 먹은경우
        if (player_LongWeapon.activeSelf)
        {
            player_LongWeapon.GetComponent<PlayerScriptRifle>().BulletInfo();
            //Debug.LogError("현재 잔탄 "  + CurrentBullet);
            //Debug.LogError("총 잔탄 "  + TotalBullet);
        }
        else
        {
            //잔탄의 수를 무한대로 하는 코드
        }
    }
    
    public void ChangeWeapon(WeaponType Wt, GameObject Weapon)
    {
        //모델링 활성화
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
        //무기 활성화
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
            try
            {
                player_CloseWeapon.GetComponent<PlayerScriptOneHand>().WeaponSynchronization();  //현재 잡은 무기 다시 탐색
            }
            catch (NullReferenceException e)
            {
               
            }
            
        }
        else if (Wt == WeaponType.longType)
        {
            try
            {
                player_LongWeapon.GetComponent<PlayerScriptRifle>().WeaponSynchronization();    //현재 잡은 무기 다시 탐색
            }
            catch (NullReferenceException e)
            {
               
            }
        }
    }
    
    
    //원거리 무기가 바뀌어도 현재 들고있는 무기의 탄을 가져오기
    //근접 무기의 경우 잔탄의 수는 무한대로 하면
    //-> 현재 들고있는 무기부터 판단
}
