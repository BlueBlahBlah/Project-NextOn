using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarNumber : MonoBehaviour
{
    public Text textMeshPro;
    public HealthBar HealthBar;

    private int guage;
    // Start is called before the first frame update
    void Start()
    {
        textMeshPro = GetComponent<Text>();
        guage = (int)HealthBar.slider.value;
    }

    // Update is called once per frame
    void Update()
    {
        guage = (int)HealthBar.slider.value;
        textMeshPro.text = guage + "/20";
    }

   
}
