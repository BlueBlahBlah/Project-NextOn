using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InGameUI : MonoBehaviour
{
    // 사전에 미리 연결 필요한 변수
    [Header("Player Information")]
    [SerializeField]
    private GameObject playerInfo;
    [SerializeField]
    private Image playerIcon; // 플레이어의 아이콘. 상태에 따라 변경 가능
    [SerializeField]
    private TextMeshProUGUI playerHp; // 플레이어의 체력 텍스트
    [SerializeField]
    private Image playerHpBar; // 플레이어의 체력 바
    //[SerializeField]
    //private Image playerWeapon1; // 플레이어의 무기 이미지1
    //[SerializeField]
    //private Image playerWeapon2; // 플레이어의 무기 이미지2

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


    // 실제 연결할 변수
    private float PlayerHp; // 플레이어 현재 체력 연결
    private float PlayerMaxHp; // 플레이어 최대 체력 연결

    private bool isBoss; // 보스 존재 유무 연결
    private float BossHp; // 보스 체력 연결
    private float BossMaxHp; // 보스 최대 체력 연결
    private string BossName; // 보스 이름 연결

    private bool isGimmick; // 기믹 존재 유무 연결
    private float GimmickPercent; // 기믹 진행도 연결
    private string GimmickName; // 기믹 이름 연결

    private int NumOfEnemy; // 필드 내 몬스터 수 연결

    private bool isDialogue; // 대화창 표시 여부
    private List<Dictionary<string, object>> data_Dialogue; // csv 파일 담을 변수

    // Start is called before the first frame update
    void Start()
    {
        FunctionTestStart(); // 테스트용 코드

        data_Dialogue = CSVReader.Read("Data (.csv)/Dialogue"); // csv 파일 호출
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePlayerInfo(); // 플레이어 info 항상 업데이트
        if (isBoss) UpdateBossInfo(); // 보스 출현 시 보스 info 업데이트
        if (isGimmick) UpdateGimmickInfo(); // 기믹 출현 시 기믹 info 업데이트
        UpdateNumOfEnemy(); // 적의 수 업데이트

        FunctionTestUpdate(); // 테스트용 코드
    }

    private void FunctionTestStart() 
    {
        // 테스트 초기화
        PlayerHp = 100f;
        BossHp = 100f;

        isBoss = true;
        isGimmick = true;

        PlayerMaxHp = PlayerHp;
        BossMaxHp = BossHp;
        GimmickPercent = 0f;
        NumOfEnemy = 0;

        BossName = "Overflow"; // 현재 출현한 보스 이름 연결
        GimmickName = "Gimmick"; // 기믹 이름 연결
    }
    
    private void FunctionTestUpdate()
    {
        // 테스트 업데이트
        if (PlayerHp == PlayerMaxHp) StartCoroutine("TestCoroutine");
    }

    IEnumerator TestCoroutine()
    {
        // 테스트 코루틴
        PlayerHp--;
        BossHp -= 7;
        NumOfEnemy++;
        yield return new WaitForSeconds(1f);
        PlayerHp--;
        BossHp -= 7;
        NumOfEnemy++;
        yield return new WaitForSeconds(1f);
        UpdateDialogue(0);
        PlayerHp--;
        BossHp -= 7;
        NumOfEnemy++;
        yield return new WaitForSeconds(1f); 
        PlayerHp--;
        BossHp -= 7;
        NumOfEnemy++;
        yield return new WaitForSeconds(1f); 
        PlayerHp--;
        BossHp -= 7;
        NumOfEnemy++;
        yield return new WaitForSeconds(1f);

        yield return null;
    }

    // Update
    #region
    public void UpdatePlayerInfo() // 플레이어 정보 갱신
    {
        // 체력 관련
        playerHp.text = PlayerHp.ToString() + " / " + PlayerMaxHp.ToString(); // 체력 갱신
        playerHpBar.fillAmount = PlayerHp / PlayerMaxHp; // 체력바 이미지 갱신
    }

    public void UpdateBossInfo() // 보스 정보 갱신
    {

        if (!bossInfo.activeInHierarchy)
        {
            // 보스가 출현 했지만, UI가 켜지지 않은 상태에서 호출
            // >> 보스 Info 초기화
            // 보스의 이름, 이미지 등 업데이트
            bossName.text = BossName;
            bossIcon.sprite = Resources.Load($"UI/Image/BossIcons/{BossName}", typeof(Sprite)) as Sprite;
            bossInfo.SetActive(true);// UI 활성화
        }
        bossHp.text = (Mathf.Round(BossHp / BossMaxHp * 100)).ToString() + "%"; // 체력 갱신
        bossHpBar.fillAmount = BossHp / BossMaxHp; // 체력바 이미지 갱신

        if (!isBoss)
        {
            // 보스 처치 시, 필드 내에 보스가 존재하지 않을 때 
            // >> 보스 Info 비활성화
            // ***** Update 에서 !isBoss 면 이 함수로 접근 불가능하므로 수정 필요
            bossInfo.SetActive(false);
        }
    }

    public void UpdateGimmickInfo() // 기믹 정보 갱신
    {
        // UI 활성화
        if (!gimmickInfo.activeInHierarchy)
        {
            // 기믹이 출현 했지만, UI가 켜지지 않은 상태에서 호출
            // >> 기믹 Info 초기화
            // 기믹의 이름, 이미지 등 업데이트
            //gimmickName.text = GimmickName;
            //gimmickIcon.sprite = Resources.Load($"UI/Image/GimmickIcons/{GimmickName}", typeof(Sprite)) as Sprite;

            // 기믹 호출 시 0%로 시작한다고 가정
            gimmickPercent.text = "0%";
            gimmickProgressBar.fillAmount = 0f;

            gimmickInfo.SetActive(true);
        }

        // 기믹의 경우, 기믹의 타입 (시간 기반 / 횟수 기반)에 따라 다르게 코드 작성
    }

    public void UpdateNumOfEnemy()
    {
        // 몬스터가 출현, 혹은 사망 시 미니맵 하단의 몬스터 수 갱신
        numOfEnemy.text = NumOfEnemy.ToString();
    }
    #endregion
    
    public void UpdateDialogue(int number) // 대화 번호 입력값으로 받음
    {
        if (!dialogue.activeInHierarchy)
        {
            dialogue.SetActive(true);
        }
        string Name = data_Dialogue[number]["Character Name"].ToString();
        Debug.Log(Name);
        // 이미지 변경
        // dialogueImage.sprite = Resources.Load($"UI/Image/Characters/{Name}", typeof(Sprite)) as Sprite;
        // 이름 변경
        dialogueName.text = Name;
        // 내용 변경
        dialogueContent.text = data_Dialogue[number]["Contents"].ToString();
    }
    // Button
    #region
    public void ButtonPause()
    {

    }

    public void ButtonPlayerInfo()
    {

    }
    #endregion

   
}
