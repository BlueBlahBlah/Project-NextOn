using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public GameObject Peiz3Monster_2;
    public bool Area3;        //Wave3의 경우 해당 변수 true && Area2 일시 진행
    public bool Wave2MonsterClear;

    public bool isPause;                                    //시간이 멈추었는지
    
    
    // Start is called before the first frame update
    void Start()
    {
        Area3 = false;
        Wave2MonsterClear = false;
        isPause = false;
    }

    // Update is called once per frame
    void Update()
    {
        //스택몬스터 20 게이지 채우면 모두 삭제
        if (MonsterManager.Instance.Gauge >= 10 && Wave2MonsterClear == false)
            MonsterManager.Instance.Clear_Wave2_Monsters();
    }
    void TimeStop()
    {
        Time.timeScale = 0;    //게임 일시정지
        isPause = true;
    }

    void TimeResume()
    {
        Time.timeScale = 1;    //게임 일시정지
        isPause = false;
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
        Peiz3Monster_2.SetActive(true);
    }

    public void FirstWelcomeMSG()
    {
        //첫번째 패널 진행
    }
    
}
