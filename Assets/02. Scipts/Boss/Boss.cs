using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum BossState
{
    Patrol,
    Trace,
    RunAttack,
    AttackDelay,
    Stiffness,
    NormalAttack,
    CriticalAttack,
    Die
}

public class Boss : MonoBehaviour, IHitable
{
    private NavMeshAgent _agent;
    private Animator _animator;
    private BossState _currentState = BossState.Patrol;
    public const float TOLERANCE = 0.1f;
    private Coroutine _dieCoroutine;

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
            case BossState.AttackDelay:
                AttackDelay(); break;            
            case BossState.Stiffness:
                Stiffness(); break;
            case BossState.NormalAttack:
                NormalAttack(); break;
            case BossState.CriticalAttack:
                CriticalAttack(); break;
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
        PlayerTrace();
        if (Vector3.Distance(_target.position, transform.position) < AttackDistance)
        {

        }
        else if (Vector3.Distance(_target.position, transform.position) <= RunAttackDistance)
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
    private void AttackDelay()
    {
        PlayerTrace();
        _delayTimer += Time.deltaTime;
        if ( _delayTimer > DelayTime )
        {
            if (Vector3.Distance(_target.position, transform.position) <= AttackDistance)
            {
                int num = Random.Range(0, 10);
                if ( num < 3 )
                {
                    Debug.Log("Boss: Trace -> CriticalAttack");
                    _currentState = BossState.CriticalAttack;
                }
                else
                {
                    Debug.Log("Boss: Trace -> NormalAttack");
                    _currentState = BossState.NormalAttack;
                }        
            }
            if (Vector3.Distance(_target.position, transform.position) <= RunAttackDistance)
            {
                Debug.Log("Boss: Trace -> RunAttack");
                _currentState = BossState.RunAttack;
            }
        }
    }
    private void Stiffness()
    {
        _stiffTimer += Time.deltaTime;
        if( _stiffTimer > StiffTime )
        {
            Debug.Log("Boss: Stiffness -> Patrol");
            _currentState = BossState.Patrol;
        }
    }
    private void NormalAttack()
    {
        Debug.Log("Boss: NormalAttack -> AttackDelay");
        _currentState = BossState.AttackDelay;
    }
    private void CriticalAttack()
    {
        Debug.Log("Boss: CriticalAttack -> AttackDelay");
        _currentState = BossState.AttackDelay;
    }
    private void Die()
    {
        if (_dieCoroutine == null)
        {
            _dieCoroutine = StartCoroutine(Die_Coroutine());
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
                Debug.Log("Boss: Run Attack");
            }
            else if (_currentState == BossState.NormalAttack)
            {
                damageInfo = new DamageInfo(DamageType.Normal, NormalDamage);
                playerHitable.Hit(damageInfo);
                Debug.Log("Boss: Normal Attack");
            }
            else if (_currentState == BossState.CriticalAttack)
            {
                damageInfo = new DamageInfo(DamageType.Critical, CriticalDamage);
                playerHitable.Hit(damageInfo);
                Debug.Log("Boss: Critical Attack");
            }
        }
    }
    public void PlayerTrace()
    {
        Vector3 dir = _target.position - this.transform.position;
        dir.Normalize();
        _agent.stoppingDistance = AttackDistance;
        _agent.destination = _target.position;
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
    private IEnumerator Die_Coroutine()
    {
        _agent.isStopped = true;
        _agent.ResetPath();
        // HealthSliderUI.gameObject.SetActive(false);
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
