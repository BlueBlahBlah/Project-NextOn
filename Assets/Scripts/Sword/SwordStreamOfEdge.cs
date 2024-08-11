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

    public float ThisCoolTime;            //현재 무기의 돌아가고 있는 쿨타임
    public float SkillCoolTime;           //현재 무기 스킬의 총 쿨타임
    [SerializeField] private float SkillCoolTimeRate;       //PlayerManager에서 가져오는 쿨타임 감소율
    
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<MeshCollider>();
        collider.enabled = false;
        Damage = 3;
        SkillCoolTimeRate = PlayerManager.Instance.SkillCoolTimeRate;
        SkillCoolTime = 10f;            //현재 무기의 쿨타임을 10초로 초기화 
        SkillCoolTime = SkillCoolTime - (SkillCoolTime * SkillCoolTimeRate);        //쿨타임은 감소율을 적용한 값으로 
        ThisCoolTime = 0;
        Btn.interactable = true;        //처음에는(먹자마자) 스킬 사용가능
    }
    
    private void OnEnable()
    {
        // 버튼 클릭 이벤트 등록
        Btn.onClick.AddListener(SkillSpawn);
        SkillCoolTimeRate = PlayerManager.Instance.SkillCoolTimeRate;
        SkillCoolTime = SkillCoolTime - (SkillCoolTime * SkillCoolTimeRate);        //쿨타임은 감소율을 적용한 값으로 
        ThisCoolTime = 0;               //쿨타임 초기화
        
        Btn.interactable = true;        //처음에는(먹자마자) 스킬 사용가능
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.LogError(ThisCoolTime);
        if (ThisCoolTime > 0)                  //쿨타임이 0보다 클때 (쿨이 남아있는 경우)
        {
            ThisCoolTime -= Time.deltaTime;     //쿨타임 감소
        }
        else if (ThisCoolTime <= 0)           //쿨타임이 0일때 
        {
            Btn.interactable = true;         //스킬 사용 가능
        }
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

        Btn.interactable = false;       //스킬 사용후 다음 쿨타임까지 버튼 잠금
        ThisCoolTime = SkillCoolTime;   //쿨타임 생김

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
