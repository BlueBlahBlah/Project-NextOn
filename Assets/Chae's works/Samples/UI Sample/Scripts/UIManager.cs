using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // @UIManager 오브젝트에 포함되는 스크립트입니다.
    // 같은 하이어라키 내에 'InGameUI'와 'LongDialogue' UI 프리팹이
    // 포함된 캔버스가 존재해야 오류가 발생하지 않습니다.
    // Dialogue 스크립트, InGameUI 스크립트에서 메소드를 가져오는 코드가 많아
    // 해당 스크립트를 참조하면 좋습니다.

    // 연동할 스크립트 선언
    [Header("Components")]
    public InGameUI inGameUI;
    public Dialogue longDialogue;
    public Dialogue shortDialogue;

    [Header("Data")]
    public int ScenarioNumber; // 현재 시나리오 넘버
    public int DialogueNumber; // 현재 csv 파일의 라인 넘버
    public bool isCompletelyPrinted; // SkipAndNext 함수를 위해 필요. 텍스트의 완전한 출력 여부 판단
    public bool doNext;

    [Header("Option")]
    public bool isAuto;
    public int printSpeed = 1;

    private bool isInGameUI;
    private bool isLongDialogue;
    private bool isShortDialogue;

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
        if (GameObject.Find("InGameUI") != null) 
        { 
            inGameUI = GameObject.Find("InGameUI").GetComponent<InGameUI>();
            isInGameUI = true; 
        }

        // **Find함수 사용 시, 하이어라키 내에서 활성화되어있지 않으면 오류 발생. (Dialogue의 Start에서 스스로 비활성화함)
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
        // GetManager => 싱글톤으로 선언되지 않은 매니저들을 가져옵니다.
        // 매니저들을 싱글톤으로 선언하도록 구조를 변경한다면 호출 방식을 바꿉니다.
        if (isInGameUI) { inGameUI.GetManager(); }
    }

    void Update()
    {
        if (isInGameUI) { UpdateInGameUI(); }
        
    }

    // InGameUI 기능
    #region
    public void InitUI()
    {
        // 현재 UIManagerTester 스크립트에서 UI 초기화를 하고있음.
    }

    public void UpdateInGameUI()
    {
        // InGameUI 스크립트에서 선언된 다양한 UI Update 함수들을 실행
        inGameUI.UpdatePlayerInfo();
        inGameUI.UpdateBossInfo();
        inGameUI.UpdateGimmickInfo();
        inGameUI.UpdateNumOfEnemy();
        inGameUI.UpdateBullet();
    }
    #endregion

    // Dialogue
    #region
    public void DialogueEventByNumber(Dialogue _dialogue, int _dialogueNumber)
    {
        _dialogue.isDialogue = true;
        _dialogue.PrintDialogueByNumber(_dialogueNumber);
    }

    public void DialogueEventByKeyword(Dialogue _dialogue, string _keyword)
    {
        // 아직 미구현된 코드
        _dialogue.PrintDialogueByKeyword(_keyword);
    }
    #endregion

    // Util
    #region
    public void ShakeUI()
    {

    }
    #endregion
}
