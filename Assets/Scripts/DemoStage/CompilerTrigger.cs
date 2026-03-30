using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompilerTrigger : MonoBehaviour
{
    private GameObject player;
    [SerializeField] private GameObject eventBtn;
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //player? 諛쒖쟾湲??ъ씠??嫄곕━媛 4?댄븯?쇰븣
        if (Vector3.Distance(gameObject.transform.position, player.transform.position) <= 4f)
           // && GameObject.Find("StageManager").GetComponent<StageManager>().Wave2MonsterClear)    //?ㅽ깮 紐ъ뒪?곕? ???≪븘?쇰쭔 ?쒖꽕???좊븣
        {
            eventBtn.SetActive(true);
        }
        else
        {
            eventBtn.SetActive(false);
        }
    }
}
