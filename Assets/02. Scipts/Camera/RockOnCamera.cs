using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    public Transform player; // 플레이어의 Transform
    public Transform targetEnemy; // 타겟할 적의 Transform
    public float cameraDistance = 10.0f; // 플레이어와 카메라 사이의 거리
    public float cameraHeight = 5.0f; // 카메라의 높이 조정
    public float detectionRadius = 15.0f; // 타겟 탐지 범위

    private Vector3 cameraOffset; // 카메라의 오프셋

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
