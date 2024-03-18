using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    public GameObject EnemyPrefab;
    private List<GameObject> _enemyPool = null;
    public int PoolSize = 30;

    private float timer = 0f;
    private float spawnTime = 3f;

    public float MinTime = 2f;
    public float MaxTime = 5f;

    //public int MaxSpawn = 30;
    private int spawnCount = 0;

    public NavMeshSurface navMeshSurface; // NavMeshSurface ����
    private bool spawnEnabled = true;

    private void Awake()
    {
        _enemyPool = new List<GameObject>();
        for (int i = 0; i < PoolSize; i++)
        {
            GameObject enemy = Instantiate(EnemyPrefab);
            enemy.transform.SetParent(this.transform);
            enemy.gameObject.SetActive(false);
            _enemyPool.Add(enemy);
        }
    }
    private void Start()
    {
        spawnTime = Random.Range(MinTime, MaxTime);
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnTime)
        {
            EnemySpawn();
            timer = 0;
            spawnTime = Random.Range(MinTime, MaxTime);
        }
        if (spawnCount >= PoolSize)
        {
            spawnEnabled = false;
        }
    }
    private void EnemySpawn()
    {
        if (!spawnEnabled)
        {
            return;
        }
        GameObject enemy = GetAvailableEnemy();
        if (enemy != null)
        {
            // NavMesh ���� ������ ������ ��ġ ����
            Vector3 randomPosition = GetRandomNavMeshPosition();
            if (randomPosition != Vector3.zero)
            {
                enemy.transform.position = randomPosition;
                enemy.gameObject.SetActive(true);
                Debug.Log($"{spawnCount}");
            }
        }
    }

    private GameObject GetAvailableEnemy()
    {
        if (spawnCount < _enemyPool.Count)
        {
            GameObject enemy = _enemyPool[spawnCount];
            enemy.SetActive(true);
            spawnCount++;
            return enemy;
        }
        return null;
    }

    private Vector3 GetRandomNavMeshPosition()
    {
        NavMeshHit hit;
        Vector3 randomPosition = Vector3.zero;
        bool found = false;

        // NavMesh �������� ��ȿ�� ���� ��ġ ã��
        while (!found)
        {
            Vector3 randomDirection = Random.insideUnitSphere * Random.Range(0f, 10f);
            randomDirection += transform.position;
            if (NavMesh.SamplePosition(randomDirection, out hit, 10f, NavMesh.AllAreas))
            {
                randomPosition = hit.position;
                found = true;
            }
        }
        return randomPosition;
    }
}
