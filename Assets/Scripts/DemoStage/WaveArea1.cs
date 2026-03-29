using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WaveArea1 : MonoBehaviour
{

    [SerializeField] private bool Active;
    // Start is called before the first frame update
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
            //?œí† ë¦¬ì–¼ ?œìž‘?˜ëŠ” ë¶€ë¶?ì¶”ê?

            Active = true;
            //EventManager.Instance.FirstWelcomeMSG();
        }
        
    }
}
