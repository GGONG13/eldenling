using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class ItemInventoryUI : MonoBehaviour, IPointerEnterHandler
{
    public TextMeshProUGUI itemIconNameText;
    public TextMeshProUGUI itemNameText;
    public TextMeshProUGUI itemDescriptionText;
    public Image itemIconImage;
    public Image itemBigImage;
    public TextMeshProUGUI countitemText;
    public ItemData CurrentitemData;

    public void OnPointerEnter(PointerEventData eventData)
    {
        UpdateItemUI();
    }

    public void OnMouseDown()
    {
        ChangeWeapon();
        Time.timeScale = 1.0f;
        if (CurrentitemData.Type == ItemType.Potion)
        {
            InventoryManager.Instance.Remove(CurrentitemData);
            Player player = GetComponent<Player>();
            player.Heal(10);
        }
    }
    public void SetItemData(ItemData itemData)
    {
        if (itemData == null)
        {
            return;
        }

        CurrentitemData = itemData;
    }
    public void UpdateItemUI()
    {
        // CurrentitemData = InventoryManager.Instance.itemDic[id];
        Inventory.instance.InventoryDescriptionUI.Refresh(CurrentitemData);
    }
    public void ChangeWeapon()
    {
        if (CurrentitemData == null)
        {
            return;
        }

        FindObjectOfType<Player>().ActivateItem(CurrentitemData);
        Inventory.instance.gameObject.SetActive(false);
    }

}
