using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class HelicopterMinigun : MonoBehaviour
{
    [SerializeField] private SplineAnimate splineAnimate;

    [SerializeField] private bool isTurned;
    public HelicopterMinigunParticle helicopterMinigunParticle;
    [SerializeField] private GameObject EffectPlane;
    [SerializeField] private GameObject LeftMuzzle;

    [SerializeField] private GameObject RightMuzzle;

    
    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = Quaternion.Euler(25f, -90f, 0f);
        splineAnimate = GetComponent<SplineAnimate>();
        helicopterMinigunParticle = GameObject.Find("ParticleArea").GetComponent<HelicopterMinigunParticle>();
        isTurned = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isTurned == false && splineAnimate.NormalizedTime >= 0.45f)
        {
            isTurned = true;
            transform.rotation = Quaternion.Euler(20f, -270f, 0f);              //헬기 회전
        }
        
        if (isTurned == false && splineAnimate.NormalizedTime <= 0.45f)
        {
            // 나아갈때 장판 위치
            EffectPlane.transform.position = transform.position - new Vector3(20f, 7f, 0f);
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
            
            EffectPlane.GetComponent<HelicopterMinigunParticle>().ParticleStop();
            //EffectPlane.SetActive(false);
            LeftMuzzle.SetActive(false);
            RightMuzzle.SetActive(false);

        }
    }
}
