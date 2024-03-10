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
        Debug.Log("어택이벤트 발생");
        //_owner.PlayerAttack();
    }
}
