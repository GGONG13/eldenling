using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Android.Types;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class NPC : MonoBehaviour
{
    public Animator animator;
    public ItemData item;
    public Image Store;
    public TextMeshProUGUI CoinText;
    public TextMeshProUGUI InfoText;
    public GameObject[] PotionSlot;
    public GameObject[] PotionObjects;

    private void Start()
    {
        Store.gameObject.SetActive(false);
        InfoText.gameObject.SetActive(false);
    }

    private void Update()
    {

    }
    public void BuyPotion()
    {
        Player player = FindAnyObjectByType<Player>();
        if (player.Coin >= 10)
        {
            player.Coin -= 10;
            InventoryManager.Instance.Add(item);
            InventoryManager.Instance.ListItem();
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
            Debug.Log("F 눌렀음");
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
