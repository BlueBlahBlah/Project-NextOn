using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scenario1UI : MonoBehaviour
{
    [SerializeField]
    private Image Darkness;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetLittleDark()
    {
        Darkness.color = new Color(Darkness.color.r, Darkness.color.g, Darkness.color.b, 0.5f);
    }

    public void SetTotallyDark()
    {
        Darkness.color = new Color(Darkness.color.r, Darkness.color.g, Darkness.color.b, 1f);
    }

    public void SetLight()
    {
        Darkness.color = new Color(Darkness.color.r, Darkness.color.g, Darkness.color.b, 0f);
    }
}
