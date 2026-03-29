using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BracketSyntaxErrorArea : MonoBehaviour
{
    [SerializeField] private bool Active;       //ë°œë™?ˆëŠ”ì§€? ?œë²ˆ ë°œë™?˜ë©´ ?¤ì‹œ ë°œë™?˜ì? ?Šë„ë¡?    // Start is called before the first frame update
    void Start()
    {
        Active = false;
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && Active == false)
        {
            Active = true;
            //EventManager.Instance.TimeStop();   //?œê°„ ?•ì?
            EventManager.Instance.PrintMSG();
            MonsterManager.Instance.MonsterTimeStop();
        }
        
    }
}
