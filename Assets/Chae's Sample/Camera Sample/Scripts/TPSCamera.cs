using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class TPSCamera : MonoBehaviour
{
    [Header("Target")]
    [SerializeField]
    private Transform target; // ĳ����(Transform)�� �����մϴ�.

    [Header("Setting")]
    [SerializeField]
    private float distance = 5.0f; // ī�޶�� ĳ���� ���� �Ÿ��� �����մϴ�.
    [SerializeField]
    private float height = 2.0f; // ī�޶��� ���̸� �����մϴ�.
    [SerializeField]
    private float offset = 1.0f; // ī�޶� ĳ������ ���������� �������� �Ÿ��� �����մϴ�.
    [SerializeField]
    private float rotationSpeed = 5.0f; // ȸ�� �ӵ��� �����մϴ�.

    [Header("Shake")]
    [SerializeField]
    public bool isShake;
    [SerializeField]
    private float shakeTime = 0.1f;
    [SerializeField]
    private float shakeAmount = 0.1f;
    
    private float currentRotationAngle = 0.0f;
    private float currentHeight = 0.0f;
    private bool isDragging = false; // ���콺 ���� ��ư�� ���ȴ��� ���θ� �����մϴ�.
    private Vector3 dragStartPosition; // �巡�׸� ������ ���콺 ��ġ�� �����մϴ�.

    public bool isCameraMove;

    private void Start()
    {
        isCameraMove = true;
    }

    void Update()
    {
        if (isCameraMove)
        {
            CameraRotation();
        }
        if (isShake)
        {
            StartCoroutine("CameraShaking");
        }
    }

    public void CameraRotation()
    {
        if (!target)
            return;

        // ���콺 ���� ��ư�� ���� �����̰� �巡�� ������ Ȯ���մϴ�.
        if (Input.GetMouseButtonDown(0))
        {
            // ���콺�� UI ��� ���� �ִ��� Ȯ���մϴ�.
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                isDragging = true;
                dragStartPosition = Input.mousePosition;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        // ���콺�� Ŭ���� ���·� �巡���� ���� ī�޶� ȸ���մϴ�.
        if (isDragging)
        {
            // ���콺 �巡�׷� ī�޶� ȸ���մϴ�.
            currentRotationAngle += Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime * 5f;
            currentHeight -= Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime * 5f;

            

        }
        // ȸ�� ������ Quaternion���� ��ȯ�մϴ�.
        Quaternion rotation = Quaternion.Euler(currentHeight, currentRotationAngle, 0);

        // ȸ�� ������ �������� �Ÿ��� �����Ͽ� ī�޶� ��ġ�� �����մϴ�.
        Vector3 negDistance = new Vector3(0.0f, height, -distance);
        Vector3 position = rotation * negDistance + target.position;

        // ������������ ������ ���͸� ����մϴ�.
        Vector3 offsetVector = Quaternion.Euler(0, currentRotationAngle, 0) * Vector3.right * offset;

        // ī�޶� ��ġ�� ������ �������� ���մϴ�.
        position += offsetVector;

        // ī�޶��� ��ġ�� ȸ���� �����մϴ�.
        transform.rotation = rotation;
        transform.position = position;

        // �÷��̾��� ȸ���� �����մϴ�. �� ��, rotation�� y���� �����ǵ��� �����մϴ�.
        // ���� �ӽ� �ڵ�, ���� �ʿ�
        Quaternion playerRotation = Quaternion.Euler(0, currentRotationAngle, 0);
        target.rotation = playerRotation;


    }

    IEnumerator CameraShaking()
    {
        Vector3 camPos = transform.position;

        float _timer = 0f;

        while (_timer <= shakeTime)
        {
            transform.position = Random.insideUnitSphere * shakeAmount + camPos;
            yield return null;

            _timer += Time.deltaTime;
        }

        transform.position = camPos;
        isShake = false;
    }
}
