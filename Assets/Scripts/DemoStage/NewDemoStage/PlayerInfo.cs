using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour
{
    public float TotalHealth;                          //최대체력
    public float Health;                               //현재체력
    public float HealthGen;                            //체젠
    private float curHealth;                               //현재 스크립트에 관리하는 체력 - 체력이 닳았는지 판단
    [SerializeField] private TextMeshPro damaged;
    public Image hpBar;

    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitInitialize());
    }

    private IEnumerator WaitInitialize()        //PlayerManager 대기
    {
        while (PlayerManager.Instance == null)
        {
            yield return null;
        }

        Initialize();
    }

    private void Initialize()       //초기화 함수
    {
        damaged.SetText("");  //데미지를 입은 경우에만 표시
        InitHPBarSize();  //체력바 사이즈 초기화
        UpdateHealthInfo();
    }

    // Update is called once per frame
    void Update()
    {
        curHealth = Health;
        Health = PlayerManager.Instance.Health;     //현재체력 계속 가져오기
        if (Health == 0 && PlayerManager.Instance.Death == false)                                 //죽은경우
        {
            PlayerManager.Instance.Death = true;
            float DamageDone = curHealth - Health;        //입은 데미지.
            ShowDamage(DamageDone);
            hpBar.rectTransform.localScale = new Vector3(0f, 0f, 0f);
        }
        else if (curHealth > Health && PlayerManager.Instance.Death == false)                     //체력이 닳은경우
        {
            float DamageDone = curHealth - Health;        //입은 데미지.
            ShowDamage(DamageDone);
            hpBar.rectTransform.localScale = new Vector3((float)Health/(float)TotalHealth, 1f, 1f);
        }
    }
    
    private void ShowDamage(float d)
    {
        TextMeshPro tempDamage = Instantiate(damaged, transform.position + new Vector3(0,3.5f,0), Quaternion.identity);
        tempDamage.SetText(d.ToString());
    }
    
    void InitHPBarSize()
    {
        //hpBar의 사이즈를 원래 자신의 사이즈의 1배 크기로 초기화
        hpBar.rectTransform.localScale = new Vector3(1f, 1f, 1f);
    }

    void UpdateHealthInfo()     //체력관련 내용 가져오는 함수
    {
        TotalHealth = PlayerManager.Instance.TotalHealth;
        Health = PlayerManager.Instance.Health;     
        HealthGen = PlayerManager.Instance.HealthGen;
        curHealth = Health;
    }
    
}
