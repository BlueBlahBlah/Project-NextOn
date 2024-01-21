using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField]
    private GameObject UI;
    private ReinforceUI reinforceUI;
    List<Dictionary<string, object>> data_dialog;

    private void Start()
    {
        reinforceUI = UI.GetComponent<ReinforceUI>();
        data_dialog = CSVReader.Read("ReinforceTest");

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            MixReinforce();
            OnUI();
        }
    }

    public void OnUI()
    {
        UI.SetActive(true);
    }

    private void MixReinforce()
    {
        Debug.Log($"Dialog count : {data_dialog.Count}");
        reinforceUI.text_left.text = data_dialog[0]["Description"].ToString();
        reinforceUI.text_middle.text = data_dialog[1]["Description"].ToString();
        reinforceUI.text_right.text = data_dialog[2]["Description"].ToString();
    }
}
