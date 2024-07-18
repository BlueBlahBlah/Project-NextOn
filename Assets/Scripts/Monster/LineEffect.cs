using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineEffect : MonoBehaviour
{
    public GameObject Target1;
    public GameObject Target2;

    private void Update()
    {
        Vector3 pos1 = Target1.transform.position;
        Vector3 pos2 = Target2.transform.position;
        
        pos1.y += 1.5f;
        pos2.y += 1.5f;

        // 위치를 두 타겟의 중간 지점으로 설정
        transform.position = Vector3.Lerp(pos1, pos2, 0.5f);

        // 두 타겟 사이의 벡터를 계산
        Vector3 direction = pos2 - pos1;

        // 오브젝트를 회전하여 두 타겟을 향하도록 설정
        transform.rotation = Quaternion.FromToRotation(Vector3.right, direction);

        // 두 타겟 사이의 거리로 스케일을 설정
        Vector3 scale = transform.localScale;
        scale.x = direction.magnitude;
        transform.localScale = scale;
    }
}
