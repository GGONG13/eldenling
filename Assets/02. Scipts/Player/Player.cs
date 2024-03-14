using System.Collections;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class Player : MonoBehaviour
{
    [Header("Inventory")]
    public ItemData ItemData;
    public GameObject[] _swords; // �̸� �Ҵ�� 5���� ���� ������
    public GameObject[] _shields; // �̸� �Ҵ�� 5���� ���� ������

    [Header("ü�� �����̴� UI")]
    public Slider HealthSliderUI;

    public int Health;
    public int MaxHealth = 100;

    [Header("���� UI")]
    public TextMeshProUGUI CoinUI;
    public int Coin = 0;

    [Header("����� ���� ���� UI POP-UP")]
    public Image SwordIcon;
    public Image ShieldIcon;


    private Animator _animator;
    private PlayerMove _playerMove; // PlayerMove Ŭ������ ���� ����

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _playerMove = GetComponent<PlayerMove>(); // PlayerMove Ŭ������ ���� ���� �ʱ�ȭ
        Health = MaxHealth;
    }
    private void Start()
    {
        // �� �������� ������ ����
        if (_swords.Length > 0 && _swords[0] != null)
        {
            ItemSwitching swordItem = _swords[0].GetComponent<ItemSwitching>();
            SwordIcon.sprite = swordItem.Item.Icon;
        }

        // ���� �������� ������ ����
        if (_shields.Length > 0 && _shields[0] != null)
        {
            ItemSwitching shieldItem = _shields[0].GetComponent<ItemSwitching>();
            ShieldIcon.sprite = shieldItem.Item.Icon;
        }
    }
    private void Update()
    {
        HealthSliderUI.value = Health / (float)MaxHealth;
        RefreshCoin();
    }

    public void Hit(DamageInfo damage)
    {
        if (_playerMove.isInvincible || !_playerMove.isAlive)
        {
            Debug.Log("���ߴ�"); // ���� �����̰ų� �̹� ������� �� ������ ���ߴٴ� �޽��� ���
            return; // ���� �����̰ų� �̹� ����� ��� �Լ� ����
        }

        Health -= damage.Amount;
        Debug.Log($"Player: {Health}");
        if (Health <= 0)
        {
            Health = 0;
            HealthSliderUI.value = 0;
            _animator.SetTrigger("Die"); // ��� �ִϸ��̼� Ʈ����
            _playerMove.OnPlayerDeath(); // PlayerMove Ŭ�������� �̵� �� �׼� ó�� ����
            StartCoroutine(DeathWithDelay(5f)); // ��� ó�� ����
        }
    }

    private IEnumerator DeathWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Death();
    }

    public void Death()
    {
        gameObject.SetActive(false);
        _playerMove.isAlive = false; // �߰�: PlayerMove Ŭ������ isAlive ���¸� false�� ����
    }

    // ü���� ȸ���ϴ� �޼���
    public void Heal(int healAmount)
    {
        Health += healAmount;
        Health = Mathf.Min(Health, MaxHealth); // ü���� �ִ� ü���� �ʰ����� �ʵ��� ��
    }
    public void ActivateItem(ItemData itemData)
    {
        switch (itemData.Type)
        {
            case ItemType.Sword:
            {
                foreach (var sword in _swords)
                {
                    sword.SetActive(false);
                }
                _swords[itemData.ID].SetActive(true);
                SwordIcon.sprite = itemData.Icon;
                break;
            }

            case ItemType.Shield:
            {
                foreach (var shield in _shields)
                {
                    shield.SetActive(false);
                }
                _shields[itemData.ID].SetActive(true);
                ShieldIcon.sprite = itemData.Icon;
                break;
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            Coin++;
            Debug.Log($"����: {Coin}��");
            other.gameObject.SetActive(false);
        }
    }

    public void RefreshCoin()
    {
        CoinUI.text = $"���� : {Coin}��";
    }

}
