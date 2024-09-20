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
            SceneContainer.instance.nextScene = "Selection Scene";
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

    public void PrintLongDialogue()
    {
        UIManager.instance.DialogueEventByNumber(UIManager.instance.longDialogue , UIManager.instance.DialogueNumber);
    }

    public void ChangeScene() { LoadingManager.ToLoadScene(); }

    // test
    public void TestCoroutine() { StartCoroutine("StartScenario1"); }


    // Coroutine
    #region
    IEnumerator StartScenario1()
    {
        // Scenario1�� ��ü���� ������ ��Ÿ�� �ڷ�ƾ�Դϴ�.
        // �� ������ �����ϰ� �ֱ� ������ Ư¡���� ���⸶�� �ּ��� �ۼ��մϴ�.

        // Ű���� ���� ���
        // yield return new WaitForSeconds(5f);


        // ��� ���1
        PrintLongDialogue();
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine("RunLoopUntilDone");
        yield return new WaitForSeconds(2f);

        // ������ ����
        scenario1UI.SetLittleDark();
        yield return new WaitForSeconds(0.1f);
        scenario1UI.SetLight();
        yield return new WaitForSeconds(0.2f);
        scenario1UI.SetLittleDark();
        yield return new WaitForSeconds(0.1f);
        scenario1UI.SetLight();
        yield return new WaitForSeconds(2f);

        // ��� ���2 �� ĳ���� ��Ʈ ����
        PrintLongDialogue();
        scenario1UI.StopCharacter();
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine("RunLoopUntilDone");
        yield return new WaitForSeconds(1f);
        scenario1UI.SetLittleDark();
        yield return new WaitForSeconds(0.5f);
        scenario1UI.SetLight();
        yield return new WaitForSeconds(1f);
        scenario1UI.SetTotallyDark();
        yield return new WaitForSeconds(2.5f);

        PrintLongDialogue();
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine("RunLoopUntilDone");

    }

    IEnumerator Scenario1Sound()
    {
        yield break;
    }

    IEnumerator RunLoopUntilDone()
    {
        while (true)
        {
            if (!UIManager.instance.isDone) 
            {
                yield return null;
            }
                
            else
            {
                yield break;
            }
        }
    }
    #endregion
}
