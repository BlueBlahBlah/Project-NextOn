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
        // 충돌??물체가 Player ?�그�?가�?경우
        if (other.CompareTag("Player"))
        //if(other.gameObject.layer == 6)
        {
            Debug.LogError("?�레?�어");
            // plane GameObject??BomberSkill ?�크립트 가?�오�?            BomberSkill bomberSkill = plane.GetComponent<BomberSkill>();

            // 가?�온 ?�크립트가 null???�니�?Bomb ?�수 ?�행
            if (bomberSkill != null)
            {
                bomberSkill.Bomb();
            }
        }
    }
    
}
