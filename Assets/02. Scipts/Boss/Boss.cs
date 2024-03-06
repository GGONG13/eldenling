using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum BossState
{
    Patrol,
    Trace,
    RunAttack,
    CloseAttack,
    CriticalAttack,
    Stiffness,
    Die
}

public class Boss : MonoBehaviour
{
    public NavMeshAgent agent;
    public float MovementRange = 15f;
    private Vector3 Destination;
    public const float TOLERANCE = 0.1f;

    private BossState _currentState = BossState.Patrol;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        Destination = agent.transform.position;
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
            case BossState.CloseAttack:
                CloseAttack(); break;
            case BossState.CriticalAttack:
                CriticalAttack(); break;
            case BossState.Stiffness:
                Stiffness(); break;
            case BossState.Die:
                Die(); break;                
        }
             
    }
    private void Patrol()
    {
        // H_Walk_IP 애니메이션 재생
        if (Vector3.Distance(transform.position, Destination) <= TOLERANCE)
        {
            MoveToRandomPosition();
        }
    }
    private void Trace()
    {

    }
    private void RunAttack()
    {

    }
    private void CloseAttack()
    {

    }
    private void CriticalAttack()
    {

    }
    private void Stiffness()
    {

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
        agent.SetDestination(targetPosition);
        Destination = targetPosition;
    }
}
