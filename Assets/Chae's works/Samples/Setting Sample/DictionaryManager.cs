using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DictionaryManager : MonoBehaviour
{
    public GameObject[] detailPanels; // �� ������ ǥ���� �г� �迭
    public Button[] tabButtons; // �� ��ư �迭

    private void Start()
    {
        // �� �� ��ư�� Ŭ�� �̺�Ʈ �����ʸ� ����մϴ�.
        for (int i = 0; i < tabButtons.Length; i++)
        {
            int index = i; // ���� ������ �ε����� ĸó
            tabButtons[i].onClick.AddListener(() => ShowDetail(index));
        }

        // �ʱ� ���·� ù ��° �� ������ ǥ���մϴ�.
        ShowDetail(0);
    }

    private void ShowDetail(int index)
    {
        // ��� �� ���� �г��� ����ϴ�.
        foreach (var panel in detailPanels)
        {
            panel.SetActive(false);
        }

        // ���õ� �ǿ� �ش��ϴ� �� ���� �г��� ǥ���մϴ�.
        if (index >= 0 && index < detailPanels.Length)
        {
            detailPanels[index].SetActive(true);
        }
    }
}
