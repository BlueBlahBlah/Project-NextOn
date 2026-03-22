using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StreamOfEdge : MonoBehaviour
{
    [SerializeField] private GameObject Sphere1;
    [SerializeField] private GameObject Sphere2;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 10f);

        // 초기 목표지점 설정
        //currentTarget = Sphere2.transform;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // 공들을 이동시키는 메서드

    
}
