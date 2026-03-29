using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempSkillTriggerBox : MonoBehaviour
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
        // ì¶©ëŒ??ë¬¼ì²´ê°€ Player ?œê·¸ë¥?ê°€ì§?ê²½ìš°
        if (other.CompareTag("Player"))
        {
            // plane GameObject??BomberSkill ?¤í¬ë¦½íŠ¸ ê°€?¸ì˜¤ê¸?            BomberSkill bomberSkill = plane.GetComponent<BomberSkill>();

            // ê°€?¸ì˜¨ ?¤í¬ë¦½íŠ¸ê°€ null???„ë‹ˆë©?Bomb ?¨ìˆ˜ ?¤í–‰
            if (bomberSkill != null)
            {
                bomberSkill.Bomb();
            }
        }
    }
    
}
