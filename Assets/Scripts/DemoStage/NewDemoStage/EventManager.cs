using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EventManager : MonoBehaviour
{
    private static EventManager instance = null;
    private void Awake()
    {
        if (null == instance)
        {
            //이 클래스 인스턴스가 탄생했을 때 전역변수 instance에 게임매니저 인스턴스가 담겨있지 않다면, 자신을 넣어준다.
            instance = this;

            //씬 전환이 되더라도 파괴되지 않게 한다.
            //gameObject만으로도 이 스크립트가 컴포넌트로서 붙어있는 Hierarchy상의 게임오브젝트라는 뜻이지만, 
            //나는 헷갈림 방지를 위해 this를 붙여주기도 한다.
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            //만약 씬 이동이 되었는데 그 씬에도 Hierarchy에 GameMgr이 존재할 수도 있다.
            //그럴 경우엔 이전 씬에서 사용하던 인스턴스를 계속 사용해주는 경우가 많은 것 같다.
            //그래서 이미 전역변수인 instance에 인스턴스가 존재한다면 자신(새로운 씬의 GameMgr)을 삭제해준다.
            Destroy(this.gameObject);
        }
    }
    //게임 매니저 인스턴스에 접근할 수 있는 프로퍼티. static이므로 다른 클래스에서 맘껏 호출할 수 있다.
    public static EventManager Instance
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
    
    
    [SerializeField] private GameObject[] Before3Peiz;      //막혀있는 다리 오브젝트
    [SerializeField] private GameObject[] After3Peiz;       //뚫린 다리 오브젝트
    [SerializeField] private GameObject WaveArea3Scrit;     //플레이어가 일찍 지나가지 못하게 하는 콜라이더
    [SerializeField] private GameObject WaveArea3Barrier;   //몬스터들이 쫒아오지 못하게 하는 콜라이더
    [SerializeField] private GameObject EventBtn;           //이벤트 버튼
    [SerializeField] private GameObject ParenthesisGauge;           //괄호 몬스터 게이지 UI
          
    //public GameObject Peiz3Monster_2;
    public bool Area3;        //Wave3의 경우 해당 변수 true && Area2 일시 진행
    public bool Wave2MonsterClear;

    [SerializeField] private GameObject Joystick;
    [SerializeField] private GameObject FireBtn;
    [SerializeField] private GameObject SkillBtn;
    [SerializeField] private GameObject[] FirstDirection;
    [SerializeField] private GameObject[] SecondDirection;
    [SerializeField] private GameObject[] ThirdDirection;
    [SerializeField] private GameObject[] FinalDirection;
    
    [SerializeField] private GameObject[] FirstPickWeapons;

    private bool FirstPickupRifle;
    private bool FirstPickUpBombSkill;
    private bool FirstPickUpBulletSupply;
    private bool SecondPickUpHelicopterSkill;
    private bool SecondPickUpSword;
    
    
    private bool randomItemDropSignal;
    private bool getFirsatBulletSupply;
    private bool getFirsatSkill;
    
    private bool getFinalBulletSupply;
    private bool getFinalSkill;
    [SerializeField] private GameObject FadeOut;

    public bool isPause;                                    //시간이 멈추었는지
    
    
    // Start is called before the first frame update
    void Start()
    {
        Joystick.SetActive(false);
        FireBtn.SetActive(false);
        SkillBtn.SetActive(false);
        EventBtn.SetActive(false);
        ParenthesisGauge.SetActive(false);
        Area3 = false;
        Wave2MonsterClear = false;
        isPause = false;
        FirstPickupRifle = false;
        FirstPickUpBombSkill = false;
        FirstPickUpBulletSupply = false;
        SecondPickUpHelicopterSkill = false;
        SecondPickUpSword = false;
        randomItemDropSignal = false;
        getFirsatBulletSupply = false;
        getFirsatSkill = false;
        
        getFinalBulletSupply = false;
        getFinalSkill = false;
        foreach (GameObject g in After3Peiz)
        {
            g.SetActive(false);
        }
        foreach (GameObject g in FirstDirection)
        {
            g.SetActive(false);
        }
        foreach (GameObject g in SecondDirection)
        {
            g.SetActive(false);
        }
        foreach (GameObject g in ThirdDirection)
        {
            g.SetActive(false);
        }
        foreach (GameObject g in FinalDirection)
        {
            g.SetActive(false);
        }
        
        UIManager.instance.DialogueNumber = 50; // 다이얼로그 넘버 저장 (대사 시작지점) 50
        PrintLongDialogue();
    }
    
    public void PrintLongDialogue()
    {
        UIManager.instance.DialogueEventByNumber(UIManager.instance.longDialogue
            , UIManager.instance.DialogueNumber);
    }

    // Update is called once per frame
    void Update()
    {
        //스택몬스터 20 게이지 채우면 모두 삭제
        /*if (MonsterManager.Instance.Gauge >= 10 && Wave2MonsterClear == false)
        {
            //TODO 
            //MonsterManager.Instance.Clear_Wave2_Monsters();
        }*/
            

        /*if (UIManager.instance.DialogueNumber == 59 && UIManager.instance.isCompletelyPrinted == true)
        {
            foreach (GameObject g in FirstDirection)
            {
                g.SetActive(true);                  //화살표 켜지기
            }
            Joystick.SetActive(true);
        }
        else if (UIManager.instance.DialogueNumber == 61 && UIManager.instance.isCompletelyPrinted == true)
        {
            //총기 떨어짐
            if (FirstPickupRifle == false)
            {
                GameObject Item = PlayerManager.Instance._dropItemPosition.DropItem(DropItemPosition.ItemList.ChangeWeaponRifle);
                Item.GetComponent<WeaponChangeGravity>().SetDialog();       //해당 아이템은 획득 시 대화창 나오기
                FirstPickupRifle = true;
            }
            JoystickActivation();     //조이스틱 활성화
        }
        else if (UIManager.instance.DialogueNumber == 64 && UIManager.instance.isCompletelyPrinted == true)
        {
            FireBtn.SetActive(true);
            MonsterManager.Instance.Appearance_First_Monster();
            JoystickActivation();     //조이스틱 활성화
        }
        else if (UIManager.instance.DialogueNumber == 69 && UIManager.instance.isCompletelyPrinted == true)
        {
            if (FirstPickUpBulletSupply == false)
            {
                PlayerManager.Instance._dropItemPosition.DropItem(DropItemPosition.ItemList.BulletSupply);     //탄 보충 아이템 떨어짐
                FirstPickUpBulletSupply = true;
            }
        }
        else if (UIManager.instance.DialogueNumber == 70 && UIManager.instance.isCompletelyPrinted == true)
        {
            if (FirstPickUpBombSkill == false)
            {
                PlayerManager.Instance._dropItemPosition.DropItem(DropItemPosition.ItemList.SkillBomb);     //폭격스킬 떨어짐
                FirstPickUpBombSkill = true;
            }
        }
        else if (UIManager.instance.DialogueNumber == 71 && UIManager.instance.isCompletelyPrinted == true)
        {
            foreach (GameObject g in SecondDirection)                           //2차 화살표 켜지기
            {
                g.SetActive(true);                  //화살표 켜지기
            }
            MonsterManager.Instance.Appearance_Second_Monster();               //두번째 몬스터들 등장
            JoystickActivation();     //조이스틱 활성화
        }
        else if (UIManager.instance.DialogueNumber == 72 && MonsterManager.Instance.TotalMonsters <= 12)
        {
            //두번째 몬스터 절반정도 잡았을때
            if (SecondPickUpHelicopterSkill == false)
            {
                PlayerManager.Instance._dropItemPosition.DropItem(DropItemPosition.ItemList.SkillHeilcopter);     //헬기스킬 떨어짐
                SecondPickUpHelicopterSkill = true;
            }
        }
        else if (UIManager.instance.DialogueNumber == 73 && UIManager.instance.isCompletelyPrinted == true)
        {
            //근접무기 떨어짐
            if (SecondPickUpSword == false)
            {
                PlayerManager.Instance._dropItemPosition.DropItem(DropItemPosition.ItemList.ChangeWeaponDemacia);
                SecondPickUpSword = true;
            }
            SkillBtn.SetActive(true);
            foreach (GameObject g in ThirdDirection)
            {
                g.SetActive(true);                  //화살표 켜지기
            }
        }
        else if (UIManager.instance.DialogueNumber == 75 && UIManager.instance.isCompletelyPrinted == true)
        {
            MonsterManager.Instance.Appearance_Third_Monster(); //세번째 몬스터 웨이브 시작
            JoystickActivation();     //조이스틱 활성화
        }
        else if (UIManager.instance.DialogueNumber == 80 && UIManager.instance.isCompletelyPrinted == true)
        {
            foreach (GameObject g in ThirdDirection)
            {
                g.SetActive(false);                  //화살표 꺼지기
            }
            Invoke("MonsterTimeResume_Invoke",2.5f);
            //괄호몬스터 UI등장
            ParenthesisGauge.SetActive(true);
            ParenthesisGauge.GetComponent<FinalGauge>().DecreaseGauge_Coriutine();          //UI 100초에 걸쳐 감소
            JoystickActivation();     //조이스틱 활성화
            if (MonsterManager.Instance.FinalPeiz == false)
            {
                MonsterManager.Instance.FinalPeiz = true;
                MonsterManager.Instance.Spawn_Parenthesis();
                MonsterManager.Instance.Spawn_Semicolon();
            }
        }
        else if (UIManager.instance.DialogueNumber == 85 && UIManager.instance.isCompletelyPrinted == true)
        {
            //마지막 최종 다리로 이동하는 화살표
            /*foreach (GameObject g in FirstDirection)
            {
                g.SetActive(true);                  //화살표 켜지기
            }
            foreach (GameObject g in SecondDirection)
            {
                g.SetActive(true);                  //화살표 켜지기
            }#1#
            foreach (GameObject g in FinalDirection)
            {
                g.SetActive(true);                  //화살표 켜지기
            }
            ParenthesisGauge.SetActive(false);      //게이지 꺼주기
            JoystickActivation();     //조이스틱 활성화
            foreach (GameObject g in After3Peiz)
            {
                g.SetActive(true);
            }
            foreach (GameObject g in Before3Peiz)
            {
                g.SetActive(false);
            }

            Area3 = true;       //다리 장벽 넘어갈 수 있음
        }*/
        
        if (UIManager.instance.DialogueNumber == 55 && UIManager.instance.isCompletelyPrinted == true)
        {
            foreach (GameObject g in FirstDirection)
            {
                g.SetActive(true);                  //화살표 켜지기
            }
            Joystick.SetActive(true);
            FireBtn.SetActive(true);
            SkillBtn.SetActive(true);
            //fadeout();
        }
        else if (UIManager.instance.DialogueNumber == 57 && UIManager.instance.isCompletelyPrinted == true)
        {
            foreach (GameObject g in FirstPickWeapons)
            {
                g.SetActive(false);
            }
            MonsterManager.Instance.Appearance_First_Monster();             //첫 몬스터 등장
            JoystickActivation();     //조이스틱 활성화
        }
        else if (UIManager.instance.DialogueNumber == 62 && UIManager.instance.isCompletelyPrinted == true)
        {
            MonsterManager.Instance.Appearance_Second_Monster();             //두번째 몬스터 등장
            if (getFirsatBulletSupply == false)
            {
                PlayerManager.Instance._dropItemPosition.DropItem(DropItemPosition.ItemList.BulletSupply);      //탄 보충 아이템 떨어짐
                getFirsatBulletSupply = true;
            }
            foreach (GameObject g in SecondDirection)
            {
                g.SetActive(true);
            }
            JoystickActivation();     //조이스틱 활성화
        }
        else if (UIManager.instance.DialogueNumber == 66 && UIManager.instance.isCompletelyPrinted == true)
        {
            MonsterManager.Instance.Appearance_Third_Monster();             //세번째 몬스터 등장
            randomItemDropSignal = true;                                    //이제 랜덤 아이템 드랍됨
            foreach (GameObject g in ThirdDirection)
            {
                g.SetActive(true);
            }
            JoystickActivation();     //조이스틱 활성화
            if (getFirsatSkill == false)
            {
                DropRandomItem_Invoke(5f);                                        //5초 후 랜덤 아이템 드랍
                getFirsatSkill = true;
            }
            
        }
        else if (UIManager.instance.DialogueNumber == 72 && UIManager.instance.isCompletelyPrinted == true)
        {
            foreach (GameObject g in ThirdDirection)
            {
                g.SetActive(false);                  //화살표 꺼지기
            }
            foreach (GameObject g in SecondDirection)
            {
                g.SetActive(false);                  //화살표 꺼지기
            }
            foreach (GameObject g in FirstDirection)
            {
                g.SetActive(false);                  //화살표 꺼지기
            }
            //괄호몬스터 UI등장
            ParenthesisGauge.SetActive(true);
            ParenthesisGauge.GetComponent<FinalGauge>().DecreaseGauge_Coriutine();          //UI 100초에 걸쳐 감소
            JoystickActivation();     //조이스틱 활성화
            if (MonsterManager.Instance.FinalPeiz == false)
            {
                MonsterManager.Instance.FinalPeiz = true;
                MonsterManager.Instance.Spawn_Parenthesis();
                MonsterManager.Instance.Spawn_Semicolon();
            }
            if (getFinalBulletSupply == false)
            {
                DropRandomItem_Invoke(5f);                                        //5초 후 랜덤 아이템 드랍
                getFinalBulletSupply = true;
            }
            if (getFinalSkill == false)
            {
                DropBulletSupply_Invoke(10f);                                        //10초 후 탄약 보충 드랍
                getFinalSkill = true;
            }
        }
        else if (UIManager.instance.DialogueNumber == 75 && UIManager.instance.isCompletelyPrinted == true)
        {
            //씬 넘어가는 코드
        }
        
            
    }
    
    private void MonsterTimeResume_Invoke()
    {
        MonsterManager.Instance.MonsterTimeResume();
    }

    //랜덤 스킬을 떨어트리는 Invoke
    private void DropRandomItem_Invoke(float time)
    {
        Invoke("DropRandomItem",time);
    }
    
    //랜덤 스킬을 떨어트리는 함수
    private void DropRandomItem()
    {
        PlayerManager.Instance._dropItemPosition.DropItem(DropItemPosition.ItemList.SkillRandom);      //랜덤스킬 아이템 떨어짐
    }
    
    //탄약 보충을 떨어트리는 Invoke
    private void DropBulletSupply_Invoke(float time)
    {
        Invoke("DropBulletSupply",time);
    }
    
    //탄약 보충을 떨어트리는 함수
    private void DropBulletSupply()
    {
        PlayerManager.Instance._dropItemPosition.DropItem(DropItemPosition.ItemList.BulletSupply);      //탄약 보충 아이템 떨어짐
    }

    //랜덤 스킬을 떨어트리는 함수 cancel
    public void CancelDropItem()
    {
        CancelInvoke("DropRandomItem");
    }
    //탄약 보충을 떨어트리는 함수 cancel
    public void CancelBulletSupply()
    {
        CancelInvoke("DropBulletSupply");
    }

    public void fadeout()
    {
        Joystick.SetActive(false);
        FireBtn.SetActive(false);
        SkillBtn.SetActive(false);
        FadeOut.GetComponent<UIBackGroundFade>().fadeout();
    }

    
    
    //마지막 페이즈가 끝나 모든 몬스터 처치
    public void LastPeizDone()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject g in enemies)
        {
            if (g.GetComponent<Semicolon>())
            {
                g.GetComponent<Semicolon>().MonsterClear();
            }
            else if(g.GetComponent<Parenthesis>())
            {
                g.GetComponent<Parenthesis>().MonsterClear();
            }
        }
    }
    
   
    public void TimeStop()
    {
        Time.timeScale = 0;    //게임 일시정지
        isPause = true;
    }

    public void TimeResume()
    {
        Time.timeScale = 1;    //게임 일시정지
        isPause = false;
    }

    //조이스틱을 비활성화 하는 함수
    private void JoystickDeactivation()
    {
        GameObject.Find("Player").GetComponent<CharacterLocomotion>().enabled = false;
    }
    //조이스틱을 활성화 하는 함수
    private void JoystickActivation()
    {
        GameObject.Find("Player").GetComponent<CharacterLocomotion>().enabled = true;
    }
    

    //탈출하는 다리 수정
    public void EscapeCompletion()
    {
        //다리를 연결
        foreach (GameObject g in Before3Peiz)
        {
            g.SetActive(false);
        }

        foreach (GameObject g in After3Peiz)
        {
            g.SetActive(true);
        }

        //길목 제거
        WaveArea3Scrit.SetActive(false);
        WaveArea3Barrier.SetActive(false);
        //EventBtn 비활성화
        EventBtn.SetActive(false);
        //Peiz3Monster_2.SetActive(true);
    }

    //타 클래스에서 대화창을 재개하는 경우 호출
    public void PrintMSG()
    {
        JoystickDeactivation();     //조이스틱 비활성화
        UIManager.instance.DialogueNumber++;
        PrintLongDialogue();
    }
    
    
}
