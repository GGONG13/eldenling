using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int damage = 20; // ���� ������
    public bool isAttacking = false; // ���� ���� ������ ����

    // ���� ����
    public void BeginAttack()
    {
        isAttacking = true;
    }

    // ���� ����
    public void EndAttack()
    {
        isAttacking = false;
    }

    // Ʈ���ſ� �� ĳ���Ͱ� ���Դ��� �˻�
    private void OnTriggerEnter(Collider other)
    {
        if (isAttacking && other.CompareTag("Enemy"))
        {
            Boss boss = other.GetComponent<Boss>();
            if (boss != null)
            {
                // ������ �������� �ִ� ����
                DamageInfo damageInfo = new DamageInfo(DamageType.Normal, damage);
                boss.Hit(damageInfo);
            }
        }
    }
}
