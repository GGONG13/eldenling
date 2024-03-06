using UnityEngine;

public class TPSCamera : MonoBehaviour
{
    public Transform Target; // Inspector에서 캐릭터의 Transform을 할당해야 합니다.
    public Vector3 Offset = new Vector3(0, 3f, -3f);
    public float a; // Offset의 y값을 조정할 변수

    private void LateUpdate()
    {
        if (Target != null) // Target이 null이 아닌지 확인합니다.
        {
            transform.localPosition = Target.position + Offset;
            transform.LookAt(Target);

            // 카메라 회전을 적용합니다.
            Vector2 xy = CameraManager.Instance.XY;
            transform.RotateAround(Target.position, Vector3.up, xy.x);
            transform.RotateAround(Target.position, transform.right, -xy.y);

            // 카메라 위치를 조정합니다.
            transform.localPosition = Target.position - transform.forward * Offset.magnitude + Vector3.up * (Offset.y - a);
        }
    }
}
