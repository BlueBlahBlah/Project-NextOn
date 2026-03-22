using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class TPSCamera : MonoBehaviour
{
    [Header("Target")]
    [SerializeField]
    private Transform target; // 캐릭터(Transform)를 지정합니다.

    [Header("Setting")]
    [SerializeField]
    private float distance = 5.0f; // 카메라와 캐릭터 간의 거리를 지정합니다.
    [SerializeField]
    private float height = 2.0f; // 카메라의 높이를 지정합니다.
    [SerializeField]
    private float offset = 1.0f; // 카메라를 캐릭터의 오른쪽으로 오프셋할 거리를 지정합니다.
    [SerializeField]
    private float rotationSpeed = 5.0f; // 회전 속도를 지정합니다.

    [Header("Shake")]
    [SerializeField]
    public bool isShake;
    [SerializeField]
    private float shakeTime = 0.1f;
    [SerializeField]
    private float shakeAmount = 0.1f;
    
    private float currentRotationAngle = 0.0f;
    private float currentHeight = 0.0f;
    private bool isDragging = false; // 마우스 왼쪽 버튼이 눌렸는지 여부를 저장합니다.
    private Vector3 dragStartPosition; // 드래그를 시작한 마우스 위치를 저장합니다.

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

        // 마우스 왼쪽 버튼이 눌린 상태이고 드래그 중인지 확인합니다.
        if (Input.GetMouseButtonDown(0))
        {
            // 마우스가 UI 요소 위에 있는지 확인합니다.
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

        // 마우스를 클릭한 상태로 드래그할 때만 카메라를 회전합니다.
        if (isDragging)
        {
            // 마우스 드래그로 카메라를 회전합니다.
            currentRotationAngle += Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime * 5f;
            currentHeight -= Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime * 5f;

            

        }
        // 회전 각도를 Quaternion으로 변환합니다.
        Quaternion rotation = Quaternion.Euler(currentHeight, currentRotationAngle, 0);

        // 회전 각도를 기준으로 거리를 조정하여 카메라 위치를 설정합니다.
        Vector3 negDistance = new Vector3(0.0f, height, -distance);
        Vector3 position = rotation * negDistance + target.position;

        // 오른쪽으로의 오프셋 벡터를 계산합니다.
        Vector3 offsetVector = Quaternion.Euler(0, currentRotationAngle, 0) * Vector3.right * offset;

        // 카메라 위치에 오른쪽 오프셋을 더합니다.
        position += offsetVector;

        // 카메라의 위치와 회전을 적용합니다.
        transform.rotation = rotation;
        transform.position = position;

        // 플레이어의 회전을 적용합니다. 이 때, rotation의 y값만 수정되도록 주의합니다.
        // 현재 임시 코드, 수정 필요
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
