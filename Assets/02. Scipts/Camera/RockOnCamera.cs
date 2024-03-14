using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    public Transform player; // �÷��̾��� Transform
    public Transform targetEnemy; // Ÿ���� ���� Transform
    public float cameraDistance = 10.0f; // �÷��̾�� ī�޶� ������ �Ÿ�
    public float cameraHeight = 5.0f; // ī�޶��� ���� ����

    private Vector3 cameraOffset; // ī�޶��� ������

    void Start()
    {
        // ī�޶��� �ʱ� ������ ���
        cameraOffset = new Vector3(0, cameraHeight, -cameraDistance);
    }

    void LateUpdate()
    {
        if (targetEnemy != null)
        {
            // �÷��̾ ���� �ٶ󺸵��� ȸ��
            player.LookAt(targetEnemy);

            // ī�޶� �÷��̾ ���󰡵��� �����ϵ�, ���� ȭ���� �߾ӿ� ��ġ�ϵ��� ����
            Vector3 cameraPosition = player.position + player.rotation * cameraOffset;
            transform.position = cameraPosition;
            transform.LookAt(targetEnemy); // ī�޶�� ���� �߽����� ����
        }
    }
}
