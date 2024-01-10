using System.Collections;
using UnityEngine;

public class BomberSkill : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 35f; // 전진 속도
    [SerializeField]
    private GameObject Player;
    [SerializeField]
    private GameObject warheadPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Bomb()
    {
        // 코루틴 시작
        StartCoroutine(MoveAndReturn());
    }

    IEnumerator MoveAndReturn()
    {
        // 일정 시간 동안 전진
        float elapsed = 0f;
        float timeBetweenWarheads = 0.5f;

        while (elapsed < 3.5f) // 3.5초 동안 전진 
        {
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
            elapsed += Time.deltaTime;

            // 이동 중에 0.5초마다 폭탄을 생성 
            if (elapsed % timeBetweenWarheads <= Time.deltaTime && elapsed >= 0.8f && elapsed <= 2.5f)
            {
                GameObject warhead = Instantiate(warheadPrefab, transform.position, Quaternion.identity);
            }

            yield return null;
        }

        // 목표 위치로 돌아오기
        transform.position = Player.transform.position - new Vector3(0f, -5f, 50f);
    }


   
}