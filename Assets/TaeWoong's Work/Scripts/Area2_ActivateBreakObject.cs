using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI 관련 클래스 사용

public class Area2_ActivateBreakObject : MonoBehaviour
{
    public Image progressBar; // UI의 Progress Bar
    public float fillDuration = 5f; // Bar가 차는 시간
    private float fillTimer = 0f;
    [SerializeField]private bool isPlayerInside = false;
    public BreakObject breakObject;
    public bool isActive = false;

    // Start is called before the first frame update
    void Start()
    {
        // Progress Bar를 처음에 비활성화
        progressBar.gameObject.SetActive(false);
        progressBar.fillAmount = 0f; // Progress Bar 초기화
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerInside)
        {
            fillTimer += Time.deltaTime;
            float fillAmount = fillTimer / fillDuration;
            progressBar.fillAmount = fillAmount;

            if (fillAmount >= 1f)
            {
                TriggerActivateObject();
            }
        }
    }

    private void TriggerActivateObject()
    {
        // Trigger가 On되는 로직 (다른 오브젝트에 알리기)
        breakObject.Activate(); // BreakObject 활성화
        if(!isActive)
        {
            isActive = true;
            MapSoundManager.Instance.EndProgress_Sound();
        }
        Debug.Log("Activate BreakObject But....");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // 플레이어가 들어왔을 때
        {
            if(!isPlayerInside)
            {
                MapSoundManager.Instance.StartProgress_Sound();
            }
            isPlayerInside = true;
            fillTimer = 0f; // 다시 초기화
            progressBar.gameObject.SetActive(true); // Progress Bar 활성화
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // 플레이어가 나갔을 때
        {
            isPlayerInside = false;
            fillTimer = 0f; // 초기화
            progressBar.fillAmount = 0; // Bar 초기화
            progressBar.gameObject.SetActive(false); // Progress Bar 비활성화
        }
    }
}
