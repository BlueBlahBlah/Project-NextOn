using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberSkill : MonoBehaviour
{
    [SerializeField] private DamageManager DamageManager;
    [SerializeField] private float moveSpeed = 35f; // 전진 속도
    [SerializeField] private GameObject Player;
    [SerializeField] private List<GameObject> GraywarheadPrefab;
    [SerializeField] private List<GameObject> RedwarheadPrefab;
    [SerializeField] private List<GameObject> GreenwarheadPrefab;
    [SerializeField] private List<GameObject> BluewarheadPrefab;
    [SerializeField] private List<GameObject> YellowwarheadPrefab;
    [SerializeField] private List<GameObject> warheadPrefab;

    // Start is called before the first frame update
    void Start()
    {
        DamageManager = GameObject.Find("DamageManager").GetComponent<DamageManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Bomb()
    {
        // 코루틴 시작
        StartCoroutine(MoveAndReturn());
    }

    IEnumerator MoveAndReturn()
    {
        // 일정 시간 동안 전진
        float elapsed = 0f;
        float timeBetweenWarheads = 0.5f;

        while (elapsed < 3.5f) // 3.5초 동안 전진 
        {
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
            elapsed += Time.deltaTime;
            int BombKind = DamageManager.Bomber_Skill_WarheadKind;
            int BombColor = DamageManager.Bomber_Skill_WarheadColor;
            
            switch (BombColor)
            {
                case 0:
                    warheadPrefab = GraywarheadPrefab;
                    break;
                case 1:
                    warheadPrefab = RedwarheadPrefab;
                    break;
                case 2:
                    warheadPrefab = GreenwarheadPrefab;
                    break;
                case 3:
                    warheadPrefab = BluewarheadPrefab;
                    break;
                case 4:
                    warheadPrefab = YellowwarheadPrefab;
                    break;
            }

            // 이동 중에 0.5초마다 폭탄을 생성 
            if (elapsed % timeBetweenWarheads <= Time.deltaTime && elapsed >= 0.8f && elapsed <= 2.5f)
            {
                if (BombKind == 0)
                {
                    GameObject warhead = Instantiate(warheadPrefab[0], transform.position, Quaternion.identity);
                    warhead.GetComponent<BomberSkillWarhead>().Range = 2;       //폭탄의 반경
                }
                else if (BombKind == 1)
                {
                    GameObject warhead = Instantiate(warheadPrefab[1], transform.position, Quaternion.identity);
                    warhead.GetComponent<BomberSkillWarhead>().Range = 4;       //폭탄의 반경
                }
                else if (BombKind == 2)
                {
                    GameObject warhead = Instantiate(warheadPrefab[2], transform.position, Quaternion.identity);
                    warhead.GetComponent<BomberSkillWarhead>().Range = 6;       //폭탄의 반경
                }
                else if (BombKind == 3)
                {
                    GameObject warhead = Instantiate(warheadPrefab[3], transform.position, Quaternion.identity);
                    warhead.GetComponent<BomberSkillWarhead>().Range = 8;       //폭탄의 반경
                }
                else if (BombKind == 4)
                {
                    GameObject warhead = Instantiate(warheadPrefab[4], transform.position, Quaternion.identity);
                    warhead.GetComponent<BomberSkillWarhead>().Range = 10;       //폭탄의 반경
                }
                
            }

            yield return null;
        }

        // 목표 위치로 돌아오기
        transform.position = Player.transform.position - new Vector3(0f, -5f, 50f);
    }


   
}