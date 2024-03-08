using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int PlayerDamage = 20;
    public float AttackDelayTime = 1f;
    private float AttackTimer = 0f;

    public float attackRange = 2.5f; // 플레이어의 공격 범위

    private Animator _animator;
    private PlayerMove _playerMove;
    public Weapon weapon; // Weapon 클래스에 대한 참조

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _playerMove = GetComponent<PlayerMove>();
        // 무기 오브젝트를 찾아서 weapon 변수에 할당합니다.
        weapon = GetComponentInChildren<Weapon>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && AttackTimer <= 0f && _playerMove.Stamina >= 12)
        {
            _animator.SetTrigger("Attack");
            weapon.BeginAttack(); // 공격 시작
            AttackTimer = AttackDelayTime;
            _playerMove.ReduceStamina(12);
        }

        if (AttackTimer > 0f)
        {
            AttackTimer -= Time.deltaTime;
        }
    }

    // 이제 무기 콜라이더가 적과 충돌했을 때 데미지를 주므로, Attack 메서드는 비워둡니다.
}
