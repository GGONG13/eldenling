using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicArrow : MonoBehaviour
{
    public int damage = 15; // 화살이 가하는 데미지
    public float magicRadius = 5f; // 화살이 탐지하는 적의 반경
    private Rigidbody rb; // 화살의 Rigidbody 컴포넌트
    private Transform target; // 화살이 추적할 타겟 적
    public float speed = 5f; // 화살의 속도
    public float rotateSpeed = 200f; // 화살이 회전하는 속도
    public GameObject MagicExplosion;

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Rigidbody 컴포넌트를 가져옴
        target = FindClosestEnemy(); // 가장 가까운 적을 찾음
    }

    void FixedUpdate()
    {
        if (target != null)
        {
            // 적의 위치를 기준으로 방향을 계산할 때, Y축으로 1만큼 높은 위치를 사용
            Vector3 targetPositionAdjusted = new Vector3(target.position.x, target.position.y + 1, target.position.z);
            Vector3 direction = (targetPositionAdjusted - transform.position).normalized; // 타겟을 향한 방향
            Vector3 rotateAmount = Vector3.Cross(transform.forward, direction); // 회전해야 할 양 계산
            rb.angularVelocity = rotateAmount * rotateSpeed; // 회전 속도 적용
            rb.velocity = transform.forward * speed; // 전진 속도 적용
        }
    }

    // 가장 가까운 적을 찾는 메서드
    Transform FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy"); // 모든 적을 찾음
        Transform closestEnemy = null;
        float closestDistance = magicRadius; // 설정된 반경 내의 가장 가까운 적을 찾기 위한 변수
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position); // 적과의 거리 계산
            if (distanceToEnemy < closestDistance)
            {
                closestDistance = distanceToEnemy; // 가장 가까운 거리 업데이트
                closestEnemy = enemy.transform; // 가장 가까운 적 업데이트
            }
        }
        return closestEnemy; // 가장 가까운 적의 Transform을 반환
    }

    // 화살이 적과 충돌했을 때 호출될 메서드
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            // 적 객체로부터 Boss 또는 Enemy 스크립트를 가져옴
            Boss boss = other.GetComponent<Boss>();
            Enemy enemyScript = other.GetComponent<Enemy>();

            Instantiate(MagicExplosion, transform.position, Quaternion.identity);
            if (boss != null)
            {
                // Boss에게 데미지를 줌
                DamageInfo damageInfo = new DamageInfo(DamageType.Normal, damage);
                boss.Hit(damageInfo);
                Destroy(gameObject); // 화살 파괴
            }
            if (enemyScript != null)
            {
                // Enemy에게 데미지를 줌
                DamageInfo damageInfo = new DamageInfo(DamageType.Normal, damage);
                enemyScript.Hit(damageInfo);
                Destroy(gameObject); // 화살 파괴
            }
        }
    }
}
