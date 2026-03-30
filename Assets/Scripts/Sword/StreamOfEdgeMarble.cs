using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreamOfEdgeMarble : MonoBehaviour
{
    [SerializeField] private float rate;

    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        rate = 3f;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > rate)
        {
            timer = 0;
            Vector3 newPosition = transform.position; // ?꾩옱 ?꾩튂 蹂듭궗
            newPosition.y = Random.Range(0f, 1f); // y 醫뚰몴瑜??쒕뜡?쇰줈 蹂寃?            transform.position = newPosition; // ?덈줈???꾩튂 ?좊떦
        }
    }
}
