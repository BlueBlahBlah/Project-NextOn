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
        //player?€ ë°œì „ê¸??¬ì´??ê±°ë¦¬ê°€ 4?´í•˜?¼ë•Œ
        if (Vector3.Distance(gameObject.transform.position, player.transform.position) <= 4f)
           // && GameObject.Find("StageManager").GetComponent<StageManager>().Wave2MonsterClear)    //?¤íƒ ëª¬ìŠ¤?°ë? ???¡ì•„?¼ë§Œ ?œì„¤??? ë•Œ
        {
            eventBtn.SetActive(true);
        }
        else
        {
            eventBtn.SetActive(false);
        }
    }
}
