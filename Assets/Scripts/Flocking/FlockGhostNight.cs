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
        // 理쒖큹????踰덉? 利됱떆 ?몄텧
        //InvokeRepeating("ShowEffect", 0f, Random.Range(minInterval, maxInterval));
        Invoke("ShowEffect", Random.Range(minInterval, maxInterval));
    }

    // Update is called once per frame
    void Update()
    {
        // 異붽??곸씤 濡쒖쭅???꾩슂?섎떎硫??ш린???묒꽦
    }

    void ShowEffect()
    {
        // ?댄럺?몃? 蹂댁뿬二쇰뒗 濡쒖쭅
        GameObject effectInstance = Instantiate(effectPrefab, transform.position, Quaternion.identity);
        // ?덈? ?ㅼ뼱, ?쇱젙 ?쒓컙??吏???꾩뿉 ?댄럺?몃? ?쒓굅?섎젮硫?        Destroy(effectInstance, 1.5f); // 1.5珥??꾩뿉 ?댄럺?몃? ?쒓굅?섎룄濡??ㅼ젙 (?먰븯???쒓컙?쇰줈 蹂寃?媛??

        AttackEnemies(); //?곸뿉寃??곕?吏
        
        // ?ㅼ쓬 ?몄텧???꾪븳 ?쒕뜡???쒓컙 媛꾧꺽 ?ㅼ젙
        Invoke("ShowEffect", Random.Range(minInterval, maxInterval));
    }

    void AttackEnemies()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, attackRadius);

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                // Enemy ?쒓렇瑜?媛吏??곸뿉寃??곕?吏 二쇨린
                //collider.GetComponent<EnemyHealth>().TakeDamage(damageAmount);
                Debug.Log("諛깃??쇳뻾 怨듦꺽");
            }
        }
    }
}
