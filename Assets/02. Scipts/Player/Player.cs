using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("Inventory")]
    public ItemData ItemData;
    public GameObject[] _swords; // �̸� �Ҵ�� 5���� ���� ������
    public GameObject[] _shields; // �̸� �Ҵ�� 5���� ���� ������

    public GameObject SwordPosition;
    public GameObject ShieldPosiotion;

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

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _playerShield = GetComponentInChildren<Player_Shield>();
        _playerMove = GetComponent<PlayerMove>(); // PlayerMove Ŭ������ ���� ���� �ʱ�ȭ
        Health = MaxHealth;
/*        _swords = GameObject.Find("Sword").GetComponentsInChildren<GameObject>(true);
        _shields = GameObject.Find("Shield").GetComponentsInChildren<GameObject>(true);
        Transform[] swordTransforms = GameObject.Find("Sword").GetComponentsInChildren<Transform>(true);
        _swords = swordTransforms.Select(t => t.gameObject).ToArray();
        Transform[] shieldTransforms = GameObject.Find("Shield").GetComponentsInChildren<Transform>(true);
        _swords = shieldTransforms.Select(t => t.gameObject).ToArray();*/
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
            _playerMove.isAlive = false;
            _animator.SetTrigger("Die"); // ��� �ִϸ��̼� Ʈ����
            _playerMove.OnPlayerDeath(); // PlayerMove Ŭ�������� �̵� �� �׼� ó�� ����
            StartCoroutine(DeathWithDelay(5f)); // ��� ó�� ����
        }
        if(_playerShield._isParrying == true)
        {
            damage.Amount = 0;
            _animator.SetTrigger("Parrying");
            Debug.Log("�и� ����");
            
        }

        if(_playerShield._isDefending == true)
        {
            damage.Amount /= 2;
            _playerMove.ReduceStamina(15);
        }
        Health -= damage.Amount;
        Debug.Log($"Player: {Health}");
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
                if (_swords[itemData.ID].activeInHierarchy == false)
                {
                    _swords[itemData.ID].SetActive(true);
                    //swords[itemData.ID].transform.position = SwordPosition.transform.position;
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
                _shields[itemData.ID].transform.position = ShieldPosiotion.transform.position;
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
