using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Scenario_4 : MonoBehaviour
{
    [SerializeField] private Scenario scenario;
    [SerializeField] private CameraManager cameraManager;
    [SerializeField] private GameObject Gague;
    [SerializeField] bool is1_TriggerPass, is2_TriggerPass, is_End;
    public GameObject[] EnemySpawnPos;
    public GameObject[] EnemyProbs;

    [Header("Door Trigger")]
    public GameObject firstTrigger;

    [Header("End Point")]
    public GameObject secondTrigger;
    public GameObject Open_Door1;
    public GameObject Open_Door2;
    public GameObject Plane;
    public GameObject AirPlane;
    public GameObject Wall;
    
    private float duration = 60f;
    private float Door_Distance = 10;
    private float Air_Distance = 3;
    private float spawnInterval = 5;



    [Header("Player Respawn")]
    public Transform Respawn;
    public GameObject Player;
    public Slider Bar;

    void Start()
    {
        //Player.transform.position = Respawn.localPosition;
        cameraManager.SpecialView = false;
        Gague.gameObject.SetActive(false);
        Wall.SetActive(false);

        // Slider 초기화
        if (Gague != null)
        {
            Bar = Gague.GetComponent<Slider>();
            Bar.maxValue = duration;
            Bar.value = 0;
        }
    }


    internal void OnChildTriggerEnter(Collider other, ChildCollisionHandler child)
    {
        if (child.name == firstTrigger.name && !is1_TriggerPass)
        {
            firstTrigger.GetComponent<CloseDoor2>().enabled = true;

            is1_TriggerPass = true;

        }
        else if(child.name ==secondTrigger.name && !is2_TriggerPass && other.tag == "Player")
        {
            scenario.UIManager.DialogueEventByNumber(scenario.Dialogue.GetComponent<Dialogue>(), 109);
            Debug.Log("대사 저 비행선을 통해서 탈출해보자 그러기위해서는 문이 열릴때까지 기다려야할거같아");

            is2_TriggerPass = true;
            StartCoroutine(DelayFunction(5f));

        }
    }

    IEnumerator DelayFunction(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        Wall.SetActive(true);
        scenario.UIManager.DialogueEventByNumber(scenario.Dialogue.GetComponent<Dialogue>(), 110);
        Debug.Log("대사 이 에러들은 어디서 나온거지? 일단 문이 열릴때까지 버텨보자");
        StartCoroutine(StartLastGame());

    }

    IEnumerator StartLastGame()
    {
        Vector3 startPositionA = Open_Door1.transform.position;
        Vector3 startPositionB = Open_Door2.transform.position;
        Vector3 startPositionC = Plane.transform.position;
        Vector3 startPositionD = AirPlane.transform.position;


        Vector3 endPositionA = startPositionA + Vector3.right * Door_Distance;
        Vector3 endPositionB = startPositionB + Vector3.left * Door_Distance;
        Vector3 endPositionC = startPositionC + Vector3.up * Air_Distance;
        Vector3 endPositionD = startPositionD + Vector3.up * Air_Distance;

        Gague.gameObject.SetActive(true);

        float elapsedTime = 0f;
        float nextSpawnTime = 0f;
        

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            nextSpawnTime += Time.deltaTime;

            float t = elapsedTime / duration;
    
            Bar.value = elapsedTime;

            Open_Door1.transform.position = Vector3.Lerp(startPositionA, endPositionA, t);
            Open_Door2.transform.position = Vector3.Lerp(startPositionB, endPositionB, t);
            Plane.transform.position = Vector3.Lerp(startPositionC, endPositionC, t);
            AirPlane.transform.position = Vector3.Lerp(startPositionD, endPositionD, t);

            if (nextSpawnTime >= spawnInterval)
            {
                nextSpawnTime = 0f; // 스폰 타이머 초기화

                int randomEnemyIndex = Random.Range(0, EnemyProbs.Length);
                int randomSpawnPosIndex = Random.Range(0, EnemySpawnPos.Length);

                GameObject ene = Instantiate(EnemyProbs[randomEnemyIndex],
                            EnemySpawnPos[randomSpawnPosIndex].transform.position,
                            Quaternion.identity);
                ene.gameObject.SetActive(true);
            }
            yield return null;
        }

        Open_Door1.transform.position = endPositionA;
        Open_Door2.transform.position = endPositionB;
        Plane.transform.position = endPositionC;
        AirPlane.transform.position = endPositionD;

        Debug.Log("대사 Game Over");
        is_End = true;
        yield return null;
    }
        // Update is called once per frame
    void Update()
    {
        cameraManager.SpecialView = false;

        if (is1_TriggerPass && is2_TriggerPass && is_End)
        {
            Scenario.instance.playing_Scenario = 4;
            Scenario.instance.StartCoroutine(scenario.END());
        }
    }
}
