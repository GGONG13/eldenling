using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicArrow : MonoBehaviour
{
    public int damage = 15; // ȭ���� ���ϴ� ������
    public float magicRadius = 5f; // ȭ���� Ž���ϴ� ���� �ݰ�
    private Rigidbody rb; // ȭ���� Rigidbody ������Ʈ
    private Transform target; // ȭ���� ������ Ÿ�� ��
    public float speed = 5f; // ȭ���� �ӵ�
    public float rotateSpeed = 200f; // ȭ���� ȸ���ϴ� �ӵ�
    public GameObject MagicExplosion;

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Rigidbody ������Ʈ�� ������
        target = FindClosestEnemy(); // ���� ����� ���� ã��
    }

    void FixedUpdate()
    {
        if (target != null)
        {
            // ���� ��ġ�� �������� ������ ����� ��, Y������ 1��ŭ ���� ��ġ�� ���
            Vector3 targetPositionAdjusted = new Vector3(target.position.x, target.position.y + 1, target.position.z);
            Vector3 direction = (targetPositionAdjusted - transform.position).normalized; // Ÿ���� ���� ����
            Vector3 rotateAmount = Vector3.Cross(transform.forward, direction); // ȸ���ؾ� �� �� ���
            rb.angularVelocity = rotateAmount * rotateSpeed; // ȸ�� �ӵ� ����
            rb.velocity = transform.forward * speed; // ���� �ӵ� ����
        }
    }

    // ���� ����� ���� ã�� �޼���
    Transform FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy"); // ��� ���� ã��
        Transform closestEnemy = null;
        float closestDistance = magicRadius; // ������ �ݰ� ���� ���� ����� ���� ã�� ���� ����
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position); // ������ �Ÿ� ���
            if (distanceToEnemy < closestDistance)
            {
                closestDistance = distanceToEnemy; // ���� ����� �Ÿ� ������Ʈ
                closestEnemy = enemy.transform; // ���� ����� �� ������Ʈ
            }
        }
        return closestEnemy; // ���� ����� ���� Transform�� ��ȯ
    }

    // ȭ���� ���� �浹���� �� ȣ��� �޼���
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            // �� ��ü�κ��� Boss �Ǵ� Enemy ��ũ��Ʈ�� ������
            Boss boss = other.GetComponent<Boss>();
            Enemy enemyScript = other.GetComponent<Enemy>();

            Instantiate(MagicExplosion, transform.position, Quaternion.identity);
            if (boss != null)
            {
                // Boss���� �������� ��
                DamageInfo damageInfo = new DamageInfo(DamageType.Normal, damage);
                boss.Hit(damageInfo);
                Destroy(gameObject); // ȭ�� �ı�
            }
            if (enemyScript != null)
            {
                // Enemy���� �������� ��
                DamageInfo damageInfo = new DamageInfo(DamageType.Normal, damage);
                enemyScript.Hit(damageInfo);
                Destroy(gameObject); // ȭ�� �ı�
            }
        }
    }
}
