using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleToBoss : MonoBehaviour
{
    Rigidbody _rigid;
    BoxCollider _boxCollider;
    public bool isEnter = false;
    public bool isPlay = false;
    // 인자 확인용 참조
    public Area3_TakeArgument Arg_int;
    public Area3_TakeArgument Arg_double;
    public Area3_TakeArgument Arg_char;
    public Area3_TakeArgument Arg_void;
    // 회전체
    public GameObject rotationObj; // 회전할 물체
    public float rotationSpeed = 50f; // 회전 속도
    public GameObject[] objectsToDisable; // 비활성화할 오브젝트 배열
    public GameObject portalToBoss; // portalToBoss 오브젝트
    private ParticleSystem portalToBossParticleSystem; // portalToBoss의 ParticleSystem


    // Start is called before the first frame update
    void Start()
    {
        portalToBoss.SetActive(false); // portalToBoss 초기화

        // portalToBoss의 ParticleSystem 초기화
        portalToBossParticleSystem = portalToBoss.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        // Rotate 함수로 회전효과 
        rotationObj.transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);

        // 인자 확인
        if(Arg_void.isTakeArg)
        {
            isEnter = true;
        }

        if(isEnter)
        {
            // 비활성화할 오브젝트 비활성화
            foreach (GameObject obj in objectsToDisable)
            {
                obj.SetActive(false);
            }

            ShowPortalToBoss();
        }

        if(portalToBossParticleSystem != null)
        {
            if (!portalToBossParticleSystem.isPlaying)
            {
                portalToBossParticleSystem.Play(); // 파티클 시스템 재생
            }
        }
    }

    private void ShowPortalToBoss()
    {
        portalToBoss.SetActive(true); // showProgress 활성화
        if(!isPlay)
        {
            isPlay = true;
            // MapSoundManager.Instance.EndProgress_Sound();
            SoundManager.instance.PlayEffectSound("활설화End");
        }
    }
}
