using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenario : MonoBehaviour
{
    public UIManager UIManager;

    public AudioSource BGM;
    public AudioSource MidBGM;
    public AudioSource FinalBGM;
    [Header("Scenario")]
    public GameObject Scenario_1;
    public GameObject Scenario_2;
    public GameObject Scenario_3;
    public GameObject Scenario_4;

    private int Playing_Scenario;
    public int playing_Scenario
    {
        get { return Playing_Scenario; }
        set { Playing_Scenario = value; }
    }
    [Header("Manager Edit")]
    public PlayerManager playerManager;
    public SceneReloader sceneReloader;
    public GameObject Dialogue;
    public GameObject Player;
    public bool is_End;
    public FixedJoystick joystick;
    public GameObject GameClearPanel;
    // 싱글톤 선언
    #region
    public static Scenario instance;

    private void Awake()
    {
        if (instance == null) // instance가 null. 즉, 시스템상에 존재하고 있지 않을 때
        {
            instance = this; // 내 자신을 instance로 넣어줍니다.
            DontDestroyOnLoad(gameObject); // OnLoad(씬이 로드 되었을 때) 자신을 파괴하지 않고 유지

#if UNITY_EDITOR
            ResetPlayerPrefsOnStart();
#endif

            LoadScenarioProgress();
        }
        else
        {
            if (instance != this) // instance가 내가 아니라면 이미 instance가 하나 존재하고 있다는 의미
            {
                Destroy(this.gameObject);
            }
        }
    }

    #endregion
    void Start()
    {
        BGM.mute = false;
        MidBGM.mute = true;
        FinalBGM.mute = true;
        UIManager = FindAnyObjectByType<UIManager>();
        is_End = false;
    }
    // 에디터 전용 초기화 코드
#if UNITY_EDITOR
    private void ResetPlayerPrefsOnStart()
    {
        PlayerPrefs.DeleteAll(); // PlayerPrefs 초기화
        PlayerPrefs.Save();      // 변경 사항을 저장
    }
#endif
    void Update()
    {
        if (playerManager.Death) 
        {
            joystick.enabled = false;
            Die(); 
        }
        else if (is_End)
        {
            //UI 띄우기, 로그창 띄우기
            Debug.Log("대사 end");
            BGM.mute = true;
            FinalBGM.mute = true;
            MidBGM.mute = true;
        }
    }
    public IEnumerator Scenario1Start()
    {
        SaveScenarioProgress();
        Debug.Log($"대사 {playing_Scenario} 시나리오 시작");

        Scenario_1.SetActive(true);
        yield return null;
    }

    public IEnumerator Scenario2Start()
    {
        SaveScenarioProgress();
        Debug.Log($"대사 {playing_Scenario} 시나리오 시작");

        StopCoroutine(Scenario1Start());
        Scenario_1.SetActive(false);
        Scenario_2.SetActive(true);
        yield return null;
    }

    public IEnumerator Scenario3Start()
    {
        SaveScenarioProgress();
        Debug.Log($"대사 {playing_Scenario} 시나리오 시작");

        StopCoroutine(Scenario2Start());
        Scenario_2.SetActive(false);
        Scenario_3.SetActive(true);
        yield return null;
    }

    public IEnumerator Scenario4Start() 
    {
        SaveScenarioProgress();
        Debug.Log($"대사 {playing_Scenario} 시나리오 시작");

        StopCoroutine(Scenario3Start());
        Scenario_3.SetActive(false);
        Scenario_4.SetActive(true);
        yield return null;
    }

    public IEnumerator END()
    {
        StopCoroutine(Scenario4Start());
        Scenario_4.SetActive(false);
        is_End = true;
        yield return null;
    }
    public void SaveScenarioProgress()
    {
        PlayerPrefs.SetInt("PlayingScenario", playing_Scenario);
        PlayerPrefs.Save(); // 명시적으로 저장
    }

    public void LoadScenarioProgress()
    {
        // 저장된 시나리오 진행 상황 로드
        playing_Scenario = PlayerPrefs.GetInt("PlayingScenario", 1); // 기본값은 1
    }
    public void Die() => StartCoroutine(sceneReloader.RestartScene());
}
