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

    public TextMeshProUGUI IconNameTextUI;
    public Image IconImageUI;

    public void Refresh(ItemData itemData)
    {
        NameTextUI.text = itemData.Name;
        DescriptionTextUI.text = itemData.Description;
        BigImageUI.sprite = itemData.BigImage;
    }
}
