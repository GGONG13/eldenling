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
        //Debug.Log("�����̺�Ʈ �߻�");
        _owner.PlayerAttack();
    }
}
