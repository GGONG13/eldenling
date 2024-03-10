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
    public ItemObject[] currentItem; // ���� UI�� ǥ���� ItemObject�� ����

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
        // ������ ���� ǥ�ø� �ʱ� ���·� �ǵ���
        ClearInfo(); // UI ������ �ʱ�ȭ�ϴ� �޼���
    }

    public void ClearInfo()
    {
        ItemNameUI.text = $"Item Name";
        ItemDescriptUI.text = $"Item Descript";
    }
}
