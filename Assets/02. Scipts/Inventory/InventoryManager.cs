using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static UnityEditor.Progress;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public List<ItemData> items = new List<ItemData>();

    public GameObject Inventory;

    public Transform itemContect;
    public GameObject InventoryItem;

    public List<ItemInventoryUI> ItemInventoryUISlots;



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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)) 
        {
            bool isActive = !Inventory.activeSelf;
            Inventory.SetActive(isActive); // 인벤토리 UI 활성화/비활성화 토글
            // 인벤토리가 활성화되면 마우스 커서를 표시하고, 그렇지 않으면 숨깁니다.
            UnityEngine.Cursor.visible = isActive;

            // 인벤토리가 활성화되면 마우스 커서를 잠그지 않고, 그렇지 않으면 잠급니다.
            UnityEngine.Cursor.lockState = isActive ? CursorLockMode.None : CursorLockMode.Locked;

            Time.timeScale = isActive ? 0 : 1;

        }
    }
    public void Add(ItemData newItem)
    {
        ItemData existingItem = items.Find(item => item.Name == newItem.Name);
        if (existingItem != null)
        {
            existingItem.Value += 1;
        }
        else
        {
            newItem.Value = 1;
            items.Add(newItem);
        }

    }
    public void Remove(ItemData item)
    {
        items.Remove(item);

    }
    public void ListItem()
    {
        foreach (Transform child in itemContect)
        {
            child.gameObject.SetActive(false);
        }
        foreach (Transform child in itemContect)
        {
            if (!child.gameObject.activeSelf)
            {
                for (int i = 0; i < items.Count; i++)
                {
                    ItemInventoryUISlots[i].gameObject.SetActive(true);
                    ItemInventoryUISlots[i].itemIconNameText.text = items[i].Name;
                    ItemInventoryUISlots[i].itemNameText.text = items[i].Name;
                    ItemInventoryUISlots[i].itemDescriptionText.text = items[i].Description;
                    ItemInventoryUISlots[i].itemIconImage.sprite = items[i].Icon;
                    ItemInventoryUISlots[i].itemBigImage.sprite = items[i].BigImage;
                    ItemInventoryUISlots[i].countitemText.text = $"{items[i].Value}";
                    ItemInventoryUISlots[i].CurrentitemData = items[i];
                }
            }
        }
    }
    public void CountItem()
    {
        foreach (var item in items)
        {

        }
    }
}
