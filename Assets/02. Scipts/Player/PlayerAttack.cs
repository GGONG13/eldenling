using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int PlayerDamage = 20;
    public float AttackDelayTime = 1f;
    private float AttackTimer = 0f;

    public float attackRange = 2.5f; // �÷��̾��� ���� ����

    private Animator _animator;
    private PlayerMove _playerMove;
    public Weapon weapon; // Weapon Ŭ������ ���� ����

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _playerMove = GetComponent<PlayerMove>();
        // ���� ������Ʈ�� ã�Ƽ� weapon ������ �Ҵ��մϴ�.
        weapon = GetComponentInChildren<Weapon>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && AttackTimer <= 0f && _playerMove.Stamina >= 12)
        {
            _animator.SetTrigger("Attack");
            weapon.BeginAttack(); // ���� ����
            AttackTimer = AttackDelayTime;
            _playerMove.ReduceStamina(12);
        }

        if (AttackTimer > 0f)
        {
            AttackTimer -= Time.deltaTime;
        }
    }

    // ���� ���� �ݶ��̴��� ���� �浹���� �� �������� �ֹǷ�, Attack �޼���� ����Ӵϴ�.
}
