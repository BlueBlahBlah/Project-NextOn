using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerate : MonoBehaviour
{
    public static MazeGenerate mazeGenerate;
    [SerializeField]
    private GameObject[] prefabs;
    [SerializeField]
    private Transform[] SpawnPoints;

    public int monsCount;
    void Awake(){
        mazeGenerate = GetComponent<MazeGenerate>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Generate(int num){
        for(int i = 0; i < num; i++){
            int x = Random.Range(0,6);
            Instantiate(prefabs[0],SpawnPoints[x].position,SpawnPoints[x].rotation);
            monsCount++;
        }
    }
}
