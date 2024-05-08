using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SwordStatic : MonoBehaviour
{
    //[SerializeField] private DamageManager DamageManager;
    [SerializeField] private Collider collider;
    public int attackNum;     //유효타횟수
    [SerializeField] private int SkillTime;     //스킬타수
    [SerializeField] private float findDistance;     //스킬거리
    [SerializeField] private GameObject Effect;     //이펙트
    public int Damage;
    
    [SerializeField] private Button Btn;
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject Skill;
    
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<MeshCollider>();
        SkillTime = 3;      //기본 3번 튕김
        findDistance = 5f;      //스킬반경 5f
        Damage = 3;
    }
    
    private void OnEnable()
    {
        // 버튼 클릭 이벤트 등록
        Btn.onClick.AddListener(SkillSpawn);
    }
    
    void SkillSpawn()
    {
        // 현재 오브젝트가 바라보는 방향을 얻기 위해 transform.forward 사용
        Vector3 direction = Player.transform.forward.normalized;

        // 새로운 위치를 현재 위치 + (바라보는 방향 * 거리) 로 설정
        Vector3 skillPosition = transform.position + (direction * 15f) + (Vector3.up * 5f) + (Vector3.right * 5f);

        Instantiate(Skill, skillPosition, Quaternion.Euler(0,90,0));
    }

    // Update is called once per frame
    void Update()
    {
        if (attackNum >= 3)         //3타 이상
        {
            attackNum = 0;          //타수 초기화
            swordSkill();           //스킬
        }
        
    }

   

    private void swordSkill()
    {
        List<GameObject> nearEnemy = FindRandomEnemy();
        //스킬 계수 추가
        int TempDamage =  DamageManager.Instance.SwordStatic_Passive_DamageCounting * Damage;

        int numEnemNear = nearEnemy.Count;
        if (numEnemNear == 1)       //주변에 다른 몬스터가 없을때
        {
            Instantiate(Effect, nearEnemy[0].transform.position, Quaternion.identity);
            nearEnemy[0].GetComponent<Enemy>().curHealth-= TempDamage;
        }
        else            //다른 몬스터가 주변에 더 있을때
        {
            // 첫번째 적과 자기자신의 중간 지점 계산 후 이펙트 생성
            Vector3 middlePoint = (nearEnemy[0].transform.position + this.gameObject.transform.position) / 2f;
            Instantiate(Effect, middlePoint, Quaternion.identity);
            nearEnemy[0].GetComponent<Enemy>().curHealth-= TempDamage;
            //다른 적들 사이에도 이펙트
            for (int i = 1; i < numEnemNear - 1; i++)
            {
                middlePoint = (nearEnemy[i - 1].transform.position + nearEnemy[i].transform.position) / 2f;
                Instantiate(Effect, middlePoint, Quaternion.identity);
                nearEnemy[0].GetComponent<Enemy>().curHealth-= TempDamage;
            }
            nearEnemy[0].GetComponent<Enemy>().curHealth-= TempDamage;
        }
    }
    
    private List<GameObject> FindRandomEnemy()
    {
        //스킬 반경내 콜라이더 가져옴
        Collider[] colliders = Physics.OverlapSphere(transform.position, findDistance);
        
        // "Enemy" 태그를 가진 오브젝트를 배열로 스킬타수 만큼 가져옴
        //GameObject[] enemies = new GameObject[SkillTime]; //= GameObject.FindGameObjectsWithTag("Enemy");
        List<GameObject> enemies = new List<GameObject>();
        int j = 0;
            
        foreach (Collider e in colliders)
        {
            if (e.CompareTag("Enemy"))
            {
                //enemies[j] = e.gameObject;
                enemies.Add(e.gameObject);
                j++;
                if(j >= SkillTime)
                    break;
            }
        }
        // 만약 enemies 배열이 비어 있다면 null을 반환
        if (enemies.Count == 0)
        {
            return null;
        }
        else
        {
            return enemies;
        }
    }
}
