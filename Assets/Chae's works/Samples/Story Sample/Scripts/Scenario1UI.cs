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
        // ������ ������ ���� (��ο�)
        Darkness.color = new Color(Darkness.color.r, Darkness.color.g, Darkness.color.b, 0.5f);
    }

    public void SetTotallyDark()
    {
        // ȭ���� ���� ����
        Darkness.color = new Color(Darkness.color.r, Darkness.color.g, Darkness.color.b, 1f);
    }

    public void SetLight()
    {
        // ������ ������ ���� (����)
        Darkness.color = new Color(Darkness.color.r, Darkness.color.g, Darkness.color.b, 0f);
    }

    public void SparkEvent()
    {
        
    }
}
