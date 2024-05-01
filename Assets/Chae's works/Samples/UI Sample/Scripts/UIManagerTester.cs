using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManagerTester : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShortDialoguePrint()
    {
        UIManager.instance.DialogueEventByNumber(UIManager.instance.shortDialogue, 0);
    }

    public void LongDialoguePrint()
    {
        UIManager.instance.DialogueEventByNumber(UIManager.instance.longDialogue, 0);
    }
}
