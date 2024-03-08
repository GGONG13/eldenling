using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Item : MonoBehaviour
{
    public ItemObject itemObject;
    public TextMeshProUGUI ItemNameUI;
    public TextMeshProUGUI ItemDescriptUI;
    public ItemObject[] currentItem; // 현재 UI에 표시할 ItemObject의 참조

    public void Refresh()
    {
        ItemNameUI.text = $"Name : {itemObject.Data.Name}";
        ItemDescriptUI.text = $"Descript : {itemObject.Data.Description}";
    }
 /*   public void OnPointerEnter(PointerEventData eventData)
    {
        foreach (var item in currentItem)
        {
            Refresh(item);
            return;
        }
    }*/

    public void OnPointerExit(PointerEventData eventData)
    {
        // 아이템 정보 표시를 초기 상태로 되돌림
        ClearInfo(); // UI 정보를 초기화하는 메서드
    }

    public void ClearInfo()
    {
        ItemNameUI.text = $"Item Name";
        ItemDescriptUI.text = $"Item Descript";
    }
}
