using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    public Animator animator;
    public ItemData item;
    public Image Store;
    public TextMeshProUGUI CoinText;
    public TextMeshProUGUI InfoText;
    public GameObject[] PotionSlot;
    public GameObject[] PotionObjects;

    private float _counter = 10f;
    public float _timer; 

    private void Start()
    {
        Store.gameObject.SetActive(false);
        InfoText.gameObject.SetActive(false);
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer < _counter)
        {
            if (!PotionSlot[PotionSlot.Length - 1].activeSelf)
            {
                for (int i = 0; i < 4; i++)
                {
                    PotionSlot[i].SetActive(true);
                    PotionObjects[i].SetActive(true);
                }
                _timer = 0;
            }
        }
        if (!Store.gameObject.activeSelf) 
        {
            UnityEngine.Cursor.visible = false;
        }
    }

    public void BuyPotion()
    {
        Player player = FindAnyObjectByType<Player>();
        if (player.Coin >= 10)
        {
            player.Coin -= 10;
            InventoryManager.Instance.Add(item);
            InventoryManager.Instance.ListItem();
            ItemPotion.Instance.Refresh();
            for (int i = 0; i < PotionSlot.Length; i++)
            {
                if (PotionSlot[i].activeSelf)
                {
                    PotionSlot[i].SetActive(false);
                    PotionObjects[i].SetActive(false);
                    break; 
                }
            }
            Store.gameObject.SetActive(false);
        }
        else
        {
            return;
        }
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Inventory);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InfoText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.F))
        {
            animator.SetTrigger("Hi");
            bool Setactive = !Store.gameObject.activeSelf;
            Store.gameObject.SetActive(Setactive);
            UnityEngine.Cursor.visible = Setactive;
            UnityEngine.Cursor.lockState = Setactive ? CursorLockMode.None : CursorLockMode.Locked;
            Player player = FindObjectOfType<Player>();
            CoinText.text = $"소지 코인 : {player.Coin}개";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Store.gameObject.SetActive(false);
            InfoText.gameObject.SetActive(false);
        }
    }
}
