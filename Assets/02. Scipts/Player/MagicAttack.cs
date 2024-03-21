using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicAttack : MonoBehaviour
{
    public float AttackDelayTime = 1f;
    private float AttackTimer = 0f;

    public GameObject MagicArrowPrefab;
    public GameObject MagicCircle;
    public Transform MagicPosition;

    private Animator _animator;
    public Weapon weapon; // Weapon 클래스에 대한 참조
    private PlayerMove _playerMove;
    private Player_Shield _playerShield;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _playerMove = GetComponent<PlayerMove>();
        weapon = GetComponentInChildren<Weapon>();
        _playerShield = GetComponent<Player_Shield>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && AttackTimer <= 0f && !_playerShield._isDefending && _playerMove.Stamina >= 25)
        {
            _animator.SetTrigger("MagicAttack");
            _playerMove.ReduceStamina(25); // 스태미너 감소 적용
            AttackTimer = AttackDelayTime;
        }

        if (AttackTimer > 0f)
        {
            AttackTimer -= Time.deltaTime;
        }
    }

    public void BeginWeaponAttack()
    {
        // 공격 시작 시 처리할 내용이 있다면 여기에 추가
    }

    public void WeaponAttack()
    {
        Instantiate(MagicArrowPrefab, MagicPosition.position, MagicPosition.rotation);
        Instantiate(MagicCircle, MagicPosition.position, MagicPosition.rotation);
    }

    public void EndWeaponAttack()
    {
        _playerMove.isAttacking = false;
        weapon.EndAttack();
        _animator.ResetTrigger("ComboAttack"); // ComboAttack 트리거 리셋
    }
}
