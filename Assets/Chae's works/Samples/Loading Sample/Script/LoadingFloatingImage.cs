using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingFloatingImage : MonoBehaviour
{
    [SerializeField]
    private Image floatingImage;

    private float posX; // �ʱ� x
    private float posY; // �ʱ� y

    [SerializeField]
    private float speed = 1f; // �ӵ�
    private float time = 0f;

    private float deltaY = 0f; // Y ��ȭ��

    void Start()
    {
        if (floatingImage == null) floatingImage = this.gameObject.GetComponent<Image>();

        posX = floatingImage.rectTransform.position.x;
        posY = floatingImage.rectTransform.position.y;
    }

    void Update()
    {
        floating();
    }

    private void floating()
    {
        time += Time.deltaTime * speed;
        deltaY = Mathf.Sin(time) * 30;

        floatingImage.rectTransform.position = new Vector2(posX, posY + deltaY);
    }
}
