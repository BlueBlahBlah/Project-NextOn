using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI 관련 클래스 사용

public class Area3_TakeArgument : MonoBehaviour
{
    public Image progressBar; // UI의 Progress Bar
    public float fillDuration = 5f; // Bar가 차는 시간
    private float fillTimer = 0f;
    public GameObject[] objectsToAwake; // 활성화할 몬스터 배열
    public Area3_GetArgument GetPlace; // 인자 전달 장소 참조
    public GameObject ActiveObj; // 인자 활성화 표시
    public GameObject EmptyObj; // 빈 인자 비활성화
    [SerializeField] private bool isPlayerInside = false; // 플레이어 진입 여부
    // public bool isGetArg = false; // 인자 획득 여부
    public bool isTakeArg = false; // 인자 전달 여부

    // Start is called before the first frame update
    void Start()
    {
        
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
                if(GetPlace.isGetArg)
                {
                    isTakeArg = true; // 플레이어가 인자 획득
                }
                else
                {
                    Debug.Log("플레이어가 인자를 가지고 있지 않습니다. 인자를 가지고 와주세요");
                }

                
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // 플레이어가 들어왔을 때
        {
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
