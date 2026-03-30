using System.Collections.Generic;
using UnityEngine;

public class BulletPoolManager : MonoBehaviour
{
    private static BulletPoolManager instance;
    public static BulletPoolManager Instance => instance;

    // ?꾨━?밸퀎濡?愿由щ릺??? (Dictionary ?ъ슜?쇰줈 ?좎뿰?섍쾶 愿由?
    private Dictionary<GameObject, List<GameObject>> pools = new Dictionary<GameObject, List<GameObject>>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            // ?ъ씠 諛붾뚯뼱???좎??섍퀬 ?띕떎硫?二쇱꽍 ?댁젣?섏꽭??
            // DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public GameObject GetBullet(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        // ?대떦 ?꾨━?뱀슜 ????놁쑝硫??앹꽦
        if (!pools.ContainsKey(prefab))
        {
            pools[prefab] = new List<GameObject>();
        }

        GameObject select = null;

        // 1. 鍮꾪솢?깊솕???ш퀬 ?덈뒗) 珥앹븣 李얘린
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

        // 2. 留뚯빟 ?ъ슜?????덈뒗 珥앹븣???놁쑝硫??덈줈 ?앹꽦
        if (select == null)
        {
            select = Instantiate(prefab, position, rotation);
            pools[prefab].Add(select);
        }

        return select;
    }
}
