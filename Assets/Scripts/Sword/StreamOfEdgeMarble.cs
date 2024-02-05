using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreamOfEdgeMarble : MonoBehaviour
{
    [SerializeField] private float rate;

    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        rate = 3f;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > rate)
        {
            timer = 0;
            Vector3 newPosition = transform.position; // 현재 위치 복사
            newPosition.y = Random.Range(0f, 1f); // y 좌표를 랜덤으로 변경
            transform.position = newPosition; // 새로운 위치 할당
        }
    }
}