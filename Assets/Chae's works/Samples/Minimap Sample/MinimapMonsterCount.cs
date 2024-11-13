using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MinimapMonsterCount : MonoBehaviour
{
    [Header("EnemyDetect")]
    private GameObject player;
    public float detectionRadius = 50f; // ������ ������ ������
    public LayerMask enemyLayerMask;   // ������ Enemy ���̾�
    [SerializeField]
    private TextMeshProUGUI enemyCountText;
    

    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
            if (player == null)
            {
                Debug.LogWarning("Player �±׸� ���� ������Ʈ�� ���� �����ϴ�.");
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
        // LayerMask�� �̿��� Ư�� ���̾��� ������Ʈ�� �˻�
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius, enemyLayerMask);

        // ������ ������Ʈ�� ���� ��
        int enemyCount = hitColliders.Length;

        // ����� ����ϰų� ���ϴ� ������ ����
        enemyCountText.text = enemyCount.ToString();
    }
}
