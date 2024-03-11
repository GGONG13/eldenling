using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance { get; private set; }

    public UI_InventoryDescription InventoryDescriptionUI;

    void Start()
    {
        instance = this;
        this.gameObject.SetActive(false);
    }
}
