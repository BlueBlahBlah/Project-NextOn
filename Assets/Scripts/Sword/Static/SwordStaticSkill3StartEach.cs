using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordStaticSkill3StartEach : MonoBehaviour
{
    [SerializeField]private List<GameObject> skill1;
    public int Damage;
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject g in skill1)
        {
            g.SetActive(false);
        }
        StartCoroutine(ActivateSkillObjects());
        Destroy(gameObject,5f);
    }
    
    // ì½”ë£¨?? skill1 ???¤ë¸Œ?íŠ¸ë¥?0.1~0.3ì´?ê°„ê²©?¼ë¡œ ?œë¤?˜ê²Œ ?˜ë‚˜???œì„±??    IEnumerator ActivateSkillObjects()
    {
        // skill1 ë¦¬ìŠ¤?¸ì— ?¤ë¸Œ?íŠ¸ê°€ ?ˆëŠ”ì§€ ?•ì¸
        if (skill1 == null || skill1.Count == 0)
        {
            Debug.LogError("skill1 ë¦¬ìŠ¤?¸ê? ë¹„ì–´?ˆìŠµ?ˆë‹¤.");
            yield break;
        }

        // skill1 ë¦¬ìŠ¤?¸ì—???œë¤?˜ê²Œ ?¤ë¸Œ?íŠ¸ë¥??˜ë‚˜???œì„±??        while (skill1.Count > 0)
        {
            // ë¦¬ìŠ¤?¸ì—???œë¤ ?¸ë±??? íƒ
            int randomIndex = Random.Range(0, skill1.Count);

            // ?´ë‹¹ ?¤ë¸Œ?íŠ¸ ?œì„±??            skill1[randomIndex].SetActive(true);

            // ?œì„±?”ëœ ?¤ë¸Œ?íŠ¸ë¥?ë¦¬ìŠ¤?¸ì—???œê±°
            skill1.RemoveAt(randomIndex);

            // 0.1~0.3ì´??¬ì´???œë¤???€ê¸??œê°„
            float randomDelay = Random.Range(0.1f, 0.3f);
            yield return new WaitForSeconds(randomDelay);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
