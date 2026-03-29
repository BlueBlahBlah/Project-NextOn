using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Splines;

public class HelicopterMinigun : MonoBehaviour
{
    [SerializeField] private SplineAnimate splineAnimate;   //?¬ë¦¬ì½¥í„° ?´ë™ ê´€ë¦?ì»´í¬?ŒíŠ¸

    [SerializeField] private bool isTurned;                 //?¬ê¸° ?Œì „ ì¡°ì • ë³€??    
    public HelicopterMinigunParticle helicopterMinigunParticle;     //?¬ê¸° ???´í™???¤í¬ë¦½íŠ¸
    [SerializeField] private GameObject EffectPlane;        //?¬ê¸° ?´í™???¥íŒ
    [SerializeField] private GameObject LeftMuzzle;         //?¬ê¸° ì¢Œì¸¡ ì´êµ¬
    [SerializeField] private GameObject RightMuzzle;        //?¬ê¸° ?°ì¸¡ ì´êµ¬
    public Camera mainCamera;
    private CameraAbove mainCameraScript;
    public GameObject Player;
    
    private Coroutine cameraCoroutine;
    
    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = Quaternion.Euler(25f, -90f, 0f);       //?¬ê¸° ?„ì¹˜ì¡°ì •
        splineAnimate = GetComponent<SplineAnimate>();                     //ì»´í¬?ŒíŠ¸ ê°€?¸ì˜¤ê¸?        helicopterMinigunParticle = GameObject.Find("ParticleArea").GetComponent<HelicopterMinigunParticle>();      //ParticleArea ?¤ë¸Œ?íŠ¸
        isTurned = false;           //ë³€??ì´ˆê¸°??        mainCamera = Camera.main;
        mainCameraScript = mainCamera.GetComponent<CameraAbove>();
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (isTurned == false && splineAnimate.NormalizedTime <= 0.45f)
        { // ?˜ì•„ê°ˆë•Œ ?¥íŒ ?„ì¹˜
            EffectPlane.transform.position = transform.position - new Vector3(20f, 7f, 0f);
        }
        
        if (isTurned == false && splineAnimate.NormalizedTime >= 0.45f)
        {
            isTurned = true;
            transform.rotation = Quaternion.Euler(20f, -270f, 0f);              //?¬ê¸° ?Œì „
        }
        
        if (isTurned == true && splineAnimate.NormalizedTime >= 0.45f)
        {
            //?˜ëŒ?„ì˜¬???¥íŒ???„ì¹˜
            EffectPlane.transform.position = transform.position + new Vector3(20f, -7f, 0f);
        }

        if (splineAnimate.NormalizedTime == 1f)         //?¤í‚¬ ì¢…ë£Œ
        {
            isTurned = false;
            transform.rotation = Quaternion.Euler(25f, -90f, 0f);               //?¬ê¸° ?Œì „
            EffectPlane.transform.position = transform.position - new Vector3(20f, 7f, 0f); //?¥íŒ???Œì „
            
            EffectPlane.GetComponent<HelicopterMinigunParticle>().ParticleStop();       //?´í™??ê·¸ë§Œ
            //?¬ê¸° ì´êµ¬ ê·¸ë§Œ
            //LeftMuzzle.SetActive(false);
            //RightMuzzle.SetActive(false);
            //ì¹´ë©”???ë˜ ?„ì¹˜ë¡?ë¶€?œëŸ½ê²?--- ?Œì•„?¤ëŠ” ì½”ë£¨?´ì˜ ?´ìƒ‰???¼ë¡œ ?¬ìš©x
            //originalCameraPosition();
            //ì¹´ë©”???¤í¬ë¦½íŠ¸ ?¬ì‹œ??            mainCameraScript.enabled = true;
            //PlayerSoundManager.Instance.StopSound();
            //PlayerSoundManager.Instance.StopSound2();
            
        }
    }
}
