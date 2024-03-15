using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public PlayerCameraController playerCameraController;

    void Update()
    {
        // 'T' Ű�� ������ �� ����
        if (Input.GetKeyDown(KeyCode.T))
        {
            // PlayerCameraController�� Ȱ��ȭ ���¸� ���
            playerCameraController.enabled = !playerCameraController.enabled;
        }
    }
}
