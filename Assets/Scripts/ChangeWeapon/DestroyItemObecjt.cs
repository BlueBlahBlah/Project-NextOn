using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyItemObecjt : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // 부모 오브젝트의 모든 하위 오브젝트를 가져옵니다.
        foreach (Transform child in this.transform)
        {
            // 하위 오브젝트에 MyScript가 없다면 추가합니다.
            if (child.gameObject.GetComponent<DestroyItemObecjt>() == null)
            {
                child.gameObject.AddComponent<DestroyItemObecjt>();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void OnTriggerEnter(Collider other)
    {
        // 충돌한 물체가 Player 태그를 가진 경우
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject,0.1f);
        }
    }
   
}
