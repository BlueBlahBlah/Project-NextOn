using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwordStreamOfEdge : MonoBehaviour
{
    [SerializeField] private Collider collider;
    [SerializeField] private GameObject Skill;
    [SerializeField] private Button Btn;
    [SerializeField] private GameObject Player;
    public int Damage;
    
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<MeshCollider>();
        collider.enabled = false;
    }
    
    private void OnEnable()
    {
        // 버튼 클릭 이벤트 등록
        Btn.onClick.AddListener(SkillSpawn);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SkillSpawn()
    {
        // 현재 오브젝트가 바라보는 방향을 얻기 위해 transform.forward 사용
        Vector3 direction = Player.transform.forward.normalized;

        // 새로운 위치를 현재 위치 + (바라보는 방향 * 거리) 로 설정
        Vector3 skillPosition = transform.position + (direction * 10f) + (Vector3.up * 5f);
        //높이는 플레이어의 눈높이
        skillPosition.y = Player.transform.position.y + 1;

        Instantiate(Skill, skillPosition, Player.transform.rotation);
        
    }
    
    void OnTriggerEnter(Collider enemy)
    {
        Debug.LogError("스트롭엣지 칼 인식");
        if (enemy.CompareTag("Enemy"))
        {
            //collider.damage--; //collider의 체력이 닳는 메커니즘
            Debug.LogError("스트림오브엣지 칼 공격");
            enemy.GetComponent<Enemy>().curHealth -= Damage ;
        }
    }

    /*public void OnCollider()
    {
        collider.enabled = true;
    }

    public void OffCollider()
    {
        collider.enabled = false;
    }*/
}
