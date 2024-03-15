using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float AttackDelayTime = 1f;
    private float AttackTimer = 0f;

    private Animator _animator;
    private PlayerMove _playerMove;
    public Weapon weapon; // Weapon 클래스에 대한 참조

    private bool isComboAttackReady = false; // 콤보 공격이 가능한 상태인지 체크하는 플래그

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
                // 첫 번째 공격 실행
                _animator.SetTrigger("Attack");
                isComboAttackReady = true; // 콤보 공격 준비
            }
            else
            {
                // 콤보 공격 실행
                _animator.SetTrigger("ComboAttack");
                isComboAttackReady = false; // 콤보 공격 사용 후 초기화
            }
            AttackTimer = AttackDelayTime;
            _playerMove.ReduceStamina(12);
        }

        if (AttackTimer > 0f)
        {
            AttackTimer -= Time.deltaTime;
        }
    }

    // 애니메이션 이벤트를 위한 메서드
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

    // 콤보 공격이 가능한 상태를 리셋하는 메서드, 공격 애니메이션의 끝부분에서 호출
    public void ResetComboAttack()
    {
        isComboAttackReady = false;
    }
}
