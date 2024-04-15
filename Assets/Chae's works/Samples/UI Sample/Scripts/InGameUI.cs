using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InGameUI : MonoBehaviour
{
    // ������ �̸� ���� �ʿ��� ����
    [Header("Player Information")]
    [SerializeField]
    private GameObject playerInfo;
    [SerializeField]
    private Image playerIcon; // �÷��̾��� ������. ���¿� ���� ���� ����
    [SerializeField]
    private TextMeshProUGUI playerHp; // �÷��̾��� ü�� �ؽ�Ʈ
    [SerializeField]
    private Image playerHpBar; // �÷��̾��� ü�� ��
    [SerializeField]
    private Image playerWeapon1; // �÷��̾��� ���� �̹���1
    [SerializeField]
    private Image playerWeapon2; // �÷��̾��� ���� �̹���2

    [Header("Boss Information")]
    [SerializeField]
    private GameObject bossInfo;
    [SerializeField]
    private Image bossIcon; // ���� ������
    [SerializeField]
    private TextMeshProUGUI bossName; // ���� �̸�
    [SerializeField]
    private TextMeshProUGUI bossHp; // ���� ü�� �ؽ�Ʈ (�ۼ�Ʈ)
    [SerializeField]
    private Image bossHpBar; // ���� ü�� ��

    [Header("Gimmick Information")]
    [SerializeField]
    private GameObject gimmickInfo;
    [SerializeField]
    private Image gimmickIcon; // ��� ������
    [SerializeField]
    private TextMeshProUGUI gimmickName; // ��� �̸�
    [SerializeField]
    private TextMeshProUGUI gimmickPercent; // ��� ���൵ (�ۼ�Ʈ)
    [SerializeField]
    private Image gimmickProgressBar; // ��� ���� ��

    [Header("Minimap")]
    [SerializeField]
    private TextMeshProUGUI numOfEnemy; // ���� ��

    // Start �Լ��� �ٸ� ��ũ��Ʈ���� ������ ����
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
        FunctionTestStart(); // �׽�Ʈ�� �ڵ�
        PlayerMaxHp = PlayerHp; 
        BossMaxHp = BossHp;
        GimmickPercent = 0f;
        NumOfEnemy = 0;
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePlayerInfo(); // �÷��̾� info �׻� ������Ʈ
        if (isBoss) UpdateBossInfo(); // ���� ���� �� ���� info ������Ʈ
        if (isGimmick) UpdateGimmickInfo(); // ��� ���� �� ��� info ������Ʈ
        UpdateNumOfEnemy(); // ���� �� ������Ʈ

        FunctionTestUpdate(); // �׽�Ʈ�� �ڵ�
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
        // ü�� ����
        playerHp.text = PlayerHp.ToString() + " / " + PlayerMaxHp.ToString(); // ü�� ����
        playerHpBar.fillAmount = PlayerHp / PlayerMaxHp; // ü�¹� �̹��� ����
    }

    public void UpdateBossInfo()
    {

        if (!bossInfo.activeInHierarchy)
        {
            // ������ ���� ������, UI�� ������ ���� ���¿��� ȣ��
            // >> ���� Info �ʱ�ȭ
            // ������ �̸�, �̹��� �� ������Ʈ
            // bossName.text = "BossName";
            // bossIcon.sprite = Resource~~
            bossInfo.SetActive(true);// UI Ȱ��ȭ
        }
        bossHp.text = (Mathf.Round(BossHp / BossMaxHp * 100)).ToString() + "%"; // ü�� ����
        bossHpBar.fillAmount = BossHp / BossMaxHp; // ü�¹� �̹��� ����


    }

    public void UpdateGimmickInfo()
    {
        // UI Ȱ��ȭ
        if (!gimmickInfo.activeInHierarchy)
        {
            // ����� ���� ������, UI�� ������ ���� ���¿��� ȣ��
            // >> ��� Info �ʱ�ȭ
            // ����� �̸�, �̹��� �� ������Ʈ
            // GimmickName.text = "GimmickName";
            // GimmickIcon.sprite = Resource~~
            gimmickPercent.text = "0%";
            gimmickProgressBar.fillAmount = 0f;
            gimmickInfo.SetActive(true);
        }

        // ����� ���, ����� Ÿ�� (�ð� ��� / Ƚ�� ���)�� ���� �ٸ��� �ڵ� �ۼ�
    }

    public void UpdateNumOfEnemy()
    {
        // ���Ͱ� ����, Ȥ�� ��� �� �̴ϸ� �ϴ��� ���� �� ����
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
