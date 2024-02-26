using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SwordStatic : MonoBehaviour
{
    [SerializeField] private Collider collider;
    [SerializeField] private int attackNum;     //유효타횟수
    [SerializeField] private int SkillTime;     //스킬타수
    [SerializeField] private float findDistance;     //스킬거리
    [SerializeField] private GameObject Effect;     //이펙트
    public int Damage;
    
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<MeshCollider>();
        SkillTime = 5;      //기본 3번 튕김
        findDistance = 10f;      //스킬반경 10f
        Damage = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if (attackNum >= 3)         //3타 이상
        {
            swordSkill();           //스킬
            attackNum = 0;          //타수 초기화
        }
        
    }

    void OnTriggerEnter(Collider enemy)
    {
        if (enemy.CompareTag("Enemy"))
        {
            //collider.damage--; //collider의 체력이 닳는 메커니즘
            attackNum++;
        }
    }

    private void swordSkill()
    {
        List<GameObject> nearEnemy = FindRandomEnemy();
        int numEnemNear = nearEnemy.Count;
        
        // 첫번째 적과 자기자신의 중간 지점 계산 후 이펙트 생성
        Vector3 middlePoint = (nearEnemy[0].transform.position + this.gameObject.transform.position) / 2f;
        Instantiate(Effect, middlePoint, Quaternion.identity);
        //nearEnemy[o]. 데미지
        //다른 적들 사이에도 이펙트
        for (int i = 1; i < numEnemNear-1; i++)
        {
            middlePoint = (nearEnemy[i-1].transform.position + nearEnemy[i].transform.position) / 2f;
            Instantiate(Effect, middlePoint, Quaternion.identity);
            //nearEnemy[i]. 데미지
        }
        //nearEnemy[numEnemNear]. 데미지
    }
    
    private List<GameObject> FindRandomEnemy()
    {
        //스킬 반경내 콜라이더 가져옴
        Collider[] colliders = Physics.OverlapSphere(transform.position, findDistance);
        
        // "Enemy" 태그를 가진 오브젝트를 배열로 스킬타수 만큼 가져옴
        GameObject[] enemies = new GameObject[SkillTime]; //= GameObject.FindGameObjectsWithTag("Enemy");
        int j = 0;
            
        foreach (Collider e in colliders)
        {
            if (e.CompareTag("Enemy"))
            {
                enemies[j] = e.gameObject;
                j++;
                if(j >= SkillTime)
                    break;
            }
        }

        // 만약 enemies 배열이 비어 있다면 null을 반환
        if (enemies.Length == 0)
        {
            return null;
        }
        // 가장 가까운 5개의 적을 저장할 리스트
        List<GameObject> nearestEnemies = new List<GameObject>();

        // 모든 적을 돌면서 가장 가까운 적을 찾음
        foreach (GameObject enemy in enemies)
        {
            nearestEnemies.Add(enemy);
        }
        return nearestEnemies;
    }
}
