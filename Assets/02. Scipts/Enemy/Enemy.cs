using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
    //private Animator _animator;
    private EnemyState _state;

    public int Health;
    public int MaxHealth = 50;

    private Transform _target;          //플레이어
    Vector3 _destination;
    public float FindDistance = 7f;     //감지 거리
    public float AttackDistance = 3f;   //공격 거리

    public float PatrolTime = 3f;
    private float _patrolTimer = 0f;
    public float PatrolRadius = 30f;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        //_animator = GetComponent<Animator>();
        _target = GameObject.FindGameObjectWithTag("Player").transform;
        _destination = transform.position;
        Init();
    }
    private void Init()
    {
        _state = EnemyState.Idle;
        Health = MaxHealth;
        _patrolTimer = 0;
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
            _state = EnemyState.Patrol;
            _patrolTimer = 0;
        }
        if (Vector3.Distance(_target.position, transform.position) <= FindDistance)
        {
            _state = EnemyState.Trace;
        }
    }
    public void Patrol()
    {

    }
    public void Return()
    {

    }
    public void Trace()
    {

    }
    public void Attack()
    {

    }
    public void Damaged()
    {

    }
    public void Death()
    {

    }
}
