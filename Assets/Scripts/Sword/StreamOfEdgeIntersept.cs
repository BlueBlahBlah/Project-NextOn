using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class StreamOfEdgeIntersept : MonoBehaviour
{
    [SerializeField] private GameObject destination;
    private Rigidbody rigidbody;
    // ?´ë™???¬ìš©???ë„ ë³€??    public float moveSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        toMove();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void toMove()
    {
        // ì½”ë£¨???œì‘
        StartCoroutine(MoveToDestination(destination.transform.position));
    }

    IEnumerator MoveToDestination(Vector3 destination)
    {
        // ?„ì¬ ?„ì¹˜ë¶€??ëª©ì ì§€ê¹Œì? ?´ë™?˜ëŠ” while ë£¨í”„
        while (transform.position != destination)
        {
            // ?„ì¬ ?„ì¹˜?ì„œ ëª©ì ì§€ ë°©í–¥?¼ë¡œ ?´ë™
            transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);
            yield return null; // ???„ë ˆ???€ê¸?        }
        
    }
}
