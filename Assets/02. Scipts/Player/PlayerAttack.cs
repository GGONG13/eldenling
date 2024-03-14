using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    
    public float AttackDelayTime = 1f;
    private float AttackTimer = 0f;

    private Animator _animator;
    private PlayerMove _playerMove;
    public Weapon weapon; // Weapon Ŭ������ ���� ����

    public bool ComboAttack;

    public bool _isDefending;
    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _playerMove = GetComponent<PlayerMove>();
        weapon = GetComponentInChildren<Weapon>();
    }

    void Update()
    {

        ComboAttack = false;
        if (Input.GetMouseButtonDown(0) && AttackTimer <= 0f && _playerMove.Stamina >= 12)
        {
            _animator.SetTrigger("Attack");
            // ���� Weapon Ŭ������ BeginAttack�� �ִϸ��̼� �̺�Ʈ�� ���� ȣ��˴ϴ�.
            AttackTimer = AttackDelayTime;
        }
        if (Input.GetMouseButtonDown(0) && _playerMove.isAttacking == true && _playerMove.Stamina >= 12)
        {
            _animator.SetTrigger("ComboAttack");
            AttackTimer = AttackDelayTime;
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
        _animator.SetBool("isAttacking", _playerMove.isAttacking);
        _playerMove.ReduceStamina(12);
    }

    public void EndWeaponAttack()
    {
        weapon.EndAttack();
        _playerMove.isAttacking = false;
        _animator.SetBool("isAttacking", _playerMove.isAttacking);

        // ComboAttack Ʈ���� ����
        _animator.ResetTrigger("ComboAttack");
    }
}