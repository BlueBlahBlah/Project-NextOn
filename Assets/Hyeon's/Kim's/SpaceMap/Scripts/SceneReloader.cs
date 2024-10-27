using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneReloader : MonoBehaviour
{
    [SerializeField] private Scenario scenario;

    public IEnumerator RestartScene()
    {
        yield return new WaitForSeconds(3);
        // 현재 활성화된 씬의 이름을 가져옵니다.
        string sceneName = SceneManager.GetActiveScene().name;
        // 현재 씬을 다시 로드합니다.
        SceneManager.LoadScene(sceneName);
        // Scenario.instance가 존재하는지 확인하고, 진행 상황을 불러옴
        if (Scenario.instance != null)
        {
            Scenario.instance.LoadScenarioProgress();
        }
        yield return null;
    }

    void Start()
    {
        Debug.Log("대사 리로더 시작");
        int scenarioNumber = Scenario.instance.playing_Scenario;
        StartScenario(scenarioNumber);
    }

    void StartScenario(int scenarioNumber)
    {
        scenario.Scenario_1.SetActive(false);
        scenario.Scenario_2.SetActive(false);
        scenario.Scenario_3.SetActive(false);
        scenario.Scenario_4.SetActive(false);

        scenario.JoyStick.enabled =true;

        Debug.Log($"대사  {scenarioNumber}");
        // 시나리오 번호에 따라 다른 로직 실행
        switch (scenarioNumber)
        {
            case 1:
                Scenario.instance.StartCoroutine(scenario.Scenario1Start());
                scenario.Player.transform.position =
                    scenario.Scenario_1.GetComponent<Scenario_1>().Respawn.transform.position;
                break;
            case 2:
                Scenario.instance.StartCoroutine(scenario.Scenario2Start());
                scenario.Player.transform.position =
                    scenario.Scenario_2.GetComponent<Scenario_2>().Respawn.transform.position;
                break;
            case 3:
                Scenario.instance.StartCoroutine(scenario.Scenario3Start());
                scenario.Player.transform.position =
                    scenario.Scenario_3.GetComponent<Scenario_3>().Respawn.transform.position;
                break;
            case 4:
                Scenario.instance.StartCoroutine(scenario.Scenario4Start());
                scenario.Player.transform.position =
                    scenario.Scenario_1.GetComponent<Scenario_1>().Respawn.transform.position;
                break;
        }
    }
}
