using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyItemObecjt : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // ë¶€ëª??¤ë¸Œ?íŠ¸??ëª¨ë“  ?˜ìœ„ ?¤ë¸Œ?íŠ¸ë¥?ê°€?¸ì˜µ?ˆë‹¤.
        foreach (Transform child in this.transform)
        {
            // ?˜ìœ„ ?¤ë¸Œ?íŠ¸??MyScriptê°€ ?†ë‹¤ë©?ì¶”ê??©ë‹ˆ??
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
        // ì¶©ëŒ??ë¬¼ì²´ê°€ Player ?œê·¸ë¥?ê°€ì§?ê²½ìš°
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject,0.1f);
        }
    }
   
}
