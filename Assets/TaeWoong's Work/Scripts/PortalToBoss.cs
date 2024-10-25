using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalToBoss : MonoBehaviour
{
    public Transform portal; // Portal 오브젝트의 Transform을 참조
    [SerializeField] private bool isPlayerInside = false;
    private GameObject player; // 플레이어 GameObject를 저장할 변수

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerInside && player != null)
        {
            // 플레이어를 Portal의 위치로 이동
            player.transform.position = portal.position;
            Debug.Log("플레이어가 보스 영역으로 이동했습니다");
            isPlayerInside = false; // 한 번 이동 후 false로 설정
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // 플레이어가 들어왔을 때
        {
            if(!isPlayerInside)
            {
                MapSoundManager.Instance.StartProgress_Sound();
            }
            isPlayerInside = true;
            player = other.gameObject; // 플레이어 GameObject 저장
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // 플레이어가 나갔을 때
        {
            if(isPlayerInside)
            {
                MapSoundManager.Instance.EndProgress_Sound();
            }
            isPlayerInside = false;
            player = null; // 플레이어 GameObject 초기화
        }
    }
}
