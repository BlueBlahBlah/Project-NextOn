using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scenario1UI : MonoBehaviour
{
    [SerializeField]
    private Image Darkness;
    [SerializeField]
    private Image Spark;

    public void SetLittleDark()
    {
        // 조명의 깜빡임 연출 (어두움)
        Darkness.color = new Color(Darkness.color.r, Darkness.color.g, Darkness.color.b, 0.5f);
    }

    public void SetTotallyDark()
    {
        // 화면의 암전 연출
        Darkness.color = new Color(Darkness.color.r, Darkness.color.g, Darkness.color.b, 1f);
    }

    public void SetLight()
    {
        // 조명의 깜빡임 연출 (밝음)
        Darkness.color = new Color(Darkness.color.r, Darkness.color.g, Darkness.color.b, 0f);
    }

    public void SparkEvent()
    {
        
    }
}
