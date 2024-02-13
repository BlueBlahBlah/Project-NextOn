using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSkillTriggerBox : MonoBehaviour
{
    public GameObject plane;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void OnTriggerEnter(Collider other)
    {
        Debug.LogError("무언가 충돌");
        // 충돌한 물체가 Player 태그를 가진 경우
        if (other.CompareTag("Player"))
        //if(other.gameObject.layer == 6)
        {
            Debug.LogError("플레이어");
            // plane GameObject의 BomberSkill 스크립트 가져오기
            BomberSkill bomberSkill = plane.GetComponent<BomberSkill>();

            // 가져온 스크립트가 null이 아니면 Bomb 함수 실행
            if (bomberSkill != null)
            {
                bomberSkill.Bomb();
            }
        }
    }
    
}
