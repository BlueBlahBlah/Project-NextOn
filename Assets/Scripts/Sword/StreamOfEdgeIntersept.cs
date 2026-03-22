using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class StreamOfEdgeIntersept : MonoBehaviour
{
    [SerializeField] private GameObject destination;
    private Rigidbody rigidbody;
    // 이동에 사용될 속도 변수
    public float moveSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        toMove();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void toMove()
    {
        // 코루틴 시작
        StartCoroutine(MoveToDestination(destination.transform.position));
    }

    IEnumerator MoveToDestination(Vector3 destination)
    {
        // 현재 위치부터 목적지까지 이동하는 while 루프
        while (transform.position != destination)
        {
            // 현재 위치에서 목적지 방향으로 이동
            transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);
            yield return null; // 한 프레임 대기
        }
        
    }
}