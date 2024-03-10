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
    public Weapon weapon; // Weapon 클래스에 대한 참조

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
            // 이제 Weapon 클래스의 BeginAttack은 애니메이션 이벤트를 통해 호출됩니다.
            AttackTimer = AttackDelayTime;
            _playerMove.ReduceStamina(12);
        }

        if (AttackTimer > 0f)
        {
            AttackTimer -= Time.deltaTime;
            if (AttackTimer <= 0f)
            {
                // AttackTimer가 0에 도달하면 공격 상태를 리셋합니다.
                // EndAttack은 애니메이션 이벤트를 통해 호출됩니다.
            }
        }
    }

    // 애니메이션 이벤트를 위한 메서드
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