using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scenario1 : MonoBehaviour
{
    [SerializeField]
    private Scenario1UI scenario1UI;

    // Start is called before the first frame update
    void Start()
    {
        // �̱��� Ŭ������ ���� ���� ����
        if (SceneContainer.instance != null)
        {
            SceneContainer.instance.currentScene = "Scenario1 Scene";
            SceneContainer.instance.nextScene = "DemoStage";
        }
        
        if (UIManager.instance != null)
        {
            UIManager.instance.ScenarioNumber = 0; // �ó����� �ѹ� ����
            UIManager.instance.DialogueNumber = 10; // ���̾�α� �ѹ� ���� (��� ��������)
        }
        
        // Scenario1UI ��������
        if (scenario1UI == null) scenario1UI = GameObject.Find("Scenario1UI").GetComponent<Scenario1UI>();

        // StartCoroutine("StartScenario1");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PrintLongDialogue()
    {
        UIManager.instance.DialogueEventByNumber(UIManager.instance.longDialogue
            , UIManager.instance.DialogueNumber);
    }

    public void ChangeScene()
    {
        LoadingManager.ToLoadScene();
    }

    // test
    public void TestCoroutine()
    {
        StartCoroutine("StartScenario1");
    }


    // Coroutine
    #region
    IEnumerator StartScenario1()
    {
        // Scenario1�� ��ü���� ������ ��Ÿ�� �ڷ�ƾ�Դϴ�.

        // Ű���� ���� ���
        // yield return new WaitForSeconds(5f);

        PrintLongDialogue();
        yield return new WaitForSeconds(7f); // ��� ��� ����� �������� ������ ��, �ڷ�ƾ ���� ���� �ʿ�
        scenario1UI.SetLittleDark();
        yield return new WaitForSeconds(0.1f);
        scenario1UI.SetLight();
        yield return new WaitForSeconds(0.2f);
        scenario1UI.SetLittleDark();
        yield return new WaitForSeconds(0.1f);
        scenario1UI.SetLight();
        yield return new WaitForSeconds(3f);
        PrintLongDialogue();
        scenario1UI.StopCharacter();
        yield return null;
    }

    IEnumerator Scenario1Sound()
    {
        yield break;
    }
    #endregion
}
