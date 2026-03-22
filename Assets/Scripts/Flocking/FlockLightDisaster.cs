using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockLightDisaster : MonoBehaviour
{
    [SerializeField] private GameObject LightBomb;

    [SerializeField] private int reload;
    // Start is called before the first frame update
    void Start()
    {
        reload = Random.Range(8, 15);
        Invoke("attack",reload);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void attack()
    {
        Instantiate(LightBomb,this.transform.position,Quaternion.identity);
        Invoke("attack",reload);
    }
}
