using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextSpawner : MonoBehaviour
{
    public GameObject[] textPrefabs; // 서로 다른 3개의 텍스트 프리팹을 저장하는 배열.
    public RectTransform canvasRect; // Canvas의 RectTransform을 연결하세요.
    public Transform TextContainer;
    public float spawnInterval = 1f; // 텍스트가 생성되는 간격.

    void Start()
    {
        InvokeRepeating("SpawnText", 0f, spawnInterval);
    }

    void SpawnText()
    {
        // 텍스트 프리팹을 선택할 랜덤한 값 생성 (0 ~ 99)
        int randomValue = Random.Range(0, 100);

        // 텍스트 프리팹을 선택할 확률 범위 설정
        int[] spawnChances = { 20, 60, 20 }; // 각 프리팹의 스폰 확률 (20%, 60%, 20%)

        // 랜덤한 값에 따라 선택된 텍스트 프리팹 결정
        int cumulativeChance = 0;
        GameObject selectedPrefab = null;
        for (int i = 0; i < textPrefabs.Length; i++)
        {
            cumulativeChance += spawnChances[i];
            if (randomValue < cumulativeChance)
            {
                selectedPrefab = textPrefabs[i];
                break;
            }
        }

        // 선택된 텍스트 프리팹을 스폰
        GameObject newText = Instantiate(selectedPrefab, TextContainer);
        FallingText fallingText = newText.GetComponent<FallingText>();

        // FallingText 스크립트에 Canvas를 설정
        fallingText.canvasRect = canvasRect;
    }
}
