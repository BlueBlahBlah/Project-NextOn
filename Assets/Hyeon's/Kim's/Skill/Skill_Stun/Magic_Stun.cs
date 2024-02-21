using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic_Stun : MonoBehaviour
{
    void Start()
    {
        Vector3 checkPosition = transform.position - new Vector3(0.001f, 0.001f, 0.001f); //�ڱ��ڽ� ���� ��

        Collider[] colliders = Physics.OverlapSphere(checkPosition, 3);
        if (colliders.Length > 0)
        {
            foreach (Collider collider in colliders)
            {
                if (collider.tag == "Enemy")        //need to check
                {
                    bool containsCenterPoint = collider.bounds.Contains(transform.position);

                    if (!containsCenterPoint) StartCoroutine(Make_SlowDamage(collider, 3f));
                }
            }
        }
    }

    IEnumerator Make_SlowDamage(Collider col, float delay)
    {
        //col.hp -= 10;                             //���ع��� ���� �ֺ� ���鿡�� ����� �ΰ� 
        col.GetComponent<MOVE>().speed = 0.3f;      //+ ������
        yield return new WaitForSeconds(delay);
        col.GetComponent<MOVE>().speed = 1.0f;

    }
}
