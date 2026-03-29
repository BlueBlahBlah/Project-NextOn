using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour
{
    public float TotalHealth;                          //ÏµúÎ?Ï≤¥Î†•
    public float Health;                               //?ÑÏû¨Ï≤¥Î†•
    public float HealthGen;                            //Ï≤¥Ï††
    private float curHealth;                               //?ÑÏû¨ ?§ÌÅ¨Î¶ΩÌä∏??Í¥ÄÎ¶¨Ìïò??Ï≤¥Î†• - Ï≤¥Î†•???≥Ïïò?îÏ? ?êÎã®
    [SerializeField] private TextMeshPro damaged;
    public Image hpBar;

    private bool updateStart = false;

    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitInitialize());
    }

    private IEnumerator WaitInitialize()        //PlayerManager ?ÄÍ∏?    {
        while (PlayerManager.Instance == null)
        {
            yield return null;
        }

        Initialize();
    }

    private void Initialize()       //Ï¥àÍ∏∞???®Ïàò
    {
        Health = PlayerManager.Instance.Health;
        curHealth = Health;
        
        damaged.SetText("");  //?∞Î?ÏßÄÎ•??ÖÏ? Í≤ΩÏö∞?êÎßå ?úÏãú
        InitHPBarSize();  //Ï≤¥Î†•Î∞??¨Ïù¥Ï¶?Ï¥àÍ∏∞??        UpdateHealthInfo();
    }

    // Update is called once per frame
    void Update()
    {
        if (updateStart)
        {
            TotalHealth = PlayerManager.Instance.TotalHealth;
            curHealth = Health;
            Health = PlayerManager.Instance.Health;     //?ÑÏû¨Ï≤¥Î†• Í≥ÑÏÜç Í∞Ä?∏Ïò§Í∏?            if (Health == 0 && PlayerManager.Instance.Death == false)                                 //Ï£ΩÏ?Í≤ΩÏö∞
            {
                PlayerManager.Instance.Death = true;
                float DamageDone = curHealth - Health;        //?ÖÏ? ?∞Î?ÏßÄ.
                ShowDamage(DamageDone);
                hpBar.rectTransform.localScale = new Vector3(0f, 0f, 0f);
            }
            else if (curHealth > Health && PlayerManager.Instance.Death == false)                     //Ï≤¥Î†•???≥Ï?Í≤ΩÏö∞
            {
                float DamageDone = curHealth - Health;        //?ÖÏ? ?∞Î?ÏßÄ.
                ShowDamage(DamageDone);
            
            }
            //Debug.LogError("TotalHealth : " + TotalHealth);
            //Debug.LogError("Health : " + Health);
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
        //hpBar???¨Ïù¥Ï¶àÎ? ?êÎûò ?êÏã†???¨Ïù¥Ï¶àÏùò 1Î∞??¨Í∏∞Î°?Ï¥àÍ∏∞??        hpBar.rectTransform.localScale = new Vector3(1f, 1f, 1f);
    }

    void UpdateHealthInfo()     //Ï≤¥Î†•Í¥Ä???¥Ïö© Í∞Ä?∏Ïò§???®Ïàò
    {
        Health = PlayerManager.Instance.Health;     
        HealthGen = PlayerManager.Instance.HealthGen;
        curHealth = Health;
        updateStart = true;
    }
    
}
