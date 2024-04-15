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
    [SerializeField]
    private Image playerWeapon1; // 플레이어의 무기 이미지1
    [SerializeField]
    private Image playerWeapon2; // 플레이어의 무기 이미지2

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

    // Start 함수나 다른 스크립트에서 가져올 변수
    private float PlayerHp;
    private float PlayerMaxHp;

    private bool isBoss;
    private float BossHp;
    private float BossMaxHp;

    private bool isGimmick;
    private float GimmickPercent;

    private int NumOfEnemy;
    
    // Start is called before the first frame update
    void Start()
    {
        FunctionTestStart(); // 테스트용 코드
        PlayerMaxHp = PlayerHp; 
        BossMaxHp = BossHp;
        GimmickPercent = 0f;
        NumOfEnemy = 0;
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
        PlayerHp = 100f;
        BossHp = 100f;

        isBoss = true;
        isGimmick = true;
    }
    
    private void FunctionTestUpdate()
    {
        if (PlayerHp == PlayerMaxHp) StartCoroutine("TestCoroutine");
    }

    IEnumerator TestCoroutine()
    {
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
    public void UpdatePlayerInfo()
    {
        // 체력 관련
        playerHp.text = PlayerHp.ToString() + " / " + PlayerMaxHp.ToString(); // 체력 갱신
        playerHpBar.fillAmount = PlayerHp / PlayerMaxHp; // 체력바 이미지 갱신
    }

    public void UpdateBossInfo()
    {

        if (!bossInfo.activeInHierarchy)
        {
            // 보스가 출현 했지만, UI가 켜지지 않은 상태에서 호출
            // >> 보스 Info 초기화
            // 보스의 이름, 이미지 등 업데이트
            // bossName.text = "BossName";
            // bossIcon.sprite = Resource~~
            bossInfo.SetActive(true);// UI 활성화
        }
        bossHp.text = (Mathf.Round(BossHp / BossMaxHp * 100)).ToString() + "%"; // 체력 갱신
        bossHpBar.fillAmount = BossHp / BossMaxHp; // 체력바 이미지 갱신


    }

    public void UpdateGimmickInfo()
    {
        // UI 활성화
        if (!gimmickInfo.activeInHierarchy)
        {
            // 기믹이 출현 했지만, UI가 켜지지 않은 상태에서 호출
            // >> 기믹 Info 초기화
            // 기믹의 이름, 이미지 등 업데이트
            // GimmickName.text = "GimmickName";
            // GimmickIcon.sprite = Resource~~
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
