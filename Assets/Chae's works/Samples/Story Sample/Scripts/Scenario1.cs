using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scenario1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UIManager.instance.ScenarioNumber = 0; // �ó����� �ѹ� ����
        UIManager.instance.DialogueNumber = 10; // ���̾�α� �ѹ� ���� (��� ��������)
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PrintLongDialogue()
    {
        UIManager.instance.DialogueEventByNumber(UIManager.instance.longDialogue, UIManager.instance.DialogueNumber);
    }
}
