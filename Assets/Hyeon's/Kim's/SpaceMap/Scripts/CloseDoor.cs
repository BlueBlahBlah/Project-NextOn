using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseDoor : MonoBehaviour
{
    public float duration = 2.0f; // ȸ���� �Ϸ�� �ð�(��)
    private float elapsedTime = 0.0f;
    private Quaternion startRotation;
    private Quaternion endRotation;
    
    void Start()
    {
        startRotation = this.transform.localRotation;
        if (this.transform.parent.transform.rotation.y != 0)
        {
            endRotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            endRotation = Quaternion.Euler(0, 90, 0);
        }


    }

    // Update is called once per frame
    void Update()
    {
        // ��� �ð��� ������Ŵ
        elapsedTime += Time.deltaTime;

        // ����� �ð� ������ ���� ȸ�� ���� Lerp�� ���
        float t = Mathf.Clamp01(elapsedTime / duration); // 0���� 1 ������ ���� ����
        transform.localRotation = Quaternion.Lerp(startRotation, endRotation, t);
    }
}
