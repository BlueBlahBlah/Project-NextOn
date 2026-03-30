using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Parenthesis_Effect : MonoBehaviour
{
    [SerializeField] private GameObject Mate_Monster;
    private Transform parentTransform;
    private float distance = 1f; // Parent???嫄곕━

    // Start??泥??꾨젅???낅뜲?댄듃 ?꾩뿉 ?몄텧?⑸땲??
    void Start()
    {
        // Mate_Monster媛 ?몄뒪?숉꽣?먯꽌 ?좊떦?섏? ?딆? 寃쎌슦 而댄룷?뚰듃?먯꽌 媛?몄삤湲?        if (Mate_Monster == null)
        {
            Mate_Monster = this.gameObject.GetComponentInParent<Parenthesis>().Mate_Monster;
        }

        // Parent ?ㅻ툕?앺듃??Transform??媛?몄삤湲?        parentTransform = this.transform.parent;
    }

    // Update??留??꾨젅?꾨쭏???몄텧?⑸땲??
    void Update()
    {
        // Mate_Monster? Parent媛 null???꾨땶 寃쎌슦 ?ㅽ뻾
        if (Mate_Monster != null && parentTransform != null)
        {
            //留뚯빟 ?섎떎 二쎌? ?곹깭?쇰㈃ ?붿씠??諛⑺뼢 愿??x
            //?섏쨷 ?섎굹?쇰룄 ?꾩쭅 ?댁븘?덈떎硫??대떦 肄붾뱶 ?숈옉
            if (Mate_Monster.GetComponent<Parenthesis>().get_isDeath() == false || this.gameObject.GetComponentInParent<Parenthesis>().get_isDeath() == false)
            {
                // Mate_Monster瑜?諛붾씪蹂대뒗 諛⑺뼢 怨꾩궛
                Vector3 directionToMate = (Mate_Monster.transform.position - parentTransform.position).normalized;

                // Mate_Monster???뺣컲? 諛⑺뼢 怨꾩궛
                Vector3 oppositeDirection = -directionToMate;

                // Parent???꾩튂?먯꽌 Mate_Monster???뺣컲? 諛⑺뼢?쇰줈 distance 嫄곕━瑜??먭퀬 ?대룞
                Vector3 newPosition = parentTransform.position + oppositeDirection * distance;

                // Y 媛믪쓣 1.5濡??ㅼ젙
                newPosition.y = 1.5f;

                // Parenthesis_Effect ?꾩튂 ?낅뜲?댄듃
                transform.position = newPosition;

                // Parenthesis_Effect媛 Mate_Monster???뺣컲? 諛⑺뼢??諛붾씪蹂대룄濡??ㅼ젙
                Vector3 lookDirection = parentTransform.position - Mate_Monster.transform.position;
                transform.rotation = Quaternion.LookRotation(lookDirection);
            }
            
        }
    }

}
