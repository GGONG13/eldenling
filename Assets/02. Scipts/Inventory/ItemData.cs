using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ItemType
{
    Sword,
    Shield,
    Potion

}
public class ItemData : ScriptableObject
{
    public GameObject Prefab;
    public Vector3 Position;
    public int ID;
    public ItemType Type;
    public string Name;
    public string Description;
    public int Value;
    public Sprite Icon;
    public Sprite BigImage;
}
