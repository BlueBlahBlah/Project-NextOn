using System.Collections;
using UnityEngine;

public class GunSpiral : MonoBehaviour
{
    public float xRotationSpeed = 2000f;
    public GameObject[] Trails;

    private int frameCount = 0;
    private int minFrameThreshold = 20;
    private int maxFrameThreshold = 40;
    private int frameThreshold;

    void Start()
    {
        // 초기 프레임 임계값 설정
        frameThreshold = Random.Range(minFrameThreshold, maxFrameThreshold + 1);
    }

    void Update()
    {
        // x축 회전
        transform.Rotate(Vector3.up * xRotationSpeed * Time.deltaTime);

        // 랜덤한 프레임 임계값에 기반하여 각 trail 활성화 상태 토글
        frameCount++;
        if (frameCount >= frameThreshold)
        {
            ToggleRandomTrailState();

            // 프레임 카운트 초기화 및 새로운 랜덤한 프레임 임계값 설정
            frameCount = 0;
            frameThreshold = Random.Range(minFrameThreshold, maxFrameThreshold + 1);
        }
    }

    void ToggleRandomTrailState()
    {
        // 각 trail의 활성화 상태 랜덤하게 토글
        foreach (GameObject trail in Trails)
        {
            bool randomState = Random.Range(0, 2) == 0; // 랜덤으로 true 또는 false 설정
            trail.SetActive(randomState);
        }
    }
}