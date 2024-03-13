using UnityEngine;

public enum WeaponType
{
    HandSword,
    Staff,
}
public class Weapon : MonoBehaviour
{
    public WeaponType type;

    public int Damage = 20; // ���� ������
    public bool _isAttacking = false; // ���� ���� ������ ����
    private bool _hasDealtDamage = false; // �̹� ���ݿ��� �̹� �������� �־����� ����

    public Animator _animator;
    private void Awake()
    {
        // �ش� ���� ������Ʈ�� �ִ� Animator ������Ʈ ������ ������
        _animator = GetComponent<Animator>();
    }

    // ���� ����
    public void BeginAttack()
    {
        _isAttacking = true;
        _hasDealtDamage = false; // ���ο� ������ ���۵Ǹ� �������� �� ���� ���ٰ� ����
    }

    // ���� ����
    public void EndAttack()
    {
        _isAttacking = false;
        _hasDealtDamage = false; // ������ ����Ǹ� ���� ������ ���� ����
    }

    // Ʈ���ſ� �� ĳ���Ͱ� ���Դ��� �˻�
    private void OnTriggerEnter(Collider other)
    {
        if (_isAttacking && !_hasDealtDamage && other.CompareTag("Enemy"))
        {
            Boss boss = other.GetComponent<Boss>();
            Enemy enemy = other.GetComponent<Enemy>();
            if (boss != null)
            {
                // ������ �������� �ִ� ����
                DamageInfo damageInfo = new DamageInfo(DamageType.Normal, Damage);
                boss.Hit(damageInfo);
                _hasDealtDamage = true; // �������� �־����Ƿ� true�� ����
            }
            if (enemy != null)
            {
                // ������ �������� �ִ� ����
                DamageInfo damageInfo = new DamageInfo(DamageType.Normal, Damage);
                enemy.Hit(damageInfo);
                _hasDealtDamage = true; // �������� �־����Ƿ� true�� ����
            }
        }
    }

    private void SetAnimationLayerWeight()
    {
        // ���� ������ ���� �ִϸ��̼� ���̾� ����
        switch (type)
        {
            case WeaponType.HandSword:
                // HandSword ���̾ ����ġ�� ����
                _animator.SetLayerWeight(0, 1f); // HandSword ���̾��� ����ġ�� 1�� ����
                _animator.SetLayerWeight(1, 0f); // Staff ���̾��� ����ġ�� 0���� ����
                break;
            case WeaponType.Staff:
                // Staff ���̾ ����ġ�� ����
                _animator.SetLayerWeight(0, 0f); // HandSword ���̾��� ����ġ�� 0���� ����
                _animator.SetLayerWeight(1, 1f); // Staff ���̾��� ����ġ�� 1�� ����
                break;
        }
    }
}