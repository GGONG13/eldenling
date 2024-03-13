using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxRandomSpawn : MonoBehaviour
{
    public GameObject[] Treasure;
    public GameObject[] MonsterBox;
    // Start is called before the first frame update
    void Start()
    {


        for (int i = 0; i < 30; i++)
        {
            float x = UnityEngine.Random.Range(-10, 10);
            float z = UnityEngine.Random.Range(-10, 10);
            Vector3 spawnPosition = new Vector3(x, 0, z);
            if (i <= 15)
            {
                if (Treasure.Length > 0) // 배열이 비어있지 않은지 확인
                {
                    Instantiate(Treasure[UnityEngine.Random.Range(0, Treasure.Length)], spawnPosition, Quaternion.identity);
                }
            }
            else
            {
                if (MonsterBox.Length > 0) // 배열이 비어있지 않은지 확인
                {
                    Instantiate(MonsterBox[UnityEngine.Random.Range(0, MonsterBox.Length)], spawnPosition, Quaternion.identity);
                }
            }
        }
    }
}
