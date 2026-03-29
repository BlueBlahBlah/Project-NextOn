using System.Collections;
using UnityEngine;

public class FlockGhostNight : MonoBehaviour
{
    [SerializeField] private GameObject effectPrefab;
    public float minInterval = 10f;
    public float maxInterval = 20f;
    public float attackRadius = 5f;
    public int damageAmount = 10;

    // Start is called before the first frame update
    void Start()
    {
        // ìµœì´ˆ????ë²ˆì? ì¦‰ì‹œ ?¸ì¶œ
        //InvokeRepeating("ShowEffect", 0f, Random.Range(minInterval, maxInterval));
        Invoke("ShowEffect", Random.Range(minInterval, maxInterval));
    }

    // Update is called once per frame
    void Update()
    {
        // ì¶”ê??ì¸ ë¡œì§???„ìš”?˜ë‹¤ë©??¬ê¸°???‘ì„±
    }

    void ShowEffect()
    {
        // ?´í™?¸ë? ë³´ì—¬ì£¼ëŠ” ë¡œì§
        GameObject effectInstance = Instantiate(effectPrefab, transform.position, Quaternion.identity);
        // ?ˆë? ?¤ì–´, ?¼ì • ?œê°„??ì§€???„ì— ?´í™?¸ë? ?œê±°?˜ë ¤ë©?        Destroy(effectInstance, 1.5f); // 1.5ì´??„ì— ?´í™?¸ë? ?œê±°?˜ë„ë¡??¤ì • (?í•˜???œê°„?¼ë¡œ ë³€ê²?ê°€??

        AttackEnemies(); //?ì—ê²??°ë?ì§€
        
        // ?¤ìŒ ?¸ì¶œ???„í•œ ?œë¤???œê°„ ê°„ê²© ?¤ì •
        Invoke("ShowEffect", Random.Range(minInterval, maxInterval));
    }

    void AttackEnemies()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, attackRadius);

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                // Enemy ?œê·¸ë¥?ê°€ì§??ì—ê²??°ë?ì§€ ì£¼ê¸°
                //collider.GetComponent<EnemyHealth>().TakeDamage(damageAmount);
                Debug.Log("ë°±ê??¼í–‰ ê³µê²©");
            }
        }
    }
}
