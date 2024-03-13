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
            // ���� Ȱ��ȭ�� �ͷ����� ã���ϴ�.
            Terrain terrain = Terrain.activeTerrain;
            // �ͷ����� �����͸� �����ɴϴ�.
            TerrainData terrainData = terrain.terrainData;

            // �ͷ����� ũ�⸦ �����ɴϴ�.
            Vector3 terrainSize = terrainData.size;

            // �ͷ����� �ʺ�(x)�� ����(z)�� �����մϴ�.
            float terrainWidth = terrainSize.x;
            float terrainLength = terrainSize.z;
            float x = Random.Range(0, terrainWidth);
            float z = Random.Range(0, terrainLength);
            // �ش� ��ġ�� �ͷ��� ���̸� �����ɴϴ�.
            float y = terrain.SampleHeight(new Vector3(x, 0, z)) + terrain.GetPosition().y;

            // ���������� ������ ��ġ�� ��ü�� ��ġ�մϴ�.
            Vector3 spawnPosition = new Vector3(x, y, z);
            if (i <= 15)
            {
                if (Treasure.Length > 0) // �迭�� ������� ������ Ȯ��
                {
                    Instantiate(Treasure[UnityEngine.Random.Range(0, Treasure.Length)], spawnPosition, Quaternion.identity);
                }
            }
            else
            {
                if (MonsterBox.Length > 0) // �迭�� ������� ������ Ȯ��
                {
                    Instantiate(MonsterBox[UnityEngine.Random.Range(0, MonsterBox.Length)], spawnPosition, Quaternion.identity);
                }
            }
        }
    }
}
