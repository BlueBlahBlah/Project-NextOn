using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempAttack : MonoBehaviour
{
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
