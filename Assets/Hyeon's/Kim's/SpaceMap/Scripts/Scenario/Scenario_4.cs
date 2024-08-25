using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Scenario_4 : MonoBehaviour
{
    [SerializeField] private Scenario scenario;
    [SerializeField] private CameraManager cameraManager;
    [SerializeField] private GameObject Gague;
    [SerializeField] bool is1_TriggerPass, is2_TriggerPass, is_End;
    public GameObject[] EnemySpawn;

    [Header("Door Trigger")]
    public GameObject firstTrigger;

    [Header("End Point")]
    public GameObject secondTrigger;
    public GameObject Open_Door1;
    public GameObject Open_Door2;
    public GameObject Plane;
    public GameObject AirPlane;
    
    
    private float duration = 120f;
    private float Door_Distance = 10;
    private float Air_Distance = 3;

    public GameObject Player;
    
    
    void Start()
    {
        Gague.SetActive(false);
    }
    internal void OnChildTriggerEnter(Collider other, ChildCollisionHandler child)
    {
        if (child.name == firstTrigger.name && !is1_TriggerPass)
        {
            firstTrigger.GetComponent<CloseDoor2>().enabled = true;
            Debug.Log("대사 이제 저기있는 비행선까지 이동해보자");
            is1_TriggerPass = true;

        }
        else if(child.name ==secondTrigger.name && !is2_TriggerPass)
        {
            StartCoroutine("StartLastGame");
            Debug.Log("이 에러들은 어디서 나온거지? 일단 문이 열릴때까지 버텨보자");
            is2_TriggerPass = true;
        }
    }

    IEnumerator StartLastGame()
    {
        yield return null;
    }
        // Update is called once per frame
        void Update()
    {
        if (Player.transform.position.y > 2f)
        {
            cameraManager.SpecialView = true;
            if (Player.transform.position.z < -34f)
            {
                cameraManager.isTopview = false;
            }
            else
            {
                cameraManager.isTopview = true;
            }
        }
        else
        {
            cameraManager.SpecialView = false;
        }

        if (is1_TriggerPass && is2_TriggerPass && is_End)
        {
            scenario.StartCoroutine("END");
        }
    }
}
