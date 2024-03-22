using System.Collections;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public ItemData Item;

    public float _itemSpeed = 10f;
    public Transform Player;
    private bool isCollecting = false;
    private bool _isPickable = false;

    private void Start()
    {
        _isPickable = true;
        Player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void Update()
    {
        if (_isPickable)
        {
            float distance = Vector3.Distance(transform.position, Player.position);
            if (distance <= 3 && !isCollecting)
            {
                isCollecting = true;
                DontshowItem();
              //  Pickup();
            }
        }
        else
        {
            return;
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
        _isPickable = false;
        // 아이템 추가 및 인벤토리 업데이트
        InventoryManager.Instance.Add(Item);
        InventoryManager.Instance.ListItem();
        gameObject.SetActive(false);
    }
    void DontshowItem()
    {
        UI_PopUPItem.Instance.ShowItemPopUp(Item.Icon, Item.Name);
        InventoryManager.Instance.Add(Item);
        InventoryManager.Instance.ListItem();
        gameObject.SetActive(false);
    }
}
