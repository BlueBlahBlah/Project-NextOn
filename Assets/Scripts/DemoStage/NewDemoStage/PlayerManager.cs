using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
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


    [Header("Revive")]
    [SerializeField] private StageFailPanel _stageFailPanel;
    public int revive;      //부활 횟수
    private bool revive_decrease_once;      //부활 횟수를 1만 줄이기 위한 변수
    public bool is_close_weapon;            //현재 근접무기 들고있는지
    
    private void Awake()
    {
        
        if (null == instance)
        {
            //이 클래스 인스턴스가 탄생했을 때 전역변수 instance에 게임매니저 인스턴스가 담겨있지 않다면, 자신을 넣어준다.
            instance = this;

            //씬 전환이 되더라도 파괴되지 않게 한다.
            //gameObject만으로도 이 스크립트가 컴포넌트로서 붙어있는 Hierarchy상의 게임오브젝트라는 뜻이지만, 
            //나는 헷갈림 방지를 위해 this를 붙여주기도 한다.
            //DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            //만약 씬 이동이 되었는데 그 씬에도 Hierarchy에 GameMgr이 존재할 수도 있다.
            //그럴 경우엔 이전 씬에서 사용하던 인스턴스를 계속 사용해주는 경우가 많은 것 같다.
            //그래서 이미 전역변수인 instance에 인스턴스가 존재한다면 자신(새로운 씬의 GameMgr)을 삭제해준다.
            //Destroy(this.gameObject);
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
        
        // player_NonWeapon과 같은 깊이의(같은 부모를 가진) 오브젝트를 찾음
        Transform parentTransform = player_NonWeapon.transform.parent;
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
        
        player_LongWeapon.SetActive(false);
        player_CloseWeapon.SetActive(false);
        
        TotalHealth = 100;  //시작 시 체력 100
        Health = TotalHealth;
        SkillCoolTimeRate = 0;     //시작 시 스킬 쿨타임
        Death = false;
        revive = 3;
        is_close_weapon = false;
    }
    
    /*void OnEnable()
    {
        // 씬 매니저의 sceneLoaded에 체인을 건다.
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }*/
    
    // 체인을 걸어서 이 함수는 매 씬마다 호출된다.
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        init();
    }

    private void init()
    {
        TotalHealth = 100;  //시작 시 체력 100
        Health = TotalHealth;
        SkillCoolTimeRate = 0;     //시작 시 스킬 쿨타임
        Death = false;
        /*player_LongWeapon = GameObject.Find("Check_Sprite_Long");
        player_NonWeapon = GameObject.Find("Check_Sprite");
        player_CloseWeapon = GameObject.Find("Check_Sprite_Short");*/
        Debug.Log("********************tlqkf*******************");

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
        if (GameObject.Find("Check_Sprite_Long") is not null)
        {
            player_LongWeapon = GameObject.Find("Check_Sprite_Long");
        }
        if (GameObject.Find("Check_Sprite") is not null)
        {
            player_NonWeapon = GameObject.Find("Check_Sprite");
        }
        if (GameObject.Find("Check_Sprite_Short") is not null)
        {
            player_CloseWeapon = GameObject.Find("Check_Sprite_Short");
        }
        
       
        

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

        revive = 3;
        revive_decrease_once = false;
    }

    

    // Update is called once per frame
    void Update()
    {
        if (player_NonWeapon is null)
        {
            init();
        }
        if (Health <= 0)        //체력이 다 닳은 경우
        {
            Health = 0;         //체력바가 길어지는 것을 방지
            if (revive_decrease_once == false)
            {
                revive_decrease_once = true;
                revive -= 1;
                if(revive >= 1)
                    revive_Health_Invoke();
                else if (revive == 0)
                {
                    //진짜 끝남
                    EventManager.Instance.BGMaudioSource.Stop();
                    Invoke("OpenFailPanel", 3f);   
                    //씬 이동하는 코드
                }
            }
            
            
        }
        //현재 총기류를 먹은경우
        if (player_LongWeapon.activeSelf)
        {
            player_LongWeapon.GetComponent<PlayerScriptRifle>().BulletInfo();
            is_close_weapon = false;
            //Debug.LogError("현재 잔탄 "  + CurrentBullet);
            //Debug.LogError("총 잔탄 "  + TotalBullet);
        }
        else
        {
            //잔탄의 수를 무한대로 하는 코드
            is_close_weapon = true;
        }
    }

    private void OpenFailPanel()
    {
        _stageFailPanel.OpenPanel();
    }

    public void find_attackBtn_Invoke()
    {
        Invoke("find_attackBtn",3f);
    }
    private void find_attackBtn()
    {
       attackBtn = GameObject.Find("FireBtn").GetComponent<Button>();
    }

    private void revive_Health_Invoke()
    {
        Invoke("revive_Health",5.5f);
    }

    public void revive_Health()
    {
        Health = 100;
        revive_decrease_once = false;
        Death = false;
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

    public void longtypeweapon_bullet_init()
    {
        foreach (GameObject g in player_WeaponList)
        {
            if (g.GetComponent<Rifle>())
            {
                g.GetComponent<Rifle>().bulletCount = 60; 
                g.GetComponent<Rifle>().maxBulletCount = 100; 
            }
            else if (g.GetComponent<Shotgun>())
            {
                g.GetComponent<Shotgun>().bulletCount = 60; 
                g.GetComponent<Shotgun>().maxBulletCount = 100; 
            }
            else if (g.GetComponent<Sniper>())
            {
                g.GetComponent<Sniper>().bulletCount = 60; 
                g.GetComponent<Sniper>().maxBulletCount = 100; 
            }
            else if (g.GetComponent<GrenadeLauncher>())
            {
                g.GetComponent<GrenadeLauncher>().bulletCount = 60; 
                g.GetComponent<GrenadeLauncher>().maxBulletCount = 100; 
            }
            else if (g.GetComponent<MachineGun>())
            {
                g.GetComponent<MachineGun>().bulletCount = 60; 
                g.GetComponent<MachineGun>().maxBulletCount = 200; 
            }
            else if (g.GetComponent<FireGun>())
            {
                g.GetComponent<FireGun>().bulletCount = 60; 
                g.GetComponent<FireGun>().maxBulletCount = 100; 
            }
        }
    }
    
    
    //원거리 무기가 바뀌어도 현재 들고있는 무기의 탄을 가져오기
    //근접 무기의 경우 잔탄의 수는 무한대로 하면
    //-> 현재 들고있는 무기부터 판단
}
