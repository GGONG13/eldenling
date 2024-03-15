using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public PlayerCameraController playerCameraController;

    void Update()
    {
        // 'T' 키를 눌렀을 때 실행
        if (Input.GetKeyDown(KeyCode.T))
        {
            // PlayerCameraController의 활성화 상태를 토글
            playerCameraController.enabled = !playerCameraController.enabled;
        }
    }
}
