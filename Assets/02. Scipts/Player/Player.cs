using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("Inventory")]
    public ItemData ItemData;
    public GameObject[] swords; // �̸� �Ҵ�� 5���� ���� ������
    public GameObject[] shields; // �̸� �Ҵ�� 5���� ���� ������

    public GameObject SwordPosition;
    public GameObject ShieldPosiotion;

    private Animator _animator;
    private PlayerMove _playerMove; // PlayerMove Ŭ������ ���� ����
    private Player_Shield _playerShield;

    [Header("ü�� �����̴� UI")]
    public Slider HealthSliderUI;

    public int Health;
    public int MaxHealth = 100;

    public int Coin = 0;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _playerShield = GetComponentInChildren<Player_Shield>();
        _playerMove = GetComponent<PlayerMove>(); // PlayerMove Ŭ������ ���� ���� �ʱ�ȭ
        Health = MaxHealth;
    }

    private void Update()
    {
        HealthSliderUI.value = Health / (float)MaxHealth;
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
            _animator.SetTrigger("Die"); // ��� �ִϸ��̼� Ʈ����
            _playerMove.OnPlayerDeath(); // PlayerMove Ŭ�������� �̵� �� �׼� ó�� ����
            StartCoroutine(DeathWithDelay(5f)); // ��� ó�� ����
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


    public void ActivateItem(ItemData itemData)
    {
        switch (itemData.Type)
        {
            case ItemType.Sword:
            {
                foreach (var sword in swords)
                {
                    sword.SetActive(false);
                }
                swords[itemData.ID].SetActive(true);
                swords[itemData.ID].transform.position = SwordPosition.transform.position;
                break;
            }

            case ItemType.Shield:
            {
                foreach (var shield in shields)
                {
                    shield.SetActive(false);
                }
                shields[itemData.ID].SetActive(true);
                shields[itemData.ID].transform.position = ShieldPosiotion.transform.position;
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
}
