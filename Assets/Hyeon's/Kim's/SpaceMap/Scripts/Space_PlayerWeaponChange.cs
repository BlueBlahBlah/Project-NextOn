using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Space_PlayerWeaponChange : MonoBehaviour
{
    [SerializeField] private Space_PlayerManager.WeaponType WeaponType;
    [SerializeField] private GameObject self;
    [SerializeField] private Button fireBtn;
    [SerializeField] private Button skillBtn;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter(Collider other)
    {
        // �浹�� ��ü�� Player �±׸� ���� ���
        if (other.CompareTag("Player") && self.activeSelf == false)
        {
            fireBtn.onClick.RemoveAllListeners();
            skillBtn.onClick.RemoveAllListeners();
            Space_PlayerManager.Instance.ChangeWeapon(WeaponType, self);
        }
    }
}
