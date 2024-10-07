using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageClearManager : MonoBehaviour
{
    // 각 스테이지 클리어 여부
    [SerializeField] private bool stage1Clear = false;
    [SerializeField] private bool stage2Clear = false;
    [SerializeField] private bool stage3Clear = false;
    [SerializeField] private bool stage4Clear = false;

    // 각 스테이지 입장 오브젝트
    private GameManager stage1Object;
    private GameManager stage2Object;
    private GameManager stage3Object;
    private GameManager stage4Object;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Scene이 변경 되었을 때, 오브젝트들의 클리어 여부를 체크. 오브젝트들이 존재한다면 파괴 실행
    }
}
