using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Parenthesis_Effect : MonoBehaviour
{
    [SerializeField] private GameObject Mate_Monster;
    private Transform parentTransform;
    private float distance = 2f; // Parent와의 거리

    // Start는 첫 프레임 업데이트 전에 호출됩니다.
    void Start()
    {
        // Mate_Monster가 인스펙터에서 할당되지 않은 경우 컴포넌트에서 가져오기
        if (Mate_Monster == null)
        {
            Mate_Monster = this.gameObject.GetComponentInParent<Parenthesis>().Mate_Monster;
        }

        // Parent 오브젝트의 Transform을 가져오기
        parentTransform = this.transform.parent;
    }

    // Update는 매 프레임마다 호출됩니다.
    void Update()
    {
        // Mate_Monster와 Parent가 null이 아닌 경우 실행
        if (Mate_Monster != null && parentTransform != null)
        {
            // Mate_Monster를 바라보는 방향 계산
            Vector3 directionToMate = (Mate_Monster.transform.position - parentTransform.position).normalized;

            // Mate_Monster의 정반대 방향 계산
            Vector3 oppositeDirection = -directionToMate;

            // Parent의 위치에서 Mate_Monster의 정반대 방향으로 distance 거리를 두고 이동
            Vector3 newPosition = parentTransform.position + oppositeDirection * distance;

            // Parenthesis_Effect 위치 업데이트
            transform.position = newPosition;

            // Parenthesis_Effect가 Mate_Monster의 정반대 방향을 바라보도록 설정
            Vector3 lookDirection = parentTransform.position - Mate_Monster.transform.position;
            transform.rotation = Quaternion.LookRotation(lookDirection);
        }
    }
}