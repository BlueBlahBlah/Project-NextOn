using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponChangeGravity : MonoBehaviour
{
    [SerializeField] private BoxCollider _boxCollider;
    [SerializeField] private Rigidbody rigidbody;
    public DropItemPosition.ItemList TypeSelf;

    public bool Dialog_After_Acquisition;       //해당 아이템 획득 시 대화창이 나오는지
    // Start is called before the first frame update
    void Start()
    {
        _boxCollider = GetComponent<BoxCollider>();
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.useGravity = true;
        Dialog_After_Acquisition = false;           //아이템 획득시 대화창 나오지 않는 것이 기본값
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < 0.8F)
        {
            rigidbody.useGravity = false;
            rigidbody.velocity = Vector3.zero; // Stop all movement
            rigidbody.angularVelocity = Vector3.zero; // Stop all rotation
            rigidbody.isKinematic = true; // Optionally, make the object kinematic to prevent any further physics interactions
        }
    }

    //Dialog_After_Acquisition를 true로 바꾸는 함수 - Dialog_After_Acquisition가 변경되는 시점을 맞추기 위함?
    public void SetDialog()
    {
       Invoke("Invoke_SetDialog",1);
    }

    private void Invoke_SetDialog()
    {
        if (Dialog_After_Acquisition == false)
        {
            Dialog_After_Acquisition = true;
        }
    }

    private void DestroyLater()
    {
        Invoke("Invoke_Destroy", 1f);
    }

    private void Invoke_Destroy()
    {
        Destroy(this.gameObject);
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (this.Dialog_After_Acquisition == true)
            {
                EventManager.Instance.PrintMSG();
            }
            DestroyLater();
            
        }
        
    }
}
