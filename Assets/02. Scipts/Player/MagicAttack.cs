using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicAttack : MonoBehaviour
{
    public float AttackDelayTime = 1f;
    private float AttackTimer = 0f;

    public GameObject MagicArrowPrefab;
    public Transform MagicPosition;

    private Animator _animator;
    // PlayerMove _playerMove ���� �� ��� ����
    public Weapon weapon; // Weapon Ŭ������ ���� ����
    private PlayerMove _playerMove;

    public bool _isShild;
    private Player_Shield _playerShield;
    
    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _playerMove = GetComponent<PlayerMove>();
        weapon = GetComponentInChildren<Weapon>();
        _playerShield=GetComponent<Player_Shield>();
    }
    private void Start()
    {
              
    }
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0) && AttackTimer <= 0f && _playerShield._isDefending == false)
        {
            _animator.SetTrigger("MagicAttack");
            
            AttackTimer = AttackDelayTime;
        }

        if (AttackTimer > 0f)
        {
            AttackTimer -= Time.deltaTime;
        }
    }

    // �ִϸ��̼� �̺�Ʈ�� ���� �޼��忡�� isAttacking ���� ���� �ڵ� ����
    public void BeginWeaponAttack()
    {
        _playerMove.isAttacking = false;
        weapon.BeginAttack();
        
        // ���⼭ ���¹̳� ���� �ڵ常 ����
    }

    public void WeaponAttack()
    {
       
        
            Instantiate(MagicArrowPrefab, MagicPosition.position, MagicPosition.rotation);
        
        
    }
    public void EndWeaponAttack()
    {
        _playerMove.isAttacking = false;
        weapon.EndAttack();
        // ComboAttack Ʈ���� ����

        _animator.ResetTrigger("ComboAttack");
    }
}
