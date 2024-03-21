using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float AttackDelayTime = 1f;
    private float attackTimer = 0f;
    public int AttackCount;

    private Animator _animator;
    private PlayerMove _playerMove;
    public Weapon weapon; // Weapon 클래스에 대한 참조
    public InventoryManager inventoryManager;
    

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _playerMove = GetComponent<PlayerMove>();
        weapon = GetComponentInChildren<Weapon>();
        inventoryManager = GetComponentInChildren<InventoryManager>();
    }

    private void Start()
    {
        AttackCount = 0;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && attackTimer <= 0f && _playerMove.Stamina >= 12 && AttackCount == 0)
        {
            
                // 첫 번째 공격 실행
            _animator.SetTrigger("Attack");
            AttackCount += 1;
            attackTimer = AttackDelayTime;
            _playerMove.ReduceStamina(12);

            AudioManager.instance.PlaySfx(AudioManager.Sfx.Sword);
            
        }
        else if (Input.GetMouseButtonDown(0) && _playerMove.Stamina >= 12 && AttackCount == 1)
        {
            // 콤보 공격 실행
            _animator.SetTrigger("ComboAttack");
            AttackCount--;
            attackTimer = AttackDelayTime;
            _playerMove.ReduceStamina(12);
            AudioManager.instance.PlaySfx(AudioManager.Sfx.Sword);
        }

        if (attackTimer > 0f)
        {
            attackTimer -= Time.deltaTime;
        }
    }

    // 애니메이션 이벤트로 호출될 메서드
    public void BeginWeaponAttack()
    {
        weapon.BeginAttack();
        _playerMove.isAttacking = true;
        _animator.SetBool("isAttacking",true);
        _animator.ResetTrigger("ComboAttack");
    }

    // 기본 공격이 끝났을 때 호출될 메서드
    public void EndBasicAttack()
    {
        _playerMove.isAttacking = false;
        weapon.EndAttack();
        _animator.SetBool("isAttacking", false);
    }

    // 콤보 공격이 끝났을 때 호출될 메서드
    public void EndComboAttack()
    {
        _playerMove.isAttacking = false;
        weapon.EndAttack();
        _animator.SetBool("isAttacking", false);
        AttackCount = 0;
    }

    
}
