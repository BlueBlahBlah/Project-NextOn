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
             * 폭발시 반경안에 존재하는 적들에게 대미지 부가    
             */
        }


        Destroy(gameObject, 0.33f); 
    }


}
