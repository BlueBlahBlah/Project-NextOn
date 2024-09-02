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
    
    
    private float duration = 120f;
    private float Door_Distance = 10;
    private float Air_Distance = 3;
    private float spawnInterval = 20;

    public GameObject Player;

    public int poolSize = 5; // �� �� Ÿ�Դ� Ǯ ������
    private Dictionary<int, List<GameObject>> pools; // ������Ʈ Ǯ�� ������ ��ųʸ�


    void Start()
    {
        cameraManager.SpecialView = false;
        Gague.SetActive(false);


        pools = new Dictionary<int, List<GameObject>>();

        for (int i = 0; i < EnemyProbs.Length; i++)
        {
            pools[i] = new List<GameObject>();

            for (int j = 0; j < poolSize; j++)
            {
                GameObject obj = Instantiate(EnemyProbs[i]);
                obj.SetActive(false);
                pools[i].Add(obj);
            }
        }
    }

    public GameObject GetPooledEnemy(int enemyType)
    {
        foreach (GameObject obj in pools[enemyType])
        {
            if (!obj.activeInHierarchy)
            {
                return obj;
            }
        }

        // Ǯ�� ��� ������ ������Ʈ�� ������ ���� �����Ͽ� �߰�
        GameObject newObj = Instantiate(EnemyProbs[enemyType]);
        newObj.SetActive(false);
        pools[enemyType].Add(newObj);
        return newObj;
    }

    internal void OnChildTriggerEnter(Collider other, ChildCollisionHandler child)
    {
        if (child.name == firstTrigger.name && !is1_TriggerPass)
        {
            firstTrigger.GetComponent<CloseDoor2>().enabled = true;

            is1_TriggerPass = true;

        }
        else if(child.name ==secondTrigger.name && !is2_TriggerPass)
        {
            Debug.Log("��� �� ���༱�� ���ؼ� Ż���غ��� �׷������ؼ��� ���� ���������� ��ٷ����ҰŰ���");

            is2_TriggerPass = true;
            StartCoroutine(DelayFunction(5f));

        }
    }

    IEnumerator DelayFunction(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        Debug.Log("��� �� �������� ��� ���°���? �ϴ� ���� ���������� ���ߺ���");
        StartCoroutine(StartLastGame());
        StartCoroutine(StartLastSpawn());

    }

    IEnumerator StartLastSpawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            //Instantiate(objectCPrefab, Vector3.zero, Quaternion.identity); // C ������Ʈ ����
        }
        yield return null;
    }

    IEnumerator StartLastGame()
    {
        Vector3 startPositionA = Open_Door1.transform.position;
        Vector3 startPositionB = Open_Door2.transform.position;

        Vector3 endPositionA = startPositionA + Vector3.right * Door_Distance;
        Vector3 endPositionB = startPositionB + Vector3.left * Door_Distance;

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            float t = elapsedTime / duration;

            Open_Door1.transform.position = Vector3.Lerp(startPositionA, endPositionA, t);
            Open_Door2.transform.position = Vector3.Lerp(startPositionB, endPositionB, t);

        }

        Open_Door1.transform.position = endPositionA;
        Open_Door2.transform.position = endPositionB;

        Debug.Log("��� Game Over");
        is_End = true;
        yield return null;
    }
        // Update is called once per frame
    void Update()
    {
        cameraManager.SpecialView = false;

        if (is1_TriggerPass && is2_TriggerPass && is_End)
        {
            scenario.StartCoroutine("END");
        }
    }
}
