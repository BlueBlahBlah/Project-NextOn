using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeStart : MonoBehaviour
{
    public static MazeStart mazeStart;
    [SerializeField]
    private Maze maze;
    public bool isMazeStart;

    private void Awake()
    {
        mazeStart = this;
    }
    private void Update()
    {
        
    }
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            isMazeStart = true;
        }
    }
}
