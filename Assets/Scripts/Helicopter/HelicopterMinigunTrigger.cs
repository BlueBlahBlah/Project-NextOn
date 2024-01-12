using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class HelicopterMinigunTrigger : MonoBehaviour
{
    public GameObject Helicopter;
    public void OnTriggerEnter(Collider other)
    {
        // 충돌한 물체가 Player 태그를 가진 경우
        if (other.CompareTag("Player"))
        {
            SplineAnimate spline = Helicopter.GetComponent<SplineAnimate>();
            if (spline.NormalizedTime == 1f)
            {
                spline.NormalizedTime = 0;
            }
            spline.Play();
            
            Helicopter.transform.rotation = Quaternion.Euler(20f, -90f, 0f);

        }
    }
}
