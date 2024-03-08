using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class ItemObjectFactory : MonoBehaviour
{
    [Header("아이템 프리펩")]
    public List<GameObject> ItemPrefabs = new List<GameObject>();
    public Item[] possibleItems; // 생성될 수 있는 아이템들의 배열

    public static ItemObjectFactory instance;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public void DropRandomItem(Vector3 position)
    {
        int index = Random.Range(0, possibleItems.Length);
        Item selectedItem = possibleItems[index];

/*        GameObject itemObj = Instantiate(itemPrefabs, position, Quaternion.identity);
        itemObj.GetComponent<ItemObject>().Initialize(selectedItem);*/
    }

    public void ItemOnOff()
    {
       
    }
}
