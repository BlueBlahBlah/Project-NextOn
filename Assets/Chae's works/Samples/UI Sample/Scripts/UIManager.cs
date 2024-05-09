using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // @UIManager ������Ʈ�� ���ԵǴ� ��ũ��Ʈ�Դϴ�.
    // ���� ���̾��Ű ���� 'InGameUI'�� 'LongDialogue' UI ��������
    // ���Ե� ĵ������ �����ؾ� ������ �߻����� �ʽ��ϴ�.

    // ������ ��ũ��Ʈ ����
    public InGameUI inGameUI;
    public Dialogue longDialogue;
    public Dialogue shortDialogue;

    private bool isInGameUI;
    private bool isLongDialogue;
    private bool isShortDialogue;

    public bool isCompletelyPrinted; // ������ ��µƴ��� ���� �Ǵ�.

    // �̱��� ����
    #region
    public static UIManager instance = null;

    private void Awake()
    {
        if (instance == null) //instance�� null. ��, �ý��ۻ� �����ϰ� ���� ������
        {
            instance = this; //���ڽ��� instance�� �־��ݴϴ�.
            DontDestroyOnLoad(gameObject); //OnLoad(���� �ε� �Ǿ�����) �ڽ��� �ı����� �ʰ� ����
        }
        else
        {
            if (instance != this) //instance�� ���� �ƴ϶�� �̹� instance�� �ϳ� �����ϰ� �ִٴ� �ǹ�
                Destroy(this.gameObject); //�� �̻� �����ϸ� �ȵǴ� ��ü�̴� ��� AWake�� �ڽ��� ����
        }

        // �ʿ��� ������Ʈ ��������
        if (GameObject.Find("InGameUI") != null) 
        { 
            inGameUI = GameObject.Find("InGameUI").GetComponent<InGameUI>();
            isInGameUI = true; 
        }

        // **Find�Լ� ��� ��, ���̾��Ű ������ �ݵ�� Ȱ��ȭ�Ǿ��־�� ��. (Dialogue�� Start���� ������ ��Ȱ��ȭ��)
        if (GameObject.Find("LongDialogue") != null) 
        { 
            longDialogue = GameObject.Find("LongDialogue").GetComponent<Dialogue>();
            isLongDialogue = true;
        }
        if (GameObject.Find("ShortDialogue") != null)
        {
            shortDialogue = GameObject.Find("ShortDialogue").GetComponent<Dialogue>();
            isShortDialogue = true;
        }
        
    }
    #endregion
    
    void Start()
    {
        // GetManager => �̱������� ������� ���� �Ŵ������� �����ɴϴ�.
        // �Ŵ������� �̱������� �����ϵ��� ������ �����Ѵٸ� ȣ�� ����� �ٲߴϴ�.
        if (isInGameUI) { inGameUI.GetManager(); }
    }

    void Update()
    {
        if (isInGameUI) { UpdateUI(); }
        
    }

    // Initailize
    public void InitUI()
    {
        // ���� UIManagerTester ��ũ��Ʈ���� UI �ʱ�ȭ�� �ϰ�����.
    }

    // Update
    public void UpdateUI()
    {
        // InGameUI ��ũ��Ʈ���� ����� �پ��� UI Update �Լ����� ����
        inGameUI.UpdatePlayerInfo();
        inGameUI.UpdateBossInfo();
        inGameUI.UpdateGimmickInfo();
        inGameUI.UpdateNumOfEnemy();
        inGameUI.UpdateBullet();
    }

    // Dialogue
    #region
    public void DialogueEventByNumber(Dialogue _dialogue, int _dialogueNumber)
    {
        _dialogue.isDialogue = true;
        _dialogue.PrintDialogueByNumber(_dialogueNumber);
    }

    public void DialogueEventByKeyword(Dialogue _dialogue, string _keyword)
    {
        // ���� �̱����� �ڵ�
        _dialogue.PrintDialogueByKeyword(_keyword);
    }
    #endregion
}
