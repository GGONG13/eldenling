using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPSCamera : MonoBehaviour
{
    public Transform target; // ī�޶� ����ٴ� ��� ĳ������ Transform
    public float distance = 2.0f; // ī�޶�� ĳ���� ���� �Ÿ�
    public float height = 1.0f; // ī�޶��� ����
    public float smoothSpeed = 0.125f; // ī�޶� �̵��� �ε巴�� �ϱ� ���� �ӵ�

    private Vector3 offset; // �ʱ� ��ġ

    void Start()
    {
        offset = new Vector3(0, height, -distance); // �ʱ� ��ġ ����
    }

    void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset; // ���ϴ� ��ġ ���
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed); // �ε巯�� �̵� ���
        transform.position = smoothedPosition;

        transform.LookAt(target); // ĳ���͸� �ٶ󺸵��� ����
    }
}
