using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class HelicopterMinigunTrigger : MonoBehaviour
{
    public GameObject Helicopter;
    
    public GameObject EffectField;
    public HelicopterMinigunParticle helicopterMinigunParticle;

    public GameObject LeftMuzzle;

    public GameObject RightMuzzle;

    private void Start()
    {
        helicopterMinigunParticle = GameObject.Find("ParticleArea").GetComponent<HelicopterMinigunParticle>();
        LeftMuzzle = GameObject.Find("HelLeftMuzzles");
        RightMuzzle = GameObject.Find("HelRightMuzzles");
        
        LeftMuzzle.SetActive(false);
        RightMuzzle.SetActive(false);
    }

    public void OnTriggerEnter(Collider other)
    {
        // 충돌한 물체가 Player 태그를 가진 경우
        if (other.CompareTag("Player"))
        {
            SplineAnimate spline = Helicopter.GetComponent<SplineAnimate>();
            if (spline.NormalizedTime == 1f)        //스킬이 끝나면 다시 준비
            {
                spline.NormalizedTime = 0;
            }
            if (spline.IsPlaying != true)           //이미 스킬 실행중이면 무시
            {
                spline.Play();
                helicopterMinigunParticle.ParticleStart();
                LeftMuzzle.SetActive(true);
                RightMuzzle.SetActive(true);
                Helicopter.transform.rotation = Quaternion.Euler(20f, -90f, 0f);
            }
        }
    }
}
