using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private Animator _animator;
    private PlayerMove _playerMove; // PlayerMove 클래스에 대한 참조

    [Header("체력 슬라이더 UI")]
    public Slider HealthSliderUI;

    public int Health;
    public int MaxHealth = 100;

    public int PlayerDamage = 20;
    public float AttackDelayTime = 1f;
    private float AttackTimer = 0f;

    public float attackRange = 2.5f; // 플레이어의 공격 범위

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _playerMove = GetComponent<PlayerMove>(); // PlayerMove 클래스에 대한 참조 초기화
        Health = MaxHealth;
        AttackTimer = 0f;
    }

    private void Update()
    {
        HealthSliderUI.value = Health / (float)MaxHealth;

        if (Input.GetMouseButtonDown(0) && AttackTimer <= 0f && _playerMove.Stamina >= 15)
        {
            Attack();
            AttackTimer = AttackDelayTime;
            _playerMove.ReduceStamina(15);
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
            HealthSliderUI.value = 0f;
            Health = 0;
            _animator.SetTrigger("Death");

            StartCoroutine(DeathWithDelay(5f)); // Death() 대신 Coroutine 호출
        }
    }

    private IEnumerator DeathWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Death();
    }

    public void Death()
    {
        gameObject.SetActive(false);
    }
}
