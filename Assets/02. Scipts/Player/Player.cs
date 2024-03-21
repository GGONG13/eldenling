using System.Collections;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class Player : MonoBehaviour
{

    [Header("���� ��ü ��ũ��Ʈ")]
    public MonoBehaviour SwordScript; // ���� ���� ��ũ��Ʈ ����
    public MonoBehaviour WandScript; // ���Ÿ� ���� ��ũ��Ʈ ����

    [Header("Inventory")]
    public ItemData ItemData;
    public GameObject[] _swords; // �̸� �Ҵ�� 5���� ���� ������
    public GameObject[] _shields; // �̸� �Ҵ�� 5���� ���� ������
    public GameObject _magicwand; // �̸� �Ҵ�� 1���� ������ ������

    private Animator _animator;
    private PlayerMove _playerMove; // PlayerMove Ŭ������ ���� ����
    private Player_Shield _playerShield;


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
    public TextMeshProUGUI StateName;

    public Image YouDiedImage;

    public GameObject SwitchingVFX;

    public GameObject PerringVFX;
    public Transform PerringPosition;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _playerShield = GetComponentInChildren<Player_Shield>();
        _playerMove = GetComponent<PlayerMove>(); // PlayerMove Ŭ������ ���� ���� �ʱ�ȭ
        Health = MaxHealth;
        YouDiedImage.gameObject.SetActive(false);
        SwitchingVFX.gameObject.SetActive(false);
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
        SwordScript.enabled = true;
        WandScript.enabled = false; 
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
        if (Health <= 0)
        {
            Health = 0;
            HealthSliderUI.value = 0;
            Death();
        }
        if(_playerShield._isParrying == true)
        {
            damage.Amount = 0;
            _animator.SetTrigger("Parrying");
            Debug.Log("�и� ����");
            Instantiate(PerringVFX, PerringPosition.position, PerringPosition.rotation);
            AudioManager.instance.PlaySfx(AudioManager.Sfx.Shield);
        }

        if(_playerShield._isDefending == true)
        {
            damage.Amount /= 2;
            _playerMove.ReduceStamina(15);
        }
        Health -= damage.Amount;
        Debug.Log($"Player: {Health}");
    }

    private IEnumerator Death_Coroutine()
    {
        _animator.SetTrigger("Die"); // ��� �ִϸ��̼� Ʈ����
        _playerMove.OnPlayerDeath(); // PlayerMove Ŭ�������� �̵� �� �׼� ó�� ����
        yield return new WaitForSeconds(5);
        YouDiedImage.gameObject.SetActive(true);
        gameObject.SetActive(false);
        _playerMove.isAlive = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0;
    }

    public void Death()
    {
        //_playerMove.isAlive = false;        
        StartCoroutine(Death_Coroutine());
    }

    // ü���� ȸ���ϴ� �޼���
    public void Heal(int healAmount)
    {
        Health += healAmount;
        Health = Mathf.Min(Health, MaxHealth); // ü���� �ִ� ü���� �ʰ����� �ʵ��� ��
    }
    public void ActivateItem(ItemData itemData)
    {
        SwitchVFX();
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
                StateName.text = $"SWORD";
                SwordScript.enabled = true;
                if (_magicwand.activeInHierarchy == true)
                {
                    _magicwand.SetActive(false);
                    WandScript.enabled = false;
                }
                
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
            case ItemType.MagicWand: 
            {
                // �������� Ȱ��ȭ�մϴ�.
                _magicwand.gameObject.SetActive(true);
                WandScript.enabled= true;
                // �ҵ尡 Ȱ��ȭ�� �������� Ȯ���մϴ�.
                foreach (var sword in _swords)
                {
                    sword.SetActive(false);
                    SwordScript.enabled= false;
                }

                SwordIcon.sprite = itemData.Icon;
                StateName.text = $"WAND";
                break;
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            Coin += 10;
            Debug.Log($"����: {Coin}��");
            other.gameObject.SetActive(false);
        }
    }

    public void RefreshCoin()
    {
        CoinUI.text = $"���� : {Coin}��";
    }
    public void SwitchVFX()
    {
        SwitchingVFX.transform.position = FindAnyObjectByType<Player>().transform.position;
        SwitchingVFX.gameObject.SetActive(true);
        StartCoroutine(ShowVFX_Coroutine());
    }
    IEnumerator ShowVFX_Coroutine()
    {
        yield return new WaitForSeconds(1);
        SwitchingVFX.gameObject.SetActive(false);
    }
}
