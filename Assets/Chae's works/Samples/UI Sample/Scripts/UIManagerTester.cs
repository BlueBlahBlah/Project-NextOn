using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManagerTester : MonoBehaviour
{
    // UI의 각 기능이 정상작동하는지, 어떻게 연결되어있는지 참고하도록 도움을 주는 스크립트입니다.

    public UIManager uiManager;
    public InGameUI inGameUI;

    [Header("Test Setting")]
    [Header("Player")]
    public float setPlayerHp;
    public int initMaxBullet;
    [Header("Boss")]
    public bool setIsBoss;
    public float setBossHp;
    [Header("Gimmick")]
    public bool setIsGimmick;
    public float setGimmickProgressPercent;
    [Header("NumOfEnemy")]
    public int setNumOfEnemy;


    // Start is called before the first frame update
    void Start()
    {
        if (uiManager == null) uiManager = GameObject.Find("@UIManager").GetComponent<UIManager>();
        if (inGameUI == null) inGameUI = GameObject.Find("InGameUI").GetComponent<InGameUI>();
        FunctionTestStart();
    }

    // Update is called once per frame
    void Update()
    {
        SetUIs();
    }

    // Button test
    public void ShortDialoguePrint()
    {
        UIManager.instance.DialogueEventByNumber(UIManager.instance.shortDialogue, 0);
    }

    public void LongDialoguePrint()
    {
        UIManager.instance.DialogueEventByNumber(UIManager.instance.longDialogue, 0);
    }

    public void UseBullet()
    {
        if (inGameUI.CurrentBullet == 0)
        {
            inGameUI.CurrentBullet = inGameUI.MaxBullet;
        }
        else if (inGameUI.CurrentBullet > 0)
        {
            inGameUI.CurrentBullet--;
        }
    }

    public void SetUIs()
    {
        SetPlayerHp();
        SetBossHp();
        SetGimmickProgressPercent();
        SetNumberOfEnemy();
    }

    public void SetPlayerHp()
    {
        inGameUI.PlayerHp = setPlayerHp;
    }

    public void SetBossHp()
    {
        inGameUI.isBoss = setIsBoss;
        inGameUI.BossHp = setBossHp;
    }

    public void SetGimmickProgressPercent()
    {
        inGameUI.isGimmick = setIsGimmick;
        inGameUI.GimmickPercent = setGimmickProgressPercent;
    }

    public void SetNumberOfEnemy()
    {
        inGameUI.NumOfEnemy = setNumOfEnemy;
    }


    private void FunctionTestStart()
    {
        // ***이 함수를 응용하여, 아래 변수들에 실제 연결할 값을 대입합니다.

        // 테스트 초기화
        inGameUI.PlayerHp = setPlayerHp;
        inGameUI.BossHp = setBossHp;
        inGameUI.MaxBullet = initMaxBullet;
        inGameUI.CurrentBullet = inGameUI.MaxBullet;

        inGameUI.PlayerMaxHp = inGameUI.PlayerHp;
        inGameUI.BossMaxHp = inGameUI.BossHp;
        inGameUI.GimmickPercent = 0f;
        inGameUI.NumOfEnemy = 0;

        inGameUI.BossName = "Overflow"; // 현재 출현한 보스 이름 연결
        inGameUI.GimmickName = "Error404"; // 기믹 이름 연결
        inGameUI.WeaponName = "Weapon1"; // 현재 착용 중인 무기 이름 연결

        inGameUI.InitWeaponInfo();
    }
}
