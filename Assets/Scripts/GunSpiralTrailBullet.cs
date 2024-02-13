using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSpiralTrailBullet : MonoBehaviour
{
    [SerializeField] private GameObject Gun;
    [SerializeField] private float destroyDistance = 2.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Gun == null)
        {
            // If Gun object is null (destroyed), destroy the bullet
            Destroy(gameObject);
            return;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, Gun.transform.position);

        // If the bullet is too far from the Gun, destroy it
        if (distanceToPlayer > destroyDistance)
        {
            Destroy(gameObject);
        }
    }
}