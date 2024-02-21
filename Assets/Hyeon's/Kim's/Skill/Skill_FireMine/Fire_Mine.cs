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
        
        //폭발시 반경안에 존재하는 적들에게 대미지 부가

        Destroy(gameObject, 0.33f); 
    }


}
