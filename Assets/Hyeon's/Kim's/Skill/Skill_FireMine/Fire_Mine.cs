using UnityEngine;

public class Fire_Mine : MonoBehaviour
{
    private Animator an;
    private float Round;
    private void Start()
    {
        an = GetComponent<Animator>();
        Round = GetComponent<Transform>().localScale.x;
    }
    private void Destroy() 
    {
        an.StopPlayback();

        Collider[] colliders = Physics.OverlapSphere(transform.position, Round);
        foreach (Collider collider in colliders)
        {
            /*
             * ���߽� �ݰ�ȿ� �����ϴ� ���鿡�� ����� �ΰ�    
             */
        }


        Destroy(gameObject, 0.33f); 
    }


}
