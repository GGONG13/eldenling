using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UI;

public class ItemPickup : MonoBehaviour
{
    public ItemData Item;

    public float _itemSpeed = 5f;
    public Transform Player;
    private bool isCollecting = false;



    private void Update()
    {
        float distance = Vector3.Distance(transform.position, Player.position);

        if (distance <= 3)
        {
            isCollecting = true;
            if (isCollecting)
            {
                Pickup();
            }
        }
    }
    public void Pickup()
    {
        StartCoroutine(Magnet_Coroutine());
    }

    private void OnMouseDown()
    {
        Pickup();
    }

    IEnumerator Magnet_Coroutine()
    {
        float startTime = Time.time;
        Vector3 startPosition = transform.position;
        while (Vector3.Distance(transform.position, Player.position) > 0.01f) // 거리가 충분히 가까워질 때까지 반복
        {
            float timeSinceStarted = Time.time - startTime;
            float fractionOfJourney = timeSinceStarted * _itemSpeed;

            transform.position = Vector3.Slerp(startPosition, Player.position, fractionOfJourney / Vector3.Distance(startPosition, Player.position));

            yield return null;
        }
        UI_PopUPItem.Instance.ShowItemPopUp(Item.Icon, Item.Name);
        // 아이템 추가 및 인벤토리 업데이트
        InventoryManager.Instance.Add(Item);
        InventoryManager.Instance.ListItem();
        gameObject.SetActive(false);
    }
}
