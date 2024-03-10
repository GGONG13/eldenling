using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int PlayerDamage = 20;
    public float AttackDelayTime = 1f;
    private float AttackTimer = 0f;

    private Animator _animator;
    private PlayerMove _playerMove;
    public Weapon weapon; // Weapon Ŭ������ ���� ����

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _playerMove = GetComponent<PlayerMove>();
        weapon = GetComponentInChildren<Weapon>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && AttackTimer <= 0f && _playerMove.Stamina >= 12)
        {
            _animator.SetTrigger("Attack");
            // ���� Weapon Ŭ������ BeginAttack�� �ִϸ��̼� �̺�Ʈ�� ���� ȣ��˴ϴ�.
            AttackTimer = AttackDelayTime;
            _playerMove.ReduceStamina(12);
        }

        if (AttackTimer > 0f)
        {
            AttackTimer -= Time.deltaTime;
            if (AttackTimer <= 0f)
            {
                // AttackTimer�� 0�� �����ϸ� ���� ���¸� �����մϴ�.
                // EndAttack�� �ִϸ��̼� �̺�Ʈ�� ���� ȣ��˴ϴ�.
            }
        }
    }

    // �ִϸ��̼� �̺�Ʈ�� ���� �޼���
    public void BeginWeaponAttack()
    {
        weapon.BeginAttack();
        _playerMove.isAttacking = true;
    }

    public void EndWeaponAttack()
    {
        weapon.EndAttack();
        _playerMove.isAttacking = false;
    }
}