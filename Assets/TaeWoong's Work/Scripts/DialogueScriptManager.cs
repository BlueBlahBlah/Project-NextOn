using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueScriptManager : MonoBehaviour
{
    public bool isPrint = false; // 스크립트 출력 이력 여부
    public InstantCameraSwitch script_1;
    public InstantCameraSwitch script_2;
    public InstantCameraSwitch script_3;
    public CameraSwitchArea script_3_1;
    public InstantCameraSwitch script_4;
    public InstantCameraSwitch script_5;
    public InstantCameraSwitch script_6;
    public InstantCameraSwitch script_7;
    public InstantCameraSwitch script_8;
    public InstantCameraSwitch script_9_1;
    public InstantCameraSwitch script_9_2;
    public InstantCameraSwitch script_9_3;
    public InstantCameraSwitch script_9_4;
    public InstantCameraSwitch script_10_1;
    public InstantCameraSwitch script_10_2;
    public InstantCameraSwitch script_10_3;
    public InstantCameraSwitch script_10_4;
    [SerializeField] private BreakObject breakObject; // Script #11, #12 확인을 위한 BreakObject 참조
    public InstantCameraSwitch script_13;
    public InstantCameraSwitch script_14;
    public InstantCameraSwitch script_15;
    public InstantArea script_16;
    public InstantArea script_17;
    public InstantArea script_18;
    public InstantArea script_19;
    public InstantArea script_20;
    public InstantArea script_21;
    public InstantArea script_22;
    public InstantArea script_23;
    public InstantArea script_24;
    public InstantCameraSwitch script_25;
    public InstantArea script_26;
    public InstantCameraSwitch script_27;

    // Start is called before the first frame update
    void Start()
    {
        if (UIManager.instance != null)
        {
            // UIManager.instance.ScenarioNumber = 0;
            UIManager.instance.DialogueNumber = 260; // 다이얼로그 넘버 저장
        }

        // 각 스크립트의 OnActivated 이벤트에 메서드 구독
        // if (script_1 != null) script_1.OnActivated += () => PrintLongDialogue(); //UnityEngine.Debug.Log("[태웅 Dialogue] " + script_1.gameObject.name + " 출력");
        if (script_2 != null) script_2.OnActivated += () => PrintLongDialogue(); // UnityEngine.Debug.Log("[태웅 Dialogue] " + script_2.gameObject.name + " 출력");
        if (script_3 != null) script_3.OnActivated += () => PrintLongDialogue(); // UnityEngine.Debug.Log("[태웅 Dialogue] " + script_3.gameObject.name + " 출력");
        if (script_3_1 != null) script_3_1.OnActivated += () => UnityEngine.Debug.Log("[태웅 Dialogue] " + script_3_1.gameObject.name + " 출력");
        if (script_4 != null) script_4.OnActivated += () => UnityEngine.Debug.Log("[태웅 Dialogue] " + script_4.gameObject.name + " 출력");
        if (script_5 != null) script_5.OnActivated += () => UnityEngine.Debug.Log("[태웅 Dialogue] " + script_5.gameObject.name + " 출력");
        if (script_6 != null) script_6.OnActivated += () => PrintLongDialogue(); // UnityEngine.Debug.Log("[태웅 Dialogue] " + script_6.gameObject.name + " 출력");
        if (script_7 != null) script_7.OnActivated += () => PrintLongDialogue(); // UnityEngine.Debug.Log("[태웅 Dialogue] " + script_7.gameObject.name + " 출력");
        if (script_8 != null) script_8.OnActivated += () => UnityEngine.Debug.Log("[태웅 Dialogue] " + script_8.gameObject.name + " 출력");
        if (script_9_1 != null) script_9_1.OnActivated += () => PrintLongDialogue(); // UnityEngine.Debug.Log("[태웅 Dialogue] " + script_9_1.gameObject.name + " 출력");
        if (script_9_2 != null) script_9_2.OnActivated += () => PrintLongDialogue(); // UnityEngine.Debug.Log("[태웅 Dialogue] " + script_9_2.gameObject.name + " 출력");
        if (script_9_3 != null) script_9_3.OnActivated += () => PrintLongDialogue(); // UnityEngine.Debug.Log("[태웅 Dialogue] " + script_9_3.gameObject.name + " 출력");
        if (script_9_4 != null) script_9_4.OnActivated += () => PrintLongDialogue(); // UnityEngine.Debug.Log("[태웅 Dialogue] " + script_9_4.gameObject.name + " 출력");
        if (script_10_1 != null) script_10_1.OnActivated += () => PrintLongDialogue(); // UnityEngine.Debug.Log("[태웅 Dialogue] " + script_10_1.gameObject.name + " 출력");
        if (script_10_2 != null) script_10_2.OnActivated += () => PrintLongDialogue(); // UnityEngine.Debug.Log("[태웅 Dialogue] " + script_10_2.gameObject.name + " 출력");
        if (script_10_3 != null) script_10_3.OnActivated += () => PrintLongDialogue(); // UnityEngine.Debug.Log("[태웅 Dialogue] " + script_10_3.gameObject.name + " 출력");
        if (script_10_4 != null) script_10_4.OnActivated += () => PrintLongDialogue(); // UnityEngine.Debug.Log("[태웅 Dialogue] " + script_10_4.gameObject.name + " 출력");
        if (script_13 != null) script_13.OnActivated += () => UnityEngine.Debug.Log("[태웅 Dialogue] " + script_13.gameObject.name + " 출력");
        if (script_14 != null) script_14.OnActivated += () => PrintLongDialogue(); //UnityEngine.Debug.Log("[태웅 Dialogue] " + script_14.gameObject.name + " 출력");
        if (script_15 != null) script_15.OnActivated += () => PrintLongDialogue(); // UnityEngine.Debug.Log("[태웅 Dialogue] " + script_15.gameObject.name + " 출력");
        if (script_16 != null) script_16.OnActivated += () => UnityEngine.Debug.Log("[태웅 Dialogue] " + script_16.gameObject.name + " 출력");
        if (script_17 != null) script_17.OnActivated += () => UnityEngine.Debug.Log("[태웅 Dialogue] " + script_17.gameObject.name + " 출력");
        if (script_18 != null) script_18.OnActivated += () => UnityEngine.Debug.Log("[태웅 Dialogue] " + script_18.gameObject.name + " 출력");
        if (script_19 != null) script_19.OnActivated += () => UnityEngine.Debug.Log("[태웅 Dialogue] " + script_19.gameObject.name + " 출력");
        if (script_20 != null) script_20.OnActivated += () => UnityEngine.Debug.Log("[태웅 Dialogue] " + script_20.gameObject.name + " 출력");
        if (script_21 != null) script_21.OnActivated += () => UnityEngine.Debug.Log("[태웅 Dialogue] " + script_21.gameObject.name + " 출력");
        if (script_22 != null) script_22.OnActivated += () => UnityEngine.Debug.Log("[태웅 Dialogue] " + script_22.gameObject.name + " 출력");
        if (script_23 != null) script_23.OnActivated += () => UnityEngine.Debug.Log("[태웅 Dialogue] " + script_23.gameObject.name + " 출력");
        if (script_24 != null) script_24.OnActivated += () => UnityEngine.Debug.Log("[태웅 Dialogue] " + script_24.gameObject.name + " 출력");
        if (script_25 != null) script_25.OnActivated += () => PrintLongDialogue(); // UnityEngine.Debug.Log("[태웅 Dialogue] " + script_25.gameObject.name + " 출력");
        if (script_26 != null) script_26.OnActivated += () => UnityEngine.Debug.Log("[태웅 Dialogue] " + script_26.gameObject.name + " 출력");
        if (script_27 != null) script_27.OnActivated += () => PrintLongDialogue(); // UnityEngine.Debug.Log("[태웅 Dialogue] " + script_27.gameObject.name + " 출력");
    }

    public void PrintLongDialogue()
    {
        UIManager.instance.DialogueEventByNumber(UIManager.instance.longDialogue , UIManager.instance.DialogueNumber);
    }

    // Update is called once per frame
    void Update()
    {
        // 필요에 따라 추가적인 업데이트 로직을 여기에 작성
    }
}
