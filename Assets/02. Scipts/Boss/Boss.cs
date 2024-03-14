using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

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

public class Boss : MonoBehaviour
{
    private NavMeshAgent _agent;
    public Animator _animator;
    public Animator _horseAnimator;
    private BossState _currentState = BossState.Patrol;
    private Coroutine _dieCoroutine;

    public int Health;
    public int MaxHealth = 500;
    public Slider BossSliderUI;
    public Image EnemyFelledImage;
    //public int NormalDamage = 5;
    //public int CriticalDamage = 7;
    public float MovementRange = 15f;
    public float AttackRadius = 15;
    public float ViewAngle = 90;
    
    private Vector3 Destination;
    private Transform _target; 
    public float FindDistance = 12f;
    public float RunAttackDistance = 8f;
    public float AttackDistance = 5f;
    public float StopDistance = 1.5f;
    public float DelayTime = 3f;
    private float _delayTimer = 0f;
    public float StiffTime = 2f;
    private float _stiffTimer = 0f;

    public float NewSpeed = 5f;
    public float Duration = 3f;
    private float originalSpeed;
    public float RotationSpeed = 0.5f;

    private void Awake()
    {
        _agent = GetComponentInParent<NavMeshAgent>();
        //_animator = GetComponent<Animator>();
        originalSpeed = _agent.speed;
        Destination = transform.position;
        _target = GameObject.FindGameObjectWithTag("Player").transform;
        _delayTimer = 0f;
        _stiffTimer = 0f;
        EnemyFelledImage.gameObject.SetActive(false);
        _currentState = BossState.Patrol;
        Health = MaxHealth;
        RefreshUI();
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
    public void RefreshUI()
    {
        BossSliderUI.value = Health / (float)MaxHealth;
    }
    private void Patrol()
    {
        if (Vector3.Distance(transform.position, Destination) <= StopDistance)
        {
            MoveToRandomPosition();
        }
        if (Vector3.Distance(_target.position, transform.position) <= FindDistance)
        {
            //Debug.Log("Boss: Patrol -> Trace");
            _currentState = BossState.Trace;
            _animator.SetTrigger("PatrolToTrace");
        }
    }
    private void Trace()
    {
        PlayerTrace();
        if (Vector3.Distance(_target.position, transform.position) <= RunAttackDistance)
        {
            //Debug.Log("Boss: Trace -> RunAttack");
            _currentState = BossState.RunAttack;
            _animator.SetTrigger("TraceToRunAttack");
            return;
        }
        else if (Vector3.Distance(_target.position, transform.position) <= AttackDistance)
        {
            //Debug.Log("Boss: Trace -> AttackDelay");
            _currentState = BossState.AttackDelay;
            _animator.SetTrigger("TraceToAttackDelay");
            _horseAnimator.SetTrigger("Attack");
            return;
        }
        else if (Vector3.Distance(_target.position, transform.position) > FindDistance)
        {
            //Debug.Log("Boss: Trace -> Patrol");
            _currentState = BossState.Patrol;
            _animator.SetTrigger("TraceToPatrol");
            _horseAnimator.SetTrigger("Trace");
        }
    }
    private void RunAttack()
    {
        StartCoroutine(_changeSpeedCoroutine());
        //Debug.Log("Boss: RunAttack");
        _currentState = BossState.AttackDelay;
    }
    private void AttackDelay()
    {     
        _delayTimer += Time.deltaTime;
        if ( _delayTimer > DelayTime )
        {
            if (Vector3.Distance(_target.position, transform.position) <= AttackDistance)
            {
                int num = Random.Range(0, 2);
                if ( num == 0 )
                {
                    if (Health < MaxHealth * 0.5)
                    {
                        _horseAnimator.SetTrigger("AttackDelay");
                    }
                    //Debug.Log("Boss: Trace -> CriticalAttack");
                    _currentState = BossState.CriticalAttack;
                    _animator.SetTrigger("CriticalAttack");
                    _delayTimer = 0;
                }
                else
                {
                    //Debug.Log("Boss: Trace -> NormalAttack");
                    _currentState = BossState.NormalAttack;
                    _animator.SetTrigger("NormalAttack");
                    _delayTimer = 0;
                }        
            }
            else
            {
                //Debug.Log("Boss: AttackDelay -> Trace");
                _currentState = BossState.Trace;
                _animator.SetTrigger("AttackDelayToTrace");
                _delayTimer = 0;
            }
        }
    }
    private void Stiffness()
    {
        //PlayerLook();
        _stiffTimer += Time.deltaTime;
        if( _stiffTimer > StiffTime )
        {
            //Debug.Log("Boss: Stiffness -> Patrol");
            _currentState = BossState.Patrol;
        }
    }
    private void NormalAttack()
    {
        //PlayerLook();
        _currentState = BossState.AttackDelay;
        //Debug.Log("Boss: NormalAttack");
    }
    private void CriticalAttack()
    {
        //PlayerLook();
        _currentState = BossState.AttackDelay;
        //Debug.Log("Boss: CriticalAttack");
    }
    /*public void PlayerAttack()
    {
        Collider[] targetsInRange = Physics.OverlapSphere(transform.position, AttackRadius);
        foreach (Collider targetCollider in targetsInRange)
        {
            Player player = targetCollider.GetComponent<Player>();
            int num = Random.Range(0, 10);
            if (player != null)
            {
                if (num < 3)
                {
                    DamageInfo damageInfo = new DamageInfo(DamageType.Critical, CriticalDamage);
                    player.Hit(damageInfo);
                }
                else
                {
                    DamageInfo damageInfo = new DamageInfo(DamageType.Normal, NormalDamage);
                    player.Hit(damageInfo);
                }           
            }
        }
    }*/
    private void Die()
    {
        if (_dieCoroutine == null)
        {
            _dieCoroutine = StartCoroutine(Die_Coroutine());
        }
    }
    /*private void PlayerLook()
    {
        Quaternion targetRotation = Quaternion.LookRotation(_target.position - transform.position);
        targetRotation.Normalize();
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * RotationSpeed);
        //this.transform.LookAt(_target.position);
    }*/
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
    private IEnumerator _changeSpeedCoroutine()
    {
        _agent.speed = NewSpeed;
        yield return new WaitForSeconds(Duration);
        _agent.speed = originalSpeed;
    }
    public void PlayerTrace()
    {
        Vector3 dir = _target.position - this.transform.position;
        dir.Normalize();
        _agent.stoppingDistance = StopDistance;
        _agent.destination = _target.position;
    }
    public void Hit(DamageInfo damage)
    {
        if (_currentState == BossState.Die)
        {
            return;
        }
        Health -= damage.Amount;
        if (_currentState == BossState.AttackDelay && Health > 0)
        {
            _animator.SetTrigger("Stiffness");
            _horseAnimator.SetTrigger("Stiffness");
            _currentState = BossState.Stiffness;
            Debug.Log("보스: 공격 딜레이 -> 스터너 상태");
        }
        if (Health <= 0)
        {
            Health = 0;
            Debug.Log("보스: 어떤 상태든 -> 죽음");
            _currentState = BossState.Die;
        }
        // 여기서 보스의 체력을 출력합니다.
        Debug.Log($"보스 체력: {Health}");
        RefreshUI();
    }
    private IEnumerator Die_Coroutine()
    {
        _animator.SetTrigger("Die");
        _horseAnimator.SetTrigger("Die");
        _agent.isStopped = true;
        _agent.ResetPath();
        BossSliderUI.gameObject.SetActive(false);
        yield return new WaitForSeconds(3f);
        EnemyFelledImage.gameObject.SetActive(true);
        //gameObject.SetActive(false);
    }
}
