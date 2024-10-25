using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingObject : MonoBehaviour
{
    public float floatAmplitude = 0.5f; // 부유하는 높이 진폭
    public float floatFrequency = 1.0f; // 부유하는 속도
    public float timeElapsed = 0.0f; // 경과 시간

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;
        Floating();
    }

    public void Floating()
    {

        float floatOffset = Mathf.Sin(timeElapsed * floatFrequency) * floatAmplitude;
        Vector3 newPosition = transform.position;
        newPosition.y =  1.0f + floatOffset;
        transform.position = newPosition;
    }
}
