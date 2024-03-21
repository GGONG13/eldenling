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

    public TrailRenderer TrailEffect;
    public Animator _animator;
    private void Awake()
    {
        // �ش� ���� ������Ʈ�� �ִ� Animator ������Ʈ ������ ������
        _animator = GetComponent<Animator>();
        TrailEffect.enabled = false;
    }

    // ���� ����
    public void BeginAttack()
    {
        _isAttacking = true;
        _hasDealtDamage = false; // ���ο� ������ ���۵Ǹ� �������� �� ���� ���ٰ� ����
        TrailEffect.enabled = true;
    }

    // ���� ����
    public void EndAttack()
    {
        _isAttacking = false;
        _hasDealtDamage = false; // ������ ����Ǹ� ���� ������ ���� ����
        TrailEffect.enabled = false;
    }

    // Ʈ���ſ� �� ĳ���Ͱ� ���Դ��� �˻�
    private void OnTriggerEnter(Collider other)
    {
        if (_isAttacking && !_hasDealtDamage && other.CompareTag("Enemy"))
        {
            Boss boss = other.GetComponent<Boss>();
            Enemy enemy = other.GetComponent<Enemy>();
            MonsterBox monsterBox = other.GetComponent<MonsterBox>();
            Debug.Log("�̹��� �����ϰ� �ֳ�?");
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
            if (monsterBox != null)
            {
                DamageInfo damageInfo = new DamageInfo(DamageType.Normal, Damage);
                monsterBox.Hit(damageInfo);
                _hasDealtDamage = true; // �������� �־����Ƿ� true�� ����
                Debug.Log("�̹� ������ ��");
            }
        }
    }

    
}