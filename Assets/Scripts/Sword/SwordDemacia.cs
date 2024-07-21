using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwordDemacia : MonoBehaviour
{
    [SerializeField] private Collider collider;
    [SerializeField] private GameObject Skill;
    [SerializeField] private Button Btn;
    [SerializeField] private GameObject Player;
    public int Damage;
    
    // Start is called before the first frame update
    void Start()
    {
         Damage = 5;
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

        Instantiate(Skill, skillPosition, Quaternion.identity);
    }
    
    
}