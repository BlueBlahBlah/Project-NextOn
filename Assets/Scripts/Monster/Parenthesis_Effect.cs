using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Parenthesis_Effect : MonoBehaviour
{
    [SerializeField] private GameObject Mate_Monster;
    private Transform parentTransform;
    private float distance = 1f; // Parent?€??ê±°ë¦¬

    // Start??ì²??„ë ˆ???…ë°?´íŠ¸ ?„ì— ?¸ì¶œ?©ë‹ˆ??
    void Start()
    {
        // Mate_Monsterê°€ ?¸ìŠ¤?™í„°?ì„œ ? ë‹¹?˜ì? ?Šì? ê²½ìš° ì»´í¬?ŒíŠ¸?ì„œ ê°€?¸ì˜¤ê¸?        if (Mate_Monster == null)
        {
            Mate_Monster = this.gameObject.GetComponentInParent<Parenthesis>().Mate_Monster;
        }

        // Parent ?¤ë¸Œ?íŠ¸??Transform??ê°€?¸ì˜¤ê¸?        parentTransform = this.transform.parent;
    }

    // Update??ë§??„ë ˆ?„ë§ˆ???¸ì¶œ?©ë‹ˆ??
    void Update()
    {
        // Mate_Monster?€ Parentê°€ null???„ë‹Œ ê²½ìš° ?¤í–‰
        if (Mate_Monster != null && parentTransform != null)
        {
            //ë§Œì•½ ?˜ë‹¤ ì£½ì? ?íƒœ?¼ë©´ ?”ì´??ë°©í–¥ ê´€??x
            //?˜ì¤‘ ?˜ë‚˜?¼ë„ ?„ì§ ?´ì•„?ˆë‹¤ë©??´ë‹¹ ì½”ë“œ ?™ì‘
            if (Mate_Monster.GetComponent<Parenthesis>().get_isDeath() == false || this.gameObject.GetComponentInParent<Parenthesis>().get_isDeath() == false)
            {
                // Mate_Monsterë¥?ë°”ë¼ë³´ëŠ” ë°©í–¥ ê³„ì‚°
                Vector3 directionToMate = (Mate_Monster.transform.position - parentTransform.position).normalized;

                // Mate_Monster???•ë°˜?€ ë°©í–¥ ê³„ì‚°
                Vector3 oppositeDirection = -directionToMate;

                // Parent???„ì¹˜?ì„œ Mate_Monster???•ë°˜?€ ë°©í–¥?¼ë¡œ distance ê±°ë¦¬ë¥??ê³  ?´ë™
                Vector3 newPosition = parentTransform.position + oppositeDirection * distance;

                // Y ê°’ì„ 1.5ë¡??¤ì •
                newPosition.y = 1.5f;

                // Parenthesis_Effect ?„ì¹˜ ?…ë°?´íŠ¸
                transform.position = newPosition;

                // Parenthesis_Effectê°€ Mate_Monster???•ë°˜?€ ë°©í–¥??ë°”ë¼ë³´ë„ë¡??¤ì •
                Vector3 lookDirection = parentTransform.position - Mate_Monster.transform.position;
                transform.rotation = Quaternion.LookRotation(lookDirection);
            }
            
        }
    }

}
