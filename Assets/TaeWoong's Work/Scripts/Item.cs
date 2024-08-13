using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    // 아이템의 종류 열거
    public enum Type { Ammo, Coin, Heart, Weapon };
    
    // 아이템 종류와 값을 저장할 변수
    public Type type;
    public int value; 

    Rigidbody rigid;
    SphereCollider sphereCollider;

    // 초기화   
    void Awake() 
    {
        rigid = GetComponent<Rigidbody>();
        sphereCollider = GetComponent<SphereCollider>(); 
        // GetComponent() 는 첫번째 컴포넌트만 가져옴
        // 따라서, 물리적인 효과를 가진 Collider는 'open prefabs'를 통해서 Move Up으로 가장 위로 올려줘야 함.
    }

    void Update() 
    {
        // Rotate 함수로 회전효과 
        transform.Rotate(Vector3.up * 20 * Time.deltaTime);
    }
    
    void OnCollisionEnter(Collision collision) 
    {
        if(collision.gameObject.tag == "Terrain")
        {
            rigid.isKinematic = true;
            sphereCollider.enabled = false;
        }    
    }
}
