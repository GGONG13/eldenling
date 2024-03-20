using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shield Object", menuName = "Items / Shield")]
public class ShieldObject : ItemData
{
    private void Awake()
    {
        Type = ItemType.Shield;
        Value = 1;
    }
}
