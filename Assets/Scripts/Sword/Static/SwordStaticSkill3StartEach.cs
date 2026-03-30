using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordStaticSkill3StartEach : MonoBehaviour
{
    [SerializeField]private List<GameObject> skill1;
    public int Damage;
    
    void Start()
    {
        foreach (GameObject g in skill1)
        {
            g.SetActive(false);
        }
        StartCoroutine(ActivateSkillObjects());
        Destroy(gameObject,5f);
    }
    
    IEnumerator ActivateSkillObjects()
    {
        if (skill1 == null || skill1.Count == 0)
        {
            Debug.LogError("skill1 Error");
            yield break;
        }

        while (skill1.Count > 0)
        {
            int randomIndex = Random.Range(0, skill1.Count);
            skill1[randomIndex].SetActive(true);
            skill1.RemoveAt(randomIndex);
            float randomDelay = Random.Range(0.1f, 0.3f);
            yield return new WaitForSeconds(randomDelay);
        }
    }

    void Update()
    {
        
    }
}
