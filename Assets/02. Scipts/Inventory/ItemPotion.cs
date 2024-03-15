using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class ItemPotion : MonoBehaviour
{
    public ItemData item;
    public Image PositionIcon;
    public TextMeshProUGUI Count;

    private void Start()
    {
        item.Value = 0;
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
            FindObjectOfType<Player>().Heal(10);
        }
        else if (item.Value == 0)
        {
            InventoryManager.Instance.Remove(item);
            Count.text = "0";
        }
        else
        {
            return;
        }
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
