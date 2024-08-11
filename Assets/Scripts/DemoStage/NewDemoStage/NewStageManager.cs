using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewStageManager : MonoBehaviour
{
    private static NewStageManager instance = null;
    private void Awake()
    {
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public static NewStageManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
}
