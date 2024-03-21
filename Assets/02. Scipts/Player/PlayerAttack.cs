using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float AttackDelayTime = 1f;
    private float attackTimer = 0f;
    public int AttackCount;

    private Animator _animator;
    private PlayerMove _playerMove;
    public Weapon weapon; // Weapon Ŭ������ ���� ����
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
            
                // ù ��° ���� ����
            _animator.SetTrigger("Attack");
            AttackCount += 1;
            attackTimer = AttackDelayTime;
            _playerMove.ReduceStamina(12);

            AudioManager.instance.PlaySfx(AudioManager.Sfx.Sword);
            
        }
        else if (Input.GetMouseButtonDown(0) && _playerMove.Stamina >= 12 && AttackCount == 1)
        {
            // �޺� ���� ����
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

    // �ִϸ��̼� �̺�Ʈ�� ȣ��� �޼���
    public void BeginWeaponAttack()
    {
        weapon.BeginAttack();
        _playerMove.isAttacking = true;
        _animator.SetBool("isAttacking",true);
        _animator.ResetTrigger("ComboAttack");
    }

    // �⺻ ������ ������ �� ȣ��� �޼���
    public void EndBasicAttack()
    {
        _playerMove.isAttacking = false;
        weapon.EndAttack();
        _animator.SetBool("isAttacking", false);
    }

    // �޺� ������ ������ �� ȣ��� �޼���
    public void EndComboAttack()
    {
        _playerMove.isAttacking = false;
        weapon.EndAttack();
        _animator.SetBool("isAttacking", false);
        AttackCount = 0;
    }

    
}
