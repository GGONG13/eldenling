using UnityEngine;

public enum WeaponType
{
    HandSword,
    Staff,
}
public class Weapon : MonoBehaviour
{
    public WeaponType type;

    public int Damage = 20; // 공격 데미지
    public bool _isAttacking = false; // 현재 공격 중인지 여부
    private bool _hasDealtDamage = false; // 이번 공격에서 이미 데미지를 주었는지 여부

    public TrailRenderer TrailEffect;
    public Animator _animator;

    private bool _hasDealtDamageToBoss = false;
    private bool _hasDealtDamageToEnemy = false;
    private bool _hasDealtDamageToMonsterBox = false;

    private void Awake()
    {
        // 해당 게임 오브젝트에 있는 Animator 컴포넌트 참조를 가져옴
        _animator = GetComponent<Animator>();
        TrailEffect.enabled = false;
    }

    // 공격 시작
    public void BeginAttack()
    {
        _isAttacking = true;
        _hasDealtDamageToBoss = false;
        _hasDealtDamageToEnemy = false;
        _hasDealtDamageToMonsterBox = false;
        TrailEffect.enabled = true;
    }

    // 공격 종료
    public void EndAttack()
    {
        _isAttacking = false;
        _hasDealtDamageToBoss = false;
        _hasDealtDamageToEnemy = false;
        _hasDealtDamageToMonsterBox = false;
        TrailEffect.enabled = false;
    }

    // 트리거에 적 캐릭터가 들어왔는지 검사
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