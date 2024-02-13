using System.Collections;
using UnityEngine;

public class FantasyAxeSkillShield : MonoBehaviour
{
    public float moveSpeed = 0.1f; // 이동 속도 조절

    private bool movingUp = true;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MoveObject());
    }

    IEnumerator MoveObject()
    {
        while (true)
        {
            if (movingUp)
            {
                transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
                if (transform.position.y >= 2.0f) // 이동 제한
                    movingUp = false;
            }
            else
            {
                transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
                if (transform.position.y <= 0.0f) // 이동 제한
                    movingUp = true;
            }

            yield return null;
        }
    }
}
