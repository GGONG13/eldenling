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

    private bool isComboAttackReady = false; // �޺� ������ ������ �������� üũ�ϴ� �÷���

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
            if (!isComboAttackReady)
            {
                // ù ��° ���� ����
                _animator.SetTrigger("Attack");
                isComboAttackReady = true; // �޺� ���� �غ�
            }
            else
            {
                // �޺� ���� ����
                _animator.SetTrigger("ComboAttack");
                isComboAttackReady = false; // �޺� ���� ��� �� �ʱ�ȭ
            }
            AttackTimer = AttackDelayTime;
            _playerMove.ReduceStamina(12);
        }

        if (AttackTimer > 0f)
        {
            AttackTimer -= Time.deltaTime;
        }
    }

    // �ִϸ��̼� �̺�Ʈ�� ���� �޼���
    public void BeginWeaponAttack()
    {
        weapon.BeginAttack();
        _playerMove.isAttacking = true;
        _animator.SetBool("isAttacking", _playerMove.isAttacking);
    }

    public void EndWeaponAttack()
    {
        weapon.EndAttack();
        _playerMove.isAttacking = false;
        _animator.SetBool("isAttacking", _playerMove.isAttacking);
    }

    // �޺� ������ ������ ���¸� �����ϴ� �޼���, ���� �ִϸ��̼��� ���κп��� ȣ��
    public void ResetComboAttack()
    {
        isComboAttackReady = false;
    }
}
