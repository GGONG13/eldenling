using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_InventoryDescription : MonoBehaviour
{
    public Image BigImageUI;
    public TextMeshProUGUI NameTextUI;
    public TextMeshProUGUI DescriptionTextUI;
    public List<ItemInventoryUI> ItemInventoryUISlots;

    public void Refresh(ItemData itemData)
    {
        NameTextUI.text = itemData.Name;
        DescriptionTextUI.text = itemData.Description;
        BigImageUI.sprite = itemData.BigImage;

    }
}
