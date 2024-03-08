using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int Health;
    public int MaxHealth = 100;

    public int PlayerDamage = 20;
    public float AttackDelayTime = 1f;
    private float AttackTimer = 0f;

    public float attackRange = 2.5f; // 플레이어의 공격 범위

    private void Awake()
    {
        Health = MaxHealth;
        AttackTimer = 0f;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && AttackTimer <= 0f)
        {
            Attack();
            AttackTimer = AttackDelayTime;
        }

        if (AttackTimer > 0f)
        {
            AttackTimer -= Time.deltaTime;
        }
    }

    void Attack()
    {
        Collider[] targetsInRange = Physics.OverlapSphere(transform.position, attackRange);
        foreach (Collider targetCollider in targetsInRange)
        {
            Boss boss = targetCollider.GetComponent<Boss>();
            if (boss != null)
            {
                DamageInfo damageInfo = new DamageInfo(DamageType.Normal, PlayerDamage);
                boss.Hit(damageInfo);
                Debug.Log("플레이어가 보스를 공격했습니다.");
            }
        }
    }

    public void Hit(DamageInfo damage)
    {
        Health -= damage.Amount;
        Debug.Log($"Player: {Health}");
        if (Health <= 0)
        {
            StopAllCoroutines();
            Health = 0;
            gameObject.SetActive(false);
        }
    }
}
