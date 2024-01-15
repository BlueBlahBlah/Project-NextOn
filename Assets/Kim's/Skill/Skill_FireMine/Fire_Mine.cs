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
        Destroy(gameObject, 0.4f); 
    }


}
