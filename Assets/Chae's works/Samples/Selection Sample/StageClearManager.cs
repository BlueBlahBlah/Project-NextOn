using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageClearManager : MonoBehaviour
{
    // 싱글톤 인스턴스
    public static StageClearManager Instance { get; private set; }

    // Stage 클리어 여부 저장
    private bool[] stageClearStatus = new bool[4]; // 0: Stage1, 1: Stage2, 2: Stage3, 3: Stage4

    private void Awake()
    {
        // 싱글톤 패턴 구현
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬이 변경되어도 객체 유지

        }
        else
        {
            Destroy(gameObject); // 이미 인스턴스가 있다면 파괴
        }
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

    // 씬이 로드되었을 때 호출되는 메서드
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Selection Scene")
        {
            CheckStageClearStatus();
        }
    }

    // 각 Stage 클리어 여부 확인 후 함수 호출 (여기서는 Debug 로그 출력)
    private void CheckStageClearStatus()
    {
        for (int i = 0; i < stageClearStatus.Length; i++)
        {
            if (stageClearStatus[i])
            {
                Debug.Log($"Stage {i + 1} 클리어 상태: 클리어됨");
                ExecuteStageClearFunction(i + 1);
            }
            else
            {
                Debug.Log($"Stage {i + 1} 클리어 상태: 클리어되지 않음");
            }
        }
    }

    // 각 Stage 가 클리어 되었다면 실행할 함수
    private void ExecuteStageClearFunction(int stageNumber)
    {
        Debug.Log($"Stage {stageNumber} 클리어 후 실행할 함수 호출");

        // "Scene Change Object - i" 라는 이름의 오브젝트를 찾음
        string objectName = $"Scene Change Object - {stageNumber}";
        GameObject targetObject = GameObject.Find(objectName);

        if (targetObject != null)
        {
            // 오브젝트가 존재하면 파괴할 것을 가정하고 로그 출력
            Debug.Log($"{objectName} 오브젝트를 파괴할 예정입니다.");
            Destroy(targetObject);
            // 실제 파괴 코드: Destroy(targetObject);  -> 추후에 실제 파괴를 적용
        }
        else
        {
            Debug.Log($"{objectName} 오브젝트가 존재하지 않습니다.");
        }
    }


    // Stage 클리어 상태 변경 메서드 (필요 시 외부에서 호출)
    public void SetStageClear(int stageNumber, bool isClear = true)
    {
        if (stageNumber >= 1 && stageNumber <= 4)
        {
            stageClearStatus[stageNumber - 1] = isClear;
            Debug.Log($"Stage {stageNumber} 클리어 상태가 {isClear}로 설정됨");
        }
        else
        {
            Debug.LogWarning("잘못된 Stage 번호");
        }
    }
}

