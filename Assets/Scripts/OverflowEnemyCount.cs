using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class OverflowEnemyCount : MonoBehaviour
{
    private Text text;
    GameObject obj;
    void Awake()
    {   
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Overflow.isclear)
            text.text = "Clear!";
        else if(!Overflow.isrepeat)
            text.text = "Repaired";
        else
            text.text = Overflow.CountEnemy.ToString();
    }
}
