using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Peiz3GaugeNumber : MonoBehaviour
{
    public Text textMeshPro;
    public Peiz3Gauge Peiz3Gauge;

    private float guage;
    // Start is called before the first frame update
    void Start()
    {
        textMeshPro = GetComponent<Text>();
        guage = (float)Peiz3Gauge.slider.value;
    }

    // Update is called once per frame
    void Update()
    {
        guage = (float)Peiz3Gauge.slider.value;
        textMeshPro.text = "ì»´íŒŒ?¼ëŸ¬ë¥?ê³ ì¹˜??ì¤?. " +  guage.ToString("F2") + "%";
    }
}
