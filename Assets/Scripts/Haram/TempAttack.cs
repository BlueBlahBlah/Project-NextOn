using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempAttack : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            
            if(GameObject.FindWithTag("Enemy"))
            {
               GameObject.FindWithTag("Enemy").SetActive(false);
            }
        }
    }
}
