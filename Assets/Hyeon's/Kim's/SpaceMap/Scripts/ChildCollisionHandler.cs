using UnityEngine;

public class ChildCollisionHandler : MonoBehaviour
{
    private CeilingTrigger parentHandler;

    void Start()
    {
        parentHandler = GetComponentInParent<CeilingTrigger>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (parentHandler != null)
        {
            parentHandler.OnChildTriggerEnter(other, this);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (parentHandler != null)
        {
            parentHandler.OnChildTriggerStay(other, this);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (parentHandler != null)
        {
            parentHandler.OnChildTriggerExit(other, this);
        }
    }
}
