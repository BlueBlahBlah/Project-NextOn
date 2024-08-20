using UnityEngine;

public class ChildCollisionHandler : MonoBehaviour
{
    //private CeilingTrigger parentHandler;
    private GameObject parentHandler;

    private CeilingTrigger Handler;
    private Scenario_1 scenario_1;
    private Scenario_2 scenario_2;
    private Scenario_3 scenario_3;
    void Start()
    {
        parentHandler = this.transform.parent.gameObject;
        if(parentHandler.GetComponent<CeilingTrigger>() != null)
        {
            Handler = parentHandler.GetComponent<CeilingTrigger>();
        }
        else if(parentHandler.GetComponent<Scenario_1>() != null)
        {
            scenario_1 = parentHandler.GetComponent<Scenario_1>();
        }
        else if(parentHandler.GetComponent<Scenario_2>() != null)
        {
            scenario_2 = parentHandler.GetComponent<Scenario_2>();
        }
        else if(parentHandler.GetComponent<Scenario_3>() != null)
        {
            scenario_3 = parentHandler.GetComponent<Scenario_3>();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (parentHandler != null)
        {
            if(Handler != null)
            {
                Handler.OnChildTriggerEnter(other, this);
            }
            else if(scenario_1 != null)
            {
                scenario_1.OnChildTriggerEnter(other, this);
            }
            else if(scenario_2 != null)
            {
                scenario_2.OnChildTriggerEnter(other, this);
            }
            else if(scenario_3 != null)
            {
                scenario_3.OnChildTriggerEnter(other, this);
            }
        }

    }

    void OnTriggerStay(Collider other)
    {
        if (parentHandler != null)
        {
            if (Handler != null)
            {
                Handler.OnChildTriggerStay(other, this);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (parentHandler != null)
        {
            if (Handler != null)
            {
                Handler.OnChildTriggerExit(other, this);
            }
        }
    }
}
