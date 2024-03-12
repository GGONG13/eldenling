using UnityEngine;
using static UnityEditor.Progress;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public float pickupRange = 2.0f; // 아이템을 줍기 위한 최대 거리

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
                break; // 첫 번째로 발견한 아이템을 줍고 종료
            }
        }
    }
}
