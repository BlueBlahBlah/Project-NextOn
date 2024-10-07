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
    
    // 코루틴: skill1 내 오브젝트를 0.1~0.3초 간격으로 랜덤하게 하나씩 활성화
    IEnumerator ActivateSkillObjects()
    {
        // skill1 리스트에 오브젝트가 있는지 확인
        if (skill1 == null || skill1.Count == 0)
        {
            Debug.LogError("skill1 리스트가 비어있습니다.");
            yield break;
        }

        // skill1 리스트에서 랜덤하게 오브젝트를 하나씩 활성화
        while (skill1.Count > 0)
        {
            // 리스트에서 랜덤 인덱스 선택
            int randomIndex = Random.Range(0, skill1.Count);

            // 해당 오브젝트 활성화
            skill1[randomIndex].SetActive(true);

            // 활성화된 오브젝트를 리스트에서 제거
            skill1.RemoveAt(randomIndex);

            // 0.1~0.3초 사이의 랜덤한 대기 시간
            float randomDelay = Random.Range(0.1f, 0.3f);
            yield return new WaitForSeconds(randomDelay);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
