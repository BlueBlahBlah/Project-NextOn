using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class InGameUI : MonoBehaviour
{
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
    #endregion

    // 실제 연결할 변수 혹은 스크립트 내에서 InGameUI 클래스 내에서 지역변수
    #region
    private float PlayerHp; // 플레이어 현재 체력 연결
    private float PlayerMaxHp; // 플레이어 최대 체력 연결

    private string WeaponName; // 무기 이름 연결 << 이미지 호출용
    private int CurrentBullet; // 실제 현재 총알 수 연결
    private int MaxBullet; // 실제 최대 총알 수 연결

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
    public int DialogueNumber; // 출력할 대화의 번호
    private float dialogueTime; // 대화의 길이 (WaitforSeconds 입력 변수)
    private int dialogueIsContinuous; // 이어지는 대화가 있는지 확인할 변수
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        data_Dialogue = CSVReader.Read("Data (.csv)/Dialogue"); // 다이얼로그 csv 파일 호출
        FunctionTestStart(); // 테스트용 코드
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePlayerInfo(); // 플레이어 info 항상 업데이트
        UpdateBossInfo(); // 보스 출현 시 보스 info 업데이트
        UpdateGimmickInfo(); // 기믹 출현 시 기믹 info 업데이트
        UpdateNumOfEnemy(); // 적의 수 업데이트
        UpdateBullet();


        FunctionTestUpdate(); // 테스트용 코드
    }


    // Initailize
    #region
    public void InitWeaponInfo() // 무기 정보 초기화 << 무기 1개 사용 할 때로 가정
    {
        // 이미지 초기화
        weaponImage.sprite = Resources.Load($"UI/Image/Icons/Weapons/{WeaponName}", typeof(Sprite)) as Sprite;

        // 총알 수 초기화
        UpdateBullet();
    }
    #endregion

    // Update
    #region
    public void UpdatePlayerInfo() // 플레이어 정보 갱신
    {
        // 체력 관련
        playerHp.text = PlayerHp.ToString() + " / " + PlayerMaxHp.ToString(); // 체력 갱신
        playerHpBar.fillAmount = PlayerHp / PlayerMaxHp; // 체력바 이미지 갱신
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
        numOfEnemy.text = NumOfEnemy.ToString();
    }
    #endregion

    // Event
    public void DialogueEvent() // 대화 번호 입력값으로 받음
    {
        if (isDialogue)
        {
            if (!dialogue.activeInHierarchy)
            {
                dialogue.SetActive(true);
            }
            string Name = data_Dialogue[DialogueNumber]["Character Name"].ToString();

            // 이미지 변경
            // ** 수정 필요한 곳1) csv에 저장된 캐릭터 이름은 한글인데, 리소스에서 Load할 이름은 영어여야 함.
            // - 아이디어
            // 대화 이미지를 사용할 캐릭터가 많지 않으니 Switch문으로 받아온 Name 비교.
            // dialogueImage.sprite = Resources.Load($"UI/Image/Characters/{Name}", typeof(Sprite)) as Sprite;

            // 이름 변경
            dialogueName.text = Name;

            // 내용 변경
            dialogueContent.text = data_Dialogue[DialogueNumber]["Contents"].ToString();

            // ** 수정 필요한 곳4) 다이얼로그의 대사 순서가 선형적이지 않을 때, 특정 넘버에서 시작하도록 하는 기능이 필요함
            dialogueTime = float.Parse(data_Dialogue[DialogueNumber]["Time"].ToString());
            dialogueIsContinuous = int.Parse(data_Dialogue[DialogueNumber]["Continuous"].ToString());

            Debug.Log($"Time : {dialogueTime}, Continuous : {dialogueIsContinuous}");
            StartCoroutine("Dialogue");
        }
        else
        {
            if (dialogue.activeInHierarchy) dialogue.SetActive(false);
        }
    }

    // Button
    #region
    public void ButtonPause()
    {
        // 실제 플레이 Pause 기능

        // 다시하기, Setting, 저장 및 종료 UI 호출
    }
    #endregion

    // Coroutine
    #region
    IEnumerator Dialogue()
    {
        yield return new WaitForSeconds(dialogueTime);
        if (dialogueIsContinuous == 1)
        {
            DialogueNumber++;
            DialogueEvent();
        }
        else if (dialogueIsContinuous == 0)
        {
            isDialogue = false;
            DialogueEvent();
        }
        yield return null;
    }
    #endregion

    // Test
    #region
    private void FunctionTestStart()
    {
        // 이 함수를 응용하여, 아래 변수들에 실제 연결할 값을 대입합니다.

        // 테스트 초기화
        PlayerHp = 100f;
        BossHp = 100f;

        isBoss = true; // 보스가 출현하였음을 가정합니다.
        isGimmick = true; // 기믹이 출현하였음을 가정합니다.
        isDialogue = true; // 대사가 출력됨을 가정합니다.

        PlayerMaxHp = PlayerHp;
        BossMaxHp = BossHp;
        GimmickPercent = 0f;
        NumOfEnemy = 0;
        DialogueNumber = 0;

        CurrentBullet = 60;
        MaxBullet = 60;

        BossName = "Overflow"; // 현재 출현한 보스 이름 연결
        GimmickName = "Error404"; // 기믹 이름 연결
        WeaponName = "Weapon1"; // 현재 착용 중인 무기 이름 연결
        
        
        InitWeaponInfo();
        DialogueEvent();
    }

    

    private void FunctionTestUpdate()
    {
        // 테스트 업데이트
        if (PlayerHp == PlayerMaxHp)
        {
            StartCoroutine("TestCoroutine");
            StartCoroutine("TestCoroutineBullet");
        }
    }

    IEnumerator TestCoroutine()
    {
        // 테스트 코루틴
        // 플레이어의 체력, 보스의 체력, 다이얼로그의 출현 및 갱신, 미니맵 하단 적의 수 기능을 테스트합니다.

        PlayerHp--;
        BossHp -= 10;
        NumOfEnemy++;
        yield return new WaitForSeconds(1f);
        PlayerHp--;
        BossHp -= 10;
        NumOfEnemy++;
        yield return new WaitForSeconds(1f);
        PlayerHp--;
        BossHp -= 10;
        NumOfEnemy++;
        yield return new WaitForSeconds(1f);
        PlayerHp--;
        BossHp -= 10;
        NumOfEnemy++;
        yield return new WaitForSeconds(1f);
        PlayerHp--;
        BossHp -= 10;
        NumOfEnemy++;
        yield return new WaitForSeconds(1f);
        PlayerHp--;
        BossHp -= 10;
        NumOfEnemy++;
        yield return new WaitForSeconds(1f);
        PlayerHp--;
        BossHp -= 10;
        NumOfEnemy++;
        yield return new WaitForSeconds(1f);
        PlayerHp--;
        BossHp -= 10;
        NumOfEnemy++;
        yield return new WaitForSeconds(1f);
        PlayerHp--;
        BossHp -= 10;
        NumOfEnemy++;
        yield return new WaitForSeconds(1f);
        PlayerHp--;
        NumOfEnemy++;
        yield return new WaitForSeconds(5f);
        isBoss = false;
        isGimmick = false;

        yield return null;
    }

    IEnumerator TestCoroutineBullet()
    {
        CurrentBullet--;
        yield return new WaitForSeconds(0.2f);
        CurrentBullet--;
        yield return new WaitForSeconds(0.2f);
        CurrentBullet--;
        yield return new WaitForSeconds(0.2f);
        CurrentBullet--;
        yield return new WaitForSeconds(0.2f);
        CurrentBullet--;
        yield return new WaitForSeconds(0.2f);
        CurrentBullet--;
        yield return new WaitForSeconds(0.2f);
        CurrentBullet--;
        yield return new WaitForSeconds(0.2f);
        CurrentBullet--;
        yield return new WaitForSeconds(0.2f);
        CurrentBullet--;
        yield return new WaitForSeconds(0.2f);
        CurrentBullet--;
        yield return new WaitForSeconds(0.2f);
        CurrentBullet--;
        yield return new WaitForSeconds(0.2f);
        CurrentBullet--;
        yield return new WaitForSeconds(0.2f);
        CurrentBullet--;
        yield return new WaitForSeconds(0.2f);
        CurrentBullet--;
        yield return new WaitForSeconds(0.2f);
        CurrentBullet--;
        yield return new WaitForSeconds(0.2f);
        CurrentBullet--;
        yield return new WaitForSeconds(0.2f);
        CurrentBullet--;
        yield return new WaitForSeconds(0.2f);
        CurrentBullet--;
        yield return new WaitForSeconds(0.2f);
        CurrentBullet--;
        yield return new WaitForSeconds(0.2f);
        CurrentBullet--;
        yield return new WaitForSeconds(0.2f);
        CurrentBullet--;
        yield return new WaitForSeconds(0.2f);
        CurrentBullet--;
        yield return new WaitForSeconds(0.2f);
        CurrentBullet--;
        yield return new WaitForSeconds(0.2f);
        CurrentBullet--;
        yield return new WaitForSeconds(0.2f);
        CurrentBullet--;
        yield return new WaitForSeconds(0.2f);
        CurrentBullet--;
        yield return new WaitForSeconds(0.2f);
        CurrentBullet--;
        yield return new WaitForSeconds(0.2f);
        CurrentBullet--;
        yield return new WaitForSeconds(0.2f);
        CurrentBullet--;
        yield return new WaitForSeconds(0.2f);
        CurrentBullet--;
        yield return new WaitForSeconds(0.2f);
        yield return null;
    }
    #endregion
}
