using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StreamOfEdge : MonoBehaviour
{
    [SerializeField] private GameObject Sphere1;
    [SerializeField] private GameObject Sphere2;

    [SerializeField] private GameObject[] ball;

    [SerializeField] private float moveSpeed = 50; // 공의 이동 속도

    private Transform[] currentTarget; // 현재 목표 지점

    private bool[] movingTowardsSphere2; // 각 공이 Sphere1에서 Sphere2로 이동 중인지 여부

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 10f);

        // 초기 목표지점 설정
        //currentTarget = Sphere2.transform;
        currentTarget = new Transform[ball.Length];
        for (int i = 0; i < ball.Length/2; i++)
        {
            currentTarget[i] = Sphere2.transform;
        }
        for (int i = ball.Length/2; i < ball.Length; i++)
        {
            currentTarget[i] = Sphere1.transform;
        }

        // movingTowardsSphere2 배열 초기화
        movingTowardsSphere2 = new bool[ball.Length];
        for (int i = 0; i < ball.Length/2; i++)
        {
            movingTowardsSphere2[i] = true;
        }
        for (int i = ball.Length/2; i < ball.Length; i++)
        {
            movingTowardsSphere2[i] = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        MoveBall();
    }
    // 공들을 이동시키는 메서드

    void MoveBall()
    {
        // 각 공들에 대해 순회
        for (int i = 0; i < ball.Length; i++)
        {
            //moveSpeed = Random.Range(20, 35);
            // 공을 목표 지점 방향으로 이동
            ball[i].transform.position = Vector3.MoveTowards(ball[i].transform.position, currentTarget[i].position, moveSpeed * Time.deltaTime);

            // 공이 목표 지점에 도착했는지 확인
            if (ball[i].transform.position == currentTarget[i].position)
            {
                //높이조정
                Vector3 newPosition = ball[i].transform.position; // 현재 위치 복사
                newPosition.y = Random.Range(0f, 1f); // y 좌표를 랜덤으로 변경
                ball[i].transform.position = newPosition; // 새로운 위치 할당
                
                // Sphere1에서 Sphere2로 이동 중인 경우
                if (movingTowardsSphere2[i])
                {
                    // 현재 목표 지점을 Sphere2로 변경
                    currentTarget[i] = Sphere2.transform;
                    // 이동 방향을 Sphere2에서 Sphere1로 변경
                    movingTowardsSphere2[i] = false;
                }
                // Sphere2에서 Sphere1로 이동 중인 경우
                else
                {
                    // 현재 목표 지점을 Sphere1로 변경
                    currentTarget[i] = Sphere1.transform;
                    // 이동 방향을 Sphere1에서 Sphere2로 변경
                    movingTowardsSphere2[i] = true;
                }
            }
        }
    }
}
