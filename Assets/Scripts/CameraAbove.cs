using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraAbove : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private Transform BigMonLocation;
    public bool operating;
    // Start is called before the first frame update
    void Start()
    {
        operating = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (operating == false)
        {
            // ?덈줈???꾩튂 ?ㅼ젙
            Vector3 newPosition = new Vector3(Player.transform.position.x, Player.transform.position.y + 12f, Player.transform.position.z - 6f);
            transform.position = newPosition;

            // ?뚯쟾 ?ㅼ젙
            transform.rotation = Quaternion.Euler(55f, 0f, 0f);
        }
        
    }

    public void LookBigMonster()
    {
        operating = true;
        StartCoroutine(LookAtBigMonsterCoroutine());
    }

    IEnumerator LookAtBigMonsterCoroutine()
    {
        Vector3 targetDirection = (BigMonLocation.position - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);

        float duration = 3f; // ?뚯쟾?섎뒗 ??嫄몃━???쒓컙 (珥?
        float timer = 0f;

        Quaternion initialRotation = transform.rotation;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float progress = timer / duration;
            transform.rotation = Quaternion.Slerp(initialRotation, targetRotation, progress);
            yield return null;
        }

        transform.rotation = targetRotation; // ?뚯쟾???꾨즺?섎㈃ ?뺥솗??諛⑺뼢?쇰줈 ?ㅼ젙
        Invoke("CameraReturn", 2f);
    }

    
}
