using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MouseOver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;

    private ItemObject itemObject;
    private bool _isPointing;

    public GameObject[] _weaponPrabs;

    private void Start()
    {
        FindAnyObjectByType(typeof(ItemObject));
        itemObject = GetComponentInParent<ItemObject>(); 
        _isPointing = false;
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        _isPointing = true;

        Debug.Log("아이템 고르는 중");
        //  Debug.Log($"{itemObject.Data.Name}");
        nameText.text = $"{ItemObject.instance.Data.Name}";
        descriptionText.text = $"{ItemObject.instance.Data.Description}";
        /* if (itemObject!= null && itemObject.Data != null)
         {
             nameText.text = $"{itemObject.Data.Name}";
             descriptionText.text = $"{itemObject.Data.Description}";
         }
         else
         {
             Debug.Log("ItemObject 인스턴스 또는 Data가 null입니다.");
         }*/
        return;
    }

    public void OnPointerExit(PointerEventData pointerEvent)
    {
        _isPointing= false;
        Debug.Log("마우스 이동 중");
        nameText.text = $"Item Name";
        descriptionText.text = $"Item Descript";
    }
}
