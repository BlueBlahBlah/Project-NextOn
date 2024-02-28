using System.Collections;
using UnityEngine;
using UnityEngine.Splines;

public class HelicopterMinigunTrigger : MonoBehaviour
{
    public GameObject Helicopter;  // 헬리콥터
    public GameObject EffectField;  // 이펙트필드
    public HelicopterMinigunParticle helicopterMinigunParticle;  // 이펙트필드 스크립트

    // 헬기 총구
    public GameObject LeftMuzzle;
    public GameObject RightMuzzle;

    public Camera mainCamera;  // 카메라
    private CameraAbove mainCameraScript;  // 카메라 스크립트

    public GameObject Player;  // 플레이어

    private bool isCameraMoving = false;  // 카메라 이동 중 여부를 나타내는 플래그
    private Vector3 targetCameraPosition;  // 목표 카메라 위치
    private float cameraMoveDuration = 1.0f;  // 카메라 이동에 걸리는 시간
    
    private void Start()
    {
        helicopterMinigunParticle = GameObject.Find("ParticleArea").GetComponent<HelicopterMinigunParticle>();
        LeftMuzzle = GameObject.Find("HelLeftMuzzles");
        RightMuzzle = GameObject.Find("HelRightMuzzles");
        mainCamera = Camera.main;
        mainCameraScript = mainCamera.GetComponent<CameraAbove>();
        Player = GameObject.Find("Player");

        LeftMuzzle.SetActive(false);
        RightMuzzle.SetActive(false);
    }

    public void OnTriggerEnter(Collider other)
    {
        // 충돌한 물체가 Player 태그를 가진 경우
        if (other.CompareTag("Player"))
        {
            SplineAnimate spline = Helicopter.GetComponent<SplineAnimate>();
            //헬기 스킬 공격계수 전달
            helicopterMinigunParticle.CalculateDamage(GetComponent<StageManager>().Helicopter_Skill_DamageCounting);
            if (spline.NormalizedTime == 1f)  // 스킬이 끝나면 다시 준비
            {
                spline.NormalizedTime = 0;
            }
            if (spline.IsPlaying != true)  // 이미 스킬 실행 중이면 무시
            {
                spline.Play();  // 헬기 이동 시작
                helicopterMinigunParticle.ParticleStart();  // 헬기 탄 이펙트 시작
                LeftMuzzle.SetActive(true);  // 좌측 총구 시작
                RightMuzzle.SetActive(true);  // 우측 총구 시작
                Helicopter.transform.rotation = Quaternion.Euler(20f, -90f, 0f);  // 헬기 방향 조정
                mainCameraScript.enabled = false;           //카메라 스크립트(카메라 위치) 비활성화

                if (!isCameraMoving)
                {
                    StartCoroutine(MoveCameraSmoothly(Player.transform.position));
                }
            }
        }
    }

    // 카메라를 부드럽게 이동시키는 코루틴
    private IEnumerator MoveCameraSmoothly(Vector3 targetPosition)
    {
        isCameraMoving = true;

        float elapsedTime = 0f;
        Vector3 startingPosition = mainCamera.transform.position;
        
       while (elapsedTime < cameraMoveDuration)
        {
            mainCamera.transform.position = Vector3.Lerp(startingPosition, new Vector3(startingPosition.x, startingPosition.y + 5, startingPosition.z-4), elapsedTime / cameraMoveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        mainCamera.transform.position = new Vector3(startingPosition.x, startingPosition.y + 5, startingPosition.z-4);

        isCameraMoving = false;
    }
}
