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
    public bool isAttacking = false; // ���� ���� ������ ����
    private bool hasDealtDamage = false; // �̹� ���ݿ��� �̹� �������� �־����� ����

    public Animator _animator;
    private void Awake()
    {
        // �ش� ���� ������Ʈ�� �ִ� Animator ������Ʈ ������ ������
        _animator = GetComponent<Animator>();
    }

    // ���� ����
    public void BeginAttack()
    {
        isAttacking = true;
        hasDealtDamage = false; // ���ο� ������ ���۵Ǹ� �������� �� ���� ���ٰ� ����
    }

    // ���� ����
    public void EndAttack()
    {
        isAttacking = false;
        hasDealtDamage = false; // ������ ����Ǹ� ���� ������ ���� ����
    }

    // Ʈ���ſ� �� ĳ���Ͱ� ���Դ��� �˻�
    private void OnTriggerEnter(Collider other)
    {
        if (isAttacking && !hasDealtDamage && other.CompareTag("Enemy"))
        {
            Boss boss = other.GetComponent<Boss>();
            if (boss != null)
            {
                // ������ �������� �ִ� ����
                DamageInfo damageInfo = new DamageInfo(DamageType.Normal, damage);
                boss.Hit(damageInfo);
                hasDealtDamage = true; // �������� �־����Ƿ� true�� ����
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