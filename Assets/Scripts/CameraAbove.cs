using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAbove : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 새로운 위치 설정
        Vector3 newPosition = new Vector3(Player.transform.position.x, Player.transform.position.y + 12f, Player.transform.position.z - 6f);
        transform.position = newPosition;

        // 회전 설정
        transform.rotation = Quaternion.Euler(55f, 0f, 0f);
    }
}
