using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class BossAxe : MonoBehaviour
{
    public int NormalDamage = 20;
    public int CriticalDamage = 25;
    private bool _attack = true;

    public float AttackDelay = 2f;
    private float _attackTimer = 0;

    private void Update()
    {
        if (_attack == false)
        {
            _attackTimer += Time.deltaTime;
            if (_attackTimer > AttackDelay )
            {
                _attack = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_attack && other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                int num = Random.Range(0, 10);
                if (num < 3)
                {
                    DamageInfo damageInfo = new DamageInfo(DamageType.Normal, NormalDamage);
                    player.Hit(damageInfo);
                }
                else
                {
                    DamageInfo damageInfo = new DamageInfo(DamageType.Normal, CriticalDamage);
                    player.Hit(damageInfo);
                }               
                _attack = false;
            }
        }
    }
}
