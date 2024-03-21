using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TPSCamera : MonoBehaviour
{
    public Transform target; // ĳ����(Transform)�� �����մϴ�.
    public float distance = 5.0f; // ī�޶�� ĳ���� ���� �Ÿ��� �����մϴ�.
    public float height = 2.0f; // ī�޶��� ���̸� �����մϴ�.
    public float offset = 1.0f; // ī�޶� ĳ������ ���������� �������� �Ÿ��� �����մϴ�.
    public float rotationSpeed = 5.0f; // ȸ�� �ӵ��� �����մϴ�.

    private float currentRotationAngle = 0.0f;
    private float currentHeight = 0.0f;
    private bool isDragging = false; // ���콺 ���� ��ư�� ���ȴ��� ���θ� �����մϴ�.
    private Vector3 dragStartPosition; // �巡�׸� ������ ���콺 ��ġ�� �����մϴ�.

    void Update()
    {
        CameraRotation();
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
        }
    }
}
