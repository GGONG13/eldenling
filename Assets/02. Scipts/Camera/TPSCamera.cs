using UnityEngine;

public class TPSCamera : MonoBehaviour
{
    public Transform Target; // Inspector���� ĳ������ Transform�� �Ҵ��ؾ� �մϴ�.
    public Vector3 Offset = new Vector3(0, 3f, -3f);
    public float a; // Offset�� y���� ������ ����

    private void LateUpdate()
    {
        if (Target != null) // Target�� null�� �ƴ��� Ȯ���մϴ�.
        {
            transform.localPosition = Target.position + Offset;
            transform.LookAt(Target);

            // ī�޶� ȸ���� �����մϴ�.
            Vector2 xy = CameraManager.Instance.XY;
            transform.RotateAround(Target.position, Vector3.up, xy.x);
            transform.RotateAround(Target.position, transform.right, -xy.y);

            // ī�޶� ��ġ�� �����մϴ�.
            transform.localPosition = Target.position - transform.forward * Offset.magnitude + Vector3.up * (Offset.y - a);
        }
    }
}
