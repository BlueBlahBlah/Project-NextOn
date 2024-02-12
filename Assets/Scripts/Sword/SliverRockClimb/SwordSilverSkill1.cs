using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSilverSkill1 : MonoBehaviour
{
    [SerializeField] private GameObject Rock1;
    
    
    // Start is called before the first frame update
    void Start()
    {
        Rock1.SetActive(true);
        Destroy(gameObject,4f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
