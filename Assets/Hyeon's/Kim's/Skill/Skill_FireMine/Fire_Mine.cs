using UnityEngine;

public class Fire_Mine : MonoBehaviour
{
    private Animator an;

    private void Start()
    {
        an = GetComponent<Animator>();

    }
    private void Destroy() 
    {
        an.StopPlayback();
        
        //���߽� �ݰ�ȿ� �����ϴ� ���鿡�� ����� �ΰ�

        Destroy(gameObject, 0.33f); 
    }


}
