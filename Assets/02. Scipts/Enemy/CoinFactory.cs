using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinFactory : MonoBehaviour
{
    public static CoinFactory instance { get; private set; }

    public GameObject CoinPrefab;
    private List<GameObject> _coinPool = null;
    public int PoolSize = 10;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        _coinPool = new List<GameObject> ();
        for (int i = 0; i < PoolSize; i++)
        {
            GameObject coin = Instantiate(CoinPrefab);
            coin.transform.SetParent(this.transform);
            coin.gameObject.SetActive (false);
            _coinPool.Add (coin);
        }
    }

    public void CoinDrop(Vector3 position)
    {
        GameObject coin = null;
        int num = Random.Range(0, 2);
        if (num == 0)
        {
            foreach (GameObject c in _coinPool)
            {
                if (c.gameObject.activeInHierarchy == false)
                {
                    coin = c;
                    coin.transform.position = position;
                    coin.gameObject.SetActive(true);
                    break;
                }
            }           
        }
    }
}
