using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reinforce : MonoBehaviour
{
    [SerializeField]
    public GameObject UI;
    [SerializeField]
    public GameObject player;
    public Stat stat;


    public void IncreaseStat()
    {
        stat.health = stat.health + (stat.health * 0.05f);
        UI.SetActive(false);
    }

    public void DecreaseStat()
    {
        stat.health = stat.health - (stat.health * 0.05f);
        UI.SetActive(false);
    }
}
