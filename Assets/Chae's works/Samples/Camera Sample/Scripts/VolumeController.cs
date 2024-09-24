using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class VolumeController : MonoBehaviour
{
    // �̺�Ʈ�� ����� �� �ֵ��� UnityEvent ����
    public UnityEvent OnFadeIn = new UnityEvent();
    public UnityEvent OnFadeOut = new UnityEvent();

    private Volume volume;
    private float lerpDuration = 1.5f; // Lerp �ð��� ����

    void Awake()
    {
        // �� ������Ʈ�� �� ���� �� �ı����� �ʵ��� ����
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        // ó�� ���� �� ���� ī�޶� ã�Ƽ� Volume�� ����
        FindAndSetVolume();

        // ���� �ε�� ������ ���� ī�޶�� Volume�� �ٽ� ã���� �̺�Ʈ ���
        SceneManager.sceneLoaded += OnSceneLoaded;

        // UnityEvent�� FadeIn, FadeOut �Լ� ���
        OnFadeIn.AddListener(FadeIn);
        OnFadeOut.AddListener(FadeOut);
    }

    void OnDestroy()
    {
        // ���� ����� �� �̺�Ʈ ��� ����
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // ���� �ε�� �� ȣ��Ǵ� �޼���
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // ���� �ε�� ������ ���� ī�޶�� Volume�� �ٽ� ����
        FindAndSetVolume();
    }

    // ���̾ Main Camera�� ī�޶� ã�� Volume ������Ʈ�� �����ϴ� �Լ�
    void FindAndSetVolume()
    {
        GameObject mainCamera = GameObject.FindWithTag("MainCamera");

        if (mainCamera != null)
        {
            volume = mainCamera.GetComponent<Volume>();
            if (volume == null)
            {
                Debug.LogError("Main Camera�� Volume ������Ʈ�� �����ϴ�.");
            }
        }
        else
        {
            Debug.LogError("Main Camera�� ã�� �� �����ϴ�.");
        }
    }

    // ���̾ Main Camera�� ī�޶� ã�� �Լ�
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

    // Weight�� 0���� 1�� �ڿ������� �����ϴ� �Լ�
    public void FadeIn()
    {
        if (volume != null)
        {
            StartCoroutine(ChangeWeight(0f, 1f));
        }
        else
        {
            Debug.LogError("Volume�� �������� �ʾҽ��ϴ�.");
        }
    }

    // Weight�� 1���� 0���� �ڿ������� �����ϴ� �Լ�
    public void FadeOut()
    {
        if (volume != null)
        {
            StartCoroutine(ChangeWeight(1f, 0f));
        }
        else
        {
            Debug.LogError("Volume�� �������� �ʾҽ��ϴ�.");
        }
    }

    // Weight ���� Lerp�� �����ϴ� Coroutine
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

        // �Ϸ� �� ������ ����
        if (volume != null)
        {
            volume.weight = endValue;
        }
    }

    // FadeIn �̺�Ʈ�� Ʈ�����ϴ� �Լ�
    public void TriggerFadeIn()
    {
        OnFadeIn.Invoke();
    }

    // FadeOut �̺�Ʈ�� Ʈ�����ϴ� �Լ�
    public void TriggerFadeOut()
    {
        OnFadeOut.Invoke();
    }
}
