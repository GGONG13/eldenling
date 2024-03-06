using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum BossState
{
    Patrol,
    Trace,
    RunAttack,
    Attack,
    AttackDelay,
    Stiffness,
    Die
}

public class Boss : MonoBehaviour
{
    private NavMeshAgent _agent;
    private Animator _animator;
    private BossState _currentState = BossState.Patrol;
    public const float TOLERANCE = 0.1f;

    public float MovementRange = 15f;
    
    private Vector3 Destination;
    private Transform _target; 
    public float FindDistance = 12f;
    public float RunAttackDistance = 8f;
    public float AttackDistance = 3.5f;
    public float DelayTime = 2f;
    private float _delayTimer = 0f;
    public float StiffTime = 1f;
    private float _stiffTimer = 0f;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        Destination = _agent.transform.position;
        _target = GameObject.FindGameObjectWithTag("Player").transform;
        _delayTimer = 0f;
        _stiffTimer = 0f;
    }
    private void Update()
    {
        /*if (GameManager.Instantiate.State != GameState.Go)
        {
            return;
        }*/
        switch (_currentState)
        {
            case BossState.Patrol:
                Patrol(); break;
            case BossState.Trace:
                Trace(); break;
            case BossState.RunAttack:
                RunAttack(); break;
            case BossState.Attack:
                Attack(); break;
            case BossState.AttackDelay:
                AttackDelay(); break;
            case BossState.Stiffness:
                Stiffness(); break;
            case BossState.Die:
                Die(); break;                
        }
             
    }
    private void Patrol()
    {
        if (Vector3.Distance(transform.position, Destination) <= TOLERANCE)
        {
            MoveToRandomPosition();
        }
        if (Vector3.Distance(_target.position, transform.position) <= FindDistance)
        {
            Debug.Log("Boss: Patrol -> Trace");
            _currentState = BossState.Trace;
        }
    }
    private void Trace()
    {
        Vector3 dir = _target.position - this.transform.position;
        dir.Normalize();
        _agent.stoppingDistance = AttackDistance;
        _agent.destination = _target.position;
        if (Vector3.Distance(_target.position, transform.position) <= AttackDistance)
        {
            Debug.Log("Boss: Trace -> Attack");
            _currentState = BossState.Attack;
        }
        if (Vector3.Distance(_target.position, transform.position) <= RunAttackDistance)
        {
            Debug.Log("Boss: Trace -> RunAttack");
            _currentState = BossState.RunAttack;
        }      
    }
    private void RunAttack()
    {
        _animator.SetTrigger("RunAttack");
        Debug.Log("Boss: RunAttack -> AttackDelay");
        _currentState = BossState.AttackDelay;
    }
    private void Attack()
    {
        Debug.Log("Boss: Attack -> AttackDelay");
        _currentState = BossState.AttackDelay;
    }
    private void AttackDelay()
    {
        _delayTimer += Time.deltaTime;
        if ( _delayTimer > DelayTime )
        {
            Debug.Log("Boss: AttackDelay -> Attack");
            _currentState = BossState.Attack;
            _delayTimer = 0;
        }
        // Stiffness
    }
    private void Stiffness()
    {
        _stiffTimer += Time.deltaTime;
        if( _stiffTimer > StiffTime )
        {
            Debug.Log("Boss: Stiffness -> Trace");
            _currentState = BossState.Trace;
        }
    }
    private void Die()
    {

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
}
