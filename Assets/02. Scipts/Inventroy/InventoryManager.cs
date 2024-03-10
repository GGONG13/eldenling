using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Purchasing;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    public List<UI_Item> ItemsUIList;

    public List<ItemObject> HaveItems = new List<ItemObject>();

    

    public GameObject InventoryUI;
    public TextMeshProUGUI ItemName;
    public TextMeshProUGUI ItemText;
    public Image[] images;
    private int _count;

    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        InventoryUI.SetActive(false);
        _count = 2;
    }

    public void Update()
    {

    }


    public void Refersh(ItemData itemData)
    {
        int haveItemCount = HaveItems.Count;
        haveItemCount++;
        for(int i = 0; i < haveItemCount; i++) 
        {
            if (i < ItemsUIList.Count)
            {
               // ItemsUIList[i].Refresh(HaveItems[i]);
                Debug.Log($"{HaveItems[i].Data.Name}가 UI에 추가됩니다.");
            }
        }
    }

  
    public void InventoryAdd(ItemData data)
    {
        _count++;
        Debug.Log($"{ data.Name}가 추가 됩니다");
        Debug.Log($"{_count}");
        for (int i = 0; i < _count; i++)
        {
            if (!images[i].gameObject.activeInHierarchy)
            {
/*                ItemName.text = data.Name;
                ItemText.text = data.Description;*/
/*                images[i] = data.image;
                data.image.transform.position = images[i].transform.position;*/
                images[i].gameObject.SetActive(true);
            }
        }
    }



    public void OnWeaponClicked()
    {
        Debug.Log($"무기가 교체 됩니다");
        InventoryUI.SetActive(false);
        ItemObjectFactory.instance.ItemOnOff();
    }

/*    public void OnWeaponAButtonClicked()
    {
        PlayerWeapon.Instance.WeaponSwitch(PlayerWeaponState.Sword);
    }
    public void OnWeaponBButtonClicked() 
    {
        PlayerWeapon.Instance.WeaponSwitch(PlayerWeaponState.MagicWand);
    }*/
}
