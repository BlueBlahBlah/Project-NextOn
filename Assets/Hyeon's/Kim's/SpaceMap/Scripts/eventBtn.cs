using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eventBtn : MonoBehaviour
{
    [SerializeField] private MissionTrigger mission;
    // Start is called before the first frame update
    void Start()
    {
        mission = GetComponent<MissionTrigger>();
    }

    // Update is called once per frame
    void Update()
    {
        if (mission.isActiveAndEnabled)
        {
            mission.Close();
        }
    }
}
