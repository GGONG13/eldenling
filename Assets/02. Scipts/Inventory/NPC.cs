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
    private Transform _player;

    private void Start()
    {
        Store.gameObject.SetActive(false);
        _player = FindAnyObjectByType<Player>().transform;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            animator.SetTrigger("Hi");
            bool Setactive = !Store.gameObject.activeSelf;
            Store.gameObject.SetActive(Setactive);
            UnityEngine.Cursor.visible = Setactive;
            UnityEngine.Cursor.lockState = Setactive ? CursorLockMode.None : CursorLockMode.Locked;
            Player player = FindAnyObjectByType<Player>();
            CoinText.text = $"소지 코인 : {player.Coin}개";
        }

        if (Vector3.Distance(transform.position, _player.position) > 2)
        {
            Store.gameObject.SetActive(false);
            InfoText.gameObject.SetActive(false);
        }

        if (Vector3.Distance(transform.position, _player.position) <= 3)
        {
            InfoText.gameObject.SetActive(true);
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
            for (int i = 0; i < PotionSlot.Length; i++)
            {
                if (PotionSlot[i].activeSelf)
                {
                    PotionSlot[i].SetActive(false);
                    break; 
                }
            }
            Store.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("코인이 부족합니다.");
        }
    }
}
