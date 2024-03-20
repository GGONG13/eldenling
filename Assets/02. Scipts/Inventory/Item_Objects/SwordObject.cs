using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Sword Object", menuName = "Items / Sword")]
public class SwordObject : ItemData
{
    public void Awake()
    {
        Type = ItemType.Sword;
        Value = 1;
    }
}
