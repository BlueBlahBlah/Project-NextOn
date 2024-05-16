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
    private Image playerIcon; // 占시뤄옙占싱억옙占쏙옙 占쏙옙占쏙옙占쏙옙. 占쏙옙占승울옙 占쏙옙占쏙옙 占쏙옙占쏙옙 占쏙옙占쏙옙
    [SerializeField]
    private TextMeshProUGUI playerHp; // 占시뤄옙占싱억옙占쏙옙 체占쏙옙 占쌔쏙옙트
    [SerializeField]
    private Image playerHpBar; // 占시뤄옙占싱억옙占쏙옙 체占쏙옙 占쏙옙

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
    private Image bossIcon; // 占쏙옙占쏙옙 占쏙옙占쏙옙占쏙옙
    [SerializeField]
    private TextMeshProUGUI bossName; // 占쏙옙占쏙옙 占싱몌옙
    [SerializeField]
    private TextMeshProUGUI bossHp; // 占쏙옙占쏙옙 체占쏙옙 占쌔쏙옙트 (占쌜쇽옙트)
    [SerializeField]
    private Image bossHpBar; // 占쏙옙占쏙옙 체占쏙옙 占쏙옙

    [Header("Gimmick Information")]
    [SerializeField]
    private GameObject gimmickInfo;
    [SerializeField]
    private Image gimmickIcon; // 占쏙옙占 占쏙옙占쏙옙占쏙옙
    [SerializeField]
    private TextMeshProUGUI gimmickName; // 占쏙옙占 占싱몌옙
    [SerializeField]
    private TextMeshProUGUI gimmickPercent; // 占쏙옙占 占쏙옙占썅도 (占쌜쇽옙트)
    [SerializeField]
    private Image gimmickProgressBar; // 占쏙옙占 占쏙옙占쏙옙 占쏙옙

    [Header("Minimap")]
    [SerializeField]
    private TextMeshProUGUI numOfEnemy; // 占쏙옙占쏙옙 占쏙옙

    [Header("Dialogue")]
    [SerializeField]
    private GameObject dialogue; // 占쏙옙화창 占쏙옙占쏙옙占쏙옙트
    [SerializeField]
    private Image dialogueImage; // 占쏙옙화 캐占쏙옙占쏙옙 占싱뱄옙占쏙옙
    [SerializeField]
    private TextMeshProUGUI dialogueName; // 占쏙옙화 캐占쏙옙占쏙옙 占싱몌옙
    [SerializeField]
    private TextMeshProUGUI dialogueContent; // 占쏙옙화 占쏙옙占쏙옙
    [SerializeField]
    private float typingSpeed = 0.05f; // 占쏙옙화 占쏙옙占 占쌈듸옙
    #endregion

    // 占쏙옙占쏙옙 占쏙옙占쏙옙占쏙옙 占쏙옙占쏙옙 혹占쏙옙 占쏙옙크占쏙옙트 占쏙옙占쏙옙占쏙옙 InGameUI 클占쏙옙占쏙옙 占쏙옙占쏙옙占쏙옙 占쏙옙占쏙옙占쏙옙占쏙옙
    #region

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


    //[SerializeField] private StageManager stageManager;
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
    public void InitWeaponInfo() // 占쏙옙占쏙옙 占쏙옙占쏙옙 占십깍옙화 << 占쏙옙占쏙옙 1占쏙옙 占쏙옙占 占쏙옙 占쏙옙占쏙옙 占쏙옙占쏙옙
    {
        // 占싱뱄옙占쏙옙 占십깍옙화
        weaponImage.sprite = Resources.Load($"UI/Image/Icons/Weapons/{WeaponName}", typeof(Sprite)) as Sprite;

        // 占싼억옙 占쏙옙 占십깍옙화
        UpdateBullet();
    }
    #endregion

    // Update **가져오는 변수들을 다른 매니저에서 호출 할 수 있도록 변경 필요
    #region
    public void UpdatePlayerInfo() // 占시뤄옙占싱억옙 占쏙옙占쏙옙 占쏙옙占쏙옙
    {
        // 체占쏙옙 占쏙옙占쏙옙
        playerHp.text = PlayerHp.ToString() + " / " + PlayerMaxHp.ToString(); // 체占쏙옙 占쏙옙占쏙옙
        playerHpBar.fillAmount = PlayerHp / PlayerMaxHp; // 체占승뱄옙 占싱뱄옙占쏙옙 占쏙옙占쏙옙
    }

    public void UpdateBullet() // 占싼억옙 占쏙옙占쏙옙 
    {
        currentBullet.text = CurrentBullet.ToString();
        maxBullet.text = MaxBullet.ToString();
    }

    public void UpdateBossInfo() // 占쏙옙占쏙옙 占쏙옙占쏙옙 占쏙옙占쏙옙
    {
        if (isBoss)
        {
            // 占쏙옙占쏙옙 占쏙옙占쏙옙 占쏙옙
            if (!bossInfo.activeInHierarchy)
            {
                // 占쏙옙占쏙옙占쏙옙 占쏙옙占쏙옙 占쏙옙占쏙옙占쏙옙, UI占쏙옙 占쏙옙占쏙옙占쏙옙 占쏙옙占쏙옙 占쏙옙占승울옙占쏙옙 호占쏙옙
                // >> 占쏙옙占쏙옙 Info 占십깍옙화
                // 占쏙옙占쏙옙占쏙옙 占싱몌옙, 占싱뱄옙占쏙옙 占쏙옙 占쏙옙占쏙옙占쏙옙트
                bossName.text = BossName;
                bossIcon.sprite = Resources.Load($"UI/Image/Icons/BossIcons/{BossName}", typeof(Sprite)) as Sprite;
                bossInfo.SetActive(true);// UI 활占쏙옙화
            }
            // 占쏙옙占쏙옙 占쏙옙占쏙옙 占쏙옙占쏙옙
            bossHp.text = (Mathf.Round(BossHp / BossMaxHp * 100)).ToString() + "%"; // 체占쏙옙 占쏙옙占쏙옙
            bossHpBar.fillAmount = BossHp / BossMaxHp; // 체占승뱄옙 占싱뱄옙占쏙옙 占쏙옙占쏙옙
        }
        else
        {
            // 占쏙옙占쏙옙 처치 占쏙옙, 占십듸옙 占쏙옙占쏙옙 占쏙옙占쏙옙占쏙옙 占쏙옙占쏙옙占쏙옙占쏙옙 占쏙옙占쏙옙 占쏙옙 
            // >> 占쏙옙占쏙옙 Info 占쏙옙활占쏙옙화
            if (bossInfo.activeInHierarchy) bossInfo.SetActive(false);
        }
    }

    public void UpdateGimmickInfo() // 占쏙옙占 占쏙옙占쏙옙 占쏙옙占쏙옙
    {
        if (isGimmick)
        {
            if (!gimmickInfo.activeInHierarchy)
            {
                // 占쏙옙占쏙옙占 占쏙옙占쏙옙 占쏙옙占쏙옙占쏙옙, UI占쏙옙 占쏙옙占쏙옙占쏙옙 占쏙옙占쏙옙 占쏙옙占승울옙占쏙옙 호占쏙옙
                // >> 占쏙옙占 Info 占십깍옙화
                // 占쏙옙占쏙옙占 占싱몌옙, 占싱뱄옙占쏙옙 占쏙옙 占쏙옙占쏙옙占쏙옙트
                gimmickName.text = GimmickName;
                gimmickIcon.sprite = Resources.Load($"UI/Image/Icons/GimmickIcons/{GimmickName}", typeof(Sprite)) as Sprite;

                // 占쏙옙占 호占쏙옙 占쏙옙 0%占쏙옙 占쏙옙占쏙옙占싼다곤옙 占쏙옙占쏙옙
                gimmickPercent.text = "0%";
                gimmickProgressBar.fillAmount = 0f;

                gimmickInfo.SetActive(true);
            }

            // 占쏙옙占쏙옙占 占쏙옙占, 占쏙옙占쏙옙占 타占쏙옙 (占시곤옙 占쏙옙占 / 횟占쏙옙 占쏙옙占)占쏙옙 占쏙옙占쏙옙 占쌕몌옙占쏙옙 占쌘듸옙 占쌜쇽옙

        }
        else
        {
            // 占쏙옙占 占쏙옙占쏙옙 占쏙옙占쏙옙
            if (gimmickInfo.activeInHierarchy) gimmickInfo.SetActive(false);
        }

    }

    public void UpdateNumOfEnemy()
    {

        // 몬스터가 출현, 혹은 사망 시 미니맵 하단의 몬스터 수 갱신
        if (stageManager != null)
        {
            NumOfEnemy = stageManager.enemies.Length;
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
