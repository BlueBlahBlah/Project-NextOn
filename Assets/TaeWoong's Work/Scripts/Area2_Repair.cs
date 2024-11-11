using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI 관련 클래스 사용

public class RepairArea : MonoBehaviour
{
    public Image progressBar; // UI의 Progress Bar
    public float fillDuration = 5f; // Bar가 차는 시간
    private float fillTimer = 0f;
    public bool isRepaired = false;
    [SerializeField] private bool isPlayerInside = false;
    public MobSpawner monsterSpawner; // MonsterSpawner를 참조
    public GameObject showProgress; // Progress Bar가 가득 찼을 때 활성화할 오브젝트
    public GameObject showComplete; // 몬스터 처치 후 활성화할 오브젝트
    public GameObject[] objectsToDisable; // 비활성화할 오브젝트 배열
    private ParticleSystem progressParticleSystem; // showProgress의 ParticleSystem
    [SerializeField] private bool isProgress = false; // Progress 활성화 여부

    void Start()
    {
        // Progress Bar를 처음에 비활성화
        progressBar.gameObject.SetActive(false);
        progressBar.fillAmount = 0f; // Progress Bar 초기화
        showProgress.SetActive(false); // showProgress 초기화
        showComplete.SetActive(false); // showComplete 초기화
        
        // showProgress의 ParticleSystem 초기화
        progressParticleSystem = showProgress.GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if(!isProgress)
        {
            if (isPlayerInside && !isRepaired) // 수리가 완료되지 않았을 때만 진행
            {
                fillTimer += Time.deltaTime;
                float fillAmount = fillTimer / fillDuration;
                progressBar.fillAmount = fillAmount;
    
                if (fillAmount >= 1f)
                {
                    TriggerMonsterSpawn();
                    ShowProgress(); // showProgress 활성화 및 재생
                }
            }
        }

        // isProgress가 true일 때 파티클 재생
        if (isProgress && progressParticleSystem != null)
        {
            if (!progressParticleSystem.isPlaying)
            {
                progressParticleSystem.Play(); // 파티클 시스템 재생
                CheckMonstersStatus();
            }
        }

    }

    private void ShowProgress()
    {
        showProgress.SetActive(true); // showProgress 활성화
        isProgress = true; // Progress 활성화
    }

    private void TriggerMonsterSpawn()
    {
        // Trigger가 On되는 로직 (다른 오브젝트에 알리기)
        monsterSpawner.SpawnMonster(); // 몬스터 스폰
        // MapSoundManager.Instance.EndProgress_Sound();
        SoundManager.instance.PlayEffectSound("활설화End");
        Debug.Log("Repair Work in Progress");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // 플레이어가 들어왔을 때
        {
            if(!isPlayerInside)
            {
                // MapSoundManager.Instance.StartProgress_Sound();
                SoundManager.instance.PlayEffectSound("활성화Start");
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

    public void CheckMonstersStatus()
    {
        // 몬스터가 모두 처치되었는지 확인하는 메서드
        if (monsterSpawner.GetSpawnedMonsterCount() == 0)
        {
            showProgress.SetActive(false); // showProgress 비활성화
            showComplete.SetActive(true); // showComplete 활성화
            isRepaired = true;
            
            // 비활성화할 오브젝트들 비활성화
            foreach (GameObject obj in objectsToDisable)
            {
                obj.SetActive(false);
            }

            // 파티클 시스템 비활성화
            isProgress = false; // Progress 비활성화
            if (progressParticleSystem != null)
            {
                progressParticleSystem.Stop(); // 파티클 시스템 정지
            }

            Debug.Log("All monsters defeated!");
        }
    }
}
