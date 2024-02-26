using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScriptOneHand : MonoBehaviour
{
    public float moveSpeed = 0f;        
    public bool walking;
    private Vector3 lastPosition;
    Animator Anim;
    [SerializeField] private int Damage;
    
    public Button attackBtn;

    [SerializeField] private BoxCollider DamageZone;
    //public Button RollBtn;

    [SerializeField] private SwordStreamOfEdge SwordStreamOfEdge;
    [SerializeField] private SwordStatic SwordStatic;
    [SerializeField] private SwordSilver SwordSilver;
    [SerializeField] private SwordDemacia SwordDemacia;
    [SerializeField] private FantasyAxe FantasyAxe;

    void Start()
    {
        // 초기 위치 저장
        lastPosition = transform.position;
        walking = false;
        Anim = GetComponentInChildren<Animator>();
        attackBtn.onClick.AddListener(OnAttackButtonClick);
        //RollBtn.onClick.AddListener(OnRollButtonClick);         //구르기버튼

        SwordStreamOfEdge = GameObject.FindObjectOfType<SwordStreamOfEdge>();
        SwordStatic = GameObject.FindObjectOfType<SwordStatic>();
        SwordSilver = GameObject.FindObjectOfType<SwordSilver>();
        SwordDemacia = GameObject.FindObjectOfType<SwordDemacia>();
        FantasyAxe = GameObject.FindObjectOfType<FantasyAxe>();
    }

    void Update()
    {
        // 현재 위치와 이전 위치 비교
        if (transform.position != lastPosition)
        {
            walking = true;
            // 위치가 변경되었을 때만 아래 코드 실행

            // 이동 방향 설정
            Vector3 moveDirection = (transform.position - lastPosition).normalized;

            // 움직임 처리
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);

            // 회전 처리
            Quaternion newRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * 10f);

            // 현재 위치를 이전 위치로 업데이트
            lastPosition = transform.position;
        }
        else
        {
            walking = false;
        }
        Anim.SetBool("walk", walking);
        
        
        if (SwordStreamOfEdge != null && SwordStreamOfEdge.gameObject.activeSelf)
        {
            Damage = SwordStreamOfEdge.GetComponent<SwordStreamOfEdge>().Damage;
        }
        else if (SwordStatic != null && SwordStatic.gameObject.activeSelf)
        {
            Damage = SwordStatic.GetComponent<SwordStatic>().Damage;
        }
        else if (SwordSilver != null && SwordSilver.gameObject.activeSelf)
        {
            Damage = SwordSilver.GetComponent<SwordSilver>().Damage;
        }
        else if (SwordDemacia != null && SwordDemacia.gameObject.activeSelf)
        {
            Damage = SwordDemacia.GetComponent<SwordDemacia>().Damage;
        }
        else if (FantasyAxe != null && FantasyAxe.gameObject.activeSelf)
        {
            Damage = FantasyAxe.GetComponent<FantasyAxe>().Damage;
        }
    }

    void OnAttackButtonClick()
    {
        Anim.SetTrigger("attack");
        //OnAtaackColliider();
        //ColliderAttack();         //애니메이션에 이벤트로 넣어둠
    }

    /*void OnAtaackColliider()
    {
        if (SwordStreamOfEdge != null && SwordStreamOfEdge.gameObject.activeSelf)
        {
            //Debug.Log("SwordStreamOfEdge 휘두르기전 Collider 켜기");
            SwordStreamOfEdge.GetComponent<MeshCollider>().enabled = true;
        }
        /*if (SwordStatic != null && SwordStatic.gameObject.activeSelf)
        {
            //Debug.Log("SwordStatic 휘두르기전 Collider 켜기");
            SwordStatic.GetComponent<MeshCollider>().enabled = true;
        }#1#
        if (SwordSilver != null && SwordSilver.gameObject.activeSelf)
        {
            //Debug.Log("SwordSilver 휘두르기전 Collider 켜기");
            SwordSilver.GetComponent<MeshCollider>().enabled = true;
        }
        if (SwordDemacia != null && SwordDemacia.gameObject.activeSelf)
        {
            //Debug.Log("SwordDemacia 휘두르기전 Collider 켜기");
            SwordDemacia.GetComponent<MeshCollider>().enabled = true;
        }
        if (FantasyAxe != null && FantasyAxe.gameObject.activeSelf)
        {
            //Debug.Log("FantasyAxe 휘두르기전 Collider 켜기");
            FantasyAxe.GetComponent<MeshCollider>().enabled = true;
        }
    }*/
    /*void OffAttackCollider()
    {
        if (SwordStreamOfEdge != null && SwordStreamOfEdge.gameObject.activeSelf)
        {
            //Debug.Log("SwordStreamOfEdge 휘두르기전 Collider 켜기");
            SwordStreamOfEdge.GetComponent<MeshCollider>().enabled = false;
        }
        /*if (SwordStatic != null && SwordStatic.gameObject.activeSelf)
        {
            //Debug.Log("SwordStatic 휘두르기전 Collider 켜기");
            SwordStatic.GetComponent<MeshCollider>().enabled = false;
        }#1#
        if (SwordSilver != null && SwordSilver.gameObject.activeSelf)
        {
            //Debug.Log("SwordSilver 휘두르기전 Collider 켜기");
            SwordSilver.GetComponent<MeshCollider>().enabled = false;
        }
        if (SwordDemacia != null && SwordDemacia.gameObject.activeSelf)
        {
            //Debug.Log("SwordDemacia 휘두르기전 Collider 켜기");
            SwordDemacia.GetComponent<MeshCollider>().enabled = false;
        }
        if (FantasyAxe != null && FantasyAxe.gameObject.activeSelf)
        {
            //Debug.Log("FantasyAxe 휘두르기전 Collider 켜기");
            FantasyAxe.GetComponent<MeshCollider>().enabled = false;
        }
    }*/
    void OnRollButtonClick()
    {
        Anim.SetTrigger("roll");
    }

    void ColliderAttack()
    {
        
        Collider[] hitColliders = Physics.OverlapBox(DamageZone.bounds.center, DamageZone.bounds.extents,
            DamageZone.transform.rotation);

        foreach (Collider col in hitColliders)
        {
            if (col.CompareTag("Enemy"))
            {
                col.GetComponent<Enemy>().curHealth--;
            }
        }
    }

}
