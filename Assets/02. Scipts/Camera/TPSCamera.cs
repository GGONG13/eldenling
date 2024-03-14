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
    public Transform target; // ī�޶� ����ٴ� ��� ĳ������ Transform
    public float distance = 2.0f; // ī�޶�� ĳ���� ���� �Ÿ�
    public float height = 1.0f; // ī�޶��� ����
    public float smoothSpeed = 0.125f; // ī�޶� �̵��� �ε巴�� �ϱ� ���� �ӵ�
    public float sensitivity = 2.0f; // ī�޶� ȸ�� ����

    private float rotationX = 0.0f;
    private float rotationY = 0.0f;

    private Vector3 offset; // �ʱ� ��ġ



    void Start()
    {
        offset = new Vector3(0, height, -distance); // �ʱ� ��ġ ����
        Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate()
    {
        rotationX += Input.GetAxis("Mouse X") * sensitivity;
        rotationY -= Input.GetAxis("Mouse Y") * sensitivity;
        rotationY = Mathf.Clamp(rotationY, -90f, 90f); // ���� ȸ�� ���� ����

        Quaternion targetRotation = Quaternion.Euler(rotationY, rotationX, 0); // ī�޶� ȸ���� ���
        Vector3 targetPosition = target.position + targetRotation * offset; // Ÿ�� ������ ��ġ ���

        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed); // �ε巯�� �̵� ���
        transform.LookAt(target.position); // ĳ���͸� �ٶ󺸵��� ����
    }
}
