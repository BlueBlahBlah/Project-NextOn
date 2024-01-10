using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberSkillWarhead : MonoBehaviour
{
    public GameObject warhead;

    public GameObject effect;

    public Rigidbody rigid;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        StartCoroutine(Explosion());
    }

    IEnumerator Explosion()
    {
        yield return new WaitForSeconds(3f);
        rigid.velocity = Vector3.zero;
        rigid.angularVelocity = Vector3.zero;
        warhead.SetActive(false);
        effect.SetActive(true);

        RaycastHit[] rayHits = Physics.SphereCastAll(transform.position, 
            15, Vector3.up, 0f, LayerMask.GetMask("Enemy"));
        foreach (RaycastHit hitObj in rayHits)
        {
            //hitObj.transform.GetComponent<>()
            //피격된 적의 체력 감소 및 이펙트
        }
        Destroy(gameObject,5);
    }
}
