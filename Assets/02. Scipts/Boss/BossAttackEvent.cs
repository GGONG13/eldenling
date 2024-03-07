using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackEvent : MonoBehaviour
{
    private Boss _owner;
    private void Start()
    {
        _owner = GetComponent<Boss>();
    }
    public void AttackEvent()
    {
        _owner.PlayerAttack();
    }
}
