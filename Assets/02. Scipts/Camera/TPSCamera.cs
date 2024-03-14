using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CameraType
{
    TPS,
    Target,
}
public class TPSCamera : MonoBehaviour
{
    public Transform target; // 카메라가 따라다닐 대상 캐릭터의 Transform
    public float distance = 2.0f; // 카메라와 캐릭터 간의 거리
    public float height = 1.0f; // 카메라의 높이
    public float smoothSpeed = 0.125f; // 카메라 이동을 부드럽게 하기 위한 속도
    public float sensitivity = 2.0f; // 카메라 회전 감도

    private float rotationX = 0.0f;
    private float rotationY = 0.0f;

    private Vector3 offset; // 초기 위치



    void Start()
    {
        offset = new Vector3(0, height, -distance); // 초기 위치 설정
        Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate()
    {
        rotationX += Input.GetAxis("Mouse X") * sensitivity;
        rotationY -= Input.GetAxis("Mouse Y") * sensitivity;
        rotationY = Mathf.Clamp(rotationY, -90f, 90f); // 상하 회전 각도 제한

        Quaternion targetRotation = Quaternion.Euler(rotationY, rotationX, 0); // 카메라 회전값 계산
        Vector3 targetPosition = target.position + targetRotation * offset; // 타겟 주위의 위치 계산

        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed); // 부드러운 이동 계산
        transform.LookAt(target.position); // 캐릭터를 바라보도록 설정
    }
}
