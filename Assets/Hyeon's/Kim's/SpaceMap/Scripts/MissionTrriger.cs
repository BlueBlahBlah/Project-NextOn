using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionTrriger : MonoBehaviour
{
    private GameObject Player;
    [SerializeField] private GameObject eventBtn;
    void Start()
    {
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(gameObject.transform.position, Player.transform.position) <= 4f)
        {
            eventBtn.SetActive(true);
        }
        else
        {
            eventBtn.SetActive(false);
        }
    }

}
