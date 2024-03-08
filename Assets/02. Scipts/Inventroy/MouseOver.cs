using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class MouseOver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{


    private bool _isPointing;

    public GameObject[] _weaponPrabs;

    public ItemObject currentItem;

    private void Start()
    {
        FindAnyObjectByType(typeof(ItemObject));
        _isPointing = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _isPointing = true;
        Debug.Log("아이템 고르는 중");
        if (currentItem != null)
        {
            UI_Item uiItem = GetComponent<UI_Item>();
            if (uiItem != null)
            {
                uiItem.Refresh(); 
            }
        }
        else
        {
            Debug.Log("currentItem이 설정되지 않았습니다.");
        }
        return;
    }

    public void PointerEnterAction()
    {
        UI_Item item = GetComponent<UI_Item>();
        // item.Refresh(currentItem);
        Debug.Log(0);
    }
    public void OnPointerExit(PointerEventData pointerEvent)
    {
        _isPointing = false;
        Debug.Log("마우스 이동 중");
    }
}

  //       nameText.text = $"Item Name";
    //    descriptionText.text = $"Item Descript";*
    

