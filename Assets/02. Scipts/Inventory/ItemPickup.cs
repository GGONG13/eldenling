using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public ItemData Item;

    public void Pickup()
    {
        InventoryManager.Instance.Add(Item);
        Inventory.instance.InventoryDescriptionUI.Refresh(Item);
       // InventoryManager.Instance.ListItem();
        this.gameObject.SetActive(false);
    }

    private void OnMouseDown()
    {
        Pickup();
    }
}
