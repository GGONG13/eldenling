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
        _hasDealtDamage = false; // 새로운 공격이 시작되면 데미지를 준 적이 없다고 리셋
        TrailEffect.enabled = true;
    }

    // 공격 종료
    public void EndAttack()
    {
        _isAttacking = false;
        _hasDealtDamage = false; // 공격이 종료되면 다음 공격을 위해 리셋
        TrailEffect.enabled = false;
    }

    // 트리거에 적 캐릭터가 들어왔는지 검사
    private void OnTriggerEnter(Collider other)
    {
        if (_isAttacking && !_hasDealtDamage && other.CompareTag("Enemy"))
        {
            Boss boss = other.GetComponent<Boss>();
            Enemy enemy = other.GetComponent<Enemy>();
            MonsterBox monsterBox = other.GetComponent<MonsterBox>();
            Debug.Log("미믹을 참조하고 있냐?");
            if (boss != null)
            {
                // 적에게 데미지를 주는 로직
                DamageInfo damageInfo = new DamageInfo(DamageType.Normal, Damage);
                boss.Hit(damageInfo);
                _hasDealtDamage = true; // 데미지를 주었으므로 true로 설정
            }
            if (enemy != null)
            {
                // 적에게 데미지를 주는 로직
                DamageInfo damageInfo = new DamageInfo(DamageType.Normal, Damage);
                enemy.Hit(damageInfo);
                _hasDealtDamage = true; // 데미지를 주었으므로 true로 설정
            }
            if (monsterBox != null)
            {
                DamageInfo damageInfo = new DamageInfo(DamageType.Normal, Damage);
                monsterBox.Hit(damageInfo);
                _hasDealtDamage = true; // 데미지를 주었으므로 true로 설정
                Debug.Log("미믹 때리는 중");
            }
        }
    }

    
}