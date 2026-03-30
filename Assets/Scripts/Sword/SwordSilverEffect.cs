using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSilverEffect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnTriggerEnter(Collider enemy)
    {
        if (enemy.CompareTag("Enemy"))
        {
            //collider.damage--; //collider??泥대젰???노뒗 硫붿빱?덉쬁
            //Debug.LogError("?ㅻ쾭?ㅽ넠 怨듦꺽");
        }
    }
}
