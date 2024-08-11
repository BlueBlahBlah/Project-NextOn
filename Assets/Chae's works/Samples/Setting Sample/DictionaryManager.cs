using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DictionaryManager : MonoBehaviour
{
    public GameObject[] detailPanels; // 상세 정보를 표시할 패널 배열
    public Button[] tabButtons; // 탭 버튼 배열

    private void Start()
    {
        // 각 탭 버튼에 클릭 이벤트 리스너를 등록합니다.
        for (int i = 0; i < tabButtons.Length; i++)
        {
            int index = i; // 로컬 변수로 인덱스를 캡처
            tabButtons[i].onClick.AddListener(() => ShowDetail(index));
        }

        // 초기 상태로 첫 번째 상세 정보를 표시합니다.
        ShowDetail(0);
    }

    private void ShowDetail(int index)
    {
        // 모든 상세 정보 패널을 숨깁니다.
        foreach (var panel in detailPanels)
        {
            panel.SetActive(false);
        }

        // 선택된 탭에 해당하는 상세 정보 패널을 표시합니다.
        if (index >= 0 && index < detailPanels.Length)
        {
            detailPanels[index].SetActive(true);
        }
    }
}
