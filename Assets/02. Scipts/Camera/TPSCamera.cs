using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPSCamera : MonoBehaviour
{
    public Transform target; // 카메라가 따라다닐 대상 캐릭터의 Transform
    public float distance = 2.0f; // 카메라와 캐릭터 간의 거리
    public float height = 1.0f; // 카메라의 높이
    public float smoothSpeed = 0.125f; // 카메라 이동을 부드럽게 하기 위한 속도

    private Vector3 offset; // 초기 위치

    void Start()
    {
        offset = new Vector3(0, height, -distance); // 초기 위치 설정
    }

    void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset; // 원하는 위치 계산
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed); // 부드러운 이동 계산
        transform.position = smoothedPosition;

        transform.LookAt(target); // 캐릭터를 바라보도록 설정
    }
}
