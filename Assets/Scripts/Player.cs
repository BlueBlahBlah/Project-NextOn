using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed; // Inspector ì°½ì—???¤ì •?????ˆë„ë¡?Public?¼ë¡œ ë³€??ì¶”ê? 
    float hAxis;
    float vAxis;
    bool wDown;

    Vector3 moveVec;

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>(); // ?ì‹ ?¤ë¸Œ?íŠ¸??Animatorë¥?ë¶ˆëŸ¬?¨ë‹¤
    }

    // Update is called once per frame
    void Update()
    {
        // InputManager?ì„œ ê´€ë¦¬í•˜??Input ê°’ë“¤???•ìˆ˜ë¡?ë°›ì•„?¤ê¸° 
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");

        wDown = Input.GetButton("Walk");

        moveVec = new Vector3(hAxis, 0, vAxis).normalized; // ??ë°©í–¥?¼ë¡œ ?´ë™ ê±°ë¦¬ë¥??‰ì????˜ì—¬ ?ìš© -> *.normalized

        // ê´€?±ì— ?˜í•œ ?°ëŸ¬ì§ì? RigidBody -> Constraint -> FreezeRotation ?¼ë¡œ ?´ê²°
        transform.position += moveVec * speed * (wDown ? 0.3f : 1f) * Time.deltaTime; // transform ?´ë™?€ ê¼?Time.deltaTime ê³±í•´ì£¼ê¸°

        anim.SetBool("isRun", moveVec != Vector3.zero);
        anim.SetBool("isWalk", wDown);


        // ?Œì „ êµ¬í˜„
        // LookAt() : ì§€?•ëœ ë²¡í„°ë¥??¥í•´???Œì „?œì¼œì£¼ëŠ” ?¨ìˆ˜
        transform.LookAt(transform.position + moveVec);
    }
}
