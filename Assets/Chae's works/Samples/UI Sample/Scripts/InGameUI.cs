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



    // Start 함수나 다른 스크립트에서 가져올 변수

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Update
    #region
    public void UpdatePlayerInfo()
    {
        
    }

    public void UpdateBossInfo()
    {

    }

    public void UpdateGimmickInfo()
    {

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

    // Minimap
    #region
    #endregion
}
