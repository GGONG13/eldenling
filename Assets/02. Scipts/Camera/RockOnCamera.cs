using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    public Transform player; // 플레이어의 Transform
    public Transform targetEnemy; // 타겟할 적의 Transform
    public float cameraDistance = 10.0f; // 플레이어와 카메라 사이의 거리
    public float cameraHeight = 5.0f; // 카메라의 높이 조정

    private Vector3 cameraOffset; // 카메라의 오프셋

    void Start()
    {
        // 카메라의 초기 오프셋 계산
        cameraOffset = new Vector3(0, cameraHeight, -cameraDistance);
    }

    void LateUpdate()
    {
        if (targetEnemy != null)
        {
            // 플레이어가 적을 바라보도록 회전
            player.LookAt(targetEnemy);

            // 카메라가 플레이어를 따라가도록 설정하되, 적이 화면의 중앙에 위치하도록 조정
            Vector3 cameraPosition = player.position + player.rotation * cameraOffset;
            transform.position = cameraPosition;
            transform.LookAt(targetEnemy); // 카메라는 적을 중심으로 설정
        }
    }
}
