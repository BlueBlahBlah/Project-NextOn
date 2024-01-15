using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class HelicopterMinigunParticle : MonoBehaviour
{
    public GameObject rangeObject;
    public GameObject Particle;
    [SerializeField] private GameObject Helicopter;
    BoxCollider rangeCollider;
    private Coroutine respawnCoroutine;  // 코루틴을 저장할 변수 추가


    private void Awake()
    {
        rangeCollider = rangeObject.GetComponent<BoxCollider>();
        
    }

    private void Start()
    {
        //StartCoroutine(RandomRespawn_Coroutine());
        
    }

    public void ParticleStop()
    {
        if (respawnCoroutine != null)
        {
            StopCoroutine(respawnCoroutine);
            respawnCoroutine = null;  // 코루틴이 정지되면 변수를 null로 설정
        }
    }

    public void ParticleStart()
    {
        respawnCoroutine = StartCoroutine(RandomRespawn_Coroutine());
    }

    private void Update()
    {
        //이펙트 장판의 위치
        //transform.position = Helicopter.transform.position - new Vector3(20f, 7f, 0f);
    }

    IEnumerator RandomRespawn_Coroutine()
    {
        while (true)
        {
            yield return WaitFrames(5); // 5프레임 대기

            GameObject instantCapsul = Instantiate(Particle, Return_RandomPosition(), Quaternion.identity);
        }
    }

    Vector3 Return_RandomPosition()
    {
        Vector3 originPosition = rangeObject.transform.position;
        float range_X = rangeCollider.bounds.size.x;
        float range_Z = rangeCollider.bounds.size.z;

        range_X = Random.Range((range_X / 2) * -1, range_X / 2);
        range_Z = Random.Range((range_Z / 2) * -1, range_Z / 2);
        Vector3 RandomPostion = new Vector3(range_X, 0f, range_Z);

        Vector3 respawnPosition = originPosition + RandomPostion;
        return respawnPosition;
    }

    // 프레임 간격 대기 함수
    IEnumerator WaitFrames(int frameCount)
    {
        while (frameCount > 0)
        {
            frameCount--;
            yield return null;
        }
    }
}