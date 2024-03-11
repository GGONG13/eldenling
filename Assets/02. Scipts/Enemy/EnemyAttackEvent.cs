using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackEvent : MonoBehaviour
{
    private Enemy _owner;
    private void Start()
    {
        _owner = GetComponent<Enemy>();
    }
    public void AttackEvent()
    {
        //Debug.Log("어택이벤트 발생");
        _owner.PlayerAttack();
    }
}
