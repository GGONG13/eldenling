using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Purchasing;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;
public enum MonsterBoxState
{
    CloseIdel,
    Open,
    OpenIdel,
    Attack,
    Run,
    Hit,
    Stun,
    Death
}
public class MonsterBox : MonoBehaviour, IHitable
{
    public MonsterBoxState State = MonsterBoxState.CloseIdel;


    public float FindDistance = 6;
    public float AttactDistance = 2.5f;
    public float FollowDistance = 2f;

    private float moveSpeed = 5f;
    private Vector3 _dir;
    private Animator _animator;
    private Transform _playerTransform;
    private Player _player;
    private NavMeshAgent _navMeshAgent;
    private int Health;
    private int Maxhealth = 10;

    public Slider HealthSlider;
    public int Damage = 10;


    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _navMeshAgent = GetComponentInChildren<NavMeshAgent>();
        _navMeshAgent.speed = moveSpeed;
        GameObject playerGameObject = GameObject.FindWithTag("Player"); // 플레이어 태그 사용
        if (playerGameObject != null)
        {
            _playerTransform = playerGameObject.transform;
            _player = playerGameObject.GetComponent<Player>();
        }
    }

    void Update()
    {
        switch (State)
        {
            case MonsterBoxState.CloseIdel:
            {
                CloseIdel();
                break;
            }
            case MonsterBoxState.Open:
            {
                Open();
                break;
            }
            case MonsterBoxState.OpenIdel:
            {
                OpenIdle();
                break;
            }
            case MonsterBoxState.Attack:
            {
                Attack();
                break;
            }
            case MonsterBoxState.Run:
            {
                Run();
                break;
            }
            case MonsterBoxState.Hit:
            {
                break;
            }
            case MonsterBoxState.Stun:
            {
                Stan();
                break;
            }
            case MonsterBoxState.Death:
            {
                Death();
                break;
            }
        }

        RefreshSlider();
    }


    void CloseIdel()
    {
        Health = Maxhealth;
        HealthSlider.gameObject.SetActive(false);
        float distance = Vector3.Distance(transform.position, _playerTransform.position);
        if (distance < 2)
        {
            State = MonsterBoxState.Open;
        }
    }

    void Open()
    {
        _animator.SetTrigger("Open");
        HealthSlider.gameObject.SetActive(true);
        StartCoroutine(OpenCoroutine());
        float distance = Vector3.Distance(transform.position, _playerTransform.position);
        if (distance < 0.8f)
        {
            State = MonsterBoxState.Attack;
        }
        if (distance < 3)
        {
            State = MonsterBoxState.Run;
        }
    }

    void OpenIdle()
    {
        _animator.SetTrigger("OpenIdle");
        float distance = Vector3.Distance(transform.position, _playerTransform.position);
        if (distance < 0.8f)
        {
            State = MonsterBoxState.Attack;
        }
        else if (distance > FollowDistance) // FollowDistance보다 멀어진 경우 Run 상태로 전환
        {
            State = MonsterBoxState.Run;
        }
    }

    void Attack()
    {
        _animator.SetTrigger($"Attack {Random.Range(1, 4)}");
        float distance = Vector3.Distance(transform.position, _playerTransform.position);
/*        if (distance <= AttactDistance) // 공격 거리 내에 있을 때
        {
            DamageInfo damageInfo = new DamageInfo(DamageType.Normal, Damage);
            _player.Hit(damageInfo);
        }*/
        if (distance < 1)
        {
            State = MonsterBoxState.OpenIdel;
        }
        else if (distance > FollowDistance) // FollowDistance보다 멀어진 경우 Run 상태로 전환
        {
            State = MonsterBoxState.Run;
        }
    }

    void Run()
    {
        _animator.SetTrigger("Run");
        _dir = _playerTransform.position - transform.position;
        _dir.y = 0;
        _dir.Normalize();
        _navMeshAgent.destination = _playerTransform.position;

        _navMeshAgent.stoppingDistance = AttactDistance;

        float distance = Vector3.Distance(transform.position, _playerTransform.position);
        if (distance < 3f)
        {
            State = MonsterBoxState.Attack;
        }
    }

    void Stan()
    {
        _animator.SetTrigger("Stan");
        State = MonsterBoxState.Stun;
        return;
    }


    public void Hit (DamageInfo damage)
    {

        Health -= damage.Amount;
        Stan();
        RefreshSlider();
        if (Health <= 0)
        {
            State = MonsterBoxState.Death;
        }
        if (State == MonsterBoxState.Death)
        {
            return;
        }
    }
    public void Hit (int damageAmount)
    {
        Hit(new DamageInfo(DamageType.Normal, damageAmount));
    }

    void Death()
    {
        int random = UnityEngine.Random.Range(1, 4);
        switch (random) 
        {
            case 1:
                _animator.SetTrigger("Death1");
                break;
            case 2:
                _animator.SetTrigger("Death2");
                break;
            case 3:
                _animator.SetTrigger("Death3");
                break;
        }
        StartCoroutine(DeathCoroutine());
        RefreshSlider();
    }

    IEnumerator DeathCoroutine()
    {
        yield return new WaitForSeconds(1.5f);
        this.gameObject.SetActive(false);
    }

    IEnumerator OpenCoroutine()
    {
        yield return new WaitForSeconds(3f);
        State = MonsterBoxState.OpenIdel;
    }

    void RefreshSlider()
    {
        float healthRatio = (float)Health / Maxhealth;
        HealthSlider.value = healthRatio;
    }
}
