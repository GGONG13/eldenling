using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private EnemyState _state = EnemyState.Idle;
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
