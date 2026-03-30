using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Splines;

public class HelicopterMinigun : MonoBehaviour
{
    [SerializeField] private SplineAnimate splineAnimate;   //?щ━肄ν꽣 ?대룞 愿由?而댄룷?뚰듃

    [SerializeField] private bool isTurned;                 //?ш린 ?뚯쟾 議곗젙 蹂??    
    public HelicopterMinigunParticle helicopterMinigunParticle;     //?ш린 ???댄럺???ㅽ겕由쏀듃
    [SerializeField] private GameObject EffectPlane;        //?ш린 ?댄럺???ν뙋
    [SerializeField] private GameObject LeftMuzzle;         //?ш린 醫뚯륫 珥앷뎄
    [SerializeField] private GameObject RightMuzzle;        //?ш린 ?곗륫 珥앷뎄
    public Camera mainCamera;
    private CameraAbove mainCameraScript;
    public GameObject Player;
    
    private Coroutine cameraCoroutine;
    
    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = Quaternion.Euler(25f, -90f, 0f);       //?ш린 ?꾩튂議곗젙
        splineAnimate = GetComponent<SplineAnimate>();                     //而댄룷?뚰듃 媛?몄삤湲?        helicopterMinigunParticle = GameObject.Find("ParticleArea").GetComponent<HelicopterMinigunParticle>();      //ParticleArea ?ㅻ툕?앺듃
        isTurned = false;           //蹂??珥덇린??        mainCamera = Camera.main;
        mainCameraScript = mainCamera.GetComponent<CameraAbove>();
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (isTurned == false && splineAnimate.NormalizedTime <= 0.45f)
        { // ?섏븘媛덈븣 ?ν뙋 ?꾩튂
            EffectPlane.transform.position = transform.position - new Vector3(20f, 7f, 0f);
        }
        
        if (isTurned == false && splineAnimate.NormalizedTime >= 0.45f)
        {
            isTurned = true;
            transform.rotation = Quaternion.Euler(20f, -270f, 0f);              //?ш린 ?뚯쟾
        }
        
        if (isTurned == true && splineAnimate.NormalizedTime >= 0.45f)
        {
            //?섎룎?꾩삱???ν뙋???꾩튂
            EffectPlane.transform.position = transform.position + new Vector3(20f, -7f, 0f);
        }

        if (splineAnimate.NormalizedTime == 1f)         //?ㅽ궗 醫낅즺
        {
            isTurned = false;
            transform.rotation = Quaternion.Euler(25f, -90f, 0f);               //?ш린 ?뚯쟾
            EffectPlane.transform.position = transform.position - new Vector3(20f, 7f, 0f); //?ν뙋???뚯쟾
            
            EffectPlane.GetComponent<HelicopterMinigunParticle>().ParticleStop();       //?댄럺??洹몃쭔
            //?ш린 珥앷뎄 洹몃쭔
            //LeftMuzzle.SetActive(false);
            //RightMuzzle.SetActive(false);
            //移대찓???먮옒 ?꾩튂濡?遺?쒕읇寃?--- ?뚯븘?ㅻ뒗 肄붾（?댁쓽 ?댁깋???쇰줈 ?ъ슜x
            //originalCameraPosition();
            //移대찓???ㅽ겕由쏀듃 ?ъ떆??            mainCameraScript.enabled = true;
            //PlayerSoundManager.Instance.StopSound();
            //PlayerSoundManager.Instance.StopSound2();
            
        }
    }
}
