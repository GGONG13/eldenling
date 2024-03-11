using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static UnityEditor.Progress;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public List<ItemData> items = new List<ItemData>();
    public Dictionary<int, ItemData> itemDic = new Dictionary<int, ItemData>();

    public Transform itemContect;
    public GameObject InventoryItem;

    public List<ItemInventoryUI> ItemInventoryUISlots;
    public Dictionary<int, GameObject> Swords = new Dictionary<int, GameObject>();
    public Dictionary<int, GameObject> Shields = new Dictionary<int, GameObject>();

    private void Start()
    {
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void Add(ItemData item)
    {
        items.Add(item);

        ItemInventoryUI itemUI = Instantiate(InventoryItem, itemContect).GetComponent<ItemInventoryUI>();
        itemUI.SetItemData(item);
    }
    public void Remove(ItemData item)
    {
        items.Remove(item);

    }
    public void ListItem()
    {
        foreach (Transform child in itemContect)
        {
            //Destroy(child.gameObject);
            child.gameObject.SetActive(false);
        }

        for (int i = 0; i < items.Count; i++)
        {
            ItemInventoryUISlots[i].gameObject.SetActive(true);
            ItemInventoryUISlots[i].itemIconNameText.text = items[i].Name;
            ItemInventoryUISlots[i].itemNameText.text = items[i].Name;
            ItemInventoryUISlots[i].itemDescriptionText.text = items[i].Description;
            ItemInventoryUISlots[i].itemIconImage.sprite = items[i].Icon;
            ItemInventoryUISlots[i].itemBigImage.sprite = items[i].BigImage;
            ItemInventoryUISlots[i].CurrentitemData = items[i];
        }
    }
    public void ActivateItem(int id, ItemType itemType)
    {
        // Sword 아이템 활성화
        if (itemType == ItemType.Sword)
        {
            /*foreach (var sword in Swords.Values)
            {
                sword.SetActive(false); // 모든 Sword를 비활성화
            }*/
            if (Swords.ContainsKey(id))
            {
                Swords[id].SetActive(true); // 선택된 ID의 Sword만 활성화
            }
        }
        // Shield 아이템 활성화
        else if (itemType == ItemType.Shield)
        {
            /*foreach (var shield in Shields.Values)
            {
                shield.SetActive(false); // 모든 Shield를 비활성화
            }*/
            if (Shields.ContainsKey(id))
            {
                Shields[id].SetActive(true); // 선택된 ID의 Shield만 활성화
            }
        }
    }
}
