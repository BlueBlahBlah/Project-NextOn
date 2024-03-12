using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompilerTrigger : MonoBehaviour
{
    private GameObject player;
    [SerializeField] private GameObject eventBtn;
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //player와 발전기 사이의 거리가 4이하일때
        if (Vector3.Distance(gameObject.transform.position, player.transform.position) <= 4f)
        {
            eventBtn.SetActive(true);
        }
        else
        {
            eventBtn.SetActive(false);
        }
    }
}
