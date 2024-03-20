using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class ItemPotion : MonoBehaviour
{
    public static ItemPotion Instance;
    public ItemData item;
    public Image PositionIcon;
    public TextMeshProUGUI Count;
    public GameObject HealEffect;
    public Transform HealingPosition;

    private void Start()
    {

        Instance = this;
        item.Value = 1;
        Refresh();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl)) 
        {
            EatPotion();
        }
        Refresh();
    }
    public void EatPotion()
    {
        if (item.Value > 0)
        {
            item.Value -= 1;
            FindObjectOfType<Player>().Heal(60);
            Instantiate(HealEffect, HealingPosition.position, HealingPosition.rotation);
        }
        else if (item.Value == 0)
        {
            InventoryManager.Instance.Remove(item);
        }
        else
        {
            return;
        }
        Refresh();
    }
    public void Refresh()
    {
        if (item.Value > 0)
        {
            PositionIcon.sprite = item.Icon;
            Count.text = $"{item.Value}";
        }
        else if (item.Value == 0)
        {
            Count.text = "0";
        }
        else
        {
            return; 
        }
    }
}
