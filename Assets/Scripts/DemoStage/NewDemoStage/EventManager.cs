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
    [SerializeField] private GameObject StageClearPanelObject;
    [SerializeField] private StageClearPanel StageClearPanel;
    private bool stageclearpannel_appearence;

    public bool isPause;                                    //시간이 멈추었는지
    
    public AudioClip BGM;
    public AudioSource BGMaudioSource;

    private bool dialogue_1;
    private bool dialogue_2;
    private bool dialogue_3;
    private bool dialogue_4;
    private bool dialogue_5;
    private bool dialogue_6;
    
    
    // Start is called before the first frame update
    void Start()
    {
        stageclearpannel_appearence = false;
        Joystick.SetActive(false);
        FireBtn.SetActive(false);
        SkillBtn.SetActive(false);
        EventBtn.SetActive(false);
        StageClearPanelObject.SetActive(false);
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
        
       
        
        BGM = Resources.Load<AudioClip>("Sound/BGM/A Fight With The Enemy");
        BGMaudioSource = gameObject.AddComponent<AudioSource>();
        BGMaudioSource.clip = BGM;
        BGMaudioSource.volume = 0.2f; // Set volume to 0.2
        BGMaudioSource.loop = true; // Enable looping
        BGMaudioSource.Play(); // Start playing BGM

        dialogue_1 = false;
        dialogue_2 = false;
        dialogue_3 = false;
        dialogue_4 = false;
        dialogue_5 = false;
        dialogue_6= false;
        
        Invoke("msg_invoke",1.5f);

    }

    private void msg_invoke()
    {
        UIManager.instance.DialogueNumber = 70; // 다이얼로그 넘버 저장 (대사 시작지점) 70
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
        if (PlayerManager.Instance.revive == 0)
        {
            BGMaudioSource.Stop();
        }
        if (UIManager.instance.DialogueNumber == 83)// && UIManager.instance.isCompletelyPrinted == true)
        {
            if (dialogue_1 == false)
            {
                dialogue_1 = true;
                foreach (GameObject g in FirstDirection)
                {
                    g.SetActive(true);                  //화살표 켜지기
                }
                Joystick.SetActive(true);
                FireBtn.SetActive(true);
                SkillBtn.SetActive(true);
                PlayerManager.Instance.find_attackBtn_Invoke();
            }
            
            //fadeout();
        }
        else if (UIManager.instance.DialogueNumber == 84)// && UIManager.instance.isCompletelyPrinted == true)
        {
            if (dialogue_2 == false)
            {
                dialogue_2 = true;
                foreach (GameObject g in FirstPickWeapons)
                {
                    g.SetActive(false);
                }
                MonsterManager.Instance.Appearance_First_Monster();             //첫 몬스터 등장
                JoystickActivation();     //조이스틱 활성화
                //무기 탄알 초기화 코드
                PlayerManager.instance.longtypeweapon_bullet_init();
            }
            
        }
        else if (UIManager.instance.DialogueNumber == 92)// && UIManager.instance.isCompletelyPrinted == true)
        {
            if (dialogue_3 == false)
            {
                dialogue_3 = true;
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
            
        }
        else if (UIManager.instance.DialogueNumber == 94)// && UIManager.instance.isCompletelyPrinted == true)
        {
            if (dialogue_4 == false)
            {
                dialogue_4 = true;
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
            
            
        }
        else if (UIManager.instance.DialogueNumber == 99)// && UIManager.instance.isCompletelyPrinted == true)
        {
            if (dialogue_5 == false)
            {
                dialogue_5 = true;
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
            
        }
        else if (UIManager.instance.DialogueNumber == 100)// && UIManager.instance.isCompletelyPrinted == true)
        {
            if (dialogue_6 == false)
            {
                dialogue_6 = true;
                //브금 중지
                if (BGMaudioSource != null && BGMaudioSource.isPlaying)
                {
                    BGMaudioSource.Stop();
                }

                if (stageclearpannel_appearence == false)
                {
                    stageclearpannel_appearence = true;
                    StageClearPanelObject.SetActive(true);
                    Invoke("stageclear_invoke",2f);
                }
            }
           
            
            
            //fadeout();
            //씬 넘어가는 코드
            //Debug.LogError("씬 넘어가는 코드 넣어야 합니다");
        }
        
            
    }

    private void stageclear_invoke()
    {
        StageClearPanel.OpenPanel();
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
    
    /*public void BGM_Start() => PlayLoopingSound(BGM);
    
    // BGM 볼륨을 조절하는 메서드
    public void SetBGMVolume(float volume)
    {
        if (audioSources.ContainsKey(BGM))
        {
            AudioSource bgmSource = audioSources[BGM];
            bgmSource.volume = Mathf.Clamp(volume, 0f, 1f); // 0부터 1까지의 범위로 제한
        }
        else
        {
            Debug.LogError("BGM의 AudioSource를 찾을 수 없습니다.");
        }
    }*/

    //타 클래스에서 대화창을 재개하는 경우 호출
    public void PrintMSG()
    {
        JoystickDeactivation();     //조이스틱 비활성화
        //UIManager.instance.DialogueNumber++;
        PrintLongDialogue();
    }
    
    
}
