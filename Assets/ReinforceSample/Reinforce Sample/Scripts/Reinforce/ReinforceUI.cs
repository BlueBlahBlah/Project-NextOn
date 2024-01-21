using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReinforceUI : MonoBehaviour
{
    [SerializeField]
    public GameObject UI;
    [SerializeField]
    public GameObject player;
    public Stat stat;

    [Header("Icon")]
    [SerializeField]
    public Image icon_left;
    [SerializeField]
    public Image icon_middle;
    [SerializeField]
    public Image icon_right;

    [Header("Description")]
    [SerializeField]
    public Text text_left;
    [SerializeField]
    public Text text_middle;
    [SerializeField]
    public Text text_right;


    public void Increase()
    {

    }

    public void Decrease()
    {

    }


    public void IncreaseStat(string _data)
    {
        Debug.Log($"{_data}");
        stat.health = stat.health + (stat.health * 0.05f);
        UI.SetActive(false);
    }

    public void DecreaseStat()
    {
        stat.health = stat.health - (stat.health * 0.05f);
        UI.SetActive(false);
    }
}
