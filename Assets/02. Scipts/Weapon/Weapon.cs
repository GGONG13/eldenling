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

    private bool _hasDealtDamageToBoss = false;
    private bool _hasDealtDamageToEnemy = false;
    private bool _hasDealtDamageToMonsterBox = false;

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
        _hasDealtDamageToBoss = false;
        _hasDealtDamageToEnemy = false;
        _hasDealtDamageToMonsterBox = false;
        TrailEffect.enabled = true;
    }

    // ���� ����
    public void EndAttack()
    {
        _isAttacking = false;
        _hasDealtDamageToBoss = false;
        _hasDealtDamageToEnemy = false;
        _hasDealtDamageToMonsterBox = false;
        TrailEffect.enabled = false;
    }

    // Ʈ���ſ� �� ĳ���Ͱ� ���Դ��� �˻�
    private void OnTriggerEnter(Collider other)
    {
        if (_isAttacking && other.CompareTag("Enemy"))
        {
            if (other.TryGetComponent<Boss>(out Boss boss) && !_hasDealtDamageToBoss)
            {
                DamageInfo damageInfo = new DamageInfo(DamageType.Normal, Damage);
                boss.Hit(damageInfo);
                _hasDealtDamageToBoss = true;
            }
            else if (other.TryGetComponent<Enemy>(out Enemy enemy) && !_hasDealtDamageToEnemy)
            {
                DamageInfo damageInfo = new DamageInfo(DamageType.Normal, Damage);
                enemy.Hit(damageInfo);
                _hasDealtDamageToEnemy = true;
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (_isAttacking && other.CompareTag("Mimix"))
        {
            if (other.TryGetComponent<MonsterBox>(out MonsterBox monsterBox) && !_hasDealtDamageToMonsterBox)
            {
                DamageInfo damageInfo = new DamageInfo(DamageType.Normal, Damage);
                monsterBox.Hit(damageInfo);
                _hasDealtDamageToMonsterBox = true;
            }
        }
    }
}