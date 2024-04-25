using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Player : MonoBehaviour
{
    // Inspector 창에서 설정할 수 있도록 Public으로 변수 추가 
    public float speed; 
    // 플레이어 무기 관련 배열함수 2개 선언
    public GameObject[] weapons;
    public bool[] hasWeapons;

    // 무기 변경을 위한 변수 선언 (장비 개수 만큼 선언)
    bool sDown1; // 1번 장비
    bool sDown2; // 2번 장비
    
    // 무기 교체 시 시간차를 위한 플래그 추가
    bool isSwap;

    float hAxis;
    float vAxis;
    bool wDown;
    bool iDown;
    bool fDown;
    
    // 공격 준비를 알리는 용도의 플래그 추가
    bool isFireReady = true;

    bool isDamage = false;

    Vector3 moveVec;

    Animator anim;
    MeshRenderer[] meshs;
    Rigidbody rigid;

    // 트리거 된 아이템을 저장하기 위한 변수 추가
    GameObject nearObject; 

    // 장착한 아이템을 저장하기 위한 변수 추가
    Weapon equipWeapon;
    int equipWeaponIndex = -1;

    // 공격 구현을 위해 키입력, 공격 딜레이, 공격 준비를 위한 변수 추가
    // fDown , fireDelay, isFireReady
    float fireDelay;
    

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>(); // 자식 오브젝트의 Animator를 불러온다
        meshs = GetComponentsInChildren<MeshRenderer>(); // GetComponent와 GetComponents를 구별하기 
    }

    // Update is called once per frame
    void Update()
    {
        // InputManager를 통해 Input 값 받아오기 
        GetInput();
        // 움직임 구현
        Move();
        // 캐릭터 회전 구현
        Turn();
        // 아이템 상호작용 구현
        Interaction();
        // 무기 교체 구현
        Swap();
        // 공격 구현
        Attack();
    }

    // 외부 충돌에 의한 RigidBody의 회전 속력 발생 문제 해결
    void FreezeRotation()
    {
        rigid.angularVelocity = Vector3.zero;
    }
    void FixedUpdate() 
    {
        FreezeRotation();
    }
    void GetInput()
    {
        // InputManager에서 관리하는 Input 값들을 정수로 받아오기 
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");

        wDown = Input.GetButton("Walk");

        iDown = Input.GetButtonDown("Interaction");

        sDown1 = Input.GetButtonDown("Swap1");
        sDown2 = Input.GetButtonDown("Swap2");

        fDown = Input.GetButtonDown("Fire1");
    }

    void Move()
    {
        // normalized = 방향값이 1로 고정된 벡터
        moveVec = new Vector3(hAxis, 0, vAxis).normalized; // 전 방향으로 이동 거리를 평준화 하여 적용 -> *.normalized

        // 무기 교체 혹은 공격 시 이동 불가
        if(isSwap || !isFireReady)
            moveVec = Vector3.zero;

        // 관성에 의한 쓰러짐은 RigidBody -> Constraint -> FreezeRotation 으로 해결
        transform.position += moveVec * speed * (wDown ? 0.3f : 1f) * Time.deltaTime; // transform 이동은 꼭 Time.deltaTime 곱해주기

        anim.SetBool("isRun", moveVec != Vector3.zero);
        anim.SetBool("isWalk", wDown);
    }

    void Turn()
    {
        // 회전 구현
        // LookAt() : 지정된 벡터를 향해서 회전시켜주는 함수
        transform.LookAt(transform.position + moveVec);
    }

    void Swap()
    {
        // 무기 중복 교체 및 없는 무기 확인을 위한 조건 추가
        if(sDown1 && (!hasWeapons[0] || equipWeaponIndex == 0))
            return;
        if(sDown2 && (!hasWeapons[1] || equipWeaponIndex == 1))
            return;

        int weaponIndex = -1;

        if(sDown1) weaponIndex = 0;
        if(sDown2) weaponIndex = 1;

        if(sDown1 || sDown2)
        {
            if(equipWeapon != null) 
                equipWeapon.gameObject.SetActive(false); // 이전 장비 장착 해제

            equipWeaponIndex = weaponIndex;
            equipWeapon = weapons[weaponIndex].GetComponent<Weapon>(); // 새로운 장비 활성화
            equipWeapon.gameObject.SetActive(true); // 새로운 장비 장착

            anim.SetTrigger("doSwap");

            isSwap = true;

            Invoke("SwapOut", 0.4f);
        }
    }

    void SwapOut()
    {
        isSwap = false;
    }

    void Interaction()
    {
        if(iDown && nearObject != null)
        {
            if(nearObject.tag == "Weapon")
            {
                Item item = nearObject.GetComponent<Item>();
                int weaponIndex = item.value;
                hasWeapons[weaponIndex] = true;

                Destroy(nearObject);
            }
        }
    }

    void Attack()
    {
        if(equipWeapon == null)
            return;
        
        // 공격 딜레이에 시간을 더해주고 공격 가능 여부 확인
        fireDelay +=Time.deltaTime;
        isFireReady = equipWeapon.rate < fireDelay;

        if(fDown && isFireReady && !isSwap)
        {
            equipWeapon.Use(); // 조건이 충족되면 Weapon.cs의 Use() 실행
            anim.SetTrigger(equipWeapon.type == Weapon.Type.Melee ? "doSwing" : "doShot"); // 무기에 따라 다른 트리거 실행
            fireDelay = 0; // 공격 딜레이를 0 으로 돌려 다음 공격까지 기다리도록 설정
        }
        
    }

    // 드랍 아이템 인지를 위한 OnTrigger 이벤트 사용
    void OnTriggerStay(Collider other) 
    {
        if (other.gameObject != null) 
        {
            if(other.tag == "Weapon")    
            {
                nearObject = other.gameObject;
 
                Debug.Log(nearObject.name);
            }
        }
    }

    void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "EnemyBullet")
        {
            if(!isDamage)
            {
                Bullets enemyBullet = other.GetComponent<Bullets>();
                // health -= enemyBullet.damage;
                StartCoroutine(OnDamage());
            }
        }    
    }

    IEnumerator OnDamage()
    {
        isDamage = true;

        foreach(MeshRenderer mesh in meshs)
        {
            mesh.material.color = Color.yellow;
        }

        yield return new WaitForSeconds(1f);

        foreach(MeshRenderer mesh in meshs)
        {
            mesh.material.color = Color.white;
        }

        isDamage = false;
    }

    void OnTriggerExit(Collider other) 
    {
        if(other.tag == "Weapon")
            nearObject = null;
    }
   
}
