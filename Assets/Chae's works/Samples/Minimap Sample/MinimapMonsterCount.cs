using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MinimapMonsterCount : MonoBehaviour
{
    [Header("EnemyDetect")]
    private GameObject player;
    public float detectionRadius = 50f; // 감지할 범위의 반지름
    public LayerMask enemyLayerMask;   // 감지할 Enemy 레이어
    [SerializeField]
    private TextMeshProUGUI enemyCountText;

    private GameObject[] enemies;
    private int TotalMonsters;
    

    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
            if (player == null)
            {
                Debug.LogWarning("Player 태그를 가진 오브젝트가 씬에 없습니다.");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        DetectEnemies();
    }

    void DetectEnemies()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        TotalMonsters = enemies.Length;

        // 결과를 출력하거나 원하는 동작을 수행
        enemyCountText.text = TotalMonsters.ToString();
    }
}
