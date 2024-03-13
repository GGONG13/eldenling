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
            // 현재 활성화된 터레인을 찾습니다.
            Terrain terrain = Terrain.activeTerrain;
            // 터레인의 데이터를 가져옵니다.
            TerrainData terrainData = terrain.terrainData;

            // 터레인의 크기를 가져옵니다.
            Vector3 terrainSize = terrainData.size;

            // 터레인의 너비(x)와 길이(z)를 참조합니다.
            float terrainWidth = terrainSize.x;
            float terrainLength = terrainSize.z;
            float x = Random.Range(0, terrainWidth);
            float z = Random.Range(0, terrainLength);
            // 해당 위치의 터레인 높이를 가져옵니다.
            float y = terrain.SampleHeight(new Vector3(x, 0, z)) + terrain.GetPosition().y;

            // 최종적으로 결정된 위치에 객체를 배치합니다.
            Vector3 spawnPosition = new Vector3(x, y, z);
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
