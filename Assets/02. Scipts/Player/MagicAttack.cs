using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicAttack : MonoBehaviour
{
    public float AttackDelayTime = 1f;
    private float AttackTimer = 0f;

    public GameObject MaigcArrowPrefab;
    public Transform MagicPosition;

    private Animator _animator;
    private PlayerMove _playerMove;
    public Weapon weapon; // Weapon 클래스에 대한 참조

    public bool ComboAttack;
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


            _animator.SetTrigger("MagicAttack");
            // 이제 Weapon 클래스의 BeginAttack은 애니메이션 이벤트를 통해 호출됩니다.
            GameObject magicArrow = Instantiate(MaigcArrowPrefab);
            magicArrow.transform.position = MagicPosition.transform.position;

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
        _animator.SetBool("isAttacking", _playerMove.isAttacking);
        _playerMove.ReduceStamina(12);
    }

    public void EndWeaponAttack()
    {
        weapon.EndAttack();
        _playerMove.isAttacking = false;
        _animator.SetBool("isAttacking", _playerMove.isAttacking);

        // ComboAttack 트리거 리셋
        _animator.ResetTrigger("ComboAttack");
    }
}

