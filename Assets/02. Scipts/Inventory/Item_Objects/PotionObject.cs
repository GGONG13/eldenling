using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Potion Object", menuName = "Items / Potion")]
public class PotionObject : ItemData
{
    private void Awake()
    {
        Type = ItemType.Potion;
    }
}
