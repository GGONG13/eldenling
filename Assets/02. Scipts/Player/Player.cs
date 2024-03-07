using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IHitable
{
    public int Health;
    public int MaxHealth = 100;

    private void Start()
    {
        Health = MaxHealth;
    }
    public void Hit(DamageInfo damage)
    {
        Health -= damage.Amount;
        Debug.Log($"Player: {Health}");
        if (Health <= 0)
        {
            StopAllCoroutines();
            Health = 0;
            gameObject.SetActive(false);
        }
    }
}
