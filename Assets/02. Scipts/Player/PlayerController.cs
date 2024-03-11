using UnityEngine;
using static UnityEditor.Progress;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public float pickupRange = 2.0f; // �������� �ݱ� ���� �ִ� �Ÿ�

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            PickupItem();
        }
    }

    void PickupItem()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, pickupRange);
        foreach (var hitCollider in hitColliders)
        {
            ItemPickup item = hitCollider.GetComponent<ItemPickup>();
            if (item != null)
            {
                item.Pickup();
                break; // ù ��°�� �߰��� �������� �ݰ� ����
            }
        }
    }
}
