using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class ItemObjectFactory : MonoBehaviour
{
    [Header("������ ������")]
    public List<GameObject> ItemPrefabs = new List<GameObject>();
    public Item[] possibleItems; // ������ �� �ִ� �����۵��� �迭

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
