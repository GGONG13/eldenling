using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public enum EnemyState
{
    Idle,
    Patrol,
    Return,
    Trace,
    Attack,
    Damaged,
    Death
}
public class Enemy : MonoBehaviour
{
    private NavMeshAgent _agent;
    private Animator _animator;
    private EnemyState _state;
    public const float TOLERANCE = 0.1f;
    private Coroutine _dieCoroutine;

    public int Health;
    public int MaxHealth = 50;
    public Slider HealthSliderUI;

    public float AttackDelay = 3f;
    private float _attackTimer = 0f;
    public int Damage = 5;

    private Transform _target;          //플레이어
    Vector3 Destination;
    Vector3 StartPosition;
    public float FindDistance = 10f;     //감지 거리
    public float AttackDistance = 1.5f;   //공격 거리
    public float MovementRange = 10f;
    public float PatrolRange = 20f;

    public float PatrolTime = 3f;
    private float _patrolTimer = 0f;
    public float PatrolRadius = 30f;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _target = GameObject.FindGameObjectWithTag("Player").transform;
        Init();
    }
    private void Init()
    {
        _state = EnemyState.Idle;
        Health = MaxHealth;
        _patrolTimer = 0;
        Destination = transform.position;
        StartPosition = transform.position;
        RefreshUI();
    }

    private void Update()
    {
        switch (_state)
        {
            case EnemyState.Idle:
                Idle(); break;
            case EnemyState.Patrol:
                Patrol(); break;
            case EnemyState.Return:
                Return(); break;
            case EnemyState.Trace:
                Trace(); break;
            case EnemyState.Attack:
                Attack(); break;
            case EnemyState.Damaged:
                Damaged(); break;
            case EnemyState.Death:
                Death(); break;
        }
    }
    public void Idle()
    {
        _patrolTimer += Time.deltaTime;
        if (_patrolTimer > PatrolTime)
        {
            Debug.Log("Enemy: Idle -> Patrol");
            _animator.SetTrigger("IdleToPatrol");
            _state = EnemyState.Patrol;
            MoveToRandomPosition();
        }
    }
    public void Patrol()
    {
        if (Vector3.Distance(transform.position, Destination) <= TOLERANCE)
        {
            MoveToRandomPosition();
        }
        if (Vector3.Distance(transform.position, _target.position) <= FindDistance)
        {
            Debug.Log("Enemy: Patrol -> Trace");
            _animator.SetTrigger("PatrolToTrace");
            _state = EnemyState.Trace;
        }
        if (Vector3.Distance(transform.position, StartPosition) > PatrolRange)
        {
            Debug.Log("Enemy: Patrol -> Return");
            _animator.SetTrigger("PatrolToReturn");
            _state = EnemyState.Return;
        }
    }
    public void Return()
    {
        _agent.destination = StartPosition;
        if (Vector3.Distance(transform.position, StartPosition) <= TOLERANCE)
        {
            Debug.Log("Enemy: Return -> Idle");
            _patrolTimer = 0;
            _animator.SetTrigger("ReturnToIdle");
            _state = EnemyState.Idle;
        }
    }
    public void Trace()
    {
        _agent.destination = _target.position;
        //transform.forward = _target.position;
        if (Vector3.Distance(transform.position, _target.position) > FindDistance)
        {
            Debug.Log("Enemy: Trace -> Patrol");
            _animator.SetTrigger("TraceToPatrol");
            _state = EnemyState.Patrol;
        }
        if (Vector3.Distance(transform.position, _target.position) <= AttackDistance)
        {
            Debug.Log("Enemy: Trace -> Attack");
            _animator.SetTrigger("TraceToAttack");
            _state = EnemyState.Attack;
        }
    }
    public void Attack()
    {
        _agent.stoppingDistance = AttackDistance;
        _agent.destination = _target.position;
        //transform.forward = _target.position;
        _attackTimer += Time.deltaTime;
        if (_attackTimer >= AttackDelay && Vector3.Distance(transform.position, _target.position) <= AttackDistance)
        {
            _animator.SetTrigger("Attack");
            _attackTimer = 0;
        }
        if (Vector3.Distance(transform.position, _target.position) > FindDistance)
        {
            Debug.Log("Enemy: Attack -> Trace");
            _attackTimer = 0;
            _animator.SetTrigger("AttackToTrace");
            _state = EnemyState.Trace;
        }
    }
    private void Damaged()
    {
        _animator.SetTrigger("Damaged");
        RefreshUI();
        Debug.Log("Enemy: Damaged -> Trace");
        _animator.SetTrigger("DamagedToTrace");
        _state = EnemyState.Trace;
    }
    public void Death()
    {
        if (_dieCoroutine == null)
        {
            _dieCoroutine = StartCoroutine(Die_Coroutine());
        }
    }
    public void PlayerAttack()
    {
        Collider[] targetsInRange = Physics.OverlapSphere(transform.position, FindDistance);
        foreach (Collider targetCollider in targetsInRange)
        {
            Player player = targetCollider.GetComponent<Player>();
            int num = Random.Range(0, 10);
            if (player != null)
            {
                DamageInfo damageInfo = new DamageInfo(DamageType.Normal, Damage);
                player.Hit(damageInfo);
            }
        }
    }

    private void MoveToRandomPosition()
    {
        Vector3 randomDirection = Random.insideUnitSphere * MovementRange;
        randomDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, MovementRange, NavMesh.AllAreas);
        Vector3 targetPosition = hit.position;
        _agent.SetDestination(targetPosition);
        Destination = targetPosition;
    }
    private void RefreshUI()
    {
        HealthSliderUI.value = Health / (float)MaxHealth;
    }
    public void Hit(DamageInfo damage)
    {
        if (_state == EnemyState.Death)
        {
            return;
        }
        Health -= damage.Amount;
        if (Health <= 0)
        {
            Health = 0;
            Debug.Log("Enemy: 어떤 상태든 -> 죽음");
            _state = EnemyState.Death;
        }
        else
        {
            _state = EnemyState.Damaged;
        }
        // 여기서 적의 체력을 출력합니다.
        Debug.Log($"적 체력: {Health}");
        RefreshUI();
    }
    private IEnumerator Die_Coroutine()
    {
        _animator.SetTrigger("Death");
        _agent.isStopped = true;
        _agent.ResetPath();
        HealthSliderUI.gameObject.SetActive(false);       
        yield return new WaitForSeconds(1f);
        CoinFactory.instance.CoinDrop(transform.position);
        //gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
