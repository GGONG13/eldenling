using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Item : MonoBehaviour
{
    public Image ItemImage;
    public TextMeshPro ItemNameUI;
    public TextMeshPro ItemDescriptUI;


    public void Refresh(ItemObject item)
    {
        ItemImage = item.Data.image;
        ItemNameUI.text = item.Data.Name;
        ItemDescriptUI.text = item.Data.Description;
    }
}
