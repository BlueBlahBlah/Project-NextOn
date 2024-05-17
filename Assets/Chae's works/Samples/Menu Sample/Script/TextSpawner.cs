using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextSpawner : MonoBehaviour
{
    public GameObject[] textPrefabs; // ���� �ٸ� 3���� �ؽ�Ʈ �������� �����ϴ� �迭.
    public RectTransform canvasRect; // Canvas�� RectTransform�� �����ϼ���.
    public Transform TextContainer;
    public float spawnInterval = 1f; // �ؽ�Ʈ�� �����Ǵ� ����.

    void Start()
    {
        InvokeRepeating("SpawnText", 0f, spawnInterval);
    }

    void SpawnText()
    {
        // �ؽ�Ʈ �������� ������ ������ �� ���� (0 ~ 99)
        int randomValue = Random.Range(0, 100);

        // �ؽ�Ʈ �������� ������ Ȯ�� ���� ����
        int[] spawnChances = { 20, 60, 20 }; // �� �������� ���� Ȯ�� (20%, 60%, 20%)

        // ������ ���� ���� ���õ� �ؽ�Ʈ ������ ����
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

        // ���õ� �ؽ�Ʈ �������� ����
        GameObject newText = Instantiate(selectedPrefab, TextContainer);
        FallingText fallingText = newText.GetComponent<FallingText>();

        // FallingText ��ũ��Ʈ�� Canvas�� ����
        fallingText.canvasRect = canvasRect;
    }
}
