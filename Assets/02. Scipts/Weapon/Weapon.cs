using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int damage = 20; // 공격 데미지
    public bool isAttacking = false; // 현재 공격 중인지 여부

    // 공격 시작
    public void BeginAttack()
    {
        isAttacking = true;
    }

    // 공격 종료
    public void EndAttack()
    {
        isAttacking = false;
    }

    // 트리거에 적 캐릭터가 들어왔는지 검사
    private void OnTriggerEnter(Collider other)
    {
        if (isAttacking && other.CompareTag("Enemy"))
        {
            Boss boss = other.GetComponent<Boss>();
            if (boss != null)
            {
                // 적에게 데미지를 주는 로직
                DamageInfo damageInfo = new DamageInfo(DamageType.Normal, damage);
                boss.Hit(damageInfo);
            }
        }
    }
}
