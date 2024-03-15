using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    public Transform player; // �÷��̾��� Transform
    public Transform targetEnemy; // Ÿ���� ���� Transform
    public float cameraDistance = 10.0f; // �÷��̾�� ī�޶� ������ �Ÿ�
    public float cameraHeight = 5.0f; // ī�޶��� ���� ����
    public float detectionRadius = 15.0f; // Ÿ�� Ž�� ����

    private Vector3 cameraOffset; // ī�޶��� ������

    void Start()
    {
        cameraOffset = new Vector3(0, cameraHeight, -cameraDistance);
    }

    void OnEnable()
    {
        FindClosestEnemy();
    }

    void LateUpdate()
    {
        if (targetEnemy != null)
        {
            player.LookAt(targetEnemy);
            Vector3 cameraPosition = player.position + player.rotation * cameraOffset;
            transform.position = cameraPosition;
            transform.LookAt(targetEnemy);
        }
    }

    void FindClosestEnemy()
    {
        Collider[] hitColliders = Physics.OverlapSphere(player.position, detectionRadius);
        float closestDistance = detectionRadius;
        Transform closestEnemy = null;

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy"))
            {
                float distance = Vector3.Distance(player.position, hitCollider.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestEnemy = hitCollider.transform;
                }
            }
        }

        targetEnemy = closestEnemy;
    }
}
