using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Splines;

public class HelicopterMinigun : MonoBehaviour
{
    [SerializeField] private SplineAnimate splineAnimate;   //헬리콥터 이동 관리 컴포넌트

    [SerializeField] private bool isTurned;                 //헬기 회전 조정 변수
    
    public HelicopterMinigunParticle helicopterMinigunParticle;     //헬기 탄 이펙트 스크립트
    [SerializeField] private GameObject EffectPlane;        //헬기 이펙트 장판
    [SerializeField] private GameObject LeftMuzzle;         //헬기 좌측 총구
    [SerializeField] private GameObject RightMuzzle;        //헬기 우측 총구
    public Camera mainCamera;
    private CameraAbove mainCameraScript;
    public GameObject Player;
    
    private Coroutine cameraCoroutine;
    
    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = Quaternion.Euler(25f, -90f, 0f);       //헬기 위치조정
        splineAnimate = GetComponent<SplineAnimate>();                     //컴포넌트 가져오기
        helicopterMinigunParticle = GameObject.Find("ParticleArea").GetComponent<HelicopterMinigunParticle>();      //ParticleArea 오브젝트
        isTurned = false;           //변수 초기화
        mainCamera = Camera.main;
        mainCameraScript = mainCamera.GetComponent<CameraAbove>();
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (isTurned == false && splineAnimate.NormalizedTime <= 0.45f)
        { // 나아갈때 장판 위치
            EffectPlane.transform.position = transform.position - new Vector3(20f, 7f, 0f);
        }
        
        if (isTurned == false && splineAnimate.NormalizedTime >= 0.45f)
        {
            isTurned = true;
            transform.rotation = Quaternion.Euler(20f, -270f, 0f);              //헬기 회전
        }
        
        if (isTurned == true && splineAnimate.NormalizedTime >= 0.45f)
        {
            //되돌아올때 장판의 위치
            EffectPlane.transform.position = transform.position + new Vector3(20f, -7f, 0f);
        }

        if (splineAnimate.NormalizedTime == 1f)         //스킬 종료
        {
            isTurned = false;
            transform.rotation = Quaternion.Euler(25f, -90f, 0f);               //헬기 회전
            EffectPlane.transform.position = transform.position - new Vector3(20f, 7f, 0f); //장판도 회전
            
            EffectPlane.GetComponent<HelicopterMinigunParticle>().ParticleStop();       //이펙트 그만
            //헬기 총구 그만
            //LeftMuzzle.SetActive(false);
            //RightMuzzle.SetActive(false);
            //카메라 원래 위치로 부드럽게 --- 돌아오는 코루틴의 어색함 으로 사용x
            //originalCameraPosition();
            //카메라 스크립트 재시작
            mainCameraScript.enabled = true;
            //PlayerSoundManager.Instance.StopSound();
            //PlayerSoundManager.Instance.StopSound2();
            
        }
    }
}
