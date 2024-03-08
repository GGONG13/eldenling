using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum ItemType
{
    Sword,
    Shield,
}

[Serializable]
public class ItemData 
{
    public ItemType ItemType;
    public string Name;
    public string Description;
    public Vector3 Position;
    public Image image; 

   /* public ItemData(string name, string description, Sprite image, GameObject itemPrefab, Vector3 position)
    {
        this.name = name;
        this.description = description;
        this.image = image;
        this.itemPrefab = itemPrefab;
        this.position = position;
    }*/
}
