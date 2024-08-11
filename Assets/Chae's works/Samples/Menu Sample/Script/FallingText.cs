using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FallingText : MonoBehaviour
{
    public TextMeshProUGUI fallingText; // �巡�� �� ������� �ؽ�Ʈ UI�� �����ϼ���.
    public float fallSpeed = 200f; // �ؽ�Ʈ�� �������� �ӵ�.
    public float destroyTime = 3f; // �ؽ�Ʈ�� ȭ�鿡�� ������� �ð�.

    public RectTransform canvasRect;

    public int alpha = 1;

    void Start()
    {
        // Canvas RectTransform�� ã�� ����
        canvasRect = GetComponentInParent<Canvas>().GetComponent<RectTransform>();

        // �� ������ ���۰� �� ��ǥ ���
        float startX;
        float endX;

        // alpha�� 2�� �� Ư���� ��Ģ ����
        if (alpha == 2)
        {
            // 2��°, 3��°, 5��° �������� �����ǵ��� ����
            if (Random.Range(0, 3) == 0)
            {
                startX = -960 + (384 * 1);
                endX = startX + 384;
            }
            else if (Random.Range(0, 2) == 0)
            {
                startX = -960 + (384 * 2);
                endX = startX + 384;
            }
            else
            {
                startX = -960 + (384 * 4);
                endX = startX + 384;
            }
        }
        else
        {
            // �Ϲ����� ���� ����
            startX = -960 + (384 * (alpha - 1));
            endX = startX + 384;
        }

        // ������ X ��ǥ ����
        float randomX = Random.Range(startX, endX);

        // Canvas�� �߾� ��ǥ�� ���
        Vector2 canvasCenter = new Vector2(canvasRect.rect.width / 2, canvasRect.rect.height / 2);

        // �ʱ� ��ġ ���� (������ x ��ġ�� Y ��ǥ�� ĵ������ ������� ����)
        fallingText.rectTransform.anchoredPosition = new Vector2(randomX, canvasCenter.y);

        // ���� �ð� �� ������Ʈ�� �ı�
        Destroy(gameObject, destroyTime);
    }


    void Update()
    {
        // �ؽ�Ʈ�� �Ʒ��� ���������� ��
        fallingText.rectTransform.anchoredPosition -= new Vector2(0, fallSpeed * Time.deltaTime);
    }
}

