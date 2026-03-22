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
    [SerializeField] private GameObject player_LongWeapon;      // 원거리 캐릭터 모델 루트
    [SerializeField] private GameObject player_NonWeapon;       // 비무장 캐릭터 모델 루트
    [SerializeField] private GameObject player_CloseWeapon;     // 근접 캐릭터 모델 루트

    [Header("무기 부착 위치 (오른손 등)")]
    [SerializeField] private Transform longWeaponMount;        // 원거리 모델의 오른손 본
    [SerializeField] private Transform closeWeaponMount;       // 근접 모델의 오른손 본

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
            instance = this;
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
    
    // 체인을 걸어서 이 함수는 매 씬마다 호출된다.
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        init();
    }

    private void init()
    {
        TotalHealth = 100;
        Health = TotalHealth;
        SkillCoolTimeRate = 0;
        Death = false;
        revive = 3;
        revive_decrease_once = false;

        player_WeaponList.Clear();
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
                revive_Health_Invoke();
            }
            
            
        }
        //현재 총기류를 먹은경우
        if (player_LongWeapon.activeSelf)
        {
            player_LongWeapon.GetComponent<PlayerScriptRifle>().BulletInfo();
            is_close_weapon = false;
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
    public void ChangeWeapon(ProjectNextOn.Data.WeaponData data)
    {
        if (data == null || data.itemPrefab == null) return;

        // 1. 모델 활성화 및 마운트 결정
        Transform parent = null;
        if (data.weaponType == WeaponType.longType)
        {
            parent = longWeaponMount;
            player_LongWeapon.SetActive(true);
            player_CloseWeapon.SetActive(false);
            player_NonWeapon.SetActive(false);
        }
        else if (data.weaponType == WeaponType.closeType)
        {
            parent = closeWeaponMount;
            player_LongWeapon.SetActive(false);
            player_CloseWeapon.SetActive(true);
            player_NonWeapon.SetActive(false);
        }
        else
        {
            player_LongWeapon.SetActive(false);
            player_CloseWeapon.SetActive(false);
            player_NonWeapon.SetActive(true);
            return;
        }

        // 2. 기존 무기 제거 (마운트 하위의 오브젝트만 파괴)
        if (parent != null)
        {
            foreach (Transform child in parent)
            {
                Destroy(child.gameObject);
            }

            // 3. 새 무기 생성 및 설정
            GameObject newWeapon = Instantiate(data.itemPrefab, parent);
            newWeapon.name = data.itemName; // 이름 설정
            
            // 트랜스폼 초기화 (손 위치에 딱 맞게)
            newWeapon.transform.localPosition = Vector3.zero;
            newWeapon.transform.localRotation = Quaternion.identity;

            // 4. 컴포넌트 연결 및 초기화
            if (data.weaponType == WeaponType.longType)
            {
                var gun = newWeapon.GetComponent<GunBase>();
                if (gun != null)
                {
                    gun.fireBtn = attackBtn; 
                    gun.playerRifle = player_LongWeapon.GetComponent<PlayerScriptRifle>(); 
                }
                
                player_LongWeapon.GetComponent<PlayerScriptRifle>().WeaponSynchronization();
            }
            else if (data.weaponType == WeaponType.closeType)
            {
                // 근접 무기 처리
                player_CloseWeapon.GetComponent<Space_PlayerScriptOneHand>().WeaponSynchronization();
                
                attackBtn.onClick.RemoveAllListeners();
                attackBtn.onClick.AddListener(player_CloseWeapon.GetComponent<Space_PlayerScriptOneHand>().OnAttackButtonClick);
            }
        }
    }
}
