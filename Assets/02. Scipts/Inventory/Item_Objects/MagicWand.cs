using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
[CreateAssetMenu(fileName = "MagicWand Object", menuName = "Items / MagicWand")]
public class MagicWand : ItemData
{
    private void Awake()
    {
        Type = ItemType.MagicWand;
    }
}
