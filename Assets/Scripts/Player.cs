using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed; // Inspector 李쎌뿉???ㅼ젙?????덈룄濡?Public?쇰줈 蹂??異붽? 
    float hAxis;
    float vAxis;
    bool wDown;

    Vector3 moveVec;

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>(); // ?먯떇 ?ㅻ툕?앺듃??Animator瑜?遺덈윭?⑤떎
    }

    // Update is called once per frame
    void Update()
    {
        // InputManager?먯꽌 愿由ы븯??Input 媛믩뱾???뺤닔濡?諛쏆븘?ㅺ린 
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");

        wDown = Input.GetButton("Walk");

        moveVec = new Vector3(hAxis, 0, vAxis).normalized; // ??諛⑺뼢?쇰줈 ?대룞 嫄곕━瑜??됱????섏뿬 ?곸슜 -> *.normalized

        // 愿?깆뿉 ?섑븳 ?곕윭吏먯? RigidBody -> Constraint -> FreezeRotation ?쇰줈 ?닿껐
        transform.position += moveVec * speed * (wDown ? 0.3f : 1f) * Time.deltaTime; // transform ?대룞? 瑗?Time.deltaTime 怨깊빐二쇨린

        anim.SetBool("isRun", moveVec != Vector3.zero);
        anim.SetBool("isWalk", wDown);


        // ?뚯쟾 援ы쁽
        // LookAt() : 吏?뺣맂 踰≫꽣瑜??ν빐???뚯쟾?쒖폒二쇰뒗 ?⑥닔
        transform.LookAt(transform.position + moveVec);
    }
}
