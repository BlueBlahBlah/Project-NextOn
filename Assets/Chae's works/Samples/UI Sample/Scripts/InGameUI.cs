using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InGameUI : MonoBehaviour
{
    // 유저 인터페이스에 보여지는 수치를 조절하는 변수들을 저장하고, 이를 수정하는 함수들을
    // 작성한 스크립트로, 'InGameUI' 프리팹에 포함되는 스크립트입니다.
    // 캔버스의 각 요소를 연결하고, 여러 매니저에서 데이터를 가져와 알맞은 부분에 적용시킵니다.
    // 단, 실제로는 <UIManager> 스크립트에서 InGameUI 스크립트를 가져와 업데이트 함수들을 수행합니다.

    // 사전에 미리 연결 필요한 변수 [SerializeField]
    #region
    [Header("Player Information")]
    [SerializeField]
    private GameObject playerInfo;
    [SerializeField]
    private Image playerIcon; // 플레이어의 아이콘. 상태에 따라 변경 가능
    [SerializeField]
    private TextMeshProUGUI playerHp; // 플레이어의 체력 텍스트
    [SerializeField]
    private Image playerHpBar; // 플레이어의 체력 바

    [Header("Weapon Information")]
    [SerializeField]
    private Image weaponImage;
    [SerializeField]
    private TextMeshProUGUI currentBullet;
    [SerializeField]
    private TextMeshProUGUI maxBullet;

    [Header("Boss Information")]
    [SerializeField]
    private GameObject bossInfo;
    [SerializeField]
    private Image bossIcon; // 보스 아이콘
    [SerializeField]
    private TextMeshProUGUI bossName; // 보스 이름
    [SerializeField]
    private TextMeshProUGUI bossHp; // 보스 체력 텍스트 (퍼센트)
    [SerializeField]
    private Image bossHpBar; // 보스 체력 바

    [Header("Gimmick Information")]
    [SerializeField]
    private GameObject gimmickInfo;
    [SerializeField]
    private Image gimmickIcon; // 기믹 아이콘
    [SerializeField]
    private TextMeshProUGUI gimmickName; // 기믹 이름
    [SerializeField]
    private TextMeshProUGUI gimmickPercent; // 기믹 진행도 (퍼센트)
    [SerializeField]
    private Image gimmickProgressBar; // 기믹 진행 바

    [Header("Minimap")]
    [SerializeField]
    private TextMeshProUGUI numOfEnemy; // 몬스터 수

    [Header("Dialogue")]
    [SerializeField]
    private GameObject dialogue; // 대화창 오브젝트
    [SerializeField]
    private Image dialogueImage; // 대화 캐릭터 이미지
    [SerializeField]
    private TextMeshProUGUI dialogueName; // 대화 캐릭터 이름
    [SerializeField]
    private TextMeshProUGUI dialogueContent; // 대화 내용
    [SerializeField]
    private float typingSpeed = 0.05f; // 대화 출력 속도
    #endregion

    // 실제 연결할 변수 혹은 스크립트 내에서 InGameUI 클래스 내에서 지역변수
    #region
    [Header("Value")]
    public float PlayerHp; // 플레이어 현재 체력 연결
    public float PlayerMaxHp; // 플레이어 최대 체력 연결

    public string WeaponName; // 무기 이름 연결 << 이미지 호출용
    public int CurrentBullet; // 실제 현재 총알 수 연결
    public int MaxBullet; // 실제 최대 총알 수 연결

    public bool isBoss; // 보스 존재 유무 연결
    public float BossHp; // 보스 체력 연결
    public float BossMaxHp; // 보스 최대 체력 연결
    public string BossName; // 보스 이름 연결

    public bool isGimmick; // 기믹 존재 유무 연결
    public float GimmickPercent; // 기믹 진행도 연결
    public int GimmickCount;
    public string GimmickName; // 기믹 이름 연결

    public int NumOfEnemy; // 필드 내 몬스터 수 연결

    [SerializeField]
    private StageManager stageManager;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // GetManager => 싱글톤으로 선언되지 않은 매니저들을 가져옵니다.
    // 매니저들을 싱글톤으로 선언하도록 구조를 변경한다면 호출 방식을 바꿉니다.
    public void GetManager()
    {
        GetStageManager();
    }

    public void GetStageManager()
    {
        if (GameObject.Find("StageManager") != null) 
            stageManager = GameObject.Find("StageManager").GetComponent<StageManager>();
    }

    // Initailize
    #region
    public void InitWeaponInfo() // 무기 정보 초기화 << 무기 1개 사용 할 때로 가정
    {
        // 총알 수 초기화
        UpdateBullet();
    }
    #endregion

    // Update **가져오는 변수들을 다른 매니저에서 호출 할 수 있도록 변경 필요
    #region
    public void UpdatePlayerInfo() // 플레이어 정보 갱신
    {
        // 체력 관련
        playerHp.text = PlayerHp.ToString() + " / " + PlayerMaxHp.ToString(); // 체력 갱신
        playerHpBar.fillAmount = PlayerHp / PlayerMaxHp; // 체력바 이미지 갱신}
    }

    public void UpdateBullet() // 총알 갱신 
    {
        currentBullet.text = CurrentBullet.ToString();
        maxBullet.text = MaxBullet.ToString();
    }

    public void UpdateBossInfo() // 보스 정보 갱신
    {
        if (isBoss)
        {
            // 보스 출현 시
            if (!bossInfo.activeInHierarchy)
            {
                // 보스가 출현 했지만, UI가 켜지지 않은 상태에서 호출
                // >> 보스 Info 초기화
                // 보스의 이름, 이미지 등 업데이트
                bossName.text = BossName;
                bossIcon.sprite = Resources.Load($"UI/Image/Icons/BossIcons/{BossName}", typeof(Sprite)) as Sprite;
                bossInfo.SetActive(true);// UI 활성화
            }
            // 보스 정보 갱신
            bossHp.text = (Mathf.Round(BossHp / BossMaxHp * 100)).ToString() + "%"; // 체력 갱신
            bossHpBar.fillAmount = BossHp / BossMaxHp; // 체력바 이미지 갱신
        }
        else
        {
            // 보스 처치 시, 필드 내에 보스가 존재하지 않을 때 
            // >> 보스 Info 비활성화
            if (bossInfo.activeInHierarchy) bossInfo.SetActive(false);
        }
    }

    public void UpdateGimmickInfo() // 기믹 정보 갱신
    {
        if (isGimmick)
        {
            if (!gimmickInfo.activeInHierarchy)
            {
                // 기믹이 출현 했지만, UI가 켜지지 않은 상태에서 호출
                // >> 기믹 Info 초기화
                // 기믹의 이름, 이미지 등 업데이트
                gimmickName.text = GimmickName;
                gimmickIcon.sprite = Resources.Load($"UI/Image/Icons/GimmickIcons/{GimmickName}", typeof(Sprite)) as Sprite;

                // 기믹 호출 시 0%로 시작한다고 가정
                gimmickPercent.text = "0%";
                gimmickProgressBar.fillAmount = 0f;

                gimmickInfo.SetActive(true);
            }

            // 기믹의 경우, 기믹의 타입 (시간 기반 / 횟수 기반)에 따라 다르게 코드 작성

        }
        else
        {
            // 기믹 출현 종료
            if (gimmickInfo.activeInHierarchy) gimmickInfo.SetActive(false);
        }

    }

    public void UpdateNumOfEnemy()
    {
        // 몬스터가 출현, 혹은 사망 시 미니맵 하단의 몬스터 수 갱신
        if (stageManager != null)
        {
            //NumOfEnemy = stageManager.enemies.Length;
        }
        else
        {
            Debug.Log("There's no stageManager!");
        }
        numOfEnemy.text = NumOfEnemy.ToString();
    }
    #endregion

 

    // Button


    
}
