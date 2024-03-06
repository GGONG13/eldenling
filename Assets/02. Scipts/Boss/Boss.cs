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

    public int Health;
    public int MaxHealth = 500;
    // public Slider HealthSliderUI;
    public int RunDamage = 10;
    public int NormalDamage = 5;
    public int CriticalDamage = 7;
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

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        Destination = _agent.transform.position;
        _target = GameObject.FindGameObjectWithTag("Player").transform;
        _delayTimer = 0f;
        _stiffTimer = 0f;
        Health = MaxHealth;
    }
    private void Update()
    {
        /*if (GameManager.Instance.state != State.Go)
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
        this.gameObject.SetActive( false );
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
    public void PlayerAttack()
    {
        IHitable playerHitable = _target.GetComponent<IHitable>();
        if (playerHitable != null)
        {
            DamageInfo damageInfo;
            if (_currentState == BossState.RunAttack)
            {
                damageInfo = new DamageInfo(DamageType.Run, RunDamage);
                playerHitable.Hit(damageInfo);
            }
            else if (_currentState == BossState.Attack)
            {
                int num = Random.Range(0, 10);
                if (num < 3)
                {
                    damageInfo = new DamageInfo(DamageType.Critical, CriticalDamage);
                    playerHitable.Hit(damageInfo);
                }
                else
                {
                    damageInfo = new DamageInfo(DamageType.Normal, NormalDamage);
                    playerHitable.Hit(damageInfo);
                }
            }         
        }
    }
    public void Hit(DamageInfo damage)
    {
        if (_currentState == BossState.Die)
        {
            return;
        }
        Health -= damage.Amount;
        if (_currentState == BossState.AttackDelay)
        {
            Debug.Log("Boss: AttackDelay -> Stiffness");
            _currentState = BossState.Stiffness;
        }
        if ( Health <= 0 )
        {
            Health = 0;
            Debug.Log("Boss: Any -> Die");
            _currentState = BossState.Die;
        }
    }
}
