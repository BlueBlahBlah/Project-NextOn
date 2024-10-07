using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class VolumeController : MonoBehaviour
{
    // 이벤트를 사용할 수 있도록 UnityEvent 정의
    public UnityEvent OnFadeIn = new UnityEvent();
    public UnityEvent OnFadeOut = new UnityEvent();

    private Volume volume;
    private float lerpDuration = 1.5f; // Lerp 시간을 설정

    void Awake()
    {
        // 이 오브젝트가 씬 변경 시 파괴되지 않도록 설정
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        // 처음 시작 시 메인 카메라를 찾아서 Volume을 설정
        FindAndSetVolume();

        // 씬이 로드될 때마다 메인 카메라와 Volume을 다시 찾도록 이벤트 등록
        SceneManager.sceneLoaded += OnSceneLoaded;

        // UnityEvent에 FadeIn, FadeOut 함수 등록
        OnFadeIn.AddListener(FadeIn);
        OnFadeOut.AddListener(FadeOut);
    }

    void OnDestroy()
    {
        // 씬이 변경될 때 이벤트 등록 해제
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // 씬이 로드될 때 호출되는 메서드
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 씬이 로드될 때마다 메인 카메라와 Volume을 다시 설정
        FindAndSetVolume();
    }

    // 레이어가 Main Camera인 카메라를 찾아 Volume 컴포넌트를 설정하는 함수
    void FindAndSetVolume()
    {
        GameObject mainCamera = GameObject.FindWithTag("MainCamera");

        if (mainCamera != null)
        {
            volume = mainCamera.GetComponent<Volume>();
            if (volume == null)
            {
                Debug.LogError("Main Camera에 Volume 컴포넌트가 없습니다.");
            }
        }
        else
        {
            Debug.LogError("Main Camera를 찾을 수 없습니다.");
        }
    }

    // 레이어가 Main Camera인 카메라를 찾는 함수
    GameObject FindMainCamera()
    {
        GameObject[] cameras = GameObject.FindGameObjectsWithTag("MainCamera");
        foreach (GameObject cam in cameras)
        {
            if (cam.layer == LayerMask.NameToLayer("Main Camera"))
            {
                return cam;
            }
        }
        return null;
    }

    // Weight를 0에서 1로 자연스럽게 변경하는 함수
    public void FadeIn()
    {
        if (volume != null)
        {
            StartCoroutine(ChangeWeight(0f, 1f));
        }
        else
        {
            Debug.LogError("Volume이 설정되지 않았습니다.");
        }
    }

    // Weight를 1에서 0으로 자연스럽게 변경하는 함수
    public void FadeOut()
    {
        if (volume != null)
        {
            StartCoroutine(ChangeWeight(1f, 0f));
        }
        else
        {
            Debug.LogError("Volume이 설정되지 않았습니다.");
        }
    }

    // Weight 값을 Lerp로 변경하는 Coroutine
    private IEnumerator ChangeWeight(float startValue, float endValue)
    {
        float elapsed = 0f;

        while (elapsed < lerpDuration)
        {
            elapsed += Time.deltaTime;
            if (volume != null)
            {
                volume.weight = Mathf.Lerp(startValue, endValue, elapsed / lerpDuration);
            }
            yield return null;
        }

        // 완료 후 최종값 적용
        if (volume != null)
        {
            volume.weight = endValue;
        }
    }

    // FadeIn 이벤트를 트리거하는 함수
    public void TriggerFadeIn()
    {
        OnFadeIn.Invoke();
    }

    // FadeOut 이벤트를 트리거하는 함수
    public void TriggerFadeOut()
    {
        OnFadeOut.Invoke();
    }
}
