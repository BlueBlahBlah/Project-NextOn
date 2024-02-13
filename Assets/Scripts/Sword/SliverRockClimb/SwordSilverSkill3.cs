using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSilverSkill3 : MonoBehaviour
{
    [SerializeField] private GameObject Rock1;
    [SerializeField] private GameObject Rock2;
    [SerializeField] private GameObject Rock3;
    [SerializeField] private GameObject Rock4;
    [SerializeField] private GameObject Rock5;
    [SerializeField] private GameObject Rock6;
    
    // Start is called before the first frame update
    void Start()
    {
        Rock1.SetActive(true);
        Rock2.SetActive(false);
        Rock3.SetActive(false);
        Rock4.SetActive(false);
        Rock5.SetActive(false);
        Rock6.SetActive(false);
        Invoke("expand",1);
        Destroy(gameObject,4f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void expand()
    {
        Rock2.SetActive(true);
        Rock3.SetActive(true);
        Rock4.SetActive(true);
        Rock5.SetActive(true);
        Rock6.SetActive(true);
    }
}
