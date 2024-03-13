using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicArrow : MonoBehaviour
{
    public int damage = 15;
    public float magicRadius = 5f;
    private Rigidbody rb;
    private Transform target;
    public float speed = 10f;
    public float rotateSpeed = 200f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        target = FindClosestEnemy();
    }

    void FixedUpdate()
    {
        if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            Vector3 rotateAmount = Vector3.Cross(transform.forward, direction);
            rb.angularVelocity = rotateAmount * rotateSpeed;
            rb.velocity = transform.forward * speed;
        }
    }

    Transform FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Transform closestEnemy = null;
        float closestDistance = magicRadius;
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < closestDistance)
            {
                closestDistance = distanceToEnemy;
                closestEnemy = enemy.transform;
            }
        }
        return closestEnemy;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            // Assuming the enemy has a script with a method to take damage
            Boss boss = other.GetComponent<Boss>();
            Enemy enemyScript = other.GetComponent<Enemy>();
            if (boss != null)
            {
                // 적에게 데미지를 주는 로직
                DamageInfo damageInfo = new DamageInfo(DamageType.Normal, damage);
                boss.Hit(damageInfo);
                Destroy(gameObject);
            }
            if (enemyScript != null)
            {
                // 적에게 데미지를 주는 로직
                DamageInfo damageInfo = new DamageInfo(DamageType.Normal, damage);
                enemyScript.Hit(damageInfo);
                Destroy(gameObject);
            }
        }
    }
}
