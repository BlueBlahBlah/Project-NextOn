using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponChangeGravity : MonoBehaviour
{
    [SerializeField] private BoxCollider _boxCollider;
    [SerializeField] private Rigidbody rigidbody;
    public DropItemPosition.ItemList TypeSelf;

    public bool Dialog_After_Acquisition;       
    
    // Start is called before the first frame update
    void Start()
    {
        _boxCollider = GetComponent<BoxCollider>();
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.useGravity = true;
        Dialog_After_Acquisition = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < 0.8F)
        {
            rigidbody.useGravity = false;
            rigidbody.velocity = Vector3.zero; 
            rigidbody.angularVelocity = Vector3.zero; 
            rigidbody.isKinematic = true; 
        }
    }

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
