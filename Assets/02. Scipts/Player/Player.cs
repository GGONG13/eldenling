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
        if (_playerMove.isInvincible)
        {
            Debug.Log("���ߴ�"); // ���� ������ �� ������ ���ߴٴ� �޽��� ���
            return; // ���� ������ ��� ���⼭ �Լ� ����
        }

        // ���� ���°� �ƴ� ���� ���� ó�� ����
        Health -= damage.Amount;
        Debug.Log($"Player: {Health}");
        if (Health <= 0)
        {
            HealthSliderUI.value = 0f;
            Health = 0;
            _animator.SetTrigger("Death");
            StartCoroutine(DeathWithDelay(5f));
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
    }
}
