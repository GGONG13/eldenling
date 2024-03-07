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

    public float viewRadius = 10f; // 시야 반경
    public LayerMask targetMask; // 대상 레이어 마스크

    private void Awake()
    {
        Health = MaxHealth;
        AttackTimer = 0f;

    }

    private void Update()
    {
        if (AttackTimer <= 0f && Input.GetMouseButtonDown(0))
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
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);
        foreach (Collider targetCollider in targetsInViewRadius)
        {
            // 대상이 IHitable을 구현한 오브젝트인지 확인
            IHitable target = targetCollider.GetComponent<IHitable>();
            if (target != null)
            {
                target.Hit(PlayerDamage);
                Debug.Log("Player attacked boss.");
                Boss boss = targetCollider.GetComponent<Boss>();
                if (boss != null)
                {
                    boss.PlayerAttack();
                    Debug.Log($"Boss Health: {boss.Health}");
                }
            }
        }
    }

    public void Hit(int damage)
    {
        Health -= damage;
        Debug.Log($"Player: {Health}");
        if (Health <= 0)
        {
            StopAllCoroutines();
            Health = 0;
            gameObject.SetActive(false);
        }
    }
}
