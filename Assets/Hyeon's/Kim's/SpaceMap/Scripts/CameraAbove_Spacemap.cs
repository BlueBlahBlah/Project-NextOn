using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAbove_Spacemap : MonoBehaviour
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
            // ���ο� ��ġ ����
            Vector3 newPosition = new Vector3(Player.transform.position.x, Player.transform.position.y + 15f, Player.transform.position.z - 3f);
            transform.position = newPosition;

            // ȸ�� ����
            transform.rotation = Quaternion.Euler(70f, 0f, 0f);
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

        float duration = 3f; // ȸ���ϴ� �� �ɸ��� �ð� (��)
        float timer = 0f;

        Quaternion initialRotation = transform.rotation;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float progress = timer / duration;
            transform.rotation = Quaternion.Slerp(initialRotation, targetRotation, progress);
            yield return null;
        }

        transform.rotation = targetRotation; // ȸ���� �Ϸ�Ǹ� ��Ȯ�� �������� ����
        Invoke("CameraReturn", 2f);
    }

}
