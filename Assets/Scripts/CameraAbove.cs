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
            // 새로운 위치 설정
            Vector3 newPosition = new Vector3(Player.transform.position.x, Player.transform.position.y + 12f, Player.transform.position.z - 6f);
            transform.position = newPosition;

            // 회전 설정
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

        float duration = 3f; // 회전하는 데 걸리는 시간 (초)
        float timer = 0f;

        Quaternion initialRotation = transform.rotation;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float progress = timer / duration;
            transform.rotation = Quaternion.Slerp(initialRotation, targetRotation, progress);
            yield return null;
        }

        transform.rotation = targetRotation; // 회전이 완료되면 정확한 방향으로 설정
        Invoke("CameraReturn", 2f);
    }

    private void CameraReturn()
    {
        operating = false;
        //멈췄던 몬스터들 다시 시작
        GameObject.Find("Player").GetComponent<PlayerSpec>().ResumeMonsters();
    }
}
