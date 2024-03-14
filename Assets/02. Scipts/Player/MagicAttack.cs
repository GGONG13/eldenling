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
    // PlayerMove _playerMove 참조 및 사용 제거
    public Weapon weapon; // Weapon 클래스에 대한 참조
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

    // 애니메이션 이벤트를 위한 메서드에서 isAttacking 상태 변경 코드 제거
    public void BeginWeaponAttack()
    {
        _playerMove.isAttacking = false;
        weapon.BeginAttack();
        
        // 여기서 스태미너 감소 코드만 유지
    }

    public void WeaponAttack()
    {
       
        
            Instantiate(MagicArrowPrefab, MagicPosition.position, MagicPosition.rotation);
        
        
    }
    public void EndWeaponAttack()
    {
        _playerMove.isAttacking = false;
        weapon.EndAttack();
        // ComboAttack 트리거 리셋

        _animator.ResetTrigger("ComboAttack");
    }
}
