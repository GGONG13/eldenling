using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int damage = 20; // 공격 데미지
    public bool isAttacking = false; // 현재 공격 중인지 여부
    private bool hasDealtDamage = false; // 이번 공격에서 이미 데미지를 주었는지 여부

    // 공격 시작
    public void BeginAttack()
    {
        isAttacking = true;
        hasDealtDamage = false; // 새로운 공격이 시작되면 데미지를 준 적이 없다고 리셋
    }

    // 공격 종료
    public void EndAttack()
    {
        isAttacking = false;
        hasDealtDamage = false; // 공격이 종료되면 다음 공격을 위해 리셋
    }

    // 트리거에 적 캐릭터가 들어왔는지 검사
    private void OnTriggerEnter(Collider other)
    {
        if (isAttacking && !hasDealtDamage && other.CompareTag("Enemy"))
        {
            Boss boss = other.GetComponent<Boss>();
            if (boss != null)
            {
                // 적에게 데미지를 주는 로직
                DamageInfo damageInfo = new DamageInfo(DamageType.Normal, damage);
                boss.Hit(damageInfo);
                hasDealtDamage = true; // 데미지를 주었으므로 true로 설정
            }
        }
    }
}
