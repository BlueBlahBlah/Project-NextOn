using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PannelManager : MonoBehaviour
{
    private static PannelManager instance = null;
        private void Awake()
        {
            if (null == instance)
            {
                //???´ë˜???¸ìŠ¤?´ìŠ¤ê°€ ?„ìƒ?ˆì„ ???„ì—­ë³€??instance??ê²Œì„ë§¤ë‹ˆ?€ ?¸ìŠ¤?´ìŠ¤ê°€ ?´ê²¨?ˆì? ?Šë‹¤ë©? ?ì‹ ???£ì–´ì¤€??
                instance = this;
    
                //???„í™˜???˜ë”?¼ë„ ?Œê´´?˜ì? ?Šê²Œ ?œë‹¤.
                //gameObjectë§Œìœ¼ë¡œë„ ???¤í¬ë¦½íŠ¸ê°€ ì»´í¬?ŒíŠ¸ë¡œì„œ ë¶™ì–´?ˆëŠ” Hierarchy?ì˜ ê²Œì„?¤ë¸Œ?íŠ¸?¼ëŠ” ?»ì´ì§€ë§? 
                //?˜ëŠ” ?·ê°ˆë¦?ë°©ì?ë¥??„í•´ thisë¥?ë¶™ì—¬ì£¼ê¸°???œë‹¤.
                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                //ë§Œì•½ ???´ë™???˜ì—ˆ?”ë° ê·??¬ì—??Hierarchy??GameMgr??ì¡´ì¬???˜ë„ ?ˆë‹¤.
                //ê·¸ëŸ´ ê²½ìš°???´ì „ ?¬ì—???¬ìš©?˜ë˜ ?¸ìŠ¤?´ìŠ¤ë¥?ê³„ì† ?¬ìš©?´ì£¼??ê²½ìš°ê°€ ë§ì? ê²?ê°™ë‹¤.
                //ê·¸ë˜???´ë? ?„ì—­ë³€?˜ì¸ instance???¸ìŠ¤?´ìŠ¤ê°€ ì¡´ì¬?œë‹¤ë©??ì‹ (?ˆë¡œ???¬ì˜ GameMgr)???? œ?´ì???
                Destroy(this.gameObject);
            }
        }
        //ê²Œì„ ë§¤ë‹ˆ?€ ?¸ìŠ¤?´ìŠ¤???‘ê·¼?????ˆëŠ” ?„ë¡œ?¼í‹°. static?´ë?ë¡??¤ë¥¸ ?´ë˜?¤ì—??ë§˜ê» ?¸ì¶œ?????ˆë‹¤.
        public static PannelManager Instance
        {
            get
            {
                if (null == instance)
                {
                    return null;
                }
                return instance;
            }
        }
        
        private List<Dictionary<string, object>> data;
        
        
    // Start is called before the first frame update
    void Start()
    {
        data = CSVReader.Read("Resources ?ˆì˜ ê²½ë¡œ");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
