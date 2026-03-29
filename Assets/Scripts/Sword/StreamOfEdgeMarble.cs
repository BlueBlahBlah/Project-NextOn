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
            Vector3 newPosition = transform.position; // ?„ì¬ ?„ì¹˜ ë³µì‚¬
            newPosition.y = Random.Range(0f, 1f); // y ì¢Œí‘œë¥??œë¤?¼ë¡œ ë³€ê²?            transform.position = newPosition; // ?ˆë¡œ???„ì¹˜ ? ë‹¹
        }
    }
}
