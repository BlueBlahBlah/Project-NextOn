using System.Collections.Generic;
using UnityEngine;

public class BulletPoolManager : MonoBehaviour
{
    private static BulletPoolManager instance;
    public static BulletPoolManager Instance => instance;

    // ?„ë¦¬?¹ë³„ë¡?ê´€ë¦¬ë˜???€ (Dictionary ?¬ìš©?¼ë¡œ ? ì—°?˜ê²Œ ê´€ë¦?
    private Dictionary<GameObject, List<GameObject>> pools = new Dictionary<GameObject, List<GameObject>>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            // ?¬ì´ ë°”ë€Œì–´??? ì??˜ê³  ?¶ë‹¤ë©?ì£¼ì„ ?´ì œ?˜ì„¸??
            // DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public GameObject GetBullet(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        // ?´ë‹¹ ?„ë¦¬?¹ìš© ?€???†ìœ¼ë©??ì„±
        if (!pools.ContainsKey(prefab))
        {
            pools[prefab] = new List<GameObject>();
        }

        GameObject select = null;

        // 1. ë¹„í™œ?±í™”???¬ê³  ?ˆëŠ”) ì´ì•Œ ì°¾ê¸°
        foreach (GameObject item in pools[prefab])
        {
            if (item != null && !item.activeSelf)
            {
                select = item;
                select.transform.SetPositionAndRotation(position, rotation);
                select.SetActive(true);
                break;
            }
        }

        // 2. ë§Œì•½ ?¬ìš©?????ˆëŠ” ì´ì•Œ???†ìœ¼ë©??ˆë¡œ ?ì„±
        if (select == null)
        {
            select = Instantiate(prefab, position, rotation);
            pools[prefab].Add(select);
        }

        return select;
    }
}
