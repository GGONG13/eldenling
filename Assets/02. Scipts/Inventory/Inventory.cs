using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance { get; private set; }

    public UI_InventoryDescription InventoryDescriptionUI;

    void Start()
    {
        instance = this;
        this.gameObject.SetActive(false);
        InventoryManager.onItemChangedCallback += UpdateUI;
    }

    private void OnDestroy()
    {
        InventoryManager.onItemChangedCallback -= UpdateUI;
    }

    public void UpdateUI()
    {
/*        // 인벤토리 UI 슬롯을 모두 비활성화합니다.
        foreach (var slot in ItemInventoryUISlots)
        {
            slot.gameObject.SetActive(false);
        }*/

        // 인벤토리에 있는 각 아이템에 대해 UI 슬롯을 활성화하고, 아이템 정보를 표시합니다.
        for (int i = 0; i < InventoryManager.Instance.items.Count; i++)
        {
            InventoryManager.Instance.ItemInventoryUISlots[i].gameObject.SetActive(true);
            InventoryManager.Instance.ItemInventoryUISlots[i].itemIconNameText.text = InventoryManager.Instance.items[i].Name;
            InventoryManager.Instance.ItemInventoryUISlots[i].itemNameText.text = InventoryManager.Instance.items[i].Name;
            InventoryManager.Instance.ItemInventoryUISlots[i].itemDescriptionText.text = InventoryManager.Instance.items[i].Description;
            InventoryManager.Instance.ItemInventoryUISlots[i].itemIconImage.sprite = InventoryManager.Instance.items[i].Icon;
            InventoryManager.Instance.ItemInventoryUISlots[i].itemBigImage.sprite = InventoryManager.Instance.items[i].BigImage;
            InventoryManager.Instance.ItemInventoryUISlots[i].countitemText.text = $"{InventoryManager.Instance.items[i].Value}";
            // ItemInventoryUISlots[i].CurrentitemData = items[i]; // 필요에 따라 추가
        }
    }


}
