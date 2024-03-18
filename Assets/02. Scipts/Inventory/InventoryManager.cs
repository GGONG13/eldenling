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

    public GameObject StateUIPopUp;

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

    private void Start()
    {
        ListItem();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)) 
        {
            bool isActive = !Inventory.activeSelf;
            Inventory.SetActive(isActive); // �κ��丮 UI Ȱ��ȭ/��Ȱ��ȭ ���
                                           // �κ��丮�� Ȱ��ȭ�Ǹ� ���콺 Ŀ���� ǥ���ϰ�, �׷��� ������ ����ϴ�.

            UnityEngine.Cursor.visible = isActive;
            // �κ��丮�� Ȱ��ȭ�Ǹ� ���콺 Ŀ���� ����� �ʰ�, �׷��� ������ ��޴ϴ�.
            UnityEngine.Cursor.lockState = isActive ? CursorLockMode.None : CursorLockMode.Locked;
            Time.timeScale = isActive ? 0 : 1;
        }
        if (Inventory.activeInHierarchy == false)
        {
            StateUIPopUp.SetActive(true);
        }
        else
        {
            StateUIPopUp.SetActive(false);
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

            if (items[item.ID].Value > 0)
            {
                items[item.ID].Value -= 1; // �������� ������ ����
                                           // UI ������ ������ ���� �ؽ�Ʈ ������Ʈ
                ItemInventoryUISlots[item.ID].countitemText.text = $"{items[item.ID].Value}";
            }
            // �������� Value�� 0�� �Ǹ�, UI ������ ��Ȱ��ȭ�ϰ� �÷��ǿ��� ������ ����
            if (items[item.ID].Value == 0)
            {
                ItemInventoryUISlots[item.ID].gameObject.SetActive(false);
                items.Remove(item);
            }
            else if (items[item.ID].Value < 0)
            {
                return;
            }
            ItemPotion.Instance.Refresh();
        
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
}
