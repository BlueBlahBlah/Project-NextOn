using System.Collections;
using UnityEngine;

public class Ice_Stun : MonoBehaviour
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

                    if (!containsCenterPoint)StartCoroutine(Make_Slow(collider, 3f));
                }
            }
        }
    }
    IEnumerator Make_Slow(Collider col, float delay)
    {
        col.GetComponent<MOVE>().speed = 0.5f;      //need to check 
        yield return new WaitForSeconds(delay);     //�ֺ� ���鵵 ���� ������
        col.GetComponent<MOVE>().speed = 1.0f;

    }
}
