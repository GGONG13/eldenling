using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private Animator _animator;
    private PlayerMove _playerMove; // PlayerMove Ŭ������ ���� ����

    [Header("ü�� �����̴� UI")]
    public Slider HealthSliderUI;

    public int Health;
    public int MaxHealth = 100;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
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

        Health -= damage.Amount;
        Debug.Log($"Player: {Health}");
        if (Health <= 0)
        {
            Health = 0;
            HealthSliderUI.value = 0;
            _animator.SetTrigger("Death"); // ��� �ִϸ��̼� Ʈ����
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
}
