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
        // ���� Ȱ��ȭ�� ���� �̸��� �����ɴϴ�.
        string sceneName = SceneManager.GetActiveScene().name;
        // ���� ���� �ٽ� �ε��մϴ�.
        SceneManager.LoadScene(sceneName);
        // Scenario.instance�� �����ϴ��� Ȯ���ϰ�, ���� ��Ȳ�� �ҷ���
        if (Scenario.instance != null)
        {
            Scenario.instance.LoadScenarioProgress();
        }
        yield return null;
    }

    void Start()
    {
        Debug.Log("��� ���δ� ����");
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

        Debug.Log($"���  {scenarioNumber}");
        // �ó����� ��ȣ�� ���� �ٸ� ���� ����
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
