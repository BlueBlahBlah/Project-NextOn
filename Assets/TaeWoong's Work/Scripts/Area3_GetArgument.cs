using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI 관련 클래스 사용

public class Area3_GetArgument : MonoBehaviour
{
    public Image progressBar; // UI의 Progress Bar
    public float fillDuration = 5f; // Bar가 차는 시간
    private float fillTimer = 0f;
    public MobSpawner[] monsterSpawner; // MonsterSpawner 참조
    public Area3_TakeArgument TakePlace; // 인자 전달 장소 참조
    [SerializeField] private bool isPlayerInside = false; // 플레이어 진입 여부
    public bool isGetArg = false; // 인자 획득 여부
    // public bool isTakeArg = false; // 인자 전달 여부

    // Start is called before the first frame update
    void Start()
    {
        progressBar.gameObject.SetActive(false);
        progressBar.fillAmount = 0f; // Progress Bar 초기화
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerInside && !isGetArg)
        {
            fillTimer += Time.deltaTime;
            float fillAmount = fillTimer / fillDuration;
            progressBar.fillAmount = fillAmount;
    
            if (fillAmount >= 1f)
            {
                isGetArg = true; // 플레이어가 인자 획득

                TriggerMonsterSpawn(); // 몬스터 스폰
            }
        }
    }

    private void TriggerMonsterSpawn()
    {
        MapSoundManager.Instance.EndProgress_Sound();
        // Trigger가 On되는 로직 (다른 오브젝트에 알리기)
         foreach (MobSpawner spawner in monsterSpawner)
        {
            spawner.SpawnMonster(); // 각 MobSpawner의 SpawnMonster 메서드 호출
        }
        Debug.Log("Repair Work in Progress");
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
