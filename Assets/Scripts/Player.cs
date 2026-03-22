using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed; // Inspector 창에서 설정할 수 있도록 Public으로 변수 추가 
    float hAxis;
    float vAxis;
    bool wDown;

    Vector3 moveVec;

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>(); // 자식 오브젝트의 Animator를 불러온다
    }

    // Update is called once per frame
    void Update()
    {
        // InputManager에서 관리하는 Input 값들을 정수로 받아오기 
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");

        wDown = Input.GetButton("Walk");

        moveVec = new Vector3(hAxis, 0, vAxis).normalized; // 전 방향으로 이동 거리를 평준화 하여 적용 -> *.normalized

        // 관성에 의한 쓰러짐은 RigidBody -> Constraint -> FreezeRotation 으로 해결
        transform.position += moveVec * speed * (wDown ? 0.3f : 1f) * Time.deltaTime; // transform 이동은 꼭 Time.deltaTime 곱해주기

        anim.SetBool("isRun", moveVec != Vector3.zero);
        anim.SetBool("isWalk", wDown);


        // 회전 구현
        // LookAt() : 지정된 벡터를 향해서 회전시켜주는 함수
        transform.LookAt(transform.position + moveVec);
    }
}
