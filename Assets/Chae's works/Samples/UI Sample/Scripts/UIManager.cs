using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // 연동할 스크립트 선언
    public InGameUI inGameUI;
    public Dialogue longDialogue;
    public Dialogue shortDialogue;

    // 싱글톤 선언
    #region
    public static UIManager instance = null;

    private void Awake()
    {
        if (instance == null) //instance가 null. 즉, 시스템상에 존재하고 있지 않을때
        {
            instance = this; //내자신을 instance로 넣어줍니다.
            DontDestroyOnLoad(gameObject); //OnLoad(씬이 로드 되었을때) 자신을 파괴하지 않고 유지
        }
        else
        {
            if (instance != this) //instance가 내가 아니라면 이미 instance가 하나 존재하고 있다는 의미
                Destroy(this.gameObject); //둘 이상 존재하면 안되는 객체이니 방금 AWake된 자신을 삭제
        }

        // 필요한 컴포넌트 가져오기
        inGameUI = GameObject.Find("InGameUI").GetComponent<InGameUI>();

        // **Find함수 사용 시, 하이어라키 내에서 반드시 활성화되어있어야 함. (Dialogue의 Start에서 스스로 비활성화함)
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
