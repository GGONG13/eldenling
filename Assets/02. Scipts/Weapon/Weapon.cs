using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int damage = 20; // ���� ������
    public bool isAttacking = false; // ���� ���� ������ ����
    private bool hasDealtDamage = false; // �̹� ���ݿ��� �̹� �������� �־����� ����

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
}
