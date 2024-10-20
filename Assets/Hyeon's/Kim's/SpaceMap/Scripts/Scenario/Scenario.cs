using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenario : MonoBehaviour
{
    public UIManager UIManager;
    

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
    public PlayerManager playerManager;
    public SceneReloader sceneReloader;
    public GameObject Dialogue;
    public bool is_End;
    // �̱��� ����
    #region
    public static Scenario instance;

    private void Awake()
    {
        if (instance == null) // instance�� null. ��, �ý��ۻ� �����ϰ� ���� ���� ��
        {
            instance = this; // �� �ڽ��� instance�� �־��ݴϴ�.
            DontDestroyOnLoad(gameObject); // OnLoad(���� �ε� �Ǿ��� ��) �ڽ��� �ı����� �ʰ� ����

#if UNITY_EDITOR
            ResetPlayerPrefsOnStart();
#endif

            LoadScenarioProgress();
        }
        else
        {
            if (instance != this) // instance�� ���� �ƴ϶�� �̹� instance�� �ϳ� �����ϰ� �ִٴ� �ǹ�
            {
                Destroy(this.gameObject);
            }
        }
    }

    #endregion
    void Start()
    {
        UIManager = FindAnyObjectByType<UIManager>();
        Debug.Log("��� �ó����� ����");
        is_End = false;
    }
    // ������ ���� �ʱ�ȭ �ڵ�
#if UNITY_EDITOR
    private void ResetPlayerPrefsOnStart()
    {
        PlayerPrefs.DeleteAll(); // PlayerPrefs �ʱ�ȭ
        PlayerPrefs.Save();      // ���� ������ ����
        Debug.Log("PlayerPrefs�� Unity �����Ϳ��� �ʱ�ȭ�Ǿ����ϴ�.");
    }
#endif
    void Update()
    {
        if (playerManager.Death) Die();
        else if (is_End)
        {
            //UI ����, �α�â ����
            Debug.Log("end");
        }
    }
    public IEnumerator Scenario1Start()
    {
        //Playing_Scenario = 1;
        SaveScenarioProgress();
        Debug.Log($"��� {playing_Scenario} �ó����� ����");

        Scenario_1.SetActive(true);
        yield return null;
    }

    public IEnumerator Scenario2Start()
    {
        //playing_Scenario = 2;
        SaveScenarioProgress();
        Debug.Log($"��� {playing_Scenario} �ó����� ����");

        StopCoroutine(Scenario1Start());
        Scenario_1.SetActive(false);
        Scenario_2.SetActive(true);
        yield return null;
    }

    public IEnumerator Scenario3Start()
    {
        //playing_Scenario = 3;
        SaveScenarioProgress();
        Debug.Log($"��� {playing_Scenario} �ó����� ����");

        StopCoroutine(Scenario2Start());
        Scenario_2.SetActive(false);
        Scenario_3.SetActive(true);
        yield return null;
    }

    public IEnumerator Scenario4Start() 
    {
        //playing_Scenario = 4;
        SaveScenarioProgress();
        Debug.Log($"��� {playing_Scenario} �ó����� ����");

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
        // Playing_Scenario�� ���� PlayerPrefs�� ����
        PlayerPrefs.SetInt("PlayingScenario", playing_Scenario);
        PlayerPrefs.Save(); // ��������� ����
    }

    public void LoadScenarioProgress()
    {
        // ����� �ó����� ���� ��Ȳ �ε�
        playing_Scenario = PlayerPrefs.GetInt("PlayingScenario", 1); // �⺻���� 1
    }
    public void Die() => StartCoroutine(sceneReloader.RestartScene());
}
