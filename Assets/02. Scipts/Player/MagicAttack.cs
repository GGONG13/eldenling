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
    public Weapon weapon; // Weapon Ŭ������ ���� ����
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
            _playerMove.ReduceStamina(25); // ���¹̳� ���� ����
            AttackTimer = AttackDelayTime;
        }

        if (AttackTimer > 0f)
        {
            AttackTimer -= Time.deltaTime;
        }
    }

    public void BeginWeaponAttack()
    {
        // ���� ���� �� ó���� ������ �ִٸ� ���⿡ �߰�
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
        _animator.ResetTrigger("ComboAttack"); // ComboAttack Ʈ���� ����
    }
}
