using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // ������ ��ũ��Ʈ ����
    public InGameUI inGameUI;
    public Dialogue longDialogue;
    public Dialogue shortDialogue;

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
        inGameUI = GameObject.Find("InGameUI").GetComponent<InGameUI>();

        // **Find�Լ� ��� ��, ���̾��Ű ������ �ݵ�� Ȱ��ȭ�Ǿ��־�� ��. (Dialogue�� Start���� ������ ��Ȱ��ȭ��)
        longDialogue = GameObject.Find("LongDialogue").GetComponent<Dialogue>();
        shortDialogue = GameObject.Find("ShortDialogue").GetComponent<Dialogue>();
    }
    #endregion
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void DialogueEventByNumber(Dialogue _dialogue, int _dialogueNumber)
    {
        _dialogue.isDialogue = true;
        _dialogue.PrintDialogueByNumber(_dialogueNumber);
    }

    public void DialogueEventByKeyword(Dialogue _dialogue, string _keyword)
    {
        _dialogue.PrintDialogueByKeyword(_keyword);
    }
}
