using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public PlayerManager playerManager;

    [Header("Data")]
    public int ScenarioNumber; // 현재 시나리오 넘버
    public int DialogueNumber; // 현재 csv 파일의 라인 넘버
    public bool isCompletelyPrinted; // SkipAndNext 함수를 위해 필요. 텍스트의 완전한 출력 여부 판단
    public bool doNext;
    public bool isDone;

    [Header("Option")]
    public bool isAuto;
    public int printSpeed = 1;

    [Header("Existence")]
    [SerializeField]
    private bool isInGameUI;
    [SerializeField]
    private bool isLongDialogue;
    [SerializeField]
    private bool isShortDialogue;
    [SerializeField]
    private bool isPlayerManager;

    // 싱글톤 선언
    private static UIManager _instance = null;
    public static UIManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<UIManager>();
                if (_instance == null)
                {
                    GameObject go = new GameObject("UIManager_Shim");
                    _instance = go.AddComponent<UIManager>();
                }
            }
            return _instance;
        }
        set => _instance = value;
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            Destroy(this.gameObject);
        }

        InitUI();
    }
    
    void Start()
    {
        // GetManager => 싱글톤으로 선언되지 않은 매니저들을 가져옵니다.
        // 매니저들을 싱글톤으로 선언하도록 구조를 변경한다면 호출 방식을 바꿉니다.
        if (isInGameUI) { inGameUI.GetManager(); }
    }

    void Update()
    {
        if (isInGameUI && isPlayerManager) { UpdateInGameUI(); }
        
    }

    private void OnEnable()
    {
        // 씬 변경 시 이벤트 등록
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // 씬 변경 시 이벤트 해제
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        InitUI();
    }

    public void InitUI()
    {
        // 필요한 컴포넌트 가져오기
        if (GameObject.Find("InGameUI") != null)
        {
            inGameUI = GameObject.Find("InGameUI").GetComponent<InGameUI>();
            isInGameUI = true;
        }
        else
        {
            isInGameUI = false;
        }

        // **Find함수 사용 시, 하이어라키 내에서 활성화되어있지 않으면 오류 발생. (Dialogue의 Start에서 스스로 비활성화함)
        if (GameObject.Find("LongDialogue") != null)
        {
            longDialogue = GameObject.Find("LongDialogue").GetComponent<Dialogue>();
            isLongDialogue = true;
        }
        else
        {
            isLongDialogue = false;
        }

        if (GameObject.Find("ShortDialogue") != null)
        {
            shortDialogue = GameObject.Find("ShortDialogue").GetComponent<Dialogue>();
            isShortDialogue = true;
        }
        else
        {
            isShortDialogue = false;
        }

        if (GameObject.Find("PlayerManager") != null)
        {
            playerManager = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();
            isPlayerManager = true;
            
        }
        else
        {
            isPlayerManager = false;
        }
    }

    // InGameUI 기능
    #region

    private void UpdateInGameUI()
    {
        // [MVC 전환 완료] 체력과 탄약 정보는 이제 컨트롤러에서 이벤트로 자동 갱신됩니다 (Polling 제거)
        // inGameUI.PlayerHp = playerManager.Health;
        // inGameUI.PlayerMaxHp = playerManager.TotalHealth;
        // inGameUI.MaxBullet = playerManager.TotalBullet; 
        // inGameUI.CurrentBullet = playerManager.CurrentBullet; 

        // InGameUI 스크립트에서 선언된 다양한 UI Update 함수들을 실행
        // inGameUI.UpdatePlayerInfo(); // 제거 (MVC 이벤트 기반)
        inGameUI.UpdateBossInfo();
        inGameUI.UpdateGimmickInfo();
        inGameUI.UpdateNumOfEnemy();
        // inGameUI.UpdateBullet(); // 제거 (MVC 이벤트 기반)
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
