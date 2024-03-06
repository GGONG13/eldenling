using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossMovement : MonoBehaviour
{
    public NavMeshAgent agent;
    public float MovementRange = 10f;
    public float Speed = 5f;
    private Vector3 Destination;
    public const float TOLERANCE = 0.1f;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        Destination = agent.transform.position;
    }
    private void Update()
    {
        if (transform.position == Destination)
        {
            MoveToRandomPosition();
        }      
    }
    private void MoveToRandomPosition()
    {
        Vector3 randomDirection = Random.insideUnitSphere * Speed;
        randomDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, Speed, NavMesh.AllAreas);
        Vector3 targetPosition = hit.position;
        agent.SetDestination(targetPosition);
        Destination = targetPosition;

        Debug.Log(targetPosition);
    }
}
