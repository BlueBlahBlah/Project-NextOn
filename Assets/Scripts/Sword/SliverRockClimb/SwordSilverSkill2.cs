using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SwordSilverSkill2 : MonoBehaviour
{
    [SerializeField] private GameObject Rock1;
    [SerializeField] private GameObject Rock2;
    [SerializeField] private GameObject Rock3;
    [SerializeField] private GameObject Rock4;
    public List<Enemy> enemyAgain;
    // Start is called before the first frame update
    void Start()
    {
        Rock1.SetActive(true);
        Rock2.SetActive(false);
        Rock3.SetActive(false);
        Rock4.SetActive(false);
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
    }

    /*private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.LogError("DAddwad");
        }
    }*/
}
