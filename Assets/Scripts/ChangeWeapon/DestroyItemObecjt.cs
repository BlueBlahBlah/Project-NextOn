using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyItemObecjt : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // 遺紐??ㅻ툕?앺듃??紐⑤뱺 ?섏쐞 ?ㅻ툕?앺듃瑜?媛?몄샃?덈떎.
        foreach (Transform child in this.transform)
        {
            // ?섏쐞 ?ㅻ툕?앺듃??MyScript媛 ?녿떎硫?異붽??⑸땲??
            if (child.gameObject.GetComponent<DestroyItemObecjt>() == null)
            {
                child.gameObject.AddComponent<DestroyItemObecjt>();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void OnTriggerEnter(Collider other)
    {
        // 異⑸룎??臾쇱껜媛 Player ?쒓렇瑜?媛吏?寃쎌슦
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject,0.1f);
        }
    }
   
}
