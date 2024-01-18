using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reinforce : MonoBehaviour
{
    [SerializeField]
    public GameObject player;
    public Stat stat;

    // Start is called before the first frame update
    void Start()
    {
        stat = player.GetComponent<Stat>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncreaseStat(float increase)
    {
        
    }

    public void DecreaseStat(float decrease)
    {

    }
}
